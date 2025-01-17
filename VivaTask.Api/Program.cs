using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using VivaTask.Application.Commands.Handlers;
using VivaTask.Application.Services;
using VivaTask.Infrastructure.Configurations;
using VivaTask.Infrastructure.DbContexts;
using VivaTask.Infrastructure.Services;
using VivaTask.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConfiguration = builder.Configuration.GetSection(nameof(SqlConfiguration)).Get<SqlConfiguration>();
var restCountriesConfiguration = builder.Configuration.GetSection(nameof(RestCountriesConfiguration)).Get<RestCountriesConfiguration>();
var redisConfiguration = builder.Configuration.GetSection(nameof(RedisConfiguration)).Get<RedisConfiguration>();

builder.Services.AddHttpClient(nameof(RestCountriesConfiguration), client =>
{
    client.BaseAddress = new Uri(restCountriesConfiguration.Url);
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlConfiguration.VivaTaskDbConnectionString,
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }
));

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFilter("System.Net.Http", LogLevel.None);
    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(FirstQuestionHandler).Assembly,
        typeof(CountriesHandler).Assembly
        )
);

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IFirstQuestionService, FirstQuestionService>();
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IHttpCountriesProvider, HttpCountriesProvider>();
builder.Services.AddScoped<ISqlCountriesProvider, SqlCountriesProvider>();
builder.Services.AddScoped<ICacheCountriesProvider, CacheCountriesProvider>();

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
