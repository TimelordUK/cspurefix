using PureFix.Types;

namespace PureFix.Test.ModularTypes.Helpers
{
    internal class TestClock : IFixClock
    {
        public DateTime Current { get; set; }
    }
}
