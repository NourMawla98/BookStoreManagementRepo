using BookStoreManagement.Domain.Context;
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
builder.Services.AddScoped<IAuthorService, AuthorService>();

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