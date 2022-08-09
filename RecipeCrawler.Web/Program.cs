using RecipeCrawler.Core.Services;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Data.EntityConfigurations;
using RecipeCrawler.Data.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
var connection = builder.Configuration["redis"];


builder.Services.AddSingleton<IRedisService, RedisService>((provider) =>
{
    var redisService = new RedisService(connection);
    redisService.Connect();
    return redisService;
});

var connectionString = builder.Configuration.GetConnectionString("cheffer");

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

app.Run();
