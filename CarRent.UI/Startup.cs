using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentACar.BL.Abstract;
using RentACar.BL.Concrete;
using RentACar.Dal.Abstract.EF;
using RentACar.Dal.Concrete.EF;
using RentACar.Dal.Contexts;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.UI
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
            services.AddControllersWithViews();
            services.AddScoped<IFuelDal, EFFuelDal>();
            services.AddScoped<ICarDal, EFCarDal>();
            services.AddScoped<IBrandDal, EFBrandDal>();
            services.AddScoped<IPhotoDal, EFPhotoDal>();
            services.AddScoped<IPhotoService, PhotoManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<ICarService, CarManager>();
            services.AddDbContext<AppIdentityDbContext>(optionsAction => optionsAction.UseSqlServer(@"Data Source=DESKTOP-EMPIGDT;Initial Catalog=CarRent;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False"));
            services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddTokenProvider("Email", typeof(EmailTokenProvider<Customer>)).AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.Cookie.Name = "RentACarCookie";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
            });


            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Car}/{action=Index}/{id?}");
            });
        }
    }
}
