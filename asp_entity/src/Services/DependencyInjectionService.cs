using Microsoft.Extensions.DependencyInjection;
using asp_entity.Interfaces;

namespace asp_entity.Services;

public sealed class DependencyInjectionService(
    SimpleExampleInterface simpleExample,
    IEnumerable<ServiceLifetimeInterface> serviceLifetimes,
    [FromKeyedServices("first")] KeyedServiceInterface firstKeyedService,
    [FromKeyedServices("second")] KeyedServiceInterface secondKeyedService,
    IEnumerable<IEnumerableInterface> enumerableServices,
    ThreadSafeSingletonInterface threadSafeSingleton)
{
    public ImplementationDescription GetSimpleExample() => Describe(simpleExample);

    public ImplementationDescription GetServiceLifetime() => Describe(serviceLifetimes.Last());

    public IEnumerable<ImplementationDescription> GetKeyedServices() =>
        [Describe(firstKeyedService), Describe(secondKeyedService)];

    public IEnumerable<ImplementationDescription> GetEnumerableServices() =>
        enumerableServices.Select(Describe);

    public ImplementationDescription GetThreadSafeSingleton() => Describe(threadSafeSingleton);

    private static ImplementationDescription Describe(SimpleExampleInterface implementation) =>
        new(
            implementation.GetImplementationPattern(),
            implementation.DocumentationReference());

    private static ImplementationDescription Describe(ServiceLifetimeInterface implementation) =>
        new(
            implementation.GetImplementationPattern(),
            implementation.DocumentationReference());

    private static ImplementationDescription Describe(KeyedServiceInterface implementation) =>
        new(
            implementation.GetImplementationPattern(),
            implementation.DocumentationReference());

    private static ImplementationDescription Describe(IEnumerableInterface implementation) =>
        new(
            implementation.GetImplementationPattern(),
            implementation.DocumentationReference());

    private static ImplementationDescription Describe(ThreadSafeSingletonInterface implementation) =>
        new(
            implementation.GetImplementationPattern(),
            implementation.DocumentationReference());
}