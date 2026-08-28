namespace asp_entity.Interfaces;

public interface ServiceLifetimeInterface
{
    string Name { get; }

    Guid InstanceId { get; }

    string GetImplementationPattern();

    string DocumentationReference();
}