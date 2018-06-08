using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Test_App.Data;
using Test_App.Data.Infrastructure.Interfaces;
using Test_App.Data.Infrastructure.Logic;
using Test_App.Data.Repositories.Interfaces;
using Test_App.Data.Repositories.Logic;
using Test_App.Service.Services.Interfaces;
using Test_App.Service.Services.Logic;

namespace Test_App
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Конфигурация PostgreSQL

            var dbConnectionString = _configuration["DbConnectionString"] ?? _configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<Test_AppContext>(options =>
                {
                    options.UseNpgsql(dbConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorCodesToAdd: null);
                        });
                }, ServiceLifetime.Scoped);

            #endregion

            #region Добавление документации

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("testapp", new Info { Title = "Пользователи", Version = "testapp" });

                var xmlPath = Path.Combine(_env.ContentRootPath, "Test_App.xml");
                c.IncludeXmlComments(xmlPath);
                c.IgnoreObsoleteProperties();
            });

            #endregion

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();

            services.AddMvc();

            return services.BuildServiceProvider();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Применение миграций

            var dbContext = app.ApplicationServices.GetService<Test_AppContext>();
            dbContext.Database.Migrate();

            #endregion

            #region Подключение документации

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ShowJsonEditor();
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/testapp/swagger.json", "testapp");
            });

            #endregion

            app.UseMvcWithDefaultRoute();
        }
    }
}
