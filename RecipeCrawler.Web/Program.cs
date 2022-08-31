using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Core.Services;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Data.EntityConfigurations;
using RecipeCrawler.Data.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Configuration.AddEnvironmentVariables();
var connection = builder.Configuration["redis"];


builder.Services.AddSingleton<IRedisService, RedisService>((provider) =>
{
    var redisService = new RedisService(connection);
    redisService.Connect();
    return redisService;
});

string connectionString = "";
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("cheffer");
}
else
{
    connectionString = builder.Configuration.GetValue<string>("CHEFFER_CONNECTION_STRING");
}

builder.Services.AddSingleton<ChefConfiguration>();
builder.Services.AddSingleton<CookbookConfiguration>();
builder.Services.AddSingleton<RecipeConfiguration>();
builder.Services.AddSingleton<StepConfiguration>();
builder.Services.AddSingleton<IngredientConfiguration>();

builder.Services.AddScoped((provider) =>
{
    var context = new ChefferDbContext(connectionString,
        provider.GetService<CookbookConfiguration>() ?? new CookbookConfiguration(),
        provider.GetService<ChefConfiguration>() ?? new ChefConfiguration(),
        provider.GetService<RecipeConfiguration>() ?? new RecipeConfiguration(),
        provider.GetService<StepConfiguration>() ?? new StepConfiguration(),
        provider.GetService<IngredientConfiguration>() ?? new IngredientConfiguration());
    return context;
});

builder.Services.AddScoped<IRecipeCrawlerService, RecipeCrawlerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ChefferDbContext>();
        Console.WriteLine("Running db migrations...");
        context.Database.Migrate();
    }

    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    return;
}

