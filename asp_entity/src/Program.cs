using Microsoft.EntityFrameworkCore;
using asp_entity.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Fetch from appsettings.json
Console.WriteLine($"Initializing Database with Connection String: {builder.Configuration.GetConnectionString("MSSQL_DOCKER_CONNECTION_STRING")}");

// Retry on error: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#configuring-the-database-provider
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
