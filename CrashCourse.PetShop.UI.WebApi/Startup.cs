using System;
using CrashCourse.PetShop.Auth.Helpers;
using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Domain.Services;
using CrashCourse.PetShop.Infrastructure.Data;
using CrashCourse.PetShop.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CrashCourse.PetShop.UI.WebApi
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
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    //ValidAudience = "CoMetaApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "CoMetaApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CSharpCrashCourse.PetShop.UI.WebApi", Version = "v1"});
            });
         
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            services.AddDbContext<PetShopContext>(
                opt =>
                {
                    opt
                        .UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=petShop.db");
                }, ServiceLifetime.Transient );
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSharpCrashCourse.PetShop.UI.WebApi v1"));

                
                // If in development mode, wipe the local database.
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetShopContext>();
                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
    
}