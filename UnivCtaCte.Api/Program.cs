using UnivCtaCte.Api.Helpers;
using UnivCtaCte.Application.UseCases;
using UnivCtaCte.Domain.Interfaces;
using UnivCtaCte.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICtaCteRepository, CtaCteRepository>();
builder.Services.AddScoped<CtaCteService>();
builder.Services.AddScoped<AppAuth>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "UnivCtaCte: Activo");
app.MapControllers();

app.Run();