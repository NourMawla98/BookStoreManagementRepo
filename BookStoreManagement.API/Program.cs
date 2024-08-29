using BookStoreManagement.API.Extensions;
using BookStoreManagement.Domain.Context;
using BookStoreManagement.Service.Helpers.AutoMapper;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using BookStoreManagement.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Optional: Increase if needed
    });
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<BookStoreDBContext>(options =>
{
    string defaultConnectionString = builder.Configuration.GetConnectionString("BOOK_STORE_CONNECTION_STRING");
    options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString));
});

builder.Services.AddScoped<IBookStoreRepository, BookStoreRepository>();

// Registering services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Use Swagger for API documentation
app.UseSwagger();
app.UseSwaggerUI();

// Use CORS
app.UseCors();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<BookStoreDBContext>();

app.Run();