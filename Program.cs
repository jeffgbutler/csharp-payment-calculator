using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using PaymentCalculator.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "csharp_payment_calculator", Version = "v1" });
});

builder.Services.AddCors();
builder.Services.AddOptions();
builder.Services.AddSingleton<PaymentService>();
builder.Services.AddSingleton<CrashService>();
builder.Services.AddSingleton<IHitCounterService, MemoryHitCounterService>();

var reloadFlagConfig = new Dictionary<string, string>() {{ "hostbuilder:reloadConfigOnChange", "false" }};
builder.Configuration.AddInMemoryCollection(reloadFlagConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "csharp_payment_calculator v1");
    c.RoutePrefix = string.Empty;
});

app.UseCors(c => c.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
