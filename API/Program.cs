using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using Infrastructure.Repositories.WithoutORM;
using Infrastructure.Repositories.WithoutORM.LinkRepositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Repositories
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

// Services
services.AddScoped<IAssetService, AssetService>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<IDepartmentService, DepartmentService>();
services.AddScoped<IEditorService, EditorService>();
services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IRiskService, RiskService>();
services.AddScoped<IProjectService, ProjectService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Project Management API"
    });
});

services.AddControllers();


var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management API v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.MapControllers();

app.Run();

/// TO-DO
/// - Сделать слой приложения (дто, сервисы, валидация, маппинг)
/// - Сделать нормальные контроллеры
/// - Добавить сваггер, протестить все еще раз 
/// - Дописать program.cs, сделать методы расширения, чтобы избавиться от хлама
/// - Продумать разделение ролей на пользователя и админа
/// - Сделать представления для визуализации данных для разных ролей
/// - Создать CLR функцию
/// - Создать хотя бы 1 триггер на таблицу
/// - Подвязать ORM, и показать его работу для пары сущностей (db context, ef core, отдельные репозитории)
