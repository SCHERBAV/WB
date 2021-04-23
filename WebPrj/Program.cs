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
                                //ћетод webBuilder.UseStartup<Startup>() устанавливает класс Startup в качестве стартового.
                                //» при запуске приложени€ среда ASP.NET будет искать в сборке приложени€ класс с именем Startup и загружать его.
                                webBuilder.UseStartup<Startup>();
                            });
    }
}
