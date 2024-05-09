using ProductRedisAPI.Data;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

string? connectionStr = builder.Configuration.GetConnectionString("ProductDataConnection");
if(connectionStr != null){
    builder.Services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect(connectionStr)
    );
}

builder.Services.AddScoped<IProductData, ProductData>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
