using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestAPI.Business;
using RestAPI.Data.VOs;
using RestAPI.Models;
using RestAPI.Repository;
using Serilog;
using System;
using System.Collections.Generic;

namespace RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers();

            var connectionString = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 0))));

            if (Environment.IsDevelopment())
                MigrateDatabase(connectionString);

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            }).AddXmlSerializerFormatters();

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "REST API with .NET Core 3.1",
                        Version = "v1",
                        Description = "REST API developed with .NET Core 3.1.",
                        Contact = new OpenApiContact
                        {
                            Name = "Vinícius Pereira",
                            Url = new Uri("https://github.com/vinirossa")
                        }
                    });
            });

            services.AddScoped<IBusiness<PersonVO>, PersonBusiness>();
            services.AddScoped<IBusiness<BookVO>, BookBusiness>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        private void MigrateDatabase(string connectionString)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "Db/Migrations", "Db/Datasets" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API with .NET Core 3.1 - v1");  });

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
