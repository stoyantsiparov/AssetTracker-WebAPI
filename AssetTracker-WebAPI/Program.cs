using System.Text;
using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.Data;
using AssetTracker_WebAPI.Services.Asset;
using AssetTracker_WebAPI.Services.Asset.Contracts;
using AssetTracker_WebAPI.Services.Auth;
using AssetTracker_WebAPI.Services.Auth.Contracts;
using AssetTracker_WebAPI.Services.External;
using AssetTracker_WebAPI.Services.External.Contracts;
using AssetTracker_WebAPI.Services.Portfolio;
using AssetTracker_WebAPI.Services.Portfolio.Contracts;
using AssetTracker_WebAPI.Services.Transaction;
using AssetTracker_WebAPI.Services.Transaction.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Entity Framework Core
builder.Services.AddDbContext<AssetTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AssetTrackerDbContext>()
.AddDefaultTokenProviders();

// Load Settings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Set to true in production
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = jwtSettings!.ValidAudience,
        ValidIssuer = jwtSettings.ValidIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});

// Register Application Services
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Register External API Services
builder.Services.AddHttpClient<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient<ICoinGeckoService, CoinGeckoService>();
builder.Services.AddHttpClient<INewsApiService, NewsApiService>();

// Swagger Configuration with JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AppConstants.Security.BearerDefinition, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = AppConstants.Security.BearerScheme,
        BearerFormat = AppConstants.Security.BearerFormat,
        Description = AppMessages.SwaggerJwtDescription
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference(AppConstants.Security.BearerDefinition, document)] = []
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Who am I?
app.UseAuthorization(); // What can I do?

app.MapControllers();

app.Run();