#!/usr/bin/env python3

import xml.etree.ElementTree as ET
import csv
import sys
from pathlib import Path
from collections import defaultdict, deque

def parse_fix_xml(xml_file):
    """Parse FIX data dictionary XML file into structured data."""
    tree = ET.parse(xml_file)
    root = tree.getroot()

    # Extract version info
    version_info = {
        'type': root.get('type', 'FIX'),
        'major': root.get('major', ''),
        'minor': root.get('minor', ''),
        'servicepack': root.get('servicepack', '0')
    }

    fields = {}
    enums = defaultdict(list)
    components = {}
    messages = {}

    # Parse fields with their enums
    fields_elem = root.find('fields')
    if fields_elem:
        for field in fields_elem.findall('field'):
            field_data = {
                'number': field.get('number'),
                'name': field.get('name'),
                'type': field.get('type'),
                'description': field.text.strip() if field.text else ''
            }
            fields[field_data['name']] = field_data

            # Parse enum values for this field
            for value in field.findall('value'):
                enum_data = {
                    'field_number': field_data['number'],
                    'field_name': field_data['name'],
                    'enum': value.get('enum'),
                    'description': value.get('description', '')
                }
                enums[field_data['name']].append(enum_data)

    # Parse components (including header and trailer)
    def parse_component_fields(component_elem):
        component_fields = []
        for child in component_elem:
            if child.tag == 'field':
                component_fields.append({
                    'type': 'field',
                    'name': child.get('name'),
                    'required': child.get('required', 'N')
                })
            elif child.tag == 'group':
                group_fields = []
                for group_field in child:
                    if group_field.tag == 'field':
                        group_fields.append({
                            'type': 'field',
                            'name': group_field.get('name'),
                            'required': group_field.get('required', 'N')
                        })
                    elif group_field.tag == 'component':
                        group_fields.append({
                            'type': 'component',
                            'name': group_field.get('name'),
                            'required': group_field.get('required', 'N')
                        })
                component_fields.append({
                    'type': 'group',
                    'name': child.get('name'),
                    'required': child.get('required', 'N'),
                    'fields': group_fields
                })
            elif child.tag == 'component':
                component_fields.append({
                    'type': 'component',
                    'name': child.get('name'),
                    'required': child.get('required', 'N')
                })
        return component_fields

    # Parse header
    header_elem = root.find('header')
    if header_elem:
        components['Header'] = {
            'name': 'Header',
            'fields': parse_component_fields(header_elem)
        }

    # Parse trailer
    trailer_elem = root.find('trailer')
    if trailer_elem:
        components['Trailer'] = {
            'name': 'Trailer',
            'fields': parse_component_fields(trailer_elem)
        }

    # Parse components section
    components_elem = root.find('components')
    if components_elem:
        for component in components_elem.findall('component'):
            comp_name = component.get('name')
            components[comp_name] = {
                'name': comp_name,
                'fields': parse_component_fields(component)
            }

    # Parse messages
    messages_elem = root.find('messages')
    if messages_elem:
        for message in messages_elem.findall('message'):
            msg_data = {
                'name': message.get('name'),
                'msgtype': message.get('msgtype'),
                'msgcat': message.get('msgcat', 'app'),
                'fields': parse_component_fields(message)
            }
            messages[msg_data['name']] = msg_data

    return version_info, fields, enums, components, messages


def expand_component_fields(component_name, components, fields, visited=None):
    """Recursively expand component references to get all contained fields."""
    if visited is None:
        visited = set()

    if component_name in visited:
        # Circular reference protection
        return []

    visited.add(component_name)
    expanded_fields = []

    if component_name not in components:
        return []

    for field_def in components[component_name]['fields']:
        if field_def['type'] == 'field':
            if field_def['name'] in fields:
                field_info = fields[field_def['name']]
                expanded_fields.append({
                    'field_number': field_info['number'],
                    'field_name': field_info['name'],
                    'field_type': field_info['type'],
                    'required': field_def['required'],
                    'component_path': component_name
                })
        elif field_def['type'] == 'component':
            # Recursively expand nested component
            nested_fields = expand_component_fields(
                field_def['name'], components, fields, visited.copy()
            )
            for nf in nested_fields:
                nf['component_path'] = f"{component_name}/{nf.get('component_path', field_def['name'])}"
            expanded_fields.extend(nested_fields)
        elif field_def['type'] == 'group':
            # Groups are special - they contain repeating fields
            group_name = field_def['name']
            if group_name in fields:
                field_info = fields[group_name]
                expanded_fields.append({
                    'field_number': field_info['number'],
                    'field_name': field_info['name'],
                    'field_type': 'GROUP',
                    'required': field_def['required'],
                    'component_path': component_name
                })
            # Add group sub-fields
            if 'fields' in field_def:
                for gf in field_def['fields']:
                    if gf['type'] == 'field' and gf['name'] in fields:
                        field_info = fields[gf['name']]
                        expanded_fields.append({
                            'field_number': field_info['number'],
                            'field_name': field_info['name'],
                            'field_type': field_info['type'],
                            'required': gf['required'],
                            'component_path': f"{component_name}/{group_name}"
                        })

    return expanded_fields


def write_fields_csv(fields, output_file):
    """Write fields to CSV."""
    with open(output_file, 'w', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=['number', 'name', 'type', 'description'])
        writer.writeheader()
        for field in sorted(fields.values(), key=lambda x: int(x['number'])):
            writer.writerow(field)


def write_enums_csv(enums, output_file):
    """Write enum values to CSV."""
    with open(output_file, 'w', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=['field_number', 'field_name', 'enum', 'description'])
        writer.writeheader()
        for field_name in sorted(enums.keys()):
            for enum in enums[field_name]:
                writer.writerow(enum)


def write_components_csv(components, fields, output_file):
    """Write components with their expanded fields to CSV."""
    with open(output_file, 'w', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=['component_name', 'field_number', 'field_name',
                                               'field_type', 'required', 'component_path'])
        writer.writeheader()

        for comp_name in sorted(components.keys()):
            expanded = expand_component_fields(comp_name, components, fields)
            for field in expanded:
                writer.writerow({
                    'component_name': comp_name,
                    **field
                })


def write_messages_csv(messages, components, fields, output_file):
    """Write messages with their fields to CSV."""
    with open(output_file, 'w', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=['message_name', 'msgtype', 'msgcat',
                                               'field_number', 'field_name', 'field_type',
                                               'required', 'position'])
        writer.writeheader()

        for msg_name in sorted(messages.keys()):
            msg = messages[msg_name]
            position = 0

            # Add header fields first (for non-FIXT messages)
            if 'Header' in components:
                for field in expand_component_fields('Header', components, fields):
                    position += 1
                    writer.writerow({
                        'message_name': msg_name,
                        'msgtype': msg['msgtype'],
                        'msgcat': msg['msgcat'],
                        'field_number': field['field_number'],
                        'field_name': field['field_name'],
                        'field_type': field['field_type'],
                        'required': field['required'],
                        'position': position
                    })

            # Add message-specific fields
            for field_def in msg['fields']:
                if field_def['type'] == 'field' and field_def['name'] in fields:
                    position += 1
                    field_info = fields[field_def['name']]
                    writer.writerow({
                        'message_name': msg_name,
                        'msgtype': msg['msgtype'],
                        'msgcat': msg['msgcat'],
                        'field_number': field_info['number'],
                        'field_name': field_info['name'],
                        'field_type': field_info['type'],
                        'required': field_def['required'],
                        'position': position
                    })
                elif field_def['type'] == 'component':
                    for field in expand_component_fields(field_def['name'], components, fields):
                        position += 1
                        writer.writerow({
                            'message_name': msg_name,
                            'msgtype': msg['msgtype'],
                            'msgcat': msg['msgcat'],
                            'field_number': field['field_number'],
                            'field_name': field['field_name'],
                            'field_type': field['field_type'],
                            'required': field['required'],
                            'position': position
                        })
                elif field_def['type'] == 'group':
                    # Add the group count field
                    if field_def['name'] in fields:
                        position += 1
                        field_info = fields[field_def['name']]
                        writer.writerow({
                            'message_name': msg_name,
                            'msgtype': msg['msgtype'],
                            'msgcat': msg['msgcat'],
                            'field_number': field_info['number'],
                            'field_name': field_info['name'],
                            'field_type': 'GROUP_COUNT',
                            'required': field_def['required'],
                            'position': position
                        })

            # Add trailer fields last
            if 'Trailer' in components:
                for field in expand_component_fields('Trailer', components, fields):
                    position += 1
                    writer.writerow({
                        'message_name': msg_name,
                        'msgtype': msg['msgtype'],
                        'msgcat': msg['msgcat'],
                        'field_number': field['field_number'],
                        'field_name': field['field_name'],
                        'field_type': field['field_type'],
                        'required': field['required'],
                        'position': position
                    })


def main():
    if len(sys.argv) < 2:
        print("Usage: python fix_xml_to_csv.py <FIX_XML_FILE> [OUTPUT_DIR]")
        sys.exit(1)

    xml_file = Path(sys.argv[1])
    output_dir = Path(sys.argv[2]) if len(sys.argv) > 2 else Path('.')

    if not xml_file.exists():
        print(f"Error: File {xml_file} not found")
        sys.exit(1)

    output_dir.mkdir(exist_ok=True)

    print(f"Parsing {xml_file}...")
    version_info, fields, enums, components, messages = parse_fix_xml(xml_file)

    # Generate output filenames based on input file
    base_name = xml_file.stem

    # Write CSVs
    fields_file = output_dir / f"{base_name}_fields.csv"
    print(f"Writing {len(fields)} fields to {fields_file}...")
    write_fields_csv(fields, fields_file)

    enums_file = output_dir / f"{base_name}_enums.csv"
    print(f"Writing enum values to {enums_file}...")
    write_enums_csv(enums, enums_file)

    components_file = output_dir / f"{base_name}_components.csv"
    print(f"Writing {len(components)} components to {components_file}...")
    write_components_csv(components, fields, components_file)

    messages_file = output_dir / f"{base_name}_messages.csv"
    print(f"Writing {len(messages)} messages to {messages_file}...")
    write_messages_csv(messages, components, fields, messages_file)

    print(f"\nConversion complete! Generated files:")
    print(f"  - {fields_file}")
    print(f"  - {enums_file}")
    print(f"  - {components_file}")
    print(f"  - {messages_file}")

    print(f"\nVersion: FIX {version_info['major']}.{version_info['minor']} SP{version_info['servicepack']}")
    print(f"Summary:")
    print(f"  - Fields: {len(fields)}")
    print(f"  - Enum values: {sum(len(v) for v in enums.values())}")
    print(f"  - Components: {len(components)}")
    print(f"  - Messages: {len(messages)}")


if __name__ == "__main__":
    main()