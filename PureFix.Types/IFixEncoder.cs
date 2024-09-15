namespace PureFix.Types
{
    public interface IFixEncoder
    {
        public void Encode(ElasticBuffer storage, Tags tags, byte delimiter) { }
    }
}
