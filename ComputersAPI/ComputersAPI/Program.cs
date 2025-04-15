using ComputersAPI.Database;
using ComputersAPI.Helpers;
using ComputersAPI.Services;
using ComputersAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ComputersDbContext>(options => options
.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddTransient<IComputersService, ComputersService>();
builder.Services.AddTransient<IComponentsService, ComponentsService>();
builder.Services.AddTransient<ICategoriesComponentsService, CategoriesComponentsService>();
builder.Services.AddTransient<IPeripheralsService, PeripheralsService>();
builder.Services.AddTransient<ICategoriesPeripheralsService, CategoriesPeripheralsService>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
