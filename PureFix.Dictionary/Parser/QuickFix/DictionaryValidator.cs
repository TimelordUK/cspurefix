using System.Xml;
using System.Xml.Linq;

namespace PureFix.Dictionary.Parser.QuickFix;

/// <summary>
/// Validates FIX dictionary XML for common errors like duplicates, missing references, etc.
/// </summary>
public class DictionaryValidator
{
    private readonly List<ParserValidationError> _errors = [];

    // Track what's been defined - case-sensitive for exact matching
    private readonly Dictionary<string, FieldDefinitionInfo> _fieldsByName = new(StringComparer.Ordinal);
    private readonly Dictionary<int, FieldDefinitionInfo> _fieldsByTag = [];
    private readonly Dictionary<string, ComponentDefinitionInfo> _componentsByName = new(StringComparer.Ordinal);
    private readonly Dictionary<string, MessageDefinitionInfo> _messagesByName = new(StringComparer.Ordinal);
    private readonly Dictionary<string, MessageDefinitionInfo> _messagesByMsgType = [];

    // Case-insensitive lookup for "did you mean" and detecting case variations
    private readonly Dictionary<string, string> _fieldNamesCaseInsensitive = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _componentNamesCaseInsensitive = new(StringComparer.OrdinalIgnoreCase);

    // Track what's been referenced (to find undefined references)
    private readonly HashSet<string> _referencedFields = new(StringComparer.Ordinal);
    private readonly HashSet<string> _referencedComponents = new(StringComparer.Ordinal);

    // All known field/component names for "did you mean" suggestions
    private readonly List<string> _allFieldNames = [];
    private readonly List<string> _allComponentNames = [];

    public IReadOnlyList<ParserValidationError> Errors => _errors;
    public bool HasErrors => _errors.Any(e => e.Severity == ValidationSeverity.Error);
    public bool HasWarnings => _errors.Any(e => e.Severity == ValidationSeverity.Warning);

    /// <summary>
    /// Validate the entire XML document
    /// </summary>
    public void Validate(XDocument doc)
    {
        // First pass: collect all definitions
        CollectFieldDefinitions(doc);
        CollectComponentDefinitions(doc);
        CollectMessageDefinitions(doc);

        // Second pass: validate references
        ValidateHeader(doc);
        ValidateTrailer(doc);
        ValidateComponentReferences(doc);
        ValidateMessageReferences(doc);

        // Third pass: check for unused definitions (warnings only)
        CheckUnusedDefinitions();
    }

    /// <summary>
    /// Throws DictionaryValidationException if there are any errors
    /// </summary>
    public void ThrowIfErrors()
    {
        if (HasErrors)
        {
            throw new DictionaryValidationException(_errors);
        }
    }

    #region Field Validation

    private void CollectFieldDefinitions(XDocument doc)
    {
        var fieldsNode = doc.Descendants("fields").FirstOrDefault();
        if (fieldsNode == null)
        {
            AddError("MISSING_FIELDS", "No <fields> section found in dictionary");
            return;
        }

        foreach (var field in fieldsNode.Elements("field"))
        {
            ValidateFieldDefinition(field);
        }
    }

    private void ValidateFieldDefinition(XElement field)
    {
        var name = field.Attribute("name")?.Value;
        var numberStr = field.Attribute("number")?.Value;
        var type = field.Attribute("type")?.Value;
        var lineInfo = (IXmlLineInfo)field;
        var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;

        // Check required attributes
        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("FIELD_NO_NAME", "Field definition missing 'name' attribute", lineNumber: lineNumber);
            return;
        }

        if (string.IsNullOrWhiteSpace(numberStr) || !int.TryParse(numberStr, out var tag))
        {
            AddError("FIELD_NO_TAG", $"Field '{name}' missing or invalid 'number' attribute", name, "field", lineNumber);
            return;
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            AddWarning("FIELD_NO_TYPE", $"Field '{name}' missing 'type' attribute, defaulting to STRING", name, "field", lineNumber);
        }

        _allFieldNames.Add(name);

        // Check for duplicate by name
        if (_fieldsByName.TryGetValue(name, out var existingByName))
        {
            AddError("DUPLICATE_FIELD_NAME",
                $"Duplicate field name '{name}' (tag {tag}). Previously defined with tag {existingByName.Tag}",
                name, "field", lineNumber,
                existingByName.Tag == tag ? "Remove the duplicate definition" : "Use unique field names");
            return;
        }

        // Check for duplicate by tag
        if (_fieldsByTag.TryGetValue(tag, out var existingByTag))
        {
            AddError("DUPLICATE_FIELD_TAG",
                $"Duplicate field tag {tag} for '{name}'. Tag already used by field '{existingByTag.Name}'",
                name, "field", lineNumber,
                "Each tag number must be unique");
            return;
        }

        var info = new FieldDefinitionInfo(name, tag, type ?? "STRING", lineNumber);
        _fieldsByName[name] = info;
        _fieldsByTag[tag] = info;
        _fieldNamesCaseInsensitive[name] = name; // Track for case-insensitive lookup

        // Validate enum values
        ValidateFieldEnums(field, name, lineNumber);
    }

    private void ValidateFieldEnums(XElement field, string fieldName, int? lineNumber)
    {
        var values = field.Elements("value").ToList();
        if (values.Count == 0) return;

        var seenEnumKeys = new HashSet<string>();
        var seenEnumDescriptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var value in values)
        {
            var enumKey = value.Attribute("enum")?.Value;
            var description = value.Attribute("description")?.Value;
            var valueLineInfo = (IXmlLineInfo)value;
            var valueLine = valueLineInfo.HasLineInfo() ? valueLineInfo.LineNumber : lineNumber;

            if (string.IsNullOrWhiteSpace(enumKey))
            {
                AddError("ENUM_NO_KEY", $"Field '{fieldName}' has enum value without 'enum' attribute",
                    fieldName, "field", valueLine);
                continue;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                AddWarning("ENUM_NO_DESC", $"Field '{fieldName}' enum '{enumKey}' has no description",
                    fieldName, "field", valueLine);
            }

            // Check for duplicate enum keys within the field
            if (!seenEnumKeys.Add(enumKey))
            {
                AddError("DUPLICATE_ENUM_KEY",
                    $"Field '{fieldName}' has duplicate enum key '{enumKey}'",
                    fieldName, "field", valueLine);
            }

            // Check for duplicate descriptions (warning - they might collide after sanitization)
            if (description != null && !seenEnumDescriptions.Add(description))
            {
                AddWarning("DUPLICATE_ENUM_DESC",
                    $"Field '{fieldName}' has duplicate enum description '{description}' which may cause naming conflicts",
                    fieldName, "field", valueLine);
            }
        }
    }

    #endregion

    #region Component Validation

    private void CollectComponentDefinitions(XDocument doc)
    {
        var componentsNode = doc.Descendants("components").FirstOrDefault();
        if (componentsNode == null)
        {
            // Components are optional
            return;
        }

        foreach (var component in componentsNode.Elements("component"))
        {
            ValidateComponentDefinition(component);
        }
    }

    private void ValidateComponentDefinition(XElement component)
    {
        var name = component.Attribute("name")?.Value;
        var lineInfo = (IXmlLineInfo)component;
        var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("COMPONENT_NO_NAME", "Component definition missing 'name' attribute", lineNumber: lineNumber);
            return;
        }

        _allComponentNames.Add(name);

        // Check for duplicate
        if (_componentsByName.TryGetValue(name, out var existing))
        {
            AddError("DUPLICATE_COMPONENT",
                $"Duplicate component name '{name}'",
                name, "component", lineNumber,
                $"Previously defined at line {existing.LineNumber}");
            return;
        }

        _componentsByName[name] = new ComponentDefinitionInfo(name, lineNumber);
        _componentNamesCaseInsensitive[name] = name; // Track for case-insensitive lookup

        // Validate fields within this component are defined
        ValidateFieldReferences(component, name, "component");
    }

    #endregion

    #region Message Validation

    private void CollectMessageDefinitions(XDocument doc)
    {
        var messagesNode = doc.Descendants("messages").FirstOrDefault();
        if (messagesNode == null)
        {
            AddError("MISSING_MESSAGES", "No <messages> section found in dictionary");
            return;
        }

        foreach (var message in messagesNode.Elements("message"))
        {
            ValidateMessageDefinition(message);
        }
    }

    private void ValidateMessageDefinition(XElement message)
    {
        var name = message.Attribute("name")?.Value;
        var msgType = message.Attribute("msgtype")?.Value;
        var lineInfo = (IXmlLineInfo)message;
        var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("MESSAGE_NO_NAME", "Message definition missing 'name' attribute", lineNumber: lineNumber);
            return;
        }

        if (string.IsNullOrWhiteSpace(msgType))
        {
            AddError("MESSAGE_NO_MSGTYPE", $"Message '{name}' missing 'msgtype' attribute",
                name, "message", lineNumber);
            return;
        }

        // Check for duplicate by name
        if (_messagesByName.TryGetValue(name, out var existingByName))
        {
            AddError("DUPLICATE_MESSAGE_NAME",
                $"Duplicate message name '{name}'",
                name, "message", lineNumber,
                $"Previously defined at line {existingByName.LineNumber}");
            return;
        }

        // Check for duplicate by msgtype
        if (_messagesByMsgType.TryGetValue(msgType, out var existingByType))
        {
            AddError("DUPLICATE_MESSAGE_TYPE",
                $"Duplicate message type '{msgType}' for message '{name}'. Type already used by '{existingByType.Name}'",
                name, "message", lineNumber);
            return;
        }

        var info = new MessageDefinitionInfo(name, msgType, lineNumber);
        _messagesByName[name] = info;
        _messagesByMsgType[msgType] = info;

        // Validate fields within this message are defined
        ValidateFieldReferences(message, name, "message");
    }

    #endregion

    #region Reference Validation

    private void ValidateHeader(XDocument doc)
    {
        var header = doc.Descendants("header").FirstOrDefault();
        if (header == null)
        {
            AddError("MISSING_HEADER", "No <header> section found in dictionary");
            return;
        }
        ValidateFieldReferences(header, "StandardHeader", "header");
    }

    private void ValidateTrailer(XDocument doc)
    {
        var trailer = doc.Descendants("trailer").FirstOrDefault();
        if (trailer == null)
        {
            AddError("MISSING_TRAILER", "No <trailer> section found in dictionary");
            return;
        }
        ValidateFieldReferences(trailer, "StandardTrailer", "trailer");
    }

    private void ValidateComponentReferences(XDocument doc)
    {
        var componentsNode = doc.Descendants("components").FirstOrDefault();
        if (componentsNode == null) return;

        foreach (var component in componentsNode.Elements("component"))
        {
            var name = component.Attribute("name")?.Value;
            if (name == null) continue;

            foreach (var compRef in component.Descendants("component"))
            {
                ValidateComponentReference(compRef, name, "component");
            }
        }
    }

    private void ValidateMessageReferences(XDocument doc)
    {
        var messagesNode = doc.Descendants("messages").FirstOrDefault();
        if (messagesNode == null) return;

        foreach (var message in messagesNode.Elements("message"))
        {
            var name = message.Attribute("name")?.Value;
            if (name == null) continue;

            foreach (var compRef in message.Descendants("component"))
            {
                ValidateComponentReference(compRef, name, "message");
            }
        }
    }

    private void ValidateFieldReferences(XElement container, string containerName, string containerType)
    {
        foreach (var fieldRef in container.Elements("field"))
        {
            var fieldName = fieldRef.Attribute("name")?.Value;
            var lineInfo = (IXmlLineInfo)fieldRef;
            var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                AddError("FIELD_REF_NO_NAME",
                    $"Field reference in {containerType} '{containerName}' missing 'name' attribute",
                    containerName, containerType, lineNumber);
                continue;
            }

            _referencedFields.Add(fieldName);

            if (!_fieldsByName.ContainsKey(fieldName))
            {
                // Check if it's a case mismatch first
                string? suggestion = null;
                if (_fieldNamesCaseInsensitive.TryGetValue(fieldName, out var correctCase))
                {
                    suggestion = correctCase;
                }
                else
                {
                    suggestion = FindSimilar(fieldName, _allFieldNames);
                }

                AddError("UNDEFINED_FIELD",
                    $"Field '{fieldName}' referenced in {containerType} '{containerName}' is not defined",
                    fieldName, "field reference", lineNumber,
                    suggestion != null ? $"Did you mean '{suggestion}'?" : "Add the field to the <fields> section");
            }
        }

        // Recursively check groups
        foreach (var group in container.Elements("group"))
        {
            var groupName = group.Attribute("name")?.Value ?? "unknown";
            _referencedFields.Add(groupName); // Group counter field

            if (!string.IsNullOrEmpty(groupName) && !_fieldsByName.ContainsKey(groupName))
            {
                var lineInfo = (IXmlLineInfo)group;
                var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;
                var suggestion = FindSimilar(groupName, _allFieldNames);

                AddError("UNDEFINED_GROUP_FIELD",
                    $"Group '{groupName}' in {containerType} '{containerName}' has no corresponding field definition (for the repeating count)",
                    groupName, "group", lineNumber,
                    suggestion != null ? $"Did you mean '{suggestion}'?" : "Add a NUMINGROUP field for this group");
            }

            ValidateFieldReferences(group, $"{containerName}.{groupName}", "group");
        }
    }

    private void ValidateComponentReference(XElement compRef, string containerName, string containerType)
    {
        var compName = compRef.Attribute("name")?.Value;
        var lineInfo = (IXmlLineInfo)compRef;
        var lineNumber = lineInfo.HasLineInfo() ? lineInfo.LineNumber : (int?)null;

        if (string.IsNullOrWhiteSpace(compName))
        {
            AddError("COMPONENT_REF_NO_NAME",
                $"Component reference in {containerType} '{containerName}' missing 'name' attribute",
                containerName, containerType, lineNumber);
            return;
        }

        _referencedComponents.Add(compName);

        if (!_componentsByName.ContainsKey(compName) &&
            compName != "StandardHeader" && compName != "StandardTrailer")
        {
            var suggestion = FindSimilar(compName, _allComponentNames);
            AddError("UNDEFINED_COMPONENT",
                $"Component '{compName}' referenced in {containerType} '{containerName}' is not defined",
                compName, "component reference", lineNumber,
                suggestion != null ? $"Did you mean '{suggestion}'?" : "Add the component to the <components> section");
        }
    }

    private void CheckUnusedDefinitions()
    {
        // Check for unused fields (warning only)
        foreach (var field in _fieldsByName.Values)
        {
            if (!_referencedFields.Contains(field.Name))
            {
                AddWarning("UNUSED_FIELD",
                    $"Field '{field.Name}' (tag {field.Tag}) is defined but never referenced",
                    field.Name, "field", field.LineNumber);
            }
        }

        // Check for unused components (warning only)
        foreach (var comp in _componentsByName.Values)
        {
            if (!_referencedComponents.Contains(comp.Name))
            {
                AddWarning("UNUSED_COMPONENT",
                    $"Component '{comp.Name}' is defined but never referenced",
                    comp.Name, "component", comp.LineNumber);
            }
        }
    }

    #endregion

    #region Helper Methods

    private void AddError(string code, string message, string? elementName = null,
        string? elementType = null, int? lineNumber = null, string? suggestion = null)
    {
        _errors.Add(new ParserValidationError(
            ValidationSeverity.Error, code, message, elementName, elementType, lineNumber, suggestion));
    }

    private void AddWarning(string code, string message, string? elementName = null,
        string? elementType = null, int? lineNumber = null, string? suggestion = null)
    {
        _errors.Add(new ParserValidationError(
            ValidationSeverity.Warning, code, message, elementName, elementType, lineNumber, suggestion));
    }

    /// <summary>
    /// Find a similar name using Levenshtein distance for "did you mean" suggestions
    /// </summary>
    private static string? FindSimilar(string input, IEnumerable<string> candidates)
    {
        string? bestMatch = null;
        var bestDistance = int.MaxValue;
        var maxDistance = Math.Max(3, input.Length / 2); // Allow up to half the length or 3 chars difference

        foreach (var candidate in candidates)
        {
            var distance = LevenshteinDistance(input.ToLowerInvariant(), candidate.ToLowerInvariant());
            if (distance < bestDistance && distance <= maxDistance)
            {
                bestDistance = distance;
                bestMatch = candidate;
            }
        }

        return bestMatch;
    }

    /// <summary>
    /// Calculate Levenshtein edit distance between two strings
    /// </summary>
    private static int LevenshteinDistance(string s1, string s2)
    {
        var n = s1.Length;
        var m = s2.Length;
        var d = new int[n + 1, m + 1];

        if (n == 0) return m;
        if (m == 0) return n;

        for (var i = 0; i <= n; i++) d[i, 0] = i;
        for (var j = 0; j <= m; j++) d[0, j] = j;

        for (var i = 1; i <= n; i++)
        {
            for (var j = 1; j <= m; j++)
            {
                var cost = s2[j - 1] == s1[i - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }

    #endregion

    #region Info Records

    private record FieldDefinitionInfo(string Name, int Tag, string Type, int? LineNumber);
    private record ComponentDefinitionInfo(string Name, int? LineNumber);
    private record MessageDefinitionInfo(string Name, string MsgType, int? LineNumber);

    #endregion
}
