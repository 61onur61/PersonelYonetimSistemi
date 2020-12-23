using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Business.Implementation;
using PersonelYonetimSistemi.Common.EmailOperations;
using PersonelYonetimSistemi.Common.Mappings;
using PersonelYonetimSistemi.Common.ResultMesajModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;
using PersonelYonetimSistemi.Data.Implementation;
using PersonelYonetimSistemi.Data.Models;

namespace PersonelYonetimSistemi.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PersonelYonetimSistemiContext>
                (Options => Options.UseSqlServer(Configuration.GetConnectionString("IdentitySqlConnection")));
            //services.AddSingleton<IEmailSender, EmailSender>();
            //services.Configure<EmailKeys>(Configuration);

            services.AddScoped<IPersonelIzinRepository, PersonelIzinRepository>();
            services.AddScoped<IPersonelIzinTipiRepository, PersonelIzinTipiRepository>();
            services.AddScoped<IPersonelIzinOnayRepository, PersonelIzinOnayRepository>();
            services.AddScoped<IPersonelIsEmirleriRepository, PersonelIsEmirleriRepository>();
            services.AddScoped<IPersonelRepository, PersonelRepository>();

            services.AddScoped<IPersonelIzinTipiBusiness, PersonelIzinTipiBusiness>();
            services.AddScoped<IPersonelIzinOnayBusiness, PersonelIzinOnayBusiness>();
            services.AddScoped<IPersonelIzinAtamaBusiness, PersonelIzinAtamaBusiness>();
            services.AddScoped<IPersonelIsEmirleriBusiness, PersonelIsEmirleriBusiness>();
            services.AddScoped<IPersonelBusiness, PersonelBusiness>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Maps));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PersonelYonetimSistemiContext>();
            services.AddIdentity<Personels, IdentityRole>().AddDefaultTokenProviders()
               .AddEntityFrameworkStores<PersonelYonetimSistemiContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<Personels> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            SeedData.Seed(userManager, roleManager);
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
