using Forex.Entities;
using Forex.Entities.Repository;
using Forex.Entities.UOW;
using Forex.Services.FixerService;
using Forex.Services.ForexService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forex.WebApi
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
            services.AddDbContext<ForexDbContext>();
            services.AddControllers()
                .AddJsonOptions(option => {
                    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    option.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            services.AddCors(option => option.AddDefaultPolicy(
                                            policyBuilder => policyBuilder.AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Forex.WebApi", Version = "v1" });
            });

            configureIoC(services);
        }

        private void configureIoC(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFixerService, FixerService>();
            services.AddScoped<IForexService, ForexService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forex.WebApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
