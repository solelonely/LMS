using Lms.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Lms.Application.Courses.Commands.CreateCourseCommand).Assembly);
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // For demonstration purposes, we are using an InMemory database.
    // In a real application, you'd use UseSqlServer with a connection string from appsettings.
    options.UseInMemoryDatabase("LmsDb");
});

builder.Services.AddScoped<Lms.Core.Interfaces.ICourseRepository, Lms.Infrastructure.Repositories.CourseRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
