using Services;
using Steeltoe.Connector.Redis;
using Steeltoe.Extensions.Logging;
using Steeltoe.Management.Endpoint;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDynamicConsole();

builder.Services.AddControllers();
builder.Services.AddAllActuators(builder.Configuration);
builder.Services.ActivateActuatorEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IHitCounterService, MemoryHitCounterService>();
}
else
{
    builder.Services.AddDistributedRedisCache(builder.Configuration);
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(c => c.AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
