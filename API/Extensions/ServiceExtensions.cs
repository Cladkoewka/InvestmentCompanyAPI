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
using Microsoft.EntityFrameworkCore;
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
        // Регистрируем репозитории с использованием ORM (EF Core)
        services.AddScoped<IAssetRepository, AssetRepositoryEfCore>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Регистрируем репозитории без ORM
        services.AddSingleton<ICustomerRepository>(new CustomerRepository(connectionString));
        services.AddSingleton<IDepartmentRepository>(new DepartmentRepository(connectionString));
        services.AddSingleton<IEditorRepository>(new EditorRepository(connectionString));
        services.AddSingleton<IEmployeeRepository>(new EmployeeRepository(connectionString));
        services.AddSingleton<IProjectRepository>(new ProjectRepository(connectionString));
        services.AddSingleton<IRiskRepository>(new RiskRepository(connectionString));

        // Регистрируем репозитории с link-таблицами
        services.AddSingleton<IProjectAssetLinkRepository>(new ProjectAssetLinkRepository(connectionString));
        services.AddSingleton<IProjectDepartmentLinkRepository>(new ProjectDepartmentLinkRepository(connectionString));
        services.AddSingleton<IProjectRiskLinkRepository>(new ProjectRiskLinkRepository(connectionString));
    }

    public static void AddServices(this IServiceCollection services)
    {
        // Регистрируем сервисы
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEditorService, EditorService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IRiskService, RiskService>();
        services.AddScoped<IProjectService, ProjectService>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtTokenService>();
        services.AddScoped<PasswordHashingService>();
    }

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        // Конфигурация Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Investment API"
            });
            
            // Добавляем схему безопасности для передачи токена
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Description = "Введите ваш JWT токен в формате: Bearer {token}"
            });

            // Применяем security scheme ко всем операциям
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

    public static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        // Конфигурация FluentValidation
        services.AddFluentValidation().AddValidatorsFromAssembly(typeof(AssetCreateDtoValidator).Assembly);
    }

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        // Конфигурация AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}