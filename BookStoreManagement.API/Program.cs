using BookStoreManagement.Domain.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookStoreDBContext>(options =>
{
    var defaultConnectionString = builder.Configuration.GetConnectionString("BOOK_STORE_CONNECTION_STRING");
    options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();