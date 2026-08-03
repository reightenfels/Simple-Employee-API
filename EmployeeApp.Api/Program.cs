using System.Reflection;
using EmployeeApp.Infrastructure.Data;
using EmployeeApi.ExceptionsHandlers;
using EmployeeApi.Repositories;
using EmployeeApp.Core.Contracts;
using EmployeeApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using EmployeeApp.Infrastructure.Repositories;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("Старт приложения");

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddUserSecrets<Program>(); 

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddExceptionHandler<ResourceNotFoundExceptionHandler>();
    builder.Services.AddProblemDetails(); 
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IDepartmentService, DepartmentService>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
        { 
            Title = "Employee API", 
            Version = "v1",
            Description = "API управления сотрудниками"
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();

    app.UseExceptionHandler(); 

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Приложение остановлено из-за исключения");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}