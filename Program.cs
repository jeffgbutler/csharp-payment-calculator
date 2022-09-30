using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PaymentCalculator.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors(c => c.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
