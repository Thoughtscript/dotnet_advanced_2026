namespace asp_entity.Interfaces;

public sealed class KeyedServiceInterfaceImpl(string name) : KeyedServiceInterface
{
    public string Name { get; } = name;

    public string GetImplementationPattern() => $"`{Name}` is selected through keyed service registration.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#keyed-services";
}