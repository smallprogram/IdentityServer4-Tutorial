using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient_ClientCredentials
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region 获取IdentityServer4的AccessToken

            var getTokenClient = new HttpClient();
            //获取IdentityServer4的入口信息
            var disco = await getTokenClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            //获取失败后使用错误判断
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //以ClientCredentials方式向IS4请求Access Token
            //ClientCredentials方式只能请求API Scope，适用于机器与机器直接的认证，不适用于用户认证授权
            var tokenResponse = await getTokenClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,   //配置ClientCredentials的IS4请求入口

                ClientId = "client",     //配置传入的ClientID，需要与IS4的Client配置一致
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A", //配置传入的ClientSecret，需要与IS4的ClientSecret配置一致
                Scope = "api1"                   //配置传入的Scope，需要与IS4的Scope配置一致
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            //打印返回的accessToken
            Console.WriteLine(tokenResponse.Json);

            #endregion


            #region 使用AccessToken访问被保护的API资源

            var ConnectApiClient = new HttpClient();
            //将AccessToken添加到HttpClient中
            ConnectApiClient.SetBearerToken(tokenResponse.AccessToken);

            //使用带有AccessToken的httpClient访问受保护的API资源
            var response = await ConnectApiClient.GetAsync("http://localhost:5001/identity");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                //获取返回结果
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadLine();

            #endregion


        }
    }
}
