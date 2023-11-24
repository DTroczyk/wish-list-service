using Microsoft.EntityFrameworkCore;
using WishListApi.Models;
using WishListApi.Services;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = Environment.GetEnvironmentVariable("dbConnectionString");
var connectionString = Environment.GetEnvironmentVariable("testDbConnectionString");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseSqlServer(connectionString); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddCors(options => options.AddPolicy("Allow all", policy => {
    policy.AllowAnyOrigin();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
