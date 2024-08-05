using Common.Logging;
using Common.Logging.Correlation;
using Discount.API.EventBusConsumer;
using Discount.API.Services;
using Discount.Application.Extentions;
using Discount.Infrastructure.Extentions;
using MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();

// DI
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.MigrateDatabase(builder.Configuration);
builder.Services.AddServicesFromDiscountApplication();

// masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ItemsBasketOrderingConsumer>();

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

var app = builder.Build();

app.UseRouting();
app.MapGrpcService<DiscountService>();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(
        "Communication with gRPC endpoints must be made through a gRPC client.");
});

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