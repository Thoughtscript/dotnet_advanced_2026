using Microsoft.AspNetCore.Mvc;
using asp_entity.Services;

namespace asp_entity.Controllers;

public class ServiceController : Controller
{
    private readonly DependencyInjectionService _service;

    public ServiceController(DependencyInjectionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult SimpleExample() => Ok(_service.GetSimpleExample());

    [HttpGet]
    public IActionResult ServiceLifetime() => Ok(_service.GetServiceLifetime());

    [HttpGet]
    public IActionResult KeyedServices() => Ok(_service.GetKeyedServices());

    [HttpGet]
    public IActionResult EnumerableServices() => Ok(_service.GetEnumerableServices());

    [HttpGet]
    public IActionResult ThreadSafeSingleton() => Ok(_service.GetThreadSafeSingleton());
}