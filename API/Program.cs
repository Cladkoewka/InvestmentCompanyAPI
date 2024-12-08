using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");

var secretKey = builder.Configuration["JwtSettings:SecretKey"];

services.AddCorsPolicy();

// JWT authentication
services.AddJwtAuthentication(configuration);

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

