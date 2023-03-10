using BookPlatform.BLL;
using BookPlatform.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEfRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddBusinessLogicServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.Run();
