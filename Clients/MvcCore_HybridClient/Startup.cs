﻿using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MvcCore_HybridClient.AuthorizationPolicy;
using System.IdentityModel.Tokens.Jwt;

namespace MvcCore_HybridClient
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
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options =>
            {
                //配置登录失败的跳转地址
                options.AccessDeniedPath = "/Account/AccessDenied";
            })   //添加Cookies
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>    //配置oidc
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "http://localhost:5000";  //配置信任的IdentityServer
                options.RequireHttpsMetadata = false;   //不使用HTTPS

                options.ClientId = "HybirdClient";
                options.ClientSecret = "bc6126ff-fcf2-4e67-a912-4e70cd2fb73d";
                options.SaveTokens = true;
                options.ResponseType = "code id_token";   //配置oidc相应类型，方式1，返回 AuthorizationCode和IdToken
                //options.ResponseType = "code id_token";   //配置oidc相应类型，方式2，返回 AuthorizationCode和IdToken
                //options.ResponseType = "code id_token";   //配置oidc相应类型，方式3，返回 AuthorizationCode和IdToken
                options.Scope.Clear();
                options.Scope.Add("api1");
                options.Scope.Add(OidcConstants.StandardScopes.OpenId);
                options.Scope.Add(OidcConstants.StandardScopes.Profile);
                options.Scope.Add(OidcConstants.StandardScopes.Email);
                options.Scope.Add(OidcConstants.StandardScopes.Phone);
                options.Scope.Add(OidcConstants.StandardScopes.Address);
                options.Scope.Add("roles");
                options.Scope.Add("locations");
                options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);  //获取refreshToken 用于获取刷新Access Token


                // ClaimActions集合里的东西 都是要被过滤掉的属性，nbf amr exp...
                //如果想不把默认过滤的东西过滤掉，需要在该集合中去除
                options.ClaimActions.Remove("nbf");
                options.ClaimActions.Remove("amr");
                options.ClaimActions.Remove("exp");


                // 如果想手动过滤掉一些Claim，需要向ClaimActions添加需要过滤的东西
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("sub");
                options.ClaimActions.DeleteClaim("idp");


                // 让IdentityServer里的Claim里面的角色映射到mvc系统识别的角色
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role
                };

            });


            //定义策略授权模式
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("SmithInSomewhere", builder =>
                //{
                //    builder.RequireAuthenticatedUser();
                //    builder.RequireClaim(JwtClaimTypes.FamilyName, "Smith");
                //    builder.RequireClaim("location", "cd");
                //});


                //自定义的授权策略
                options.AddPolicy("SmithInSomewhere", builder =>
                {
                    builder.AddRequirements(new SmithInSomewareRequirement());
                });
            });

            //将定义的策略注入到服务中
            services.AddSingleton<IAuthorizationHandler, SmithInSomewhereHandler>();
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
