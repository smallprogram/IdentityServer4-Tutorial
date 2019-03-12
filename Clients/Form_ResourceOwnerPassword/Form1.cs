using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Form_ResourceOwnerPassword
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tb_username.Text = "";
            tb_username.Text = "";
        }

        private string _accessToken { get; set; }
        private DiscoveryResponse _disco;

        private async void btn_GetAccessToken_Click(object sender, EventArgs e)
        {
            rtb_accessToken.Text = "";
            rtb_Source.Text = "";
            rtb_IdentitySource.Text = "";

            if (string.IsNullOrEmpty(tb_password.Text) || string.IsNullOrEmpty(tb_username.Text))
            {
                MessageBox.Show("请填写完整的用户名和密码！");
                return;
            }



            var getTokenClient = new HttpClient();
            //获取IdentityServer4的入口信息
            this._disco = await getTokenClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            //获取失败后使用错误判断
            if (_disco.IsError)
            {
                MessageBox.Show(_disco.Error);
                return;
            }

            //以ResourceOwnerPassword方式向IS4请求Access Token
            var tokenResponse = await getTokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = _disco.TokenEndpoint,   //配置IS4请求入口

                ClientId = "ResourceOwnerPassword client",     //配置传入的ClientID，需要与IS4的Client配置一致
                ClientSecret = "ResourceOwnerPasswordClient", //配置传入的ClientSecret，需要与IS4的ClientSecret配置一致

                UserName = tb_username.Text.Trim(),
                Password = tb_password.Text.Trim(),

                Scope = "api1 openid profile phone email address"                   //配置传入的Scope，需要与IS4的Scope配置一致

            });
            rtb_accessToken.Text = $"用户名为 {tb_username.Text}  密码为 {tb_password.Text} \n";
            if (tokenResponse.IsError)
            {
                MessageBox.Show(tokenResponse.Error);
                return;
            }
            //打印返回的accessToken
            rtb_accessToken.Text += tokenResponse.Json;
            this._accessToken = tokenResponse.AccessToken;
        }

        private async void btn_GetSourceAPI_Click(object sender, EventArgs e)
        {
            rtb_Source.Text = "";

            //if (String.IsNullOrEmpty(this.accessToken))
            //{
            //    MessageBox.Show("请先获取accessToken再进行保护资源的请求");
            //    return;
            //}

            //使用AccessToken访问被保护的API资源
            var ConnectApiClient = new HttpClient();
            //将AccessToken添加到HttpClient中
            ConnectApiClient.SetBearerToken(this._accessToken);

            //使用带有AccessToken的httpClient访问受保护的API资源
            var response = await ConnectApiClient.GetAsync("http://localhost:5001/identity");

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            else
            {
                //获取返回结果

                var content = await response.Content.ReadAsStringAsync();
                rtb_Source.Text = content;

            }


        }

        private async void btn_GetIdentitySource_Click(object sender, EventArgs e)
        {

            rtb_IdentitySource.Text = "";

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(_accessToken);

            var response = await apiClient.GetAsync(_disco.UserInfoEndpoint); //使用IdentityServier发现文档中UserInfoEndPoint作为入口访问Identity保护资源
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                rtb_IdentitySource.Text = content;
            }
        }
    }
}
