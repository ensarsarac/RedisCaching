using Microsoft.EntityFrameworkCore;
using RedisCache.Example.DataAccess.Context;
using RedisCache.Example.RedisCache;
using RedisCache.Example.Services;
using RedisCache.Example.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RedisContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton(typeof(IRedisCacheService<>), typeof(RedisCacheService<>));


var redisCacheSettings = builder.Configuration.GetSection("RedisCacheSetting").Get<RedisCacheSetting>();

builder.Services.Configure<RedisCacheSetting>(builder.Configuration.GetSection("RedisCacheSetting"));
builder.Services.AddStackExchangeRedisCache(opt =>
{
	opt.Configuration = redisCacheSettings?.ConnectionString;
	opt.InstanceName = redisCacheSettings?.InstanceName;
});

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
