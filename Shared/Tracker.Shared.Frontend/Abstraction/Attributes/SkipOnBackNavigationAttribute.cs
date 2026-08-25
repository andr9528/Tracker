namespace Tracker.Shared.Frontend.Abstraction.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class SkipOnBackNavigationAttribute : Attribute
    {
    }
}
