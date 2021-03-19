
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFind.Middlewares
{
    public static class CustomConfigureMiddleware
    {
      
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/apidoc/swagger.json", "JOB FIND API");
            });
        }

        public static void UseCustomEndpoint(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               
            });

            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });
        }
    }
}
