using ChucksKitchen.Data;
using ChucksKitchen.Interfaces;
using ChucksKitchen.Repositories;
using ChucksKitchen.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Chuk's Kitchen Api", Version = "v1" });
});
builder.Services.AddSingleton<DataStorage>();
builder.Services.AddScoped<IAppUser, AppUserRepo>();
builder.Services.AddScoped<IFoodRepository, FoodRepo>();
builder.Services.AddScoped<IOrder, OrderRepo>();
//register services
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    policy.WithOrigins("http://localhost:5214", "https://chukskitchen.com")
    .AllowAnyMethod()
    .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();


app.Run();
