using BankLab.Modern.API.Configuration;
using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.Middleware;
using BankLab.Modern.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<BankLabOptions>(builder.Configuration.GetSection("BankLab"));

// Services
builder.Services.AddScoped<IClientesService, ClientesService>();
builder.Services.AddScoped<ICreditosService, CreditosService>();
builder.Services.AddScoped<ICuentasService, CuentasService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICreditoRepository, CreditoRepository>();
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BankLab API - Banco Ejemplo", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }

