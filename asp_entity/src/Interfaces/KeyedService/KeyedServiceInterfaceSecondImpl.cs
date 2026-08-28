namespace asp_entity.Interfaces;

public sealed class KeyedServiceInterfaceSecondImpl(string name) : KeyedServiceInterface
{
    public string Name { get; } = name;

    public string GetImplementationPattern() => $"`{Name}` is selected through the second keyed service implementation.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#keyed-services";
}