global using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibroMind_BE.API.Common;
using LibroMind_BE.API.Validations;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Implementations;
using LibroMind_BE.DAL.Repositories.Interfaces;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Implementations;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddScoped<IValidator<AddressPostDTO>, AddAddressValidator>()
    .AddScoped<IValidator<AuthorPostDTO>, AddAuthorValidator>()
    .AddScoped<IValidator<BorrowPutDTO>, UpdateBorrowValidator>()
    .AddScoped<IValidator<BookLibraryPutDTO>, UpdateBookLibraryValidator>()
    .AddScoped<IValidator<BookPostDTO>, AddBookValidator>()
    .AddScoped<IValidator<BookCategoryPostDTO>, AddBookCategoryValidator>()
    .AddScoped<IValidator<CategoryPostDTO>, AddCategoryValidator>();

builder.Services.AddSingleton<ProblemDetailsFactory, LibroMindProblemDetailsFactory>();

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibroMindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Repositories
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

// Register Services
builder.Services.AddScoped<IAddressService, AddressService>();

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error");
    }
    else
    {
        app.UseExceptionHandler("/error-development");
    }

    app.UseHttpsRedirection();
    //app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}