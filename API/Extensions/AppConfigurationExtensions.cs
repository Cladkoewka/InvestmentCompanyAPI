namespace API.Extensions;

public static class AppConfigurationExtensions
{
    public static void ConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Investment Company API v1");
                options.RoutePrefix = string.Empty; 
            });
        }
    }
}