namespace PureFix.LogMessageParser
{
    public class DictMeta {
        public string? Name { get; set; }
        public string? Dict { get; set; }
        public string? Type { get; set; }
        public string? Sender { get; set; }
    }

    public class  DictMetaSet
    {
        public List<DictMeta> Metas { get; set; } = [];
    }
}
