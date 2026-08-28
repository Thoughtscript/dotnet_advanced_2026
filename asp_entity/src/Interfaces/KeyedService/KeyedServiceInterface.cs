namespace asp_entity.Interfaces;

public interface KeyedServiceInterface
{
    string Name { get; }

    string GetImplementationPattern();

    string DocumentationReference();
}