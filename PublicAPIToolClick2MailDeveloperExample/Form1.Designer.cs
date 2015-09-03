namespace WindowsFormsApplication1
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
            this.TB_Template = new System.Windows.Forms.TextBox();
            this.TB_AN = new System.Windows.Forms.TextBox();
            this.TB_PDF = new System.Windows.Forms.TextBox();
            this.TB_Org = new System.Windows.Forms.TextBox();
            this.TB_A1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_A2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_A3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_CITY = new System.Windows.Forms.TextBox();
            this.TB_IL = new System.Windows.Forms.TextBox();
            this.TB_ZIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // TB_Template
            // 
            this.TB_Template.Enabled = false;
            this.TB_Template.Location = new System.Drawing.Point(172, 88);
            this.TB_Template.Name = "TB_Template";
            this.TB_Template.Size = new System.Drawing.Size(214, 20);
            this.TB_Template.TabIndex = 0;
            // 
            // TB_AN
            // 
            this.TB_AN.Location = new System.Drawing.Point(172, 140);
            this.TB_AN.Name = "TB_AN";
            this.TB_AN.Size = new System.Drawing.Size(214, 20);
            this.TB_AN.TabIndex = 1;
            // 
            // TB_PDF
            // 
            this.TB_PDF.Enabled = false;
            this.TB_PDF.Location = new System.Drawing.Point(172, 114);
            this.TB_PDF.Name = "TB_PDF";
            this.TB_PDF.Size = new System.Drawing.Size(214, 20);
            this.TB_PDF.TabIndex = 1;
            // 
            // TB_Org
            // 
            this.TB_Org.Location = new System.Drawing.Point(172, 166);
            this.TB_Org.Name = "TB_Org";
            this.TB_Org.Size = new System.Drawing.Size(214, 20);
            this.TB_Org.TabIndex = 2;
            // 
            // TB_A1
            // 
            this.TB_A1.Location = new System.Drawing.Point(172, 192);
            this.TB_A1.Name = "TB_A1";
            this.TB_A1.Size = new System.Drawing.Size(214, 20);
            this.TB_A1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Address Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Orginization";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(392, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Address 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Address 2";
            // 
            // TB_A2
            // 
            this.TB_A2.Location = new System.Drawing.Point(172, 218);
            this.TB_A2.Name = "TB_A2";
            this.TB_A2.Size = new System.Drawing.Size(214, 20);
            this.TB_A2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(392, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Address 3";
            // 
            // TB_A3
            // 
            this.TB_A3.Location = new System.Drawing.Point(172, 244);
            this.TB_A3.Name = "TB_A3";
            this.TB_A3.Size = new System.Drawing.Size(214, 20);
            this.TB_A3.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(392, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "City, ST ZIP";
            // 
            // TB_CITY
            // 
            this.TB_CITY.Location = new System.Drawing.Point(172, 270);
            this.TB_CITY.Name = "TB_CITY";
            this.TB_CITY.Size = new System.Drawing.Size(125, 20);
            this.TB_CITY.TabIndex = 11;
            // 
            // TB_IL
            // 
            this.TB_IL.Location = new System.Drawing.Point(303, 270);
            this.TB_IL.Name = "TB_IL";
            this.TB_IL.Size = new System.Drawing.Size(28, 20);
            this.TB_IL.TabIndex = 13;
            // 
            // TB_ZIP
            // 
            this.TB_ZIP.Location = new System.Drawing.Point(337, 270);
            this.TB_ZIP.Name = "TB_ZIP";
            this.TB_ZIP.Size = new System.Drawing.Size(49, 20);
            this.TB_ZIP.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(368, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Send To CLickToMail";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(395, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Select Template";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(395, 111);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Select PDF";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(163, 521);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(368, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Send To CLickToMail AUTO PARSE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(705, 63);
            this.label7.TabIndex = 19;
            this.label7.Text = "This is to show how you can launch and control the API.  Prior to using this, mak" +
    "e sure you create a template and save it.";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(43, 464);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(705, 54);
            this.label8.TabIndex = 20;
            this.label8.Text = "The below button will parse and send a PDF reading the addresses out of the mappe" +
    "d portion of the template the same as the Interface.";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(54, 345);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(705, 37);
            this.label9.TabIndex = 21;
            this.label9.Text = "The below button will Send to the address typed.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 588);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TB_ZIP);
            this.Controls.Add(this.TB_IL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TB_CITY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_A3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_A2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_A1);
            this.Controls.Add(this.TB_Org);
            this.Controls.Add(this.TB_PDF);
            this.Controls.Add(this.TB_AN);
            this.Controls.Add(this.TB_Template);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Template;
        private System.Windows.Forms.TextBox TB_AN;
        private System.Windows.Forms.TextBox TB_PDF;
        private System.Windows.Forms.TextBox TB_Org;
        private System.Windows.Forms.TextBox TB_A1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_A2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_A3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_CITY;
        private System.Windows.Forms.TextBox TB_IL;
        private System.Windows.Forms.TextBox TB_ZIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

