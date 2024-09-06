using TemplateDotNet.Shared.Enum;

namespace TemplateDotNet.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionAttribute(DI di) : Attribute
    {
        public DI Di { get; private set; } = di;
    }
}
