
//# Click2MailTools

//This file is part of Click2Mail API Tool, Developed by Vincent D. Senese.

//Click2Mail API Tool is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//Click2Mail API Tool is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

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
using Microsoft.VisualBasic;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
namespace ConvertedClick2Mail
{
    public partial class frm_SendSingleDocument : Form


    {
        public frm_SendSingleDocument()
        {
            InitializeComponent();
        }

        	private System.Xml.XmlDocument XMLDOC;

		internal string _filename = string.Empty;



		private void readExcel()
		{

			try {

				if (!string.IsNullOrEmpty(this.OpenFileDialog1.FileName)) {
					string fileName = OpenFileDialog1.FileName;
					string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);
					System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + this.ListBox1.SelectedItem + "]", connectionString);
					DataSet ds = new DataSet();

					adapter.Fill(ds, "mytable");
					DataTable dt = ds.Tables["mytable"];

					this.DataGridView1.DataSource = dt;
					this.ListBox2.Items.Clear();
					foreach (DataColumn c in dt.Columns) {
						this.ListBox2.Items.Add(c.ColumnName);
					}
				}
			} catch (Exception ex) {
				Interaction.MsgBox(ex.Message);
			}
		}

		private List<string> ListSheetInExcel(string filePath)
		{
			OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
			String strExtendedProperties = String.Empty;
			sbConnection.DataSource = filePath;
			if (Path.GetExtension(filePath).Equals(".xls")) {
				//for 97-03 Excel file

				sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
				//HDR=ColumnHeader,IMEX=InterMixed
				strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";
			} else if (Path.GetExtension(filePath).Equals(".xlsx")) {
            				//for 2007 Excel file
				sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
				strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
			}
			sbConnection.Add("Extended Properties", strExtendedProperties);
			List<string> listSheet = new List<string>();
			using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString())) {
				try {
					conn.Open();
				} catch (Exception ex) {
					Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical);
                  }
				DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

				foreach (DataRow drSheet in dtSheet.Rows) {
					if (drSheet["TABLE_NAME"].ToString().Contains("$")) {
						//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
						listSheet.Add(drSheet["TABLE_NAME"].ToString());
					}
				}
			}
			return listSheet;
		}

		private void frm_sendSingleDocument_Load(object sender, EventArgs e)
		{

			if (string.IsNullOrEmpty(_filename)) {
				OpenFileDialog1.FileName = "";
				OpenFileDialog1.Filter = "Excel 97-2000(*.xls)|*.xls|Excel 2003-2012(*.xlsx)|*.xlsx";
				OpenFileDialog1.ShowDialog();
				_filename = OpenFileDialog1.FileName;
			}
			if (!string.IsNullOrEmpty(_filename)) {
				OpenFileDialog1.FileName = _filename;
				List<string> listsheet = null;
				listsheet = ListSheetInExcel(OpenFileDialog1.FileName);
				this.ListBox1.DataSource = listsheet;
				this.ListBox1.SelectedIndex = 0;
				readExcel();
			}

		}

		private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			readExcel();
		}


		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DataTable dt = (System.Data.DataTable) DataGridView1.DataSource;
			this.TextBox2.Text = this.TextBox1.Text;
			foreach (DataColumn c in dt.Columns) {
				try {
					if ((!object.ReferenceEquals(dt.Rows[1][c], DBNull.Value))) {
						this.TextBox2.Text = this.TextBox2.Text.Replace ("{" + c.ColumnName + "}", (string)dt.Rows[0][c]);
					} else {
						this.TextBox2.Text = Strings.Replace(this.TextBox2.Text, "{" + c.ColumnName + "}", "");
					}

				} catch {
				}
			}
			TextBox2.Text = Regex.Replace(TextBox2.Text, "^\\s+$[\\r\\n]*", "", RegexOptions.Multiline);
		}


		private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataGridView gr = new DataGridView();
			gr = (DataGridView) sender;
			switch (e.ColumnIndex) {
				case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
-1:
					break;
				//  MsgBox(gr.Columns(e.ColumnIndex).Name)

			}

		}

		private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this.ListBox2.SelectedItems.Count == 0) {
				Interaction.MsgBox("Please select a column from the list first");
				return;
			}
			this.TextBox1.Text = this.TextBox1.Text + "{" + this.ListBox2.SelectedItem + "}";

		}



		private void ListBox2_DoubleClick(object sender, EventArgs e)
		{
			if (this.ListBox2.SelectedItems.Count == 0) {
				Interaction.MsgBox("Please select a column from the list first");
				return;
			}
			this.TextBox1.Text = this.TextBox1.Text + "{" + this.ListBox2.SelectedItem + "}";
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			this.TextBox1.Text = "";
			this.Close();
		}







	
    }
}
