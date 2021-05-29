using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPrj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                            {
                                //Метод webBuilder.UseStartup<Startup>() устанавливает класс Startup в качестве стартового.
                                //И при запуске приложения среда ASP.NET будет искать в сборке приложения класс с именем Startup и загружать его.
                                webBuilder.UseStartup<Startup>();
                            }).ConfigureLogging(lp => { lp.ClearProviders(); lp.AddFilter("Microsoft", LogLevel.None); });  //lb9. Указание фильтра логирования
    }
}
