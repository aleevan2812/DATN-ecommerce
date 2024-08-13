using Common.Logging;
using Common.Logging.Correlation;
using Inventory.Application.Extentions;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

builder.Services.AddServicesFromInventoryApplication(builder.Configuration);
builder.Services.AddServicesFromInventoryInfrastructure(builder.Configuration);

// DI
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// add the built-in exception middleware to the pipeline.
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}