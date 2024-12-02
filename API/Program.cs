using API.Extensions;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validation;
using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories.WithoutORM;
using Infrastructure.Repositories.WithoutORM.LinkRepositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
/// - Продумать разделение ролей на пользователя и админа
/// - Сделать представления для визуализации данных для разных ролей
/// - Создать CLR функцию
/// - Создать хотя бы 1 триггер на таблицу
/// - Подвязать ORM, и показать его работу для пары сущностей (db context, ef core, отдельные репозитории)
