using Casetask;
using Casetask.Business;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using Casetask.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGenericRepository<Teacher>, GenericRepository<Teacher>>();
builder.Services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
builder.Services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
builder.Services.AddScoped<IGenericRepository<Score>, GenericRepository<Score>>();

builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();