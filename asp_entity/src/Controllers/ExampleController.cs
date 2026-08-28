using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_entity.Database;

namespace asp_entity.Controllers;

public class ExampleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDatabaseContext _db;

    public ExampleController(ILogger<HomeController> logger, ApplicationDatabaseContext ctx)
    {
        _logger = logger;
        _db = ctx;
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
        var examples = await this._db.Example.ToListAsync();
        return Ok(examples);
    }
}