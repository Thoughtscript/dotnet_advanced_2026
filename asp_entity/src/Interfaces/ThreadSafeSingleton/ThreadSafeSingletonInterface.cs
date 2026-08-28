namespace asp_entity.Interfaces;

public interface ThreadSafeSingletonInterface
{
    string Name { get; }

    int IncrementAndGet();

    string GetImplementationPattern();

    string DocumentationReference();
}