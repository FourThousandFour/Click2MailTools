namespace ConvertedClick2Mail
{
    partial class frm_Settings
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
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.tb_templatePath = new System.Windows.Forms.TextBox();
            this.chk_TEST = new System.Windows.Forms.CheckBox();
            this.chk_OmitUSPSWarning = new System.Windows.Forms.CheckBox();
            this.Chkbox_NonValidated = new System.Windows.Forms.CheckBox();
            this.Chkbox_NonStandardized = new System.Windows.Forms.CheckBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.cb_mailclass = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.tb_PostalCode = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.tb_raState = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.tb_raCity = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.tb_raAddress2 = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Tb_raAddress1 = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.tb_raOrganization = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.tb_raName = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.cb_documentclass = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.cb_printoption = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.cb_papertype = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.cb_envelope = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cb_productiontime = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cb_layout = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label19
            // 
            this.Label19.Location = new System.Drawing.Point(425, 303);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(412, 23);
            this.Label19.TabIndex = 88;
            this.Label19.Text = "Note, saving as defaults will populate these values automatically for new Templat" +
    "es";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label18
            // 
            this.Label18.Location = new System.Drawing.Point(481, 252);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(126, 23);
            this.Label18.TabIndex = 87;
            this.Label18.Text = "Default Template Path";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_templatePath
            // 
            this.tb_templatePath.Location = new System.Drawing.Point(620, 254);
            this.tb_templatePath.Name = "tb_templatePath";
            this.tb_templatePath.Size = new System.Drawing.Size(239, 20);
            this.tb_templatePath.TabIndex = 86;
            // 
            // chk_TEST
            // 
            this.chk_TEST.AutoSize = true;
            this.chk_TEST.Checked = true;
            this.chk_TEST.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_TEST.Location = new System.Drawing.Point(56, 253);
            this.chk_TEST.Name = "chk_TEST";
            this.chk_TEST.Size = new System.Drawing.Size(195, 17);
            this.chk_TEST.TabIndex = 85;
            this.chk_TEST.Text = "Use Test Mode with Staging server.";
            this.chk_TEST.UseVisualStyleBackColor = true;
            // 
            // chk_OmitUSPSWarning
            // 
            this.chk_OmitUSPSWarning.AutoSize = true;
            this.chk_OmitUSPSWarning.Location = new System.Drawing.Point(484, 194);
            this.chk_OmitUSPSWarning.Name = "chk_OmitUSPSWarning";
            this.chk_OmitUSPSWarning.Size = new System.Drawing.Size(180, 17);
            this.chk_OmitUSPSWarning.TabIndex = 84;
            this.chk_OmitUSPSWarning.Text = "Omit USPS Warning Addresses?";
            this.chk_OmitUSPSWarning.UseVisualStyleBackColor = true;
            // 
            // Chkbox_NonValidated
            // 
            this.Chkbox_NonValidated.AutoSize = true;
            this.Chkbox_NonValidated.Checked = true;
            this.Chkbox_NonValidated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chkbox_NonValidated.Location = new System.Drawing.Point(484, 226);
            this.Chkbox_NonValidated.Name = "Chkbox_NonValidated";
            this.Chkbox_NonValidated.Size = new System.Drawing.Size(309, 17);
            this.Chkbox_NonValidated.TabIndex = 83;
            this.Chkbox_NonValidated.Text = "Omit Non Validated Addresses(Addresses with missing parts)";
            this.Chkbox_NonValidated.UseVisualStyleBackColor = true;
            // 
            // Chkbox_NonStandardized
            // 
            this.Chkbox_NonStandardized.AutoSize = true;
            this.Chkbox_NonStandardized.Location = new System.Drawing.Point(484, 210);
            this.Chkbox_NonStandardized.Name = "Chkbox_NonStandardized";
            this.Chkbox_NonStandardized.Size = new System.Drawing.Size(219, 17);
            this.Chkbox_NonStandardized.TabIndex = 82;
            this.Chkbox_NonStandardized.Text = "Omit Non USPS Standardized Addresses";
            this.Chkbox_NonStandardized.UseVisualStyleBackColor = true;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(477, 280);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(316, 23);
            this.Button1.TabIndex = 81;
            this.Button1.Text = "Save as Program Defaults";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(12, 227);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(126, 23);
            this.Label3.TabIndex = 79;
            this.Label3.Text = "Document Class";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_mailclass
            // 
            this.cb_mailclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mailclass.FormattingEnabled = true;
            this.cb_mailclass.Items.AddRange(new object[] {
            "First Class",
            "Standard"});
            this.cb_mailclass.Location = new System.Drawing.Point(144, 227);
            this.cb_mailclass.Name = "cb_mailclass";
            this.cb_mailclass.Size = new System.Drawing.Size(239, 21);
            this.cb_mailclass.TabIndex = 80;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(56, 277);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(316, 23);
            this.btn_save.TabIndex = 78;
            this.btn_save.Text = "Update Template";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click_1);
            // 
            // Label17
            // 
            this.Label17.Location = new System.Drawing.Point(425, 165);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(189, 23);
            this.Label17.TabIndex = 77;
            this.Label17.Text = "Return Address Postal Code";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_PostalCode
            // 
            this.tb_PostalCode.Location = new System.Drawing.Point(620, 168);
            this.tb_PostalCode.Name = "tb_PostalCode";
            this.tb_PostalCode.Size = new System.Drawing.Size(83, 20);
            this.tb_PostalCode.TabIndex = 76;
            // 
            // Label16
            // 
            this.Label16.Location = new System.Drawing.Point(425, 142);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(189, 23);
            this.Label16.TabIndex = 75;
            this.Label16.Text = "Return Address State";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_raState
            // 
            this.tb_raState.Location = new System.Drawing.Point(620, 145);
            this.tb_raState.Name = "tb_raState";
            this.tb_raState.Size = new System.Drawing.Size(26, 20);
            this.tb_raState.TabIndex = 74;
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(425, 116);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(189, 23);
            this.Label15.TabIndex = 73;
            this.Label15.Text = "Return Address City";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_raCity
            // 
            this.tb_raCity.Location = new System.Drawing.Point(620, 122);
            this.tb_raCity.Name = "tb_raCity";
            this.tb_raCity.Size = new System.Drawing.Size(239, 20);
            this.tb_raCity.TabIndex = 72;
            // 
            // Label14
            // 
            this.Label14.Location = new System.Drawing.Point(425, 93);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(189, 23);
            this.Label14.TabIndex = 71;
            this.Label14.Text = "Return Address - Address 2";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_raAddress2
            // 
            this.tb_raAddress2.Location = new System.Drawing.Point(620, 96);
            this.tb_raAddress2.Name = "tb_raAddress2";
            this.tb_raAddress2.Size = new System.Drawing.Size(239, 20);
            this.tb_raAddress2.TabIndex = 70;
            // 
            // Label13
            // 
            this.Label13.Location = new System.Drawing.Point(425, 70);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(189, 23);
            this.Label13.TabIndex = 69;
            this.Label13.Text = "Return Address - Address 1";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Tb_raAddress1
            // 
            this.Tb_raAddress1.Location = new System.Drawing.Point(620, 73);
            this.Tb_raAddress1.Name = "Tb_raAddress1";
            this.Tb_raAddress1.Size = new System.Drawing.Size(239, 20);
            this.Tb_raAddress1.TabIndex = 68;
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(425, 44);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(189, 23);
            this.Label12.TabIndex = 67;
            this.Label12.Text = "Return Address Organization";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_raOrganization
            // 
            this.tb_raOrganization.Location = new System.Drawing.Point(620, 47);
            this.tb_raOrganization.Name = "tb_raOrganization";
            this.tb_raOrganization.Size = new System.Drawing.Size(239, 20);
            this.tb_raOrganization.TabIndex = 66;
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(425, 18);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(189, 23);
            this.Label11.TabIndex = 65;
            this.Label11.Text = "Return Address Name";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_raName
            // 
            this.tb_raName.Location = new System.Drawing.Point(620, 21);
            this.tb_raName.Name = "tb_raName";
            this.tb_raName.Size = new System.Drawing.Size(239, 20);
            this.tb_raName.TabIndex = 64;
            // 
            // Label10
            // 
            this.Label10.Location = new System.Drawing.Point(12, 61);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(126, 23);
            this.Label10.TabIndex = 50;
            this.Label10.Text = "Document Class";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_documentclass
            // 
            this.cb_documentclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_documentclass.FormattingEnabled = true;
            this.cb_documentclass.Items.AddRange(new object[] {
            "Letter 8.5 x 11",
            "Letter 8.5 x 14",
            "Postcard 3.5 x 5",
            "Postcard 4.25 x 6",
            "Postcard 6 x 11",
            "Flyer 8.5 x 11",
            "Brochure 11 x 8.5"});
            this.cb_documentclass.Location = new System.Drawing.Point(144, 61);
            this.cb_documentclass.Name = "cb_documentclass";
            this.cb_documentclass.Size = new System.Drawing.Size(239, 21);
            this.cb_documentclass.TabIndex = 63;
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(12, 204);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(126, 23);
            this.Label9.TabIndex = 62;
            this.Label9.Text = "Print Option";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_printoption
            // 
            this.cb_printoption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_printoption.FormattingEnabled = true;
            this.cb_printoption.Items.AddRange(new object[] {
            "Printing One side",
            "Printing both sides"});
            this.cb_printoption.Location = new System.Drawing.Point(144, 204);
            this.cb_printoption.Name = "cb_printoption";
            this.cb_printoption.Size = new System.Drawing.Size(239, 21);
            this.cb_printoption.TabIndex = 61;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(12, 181);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(126, 23);
            this.Label8.TabIndex = 60;
            this.Label8.Text = "Paper Type";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_papertype
            // 
            this.cb_papertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_papertype.FormattingEnabled = true;
            this.cb_papertype.Items.AddRange(new object[] {
            "White 24#",
            "Off-White 28#",
            "Canary 24#",
            "High Quality Paper",
            "White Matte",
            "White Matte with Gloss UV Finish - MS"});
            this.cb_papertype.Location = new System.Drawing.Point(144, 181);
            this.cb_papertype.Name = "cb_papertype";
            this.cb_papertype.Size = new System.Drawing.Size(239, 21);
            this.cb_papertype.TabIndex = 59;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(12, 158);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(126, 23);
            this.Label7.TabIndex = 58;
            this.Label7.Text = "Color";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_color
            // 
            this.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_color.FormattingEnabled = true;
            this.cb_color.Items.AddRange(new object[] {
            "Black and White",
            "Full Color"});
            this.cb_color.Location = new System.Drawing.Point(144, 158);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(239, 21);
            this.cb_color.TabIndex = 57;
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(12, 135);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(126, 23);
            this.Label6.TabIndex = 56;
            this.Label6.Text = "Envelope";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_envelope
            // 
            this.cb_envelope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_envelope.FormattingEnabled = true;
            this.cb_envelope.Items.AddRange(new object[] {
            "#10 Double Window",
            "Flat Envelope",
            "#10 Open Window Envelope"});
            this.cb_envelope.Location = new System.Drawing.Point(144, 135);
            this.cb_envelope.Name = "cb_envelope";
            this.cb_envelope.Size = new System.Drawing.Size(239, 21);
            this.cb_envelope.TabIndex = 55;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(12, 111);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(126, 23);
            this.Label5.TabIndex = 54;
            this.Label5.Text = "Production Time";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_productiontime
            // 
            this.cb_productiontime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_productiontime.FormattingEnabled = true;
            this.cb_productiontime.Items.AddRange(new object[] {
            "Next Day",
            "Within 3 Days",
            "Within 7 Days"});
            this.cb_productiontime.Location = new System.Drawing.Point(144, 111);
            this.cb_productiontime.Name = "cb_productiontime";
            this.cb_productiontime.Size = new System.Drawing.Size(239, 21);
            this.cb_productiontime.TabIndex = 53;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(12, 88);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(126, 23);
            this.Label4.TabIndex = 52;
            this.Label4.Text = "Layout";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_layout
            // 
            this.cb_layout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_layout.FormattingEnabled = true;
            this.cb_layout.Items.AddRange(new object[] {
            "Address on Separate Page",
            "Address on First Page",
            "Picture and Address First Page",
            "Address Back Page",
            "Same as a Stamp",
            "Single Sided Postcard",
            "Double Sided Postcard",
            "Double Sided Vertical Front Postcard",
            "Picture Postcard",
            "Vertical Split Postcard",
            "Self Mailer",
            "Self Mailer with Message Area on Address Panel"});
            this.cb_layout.Location = new System.Drawing.Point(144, 88);
            this.cb_layout.Name = "cb_layout";
            this.cb_layout.Size = new System.Drawing.Size(239, 21);
            this.cb_layout.TabIndex = 51;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(12, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(126, 23);
            this.Label2.TabIndex = 49;
            this.Label2.Text = "Click2Mail Password:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(144, 38);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(239, 20);
            this.tb_password.TabIndex = 48;
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(126, 23);
            this.Label1.TabIndex = 47;
            this.Label1.Text = "Click2Mail Username:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(144, 12);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(239, 20);
            this.tb_username.TabIndex = 46;
            // 
            // frm_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 338);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.tb_templatePath);
            this.Controls.Add(this.chk_TEST);
            this.Controls.Add(this.chk_OmitUSPSWarning);
            this.Controls.Add(this.Chkbox_NonValidated);
            this.Controls.Add(this.Chkbox_NonStandardized);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.cb_mailclass);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.tb_PostalCode);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.tb_raState);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.tb_raCity);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.tb_raAddress2);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Tb_raAddress1);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.tb_raOrganization);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.tb_raName);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.cb_documentclass);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.cb_printoption);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.cb_papertype);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.cb_color);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.cb_envelope);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cb_productiontime);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.cb_layout);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.tb_username);
            this.Name = "frm_Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.TextBox tb_templatePath;
        internal System.Windows.Forms.CheckBox chk_TEST;
        internal System.Windows.Forms.CheckBox chk_OmitUSPSWarning;
        internal System.Windows.Forms.CheckBox Chkbox_NonValidated;
        internal System.Windows.Forms.CheckBox Chkbox_NonStandardized;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cb_mailclass;
        internal System.Windows.Forms.Button btn_save;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.TextBox tb_PostalCode;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.TextBox tb_raState;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox tb_raCity;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox tb_raAddress2;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox Tb_raAddress1;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.TextBox tb_raOrganization;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox tb_raName;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.ComboBox cb_documentclass;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ComboBox cb_printoption;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox cb_papertype;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox cb_color;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox cb_envelope;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cb_productiontime;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cb_layout;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox tb_password;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tb_username;
    }
}