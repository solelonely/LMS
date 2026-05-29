using Lms.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Setup Cookie Auth for fake admin login
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "Lms.Cookie";
        config.LoginPath = "/Account/Login";
    });

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

var supportedCultures = new[] { "zh-TW", "en" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("zh-TW")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
