using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Sign.Db
{
    public static class DBInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SignContext>();
                
                context.Database.EnsureCreated();

                if (!context.Signs.Any())
                {
                    var signs = Initializes.SignSeed.Seed();
                    context.Signs.AddRange(signs);
                    context.SaveChanges();
                }
            }
        }
    }
}
