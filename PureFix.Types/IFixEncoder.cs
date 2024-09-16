namespace PureFix.Types
{
    public interface IFixEncoder
    {
        public void Encode(IFixWriter writer);
    }
}
