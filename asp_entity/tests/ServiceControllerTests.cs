using Microsoft.AspNetCore.Mvc;
using Xunit;
using asp_entity.Controllers;
using asp_entity.Interfaces;
using asp_entity.Services;

namespace asp_entity.Tests;

public class ServiceControllerTests
{
    [Fact]
    public void SimpleExample_returns_implementation_description()
    {
        var result = CreateController().SimpleExample();

        var description = Assert.IsType<ImplementationDescription>(Assert.IsType<OkObjectResult>(result).Value);
        Assert.Contains("registered and resolved", description.GetImplementationPattern);
        Assert.Equal("https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/basics", description.DocumentationReference);
    }

    [Fact]
    public void ServiceLifetime_returns_implementation_description()
    {
        var result = CreateController().ServiceLifetime();

        var description = Assert.IsType<ImplementationDescription>(Assert.IsType<OkObjectResult>(result).Value);
        Assert.Contains("service lifetimes", description.GetImplementationPattern);
        Assert.Equal("https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes", description.DocumentationReference);
    }

    [Fact]
    public void KeyedServices_returns_both_keyed_implementations()
    {
        var result = CreateController().KeyedServices();

        var descriptions = Assert.IsAssignableFrom<IEnumerable<ImplementationDescription>>(Assert.IsType<OkObjectResult>(result).Value).ToList();
        Assert.Equal(2, descriptions.Count);
        Assert.Contains(descriptions, description => description.GetImplementationPattern.Contains("keyed service registration"));
        Assert.Contains(descriptions, description => description.GetImplementationPattern.Contains("second keyed service implementation"));
    }

    [Fact]
    public void EnumerableServices_returns_both_enumerable_implementations()
    {
        var result = CreateController().EnumerableServices();

        var descriptions = Assert.IsAssignableFrom<IEnumerable<ImplementationDescription>>(Assert.IsType<OkObjectResult>(result).Value).ToList();
        Assert.Equal(2, descriptions.Count);
        Assert.All(descriptions, description => Assert.Contains("IEnumerable collection", description.GetImplementationPattern));
        Assert.All(descriptions, description => Assert.Contains("dependency-injection#register-multiple-services", description.DocumentationReference));
    }

    [Fact]
    public void ThreadSafeSingleton_returns_implementation_description()
    {
        var result = CreateController().ThreadSafeSingleton();

        var description = Assert.IsType<ImplementationDescription>(Assert.IsType<OkObjectResult>(result).Value);
        Assert.Contains("Interlocked", description.GetImplementationPattern);
        Assert.Contains("thread-safety", description.DocumentationReference);
    }

    private static ServiceController CreateController()
    {
        var service = new DependencyInjectionService(
            new SimpleExampleInterfaceImpl(),
            [
                new ServiceLifetimeInterfaceImpl(),
                new ServiceLifetimeInterfaceImpl(),
                new ServiceLifetimeInterfaceImpl()
            ],
            new KeyedServiceInterfaceImpl("first"),
            new KeyedServiceInterfaceSecondImpl("second"),
            [
                new IEnumerableInterfaceImpl("first"),
                new IEnumerableInterfaceSecondImpl("second")
            ],
            new ThreadSafeSingletonInterfaceImpl());

        return new ServiceController(service);
    }
}