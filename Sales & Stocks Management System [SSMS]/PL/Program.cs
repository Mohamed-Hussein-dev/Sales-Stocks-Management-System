using BLL.Interfaces.Invoice;
using BLL.Repositories.Invoice;
using BLL.Interfaces;
using DAL.DbContexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories;
using BLL.Interfaces.Contact;
using BLL.Repositories.Contact;
using PL.MappingProfiles;
using PL.Healpers;
using Microsoft.Data.SqlClient;

namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Stock_SalseDbContext>();
            builder.Services.AddScoped<ISalesInvoice, SalesInvoiceRepository>();
            builder.Services.AddScoped<IPurchaseInvoice, PurchaseInvoiceRepository>();
            builder.Services.AddScoped<IProduct, ProductRepository>();
            builder.Services.AddScoped<ICustomer, CustomerRepository>();
            builder.Services.AddScoped<ISupplier, SupplierRepository>();
            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new CustomerProfile()));

            //ExcelToDb.ProcessExcelFile("C:\\Users\\Mohamed\\Downloads\\Telegram Desktop\\الاصناف.xlsx");
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
}