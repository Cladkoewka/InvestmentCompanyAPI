using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");

var secretKey = builder.Configuration["JwtSettings:SecretKey"];
// Настройка JWT аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

// Настройка авторизации
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ViewerOnly", policy => policy.RequireRole("Viewer"));
});

// DbContext
services.AddApplicationDbContext(connectionString);

// Repositories
services.AddRepositories(connectionString);

// Services
services.AddServices();

// AutoMapper
services.AddAutoMapperConfiguration();

// FluentValidation
services.AddFluentValidationConfiguration();

// Swagger
services.AddSwaggerConfiguration();

services.AddControllers();


var app = builder.Build();

// Добавляем middleware для аутентификации
app.UseAuthentication(); // Включаем аутентификацию
app.UseAuthorization(); // Включаем авторизацию

// Swagger configure
app.ConfigureSwagger();

app.MapControllers();

app.Run();

/// TO-DO
/// - Подвязать ORM, и показать его работу для пары сущностей (db context, ef core, отдельные репозитории)
