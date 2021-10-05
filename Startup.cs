using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Builder;

namespace Commander
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

            //////////////////////////
            //services.Configure<Commander.SecretStrings>(Configuration.GetSection(nameof(Commander.SecretStrings)));
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<Commander.SecretStrings>();
            
            var dom = builder.Build();

            string sUsername = string.Empty;

            string sPassword = string.Empty;

            sUsername = dom["MVCRestAPIUsername"];
            sPassword = dom["MVCRestAPIPassword"];
 
            //////////////////////////
            string sConnectionString = String.Empty;
            
            sConnectionString = Configuration.GetConnectionString("CommanderConnection");
            
            sConnectionString = sConnectionString.Replace("user$", sUsername);

            sConnectionString = sConnectionString.Replace("pass$", sPassword);

            System.Console.WriteLine("Connection String = " + sConnectionString);

            services.AddDbContext<Commander.Data.CommanderContext>(opt => opt.UseSqlServer(sConnectionString));

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddScoped<Commander.Data.ICommanderRepo, Commander.Data.MockCommanderRepo>();
            services.AddScoped<Commander.Data.ICommanderRepo, Commander.Data.SqlCommanderRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Commander", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commander v1"));
            }

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