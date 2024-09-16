namespace PureFix.Types
{
    /// <summary>
    /// Writes the contents of the message to a writer
    /// </summary>
    public interface IFixEncoder
    {
        public void Encode(IFixWriter writer);
    }
}
