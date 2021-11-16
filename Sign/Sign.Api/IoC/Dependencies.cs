using Microsoft.Extensions.DependencyInjection;
using Sign.Db.Interfaces;
using Sign.Db.Repositories;
using Sign.Logic.Interfaces;
using Sign.Logic.Managers;
using Sign.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sign.Api.IoC
{
    public static class Dependencies
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            // Repositories
            services.AddTransient<ISignRepository, SignRepository>();

            //Managers
            services.AddTransient<ISignManager, SignManager>();


            // Worker
            services.AddTransient<ISender, Sender>();

        }
    }
}
