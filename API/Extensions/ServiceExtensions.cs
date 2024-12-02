using Application.Interfaces;
using Application.Services;
using Application.Validation;
using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories.WithoutORM;
using Infrastructure.Repositories.WithoutORM.LinkRepositories;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        // Регистрируем репозитории
        services.AddSingleton<IProjectAssetLinkRepository>(new ProjectAssetLinkRepository(connectionString));
        services.AddSingleton<IProjectDepartmentLinkRepository>(new ProjectDepartmentLinkRepository(connectionString));
        services.AddSingleton<IProjectRiskLinkRepository>(new ProjectRiskLinkRepository(connectionString));

        services.AddSingleton<IAssetRepository>(new AssetRepository(connectionString));
        services.AddSingleton<ICustomerRepository>(new CustomerRepository(connectionString));
        services.AddSingleton<IDepartmentRepository>(new DepartmentRepository(connectionString));
        services.AddSingleton<IEditorRepository>(new EditorRepository(connectionString));
        services.AddSingleton<IEmployeeRepository>(new EmployeeRepository(connectionString));
        services.AddSingleton<IProjectRepository>(new ProjectRepository(connectionString));
        services.AddSingleton<IRiskRepository>(new RiskRepository(connectionString));
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
                Title = "Project Management API"
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