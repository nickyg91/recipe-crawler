using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeCrawler.Core.Mappers.Profiles;
using RecipeCrawler.Core.Services;
using RecipeCrawler.Core.Services.Accounts;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Data.EntityConfigurations;
using RecipeCrawler.Data.Redis;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Data.Repositories.Implementations;
using RecipeCrawler.Web.Authentication;
using RecipeCrawler.Web.Configuration;
using System.Text;
using RecipeCrawler.Core.Configuration;
using RecipeCrawler.Core.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Configuration.AddEnvironmentVariables();
var connection = builder.Configuration["redis"];
builder.Services.AddOptions();

builder.Services.AddSingleton<IRedisService, RedisService>((provider) =>
{
    var redisService = new RedisService(connection);
    redisService.Connect();
    return redisService;
});
var jwtSettingsSection = builder.Configuration.GetSection(JwtSettingsOptions.JwtSettingsSection);
var emailConfigSettingsSection = builder.Configuration.GetSection(EmailConfigurationOptions.EmailConfigurationSection);
builder.Services.Configure<JwtSettingsOptions>(jwtSettingsSection);
builder.Services.Configure<EmailConfigurationOptions>(emailConfigSettingsSection);

var settings = builder.Configuration.Get<RecipeCrawlerConfiguration>();
string oauthSecret = settings.JwtSettings.Key;
string connectionString = builder.Configuration.GetConnectionString("cheffer");
string authorityUrl = settings.JwtSettings.AuthorityUrl;
string audience = settings.JwtSettings.Audience;

string url = builder.Environment.IsDevelopment() ? "https://localhost:7215" : "https://cheffer.nickganter.dev";

if (!builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetValue<string>("CHEFFER_CONNECTION_STRING");
    oauthSecret = builder.Configuration.GetValue<string>("OAUTH_SECRET");
}

builder.Services.AddAutoMapper(typeof(ChefProfile));
builder.Services.AddSingleton<ChefConfiguration>();
builder.Services.AddSingleton<CookbookConfiguration>();
builder.Services.AddSingleton<RecipeConfiguration>();
builder.Services.AddSingleton<StepConfiguration>();
builder.Services.AddSingleton<IngredientConfiguration>();
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<IEmailService, EmailService>((provider) => new EmailService(settings.EmailConfiguration, url));
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Audience = audience;
    options.Authority = authorityUrl;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(oauthSecret)),
        ValidAudience = audience,
        ValidIssuer = authorityUrl,
        ValidateLifetime = true,
        ValidateAudience = true,
    };
});

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
app.UseAuthentication();
app.UseAuthorization();

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

