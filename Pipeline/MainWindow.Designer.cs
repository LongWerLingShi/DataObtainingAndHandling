namespace Pipeline
{
    partial class MainWindow
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.filelistbox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.totalfilebox = new System.Windows.Forms.TextBox();
            this.Process_Sig = new System.Windows.Forms.Button();
            this.Raw_Data = new System.Windows.Forms.Button();
            this.Denoising_Data = new System.Windows.Forms.Button();
            this.Word_Segment = new System.Windows.Forms.Button();
            this.Final_Data = new System.Windows.Forms.Button();
            this.Process_All = new System.Windows.Forms.Button();
            this.Input_New = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Translate_Btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(262, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "学霸  Pipeline";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filelistbox
            // 
            this.filelistbox.FormattingEnabled = true;
            this.filelistbox.ItemHeight = 20;
            this.filelistbox.Location = new System.Drawing.Point(6, 25);
            this.filelistbox.Name = "filelistbox";
            this.filelistbox.Size = new System.Drawing.Size(498, 284);
            this.filelistbox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "文件总数:";
            // 
            // totalfilebox
            // 
            this.totalfilebox.Location = new System.Drawing.Point(111, 312);
            this.totalfilebox.Name = "totalfilebox";
            this.totalfilebox.Size = new System.Drawing.Size(125, 26);
            this.totalfilebox.TabIndex = 4;
            // 
            // Process_Sig
            // 
            this.Process_Sig.Location = new System.Drawing.Point(395, 315);
            this.Process_Sig.Name = "Process_Sig";
            this.Process_Sig.Size = new System.Drawing.Size(109, 27);
            this.Process_Sig.TabIndex = 5;
            this.Process_Sig.Text = "开始处理";
            this.Process_Sig.UseVisualStyleBackColor = true;
            this.Process_Sig.Click += new System.EventHandler(this.Process_Sig_Click);
            // 
            // Raw_Data
            // 
            this.Raw_Data.Location = new System.Drawing.Point(24, 101);
            this.Raw_Data.Name = "Raw_Data";
            this.Raw_Data.Size = new System.Drawing.Size(119, 28);
            this.Raw_Data.TabIndex = 6;
            this.Raw_Data.Text = "原始数据";
            this.Raw_Data.UseVisualStyleBackColor = true;
            this.Raw_Data.Click += new System.EventHandler(this.Raw_Data_Click);
            // 
            // Denoising_Data
            // 
            this.Denoising_Data.Location = new System.Drawing.Point(24, 148);
            this.Denoising_Data.Name = "Denoising_Data";
            this.Denoising_Data.Size = new System.Drawing.Size(119, 26);
            this.Denoising_Data.TabIndex = 7;
            this.Denoising_Data.Text = "去噪";
            this.Denoising_Data.UseVisualStyleBackColor = true;
            this.Denoising_Data.Click += new System.EventHandler(this.Denoising_Data_Click);
            // 
            // Word_Segment
            // 
            this.Word_Segment.Location = new System.Drawing.Point(24, 196);
            this.Word_Segment.Name = "Word_Segment";
            this.Word_Segment.Size = new System.Drawing.Size(119, 25);
            this.Word_Segment.TabIndex = 8;
            this.Word_Segment.Text = "分词";
            this.Word_Segment.UseVisualStyleBackColor = true;
            this.Word_Segment.Click += new System.EventHandler(this.Word_Segment_Click);
            // 
            // Final_Data
            // 
            this.Final_Data.Location = new System.Drawing.Point(24, 283);
            this.Final_Data.Name = "Final_Data";
            this.Final_Data.Size = new System.Drawing.Size(119, 28);
            this.Final_Data.TabIndex = 9;
            this.Final_Data.Text = "最终结果";
            this.Final_Data.UseVisualStyleBackColor = true;
            this.Final_Data.Click += new System.EventHandler(this.Final_Data_Click);
            // 
            // Process_All
            // 
            this.Process_All.Location = new System.Drawing.Point(256, 315);
            this.Process_All.Name = "Process_All";
            this.Process_All.Size = new System.Drawing.Size(119, 27);
            this.Process_All.TabIndex = 10;
            this.Process_All.Text = "全部开始";
            this.Process_All.UseVisualStyleBackColor = true;
            this.Process_All.Click += new System.EventHandler(this.Process_All_Click);
            // 
            // Input_New
            // 
            this.Input_New.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Input_New.Location = new System.Drawing.Point(24, 51);
            this.Input_New.Name = "Input_New";
            this.Input_New.Size = new System.Drawing.Size(119, 34);
            this.Input_New.TabIndex = 11;
            this.Input_New.Text = "添加文本";
            this.Input_New.UseVisualStyleBackColor = true;
            this.Input_New.Click += new System.EventHandler(this.Input_New_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filelistbox);
            this.groupBox1.Controls.Add(this.Process_All);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.totalfilebox);
            this.groupBox1.Controls.Add(this.Process_Sig);
            this.groupBox1.Location = new System.Drawing.Point(26, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 353);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择文件:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Translate_Btn);
            this.groupBox2.Controls.Add(this.Input_New);
            this.groupBox2.Controls.Add(this.Raw_Data);
            this.groupBox2.Controls.Add(this.Final_Data);
            this.groupBox2.Controls.Add(this.Denoising_Data);
            this.groupBox2.Controls.Add(this.Word_Segment);
            this.groupBox2.Location = new System.Drawing.Point(553, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 353);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "功能";
            // 
            // Translate_Btn
            // 
            this.Translate_Btn.Location = new System.Drawing.Point(24, 239);
            this.Translate_Btn.Name = "Translate_Btn";
            this.Translate_Btn.Size = new System.Drawing.Size(119, 25);
            this.Translate_Btn.TabIndex = 12;
            this.Translate_Btn.Text = "翻译";
            this.Translate_Btn.UseVisualStyleBackColor = true;
            this.Translate_Btn.Click += new System.EventHandler(this.Translate_Btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(577, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "退出主界面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 458);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pipeline";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox filelistbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox totalfilebox;
        private System.Windows.Forms.Button Process_Sig;
        private System.Windows.Forms.Button Raw_Data;
        private System.Windows.Forms.Button Denoising_Data;
        private System.Windows.Forms.Button Word_Segment;
        private System.Windows.Forms.Button Final_Data;
        private System.Windows.Forms.Button Process_All;
        private System.Windows.Forms.Button Input_New;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Translate_Btn;
    }
}

