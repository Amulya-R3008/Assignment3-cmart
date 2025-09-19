using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagement.Core.Models;
using StudentManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<StudentStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(StudentStoreDatabaseSettings)));

builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(Sp =>
Sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(S =>
new MongoClient(builder.Configuration.GetValue<String>("StudentStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IStudentService,StudentService>();
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
