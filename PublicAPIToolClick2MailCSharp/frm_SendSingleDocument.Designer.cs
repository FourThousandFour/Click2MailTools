namespace ConvertedClick2Mail
{
    partial class frm_SendSingleDocument
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
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.LinkLabel2 = new System.Windows.Forms.LinkLabel();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.ListBox2 = new System.Windows.Forms.ListBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBox2
            // 
            this.TextBox2.Enabled = false;
            this.TextBox2.Location = new System.Drawing.Point(481, 120);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TextBox2.Size = new System.Drawing.Size(308, 117);
            this.TextBox2.TabIndex = 12;
            // 
            // DataGridView1
            // 
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(12, 241);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DataGridView1.Size = new System.Drawing.Size(980, 349);
            this.DataGridView1.TabIndex = 11;
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(806, 6);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListBox1.Size = new System.Drawing.Size(186, 225);
            this.ListBox1.TabIndex = 10;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(288, 212);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(184, 23);
            this.Button2.TabIndex = 18;
            this.Button2.Text = "Cancel";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(288, 187);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(184, 23);
            this.Button1.TabIndex = 17;
            this.Button1.Text = "Close and Check Addresses";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // LinkLabel2
            // 
            this.LinkLabel2.AutoSize = true;
            this.LinkLabel2.Location = new System.Drawing.Point(359, 57);
            this.LinkLabel2.Name = "LinkLabel2";
            this.LinkLabel2.Size = new System.Drawing.Size(116, 13);
            this.LinkLabel2.TabIndex = 16;
            this.LinkLabel2.TabStop = true;
            this.LinkLabel2.Text = "Insert In Address Block";
            this.LinkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(481, 3);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TextBox1.Size = new System.Drawing.Size(308, 109);
            this.TextBox1.TabIndex = 15;
            // 
            // ListBox2
            // 
            this.ListBox2.FormattingEnabled = true;
            this.ListBox2.Location = new System.Drawing.Point(12, 12);
            this.ListBox2.Name = "ListBox2";
            this.ListBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListBox2.Size = new System.Drawing.Size(256, 225);
            this.ListBox2.TabIndex = 14;
            this.ListBox2.DoubleClick += new System.EventHandler(ListBox2_DoubleClick);

            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Location = new System.Drawing.Point(418, 171);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(57, 13);
            this.LinkLabel1.TabIndex = 13;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "PREVIEW";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // frm_SendSingleDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 597);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.LinkLabel2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.ListBox2);
            this.Controls.Add(this.LinkLabel1);
            this.Name = "frm_SendSingleDocument";
            this.Text = "frm_SendSingleDocument";
            this.Load += new System.EventHandler(this.frm_sendSingleDocument_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.ListBox ListBox1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.LinkLabel LinkLabel2;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.ListBox ListBox2;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
    }
}