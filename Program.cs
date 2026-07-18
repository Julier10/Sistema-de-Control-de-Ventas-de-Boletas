using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Service;
using SistemaVentaBoletas.Infrastructure.Interfaces;
using SistemaVentaBoletas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SistemaVentaBoletas.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión con la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CadenaSQL")));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IBoletaRepository, BoletaRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IBoletaService, BoletaService>();
builder.Services.AddScoped<IVentaService, VentaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();