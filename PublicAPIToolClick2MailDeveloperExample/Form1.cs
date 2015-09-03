
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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Click2Mail.SetupStationaryFields.addressitem ai = new Click2Mail.SetupStationaryFields.addressitem();
            ai.Address1 = TB_A1.Text ;
            ai.Address2 = TB_A2.Text ;
            ai.nAddress3 = TB_A3.Text ;
            ai.city  = TB_CITY.Text;
            ai.state = TB_IL.Text ;
        ai.zip5 = TB_ZIP.Text;

        Click2Mail.SetupStationaryFields  c2mmail  = new Click2Mail.SetupStationaryFields(new System.IO.FileInfo(TB_Template.Text  ).DirectoryName);
        c2mmail._hideform = true ;
        c2mmail.WindowState = FormWindowState.Minimized;
        c2mmail.verifysingledocument(ai, TB_PDF.Text, new System.IO.FileInfo(TB_Template.Text  ).Name , true);
        c2mmail.ShowDialog();
        if(c2mmail.merror != "" )
        {
                 MessageBox.Show(c2mmail.merror);
        }
        
        //c2mmail.RichTextBox1.Text  RECEIVED XML From Click2Mail
        //c2mmail.mode LIVE OR TEST MODE
        //c2mmail.sentXML  The exact XML sent to Click2Mail (WARNING UNENCRYPTED PW Will be on this)

        c2mmail.Dispose();

        }

        private void button4_Click(object sender, EventArgs e)
        {
       
        Click2Mail.SetupStationaryFields  c2mmail  = new Click2Mail.SetupStationaryFields(new System.IO.FileInfo(TB_Template.Text  ).DirectoryName);
        c2mmail._hideform = true ;
        c2mmail.WindowState = FormWindowState.Normal;

              c2mmail.templatemulti( TB_PDF.Text, new System.IO.FileInfo(TB_Template.Text  ).Name , true);
        c2mmail.ShowDialog();
        if(c2mmail.merror != "" )
        {
                 MessageBox.Show(c2mmail.merror);
        }
            //c2mmail.RichTextBox1.Text  RECEIVED XML From Click2Mail
            //c2mmail.mode LIVE OR TEST MODE
            //c2mmail.sentXML  The exact XML sent to Click2Mail (WARNING UNENCRYPTED PW Will be on this)

        c2mmail.Dispose();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter =  "Click2Mail Template(*.c2m)|*.c2m";
            openFileDialog1.ShowDialog();
            this.TB_Template.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.FileName = "";
            openFileDialog2.Filter = "PDF File(*.pdf)|*.pdf";
            openFileDialog2.ShowDialog();
            this.TB_PDF.Text = openFileDialog2.FileName;
        }
    }
}
