using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using Infrastructure.Repositories.WithoutORM;
using Infrastructure.Repositories.WithoutORM.LinkRepositories;

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

// AutoMapper
services.AddAutoMapper(typeof(Program));


services.AddControllers();

var app = builder.Build();

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
