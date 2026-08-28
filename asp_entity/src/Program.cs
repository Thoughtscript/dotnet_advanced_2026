using Microsoft.EntityFrameworkCore;
using asp_entity.Database;
using asp_entity.Interfaces;
using asp_entity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Registers MVC controllers and Razor views.
builder.Services.AddControllersWithViews();

// Registers a simple interface-to-implementation mapping with transient lifetime.
builder.Services.AddTransient<SimpleExampleInterface, SimpleExampleInterfaceImpl>();
// Registers the service as transient so each resolution can create a new instance.
builder.Services.AddTransient<ServiceLifetimeInterface, ServiceLifetimeInterfaceImpl>();
// Registers the service as scoped so one instance is reused within a scope.
builder.Services.AddScoped<ServiceLifetimeInterface, ServiceLifetimeInterfaceImpl>();
// Registers the service as singleton so one instance is reused for the application lifetime.
builder.Services.AddSingleton<ServiceLifetimeInterface, ServiceLifetimeInterfaceImpl>();
// Registers the first keyed implementation under the "first" key.
builder.Services.AddKeyedTransient<KeyedServiceInterface>("first", (_, _) => new KeyedServiceInterfaceImpl("first"));
// Registers the second implementation under the "second" key.
builder.Services.AddKeyedTransient<KeyedServiceInterface>("second", (_, _) => new KeyedServiceInterfaceSecondImpl("second"));
// Registers the first implementation that will be returned through IEnumerable<IEnumerableInterface>.
builder.Services.AddTransient<IEnumerableInterface>(_ => new IEnumerableInterfaceImpl("first"));
// Registers a distinct second implementation that will be returned through IEnumerable<IEnumerableInterface>.
builder.Services.AddTransient<IEnumerableInterface>(_ => new IEnumerableInterfaceSecondImpl("second"));
// Registers the thread-safe stateful service as a singleton.
builder.Services.AddSingleton<ThreadSafeSingletonInterface, ThreadSafeSingletonInterfaceImpl>();
// Registers the service that aggregates and exposes all DI examples.
builder.Services.AddTransient<DependencyInjectionService>();

// Fetch from appsettings.json
Console.WriteLine($"Initializing Database with Connection String: {builder.Configuration.GetConnectionString("MSSQL_DOCKER_CONNECTION_STRING")}");

// Retry on error: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#configuring-the-database-provider
// Registers the EF Core context with SQL Server and transient-failure retries.
builder.Services.AddDbContext<ApplicationDatabaseContext>(
    opsBuilder => opsBuilder.UseSqlServer(
        builder.Configuration.GetConnectionString("MSSQL_DOCKER_CONNECTION_STRING"),
        providerOpts => { providerOpts.EnableRetryOnFailure(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
