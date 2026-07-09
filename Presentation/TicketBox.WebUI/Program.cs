using FluentValidation;
using MediatR;
using TicketBox.Application;
using TicketBox.Application.Features.Categories.Handlers;
using TicketBox.Application.Features.Common.Behaviours;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Domain.Interfaces;
using TicketBox.Persistance.Context;
using TicketBox.Persistance.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        //AutoMapper ve Validation kaydı
        builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
        builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        builder.Services.AddHttpContextAccessor();


        //Repository ve DbContext kaydı
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddDbContext<TicketContext>();

        //MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetEventQuery).Assembly);
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}