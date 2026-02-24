using FinTracker.API.Controllers;
using FinTracker.API.Models;
using FinTracker.Core.Repositories;
using FinTracker.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ExpenseRepository>(
    new ExpenseRepository("expenses.json")
);
builder.Services.AddSingleton<ExpenseService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();