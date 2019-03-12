using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient_ResourceOwnerPassword
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var getTokenClient = new HttpClient();
            //获取IdentityServer4的入口信息
            var disco = await getTokenClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            //获取失败后使用错误判断
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //以ResourceOwnerPassword方式向IS4请求Access Token
            var tokenResponse = await getTokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,   //配置IS4请求入口

                ClientId = "ResourceOwnerPassword client",     //配置传入的ClientID，需要与IS4的Client配置一致
                ClientSecret = "ResourceOwnerPasswordClient", //配置传入的ClientSecret，需要与IS4的ClientSecret配置一致

                UserName = "alice",  
                Password = "alice",

                Scope = "api1"                   //配置传入的Scope，需要与IS4的Scope配置一致
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            //打印返回的accessToken
            Console.WriteLine(tokenResponse.Json);

            //使用AccessToken访问被保护的API资源
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
        }
    }
}
