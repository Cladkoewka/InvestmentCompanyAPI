using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");/*"User ID=postgres;Password=1339;Port=5432;Database=to-do;Host=localhost"*/;
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL");
    

services.AddCorsPolicy(frontendUrl);

// JWT authentication
services.AddJwtAuthentication(jwtKey);

// Authorization
services.AddAuthorizationPolicies();

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

app.UseCors("AllowFrontend");

// Authentication & Authorization 
app.UseAuthentication(); 
app.UseAuthorization(); 

// Swagger configure
app.ConfigureSwagger();

app.MapControllers();

app.Run();

