using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPrj.Middleware;
using Microsoft.AspNetCore.Builder;

namespace WebPrj.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app) => app.UseMiddleware<LogMiddleware>();
    }
}
