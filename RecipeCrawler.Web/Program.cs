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
using RecipeCrawler.Core.Authentication;
using RecipeCrawler.Core.Configuration;
using RecipeCrawler.Core.Services.Chef;
using RecipeCrawler.Core.Services.Chef.Interfaces;
using RecipeCrawler.Core.Services.Email;
using RecipeCrawler.Core.Services.Recipe;
using RecipeCrawler.Core.Services.Recipe.Interfaces;
using RecipeCrawler.ViewModels.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Configuration.AddEnvironmentVariables();
var connection = builder.Configuration["redis"];
builder.Services.AddOptions();

builder.Services.AddSingleton<IRedisService, RedisService>((provider) =>
{
    var redisService = new RedisService(connection!);
    redisService.Connect();
    return redisService;
});
var jwtSettingsSection = builder.Configuration.GetSection(JwtSettingsOptions.JwtSettingsSection);
var emailConfigSettingsSection = builder.Configuration.GetSection(EmailConfigurationOptions.EmailConfigurationSection);
builder.Services.Configure<JwtSettingsOptions>(jwtSettingsSection);
builder.Services.Configure<EmailConfigurationOptions>(emailConfigSettingsSection);

var settings = builder.Configuration.Get<RecipeCrawlerConfiguration>();
string oauthSecret = settings.JwtSettings!.Key;
string connectionString = builder.Configuration.GetConnectionString("cheffer")!;
string issuer = settings.JwtSettings.Issuer;
string audience = settings.JwtSettings.Audience;
string url = builder.Environment.IsDevelopment() ? "https://localhost:5002" : "https://cheffer.nickganter.dev";

if (!builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetValue<string>("CHEFFER_CONNECTION_STRING");
    oauthSecret = builder.Configuration.GetValue<string>("OAUTH_SECRET");
}

builder.Services.AddAutoMapper(typeof(ChefProfile));
builder.Services.AddAutoMapper(typeof(CookbookProfile));
builder.Services.AddAutoMapper(typeof(RecipeProfile));
builder.Services.AddAutoMapper(typeof(StepProfile));
builder.Services.AddAutoMapper(typeof(IngredientProfile));
builder.Services.AddSingleton<ChefConfiguration>();
builder.Services.AddSingleton<CookbookConfiguration>();
builder.Services.AddSingleton<RecipeConfiguration>();
builder.Services.AddSingleton<StepConfiguration>();
builder.Services.AddSingleton<IngredientConfiguration>();
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<IEmailService, EmailService>((provider) => new EmailService(settings.EmailConfiguration!, url));
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddScoped<IChefService, ChefService>();
builder.Services.AddScoped<ICookbookRepository, CookbookRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IStepRepository, StepRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticatedChef, AuthenticatedChef>(provider =>
{
    var user =
        provider.GetService<IHttpContextAccessor>()!.HttpContext!.User;
    return new AuthenticatedChef(user);
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oauthSecret)),
        ValidIssuer = issuer,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

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
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}

