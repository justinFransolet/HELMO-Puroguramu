using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Puroguramu.App.Middlewares;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice.Data;
using Puroguramu.Domains.User.Data;
using Puroguramu.Infrastructures.Data;
using Puroguramu.Infrastructures.Data.Entities;
using Puroguramu.Infrastructures.Repository;
using Puroguramu.Infrastructures.Roslyn;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ILogger, Logger<object>>();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();
builder.Services.AddScoped<IAssessExercise, RoslynAssessor>();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<PuroguramuDbContext>(options =>
    {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServPuroguramuDbContext"));
    });
}
else
{
    builder.Services.AddDbContext<PuroguramuDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliPuroguramuDbContext"));
    });
}
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

//.AddRoles<IdentityRole>()
builder.Services.AddDefaultIdentity<IdentityPuroguramuUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PuroguramuDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryExercise, ExerciseRepository>();
builder.Services.AddScoped<IRepositoryLesson, LessonRepository>();
builder.Services.AddRazorPages();

// Add UserManager service
builder.Services.AddScoped<UserManager<IdentityPuroguramuUser>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions());
app.UseReverseProxyLinks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
