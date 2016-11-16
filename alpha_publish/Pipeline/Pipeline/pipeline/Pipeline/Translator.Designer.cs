namespace Pipeline
{
    partial class Translator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Translator));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Original_Text_Tab_Ctrl = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.original_Text = new System.Windows.Forms.TextBox();
            this.Translated_Text_Tab_Ctrl = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.translated_Text = new System.Windows.Forms.TextBox();
            this.Text_Compare_Tab_Ctrl = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.text_Compare_Translated_Text = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.text_Compare_Original_Text = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.Original_Text_Tab_Ctrl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Translated_Text_Tab_Ctrl.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.Text_Compare_Tab_Ctrl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Original_Text_Tab_Ctrl);
            this.tabControl1.Controls.Add(this.Translated_Text_Tab_Ctrl);
            this.tabControl1.Controls.Add(this.Text_Compare_Tab_Ctrl);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(553, 592);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Ori_Txt_Tab_Ctrl_DrawIt);
            // 
            // Original_Text_Tab_Ctrl
            // 
            //this.Original_Text_Tab_Ctrl.BackgroundImage = global::Pipeline.Properties.Resources._5375d1d689bd2;
            this.Original_Text_Tab_Ctrl.Controls.Add(this.groupBox3);
            this.Original_Text_Tab_Ctrl.Location = new System.Drawing.Point(4, 22);
            this.Original_Text_Tab_Ctrl.Margin = new System.Windows.Forms.Padding(2);
            this.Original_Text_Tab_Ctrl.Name = "Original_Text_Tab_Ctrl";
            this.Original_Text_Tab_Ctrl.Padding = new System.Windows.Forms.Padding(2);
            this.Original_Text_Tab_Ctrl.Size = new System.Drawing.Size(545, 566);
            this.Original_Text_Tab_Ctrl.TabIndex = 0;
            this.Original_Text_Tab_Ctrl.Text = "原文";
            this.Original_Text_Tab_Ctrl.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.original_Text);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(104, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(334, 537);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "原文";
            // 
            // original_Text
            // 
            this.original_Text.Location = new System.Drawing.Point(4, 19);
            this.original_Text.Margin = new System.Windows.Forms.Padding(2);
            this.original_Text.Multiline = true;
            this.original_Text.Name = "original_Text";
            this.original_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.original_Text.Size = new System.Drawing.Size(326, 510);
            this.original_Text.TabIndex = 0;
            // 
            // Translated_Text_Tab_Ctrl
            // 
            //this.Translated_Text_Tab_Ctrl.BackgroundImage = global::Pipeline.Properties.Resources._5375d1d689bd2;
            this.Translated_Text_Tab_Ctrl.Controls.Add(this.groupBox4);
            this.Translated_Text_Tab_Ctrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Translated_Text_Tab_Ctrl.Location = new System.Drawing.Point(4, 22);
            this.Translated_Text_Tab_Ctrl.Margin = new System.Windows.Forms.Padding(2);
            this.Translated_Text_Tab_Ctrl.Name = "Translated_Text_Tab_Ctrl";
            this.Translated_Text_Tab_Ctrl.Padding = new System.Windows.Forms.Padding(2);
            this.Translated_Text_Tab_Ctrl.Size = new System.Drawing.Size(545, 566);
            this.Translated_Text_Tab_Ctrl.TabIndex = 1;
            this.Translated_Text_Tab_Ctrl.Text = "译文";
            this.Translated_Text_Tab_Ctrl.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.translated_Text);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(105, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(334, 537);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "译文";
            // 
            // translated_Text
            // 
            this.translated_Text.Location = new System.Drawing.Point(4, 21);
            this.translated_Text.Margin = new System.Windows.Forms.Padding(2);
            this.translated_Text.Multiline = true;
            this.translated_Text.Name = "translated_Text";
            this.translated_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.translated_Text.Size = new System.Drawing.Size(326, 510);
            this.translated_Text.TabIndex = 0;
            // 
            // Text_Compare_Tab_Ctrl
            // 
            this.Text_Compare_Tab_Ctrl.BackColor = System.Drawing.Color.Transparent;
            //this.Text_Compare_Tab_Ctrl.BackgroundImage = global::Pipeline.Properties.Resources._5375d1d689bd2;
            this.Text_Compare_Tab_Ctrl.Controls.Add(this.button1);
            this.Text_Compare_Tab_Ctrl.Controls.Add(this.groupBox2);
            this.Text_Compare_Tab_Ctrl.Controls.Add(this.groupBox1);
            this.Text_Compare_Tab_Ctrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Text_Compare_Tab_Ctrl.Location = new System.Drawing.Point(4, 22);
            this.Text_Compare_Tab_Ctrl.Margin = new System.Windows.Forms.Padding(2);
            this.Text_Compare_Tab_Ctrl.Name = "Text_Compare_Tab_Ctrl";
            this.Text_Compare_Tab_Ctrl.Size = new System.Drawing.Size(545, 566);
            this.Text_Compare_Tab_Ctrl.TabIndex = 2;
            this.Text_Compare_Tab_Ctrl.Text = "双语对照";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimHei", 12F);
            this.button1.Location = new System.Drawing.Point(457, 535);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.text_Compare_Translated_Text);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(273, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(268, 530);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "译文";
            // 
            // text_Compare_Translated_Text
            // 
            this.text_Compare_Translated_Text.Location = new System.Drawing.Point(2, 19);
            this.text_Compare_Translated_Text.Margin = new System.Windows.Forms.Padding(2);
            this.text_Compare_Translated_Text.Multiline = true;
            this.text_Compare_Translated_Text.Name = "text_Compare_Translated_Text";
            this.text_Compare_Translated_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_Compare_Translated_Text.Size = new System.Drawing.Size(263, 512);
            this.text_Compare_Translated_Text.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.text_Compare_Original_Text);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(268, 535);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原文";
            // 
            // text_Compare_Original_Text
            // 
            this.text_Compare_Original_Text.Location = new System.Drawing.Point(2, 19);
            this.text_Compare_Original_Text.Margin = new System.Windows.Forms.Padding(2);
            this.text_Compare_Original_Text.Multiline = true;
            this.text_Compare_Original_Text.Name = "text_Compare_Original_Text";
            this.text_Compare_Original_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_Compare_Original_Text.Size = new System.Drawing.Size(263, 512);
            this.text_Compare_Original_Text.TabIndex = 0;
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 594);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Translator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Translator";
            this.Load += new System.EventHandler(this.Translator_Load);
            this.tabControl1.ResumeLayout(false);
            this.Original_Text_Tab_Ctrl.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Translated_Text_Tab_Ctrl.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Text_Compare_Tab_Ctrl.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Original_Text_Tab_Ctrl;
        private System.Windows.Forms.TextBox original_Text;
        private System.Windows.Forms.TabPage Translated_Text_Tab_Ctrl;
        private System.Windows.Forms.TabPage Text_Compare_Tab_Ctrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox text_Compare_Translated_Text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox text_Compare_Original_Text;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox translated_Text;
        private System.Windows.Forms.Button button1;

    }
}