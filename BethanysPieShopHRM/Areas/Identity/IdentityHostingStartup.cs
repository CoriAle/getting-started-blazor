using System;
using BethanysPieShopHRM.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BethanysPieShopHRM.Areas.Identity.IdentityHostingStartup))]
namespace BethanysPieShopHRM.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BethanysPieShopHRMContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BethanysPieShopHRMContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BethanysPieShopHRMContext>();
            });
        }
    }
}