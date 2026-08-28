using System.Threading;

namespace asp_entity.Interfaces;

public sealed class ThreadSafeSingletonInterfaceImpl : ThreadSafeSingletonInterface
{
    private int _value;

    public string Name { get; } = nameof(ThreadSafeSingletonInterfaceImpl);

    public int IncrementAndGet() => Interlocked.Increment(ref _value);

    public string GetImplementationPattern() => $"`{Name}` is registered with .NET's dependency injection container as a singleton. The container creates and caches one instance, coordinating singleton creation and lazy initialization safely when multiple threads request it. That guarantee covers the service lifetime and construction; it does not make mutable fields or method operations thread-safe. Because IncrementAndGet updates shared state that can be accessed concurrently, Interlocked.Increment is required to make the read-modify-write operation atomic and prevent lost updates.";

    public string DocumentationReference() => "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes#thread-safety";
}