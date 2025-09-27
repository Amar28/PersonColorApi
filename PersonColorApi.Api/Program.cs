using Application.Persons.Queries;
using PersonColorApi.Domain.Interfaces;
using Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetAllPersonsQuery).Assembly);
builder.Services.AddSingleton<IPersonRepository>(_ => new CsvPersonRepository("Data/sample-input.csv"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();