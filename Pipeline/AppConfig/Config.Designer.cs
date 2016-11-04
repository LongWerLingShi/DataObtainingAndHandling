namespace Pipeline.AppConfig
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.upload = new System.Windows.Forms.RadioButton();
            this.pipeline = new System.Windows.Forms.RadioButton();
            this.savepassword = new System.Windows.Forms.CheckBox();
            this.serveriptext = new System.Windows.Forms.TextBox();
            this.passwordtext = new System.Windows.Forms.TextBox();
            this.usernametext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.upload);
            this.groupBox1.Controls.Add(this.pipeline);
            this.groupBox1.Controls.Add(this.savepassword);
            this.groupBox1.Controls.Add(this.serveriptext);
            this.groupBox1.Controls.Add(this.passwordtext);
            this.groupBox1.Controls.Add(this.usernametext);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 238);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户登陆";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "登陆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // upload
            // 
            this.upload.AutoSize = true;
            this.upload.Location = new System.Drawing.Point(212, 141);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(73, 17);
            this.upload.TabIndex = 10;
            this.upload.TabStop = true;
            this.upload.Text = "上传数据";
            this.upload.UseVisualStyleBackColor = true;
            // 
            // pipeline
            // 
            this.pipeline.AutoSize = true;
            this.pipeline.Location = new System.Drawing.Point(65, 141);
            this.pipeline.Name = "pipeline";
            this.pipeline.Size = new System.Drawing.Size(73, 17);
            this.pipeline.TabIndex = 9;
            this.pipeline.TabStop = true;
            this.pipeline.Text = "处理数据";
            this.pipeline.UseVisualStyleBackColor = true;
            // 
            // savepassword
            // 
            this.savepassword.AutoSize = true;
            this.savepassword.Location = new System.Drawing.Point(65, 176);
            this.savepassword.Name = "savepassword";
            this.savepassword.Size = new System.Drawing.Size(74, 17);
            this.savepassword.TabIndex = 7;
            this.savepassword.Text = "保存密码";
            this.savepassword.UseVisualStyleBackColor = true;
            // 
            // serveriptext
            // 
            this.serveriptext.Location = new System.Drawing.Point(115, 100);
            this.serveriptext.Name = "serveriptext";
            this.serveriptext.Size = new System.Drawing.Size(183, 20);
            this.serveriptext.TabIndex = 6;
            // 
            // passwordtext
            // 
            this.passwordtext.Location = new System.Drawing.Point(115, 67);
            this.passwordtext.Name = "passwordtext";
            this.passwordtext.Size = new System.Drawing.Size(183, 20);
            this.passwordtext.TabIndex = 5;
            this.passwordtext.UseSystemPasswordChar = true;
            // 
            // usernametext
            // 
            this.usernametext.Location = new System.Drawing.Point(115, 35);
            this.usernametext.Name = "usernametext";
            this.usernametext.Size = new System.Drawing.Size(183, 20);
            this.usernametext.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "服务器地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "登录名";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 262);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton upload;
        private System.Windows.Forms.RadioButton pipeline;
        private System.Windows.Forms.CheckBox savepassword;
        private System.Windows.Forms.TextBox serveriptext;
        private System.Windows.Forms.TextBox passwordtext;
        private System.Windows.Forms.TextBox usernametext;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;

    }
}