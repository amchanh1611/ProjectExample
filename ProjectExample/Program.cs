using Microsoft.EntityFrameworkCore;
using ProjectExample.Persistence.Contexts;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
builder.Services.AddDbContextPool<ApplicationDbContext>(option =>
    option.UseMySql("server=localhost;user id=root;password='';port=3306;database=example;",
    ServerVersion.AutoDetect("server=localhost;user id=root;password='';port=3306;database=example;")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
