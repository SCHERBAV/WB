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

namespace WebPrj
{
    //класс Startup является входной точкой в приложение ASP.NET CORE
    //Этот класс производит конфигурацию приложения, настраивает сервисы, которые приложение будет использовать,
    //устанавливает компоненты для обработки запроса или middleware.
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //метод ConfigureServices() регистрирует сервисы, которые используются приложением.
        //В качестве параметра он принимает объект IServiceCollection, который и представляет коллекцию сервисов в приложении.
        //С помощью методов расширений этого объекта производится конфигурация приложения для использования сервисов.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))) ;

            services.AddDatabaseDeveloperPageExceptionFilter();
           
            services.AddControllersWithViews();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //lb4.Для возможности использования ролей и для простых паролей
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //lb4. Для использования Razor страниц
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Метод Configure устанавливает, как приложение будет обрабатывать запрос. Этот метод является обязательным.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // если приложение в процессе разработки
            if (env.IsDevelopment())
            {
                // то выводим информацию об ошибке, при наличии ошибки
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
            app.UseStaticFiles();   //для использования статических файлов находящихся в папке wwwroot

            //добавляем возможности маршрутизации
            //Вызов app.UseRouting() добавляет некоторые возможности маршрутизации,
            //благодаря чему приложение может соотносить запросы с определенными маршрутами.
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            DbInitializer.Seed(context, userManager, roleManager).GetAwaiter().GetResult();


            //устанавливаем адреса, которые будут обрабатываться
            //Далее идет вызов app.UseEndpoints
            //(endpoints =>, который позволяет определить маршруты, которые будут обрабатываться приложением.
            app.UseEndpoints(endpoints =>
                                {
                                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                                    endpoints.MapRazorPages();
                                });   
        }
    }
}