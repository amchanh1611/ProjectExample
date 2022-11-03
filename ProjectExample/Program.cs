using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Mapping;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Services;
using ProjectExample.Persistence.Contexts;
using ProjectExample.Persistence.Repositories;
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

//RepositoryWrapper
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddTransient<IMediaService,MediaServices>();
//AutoMapper
builder.Services.AddAutoMapper(typeof(Profiles));

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining(typeof(CreateOrUpdateScheduleRequestValidator));

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
