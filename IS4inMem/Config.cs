// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS4inMem
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),   //代表该请求是openid请求
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource("roles","角色",new List<string>{JwtClaimTypes.Role}), //为Identity资源添加角色Clams
                new IdentityResource("locations", "地点", new List<string> { "location" }), //为Identity资源添加地点Clams
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1", new List<string> {"locations" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },


                new Client
                {
                    ClientId = "ResourceOwnerPassword client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets = {new Secret("ResourceOwnerPasswordClient".Sha256()) },
                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address

                    }

                },


                // MVC client using CodeAndClientCredentials flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,  //返回refreshToken

                    AlwaysIncludeUserClaimsInIdToken = true, //总是返回带有用户Claims的idToken

                    AccessTokenLifetime = 60,  //AccessToken的过期时间，单位是秒

                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address
                    }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "AngularClient",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:4200",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RequireConsent = true, //是否需要用户点击同意
                    AccessTokenLifetime = 60 *5, //AccessToken过期时间 单位是秒


                    RedirectUris =
                    {
                        "http://localhost:4200/signin-oidc",    //登录之后的跳转地址
                        "http://localhost:4200/redirect-silentrenew"   //刷新accessToken的跳转地址
                        //"http://localhost:5002/callback.html",
                        //"http://localhost:5002/silent.html",
                        //"http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:4200" },  //登出之后的跳转地址
                    AllowedCorsOrigins = { "http://localhost:4200" },     //允许跨域访问的地址

                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address
                    }
                },

                //hybrid flow client
                new Client
                {
                    ClientId = "HybirdClient",
                    ClientName = "Asp.net Core hybird Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("bc6126ff-fcf2-4e67-a912-4e70cd2fb73d".Sha256()) },

                    RedirectUris = { "http://localhost:5004/signin-oidc" },   //登录之后的跳转地址
                    PostLogoutRedirectUris = { "http://localhost:5004/signout-callback-oidc" },  //登出之后的跳转地址

                    AllowOfflineAccess = true, //返回refreshToken

                    AlwaysIncludeUserClaimsInIdToken = true, //总是返回带有用户Claims的idToken

                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "locations"
                    }
                }
            };
        }
    }
}