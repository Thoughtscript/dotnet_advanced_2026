namespace asp_entity.Interfaces;

public class ServiceLifetimeInterfaceImpl : ServiceLifetimeInterface
{
    public string Name { get; } = nameof(ServiceLifetimeInterfaceImpl);

    public Guid InstanceId { get; } = Guid.NewGuid();

    public string GetImplementationPattern() => $"`{Name}` exposes instance identity to demonstrate service lifetimes.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes";
}