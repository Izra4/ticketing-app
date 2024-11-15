using Microsoft.EntityFrameworkCore;
using Ticketing.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionsString = builder.Configuration.GetConnectionString("DefaultConnection");
System.Diagnostics.Debugger.Break();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionsString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
