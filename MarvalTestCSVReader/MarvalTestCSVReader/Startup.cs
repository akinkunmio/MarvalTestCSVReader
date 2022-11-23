using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MarvalTestCSVReader.Data;
using MarvalTestCSVReader.Helpers;
using MarvalTestCSVReader.Models.Core;
using MarvalTestCSVReader.Models;
using Microsoft.OpenApi.Models;

namespace MarvalTestCSVReader
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
          
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            EnsureDatabaseExists(connectionString);

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Marvel API",
            //        Version = "v1",
            //        Description = "Description for the API goes here.",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Akinkunmi Okunola",
            //            Email = string.Empty,
            //            Url = new Uri("https://coderjony.com/"),
            //        },
            //    });
            //});
            services.AddTransient<IFileReader, CsvFileReader>();
            services.AddSingleton<PersonRow>();
            services.AddTransient<IFileContentValidator<PersonRow, PersonContext>, FileContentValidator>();

            services.AddCors();
            services.AddRazorPages();
            services.AddControllers(); 
        }

        private void EnsureDatabaseExists(string connection)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connection);

            using var context = new ApplicationDbContext(builder.Options);
            context.Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marvel API V1");

            //    c.RoutePrefix = string.Empty;
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              );

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapRazorPages();
            //});
        }
    }
}
