using FuelStation.EF.Context;
using FuelStation.EF.MockRepositories;
using FuelStation.EF.Repositories;
using FuelStation.EF;
using FuelStation.Model;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FuelStationContext>();
builder.Services.AddTransient<IEntityRepo<Customer>, CustomerRepo>();
builder.Services.AddTransient<IEntityRepo<Employee>, EmployeeRepo>();
builder.Services.AddTransient<IEntityRepo<Item>, ItemRepo>();
builder.Services.AddTransient<IEntityRepo<Transaction>, TransactionRepo>();
builder.Services.AddTransient<IEntityRepo<TransactionLine>, TransactionLineRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
