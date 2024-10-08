using Basket.Application.Extentions;
using Basket.Application.GrpcService;
using Basket.Core.IRepositories;
using Basket.Infrastructure.Extentions;
using Basket.Infrastructure.Reposotories;
using Common.Logging;
using Common.Logging.Correlation;
using MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

builder.Services.AddControllers();

// add Redis settings
builder.Services.AddServicesFromBasketInfrastructure(builder.Configuration);

// Add external services
builder.Services.AddServicesFromBasketApplication(builder.Configuration);

// DI
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddScoped<DiscountGrpcService>();

// masstransit
builder.Services.AddMassTransit(x =>
{
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.Run();