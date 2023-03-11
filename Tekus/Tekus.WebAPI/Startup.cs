using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Data;
using Tekus.BusinessLogic.Logic;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.WebAPI.DTOs;
using Tekus.WebAPI.Middleware;

namespace Tekus.WebAPI
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
            services.AddScoped<ITokenService, TokenService>();
            var builder = services.AddIdentityCore<User>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();

            builder.AddEntityFrameworkStores<SecurityDbContext>();
            builder.AddSignInManager<SignInManager<User>>();
                
            services.AddAuthentication();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };

            });

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IGenericRepository<>),
                typeof(GenericRepository<>));


            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped(typeof(IGenericSecurityRepository<>), (typeof(GenericSecurityRepository<>)));
 


            services.AddDbContext<TekusDbContext>
                (options => {
                    options.UseSqlServer(
                    Configuration.GetConnectionString
                    ("DefaultConnection"));
                });

            services.AddDbContext<SecurityDbContext>(x => 
            {
                x.UseSqlServer(Configuration.GetConnectionString("IdentitySecurity"));
            });
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddControllers();
            services.AddCors(options => 
            {
                options.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins("*");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tekus.WebAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tekus.WebAPI v1"));

            }
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors","?code?{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
