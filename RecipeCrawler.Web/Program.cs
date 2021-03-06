using RecipeCrawler.Core.Services;
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
