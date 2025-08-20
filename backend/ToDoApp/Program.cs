using Microsoft.OpenApi.Models;
using ToDoApp.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo", Version = "v1" });

    options.SwaggerGeneratorOptions.Servers = new List<OpenApiServer>
    {
            new OpenApiServer { Url = "https://localhost:7080" }
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo v1");
        options.RoutePrefix = string.Empty;
    });

    app.MapGet("/openapi.json", async context =>
    {
        context.Response.ContentType = "application/json";
        var provider = app.Services.GetRequiredService<Swashbuckle.AspNetCore.Swagger.ISwaggerProvider>();
        var swagger = provider.GetSwagger("v1");
        var json = System.Text.Json.JsonSerializer.Serialize(swagger, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
        await context.Response.WriteAsync(json);
    });
}


app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
