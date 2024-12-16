using System.Text;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Services;
using Application.Services.Auth;
using Application.Validation;
using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Repositories.WithORM;
using Infrastructure.Repositories.WithoutORM;
using Infrastructure.Repositories.WithoutORM.LinkRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
    
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        // Repositories with ORM (EF Core)
        services.AddScoped<IAssetRepository, AssetRepositoryEfCore>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Repositories without ORM
        services.AddSingleton<ICustomerRepository>(new CustomerRepository(connectionString));
        services.AddSingleton<IDepartmentRepository>(new DepartmentRepository(connectionString));
        services.AddSingleton<IEditorRepository>(new EditorRepository(connectionString));
        services.AddSingleton<IEmployeeRepository>(new EmployeeRepository(connectionString));
        services.AddSingleton<IProjectRepository>(new ProjectRepository(connectionString));
        services.AddSingleton<IRiskRepository>(new RiskRepository(connectionString));
        services.AddSingleton<FunctionalRepository>(new FunctionalRepository(connectionString));

        // Link Repositories
        services.AddSingleton<IProjectAssetLinkRepository>(new ProjectAssetLinkRepository(connectionString));
        services.AddSingleton<IProjectDepartmentLinkRepository>(new ProjectDepartmentLinkRepository(connectionString));
        services.AddSingleton<IProjectRiskLinkRepository>(new ProjectRiskLinkRepository(connectionString));
    }

    public static void AddServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEditorService, EditorService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IRiskService, RiskService>();
        services.AddScoped<IProjectService, ProjectService>();
        
        // AuthServices
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtTokenService>();
        services.AddScoped<PasswordHashingService>();
    }

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Investment API"
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Description = "JWT token: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            
        });
    }

    public static void AddFluentValidationConfiguration(this IServiceCollection services) => 
        services.AddFluentValidation().AddValidatorsFromAssembly(typeof(AssetCreateDtoValidator).Assembly);

    public static void AddAutoMapperConfiguration(this IServiceCollection services) => 
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    public static void AddJwtAuthentication(this IServiceCollection services, string jwtKey)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
    }
    
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ViewerOnly", policy => policy.RequireRole("Viewer"));
        });
    }
    
    public static void AddCorsPolicy(this IServiceCollection services, string frontendUrl)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy.WithOrigins(frontendUrl) 
                        .AllowAnyHeader()  
                        .AllowAnyMethod();
                });
        });
    }
}