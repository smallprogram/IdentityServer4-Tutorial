using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiResource
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //配置API项目开启认证服务
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //配置API项目认证方式以及认证服务器地址等
            //用于验证传入accessToken的有效性
            //使用的是JWT Token
            //services.AddAuthentication("Bearer")   //身份验证服务添加到DI并配置"Bearer"为默认方案
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.Audience = "api1";   //与IdentityServerAPI中的值保持一致

            //        options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);  //每隔多长时间验证token
            //        options.TokenValidationParameters.RequireExpirationTime = true;  //要求Token必须有超时时间
            //    });


            //使用的是Reference Token
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.ApiName = "api1";
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "bc6126ff-fcf2-4e67-a912-4e70cd2fb73d";
                });

            services.AddMemoryCache();

            //添加Angular跨域请求策略
            services.AddCors(options =>
            {
                options.AddPolicy("AngularClientOrigin",  //策略名字
                    builder => builder.AllowAnyOrigin()   //允许任何组织
                    .AllowAnyHeader()                     //允许任何头
                    .AllowAnyMethod());                   //允许任何方法
            });

            //向服务添加之前配置的策略信息
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AngularClientOrigin"));
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AngularClientOrigin");

            //添加认证服务到管道
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
