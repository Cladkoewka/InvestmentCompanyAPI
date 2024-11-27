using Domain.Interfaces;
using Domain.Interfaces.LinkRepositories;
using Domain.Models;
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



services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
