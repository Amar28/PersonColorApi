using Application.Persons.Queries;
using PersonColorApi.Domain.Interfaces;
using PersonColorApi.Infrastructure.Repositories;
using PersonColorApi.Infrastructure.Ef;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfiguration laden
var configuration = builder.Configuration;
var dataSource = configuration.GetValue<string>("DataSource") ?? "csv";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetAllPersonsQuery).Assembly);

if (dataSource.ToLower() == "ef")
{
    // EF Core Datenbank anbinden
    builder.Services.AddDbContext<PersonDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("PersonDb")));

    builder.Services.AddScoped<IPersonRepository, EfPersonRepository>();
}
else
{
    // Standard: CSV Datei einlesen
    var csvPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "sample-input.csv");
    builder.Services.AddSingleton<IPersonRepository>(_ => new CsvPersonRepository(csvPath));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();