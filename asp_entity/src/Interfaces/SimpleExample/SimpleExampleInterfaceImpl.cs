namespace asp_entity.Interfaces;

public class SimpleExampleInterfaceImpl : SimpleExampleInterface
{
    public string Name { get; } = nameof(SimpleExampleInterfaceImpl);

    public string GetMessage() => "Simple dependency injection example.";

    public string GetImplementationPattern() => $"`{Name}` is registered and resolved through its interface.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/basics";
}