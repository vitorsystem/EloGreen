using EloGreen.API.Handlers;
using EloGreen.Application.Services;
using EloGreen.Application.Services.Interfaces;
using EloGreen.Application.Validators;
using EloGreen.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateSupplierValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateSupplierValidator>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductValidator>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

#region DB Configuration
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection"); 
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString, b => b.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19)).EnableSensitiveDataLogging(true));
#endregion

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();