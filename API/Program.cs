using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");

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

// Swagger configure
app.ConfigureSwagger();

app.MapControllers();

app.Run();

/// TO-DO
/// - Продумать разделение ролей на пользователя и админа (Роли сотрудника и пользователя, просто смотрящего инвест проекты)
/// - Сделать представления для визуализации данных для разных ролей
/// - Создать CLR функцию
/// - Создать хотя бы 1 триггер на таблицу
/// - Подвязать ORM, и показать его работу для пары сущностей (db context, ef core, отдельные репозитории)
