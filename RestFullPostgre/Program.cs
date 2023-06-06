using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFullPostgre.Context;
using RestFullPostgre.Middleware;
using RestFullPostgre.Repositories;
using RestFullPostgre.Services;
using RestFullPostgre.Services.Impl;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowOrigin",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddSingleton(new GlobalVariableList
{
    _connextion_SQL_Trancode = builder.Configuration.GetConnectionString("dev")
});
builder.Services.AddSingleton<DapperContext>();

//DI
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITrancodeInformationService, TrancodeInformationService>();
builder.Services.AddTransient<ITrancodeManualService, TrancodeManualService>();
builder.Services.AddTransient<ICallerLanguageService, CallerLanguageService>();
builder.Services.AddTransient<ISquadRelatedService, SquadRelatedService>();
builder.Services.AddSingleton<HandleExceptionMiddleware>();

// add memory cache
builder.Services.AddMemoryCache();


var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseMiddleware<HandleExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
