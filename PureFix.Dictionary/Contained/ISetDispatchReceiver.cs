namespace PureFix.Dictionary.Contained
{
    public interface ISetDispatchReceiver
    {
        void OnSimple(ContainedSimpleField sf, int index, object? peek);
        void OnComponent(ContainedComponentField cf, int index);
        void OnGroup(ContainedGroupField cf, int index);

        void PreIterate(IContainedSet containedSet)
        {
        }

        void PostIterate(IContainedSet containedSet)
        {
        }
    }
}
