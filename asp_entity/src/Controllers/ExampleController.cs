using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_entity.Database;
using asp_entity.Services;

namespace asp_entity.Controllers;

public class ExampleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDatabaseContext _db;
    private readonly ExampleQueryService _exampleQueryService;

    public ExampleController(
        ILogger<HomeController> logger,
        ApplicationDatabaseContext ctx,
        ExampleQueryService exampleQueryService)
    {
        _logger = logger;
        _db = ctx;
        _exampleQueryService = exampleQueryService;
    }

    // Automatic sub-path
    // GET: /Example/SimpleString
    public string SimpleString()
    {
        return "Simple String response...";
    }

    [HttpGet]
    public IActionResult JsonResponse()
    {
        var response = new
        {
            Id = 1,
            Msg = "Test Message",
            Num = 999
        };

        return Ok(response);
    }

    // Helpful: https://github.com/adamajammary/simple-web-app-mvc-dotnet/blob/master/SimpleWebAppMVC/Controllers/TasksApiController.cs
    [HttpGet]
    public async Task<IActionResult> SqlExamples()
    {
        Console.WriteLine($"Database can connect? {this._db.Database.CanConnect()}");
        var examples = await _exampleQueryService.GetExamplesAsync();
        return Ok(examples);
    }
}