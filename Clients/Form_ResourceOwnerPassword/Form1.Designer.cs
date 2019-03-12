namespace Form_ResourceOwnerPassword
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.btn_GetAccessToken = new System.Windows.Forms.Button();
            this.rtb_accessToken = new System.Windows.Forms.RichTextBox();
            this.btn_GetSourceAPI = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rtb_Source = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtb_IdentitySource = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_GetIdentitySource = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(84, 16);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(165, 21);
            this.tb_username.TabIndex = 1;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(334, 16);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(184, 21);
            this.tb_password.TabIndex = 2;
            // 
            // btn_GetAccessToken
            // 
            this.btn_GetAccessToken.Location = new System.Drawing.Point(566, 3);
            this.btn_GetAccessToken.Name = "btn_GetAccessToken";
            this.btn_GetAccessToken.Size = new System.Drawing.Size(133, 44);
            this.btn_GetAccessToken.TabIndex = 3;
            this.btn_GetAccessToken.Text = "获取AccessToken";
            this.btn_GetAccessToken.UseVisualStyleBackColor = true;
            this.btn_GetAccessToken.Click += new System.EventHandler(this.btn_GetAccessToken_Click);
            // 
            // rtb_accessToken
            // 
            this.rtb_accessToken.Location = new System.Drawing.Point(24, 77);
            this.rtb_accessToken.Name = "rtb_accessToken";
            this.rtb_accessToken.ReadOnly = true;
            this.rtb_accessToken.Size = new System.Drawing.Size(969, 188);
            this.rtb_accessToken.TabIndex = 5;
            this.rtb_accessToken.Text = "";
            // 
            // btn_GetSourceAPI
            // 
            this.btn_GetSourceAPI.Location = new System.Drawing.Point(705, 3);
            this.btn_GetSourceAPI.Name = "btn_GetSourceAPI";
            this.btn_GetSourceAPI.Size = new System.Drawing.Size(124, 44);
            this.btn_GetSourceAPI.TabIndex = 4;
            this.btn_GetSourceAPI.Text = "访问受保护资源";
            this.btn_GetSourceAPI.UseVisualStyleBackColor = true;
            this.btn_GetSourceAPI.Click += new System.EventHandler(this.btn_GetSourceAPI_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "AccessToken";
            // 
            // rtb_Source
            // 
            this.rtb_Source.Location = new System.Drawing.Point(24, 335);
            this.rtb_Source.Name = "rtb_Source";
            this.rtb_Source.ReadOnly = true;
            this.rtb_Source.Size = new System.Drawing.Size(969, 188);
            this.rtb_Source.TabIndex = 6;
            this.rtb_Source.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "受保护资源返回结果";
            // 
            // rtb_IdentitySource
            // 
            this.rtb_IdentitySource.Location = new System.Drawing.Point(24, 572);
            this.rtb_IdentitySource.Name = "rtb_IdentitySource";
            this.rtb_IdentitySource.ReadOnly = true;
            this.rtb_IdentitySource.Size = new System.Drawing.Size(969, 188);
            this.rtb_IdentitySource.TabIndex = 7;
            this.rtb_IdentitySource.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 557);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "IdentityServer保护资源";
            // 
            // btn_GetIdentitySource
            // 
            this.btn_GetIdentitySource.Location = new System.Drawing.Point(835, 3);
            this.btn_GetIdentitySource.Name = "btn_GetIdentitySource";
            this.btn_GetIdentitySource.Size = new System.Drawing.Size(158, 44);
            this.btn_GetIdentitySource.TabIndex = 9;
            this.btn_GetIdentitySource.Text = "访问Identity受保护资源";
            this.btn_GetIdentitySource.UseVisualStyleBackColor = true;
            this.btn_GetIdentitySource.Click += new System.EventHandler(this.btn_GetIdentitySource_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 789);
            this.Controls.Add(this.btn_GetIdentitySource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtb_IdentitySource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtb_Source);
            this.Controls.Add(this.rtb_accessToken);
            this.Controls.Add(this.btn_GetSourceAPI);
            this.Controls.Add(this.btn_GetAccessToken);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Button btn_GetAccessToken;
        private System.Windows.Forms.RichTextBox rtb_accessToken;
        private System.Windows.Forms.Button btn_GetSourceAPI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtb_Source;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtb_IdentitySource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_GetIdentitySource;
    }
}

