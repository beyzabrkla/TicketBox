using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application;
using TicketBox.Application.Features.Common.Behaviours;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;
using TicketBox.Persistance.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // AutoMapper kaydı
        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<TicketBox.Application.Mapping.TicketProfile>();
        }, typeof(ApplicationAssemblyReference).Assembly);

        // FluentValidation
        builder.Services.AddValidatorsFromAssembly(
            typeof(ApplicationAssemblyReference).Assembly,
            ServiceLifetime.Scoped);
        
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

        // DbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<TicketContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<TicketContext>());


        builder.Services.AddScoped<IEmailService, EmailService>();

        // MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
            // Pipeline (Validation) davranışı
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<TicketContext>()
        .AddDefaultTokenProviders();

        // Cookie
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/SignIn";
            options.AccessDeniedPath = "/Auth/AccessDenied";
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Route Tanımları
        app.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Events}/{action=Index}/{id?}");

        app.MapAreaControllerRoute(
            name: "UserArea",
            areaName: "User",
            pattern: "User/{controller=UserDashboard}/{action=MyTickets}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}