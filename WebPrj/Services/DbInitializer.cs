using System.Linq;
using System.Threading.Tasks;
using WebPrj.DAL.Data;
using WebPrj.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebPrj.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //lb4. Если БД еще не создана, создать ее
            context.Database.EnsureCreated();

            //lb4. Проверка наличия ролей
            if (!context.Roles.Any())
            {
                IdentityRole roleAdmin = new IdentityRole { Name = "admin", NormalizedName = "admin" };
                await roleManager.CreateAsync(roleAdmin);   //создание роли "admin"
            }
            
            //lb4. Проверка наличия пользователей
            if(!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser { Email = "user@mail.ru", UserName = "user@mail.ru" };
                await userManager.CreateAsync(user, "qwerty");    //создание пользователя "user"

                ApplicationUser admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
                await userManager.CreateAsync(admin, "qwerty");    //создание пользователя "admin"

                admin = await userManager.FindByNameAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");   //назначение пользователю "admin", роли "admin"
            }
        }
    }
}