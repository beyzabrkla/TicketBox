using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application;
using TicketBox.Application.Features.Common.Behaviours;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        //AutoMapper kaydı
        //Hem Web hem Application katmanındaki profilleri bulması için
        builder.Services.AddAutoMapper(
            typeof(ApplicationAssemblyReference).Assembly, // Application'ı tara
            typeof(Program).Assembly // Kendi projenin içini de tara
        );

        // FluentValidation: ApplicationAssemblyReference sınıfının olduğu DLL'i tara
        builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        builder.Services.AddHttpContextAccessor();


        // DbContext kaydı
        // Bağlantı dizesini appsettings.json'dan çekiyoruz
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // TicketContext'i bu dize ile kaydediyoruz
        builder.Services.AddDbContext<TicketContext>(options =>
            options.UseSqlServer(connectionString));

        //Bağlı olduğu interface eşlemesi
        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<TicketContext>());

        // MediatR: GetEventQuery'nin olduğu (Application) katmanı tara
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //Pipeline (Validation) için kritik kayıt !!!
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        // Identity servislerini ekle
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

        // Cookie ayarları
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/SignIn";
            options.AccessDeniedPath = "/Auth/AccessDenied";
        });


        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAreaControllerRoute(
            name: "AdminArea",
            areaName: "Admin",
            pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapAreaControllerRoute(
            name: "UserArea",
            areaName: "User",
            pattern: "User/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}