using backend.API.Controllers;
using backend.Application.Interfaces;
using backend.Domain.Models;
using backend.Infrastructure.Repository;

var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:8080");
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICoffeeRepository, CoffeeRepository>();

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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
