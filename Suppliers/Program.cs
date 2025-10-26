using FluentValidation;
using Serilog;
using Suppliers.Configuration;
using Suppliers.DAO;
using Suppliers.DTO;
using Suppliers.Services;
using Suppliers.Validators;
using AutoMapper;

namespace Suppliers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, config) =>
            {
            config.ReadFrom.Configuration(context.Configuration);
                    /*.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                    .WriteTo.Console()
                    .WriteTo.File(
                        "Logs/logs.txt",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {SourceContext} {level}]" + "{Message}{NewLine}{Exception}",
                        retainedFileCountLimit: null,
                        fileSizeLimitBytes: null
                    );*/
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<ISupplierDAO, SupplierDAOImpl>();
            builder.Services.AddScoped<ISupplierService, SupplierServiceImpl>();
            builder.Services.AddScoped<IValidator<SupplierInsertDTO>, SupplierInsertValidator>();
            builder.Services.AddScoped<IValidator<SupplierUpdateDTO>, SupplierUpdateValidator>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
