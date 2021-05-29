using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPrj.DAL.Data;
using WebPrj.DAL.Entities;
using WebPrj.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebPrj.Models;

//lb9
using Microsoft.Extensions.Logging;
using WebPrj.Extensions;    //��� ������ ���������� ������ IApplicationBuilder

namespace WebPrj
{
    //����� Startup �������� ������� ������ � ���������� ASP.NET CORE
    //���� ����� ���������� ������������ ����������, ����������� �������, ������� ���������� ����� ������������,
    //������������� ���������� ��� ��������� ������� ��� middleware.
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //����� ConfigureServices() ������������ �������, ������� ������������ �����������.
        //� �������� ��������� �� ��������� ������ IServiceCollection, ������� � ������������ ��������� �������� � ����������.
        //� ������� ������� ���������� ����� ������� ������������ ������������ ���������� ��� ������������� ��������.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //lb4.��� ����������� ������������� ����� � ��� ������� �������
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //lb4. ��� ������������� Razor �������
            services.AddRazorPages();

            //lb8. ��� ������������� ������ � �������
            services.AddDistributedMemoryCache();
            services.AddSession(opt => { opt.Cookie.HttpOnly = true; opt.Cookie.IsEssential = true; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<Cart>(sp => CartService.GetCart(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //����� Configure �������������, ��� ���������� ����� ������������ ������. ���� ����� �������� ������������.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory logger)
        {
            //lb9. 4.1 ����������� ������
            logger.AddFile("Logs/log-{Date}.txt");

            // ���� ���������� � �������� ����������
            if (env.IsDevelopment())
            {
                // �� ������� ���������� �� ������, ��� ������� ������
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();   //��� ������������� ����������� ������ ����������� � ����� wwwroot

            //��������� ����������� �������������
            //����� app.UseRouting() ��������� ��������� ����������� �������������,
            //��������� ���� ���������� ����� ���������� ������� � ������������� ����������.
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //lb8.
            app.UseSession();

            DbInitializer.Seed(context, userManager, roleManager).GetAwaiter().GetResult();

            //lb9.
            app.UseFileLogging();


            //������������� ������, ������� ����� ��������������
            //����� ���� ����� app.UseEndpoints
            //(endpoints =>, ������� ��������� ���������� ��������, ������� ����� �������������� �����������.
            app.UseEndpoints(endpoints =>
                                {
                                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                                    endpoints.MapRazorPages();
                                });   
        }
    }
}