using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sign.Api.HealthChecks;
using Sign.Db;
using Sign.Logic.AutoMapperProfile;
using Sign.Logic.Interfaces;
using Sign.Models.Models;
using Sign.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sign.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHealthChecks()
                .AddCheck("Health Check", () =>
                    HealthCheckResult.Healthy("Health check OK"))
                .AddCheck("DB Check", ()=> DatabaseCheck.Check());


            services.AddStackExchangeRedisCache(options =>
            {
                var redisSettings = Configuration.GetSection("Redis").Get<RedisSettings>();
                options.Configuration = redisSettings.Server;
                options.InstanceName = "SignAPI_";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sign.Api", Version = "v1" });
            });

            services.AddDbContextFactory<SignContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db")));
            services.AddDbContext<SignContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db")));
            //AutoMapper
            var autoMapperConfig = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });
            var mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Rabbit MQ 
            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            //Worker Service 
            if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<Receiver>();
            }

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ISignManager).Assembly);

            // Add all the dependencies in IoC.Dependencies file to keep this file clean
            IoC.Dependencies.ConfigureDependencies(services);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Running Sign.Db => DbInitializer to seed test data 
                DBInitializer.Initialize(app);

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sign.Api v1"));
            }
            // Running migrations to update database 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SignContext>();
                context.Database.Migrate();
            }
            // Add api prefix to use it in docker-compose
            app.UsePathBase(new PathString("/api"));

            // comment out Https redirection. Dotnet certrificate covers "https://localhost" but not  "host.docker.internal"
            // and this causes an SSL issue during development. Either implement SAN Certificates
            // Check : http://logicwiki.co.uk/SSL_SAN_Self_Signed_Certificate_Creation_in_PowerShell
            //--------------------------------------------------------------------------------------------------------------           
            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
            });
        }
    }
}
