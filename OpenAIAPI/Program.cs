using BookPlatform.AIAPI;
using BookPlatform.AIAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAIServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

var listener = app.Services.GetService<RabbitMQListenerAI>();
listener.StartListening();

app.Run();
