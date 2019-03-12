using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcCoreClient
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //关闭JWT类型映射声明
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;   //配置默认cookie的名字
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;   //配置OpenID Connect的默认名字
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)   //添加Cookies
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>    //配置oidc
             {
                 options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.Authority = "http://localhost:5000";  //配置信任的IdentityServer
                 options.RequireHttpsMetadata = false;   //不使用HTTPS

                 options.ClientId = "mvc";
                 options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                 options.SaveTokens = true;
                 options.ResponseType = "code";   //配置oidc只获取授权码Authentization code
                 options.Scope.Clear();
                 options.Scope.Add("api1");
                 options.Scope.Add(OidcConstants.StandardScopes.OpenId);
                 options.Scope.Add(OidcConstants.StandardScopes.Profile);
                 options.Scope.Add(OidcConstants.StandardScopes.Email);
                 options.Scope.Add(OidcConstants.StandardScopes.Phone);
                 options.Scope.Add(OidcConstants.StandardScopes.Address);
                 options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);  //获取refreshToken
             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
