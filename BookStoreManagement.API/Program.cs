using BookStoreManagement.API.Extensions;
using BookStoreManagement.Domain.Context;
using BookStoreManagement.Service.Helpers.AutoMapper;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using BookStoreManagement.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register controllers
builder.Services.AddControllers(); // Add this line to register services for controllers

// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Configure DbContext
builder.Services.AddDbContext<BookStoreDBContext>(options =>
{
    string defaultConnectionString = builder.Configuration.GetConnectionString("BOOK_STORE_CONNECTION_STRING");
    options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString));
});

// Add scoped services
builder.Services.AddScoped<IBookStoreRepository, BookStoreRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Use Swagger for API documentation
app.UseSwagger();
app.UseSwaggerUI();

// Use CORS (make sure you have configured CORS policies if needed)
app.UseCors();

app.UseDeveloperExceptionPage();

app.UseRouting();

// Use authorization middleware
app.UseAuthorization();

app.MapControllers(); // Map controllers

// Migrate database
app.MigrateDatabase<BookStoreDBContext>();

app.Run();
