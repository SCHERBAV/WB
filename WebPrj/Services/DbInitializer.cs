using System.Linq;
using System.Threading.Tasks;
using WebPrj.DAL.Data;
using WebPrj.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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
            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser { Email = "user@mail.ru", UserName = "user@mail.ru" };
                await userManager.CreateAsync(user, "qwerty");    //создание пользователя "user"

                ApplicationUser admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
                await userManager.CreateAsync(admin, "qwerty");    //создание пользователя "admin"

                admin = await userManager.FindByNameAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");   //назначение пользователю "admin", роли "admin"
            }

            //lb 8. Проверка наличия групп объектов(в данном случае производителей)
            if (!context.Producers.Any())
            {
                context.Producers.AddRange(new List<Producer> {
                                                                new Producer()
                                                                {
                                                                    ProducerName = "Acer",
                                                                    Country = "Тайвань"
                                                                },
                                                                new Producer()
                                                                {
                                                                    ProducerName = "Asus",
                                                                    Country = "Тайвань"
                                                                },
                                                                new Producer()
                                                                {
                                                                    ProducerName = "Apple",
                                                                    Country = "США"
                                                                },
                                                                new Producer()
                                                                {
                                                                    ProducerName = "Lenovo",
                                                                    Country = "Китай"
                                                                }
                });
                await context.SaveChangesAsync();
            }

            //lb 8. Проверка наличия объектов(в данном случае ноутбуков)
            if (!context.Laptops.Any()) 
            {
                context.Laptops.AddRange(new List<Laptop>{ 
                                                                new Laptop()
                                                                {
                                                                    Model = "Nitro 5 AN517-52-74G2",
                                                                    Processor = "Intel Core i7",
                                                                    RAM = "8 gb",
                                                                    Graphics = "NVIDIA GeForce RTX 3060",
                                                                    SSD = "512 gb",
                                                                    Image = "Acer Nitro 5 AN517-52-74G2 NH.QAWEP.004.jpeg",
                                                                    Price = 1730,
                                                                    ProducerId = 1
                                                                },
                                                                new Laptop()
                                                                {
                                                                    Model = "TUF Gaming Dash F15 FX516PM-HN130T",
                                                                    Processor = "Intel Core i7",
                                                                    RAM = "16 gb",
                                                                    Graphics = "NVIDIA GeForce RTX 3060",
                                                                    SSD = "512 gb",
                                                                    Image = "ASUS TUF Gaming Dash F15 FX516PM-HN130T.jpeg",
                                                                    Price = 1564,
                                                                    ProducerId = 2,
                                                                },
                                                                new Laptop()
                                                                {
                                                                    Model = "Macbook Pro M1 2020 MYD82",
                                                                    Processor = "Apple M1",
                                                                    RAM = "8 gb",
                                                                    Graphics = "Apple M1 GPU",
                                                                    SSD = "256 gb",
                                                                    Image = "Apple Macbook Pro 13 M1 2020 MYD82.jpeg",
                                                                    Price = 2007,
                                                                    ProducerId = 3
                                                                },
                                                                new Laptop()
                                                                {
                                                                    Model = "IdeaPad L340-15IRH Gaming 81LK00Q4RE",
                                                                    Processor = "Intel Core i5",
                                                                    RAM = "8 gb",
                                                                    Graphics = "NVIDIA GeForce GTX 1050",
                                                                    SSD = "512 gb",
                                                                    Image = "Lenovo IdeaPad L340-15IRH Gaming 81LK00Q4RE.jpeg",
                                                                    Price = 826,
                                                                    ProducerId = 4
                                                                },
                                                                new Laptop()
                                                                {
                                                                    Model = "TUF Gaming F15 FX506LI-HN012",
                                                                    Processor = "Intel Core i5",
                                                                    RAM = "8 gb",
                                                                    Graphics = "NVIDIA GeForce GTX 1650 Ti",
                                                                    SSD = "512 gb",
                                                                    Image = "ASUS TUF Gaming F15 FX506LI-HN012.jpeg",
                                                                    Price = 1181,
                                                                    ProducerId = 2
                                                                },
                                                                new Laptop()
                                                                {
                                                                    Model = "Aspire 3 A314-22-A5LQ NX.HVVER.005",
                                                                    Processor = "AMD 3020e",
                                                                    RAM = "4 gb",
                                                                    Graphics = "Integrated",
                                                                    HDD = "500 gb",
                                                                    Image = "Acer Aspire 3 A314-22-A5LQ NX.HVVER.005.jpeg",
                                                                    Price = 431,
                                                                    ProducerId = 1
                                                                }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}