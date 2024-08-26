using BookStoreManagement.Domain.Context;
using BookStoreManagement.Service.Helpers.AutoMapper;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using BookStoreManagement.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookStoreDBContext>(options =>
{
    string defaultConnectionString = builder.Configuration.GetConnectionString("BOOK_STORE_CONNECTION_STRING") ?? "";
    options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString));
});

builder.Services.AddScoped<IBookStoreRepository, BookStoreRepository>();

// upcoming below: dependency injections lal services li 3mlnenun implement
// Registering AuthorService
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Registering BookService
builder.Services.AddScoped<IBookService, BookService>();

// Registering PublisherService
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddScoped<IPurchaseService, PurchaseService>();

//Mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();