using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ScheduleServer.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
var builder = WebApplication.CreateBuilder(args);



// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ← Регистрация Swagger
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });


builder.Services.AddDbContext<ShcheduleContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("ScheduleDb")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // ← Важно: до UseHttpsRedirection!
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "docs"; // Теперь Swagger будет на /docs
});
app.Run();