using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFullPostgre.Context;
using RestFullPostgre.Repositories;
using RestFullPostgre.Services;
using RestFullPostgre.Services.Impl;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new GlobalVariableList
{
    _connextion_SQL_Trancode = builder.Configuration.GetConnectionString("dev")
});
builder.Services.AddSingleton<DapperContext>();

//DI
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITrancodeInformationService, TrancodeInformationService>();
builder.Services.AddTransient<ITrancodeManualService, TrancodeManualService>();


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
