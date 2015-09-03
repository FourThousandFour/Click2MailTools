
//# Public API Tool for Click2Mail
//
//This file is part ofPublic API Tool for Click2Mail, Developed by Vincent D. Senese.
//
//Public API Tool for Click2Mail is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//Public API Tool for Click2Mail is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Click2Mail Too.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic;
namespace ConvertedClick2Mail
{
    public partial class frm_Settings : Form
    {
        public frm_Settings()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet("StationaryDataset");
        private string _TemplatePath = "";
        private DataTable _dtt;


        private void savexml()
        {
            if (ds.Tables.Count == 0)
            {
                ds.Tables.Add(_dtt);
            }
            this._dtt.WriteXml("defaults.xml");
            Interaction.MsgBox("Defaults Saved");
        }
        private void frm_settings_Load(object sender, EventArgs e)
        {
            var mypath = "defaults.xml";

            if (System.IO.File.Exists(mypath))
            {
                ds.ReadXml(mypath);
                _dtt = ds.Tables[0];
                if (string.IsNullOrEmpty(tb_password.Text) & string.IsNullOrEmpty(tb_username.Text) & string.IsNullOrEmpty((string)cb_layout.SelectedItem) & string.IsNullOrEmpty((string)this.cb_productiontime.SelectedItem) & string.IsNullOrEmpty((string)this.cb_envelope.SelectedItem) & string.IsNullOrEmpty((string)this.cb_color.SelectedItem) & string.IsNullOrEmpty((string)this.cb_papertype.SelectedItem) & string.IsNullOrEmpty((string)this.cb_papertype.SelectedItem) & string.IsNullOrEmpty(this.tb_raName.Text) & string.IsNullOrEmpty((string)this.tb_raName.Text))
                {
                    this.tb_username.Text = (string)_dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];
                    this.tb_password.Text = (string)_dtt.Select("setting = true and fieldname = 'password'")[0]["misc"];
                    this.cb_documentclass.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
                    this.cb_layout.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
                    this.cb_productiontime.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
                    this.cb_envelope.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];
                    this.cb_color.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
                    this.cb_papertype.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
                    this.cb_printoption.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
                    this.cb_mailclass.SelectedItem = (string)_dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
                    this.tb_raName.Text = (string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
                    this.tb_raOrganization.Text = (string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
                    this.Tb_raAddress1.Text = (string)_dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
                    this.tb_raAddress2.Text = (string)_dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
                    this.tb_raCity.Text = (string)_dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
                    this.tb_raState.Text = (string)_dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
                    this.tb_PostalCode.Text = (string)_dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
                    this.Chkbox_NonStandardized.Checked = (bool)_dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"];
                    this.Chkbox_NonValidated.Checked = (bool)_dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"];
                    this.chk_OmitUSPSWarning.Checked = (bool)_dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"];
                    this.chk_TEST.Checked = (bool)_dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"];
                    if (_dtt.Select("setting = true and fieldname = 'templatePath'").Count() > 0)
                    {
                        this.tb_templatePath.Text = (string)_dtt.Select("setting = true and fieldname = 'templatePath'")[0]["misc"];
                    }
                }
                else
                {
                    this.tb_templatePath.Visible = false;
                    this.Label18.Visible = false;
                    if (_dtt.Select("setting = true and fieldname = 'templatePath'").Count() > 0)
                    {
                        this.tb_templatePath.Text = (string)_dtt.Select("setting = true and fieldname = 'templatePath'")[0]["misc"];
                    }
                }
            }
        }
        private void saveDefaults()
        {
            _dtt = new DataTable("FixedFields");


            DataColumn c = null;
            c = new DataColumn("fieldname", System.Type.GetType("System.String"));
            _dtt.Columns.Add(c);
            c.DefaultValue = 0;
            c = new DataColumn("x", System.Type.GetType("System.Int32"));
            c.DefaultValue = 0;
            _dtt.Columns.Add(c);
            c = new DataColumn("y", System.Type.GetType("System.Int32"));
            c.DefaultValue = 0;
            _dtt.Columns.Add(c);
            c = new DataColumn("width", System.Type.GetType("System.Int32"));
            c.DefaultValue = 0;
            _dtt.Columns.Add(c);

            c = new DataColumn("height", System.Type.GetType("System.Int32"));
            c.DefaultValue = 0;
            _dtt.Columns.Add(c);
            c = new DataColumn("misc", System.Type.GetType("System.String"));
            c.DefaultValue = "";
            _dtt.Columns.Add(c);
            c = new DataColumn("rowid", System.Type.GetType("System.Int32"));
            c.DefaultValue = 0;
            _dtt.Columns.Add(c);
            c = new DataColumn("Visible", System.Type.GetType("System.Boolean"));
            c.DefaultValue = false;
            _dtt.Columns.Add(c);
            c = new DataColumn("Setting", System.Type.GetType("System.Boolean"));
            c.DefaultValue = false;
            _dtt.Columns.Add(c);
            int i = 0;

            DataRow dr = _dtt.NewRow();

            dr["fieldname"] = "username";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;
            dr = _dtt.NewRow();
            dr["fieldname"] = "password";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();
            dr["fieldname"] = "appSignature";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poDocumentClass";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poLayout";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poProductionTime";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poEnvelope";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poColor";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "poPaperType";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;


            dr = _dtt.NewRow();

            dr["fieldname"] = "poPrintOption";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;


            dr = _dtt.NewRow();

            dr["fieldname"] = "poMailClass";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "raName";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;
            dr = _dtt.NewRow();

            dr["fieldname"] = "raOrganization";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;
            dr = _dtt.NewRow();

            dr["fieldname"] = "raAddress1";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "raAddress2";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "raCity";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "raState";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "raPostalCode";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;
            _dtt.Rows.Add(dr);


            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "omitNonStandard";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;

            _dtt.Rows.Add(dr);

            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "omitNonStandardWarning";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;

            _dtt.Rows.Add(dr);
            i += 1;

            dr = _dtt.NewRow();

            dr["fieldname"] = "omitNonValidated";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;

            _dtt.Rows.Add(dr);

            dr = _dtt.NewRow();

            dr["fieldname"] = "testMode";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;

            _dtt.Rows.Add(dr);


            dr = _dtt.NewRow();

            dr["fieldname"] = "templatePath";
            dr["rowid"] = i + 1;
            dr["Visible"] = false;
            dr["Setting"] = true;

            _dtt.Rows.Add(dr);

            _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"] = this.tb_username.Text;
            _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"] = this.tb_password.Text;
            _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"] = this.cb_documentclass.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"] = this.cb_layout.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"] = this.cb_productiontime.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"] = this.cb_envelope.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"] = this.cb_color.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"] = this.cb_papertype.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"] = this.cb_printoption.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"] = this.cb_mailclass.SelectedItem;
            _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"] = this.tb_raName.Text;
            _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"] = this.tb_raOrganization.Text;
            _dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"] = this.Tb_raAddress1.Text;
            _dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"] = this.tb_raAddress2.Text;
            _dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"] = this.tb_raCity.Text;
            _dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"] = this.tb_raState.Text;
            _dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"] = this.tb_PostalCode.Text;
            _dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"] = this.Chkbox_NonStandardized.Checked;
            _dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"] = this.Chkbox_NonValidated.Checked;
            _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"] = this.chk_OmitUSPSWarning.Checked;
            _dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"] = this.chk_TEST.Checked;

            string fileName = this.tb_templatePath.Text;
            fileName = fileName.TrimEnd(new[] { '/', '\\' });
            _dtt.Select("setting = true and fieldname = 'templatePath'")[0]["misc"] = fileName;
            savexml();


        }

        private void btn_save_Click_1(object sender, EventArgs e)
        {
            if (this.tb_templatePath.Text.Trim() != _TemplatePath.Trim())
            {
                MsgBoxResult y = Interaction.MsgBox("You have changed the Default Template Path and have not saved it.  Do you want to save this new Template Path?  A Template path is used mostly if you are storing on a network location.  Note this will overwrite all defaults to this, if this is not what you want click NO then close this and click CLEAR TEMPLATE to reload defaults", MsgBoxStyle.YesNo);
                if (y == MsgBoxResult.Yes)
                {
                    saveDefaults();
                }
            }
            Interaction.MsgBox("This template will UPDATE for this run, click SAVE TEMPLATE to actually save it for future uses when this closes.");
            this.Close();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            saveDefaults();
        }
    }
}

