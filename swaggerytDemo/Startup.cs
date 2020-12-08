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
using System.Threading.Tasks;

namespace swaggerytDemo
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

            //Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});

            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });

            services.AddSwaggerGen(config =>
            {
                var titlebase = "Ytdemo1";
                var desc = "Description";
                var termsofservice = new Uri("http://achrafbenalaya.com/");
                var license = new OpenApiLicense()
                {
                    Name = "MIT"
                };

                var contact = new OpenApiContact()
                {
                    Name = "achraf",
                    Email = "achrafbenalaya@gmail.com",
                    Url = new Uri("http://achrafbenalaya.com/")

                };


                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titlebase + " V 1",
                    Description = desc,
                    Contact = contact,
                    License = license,
                    TermsOfService = termsofservice


                });

                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = titlebase + " V2",
                    Description = desc,
                    Contact = contact,
                    License = license,
                    TermsOfService = termsofservice


                });

            }

      );





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c => {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");

            });



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
