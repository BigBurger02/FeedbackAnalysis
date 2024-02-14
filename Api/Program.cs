using Api.Data;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
string? dataConnectionString = builder.Configuration.GetConnectionString("DBConnectionString");
if (string.IsNullOrWhiteSpace(dataConnectionString))
    throw new InvalidOperationException("Connection string 'DBConnectionString' not found.");
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(dataConnectionString));

builder.Services.AddTransient<IAnalyseWithAzureLanguageService, AnalyseWithAzureLanguageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();