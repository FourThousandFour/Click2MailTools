
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


using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
namespace ConvertedClick2Mail
{
    public partial class SetupStationaryFields : Form
    {
        public SetupStationaryFields(string TemplatePath = "")
        {

            
                // This call is required by the designer.
                InitializeComponent();

                // Add any initialization after the InitializeComponent() call.
                if (!(TemplatePath == string.Empty))
                {
                    string fileName = TemplatePath;
                    fileName = fileName.TrimEnd(new[] { '/', '\\' });
                    _dtt.Select("setting = true and fieldname = 'templatePath'")[0]["misc"] = fileName;
                    _path = TemplatePath;
                }
                else
                {
                    dynamic mypath = "defaults.xml";
                    if (System.IO.File.Exists(mypath))
                    {
                        ds1.Clear();
                        ds1.ReadXml(mypath);
                        DataTable _dtt1 = null;
                        _dtt1 =  ds1.Tables[0];

                        if ( _dtt1.Select("setting = true and fieldname = 'templatePath'").Count() > 0)
                        {
                            _path = (string) _dtt1.Select("setting = true and fieldname = 'templatePath'")[0]["misc"];
                        }
                    }
                }
            

         }

        public bool keepopen
        {
            get
            {
                return _keepopen;
            }
            set
            {
                _keepopen = value;
            }

        }
        private bool _keepopen = false;
        private string _xlsfilename = string.Empty;
		private string _xtemplate = string.Empty;
		private DateTime _md;
		private int startover = 1;
		private DataTable _dtt;
		private int _mode = 1;
		public frm_Click2Mail.mode  mode;
		public string sentXML;
		private string oversized = "Flat Envelope";
		private GhostscriptVersionInfo gvi;
		private GhostscriptRasterizer rasterizer;
		private Rectangle Rect;
		private Rectangle RectValidate;
		private DataTable _DT = new DataTable();
		private bool Keeplast = false;
		private decimal zoom = 1;
		private DataSet ds = new DataSet("StationaryDataset");
		private DataSet ds1 = new DataSet("StationaryDataset");
		private decimal ww;
		private decimal hh;
		private decimal tt;
		private decimal ll;
		bool mouseb = false;
		private bool loadedbool = false;
		private  int _CurrentCount = 0;
		public System.Xml.XmlDocument XMLDOC;
		private string _validatetext = "";
		public string _sourcefilename = "";
		private string _CurrentTemplate = "";
		private string badaddress = "";
		private string _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
		private delegate void updatetext(string t);
		private delegate void updatert(XmlDocument t, string ba);
		private delegate void updatecomplete();
		private delegate void updatedatagrid(addresscollection aic);
		private string sDLLPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll32.dll");
		private string sDLLPath64 = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll64.dll");
		private Image _originalimage;
		private string _EncryptionKey = "kmjfds(#1231SDSA()#rt32geswfkjFJDSKFJDSFd";
		private addressitem _aiSingle;
		public bool _hideform = false;
		public bool iscomplete = false;
		public string merror = "";
		private System.Drawing.Image img;
		private int CurrentPage = 0;
		private System.Drawing.Bitmap bimg;
		private string _file = "";
		private  bool _startuploadwhendone = false;
		public DataTable dt {
			get { return _DT; }
		}
		public string CurrentTemplateFile {
			get {
				FileInfo f = new FileInfo(_CurrentTemplate);
				return f.Name;
			}
		}
		#region "Secure Password Storage"
		private string Encrypt(string clearText)
		{


			byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes encryptor = Aes.Create()) {
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_EncryptionKey, new byte[] {
					0x49,
					0x76,
					0x61,
					0x6e,
					0x20,
					0x4d,
					0x65,
					0x64,
					0x76,
					0x65,
					0x64,
					0x65,
					0x76
				});
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream()) {
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					clearText = Convert.ToBase64String(ms.ToArray());
				}
			}
			return clearText;
		}
		private string Decrypt(string cipherText)
		{

			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			using (Aes encryptor = Aes.Create()) {
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_EncryptionKey, new byte[] {
					0x49,
					0x76,
					0x61,
					0x6e,
					0x20,
					0x4d,
					0x65,
					0x64,
					0x76,
					0x65,
					0x64,
					0x65,
					0x76
				});
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream()) {
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					cipherText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return cipherText;
		}
		#endregion
		#region "Image Manipulation"
		private void TrackBar1_Scroll_1(object sender, EventArgs e)
        {

        }
		public Image ZoomImage(double ZoomFactor)
		{
            
			return new Bitmap(_originalimage, Convert.ToInt32( ((double) _originalimage.Width  * ZoomFactor)), Convert.ToInt32( ((double) _originalimage.Height * ZoomFactor)));
		}
		private System.Drawing.Image getImageFromFile(string filename, int page, int dpi)
		{

			int dpi_x = dpi;
			int dpi_y = dpi;
			int i = 1;

			Image img = rasterizer.GetPage(dpi_x, dpi_y, page + 1);
			_originalimage = img;
			// System.Drawing.Image obtained. Now it can be used at will.
			// Simply save it to storage as an example.
			return img;


		}
		#endregion
		#region "Returning Thread Calls"
		private void updatedatagridonMail(addresscollection aic)
		{
			this.DataGridView2.DataSource = aic;
			updategrid(DataGridView2);
		}
		private void updatecompleted()
		{
			this.Button1.Enabled = true;
			this.Button2.Enabled = true;
			this.Button3.Enabled = true;
			this.Button4.Enabled = true;
			this.Button5.Enabled = true;
			this.Button6.Enabled = true;
			this.Button7.Enabled = true;
			this.btn_upload.Enabled = true;
			this.ControlBox = true;
			if (_hideform == true) {
				addressitem ai = ((addresscollection)DataGridView2.DataSource).Item(0);
				if (ai.ommitted == true) {
					MessageBox.Show("Correct the address on the right");
					this.WindowState = FormWindowState.Normal;
					//Me.TopMost = True
					rasterizer.Close();

					rasterizer.Open(_sourcefilename, gvi, false);
					return;
				}
			}

			if (_startuploadwhendone == true) {
				_startuploadwhendone = false;
				startupload();
			} else {
				MessageBox.Show("Completed");
			}

		}
		private void updatelabel1text(string t)
		{
            this.Label2.Text = t;
		}
		private void updaterichtext(XmlDocument xml, string badaddresses)
		{
			this.RichTextBox1.Text = badaddresses;
			//& Beautify(xml)
			XMLDOC = xml;
		}
		#endregion
		#region "Drawing and Mapping Mouse Click"
		private void drawrectangles()
		{

			if (Convert.ToInt32( _dtt.Rows[0]["width"]) > 0) {
				Rect.Location = new System.Drawing.Point(Convert.ToInt32( _dtt.Rows[0]["x"]), Math.Abs(Convert.ToInt32( this.PictureBox1.Height) - Convert.ToInt32( _dtt.Rows[0]["y"] )- Convert.ToInt32( _dtt.Rows[0]["height"])));
				Rect.Size = new Size(Convert.ToInt32( _dtt.Rows[0]["width"]), Convert.ToInt32( _dtt.Rows[0]["height"]));
				mouseb = true;
				this.PictureBox1.Invalidate();
			}

			if (Convert.ToInt32( _dtt.Rows[1]["width"]) > 0) {
				RectValidate.Location = new System.Drawing.Point(Convert.ToInt32(_dtt.Rows[1]["x"]), Math.Abs(Convert.ToInt32( PictureBox1.Height) - Convert.ToInt32( _dtt.Rows[1]["y"]) - Convert.ToInt32( _dtt.Rows[1]["height"])));
				RectValidate.Size = new Size(Convert.ToInt32( _dtt.Rows[1]["width"]), Convert.ToInt32( _dtt.Rows[1]["height"]));
				mouseb = true;
				this.PictureBox1.Invalidate();
			}
		}
		private void Panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _md = DateAndTime.Now;
            if (TrackBar1.Value != 100)
            {
                Interaction.MsgBox("The Zoom must be set to defaul when selecting your template");
                this.TrackBar1.Value = 100;
                this.PictureBox1.Image = ZoomImage(this.TrackBar1.Value / 100);
            }
            RectValidate.Width = 0;
            RectValidate.Height = 0;
            Rect.Location = e.Location;
            Rect.Size = new Size(e.X - Rect.X, e.Y - Rect.Y);


            mouseb = true;

        }
		private void Panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rect.Size = new Size(e.X - Rect.X, e.Y - Rect.Y);


                this.PictureBox1.Invalidate();
            }
            this.Label1.Text = "x : " + Rect.X + " y : " + System.Math.Abs(this.PictureBox1.Height - Rect.Y) + " Width : " + Rect.Width + " Heigt : " + Rect.Height;

        }
		private void Panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DateTime x = new DateTime();

            if (_md <= new DateTime(1900,1,1))
            {
                return;
            }
            //x = Now.AddMilliseconds(-200)
            //If x < _md Then
            //        MsgBox("This ")
            //Return
            //End If
            int L = 0;
            int T = 0;
            int W = 0;
            int H = 0;
            L = Rect.X;
            T = Rect.Y;
            W = Rect.Width;
            H = Rect.Height;
            if (W < 0)
            {
                //L += W : W = -W
                return;


            }
            if (H < 0)
            {
                return;
            }
            if (this.Rect.Width > 20)
            {
                this.loadedbool = true;
            }
            if (_mode == 1)
            {
                if (this.loadedbool)
                {
                    if (Interaction.MsgBox("Is this the area you want to scan?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                    {
                        //If startover = 1 Then
                        // _dtt = Nothing
                        //setuptable()
                        //Me.DataGridView1.DataSource = _dtt
                        //  startover = 0
                        //End If
                        //_DT.Rows.Add(dr)
                        //Me.DataGridView1.DataSource = _DT
                        this.ExtractTextFromRegionOfPdf(this.OpenFileDialog1.FileName);
                    }
                    else
                    {
                        //            Rect.Width = -1
                        PictureBox1.Invalidate();
                    }
                }
            }

        }
        private void Panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int L = 0;
            int T = 0;
            int W = 0;
            int H = 0;
            L = Rect.X;
            T = Rect.Y;
            W = Rect.Width;
            H = Rect.Height;
            if (W < 0)
            {
                //L += W : W = -W
                return;


            }
            if (H < 0)
            {
                //        T += H : H = -H
                return;
            }

            int L1 = 0;
            int T1 = 0;
            int W1 = 0;
            int H1 = 0;
            L1 = RectValidate.X;
            T1 = RectValidate.Y;
            W1 = RectValidate.Width;
            H1 = RectValidate.Height;
            if (W1 < 0)
            {
                //L += W : W = -W
                return;


            }
            if (H1 < 0)
            {
                //        T += H : H = -H
                return;
            }
            if (mouseb)
            {
                e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(L, T, W, H));
                e.Graphics.DrawRectangle(Pens.Green, new Rectangle(L1, T1, W1, H1));
            }

        }
		#endregion
		#region "ParseAddresses"
        private bool parseaddresssingle_GeneratedPDF(ref addressitem ai, ref XmlNode BatchNode)
        {
            int startpage = 1;


            //Validate Text


            //It is a first page, but now lets see if there is a valid address
            //If address1 validates
            //


            if (BatchNode == null)
            {

                XMLDOC = new System.Xml.XmlDocument();
                System.Xml.XmlNode docNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", null);
                XMLDOC.AppendChild(docNode);
                BatchNode = XMLDOC.CreateElement("batch");
                System.Xml.XmlNode ns = null;
                ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
                ns.Value = "http://www.w3.org/2001/XMLSchema-instance";
                //BatchNode.Attributes.Append(ns);
                XMLDOC.AppendChild(BatchNode);

                System.Xml.XmlNode un = XMLDOC.CreateElement("username");
                BatchNode.AppendChild(un);
                un.InnerText = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];

                System.Xml.XmlNode pw = XMLDOC.CreateElement("password");
                pw.InnerText = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
                BatchNode.AppendChild(pw);
                System.Xml.XmlNode fn = XMLDOC.CreateElement("filename");
                fn.InnerText = _sourcefilename;
                BatchNode.AppendChild(fn);
                System.Xml.XmlNode as1 = XMLDOC.CreateElement("appSignature");
                BatchNode.AppendChild(as1);
                as1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"];
            }

            // ai = New addressitem





            System.Xml.XmlNode job = XMLDOC.CreateElement("job");
            BatchNode.AppendChild(job);

            System.Xml.XmlNode startingpage = XMLDOC.CreateElement("startingPage");
            job.AppendChild(startingpage);
            startingpage.InnerText = Convert.ToString( ai.startpage);
            System.Xml.XmlNode endpage = XMLDOC.CreateElement("endingPage");
            endpage.InnerText = Convert.ToString(ai.endpage);
            job.AppendChild(endpage);


            System.Xml.XmlNode printProductionOptions = XMLDOC.CreateElement("printProductionOptions");
            job.AppendChild(printProductionOptions);
            System.Xml.XmlNode docclass = XMLDOC.CreateElement("documentClass");
            docclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
            printProductionOptions.AppendChild(docclass);

            System.Xml.XmlNode layout = XMLDOC.CreateElement("layout");
            layout.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
            printProductionOptions.AppendChild(layout);

            System.Xml.XmlNode productiontime = XMLDOC.CreateElement("productionTime");
            productiontime.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
            printProductionOptions.AppendChild(productiontime);

            System.Xml.XmlNode envelope = XMLDOC.CreateElement("envelope");
            envelope.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];

            if ("Printing One side" == (string) _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"])
            {
                if ((ai.endpage - ai.startpage) + 1 > 5)
                {
                    envelope.InnerText = oversized;
                }

            }
            else
            {
                if ((ai.endpage - ai.startpage) + 1 > 10)
                {
                    envelope.InnerText = oversized;
                }

            }

            printProductionOptions.AppendChild(envelope);

            System.Xml.XmlNode color = XMLDOC.CreateElement("color");
            color.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
            printProductionOptions.AppendChild(color);

            System.Xml.XmlNode papertype = XMLDOC.CreateElement("paperType");
            papertype.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
            printProductionOptions.AppendChild(papertype);


            System.Xml.XmlNode printoption = XMLDOC.CreateElement("printOption");
            printoption.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
            printProductionOptions.AppendChild(printoption);

            System.Xml.XmlNode mailclass = XMLDOC.CreateElement("mailClass");
            mailclass.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
            printProductionOptions.AppendChild(mailclass);


            if (!string.IsNullOrEmpty((string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"]) | !string.IsNullOrEmpty((string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"]))
            {
                System.Xml.XmlNode returnAddress = XMLDOC.CreateElement("returnAddress");
                job.AppendChild(returnAddress);

                System.Xml.XmlNode raname = XMLDOC.CreateElement("name");
                raname.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
                returnAddress.AppendChild(raname);

                System.Xml.XmlNode raorg = XMLDOC.CreateElement("organization");
                raorg.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
                returnAddress.AppendChild(raorg);


                System.Xml.XmlNode raaddress1 = XMLDOC.CreateElement("address1");
                raaddress1.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
                returnAddress.AppendChild(raaddress1);

                System.Xml.XmlNode raaddress2 = XMLDOC.CreateElement("address2");
                raaddress2.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
                returnAddress.AppendChild(raaddress2);

                System.Xml.XmlNode racity = XMLDOC.CreateElement("city");
                racity.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
                returnAddress.AppendChild(racity);

                System.Xml.XmlNode rastate = XMLDOC.CreateElement("state");
                rastate.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
                returnAddress.AppendChild(rastate);

                System.Xml.XmlNode rapost = XMLDOC.CreateElement("postalCode");
                rapost.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
                returnAddress.AppendChild(rapost);
            }




            System.Xml.XmlNode recipients = XMLDOC.CreateElement("recipients");
            job.AppendChild(recipients);


            //VARIABLES TO HOLD INDIVIDUAL PARTS



            //SHOW RESULT
            //  MessageBox.Show("Name: " & AddressName)
            //MessageBox.Show("Address1: " & Address1)
            //MessageBox.Show("Address2: " & Address2)
            //MessageBox.Show("Address3: " & Address3)
            //MessageBox.Show("City: " & City)
            //MessageBox.Show("State: " & State)
            //MessageBox.Show("Zip: " & Zip)



            System.Xml.XmlNode address = XMLDOC.CreateElement("address");

            System.Xml.XmlNode xname = null;
            System.Xml.XmlNode xorginization = null;
            System.Xml.XmlNode xAddress1 = null;
            System.Xml.XmlNode xAddress2 = null;
            System.Xml.XmlNode xAddress3 = null;
            System.Xml.XmlNode xCity = null;
            System.Xml.XmlNode xState = null;
            System.Xml.XmlNode xZip = null;
            System.Xml.XmlNode xcountry = null;


            // Private _nName As String
            //        Private _norganization As String
            //Private _nAddress3 As String 'This is not used except for single item
            //        Private _Address1 As String
            //Private _Address2 As String
            //       Private _City As String
            //      Private _State As String
            //     Private _Zip5 As String
            //    Private _Zip4 As String

            xname = XMLDOC.CreateElement("name");
            xname.InnerText = ai.nname;
            xorginization = XMLDOC.CreateElement("organization");
            xorginization.InnerText = ai.norganization;
            xAddress1 = XMLDOC.CreateElement("address1");
            xAddress1.InnerText = Strings.Trim(ai.Address1);
            xAddress2 = XMLDOC.CreateElement("address2");
            xAddress2.InnerText = Strings.Trim(ai.Address2);
            xAddress3 = XMLDOC.CreateElement("address3");
            xAddress3.InnerText = Strings.Trim(ai.nAddress3);
            xCity = XMLDOC.CreateElement("city");
            xCity.InnerText = Strings.Trim(ai.city);
            xState = XMLDOC.CreateElement("state");
            xState.InnerText = Strings.Trim(ai.state);
            xZip = XMLDOC.CreateElement("postalCode");
            xZip.InnerText = Strings.Trim(ai.zip5 + "-" + ai.zip4);
            xcountry = XMLDOC.CreateElement("country");



            address.AppendChild(xname);
            address.AppendChild(xorginization);

            address.AppendChild(xAddress1);
            address.AppendChild(xAddress2);
            address.AppendChild(xAddress3);
            address.AppendChild(xCity);
            address.AppendChild(xState);
            address.AppendChild(xZip);
            address.AppendChild(xcountry);
            recipients.AppendChild(address);
            return true;
        }

		private bool parseaddress(string s, string validatetext, ref System.Xml.XmlNode batchnode, int startpage, ref System.Xml.XmlNode endpage, ref System.Xml.XmlNode envelope, ref System.Xml.XmlNode startingpage, ref addressitem ai)
		{

			s = Regex.Replace(s, "^\\s+$[\\r\\n]*", "", RegexOptions.Multiline);
            string[] parts = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

			//Validate Text
			//We're looking for blank
			if (Convert.ToInt32( _dtt.Rows[1]["width"] )> 0) {
				if (string.IsNullOrEmpty(_validatetext) & !(_validatetext == validatetext)) {
					return false;
					//It was not blank
				}
				validatetext = validatetext.Replace("\r", " ").Replace("\n", " ").Replace("  ", " ");
				string v = Utils.Left(validatetext.ToUpper(),_validatetext.Length + 1);

				if (!(v.Trim() == _validatetext.Trim().ToUpper()) & (batchnode != null)) {
					return false;
				}
			}


			//It is a first page, but now lets see if there is a valid address
			//If address1 validates
			//

			if (batchnode == null) {
				XMLDOC = new System.Xml.XmlDocument();
				System.Xml.XmlNode docNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", null);
				XMLDOC.AppendChild(docNode);
				batchnode = XMLDOC.CreateElement("batch");
				System.Xml.XmlNode ns = null;
				ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
				ns.Value = "http://www.w3.org/2001/XMLSchema-instance";
                //batchnode.Attributes.Append(ns);
				XMLDOC.AppendChild(batchnode);

				System.Xml.XmlNode un = XMLDOC.CreateElement("username");
				batchnode.AppendChild(un);
				un.InnerText = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];

				System.Xml.XmlNode pw = XMLDOC.CreateElement("password");
				pw.InnerText = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
				batchnode.AppendChild(pw);
				System.Xml.XmlNode fn = XMLDOC.CreateElement("filename");
				fn.InnerText = _sourcefilename;
				batchnode.AppendChild(fn);
				System.Xml.XmlNode as1 = XMLDOC.CreateElement("appSignature");
				batchnode.AppendChild(as1);
				as1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"];

			}



			if ((endpage != null)) {
				endpage.InnerText = Convert.ToString(startpage - 1);
				if ("Printing One side" == _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"]) {
					//28 - 23 is 5, but that is 6 pages
					if (Convert.ToInt32(endpage.InnerText) - Convert.ToInt32(startingpage.InnerText) + 1 > 5) {
						envelope.InnerText = oversized;
					}
				} else {
					if (Convert.ToInt32(endpage.InnerText) - Convert.ToInt32(startingpage.InnerText + 1) > 10) {
						envelope.InnerText = oversized;
					}
				}
			}
			if ((ai != null)) {
				ai.endpage = startpage - 1;
			}
			endpage = null;

			ai = new addressitem();
			ai.row = startpage;
			ai.startpage = startpage;
			ai.validatedStatus = "false";
			ai.uspsStatus = "Not Processed";


			//   If startpage = 8 Then
			//Console.Write(Trim(parts(parts.Length - 1)))
			//End If
			Regex rp = new Regex("((?:\\w|\\s)+),\\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\\d{5}(-\\d{4}|\\d{4}|$)))$");

			if (!rp.IsMatch(Strings.Trim(parts[parts.Length - 1]))) {
				if (Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"]) == true) {
					Console.WriteLine(startpage + " is not a valid address.");
					badaddress += "Document starting on page " + startpage + " is not a valid address, this was omitted." + "\r\n";
					ai.ommitted = true;
					return true;
				} else {
					ai.validatedStatus = "false";
				}
			} else {
				ai.validatedStatus = "true";
			}



			string AddressName = parts[0];
			string Addressorganization = string.Empty;
			string Address1 = string.Empty;
			string Address2 = string.Empty;
			string Address3 = string.Empty;
			string City = string.Empty;
			string State = string.Empty;
			string Zip = string.Empty;
			ai.nname = AddressName;


			try {
				string[] Words = Strings.Trim(parts[parts.Length - 1]).Split(' ');

				//GRAB ZIP (ALWAYS LAST IN THE ARRAY)
				Zip = Words[Words.Length - 1];
				//GRAB STATE ABBR (ALWAYS SECOND TO LAST IN ARRAY)
				State = Words[Words.Length - 2];
				//LOOP REMAINING ARRAY ELEMENT TO FORM CITY NAME 
				//(THIS WORKS FOR ANY NUMBER OF WORDS IN CITY NAME)
				for (int i = 0; i <= Words.Length - 3; i++) {
					City += Words[i] + " ";
					//ADD SPACE BACK IN BETWEEN WORDS
				}
				City = Strings.Replace(City, ",", "");
			} catch {
			}


            bool checkstandard = Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"]);
			bool checkstandardwarning = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"]);

			//We only want last two lines
			if (parts.Length >= 5) {
				Address1 = Strings.Trim(parts[parts.Length - 4]);
				Address2 = Strings.Trim(parts[parts.Length - 3]);
				Address3 = Strings.Trim(parts[parts.Length - 2]);

				ai.norganization = Address1;
				ai.Address1 = Address2;
				ai.Address2 = Address3;
			} else if (parts.Length == 4) {
				Address1 = Strings.Trim(parts[parts.Length - 3]);
				Address2 = Strings.Trim(parts[parts.Length - 2]);
				ai.Address1 = Address1;
				ai.Address2 = Address2;
			} else if (parts.Length == 3) {
				Address1 = Strings.Trim(parts[parts.Length - 2]);
				ai.Address1 = Address1;
			}


			ai.city = City;
			ai.state = State;
			ai.zip5 = Zip;

			standardizeaddress(ref ai);

			if (!(ai.uspsStatus == "1")) {
				//Omit for non standard
				if (ai.uspsStatus == "2" & checkstandardwarning == true) {
					Console.WriteLine(startpage + " is not a valid address.");
					badaddress += "Document starting on page " + startpage + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
					ai.ommitted = true;
					return true;
				} else if (ai.uspsStatus == "2") {
					Console.WriteLine(startpage + " is allowed through even though there is a warning.");
				} else if (checkstandard == true) {
					Console.WriteLine(startpage + " is not a valid address.");
					badaddress += "Document starting on page " + startpage + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
					ai.ommitted = true;
					return true;
				}

			} else {
				Address1 = ai.Address1;
				Address2 = ai.Address2;
				City = ai.city;
				State = ai.state;
				Zip = ai.zip5 + "-" + ai.zip4;
			}





			System.Xml.XmlNode job = XMLDOC.CreateElement("job");
			batchnode.AppendChild(job);

			startingpage = XMLDOC.CreateElement("startingPage");
			job.AppendChild(startingpage);
			startingpage.InnerText = Convert.ToString(startpage);
			endpage = XMLDOC.CreateElement("endingPage");
			job.AppendChild(endpage);
			System.Xml.XmlNode printProductionOptions = XMLDOC.CreateElement("printProductionOptions");
			job.AppendChild(printProductionOptions);
			System.Xml.XmlNode docclass = XMLDOC.CreateElement("documentClass");
			docclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
			printProductionOptions.AppendChild(docclass);

			System.Xml.XmlNode layout = XMLDOC.CreateElement("layout");
			layout.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
			printProductionOptions.AppendChild(layout);

			System.Xml.XmlNode productiontime = XMLDOC.CreateElement("productionTime");
			productiontime.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
			printProductionOptions.AppendChild(productiontime);

			envelope = XMLDOC.CreateElement("envelope");
			envelope.InnerText =(string)  _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];
			printProductionOptions.AppendChild(envelope);

			System.Xml.XmlNode color = XMLDOC.CreateElement("color");
			color.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
			printProductionOptions.AppendChild(color);

			System.Xml.XmlNode papertype = XMLDOC.CreateElement("paperType");
			papertype.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
			printProductionOptions.AppendChild(papertype);


			System.Xml.XmlNode printoption = XMLDOC.CreateElement("printOption");
			printoption.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
			printProductionOptions.AppendChild(printoption);

			System.Xml.XmlNode mailclass = XMLDOC.CreateElement("mailClass");
			mailclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
			printProductionOptions.AppendChild(mailclass);


			if (!string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"]) | !string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"])) {
				System.Xml.XmlNode returnAddress = XMLDOC.CreateElement("returnAddress");
				job.AppendChild(returnAddress);

				System.Xml.XmlNode raname = XMLDOC.CreateElement("name");
				raname.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
				returnAddress.AppendChild(raname);

				System.Xml.XmlNode raorg = XMLDOC.CreateElement("organization");
				raorg.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
				returnAddress.AppendChild(raorg);


				System.Xml.XmlNode raaddress1 = XMLDOC.CreateElement("address1");
				raaddress1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
				returnAddress.AppendChild(raaddress1);

				System.Xml.XmlNode raaddress2 = XMLDOC.CreateElement("address2");
				raaddress2.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
				returnAddress.AppendChild(raaddress2);

				System.Xml.XmlNode racity = XMLDOC.CreateElement("city");
				racity.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
				returnAddress.AppendChild(racity);

				System.Xml.XmlNode rastate = XMLDOC.CreateElement("state");
				rastate.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
				returnAddress.AppendChild(rastate);

				System.Xml.XmlNode rapost = XMLDOC.CreateElement("postalCode");
				rapost.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
				returnAddress.AppendChild(rapost);
			}




			System.Xml.XmlNode recipients = XMLDOC.CreateElement("recipients");
			job.AppendChild(recipients);


			//VARIABLES TO HOLD INDIVIDUAL PARTS



			//SHOW RESULT
			//  MessageBox.Show("Name: " & AddressName)
			//MessageBox.Show("Address1: " & Address1)
			//MessageBox.Show("Address2: " & Address2)
			//MessageBox.Show("Address3: " & Address3)
			//MessageBox.Show("City: " & City)
			//MessageBox.Show("State: " & State)
			//MessageBox.Show("Zip: " & Zip)



			System.Xml.XmlNode address = XMLDOC.CreateElement("address");

			System.Xml.XmlNode xname = null;
			System.Xml.XmlNode xorginization = null;
			System.Xml.XmlNode xAddress1 = null;
			System.Xml.XmlNode xAddress2 = null;
			System.Xml.XmlNode xAddress3 = null;
			System.Xml.XmlNode xCity = null;
			System.Xml.XmlNode xState = null;
			System.Xml.XmlNode xZip = null;
			System.Xml.XmlNode xcountry = null;




			xname = XMLDOC.CreateElement("name");
			xname.InnerText = ai.nname;
			xorginization = XMLDOC.CreateElement("organization");
			xorginization.InnerText = ai.norganization;
			xAddress1 = XMLDOC.CreateElement("address1");
			xAddress1.InnerText = Strings.Trim(ai.Address1);
			xAddress2 = XMLDOC.CreateElement("address2");
			xAddress2.InnerText = Strings.Trim(ai.Address2);
			xAddress3 = XMLDOC.CreateElement("address3");
			xAddress3.InnerText = Strings.Trim(ai.nAddress3);
			xCity = XMLDOC.CreateElement("city");
			xCity.InnerText = Strings.Trim(ai.city);
			xState = XMLDOC.CreateElement("state");
			xState.InnerText = Strings.Trim(ai.state);
			xZip = XMLDOC.CreateElement("postalCode");
			xZip.InnerText = Strings.Trim(ai.zip5 + "-" + ai.zip4);
			xcountry = XMLDOC.CreateElement("country");



			address.AppendChild(xname);
			address.AppendChild(xorginization);

			address.AppendChild(xAddress1);
			address.AppendChild(xAddress2);
			address.AppendChild(xAddress3);
			address.AppendChild(xCity);
			address.AppendChild(xState);
			address.AppendChild(xZip);
			address.AppendChild(xcountry);
			recipients.AppendChild(address);
			return true;
		}
		private bool parseaddresssingledoctomultiple(ref addressitem ai, ref XmlNode batchnode, string s, int startpage, int endingpage, int row, ref XmlNode recipients )
		{

            string[] parts = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries );
			ai = new addressitem();
			//It is a first page, but now lets see if there is a valid address
			//If address1 validates
			//
			ai.row = row;
			if (batchnode == null) {
				XMLDOC = new System.Xml.XmlDocument();

				System.Xml.XmlNode docNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", null);
				XMLDOC.AppendChild(docNode);
				batchnode = XMLDOC.CreateElement("batch");
				System.Xml.XmlNode ns = null;
				ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
				ns.Value = "http://www.w3.org/2001/XMLSchema-instance";
				//batchnode.Attributes.Append(ns);
				XMLDOC.AppendChild(batchnode);

				System.Xml.XmlNode un = XMLDOC.CreateElement("username");
				batchnode.AppendChild(un);
				un.InnerText = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];

				System.Xml.XmlNode pw = XMLDOC.CreateElement("password");
				pw.InnerText = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
				batchnode.AppendChild(pw);
				System.Xml.XmlNode fn = XMLDOC.CreateElement("filename");
				fn.InnerText = _sourcefilename;
				batchnode.AppendChild(fn);
				System.Xml.XmlNode as1 = XMLDOC.CreateElement("appSignature");
				batchnode.AppendChild(as1);
				as1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"];

                System.Xml.XmlNode job = XMLDOC.CreateElement("job");
                batchnode.AppendChild(job);

                System.Xml.XmlNode startingpage = XMLDOC.CreateElement("startingPage");
                job.AppendChild(startingpage);
                startingpage.InnerText = Convert.ToString(startpage);
                XmlNode endpage = XMLDOC.CreateElement("endingPage");
                endpage.InnerText = Convert.ToString(endingpage);
                job.AppendChild(endpage);

                System.Xml.XmlNode printProductionOptions = XMLDOC.CreateElement("printProductionOptions");
                job.AppendChild(printProductionOptions);
                System.Xml.XmlNode docclass = XMLDOC.CreateElement("documentClass");
                docclass.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
                printProductionOptions.AppendChild(docclass);

                System.Xml.XmlNode layout = XMLDOC.CreateElement("layout");
                layout.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
                printProductionOptions.AppendChild(layout);

                System.Xml.XmlNode productiontime = XMLDOC.CreateElement("productionTime");
                productiontime.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
                printProductionOptions.AppendChild(productiontime);

                System.Xml.XmlNode envelope = XMLDOC.CreateElement("envelope");
                envelope.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];
                printProductionOptions.AppendChild(envelope);



                if ("Printing One side" == _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"])
                {
                    if (endingpage > 5)
                    {
                        envelope.InnerText = oversized;
                    }

                }
                else
                {
                    if (endingpage > 10)
                    {
                        envelope.InnerText = oversized;
                    }

                }




                System.Xml.XmlNode color = XMLDOC.CreateElement("color");
                color.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
                printProductionOptions.AppendChild(color);

                System.Xml.XmlNode papertype = XMLDOC.CreateElement("paperType");
                papertype.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
                printProductionOptions.AppendChild(papertype);


                System.Xml.XmlNode printoption = XMLDOC.CreateElement("printOption");
                printoption.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
                printProductionOptions.AppendChild(printoption);

                System.Xml.XmlNode mailclass = XMLDOC.CreateElement("mailClass");
                mailclass.InnerText = (string)_dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
                printProductionOptions.AppendChild(mailclass);


                if (!string.IsNullOrEmpty((string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"]) | !string.IsNullOrEmpty((string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"]))
                {
                    System.Xml.XmlNode returnAddress = XMLDOC.CreateElement("returnAddress");
                    job.AppendChild(returnAddress);

                    System.Xml.XmlNode raname = XMLDOC.CreateElement("name");
                    raname.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
                    returnAddress.AppendChild(raname);

                    System.Xml.XmlNode raorg = XMLDOC.CreateElement("organization");
                    raorg.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
                    returnAddress.AppendChild(raorg);


                    System.Xml.XmlNode raaddress1 = XMLDOC.CreateElement("address1");
                    raaddress1.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
                    returnAddress.AppendChild(raaddress1);

                    System.Xml.XmlNode raaddress2 = XMLDOC.CreateElement("address2");
                    raaddress2.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
                    returnAddress.AppendChild(raaddress2);

                    System.Xml.XmlNode racity = XMLDOC.CreateElement("city");
                    racity.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
                    returnAddress.AppendChild(racity);

                    System.Xml.XmlNode rastate = XMLDOC.CreateElement("state");
                    rastate.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
                    returnAddress.AppendChild(rastate);

                    System.Xml.XmlNode rapost = XMLDOC.CreateElement("postalCode");
                    rapost.InnerText = (string)_dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
                    returnAddress.AppendChild(rapost);
                }




                recipients = XMLDOC.CreateElement("recipients");
                job.AppendChild(recipients);
                
			}




			ai = new addressitem();
			ai.startpage = startpage;
			ai.row = row;
			ai.validatedStatus = "false";
			ai.uspsStatus = "Not Processed";
			ai.endpage = endingpage;

			//   If startpage = 8 Then
			//Console.Write(Trim(parts(parts.Length - 1)))
			//End If
			Regex rp = new Regex("((?:\\w|\\s)+),\\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\\d{5}(-\\d{4}|\\d{4}|$)))$");

			if (!rp.IsMatch(Strings.Trim(parts[parts.Length - 1]))) {
				if (Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"]) == true) {
					Console.WriteLine(startpage + " is not a valid address.");
					badaddress += "Address on row " + row + " is not a valid address, this was omitted." + "\r\n";
					ai.ommitted = true;
					return true;
				} else {
					ai.validatedStatus = "false";
				}
			} else {
				ai.validatedStatus = "true";
			}



			string AddressName = parts[0];
			string Organization = string.Empty;
			string Address1 = string.Empty;
			string Address2 = string.Empty;
			string Address3 = string.Empty;
			string City = string.Empty;
			string State = string.Empty;
			string Zip = string.Empty;
			ai.nname = AddressName;
			try {
				string[] Words = Strings.Trim(parts[parts.Length - 1]).Split(' ');

				//GRAB ZIP (ALWAYS LAST IN THE ARRAY)
				Zip = Words[Words.Length - 1];
				//GRAB STATE ABBR (ALWAYS SECOND TO LAST IN ARRAY)
				State = Words[Words.Length - 2];
				//LOOP REMAINING ARRAY ELEMENT TO FORM CITY NAME 
				//(THIS WORKS FOR ANY NUMBER OF WORDS IN CITY NAME)
				for (int i = 0; i <= Words.Length - 3; i++) {
					City += Words[i] + " ";
					//ADD SPACE BACK IN BETWEEN WORDS
				}
				City = Strings.Replace(City, ",", "");
			} catch {
			}


			bool checkstandard = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"]);
			bool checkstandardwarning = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"]);

			if (parts.Length >= 5) {
				Address1 = Strings.Trim(parts[1]);
				Address2 = Strings.Trim(parts[2]);
				Address3 = Strings.Trim(parts[3]);

				if (string.IsNullOrEmpty(Address3) & string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address1;
				} else if (!string.IsNullOrEmpty(Address3) & string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address1;
					ai.Address2 = Address3;
				} else if (!string.IsNullOrEmpty(Address3) & !string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address2;
					ai.Address2 = Address3;
				} else {
					ai.Address1 = Address1;
					ai.Address2 = Address2;
				}

				ai.city = City;
				ai.state = State;
				ai.zip5 = Zip;
				standardizeaddress(ref ai);

				if (!(ai.uspsStatus == "1")) {
					//Omit for non standard
					if (ai.uspsStatus == "2" & checkstandardwarning == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
						ai.ommitted = true;
						return true;
					} else if (ai.uspsStatus == "2") {
						Console.WriteLine(startpage + " is allowed through even though there is a warning.");
					} else if (checkstandard == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
						ai.ommitted = true;
						return true;
					}
				} else {
					Organization = Address1;
					Address1 = ai.Address1;
					Address2 = ai.Address2;
					City = ai.city;
					State = ai.state;
					Zip = ai.zip5 + "-" + ai.zip4;
				}

			} else if (parts.Length == 4) {
				Address1 = Strings.Trim(parts[1]);
				Address2 = Strings.Trim(parts[2]);

				ai.Address1 = Address1;
				ai.Address2 = Address2;
				ai.city = City;
				ai.state = State;
				ai.zip5 = Zip;
				standardizeaddress(ref ai);
				if (!(ai.uspsStatus == "1")) {
					//Omit for non standard
					if (ai.uspsStatus == "2" & checkstandardwarning == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
						ai.ommitted = true;
						return true;
					} else if (ai.uspsStatus == "2") {
						Console.WriteLine(startpage + " is allowed through even though there is a warning.");
					} else if (checkstandard == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
						ai.ommitted = true;
						return true;
					}
				} else {
					Address2 = ai.Address2;
					Address1 = ai.Address1;
					City = ai.city;
					State = ai.state;
					Zip = ai.zip5 + "-" + ai.zip4;
				}
			} else if (parts.Length >= 3) {
				Address1 = Strings.Trim(parts[1]);
				ai.Address1 = Address1;
				ai.city = City;
				ai.state = State;
				ai.zip5 = Zip;


				standardizeaddress(ref ai);
				if (!(ai.uspsStatus == "1")) {
					//Omit for non standard
					if (ai.uspsStatus == "2" & checkstandardwarning == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
						ai.ommitted = true;
						return true;
					} else if (ai.uspsStatus == "2") {
						Console.WriteLine(startpage + " is allowed through even though there is a warning.");
					} else if (checkstandard == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Address on row " + row + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
						ai.ommitted = true;
						return true;
					}
				} else {
					Address1 = ai.Address1;
					Address2 = ai.Address2;
					City = ai.city;
					State = ai.state;
					Zip = ai.zip5 + "-" + ai.zip4;
				}
			}




			

			//VARIABLES TO HOLD INDIVIDUAL PARTS



			//SHOW RESULT
			//  MessageBox.Show("Name: " & AddressName)
			//MessageBox.Show("Address1: " & Address1)
			//MessageBox.Show("Address2: " & Address2)
			//MessageBox.Show("Address3: " & Address3)
			//MessageBox.Show("City: " & City)
			//MessageBox.Show("State: " & State)
			//MessageBox.Show("Zip: " & Zip)



			System.Xml.XmlNode address = XMLDOC.CreateElement("address");

			System.Xml.XmlNode xname = null;
			System.Xml.XmlNode xorginization = null;
			System.Xml.XmlNode xAddress1 = null;
			System.Xml.XmlNode xAddress2 = null;
			System.Xml.XmlNode xAddress3 = null;
			System.Xml.XmlNode xCity = null;
			System.Xml.XmlNode xState = null;
			System.Xml.XmlNode xZip = null;
			System.Xml.XmlNode xcountry = null;




			xname = XMLDOC.CreateElement("name");
			xname.InnerText = AddressName;
			xorginization = XMLDOC.CreateElement("organization");
			xorginization.InnerText = Organization;
			xAddress1 = XMLDOC.CreateElement("address1");
			xAddress1.InnerText = Strings.Trim(Address1);
			xAddress2 = XMLDOC.CreateElement("address2");
			xAddress2.InnerText = Strings.Trim(Address2);
			xAddress3 = XMLDOC.CreateElement("address3");
			xAddress3.InnerText = Strings.Trim(Address3);
			xCity = XMLDOC.CreateElement("city");
			xCity.InnerText = Strings.Trim(City);
			xState = XMLDOC.CreateElement("state");
			xState.InnerText = Strings.Trim(State);
			xZip = XMLDOC.CreateElement("postalCode");
			xZip.InnerText = Strings.Trim(Zip);
			xcountry = XMLDOC.CreateElement("country");



			address.AppendChild(xname);
			address.AppendChild(xorginization);

			address.AppendChild(xAddress1);
			address.AppendChild(xAddress2);
			address.AppendChild(xAddress3);
			address.AppendChild(xCity);
			address.AppendChild(xState);
			address.AppendChild(xZip);
			address.AppendChild(xcountry);
			recipients.AppendChild(address);
			return true;
		}
		private bool parseaddresssingle(ref addressitem ai, int totalpages)
		{
			int startpage = 1;
			XmlNode batchnode = null;

			//Validate Text


			//It is a first page, but now lets see if there is a valid address
			//If address1 validates
			//


			XMLDOC = new System.Xml.XmlDocument();
			System.Xml.XmlNode docNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", null);
			XMLDOC.AppendChild(docNode);
			batchnode = XMLDOC.CreateElement("batch");
			System.Xml.XmlNode ns = null;
			ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
			ns.Value = "http://www.w3.org/2001/XMLSchema-instance";
			//batchnode.Attributes.Append(ns);
			XMLDOC.AppendChild(batchnode);

			System.Xml.XmlNode un = XMLDOC.CreateElement("username");
			batchnode.AppendChild(un);
			un.InnerText = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];

			System.Xml.XmlNode pw = XMLDOC.CreateElement("password");
			pw.InnerText = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
			batchnode.AppendChild(pw);
			System.Xml.XmlNode fn = XMLDOC.CreateElement("filename");
			fn.InnerText = _sourcefilename;
			batchnode.AppendChild(fn);
			System.Xml.XmlNode as1 = XMLDOC.CreateElement("appSignature");
			batchnode.AppendChild(as1);
			as1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"];


			// ai = New addressitem
			ai.startpage = 1;
			ai.row = startpage;

			ai.validatedStatus = "false";
			ai.uspsStatus = "Not Processed";


			Regex rp = new Regex("((?:\\w|\\s)+),\\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\\d{5}(-\\d{4}|\\d{4}|$)))$");

			if (!rp.IsMatch(ai.city + ", " + ai.state + " " + ai.zip5)) {
				if (Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"]) == true) {
					Console.WriteLine(1 + " is not a valid address.");
					badaddress += "Document starting on page " + 1 + " is not a valid address, this was omitted." + "\r\n";
					ai.ommitted = true;
					return true;
				} else {
					ai.validatedStatus = "false";
				}
			} else {
				ai.validatedStatus = "true";
			}



			string AddressName = ai.nname;
			string Organization = string.Empty;
			string Address1 = ai.Address1;
			string Address2 = ai.Address2;
			string Address3 = ai.nAddress3;
			string City = ai.city;
			string State = ai.state;
			string Zip = ai.zip5;



			bool checkstandard = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"]);
			bool checkstandardwarning = Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"]);

			if (!string.IsNullOrEmpty(Address3)) {
				Address1 = ai.Address1;
				Address2 = ai.Address2;
				Address3 = ai.nAddress3;

				if (string.IsNullOrEmpty(Address3) & string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address1;
				} else if (!string.IsNullOrEmpty(Address3) & string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address1;
					ai.Address2 = Address3;
				} else if (!string.IsNullOrEmpty(Address3) & !string.IsNullOrEmpty(Address2)) {
					ai.Address1 = Address2;
					ai.Address2 = Address3;
				} else {
					ai.Address1 = Address1;
					ai.Address2 = Address2;
				}

				ai.city = City;
				ai.state = State;
				ai.zip5 = Zip;
				standardizeaddress(ref ai);

				if (!(ai.uspsStatus == "1")) {
					//Omit for non standard
					if (ai.uspsStatus == "2" & checkstandardwarning == true) {
						Console.WriteLine(1 + " is not a valid address.");
						badaddress += "Document starting on page " + 1 + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
						ai.ommitted = true;
						return true;
					} else if (ai.uspsStatus == "2") {
						Console.WriteLine(1 + " is allowed through even though there is a warning.");
					} else if (checkstandard == true) {
						Console.WriteLine(1 + " is not a valid address.");
						badaddress += "Document starting on page " + 1 + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
						ai.ommitted = true;
						return true;
					}
				} else {
					Organization = Address1;
					Address1 = ai.Address1;
					Address2 = ai.Address2;
					City = ai.city;
					State = ai.state;
					Zip = ai.zip5 + "-" + ai.zip4;
				}


			} else {
				standardizeaddress(ref ai);
				if (!(ai.uspsStatus == "1")) {
					//Omit for non standard
					if (ai.uspsStatus == "2" & checkstandardwarning == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Document starting on page " + startpage + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
						ai.ommitted = true;
						return true;
					} else if (ai.uspsStatus == "2") {
						Console.WriteLine(startpage + " is allowed through even though there is a warning.");
					} else if (checkstandard == true) {
						Console.WriteLine(startpage + " is not a valid address.");
						badaddress += "Document starting on page " + startpage + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";
						ai.ommitted = true;
						return true;
					}
				} else {
					Address2 = ai.Address2;
					Address1 = ai.Address1;
					City = ai.city;
					State = ai.state;
					Zip = ai.zip5 + "-" + ai.zip4;
				}

			}




			System.Xml.XmlNode job = XMLDOC.CreateElement("job");
			batchnode.AppendChild(job);

			System.Xml.XmlNode startingpage = XMLDOC.CreateElement("startingPage");
			job.AppendChild(startingpage);
			startingpage.InnerText =Convert.ToString(startpage);
			System.Xml.XmlNode endpage = XMLDOC.CreateElement("endingPage");
			endpage.InnerText = Convert.ToString(totalpages);
			job.AppendChild(endpage);


			System.Xml.XmlNode printProductionOptions = XMLDOC.CreateElement("printProductionOptions");
			job.AppendChild(printProductionOptions);
			System.Xml.XmlNode docclass = XMLDOC.CreateElement("documentClass");
			docclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
			printProductionOptions.AppendChild(docclass);

			System.Xml.XmlNode layout = XMLDOC.CreateElement("layout");
			layout.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
			printProductionOptions.AppendChild(layout);

			System.Xml.XmlNode productiontime = XMLDOC.CreateElement("productionTime");
			productiontime.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
			printProductionOptions.AppendChild(productiontime);

			System.Xml.XmlNode envelope = XMLDOC.CreateElement("envelope");
			envelope.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];

			if ("Printing One side" == _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"]) {
				if (totalpages > 5) {
					envelope.InnerText = oversized;
				}

			} else {
				if (totalpages > 10) {
					envelope.InnerText = oversized;
				}

			}

			printProductionOptions.AppendChild(envelope);

			System.Xml.XmlNode color = XMLDOC.CreateElement("color");
			color.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
			printProductionOptions.AppendChild(color);

			System.Xml.XmlNode papertype = XMLDOC.CreateElement("paperType");
			papertype.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
			printProductionOptions.AppendChild(papertype);


			System.Xml.XmlNode printoption = XMLDOC.CreateElement("printOption");
			printoption.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
			printProductionOptions.AppendChild(printoption);

			System.Xml.XmlNode mailclass = XMLDOC.CreateElement("mailClass");
			mailclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
			printProductionOptions.AppendChild(mailclass);


			if (!string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"]) | !string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"])) {
				System.Xml.XmlNode returnAddress = XMLDOC.CreateElement("returnAddress");
				job.AppendChild(returnAddress);

				System.Xml.XmlNode raname = XMLDOC.CreateElement("name");
				raname.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
				returnAddress.AppendChild(raname);

				System.Xml.XmlNode raorg = XMLDOC.CreateElement("organization");
				raorg.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
				returnAddress.AppendChild(raorg);


				System.Xml.XmlNode raaddress1 = XMLDOC.CreateElement("address1");
				raaddress1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
				returnAddress.AppendChild(raaddress1);

				System.Xml.XmlNode raaddress2 = XMLDOC.CreateElement("address2");
				raaddress2.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
				returnAddress.AppendChild(raaddress2);

				System.Xml.XmlNode racity = XMLDOC.CreateElement("city");
				racity.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
				returnAddress.AppendChild(racity);

				System.Xml.XmlNode rastate = XMLDOC.CreateElement("state");
				rastate.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
				returnAddress.AppendChild(rastate);

				System.Xml.XmlNode rapost = XMLDOC.CreateElement("postalCode");
				rapost.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
				returnAddress.AppendChild(rapost);
			}




			System.Xml.XmlNode recipients = XMLDOC.CreateElement("recipients");
			job.AppendChild(recipients);


			//VARIABLES TO HOLD INDIVIDUAL PARTS



			//SHOW RESULT
			//  MessageBox.Show("Name: " & AddressName)
			//MessageBox.Show("Address1: " & Address1)
			//MessageBox.Show("Address2: " & Address2)
			//MessageBox.Show("Address3: " & Address3)
			//MessageBox.Show("City: " & City)
			//MessageBox.Show("State: " & State)
			//MessageBox.Show("Zip: " & Zip)



			System.Xml.XmlNode address = XMLDOC.CreateElement("address");

			System.Xml.XmlNode xname = null;
			System.Xml.XmlNode xorginization = null;
			System.Xml.XmlNode xAddress1 = null;
			System.Xml.XmlNode xAddress2 = null;
			System.Xml.XmlNode xAddress3 = null;
			System.Xml.XmlNode xCity = null;
			System.Xml.XmlNode xState = null;
			System.Xml.XmlNode xZip = null;
			System.Xml.XmlNode xcountry = null;




			xname = XMLDOC.CreateElement("name");
			xname.InnerText = AddressName;
			xorginization = XMLDOC.CreateElement("organization");
			xorginization.InnerText = Organization;
			xAddress1 = XMLDOC.CreateElement("address1");
			xAddress1.InnerText = Strings.Trim(Address1);
			xAddress2 = XMLDOC.CreateElement("address2");
			xAddress2.InnerText = Strings.Trim(Address2);
			xAddress3 = XMLDOC.CreateElement("address3");
			xAddress3.InnerText = Strings.Trim(Address3);
			xCity = XMLDOC.CreateElement("city");
			xCity.InnerText = Strings.Trim(City);
			xState = XMLDOC.CreateElement("state");
			xState.InnerText = Strings.Trim(State);
			xZip = XMLDOC.CreateElement("postalCode");
			xZip.InnerText = Strings.Trim(Zip);
			xcountry = XMLDOC.CreateElement("country");



			address.AppendChild(xname);
			address.AppendChild(xorginization);

			address.AppendChild(xAddress1);
			address.AppendChild(xAddress2);
			address.AppendChild(xAddress3);
			address.AppendChild(xCity);
			address.AppendChild(xState);
			address.AppendChild(xZip);
			address.AppendChild(xcountry);
			recipients.AppendChild(address);
			return true;
		}
		private bool ParseAddressReprocess(ref addressitem ai, ref XmlNode Batchnode)
		{

			//It is a first page, but now lets see if there is a valid address
			//If address1 validates
			bool prioromit = ai.ommitted;

			ai.ommitted = false;
			if (XMLDOC == null) {
				XMLDOC = new System.Xml.XmlDocument();

				System.Xml.XmlNode docNode = XMLDOC.CreateXmlDeclaration("1.0", "UTF-8", null);
				XMLDOC.AppendChild(docNode);
				Batchnode = XMLDOC.CreateElement("batch");
				System.Xml.XmlNode ns = null;
				ns = XMLDOC.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
				ns.Value = "http://www.w3.org/2001/XMLSchema-instance";
				//batchnode.Attributes.Append(ns);
				XMLDOC.AppendChild(Batchnode);

				System.Xml.XmlNode un = XMLDOC.CreateElement("username");
				Batchnode.AppendChild(un);
				un.InnerText = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];

				System.Xml.XmlNode pw = XMLDOC.CreateElement("password");
				pw.InnerText = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
				Batchnode.AppendChild(pw);
				System.Xml.XmlNode fn = XMLDOC.CreateElement("filename");
				fn.InnerText = _sourcefilename;
				Batchnode.AppendChild(fn);
				System.Xml.XmlNode as1 = XMLDOC.CreateElement("appSignature");
				Batchnode.AppendChild(as1);
				as1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"];

			}






			//   If startpage = 8 Then
			//Console.Write(Trim(parts(parts.Length - 1)))
			//End If
			Regex rp = new Regex("((?:\\w|\\s)+),\\s(AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY)(|.(\\d{5}(-\\d{4}|\\d{4}|$)))$");

			if (!rp.IsMatch(ai.city + ", " + ai.state + " " + ai.zip5)) {
				if (Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"]) == true) {
					Console.WriteLine(ai.row + "row is not a valid address.");
					badaddress += "Address on row " + ai.row + " is not a valid address, this was omitted." + "\r\n";
					if (prioromit) {
						ai.ommitted = true;
						return true;
					}

				} else {
					ai.validatedStatus = "false";
				}
			} else {
				ai.validatedStatus = "true";
			}



			string AddressName = ai.nname;
			string Organization = string.Empty;
			string Address1 = ai.Address1;
			string Address2 = ai.Address2;
			string Address3 = ai.nAddress3;
			string City = ai.city;
			string State = ai.state;
			string Zip = ai.zip5;



			bool checkstandard = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"]);
			bool checkstandardwarning = Convert.ToBoolean(  _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"]);


			if (!(ai.uspsStatus == "1")) {
				//Omit for non standard
				if (ai.uspsStatus == "2" & checkstandardwarning == true) {
					Console.WriteLine(ai.row + " row is not a valid address.");
					badaddress += "Address on row " + ai.row + " IS Valid VIA THE USPS, But this was omitted due to warning." + "\r\n";
					if (prioromit) {
						ai.ommitted = true;
						return true;
					}

				} else if (ai.uspsStatus == "2") {
					Console.WriteLine(ai.row + " row is allowed through even though there is a warning.");
				} else if (checkstandard == true) {
					Console.WriteLine(ai.row + " row  is not a valid address.");
					badaddress += "Address on row " + ai.row + " is not a valid address VIA THE USPS, this was omitted." + "\r\n";

					if (prioromit) {
						ai.ommitted = true;
						return true;
					}
				}
			} else {
				Organization = ai.nAddress3;
				Address1 = ai.Address1;
				Address2 = ai.Address2;
				City = ai.city;
				State = ai.state;
				Zip = ai.zip5 + "-" + ai.zip4;
			}




			System.Xml.XmlNode job = XMLDOC.CreateElement("job");
			Batchnode.AppendChild(job);

			System.Xml.XmlNode startingpage = XMLDOC.CreateElement("startingPage");
			job.AppendChild(startingpage);
			startingpage.InnerText = Convert.ToString( ai.startpage);
			XmlNode endpage = XMLDOC.CreateElement("endingPage");
			endpage.InnerText = Convert.ToString(  ai.endpage);
			job.AppendChild(endpage);

			System.Xml.XmlNode printProductionOptions = XMLDOC.CreateElement("printProductionOptions");
			job.AppendChild(printProductionOptions);
			System.Xml.XmlNode docclass = XMLDOC.CreateElement("documentClass");
			docclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
			printProductionOptions.AppendChild(docclass);

			System.Xml.XmlNode layout = XMLDOC.CreateElement("layout");
			layout.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
			printProductionOptions.AppendChild(layout);

			System.Xml.XmlNode productiontime = XMLDOC.CreateElement("productionTime");
			productiontime.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
			printProductionOptions.AppendChild(productiontime);

			System.Xml.XmlNode envelope = XMLDOC.CreateElement("envelope");
			envelope.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];
			printProductionOptions.AppendChild(envelope);

			if ("Printing One side" == _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"]) {
				if (ai.endpage - ai.startpage + 1 > 5) {
					envelope.InnerText = oversized;
				}

			} else {
				if (ai.endpage - ai.startpage + 1 > 10) {
					envelope.InnerText = oversized;
				}

			}


			System.Xml.XmlNode color = XMLDOC.CreateElement("color");
			color.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
			printProductionOptions.AppendChild(color);

			System.Xml.XmlNode papertype = XMLDOC.CreateElement("paperType");
			papertype.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
			printProductionOptions.AppendChild(papertype);


			System.Xml.XmlNode printoption = XMLDOC.CreateElement("printOption");
			printoption.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
			printProductionOptions.AppendChild(printoption);

			System.Xml.XmlNode mailclass = XMLDOC.CreateElement("mailClass");
			mailclass.InnerText = (string) _dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
			printProductionOptions.AppendChild(mailclass);


			if (!string.IsNullOrEmpty((string)_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"]) | !string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"])) {
				System.Xml.XmlNode returnAddress = XMLDOC.CreateElement("returnAddress");
				job.AppendChild(returnAddress);

				System.Xml.XmlNode raname = XMLDOC.CreateElement("name");
				raname.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
				returnAddress.AppendChild(raname);

				System.Xml.XmlNode raorg = XMLDOC.CreateElement("organization");
				raorg.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
				returnAddress.AppendChild(raorg);


				System.Xml.XmlNode raaddress1 = XMLDOC.CreateElement("address1");
				raaddress1.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
				returnAddress.AppendChild(raaddress1);

				System.Xml.XmlNode raaddress2 = XMLDOC.CreateElement("address2");
				raaddress2.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
				returnAddress.AppendChild(raaddress2);

				System.Xml.XmlNode racity = XMLDOC.CreateElement("city");
				racity.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
				returnAddress.AppendChild(racity);

				System.Xml.XmlNode rastate = XMLDOC.CreateElement("state");
				rastate.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
				returnAddress.AppendChild(rastate);

				System.Xml.XmlNode rapost = XMLDOC.CreateElement("postalCode");
				rapost.InnerText = (string) _dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
				returnAddress.AppendChild(rapost);
			}




			System.Xml.XmlNode recipients = XMLDOC.CreateElement("recipients");
			job.AppendChild(recipients);


			//VARIABLES TO HOLD INDIVIDUAL PARTS



			//SHOW RESULT
			//  MessageBox.Show("Name: " & AddressName)
			//MessageBox.Show("Address1: " & Address1)
			//MessageBox.Show("Address2: " & Address2)
			//MessageBox.Show("Address3: " & Address3)
			//MessageBox.Show("City: " & City)
			//MessageBox.Show("State: " & State)
			//MessageBox.Show("Zip: " & Zip)



			System.Xml.XmlNode address = XMLDOC.CreateElement("address");

			System.Xml.XmlNode xname = null;
			System.Xml.XmlNode xorginization = null;
			System.Xml.XmlNode xAddress1 = null;
			System.Xml.XmlNode xAddress2 = null;
			System.Xml.XmlNode xAddress3 = null;
			System.Xml.XmlNode xCity = null;
			System.Xml.XmlNode xState = null;
			System.Xml.XmlNode xZip = null;
			System.Xml.XmlNode xcountry = null;




			xname = XMLDOC.CreateElement("name");
			xname.InnerText = AddressName;
			xorginization = XMLDOC.CreateElement("organization");
			xorginization.InnerText = Organization;
			xAddress1 = XMLDOC.CreateElement("address1");
			xAddress1.InnerText = Strings.Trim(Address1);
			xAddress2 = XMLDOC.CreateElement("address2");
			xAddress2.InnerText = Strings.Trim(Address2);
			xAddress3 = XMLDOC.CreateElement("address3");
			xAddress3.InnerText = Strings.Trim(Address3);
			xCity = XMLDOC.CreateElement("city");
			xCity.InnerText = Strings.Trim(City);
			xState = XMLDOC.CreateElement("state");
			xState.InnerText = Strings.Trim(State);
			xZip = XMLDOC.CreateElement("postalCode");
			xZip.InnerText = Strings.Trim(Zip);
			xcountry = XMLDOC.CreateElement("country");



			address.AppendChild(xname);
			address.AppendChild(xorginization);

			address.AppendChild(xAddress1);
			address.AppendChild(xAddress2);
			address.AppendChild(xAddress3);
			address.AppendChild(xCity);
			address.AppendChild(xState);
			address.AppendChild(xZip);
			address.AppendChild(xcountry);
			recipients.AppendChild(address);
			return true;
		}
		#endregion
		#region "Page Manipulation"
		private void dg_confirms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gr;
            gr = (DataGridView) sender;
               addressitem v1 = default(addressitem);

               if (gr.CurrentRow.DataBoundItem != null)
               {
                   v1 = (addressitem)gr.CurrentRow.DataBoundItem;


                   CurrentPage = v1.startpage - 1;
                   System.Drawing.Image img = getImageFromFile(_sourcefilename, CurrentPage, 72);
                   bimg = new Bitmap(img, PictureBox1.Width, PictureBox1.Height);
                   this.PictureBox1.Image = bimg;
                   setpage();
               }

                   
                        
        }

		private void NextPage()
		{
			if (CurrentPage < rasterizer.PageCount - 1) {
				CurrentPage += 1;
				System.Drawing.Image img = getImageFromFile(this.OpenFileDialog1.FileName, CurrentPage, 72);
				bimg = new Bitmap(img, PictureBox1.Width, PictureBox1.Height);
				this.PictureBox1.Image = bimg;
			}
			setpage();
		}
		private void setpage()
		{
			if (CurrentPage == rasterizer.PageCount - 1) {
				this.Button3.Enabled = false;
			} else {
				this.Button3.Enabled = true;
			}
			if (CurrentPage == 0) {
				this.Button4.Enabled = false;
			} else {
				this.Button4.Enabled = true;
			}
			this.lbl_pages.Text = "Page " + (CurrentPage + 1) + " of " + rasterizer.PageCount;
		}
		private void PriorPage()
		{
			if (CurrentPage > 0) {
				CurrentPage -= 1;
				try {
					System.Drawing.Image img = getImageFromFile(this.OpenFileDialog1.FileName, CurrentPage, 72);
					bimg = new Bitmap(img, PictureBox1.Width, PictureBox1.Height);
					this.PictureBox1.Image = bimg;
				} catch (Exception ex) {
				}
			}
			setpage();
		}
		private void Button3_Click(System.Object sender, System.EventArgs e)
		{
			NextPage();
		}
		private void Button4_Click(System.Object sender, System.EventArgs e)
		{
			PriorPage();
		}
		#endregion
		#region "PDF Load and Extraction of Text"
		public string ExtractTextFromRegionOfPdf(string sourceFileName)
		{

			FileStream x = new FileStream(sourceFileName, FileMode.Open);
			iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(x);
			//AWESOME!!

			System.util.RectangleJ rect1 = new System.util.RectangleJ(Rect.X, System.Math.Abs(this.PictureBox1.Height - Rect.Y) - Rect.Height, Rect.Width, Rect.Height);
			iTextSharp.text.pdf.parser.RegionTextRenderFilter rf = new iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect1);
			iTextSharp.text.pdf.parser.LocationTextExtractionStrategy mystrat = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
			iTextSharp.text.pdf.parser.RegionTextRenderFilter[] rtrf = new iTextSharp.text.pdf.parser.RegionTextRenderFilter[2];
			rtrf[0] = rf;
			//Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
			//Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
			iTextSharp.text.pdf.parser.FilteredTextRenderListener textExtractionStrategy = new iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf);

			//rtrf(0) = rf2
			//textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
			//MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
			// iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)
			x.Close();
			x.Dispose();
			reader.Close();

			if (_mode == 1) {

				if (this.loadedbool) {
					string s = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, CurrentPage + 1, textExtractionStrategy);

				    DialogResult y =  MessageBox.Show("This field is showing : " + s + "\r\n" + "Is this the correct variable on this page?","Confirm" , MessageBoxButtons.YesNo );

					if (y == DialogResult.Yes ) {

                                                
						_dtt.Rows[_CurrentCount]["x"] = Rect.X;
						_dtt.Rows[_CurrentCount]["y"] = System.Math.Abs(this.PictureBox1.Height - Rect.Y) - Rect.Height;
						_dtt.Rows[_CurrentCount]["width"] = Rect.Width;
						_dtt.Rows[_CurrentCount]["height"] = Rect.Height;

						if (_CurrentCount == 0) {
                            DialogResult xx = MessageBox.Show("There is an optional Parimeter where you can select something that only appears on the first page, do you want to add this.  It can be part of a string like Page 1 of XX?", "Confirm", MessageBoxButtons.YesNo);
                            if (xx == DialogResult.No)
                            {
								_CurrentCount = 2;
							}
						}

						if (_dtt.Rows[_CurrentCount]["FieldName"] == "FirstPageConstant") {
							_validatetext = Interaction.InputBox("Enter Charectors to match, if you enter \"1 of \" it will be true for anything after the of");
							_dtt.Rows[_CurrentCount + 1]["misc"] = _validatetext;
							_CurrentCount += 1;
						}
						_CurrentCount += 1;



						if (_CurrentCount == 3) {
							_CurrentCount = 0;
							this.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" + _dtt.Rows[0]["fieldname"];
							startover = 1;
							drawrectangles();

							MessageBox.Show("Make sure you save this if you want to use it in the future.");

						} else {
							this.Label2.Text = "Now Select : " + _dtt.Rows[_CurrentCount]["fieldname"];
						}
					}
				}
			} else {
				this.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" + _dtt.Rows[0]["fieldname"];
			}
			return "";
		}
		private void loadpdf(string file)
		{
			this.Label2.Visible = true;
			loadtemplate();

			try {

				this.DataGridView1.DataSource = _dtt;
				updategrid(DataGridView1);
				//pdfv = New PDFView.PDFViewer
				this.Panel1.AutoScroll = true;
				this.Panel1.AutoSize = false;

				//Me.PictureBox1.SizeMode = Windows.Forms.PictureBoxSizeMode.Zoom
				//Me.Label1.Text = "Page 1 of " & img.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page)

				this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;

				//pdfv.FileName1 = file
				if (file != _file) {
					//pdfv.Dispose()
					rasterizer.Close();
					_file = file;
					rasterizer.Open(file, gvi, false);

				}

				this.PictureBox1.Image = getImageFromFile(file, 0, 72);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
			//Dim x As New FileStream("C:\image\test.pdf", FileMode.Open)
			//Dim reader As New iTextSharp.text.pdf.PdfReader(x)
			//
			//ww = PictureBox1.Width = reader.GetPageSize(1).Width * 3
			//hh = PictureBox1.Height = reader.GetPageSize(1).Height * 3
			//tt = PictureBox1.Top = reader.GetPageSize(1).Top
			//ll = PictureBox1.Left = reader.GetPageSize(1).Left
			//Me.PictureBox1.Update()
			//reader.Close()
			//  Dim reader As New iTextSharp.text.pdf.PdfReader("C:\image\test.pdf")
			//reader.GetPageSize(1)
			//PictureBox1.Width = reader.GetPageSize(1).Width
			//PictureBox1.Height = reader.GetPageSize(1).Height
			//PictureBox1.Top = reader.GetPageSize(1).Top
			//PictureBox1.Left = reader.GetPageSize(1).Left
			setpage();
			this.Label2.Text = "OK, you are ready to start, use your mouse to highlight the entire field where this field is located : " + _dtt.Rows[0]["fieldname"];
			this.Invalidate();

		}
		#endregion
		#region "General Functions"
		private void Button5_Click(System.Object sender, System.EventArgs e)
		{
			startload();
		}
		private void startload()
		{

			try {

				CurrentPage = 0;
				_mode = 1;

				this.OpenFileDialog1.Multiselect = false;
				this.OpenFileDialog1.Filter = "PDF|";
				System.Windows.Forms.DialogResult y = default(System.Windows.Forms.DialogResult);
				OpenFileDialog1.FileName = "*.pdf";
				y = OpenFileDialog1.ShowDialog();

				if (y == System.Windows.Forms.DialogResult.OK) {
					loadpdf(this.OpenFileDialog1.FileName);

					btn_upload.Enabled = true;
					FileInfo fi = new FileInfo(this.OpenFileDialog1.FileName);

				}
				updatetest();
				loadtemplate();
			} catch (Exception ex) {
				MessageBox.Show("You must select a file");
				this.Close();
			}
		}
		private void Button2_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}
		public string Beautify(System.Xml.XmlDocument doc)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings {
				Indent = true,
				IndentChars = "  ",
				NewLineChars = "\r" + "\n",
				NewLineHandling = System.Xml.NewLineHandling.Replace
			};
			using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(sb, settings)) {
				doc.Save(writer);
			}
			return sb.ToString();
		}
		private void verifydocument(bool startuploadwhendone = false)
		{
			if (Convert.ToInt32( _dtt.Rows[0]["width"]) == 0) {
				MessageBox.Show("You have not Selected the Address Block yet.  Please read the text in black and click and drag your mouse over the correct areas.");
				return;
			}
			if (string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"])) {
				MessageBox.Show("You have not setup this Print Document Yet.");
				openform();
			}

			_startuploadwhendone = startuploadwhendone;

			this.Button1.Enabled = false;
			this.Button2.Enabled = false;
			this.Button3.Enabled = false;
			this.Button4.Enabled = false;
			this.Button5.Enabled = false;
			this.Button6.Enabled = false;
			this.Button7.Enabled = false;
			this.btn_upload.Enabled = false;
			this.ControlBox = false;
			if (_loadtype == loadtype.regular) {
				_sourcefilename = this.OpenFileDialog1.FileName;
			}
			badaddress = "";

			System.Threading.Thread x = new System.Threading.Thread(verify);
			x.IsBackground = false;
			x.Start();

		}

		public void verifysingledocument(addressitem ai, string filename, string template, bool startuploadwhendone = false)
		{
			_sourcefilename = filename;
			_CurrentTemplate = _path + "\\" + template;
			_aiSingle = ai;
			_startuploadwhendone = startuploadwhendone;
			CurrentPage = 1;
		}
		#endregion
		#region "Page and Layout"
		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
			updatefiles(TextBox1.Text);
		}
		private void updategrid(DataGridView dgv)
		{

			try {

				dgv.BackgroundColor = Color.LightGray;
				dgv.BorderStyle = BorderStyle.Fixed3D;

				// Set property values appropriate for read-only display and  
				// limited interactivity. 
				dgv.AllowUserToAddRows = false;
				dgv.AllowUserToDeleteRows = false;
				dgv.AllowUserToOrderColumns = true;
				dgv.ReadOnly = false;
				dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
				dgv.MultiSelect = false;
				dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
				dgv.AllowUserToResizeColumns = true;
				dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
				dgv.AllowUserToResizeRows = false;
				dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;

				// Set the selection background color for all the cells.
				dgv.DefaultCellStyle.SelectionBackColor = Color.White;
				dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

				// Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
				// value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
				dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

				// Set the background color for all rows and for alternating rows.  
				// The value for alternating rows overrides the value for all rows. 
				dgv.RowsDefaultCellStyle.BackColor = Color.LightCyan;
				dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Cyan;
				// Set the row and column header styles.
				dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
				dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
				dgv.RowHeadersDefaultCellStyle.BackColor = Color.Black;







				foreach (DataGridViewColumn datacolumn in dgv.Columns) {
					if (datacolumn.Name.Contains( "Visible")) {
						foreach (DataGridViewRow dr in dgv.Rows) {
							if (Convert.ToBoolean( dr.Cells["Visible"].Value) == false) {
								dr.Visible = false;


							}
						}
						datacolumn.Visible = false;

					} else {
						//   datacolumn.ReadOnly = True
					}
					if (datacolumn.Name.Contains( "Setting")) {
						datacolumn.Visible = false;
					}
					if (datacolumn.Name.Contains( "startpage")) {
						datacolumn.DisplayIndex = 0;
					}
					if (datacolumn.Name.Contains( "endpage")) {
						datacolumn.DisplayIndex = 0;
					}
					if (datacolumn.Name.Contains( "uspsStatus")) {
						datacolumn.DisplayIndex = 0;

					}
					if (datacolumn.Name.Contains( "validatedStatus")) {
						datacolumn.DisplayIndex = 0;

					}

					datacolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				}

				if (dgv.Columns.Contains("row") & !dgv.Columns.Contains("Recheck Address")) {
					DataGridViewButtonColumn c = new DataGridViewButtonColumn();
					c.Name = "Recheck Address";
					c.HeaderText = "Recheck Address";
					c.Text = "ReCheck Address";
					c.UseColumnTextForButtonValue = true;
					dgv.Columns.Add(c);

				}

				if (dgv.Columns.Contains("Recheck Address")) {
					dgv.Columns["Recheck Address"].DisplayIndex = 0;
				}

			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		private void updatetest()
		{
            
            this.lbl_Live.Text = "Processing Notes: " + (Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"]) ? "TEST MODE" : "LIVE MODE");
		}
		private void setuptable()
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
			dr["fieldname"] = "AddressBlock";
			dr["rowid"] = i + 1;
			dr["Visible"] = 1;
			_dtt.Rows.Add(dr);
			i += 1;



			dr = _dtt.NewRow();
			dr["fieldname"] = "FirstPageConstant";
			dr["rowid"] = i + 1;
			dr["Visible"] = 1;
			_dtt.Rows.Add(dr);
			i += 1;

			dr = _dtt.NewRow();
			dr["fieldname"] = "FirstPageConstantCompare";
			dr["rowid"] = i + 1;
			dr["Visible"] = 1;
			_dtt.Rows.Add(dr);
			i += 1;
			dr = _dtt.NewRow();
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
			dr["Misc"] = true;
			_dtt.Rows.Add(dr);

			i += 1;

			dr = _dtt.NewRow();

			dr["fieldname"] = "omitNonStandardWarning";
			dr["rowid"] = i + 1;
			dr["Visible"] = false;
			dr["Setting"] = true;
			dr["Misc"] = true;
			_dtt.Rows.Add(dr);



			i += 1;

			dr = _dtt.NewRow();
			dr["fieldname"] = "omitNonValidated";
			dr["rowid"] = i + 1;
			dr["Visible"] = false;
			dr["Setting"] = true;
			dr["Misc"] = true;

			_dtt.Rows.Add(dr);


			i += 1;

			dr = _dtt.NewRow();
			dr["fieldname"] = "testMode";
			dr["rowid"] = i + 1;
			dr["Visible"] = false;
			dr["Setting"] = true;
			dr["Misc"] = true;
			_dtt.Rows.Add(dr);

		}
		#endregion
		#region "Template Functions"
		private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty((string) this.lb_SavedTemplates.SelectedItem))
                {
                    if (!(Strings.InStr((string) this.lb_SavedTemplates.SelectedItem, "system_") <= 0))
                    {
                        Interaction.MsgBox("This is a sysem template you cannot delete this.");
                    }
                    else
                    {
                        MsgBoxResult y = Interaction.MsgBox("Are you sure you want to delete template: " + this.lb_SavedTemplates.SelectedItem, MsgBoxStyle.YesNo);
                        if (y == MsgBoxResult.Yes)
                        {
                            File.Delete(_path + "\\" + this.lb_SavedTemplates.SelectedItem + ".c2m");
                            Interaction.MsgBox("Deleted");
                            updatefiles();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex, MsgBoxStyle.Critical);
            }
        }

		private void lb_SavedTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((this.PictureBox1.Image != null))
            {



                if (string.IsNullOrEmpty(_CurrentTemplate) & Convert.ToInt32(_dtt.Rows[0]["x"]) == 0)
                {

                    if (this.lb_SavedTemplates.Items.Count > 0)
                    {
                        _CurrentTemplate = _path + "\\" + this.lb_SavedTemplates.SelectedItem + ".c2m";
                        loadtemplate();
                    }
                }
                else if (_CurrentTemplate != this.lb_SavedTemplates.SelectedItem)
                {
                    MsgBoxResult y = Interaction.MsgBox("You have selected a new template, do you want to Load this?", MsgBoxStyle.YesNo);
                    if (y == MsgBoxResult.Yes)
                    {
                        _CurrentTemplate = _path + "\\" + this.lb_SavedTemplates.SelectedItem + ".c2m";
                        loadtemplate();
                    }

                }
                updatetest();
            }
            if (_mode == 2)
            {
                MsgBoxResult y = Interaction.MsgBox("You are currently in Single Item to Multiple Recipients mode.  You MUST reload your data to reflect changes", MsgBoxStyle.YesNo);
                if (y == MsgBoxResult.Yes)
                {
                    singletomultiple();
                }
            }
        }
		private void reloadtemplate()
		{
			if (!string.IsNullOrEmpty(_CurrentTemplate)) {
				loadtemplate();
			}
		}
		private void Button7_Click(object sender, EventArgs e)
		{
			openform();
		}
		private void openform()
		{
	frm_Settings  frm = new frm_Settings();
            frm.tb_username.Text = (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"];
            frm.tb_password.Text = Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]);
            frm.cb_documentclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"];
            frm.cb_layout.SelectedItem = _dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"];
			frm.cb_productiontime.SelectedItem = _dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"];
			frm.cb_envelope.SelectedItem = _dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"];
			frm.cb_color.SelectedItem = _dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"];
			frm.cb_papertype.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"];
			frm.cb_printoption.SelectedItem = _dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"];
			frm.cb_mailclass.SelectedItem = _dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"];
			frm.tb_raName.Text = (string) _dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"];
            frm.tb_raOrganization.Text = (string)_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"];
            frm.Tb_raAddress1.Text = (string)_dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"];
            frm.tb_raAddress2.Text = (string)_dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"];
            frm.tb_raCity.Text = (string)_dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"];
            frm.tb_raState.Text = (string)_dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"];
            frm.tb_PostalCode.Text = (string)_dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"];
            frm.Chkbox_NonStandardized.Checked = Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"]);
			frm.Chkbox_NonValidated.Checked =Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"]);
			frm.chk_OmitUSPSWarning.Checked =Convert.ToBoolean(_dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"]);
			frm.chk_TEST.Checked = Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"]);
			frm.ShowDialog();

			_dtt.Select("setting = true and fieldname = 'username'")[0]["misc"] = frm.tb_username.Text;

			_dtt.Select("setting = true and fieldname = 'password'")[0]["misc"] = Encrypt(frm.tb_password.Text);
			_dtt.Select("setting = true and fieldname = 'appSignature'")[0]["misc"] = "SRC_AutoMailer VSenese";
			_dtt.Select("setting = true and fieldname = 'poDocumentClass'")[0]["misc"] = frm.cb_documentclass.SelectedItem;

			_dtt.Select("setting = true and fieldname = 'poLayout'")[0]["misc"] = frm.cb_layout.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poProductionTime'")[0]["misc"] = frm.cb_productiontime.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poEnvelope'")[0]["misc"] = frm.cb_envelope.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poColor'")[0]["misc"] = frm.cb_color.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poPaperType'")[0]["misc"] = frm.cb_papertype.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poPrintOption'")[0]["misc"] = frm.cb_printoption.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'poMailClass'")[0]["misc"] = frm.cb_mailclass.SelectedItem;
			_dtt.Select("setting = true and fieldname = 'raName'")[0]["misc"] = frm.tb_raName.Text;
			_dtt.Select("setting = true and fieldname = 'raOrganization'")[0]["misc"] = frm.tb_raOrganization.Text;
			_dtt.Select("setting = true and fieldname = 'raAddress1'")[0]["misc"] = frm.Tb_raAddress1.Text;
			_dtt.Select("setting = true and fieldname = 'raAddress2'")[0]["misc"] = frm.tb_raAddress2.Text;
			_dtt.Select("setting = true and fieldname = 'raCity'")[0]["misc"] = frm.tb_raCity.Text;
			_dtt.Select("setting = true and fieldname = 'raState'")[0]["misc"] = frm.tb_raState.Text;
			_dtt.Select("setting = true and fieldname = 'raPostalCode'")[0]["misc"] = frm.tb_PostalCode.Text;
			_dtt.Select("setting = true and fieldname = 'omitNonStandard'")[0]["misc"] = frm.Chkbox_NonStandardized.Checked;
			_dtt.Select("setting = true and fieldname = 'omitNonValidated'")[0]["misc"] = frm.Chkbox_NonValidated.Checked;
			_dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")[0]["misc"] = frm.chk_OmitUSPSWarning.Checked;
			_dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"] = frm.chk_TEST.Checked;

			var mypath = "defaults.xml";
			if (System.IO.File.Exists(mypath)) {
				DataTable _dtt1 = null;
				ds1.Clear();
				ds1.ReadXml(mypath);
				_dtt1 = ds1.Tables[0];
				if (_dtt1.Select("setting = true and fieldname = 'templatePath'").Count() > 0) {
					if (!string.IsNullOrEmpty((string) _dtt1.Select("setting = true and fieldname = 'templatePath'")[0]["misc"])) {
						_path = (string) _dtt1.Select("setting = true and fieldname = 'templatePath'")[0]["misc"];
						updatefiles();
					}
				}
			}
			updatetest();
			if (_mode == 2) {
                MsgBoxResult y = Microsoft.VisualBasic.Interaction.MsgBox("You have changed this template, you MUST re-load your mail list Do you want to do that now?", MsgBoxStyle.YesNo);
				if (y == MsgBoxResult.Yes) {
					singletomultiple();
				}
			}
		}
		private void savexml()
		{
			string x = "";
			//If System.IO.Directory.Exists(x) = False Then
			//System.IO.Directory.CreateDirectory(x)
			//End If
			if (ds.Tables.Count == 0) {
				ds.Tables.Add(_dtt);
			}
			string fn = "";
			if (!string.IsNullOrEmpty(_CurrentTemplate)) {
				fn = Strings.Replace(new FileInfo(_CurrentTemplate).Name, ".c2m", "");
			}

			string s =  Microsoft.VisualBasic.Interaction.InputBox("Enter Name of Template", "Template Name" , fn);

			if (s.Length > 3) {
				s = s + ".c2m";
			} else {
				MessageBox.Show("Not a proper name, Must be at least 3 charectors");
				return;
			}

			this._dtt.WriteXml(_path + "\\" + s);
			_CurrentTemplate = s;
		}
		private void loadtemplate()
		{
			string mypath = _CurrentTemplate;

			//  MsgBox(_CurrentTemplate)
			if (!string.IsNullOrEmpty(mypath) & System.IO.File.Exists(mypath)) {
				ds.Clear();
				ds.ReadXml(mypath);
				_dtt = ds.Tables[0];
				this.DataGridView1.DataSource = _dtt;
				updategrid(DataGridView1);
				drawrectangles();

				foreach (DataRow dr in _dtt.Rows) {
                    Console.WriteLine(dr["FieldName"]);

					if (Convert.ToString(dr["FieldName"]).Trim() == "FirstPageConstantCompare") {
						_validatetext = (string) dr["misc"];
					}
					this.Label2.Text = "OK, you have completed the template, if you wish to start over simply do it again and start by selecting the area with:" + _dtt.Rows[0]["fieldname"];
					startover = 1;

				}
				FileInfo t = new FileInfo(_CurrentTemplate);
				this.lbl_CurrentTemplate.Text = "Current Template: " + Strings.Replace(t.Name, ".c2m", "");
			} else {
				this.lbl_CurrentTemplate.Text = "";
				setuptable();
			}
		}
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			savexml();
			MessageBox.Show("Item has saved");
			loadtemplate();
		}
		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			loadtemplate();
		}
		private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_CurrentCount = 0;
			_CurrentTemplate = "";
			_dtt = null;
			loadtemplate();

			this.DataGridView1.DataSource = _dtt;
			updategrid(this.DataGridView1);
			mouseb = false;
			this.PictureBox1.Invalidate();
		}
		#endregion
		#region "Address Functions"
		private void revalidate()
		{
			XMLDOC = null;
			badaddress = "";
			XmlNode batchnode = null;
			foreach (addressitem ai in (addresscollection) DataGridView2.DataSource) {
                addressitem air = ai;
				ParseAddressReprocess(ref air, ref batchnode);
			}
			this.RichTextBox1.Invoke(new updatert(updaterichtext), new object[] {
				XMLDOC,
				badaddress
			});
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
			Invoke(new updatedatagrid(updatedatagridonMail), new object[] { DataGridView2.DataSource });
			Invoke(new updatecomplete(updatecompleted), new object[]{});
		}
		private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dynamic senderGrid = (DataGridView)sender;
            //       Dim checkstandard As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandard'")(0)("misc")
            //        Dim checkstandardwarning As Boolean = _dtt.Select("setting = true and fieldname = 'omitNonStandardWarning'")(0)("misc")
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                addressitem y = senderGrid.Rows[e.RowIndex].DataBoundItem;
                standardizeaddress(ref y, true);
                if (y.uspsStatus == "1")
                {
                    Interaction.MsgBox("This address successfully passed standard address check");
                }
                else if (y.uspsStatus == "2")
                {
                    Interaction.MsgBox("This address successfully passed standard address check, but still is not complete and needs more info");
                }
                else
                {
                    Interaction.MsgBox("This address is still bad.");
                }
                MsgBoxResult yy = Interaction.MsgBox("In order for any changes to addresses to take place, you need to revalidate this, do you want to do that now?", MsgBoxStyle.YesNo);
                if (yy == MsgBoxResult.Yes)
                {
                    System.Threading.Thread x = new System.Threading.Thread(revalidate);
                    x.IsBackground = false;
                    x.Start();
                }

            }
        }
		public void verify()
		{
			addresscollection aic = new addresscollection();
			addressitem ai = null;
			string sourceFileName = _sourcefilename;
			FileStream x = new FileStream(sourceFileName, FileMode.Open);
			iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(x);
			//AWESOME!!
			x.Close();
			x.Dispose();
			string s = "";
			string s1 = "";
			System.Xml.XmlNode ep = null;
			System.Xml.XmlNode batch = null;
			System.Xml.XmlNode startingpage = null;
			System.Xml.XmlNode envelope = null;
			int pages = reader.NumberOfPages;
            int i;
			for (i = 0; i <= reader.NumberOfPages - 1; i++) {
				this.Label2.Invoke(new updatetext(updatelabel1text), new object[] { "Processing Page " + Convert.ToString(i + 1)  + " of " + pages });
				DataRow dr = _dtt.Rows[0];
				System.util.RectangleJ rect1 = new System.util.RectangleJ(Convert.ToInt32( dr["x"]), Convert.ToInt32( dr["y"]), Convert.ToInt32( dr["width"]), Convert.ToInt32( dr["height"]));
				iTextSharp.text.pdf.parser.RegionTextRenderFilter rf = new iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect1);
				iTextSharp.text.pdf.parser.LocationTextExtractionStrategy mystrat = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
				iTextSharp.text.pdf.parser.RegionTextRenderFilter[] rtrf = new iTextSharp.text.pdf.parser.RegionTextRenderFilter[2];
				rtrf[0] = rf;
				//Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
				//Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
				iTextSharp.text.pdf.parser.FilteredTextRenderListener textExtractionStrategy = new iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf);


				DataRow dr1 = _dtt.Rows[1];
				System.util.RectangleJ rect2 = new System.util.RectangleJ(Convert.ToInt32( dr1["x"]), Convert.ToInt32( dr1["y"]), Convert.ToInt32( dr1["width"]), Convert.ToInt32( dr1["height"]));
				iTextSharp.text.pdf.parser.RegionTextRenderFilter rf1 = new iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2);
				iTextSharp.text.pdf.parser.LocationTextExtractionStrategy mystrat1 = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
				iTextSharp.text.pdf.parser.RegionTextRenderFilter[] rtrf1 = new iTextSharp.text.pdf.parser.RegionTextRenderFilter[2];
				rtrf1[0] = rf1;
				//Dim rect2 As New System.util.RectangleJ(0, 700, 800, 140)
				//Dim rf2 As New iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect2)
				iTextSharp.text.pdf.parser.FilteredTextRenderListener textExtractionStrategy1 = new iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat1, rtrf1);

				s = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy);

				if (!string.IsNullOrEmpty(s)) {
					s1 = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy1);






					if (parseaddress(s, s1, ref batch, i + 1, ref ep, ref envelope, ref startingpage, ref ai)) {
						//Des not catch a single first page, anytime this is true it's a new first page
						//    ep.InnerText = i + 1
						// Console.WriteLine(ai.Address1)
						aic.Add(ai);
					}

				}

				if (i == reader.NumberOfPages - 1 & (ep != null)) {
					ep.InnerText = Convert.ToString(i + 1);
				}

				if (i == reader.NumberOfPages - 1 & (ai != null)) {
					ai.endpage = Convert.ToInt32( i + 1);
				}
				// CurrentPage = CurrentPage + 1
			}
			reader.Close();
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC.OuterXml, badaddress})
			this.RichTextBox1.Invoke(new updatert(updaterichtext), new object[] {
				XMLDOC,
				badaddress
			});
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
			Invoke(new updatedatagrid(updatedatagridonMail), new object[] { aic });
			Invoke(new updatecomplete(updatecompleted), new object[]{});


			//rtrf(0) = rf2
			//textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
			//MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
			// iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)





		}
		private void verifySingleItem()
		{
			addresscollection aic = new addresscollection();
			string sourceFileName = _sourcefilename;
			FileStream x = new FileStream(sourceFileName, FileMode.Open);
			iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(x);

			//AWESOME!!
			//      x.Close()

			//x.Dispose()
			string s = "";
			string s1 = "";
			System.Xml.XmlNode ep = null;
			System.Xml.XmlNode batch = null;
			int pages = reader.NumberOfPages;
			reader.Close();
			x.Close();
			if (parseaddresssingle(ref _aiSingle, pages)) {
				//Des not catch a single first page, anytime this is true it's a new first page
				//    ep.InnerText = i + 1
				// Console.WriteLine(ai.Address1)

			}
			aic.Add(_aiSingle);


			reader.Close();
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {XMLDOC.OuterXml, badaddress})
			Invoke(new updatert(updaterichtext), new object[] {
				XMLDOC,
				badaddress
			});
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
			Invoke(new updatedatagrid(updatedatagridonMail), new object[] { aic });
			Invoke(new updatecomplete(updatecompleted), new object[]{});


			//rtrf(0) = rf2
			//textExtractionStrategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(mystrat, rtrf)
			//MsgBox(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy))
			// iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, textExtractionStrategy)





		}
		private void Button6_Click(System.Object sender, System.EventArgs e)
		{
			verifydocument();

		}
		public class addressitem
		{
			//Add Name
			//Add Address3


			private string _nName;
			private string _norganization;
				//This is not used except for single item
			private string _nAddress3;
			private string _Address1;
			private string _Address2;
			private string _City;
			private string _State;
			private string _Zip5;
			private string _Zip4;
			private string _ReturnText;
			private string _uspsStatus;
			private string _ValidatedStatus;
			private int _startpage;
			private int _endpage;
			private bool _ommitted;
			private int _row;
			public bool ommitted {

				get { return _ommitted; }
				set { _ommitted = value; }
			}
			public string norganization {

				get { return _norganization; }
				set { _norganization = value; }
			}
			public int row {

				get { return _row; }
				set { _row = value; }
			}
			public string nname {

				get { return _nName; }
				set { _nName = value; }
			}
			public string nAddress3 {

				get { return _nAddress3; }
				set { _nAddress3 = value; }
			}
			public string Address1 {

				get { return _Address1; }
				set { _Address1 = value; }
			}

			public string Address2 {

				get { return _Address2; }
				set { _Address2 = value; }
			}

			public string city {

				get { return _City; }
				set { _City = value; }
			}

			public string state {

				get { return _State; }
				set { _State = value; }
			}

			public string zip5 {

				get { return _Zip5; }
				set { _Zip5 = value; }
			}

			public string zip4 {

				get { return _Zip4; }
				set { _Zip4 = value; }
			}
			public string returntext {

				get { return _ReturnText; }
				set { _ReturnText = value; }
			}
			public string uspsStatus {

				get { return _uspsStatus; }
				set { _uspsStatus = value; }
			}
			public string validatedStatus {

				get { return _ValidatedStatus; }
				set { _ValidatedStatus = value; }
			}
			public int startpage {

				get { return _startpage; }
				set { _startpage = value; }
			}
			public int endpage {

				get { return _endpage; }
				set { _endpage = value; }
			}
		}

		public class addresscollection : CollectionBase
		{
			public int Add(addressitem value)
			{
				return List.Add(value);
			}
			//Add
			public int IndexOf(addressitem value)
			{
				return List.IndexOf(value);
			}
			//IndexOf

			public void Insert(int index, addressitem value)
			{
				List.Insert(index, value);
			}
			//Insert
			public void Remove(addressitem value)
			{
				List.Remove(value);
			}
			//Remove
			public addressitem Item(int index)
			{
				return (addressitem)List[index];
			}
			//Remove
			public addressitem Contains(int Startpage)
			{
				foreach (addressitem Col in this) {
					if (Startpage ==Col.startpage ) {
						return Col;
					}
				}
				return null;
			}
		}
		private void standardizeaddress(ref addressitem ai, bool bypassvalidate = false)
		{
			if (bypassvalidate == false) {
				if ( ai.validatedStatus == "false") {
					return;
				}
			}

			if (!string.IsNullOrEmpty(ai.Address1)) {
				ai.Address1 = ai.Address1.Replace( "#", " APT ").Replace( "  ", " ");
				ai.Address1 = ai.Address1.ToUpper().Replace( " APT APT ", " APT ");
			}
			if (!string.IsNullOrEmpty(ai.Address2)) {
				ai.Address2 = ai.Address2.Replace( "#", " APT ").Replace( "  ", " ");
				ai.Address2 = ai.Address2.ToUpper().Replace(" APT APT ", " APT ");
			}




			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;
			Uri address = null;
			Stream postStream = null;

			var _url = "http://production.shippingapis.com/ShippingAPITest.dll?API=Verify&XML=";
			XmlDocument xm = new XmlDocument();
			XmlNode xadr = xm.CreateElement("AddressValidateRequest");
			XmlAttribute attr = xm.CreateAttribute("USERID");
			attr.Value = "264NPWU04230";
			xadr.Attributes.Append(attr);



			XmlNode xad = xm.CreateElement("Address");
			XmlNode xad1 = xm.CreateElement("Address1");
			xad1.InnerText = ai.Address1;


			XmlNode xad2 = xm.CreateElement("Address2");
			xad2.InnerText = ai.Address2;




			XmlNode xcity = xm.CreateElement("City");
			xcity.InnerText = ai.city;
			XmlNode xstate = xm.CreateElement("State");
			xstate.InnerText = ai.state;
			XmlNode xzip = xm.CreateElement("Zip5");
			xzip.InnerText = ai.zip5;
			XmlNode xzip4 = xm.CreateElement("Zip4");

			xzip.InnerText = Strings.Left(ai.zip5, 5);
			xad.AppendChild(xad1);
			xad.AppendChild(xad2);
			xad.AppendChild(xcity);
			xad.AppendChild(xstate);
			xad.AppendChild(xzip);
			xad.AppendChild(xzip4);

			xadr.AppendChild(xad);
			xm.AppendChild(xadr);


			Console.WriteLine(_url + xm.OuterXml);
			address = new Uri(_url + xm.OuterXml);


			// Create the web request  
			request = (HttpWebRequest)WebRequest.Create(address);
			request.Method = "GET";
			request.ContentType = "text/xml";

			//data = New StringBuilder()
			//data.Append("test=1")
			//data.Append("&layout=" + WebUtility.UrlEncode("Address on Separate Page"))
			//data.Append("&productionTime=" + WebUtility.UrlEncode("Next Day"))
			//data.Append("&envelope=" + WebUtility.UrlEncode("#10 Double Window"))
			//data.Append("&color=" + WebUtility.UrlEncode("Black and White"))
			//data.Append("&paperType=" + WebUtility.UrlEncode("White 24#"))
			//data.Append("&printOption=" + WebUtility.UrlEncode("Printing One side"))
			//data.Append("&documentId=" + WebUtility.UrlEncode(c2m.documentid))
			//data.Append("&addressId=" + WebUtility.UrlEncode(c2m.addressid))
			//Console.Write(data.ToString)
			//byteData = UTF8Encoding.UTF8.GetBytes(Data.ToString())

			//        postStream = request.GetRequestStream()
			//       postStream.Write(byteData, 0, byteData.Length)

			try {
				response = (HttpWebResponse) request.GetResponse();
			} catch (WebException wex) {
				// This exception will be raised if the server didn't return 200 - OK  
				// Try to retrieve more information about the network error  
				if ((wex.Response != null)) {
					HttpWebResponse errorResponse = null;
					try {
						errorResponse = (HttpWebResponse)wex.Response;
						Console.WriteLine("The server returned '{0}' with the status code {1} ({2:d}).", errorResponse.StatusDescription, errorResponse.StatusCode, errorResponse.StatusCode);
					} finally {
						if ((errorResponse != null))
							errorResponse.Close();
					}
				}
			} finally {
				if ((postStream != null))
					postStream.Close();
			}
			//

			try {
				reader = new StreamReader(response.GetResponseStream());

				// Console application output  
				string s = reader.ReadToEnd();
				reader.Close();
				if (string.IsNullOrEmpty(parsexml(s, "City"))) {
					ai.uspsStatus = "BAD ADDRESS";
					return;

				} else {
					ai.Address1 = parsexml(s, "Address1");
					ai.Address2 = parsexml(s, "Address2");
					ai.city = parsexml(s, "City");
					ai.state = parsexml(s, "State");
					ai.zip5 = parsexml(s, "Zip5");
					ai.zip4 = parsexml(s, "Zip4");
					ai.returntext = parsexml(s, "ReturnText");
					ai.uspsStatus = "1";
					if (!string.IsNullOrEmpty(ai.returntext)) {
						ai.uspsStatus = "2";
					}

				}
				//Return parsexml(s, "id")
				//    c2m.StatusPick.jobStatus = parsexml(s, "status")
				//MsgBox(s)

			} finally {
				// If c2m.jobid = 0 Then
				//            c2m.StatusPick.jobStatus = 99
				//End If
				//If Not response Is Nothing Then response.Close()
			}
		}
		private string parsexml(string strxml, string lookfor)
		{

			string s = "";

			// Create an XmlReader

			try {

				using (XmlReader reader = XmlReader.Create(new StringReader(strxml))) {

					//            reader.ReadToFollowing(lookfor)
					//reader.MoveToFirstAttribute()
					//Dim genre As String = reader.Value
					//output.AppendLine("The genre value: " + genre)

					reader.ReadToFollowing(lookfor);
					s = reader.ReadElementContentAsString();
					reader.Close();

				}
			} catch (Exception ex) {
			}
			return s;



		}
		#endregion
		#region "Send Data to Click to Mail Final Steps"
        private void btn_upload_Click(object sender, EventArgs e)
        {
            keepopen = true;
            if (_mode == 2)
            {
                MsgBoxResult y = Interaction.MsgBox("You are sending this ENTIRE PDF to every name that is not Ommitted, CLick yes to continue", MsgBoxStyle.YesNo);
                if (y == MsgBoxResult.No)
                {
                    return;
                }
                if (File.Exists("tmp.pdf"))
                {
                    try
                    {
                        File.Delete("tmp.pdf");
                    }
                    catch (Exception ex)
                    {
                        Interaction.MsgBox("Please Close file and try again");
                        return;
                    }

                }
          //      addresscollection aic1 = (addresscollection) this.DataGridView2.DataSource;
            //    addresscollection aic2 = new addresscollection();
              //  Utils.Merge(_sourcefilename, (string)"tmp.pdf", ref aic1);
                
                //XmlNode batchnode = null;

                //foreach (addressitem ai in aic1)
                //{
                    //addressitem ai1 = ai;
                    //if (!ai.ommitted)
                    //{
                        //parseaddresssingle_GeneratedPDF(ref ai1, ref batchnode);
                    //}
                    //aic2.Add(ai1);
                //}

                //_sourcefilename = "tmp.pdf";

//                Invoke(new updatedatagrid(updatedatagridonMail), new object[] { aic2 });
                //Invoke(New updatecomplete(AddressOf updatecompleted), New Object() {})
                //Process.Start("tmp.pdf")

                startupload();
  //              loadpdf(_sourcefilename);


            }
            else
            {


                if (XMLDOC == null)
                {
                    verifydocument(true);
                    //
                    return;

                }
                else
                {
                    MsgBoxResult y = Interaction.MsgBox("This is about to submit Do you want to Re Verify before Submit, if not any template changes since the last run will not be included?", MsgBoxStyle.YesNo);
                    if (y == MsgBoxResult.Yes)
                    {
                        verifydocument(true);
                        return;
                    }
                    else
                    {
                        startupload();
                    }
                }
            }

        }


		private void startupload()
		{
			rasterizer.Close();

			if (Convert.ToBoolean( _dtt.Select("setting = true and fieldname = 'testMode'")[0]["misc"]) == true) {
				//mode = frm_clicktomail.mode.test;
			} else {
				//mode = frm_clicktomail.mode.live;
			}
			sentXML = XMLDOC.OuterXml;
            Console.Write(sentXML);
			frm_Click2Mail  x = new frm_Click2Mail(XMLDOC, _sourcefilename, mode, (string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"], Decrypt((string) _dtt.Select("setting = true and fieldname = 'password'")[0]["misc"]), this);
            x.keepopen = this.keepopen;
			x.Show();
           }
		#endregion
		#region "File Watch"
		private void FileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
		{
			updatefiles();
		}
		private void FileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
		{
			updatefiles();
		}
		private void updatefiles(string text = "")
		{

			try {

				string[] s = System.IO.Directory.GetFiles(_path + "\\", text + "*.c2m");
				this.lb_SavedTemplates.Items.Clear();
				foreach (string ss in s) {
					FileInfo f = new FileInfo(ss);
					this.lb_SavedTemplates.Items.Add(Strings.Replace(f.Name, ".c2m", ""));
				}

			} catch (Exception ex) {
			}
		}
		#endregion
		#region "Single to Multiple"

		private void singletomultiple()
		{
			if (string.IsNullOrEmpty(_xlsfilename)) {
				MsgBoxResult y =Microsoft.VisualBasic.Interaction.MsgBox("This feature is if you are sending EVERY PAGE of this PDF to multiple recipients, are you sure this is what you want to do?");
				if (y == MsgBoxResult.No) {
					return;
				}
			}

			frm_SendSingleDocument  frm = new frm_SendSingleDocument ();
			frm._filename = _xlsfilename;
			frm.TextBox1.Text = _xtemplate;
			frm.ShowDialog();
			if (string.IsNullOrEmpty(frm.TextBox1.Text)) {
				return;
			}
			string xTemplate = frm.TextBox1.Text;

			XMLDOC = null;
			_xlsfilename = frm._filename;
			_xtemplate = xTemplate;
			_sourcefilename = this.OpenFileDialog1.FileName;
			badaddress = "";
			_startuploadwhendone = false;
			DataTable dt = (System.Data.DataTable ) frm.DataGridView1.DataSource;
			this.DataGridView2.DataSource = null;
			if ((dt != null)) {
				_mode = 2;
				var x = new System.Threading.Thread(() => this.verifysendsingletomultiple(xTemplate, dt));
				x.IsBackground = false;
				x.Start();
			}
		}
		private void Button8_Click_1(object sender, EventArgs e)
		{
			singletomultiple();
		}

		private void verifysendsingletomultiple(string xtemplate, DataTable dt)
		{
			addresscollection aic = new addresscollection();
			string x = null;
			x = xtemplate;
			System.Xml.XmlNode batchnode = null;
            System.Xml.XmlNode recipients = null;

			FileStream x1 = new FileStream(_sourcefilename, FileMode.Open);
			iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(x1);
			//AWESOME!!
			x1.Close();
			x1.Dispose();
			string s = "";
			string s1 = "";
			int nop = reader.NumberOfPages;
			reader.Close();
			System.Xml.XmlNode ep = null;
			System.Xml.XmlNode batch = null;
			int pages = reader.NumberOfPages;
			int i = 0;
			foreach (DataRow r in dt.Rows) {
				this.Label2.Invoke(new updatetext(updatelabel1text), new object[] { "Processing Page " + (i + 1) + " of " + dt.Rows.Count });
				i += 1;
				foreach (DataColumn c in dt.Columns) {
					try {
						if ((!object.ReferenceEquals(dt.Rows[1][c], DBNull.Value))) {
							x = Strings.Replace(x, "{" + c.ColumnName + "}", (string) r[c]);
						} else {
							x = Strings.Replace(x, "{" + c.ColumnName + "}", "");
						}

					} catch {
					}
				}
				x = Regex.Replace(x, "^\\s+$[\\r\\n]*", "", RegexOptions.Multiline);
				addressitem ai = null;

				if (parseaddresssingledoctomultiple(ref ai, ref batchnode, x, 1, nop, i, ref recipients)) {
				}
				//EVERY PAGE IS GOOD
				aic.Add(ai);
				x = xtemplate;
			}
			//Me.DataGridView2.DataSource = aic


			this.RichTextBox1.Invoke(new updatert(updaterichtext), new object[] {
				XMLDOC,
				badaddress
			});
			//Me.RichTextBox1.Invoke(New updatert(AddressOf updaterichtext), New Object() {badaddress})
			Invoke(new updatedatagrid(updatedatagridonMail), new object[] { aic });
			Invoke(new updatecomplete(updatecompleted), new object[]{});
		}
		#endregion

		private void SetupStationaryFields_Disposed(object sender, EventArgs e)
		{
			//rasterizer.Close()
		}
		private void SetupStationaryFields_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!string.IsNullOrEmpty(_file)) {
				rasterizer.Close();
			}

		}

		public enum loadtype
		{
			singledoc,
			templatemulti,
			regular
		}
		private loadtype _loadtype = loadtype.regular;
		public void templatemulti(string filename, string template, bool startuploadwhendone = false)
		{
			_loadtype = loadtype.templatemulti;
			_sourcefilename = filename;
			_CurrentTemplate = _path + "\\" + template;
			_startuploadwhendone = startuploadwhendone;
			CurrentPage = 1;
		}
		private void SetupStationaryFields_Load(object sender, System.EventArgs e)
		{
			if (_hideform) {
				//  Me.Hide()
				loadtemplate();

				if (string.IsNullOrEmpty((string) _dtt.Select("setting = true and fieldname = 'username'")[0]["misc"])) {
					MessageBox.Show("You have not setup this Print Document Yet.");
					openform();
				}
				badaddress = "";
				XMLDOC = null;
				if (_loadtype == loadtype.singledoc) {
					System.Threading.Thread x = new System.Threading.Thread(verifySingleItem);
					x.IsBackground = false;
					x.Start();
				} else if (_loadtype == loadtype.templatemulti) {
					verifydocument(true);
				}
			}
			gvi = new GhostscriptVersionInfo(sDLLPath);
			rasterizer = new GhostscriptRasterizer();

			FileSystemWatcher1.Path = _path;
			if (_hideform == false) {
				startload();
			}

			updatefiles();
			this.WindowState = FormWindowState.Normal;
			this.Activate();

		}

        private void Button8_Click(object sender, EventArgs e)
        {
            singletomultiple();
        }


      

        


    
	}

    
}
