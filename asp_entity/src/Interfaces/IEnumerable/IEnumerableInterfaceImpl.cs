namespace asp_entity.Interfaces;

public sealed class IEnumerableInterfaceImpl(string name) : IEnumerableInterface
{
    public string Name { get; } = name;

    public string GetImplementationPattern() => $"`{Name}` is one implementation returned through an IEnumerable collection.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#register-multiple-services";
}