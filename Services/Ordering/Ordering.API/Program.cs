using Common.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.API.EventBusConsumer;
using Ordering.Application.Extentions;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

// DI

// Add services to the container.

// add Infrastructure services
builder.Services.AddInfraServices(builder.Configuration);

// add Application services
builder.Services.AddDiscountApplicationServices();

// masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<BasketOrderingConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

ApplyMigration();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<OrderContext>();
        var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
        await context.Database.MigrateAsync();
        await OrderContextSeed.SeedAsync(context, logger);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<OrderContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}