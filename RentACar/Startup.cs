using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using RentACar.UI.Helpers;
using RentACar.UI.Services.Abstract;
using RentACar.UI.Services.Concrete;
using System;
using System.Net.Mail;

namespace RentACar
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
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // services.AddScoped<AppIdentityDbContext, AppIdentityDbContext>();
            services.AddScoped<IFuelDal, EFFuelDal>();
            services.AddScoped<ICarDal, EFCarDal>();
            services.AddScoped<IBrandDal, EFBrandDal>();
            services.AddScoped<IRentDal, EFRentDal>();
            services.AddScoped<IPhotoDal, EFPhotoDal>();
            services.AddScoped<IColorDal, EFColorDal>();
            services.AddScoped<IGearBoxDal, EFGearBoxDal>();
            services.AddScoped<IPhotoService, PhotoManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<ICarService, CarManager>();
            services.AddScoped<IRentDetailDal, EFRentDetailDal>();
            services.AddScoped<IRentDetailService, RentDetailManager>();
            services.AddScoped<IFuelService, FuelManager>();
            services.AddScoped<IGearBoxService, GearBoxManager>();
            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<ISmtpService, SmtpService>();
            services.AddScoped<SmtpClient, SmtpClient>();
            services.AddScoped<IRentCookieeService, RentCookieeService>();
            services.AddScoped<ICarCookieeService, CarCookieeService>();
            services.AddScoped<IRentService, RentManager>();

            services.AddDbContext<AppIdentityDbContext>(optionsAction => optionsAction.UseSqlServer(@"Data Source=DESKTOP-EMPIGDT;Initial Catalog=RentACar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddTokenProvider("Email", typeof(EmailTokenProvider<Customer>)).AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.ConfigureApplicationCookie(x =>
            {
                x.Cookie.Name = "RentCarCookie";
                x.Cookie.HttpOnly = true;
                x.Cookie.SameSite = SameSiteMode.Strict;
                x.LoginPath = "/Account/Login";
                x.LogoutPath = "/Account/Logout";
                x.ExpireTimeSpan = TimeSpan.FromDays(30);

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
            services.AddAutoMapper(typeof(AutoMapperProfiles));


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
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Car}/{action=index}/{id?}");

                endpoints.MapAreaControllerRoute(
                 name: "areaRoute",
                 areaName: "Admin",
                 pattern: "{area:exists}/{controller=Admin}/{action=HomePage}/{id?}");

                endpoints.MapControllerRoute(name: "Login", "{controller=Account}/{action=Login}");

                endpoints.MapControllerRoute("AccesDenied", "{controller=Account}/{action=Login}");





            });
        }
    }
}