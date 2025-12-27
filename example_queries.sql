-- Example SQL queries for analyzing FIX protocol data from CSV files
-- These queries assume you have loaded the CSV files as CTEs or tables

-- 1. Find all fields used in a specific message type (e.g., NewOrderSingle)
SELECT DISTINCT
    m.field_number,
    m.field_name,
    m.field_type,
    m.required,
    f.description
FROM messages m
JOIN fields f ON m.field_number = f.number
WHERE m.message_name = 'NewOrderSingle'
ORDER BY m.position;

-- 2. Find all messages that use a specific field (e.g., Symbol)
SELECT DISTINCT
    message_name,
    msgtype,
    msgcat
FROM messages
WHERE field_name = 'Symbol'
ORDER BY message_name;

-- 3. Get all enum values for a specific field type
SELECT
    field_name,
    enum,
    description
FROM enums
WHERE field_name = 'OrdType'
ORDER BY enum;

-- 4. Find all required fields across all messages
SELECT
    field_name,
    field_number,
    COUNT(DISTINCT message_name) as used_in_messages
FROM messages
WHERE required = 'Y'
GROUP BY field_name, field_number
ORDER BY used_in_messages DESC, field_name;

-- 5. Analyze component usage - which components are used most frequently
WITH component_usage AS (
    SELECT
        c.component_name,
        COUNT(DISTINCT m.message_name) as message_count
    FROM components c
    JOIN messages m ON c.field_name = m.field_name
    GROUP BY c.component_name
)
SELECT
    component_name,
    message_count
FROM component_usage
ORDER BY message_count DESC
LIMIT 10;

-- 6. Find all group fields (repeating groups) in messages
SELECT DISTINCT
    message_name,
    field_name,
    field_number
FROM messages
WHERE field_type IN ('GROUP', 'GROUP_COUNT', 'NUMINGROUP')
ORDER BY message_name, field_name;

-- 7. Get the complete field hierarchy for a specific component
SELECT
    component_name,
    field_name,
    field_type,
    required,
    component_path
FROM components
WHERE component_name = 'Instrument'
ORDER BY component_path, field_name;

-- 8. Find messages by category
SELECT
    msgcat,
    COUNT(DISTINCT message_name) as message_count,
    GROUP_CONCAT(DISTINCT message_name) as messages
FROM messages
GROUP BY msgcat;

-- 9. Identify fields that have enum values
SELECT DISTINCT
    e.field_name,
    e.field_number,
    f.type as field_type,
    COUNT(DISTINCT e.enum) as enum_count
FROM enums e
JOIN fields f ON e.field_number = f.number
GROUP BY e.field_name, e.field_number, f.type
ORDER BY enum_count DESC;

-- 10. Find all administrative vs application messages
SELECT
    msgcat,
    message_name,
    msgtype
FROM (
    SELECT DISTINCT
        message_name,
        msgtype,
        msgcat
    FROM messages
) m
ORDER BY msgcat, msgtype;