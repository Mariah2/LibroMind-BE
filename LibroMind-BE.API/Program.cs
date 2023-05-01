global using Microsoft.EntityFrameworkCore;
using FluentValidation;
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
using LibroMind_BE.Services.Models.Put;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
    .AddScoped<IValidator<BookCategoryPostDTO>, AddBookCategoryValidator>()
    .AddScoped<IValidator<BookLibraryPostDTO>, AddBookLibraryValidator>()
    .AddScoped<IValidator<BookLibraryPutDTO>, UpdateBookLibraryValidator>()
    .AddScoped<IValidator<BookPostDTO>, AddBookValidator>()
    .AddScoped<IValidator<BookUserPostDTO>, AddBookUserValidator>()
    .AddScoped<IValidator<BorrowPostDTO>, AddBorrowValidator>()
    .AddScoped<IValidator<BorrowPutDTO>, UpdateBorrowValidator>()
    .AddScoped<IValidator<CategoryPostDTO>, AddCategoryValidator>()
    .AddScoped<IValidator<LibraryPostDTO>, AddLibraryValidator>()
    .AddScoped<IValidator<PublisherPostDTO>, AddPublisherValidator>()
    .AddScoped<IValidator<ReviewPostDTO>, AddReviewValidator>()
    .AddScoped<IValidator<UserPostDTO>, AddUserValidator>()
    .AddScoped<IValidator<UserPutDTO>, UpdateUserValidator>();

builder.Services.AddSingleton<ProblemDetailsFactory, LibroMindProblemDetailsFactory>();

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibroMindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), op =>
        op.CommandTimeout(120));
});

//Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Repositories
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
builder.Services.AddScoped<IBookLibraryRepository, BookLibraryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookUserRepository, BookUserRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



// Register Services
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookCategoryService, BookCategoryService>();
builder.Services.AddScoped<IBookLibraryService, BookLibraryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookUserService, BookUserService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes("my-top-secret-key"))
            });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "LibroMind API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

    app.UseCors("default");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}