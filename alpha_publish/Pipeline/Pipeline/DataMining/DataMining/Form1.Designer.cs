namespace DataMining
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Segment_Start_Button = new System.Windows.Forms.Button();
            this.Text_For_Segment = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Segment_Text = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Word Segment ：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Lucene.Net.Analysis.KeywordAnalyzer",
            "Lucene.Net.Analysis.PanGu.PanGuAnalyzer",
            "Lucene.Net.Analysis.SimpleAnalyzer",
            "Lucene.Net.Analysis.Standard.StandardAnalyzer",
            "Lucene.Net.Analysis.StopAnalyzer",
            "Lucene.Net.Analysis.WhitespaceAnalyzer",
            "Lucene.China.ChineseAnalyzer"});
            this.comboBox1.Location = new System.Drawing.Point(162, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(632, 23);
            this.comboBox1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Text ：";
            // 
            // Segment_Start_Button
            // 
            this.Segment_Start_Button.Location = new System.Drawing.Point(818, 52);
            this.Segment_Start_Button.Name = "Segment_Start_Button";
            this.Segment_Start_Button.Size = new System.Drawing.Size(95, 23);
            this.Segment_Start_Button.TabIndex = 12;
            this.Segment_Start_Button.Text = "Start ";
            this.Segment_Start_Button.UseVisualStyleBackColor = true;
            this.Segment_Start_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Text_For_Segment
            // 
            this.Text_For_Segment.Location = new System.Drawing.Point(162, 12);
            this.Text_For_Segment.Name = "Text_For_Segment";
            this.Text_For_Segment.Size = new System.Drawing.Size(751, 25);
            this.Text_For_Segment.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Segment_Text);
            this.groupBox2.Location = new System.Drawing.Point(33, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(930, 503);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Segment Result";
            // 
            // Segment_Text
            // 
            this.Segment_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Segment_Text.Location = new System.Drawing.Point(6, 24);
            this.Segment_Text.Multiline = true;
            this.Segment_Text.Name = "Segment_Text";
            this.Segment_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Segment_Text.Size = new System.Drawing.Size(918, 473);
            this.Segment_Text.TabIndex = 10;
            this.Segment_Text.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 646);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Segment_Start_Button);
            this.Controls.Add(this.Text_For_Segment);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "你好---";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Segment_Start_Button;
        private System.Windows.Forms.TextBox Text_For_Segment;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Segment_Text;
    }
}

