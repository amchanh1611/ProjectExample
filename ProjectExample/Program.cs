using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Mapping;
using ProjectExample.Modules.Medias.Services;
using ProjectExample.Persistence.Contexts;
using ProjectExample.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AppSettings

//DbContext
builder.Services.AddDbContextPool<ApplicationDbContext>(option =>
    option.UseMySql(builder.Configuration["Database:ConnectionString"],
    ServerVersion.AutoDetect(builder.Configuration["Database:ConnectionString"])));

//RepositoryWrapper
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//Services
builder.Services.AddTransient<IMediaServices, MediaServices>();
builder.Services.AddTransient<IScheduleServices, ScheduleServices>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Profiles));

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining(typeof(Program));

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