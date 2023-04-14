using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swagger.DataAccess.Context;
using Swagger.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<ReviewContext>(options =>
                options.UseSqlite("Filename=Reviews.db"));

builder.Services.AddScoped<IReviewRepository, SQLiteRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
