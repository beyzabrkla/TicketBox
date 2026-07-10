using FluentValidation;
using MediatR;
using TicketBox.Application;
using TicketBox.Application.Features.Common.Behaviours;
using TicketBox.Domain.Interfaces;
using TicketBox.Persistance.Context;
using TicketBox.Persistance.Repositories;

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


        //Repository ve DbContext kaydı
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddDbContext<TicketContext>();
        
        // MediatR: GetEventQuery'nin olduğu (Application) katmanı tara
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        });

        //Pipeline (Validation) için kritik kayıt !!!
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


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