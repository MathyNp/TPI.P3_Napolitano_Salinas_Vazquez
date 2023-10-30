using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using TPI_NapolitanoSalinasVazquez_P3.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TPI_NapolitanoSalinasVazquez_P3Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TPI_NapolitanoSalinasVazquez_P3Context")));

// Add services to the containers.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
