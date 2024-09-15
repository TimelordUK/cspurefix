using System.Collections.Frozen;

namespace PureFix.Types
{
    public static class TagManager
    {
        private static readonly FrozenDictionary<string, TagMetaData> s_FixTypeToMetaData;
        private static readonly FrozenDictionary<TagType, TagMetaData> s_TagTypeToMetaData;

        static TagManager()
        {
            s_TagTypeToMetaData = new Dictionary<TagType, TagMetaData>()
            {
                {TagType.String, new TagMetaData{TagType = TagType.String, Type = typeof(string), TypeName = "string", Getter = "GetString", Writer = "WriteString"}},
                {TagType.Int, new TagMetaData{TagType = TagType.Int, Type = typeof(int), TypeName = "int", Getter = "GetInt32", Writer = "WriteWholeNumber"}},
                {TagType.Float, new TagMetaData{TagType = TagType.Float, Type = typeof(double), TypeName = "double", Getter = "GetDouble", Writer = "WriteNumber"}},
                {TagType.Boolean, new TagMetaData{TagType = TagType.Boolean, Type = typeof(bool), TypeName = "bool", Getter = "GetBool", Writer = "WriteBoolean"}},
                {TagType.UtcTimestamp, new TagMetaData{TagType = TagType.UtcTimestamp, Type = typeof(DateTime), TypeName = "DateTime", Getter = "GetDateTime", Writer = "WriteUtcTimeStamp"}},
                {TagType.UtcDateOnly, new TagMetaData{TagType = TagType.UtcDateOnly, Type = typeof(DateOnly), TypeName = "DateOnly", Getter = "GetDateOnly", Writer = "WriteUtcDateOnly"}},
                {TagType.UtcTimeOnly, new TagMetaData{TagType = TagType.UtcTimeOnly, Type = typeof(TimeOnly), TypeName = "TimeOnly", Getter = "GetTimeOnly", Writer = "WriteTimeOnly"}},
                {TagType.LocalDate, new TagMetaData{TagType = TagType.LocalDate, Type = typeof(DateOnly), TypeName = "DateOnly", Getter = "GetDateOnly", Writer = "WriteLocalDateOnly"}},
                {TagType.RawData, new TagMetaData{TagType = TagType.RawData, Type = typeof(byte[]), TypeName = "byte[]", Getter = "GetByteArray", Writer = "WriteBuffer"}},
                {TagType.Length, new TagMetaData{TagType = TagType.Length, Type = typeof(int), TypeName = "int", Getter = "GetInt32", Writer = "WriteWholeNumber"}},
                {TagType.MonthYear, new TagMetaData{TagType = TagType.MonthYear, Type = typeof(MonthYear), TypeName = "MonthYear", Getter = "GetMonthYear", Writer = "WriteMonthYear"}},
            }.ToFrozenDictionary();


            s_FixTypeToMetaData = new Dictionary<string, TagMetaData>()
            {
                {"currency", s_TagTypeToMetaData[TagType.String]},
                {"string", s_TagTypeToMetaData[TagType.String]},
                {"char", s_TagTypeToMetaData[TagType.String]},
                {"int", s_TagTypeToMetaData[TagType.Int]},
                {"numingroup", s_TagTypeToMetaData[TagType.Int]},
                {"seqnum", s_TagTypeToMetaData[TagType.Int]},
                
                {"qty", s_TagTypeToMetaData[TagType.Float]},
                {"percentage", s_TagTypeToMetaData[TagType.Float]},
                {"amt", s_TagTypeToMetaData[TagType.Float]},
                {"price", s_TagTypeToMetaData[TagType.Float]},
                {"priceoffset", s_TagTypeToMetaData[TagType.Float]},
                {"float", s_TagTypeToMetaData[TagType.Float]},
                
                {"length", s_TagTypeToMetaData[TagType.Length]},
                {"boolean", s_TagTypeToMetaData[TagType.Boolean]},
                {"utctimestamp", s_TagTypeToMetaData[TagType.UtcTimestamp]},

                {"localmktdate", s_TagTypeToMetaData[TagType.LocalDate]},
                {"utcdateonly", s_TagTypeToMetaData[TagType.UtcDateOnly]},
                {"utctimeonly", s_TagTypeToMetaData[TagType.UtcTimeOnly]},
                {"data", s_TagTypeToMetaData[TagType.RawData]},
                {"monthyear", s_TagTypeToMetaData[TagType.MonthYear]},
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

            var byTagType = new Dictionary<TagType, TagMetaData>();

            foreach(var metaData in s_FixTypeToMetaData.Values)
            {
                byTagType[metaData.TagType] = metaData;
            }

            s_TagTypeToMetaData = byTagType.ToFrozenDictionary();
        }

        /// <summary>
        /// Returns the TagType to use for a FIX type
        /// </summary>
        /// <param name="fixType"></param>
        /// <returns></returns>
        public static TagType ToType(string? fixType)
        {
            fixType = fixType ?? "string";
            if (s_FixTypeToMetaData.TryGetValue(fixType, out var metaData))
            {
                return metaData.TagType;
            }

            return s_FixTypeToMetaData["string"].TagType;
        }

        /// <summary>
        /// Returns the C# type to use to store a tag type
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public static string ToCsType(TagType tagType)
        {
            return s_TagTypeToMetaData[tagType].TypeName;
        }

        /// <summary>
        /// Returns meta data about a tag that can be used to generate classes
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public static TagMetaData GetTagMetaData(TagType tagType)
        {
            return s_TagTypeToMetaData[tagType];
        }
    }
}
