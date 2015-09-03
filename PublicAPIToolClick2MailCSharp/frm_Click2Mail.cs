
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

using System.IO;
using System.Net;
using System.Text;
using System.Xml;
namespace ConvertedClick2Mail
{
    public partial class frm_Click2Mail : Form
    {
        //public frm_Click2Mail()
//        {
            
  //      }

        public frm_Click2Mail(XmlDocument xml, string file, mode m, string username, string pw, SetupStationaryFields caller)
    {
        
	frm = caller;
	// This call is required by the designer.
	InitializeComponent();
	if (m == mode.live) {
		_url = _Lmainurl;
	} else {
		_url = _Smainurl;
	}
	_authinfo = username + ":" + pw;
	_XMLDOC = xml;
	_file = file;
	// Add any initialization after the InitializeComponent() call.

}


		private HttpWebRequest wr;
		private XmlDocument _XMLDOC = null;
		private string _file = string.Empty;
		private const string _Smainurl = "https://stage-batch.click2mail.com";
		private const string _Lmainurl = "https://batch.click2mail.com";
		private string _url = string.Empty;
		private string _authinfo = string.Empty;
        private bool _keepopen = false;

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
            

		public delegate void UpdateStatusTextCallback(string text);
		public delegate void UpdateCountTextCallback(string text);
		public delegate void processdone(int success, string results);

		private SetupStationaryFields frm;
		public void updatestatuslabel(string text)
		{
			this.lbl_status.Text  = text;
		}


		public void updatecountlabel(string text)
		{
			this.lbl_count.Text = text;
		}


		public void webrequestdo()
		{
			try {
				int batchid = 0;
				this.lbl_status.Invoke(new UpdateStatusTextCallback(updatestatuslabel), new object[] { "Creating Batch" });
				batchid = createbatch();
				this.lbl_status.Invoke(new UpdateStatusTextCallback(updatestatuslabel), new object[] { "Uploading XML" });
				//uploadfilexml(batchid)
				uploadxml(batchid);
				this.lbl_status.Invoke(new UpdateStatusTextCallback(updatestatuslabel), new object[] { "Uploading Document" });
				uploadPDF(batchid);

				this.lbl_status.Invoke(new UpdateStatusTextCallback(updatestatuslabel), new object[] { "Submit" });
				submitbatch(batchid);
				string results = string.Empty;
				if (getbatchstatus(batchid, ref results) == "false") {
					if (parsexml(results, "submitted") == "true") {
						Invoke(new processdone(processdonesub), new object[] {
							1,
							results
						});
					} else {
						Invoke(new processdone(processdonesub), new object[] {
							2,
							results
						});
					}

				} else {
					Invoke(new processdone(processdonesub), new object[] {
						3,
						results
					});
				}

			} catch (Exception ex) {
				string results = "<error>There was an Error and this request did not complete, Please check your username and PW and also verify it is correctly set for the LIVE or Stage servers.  Keep in mind each server requires it's own login and credentials " + ex.Message + "</error>";
				Invoke(new processdone(processdonesub), new object[] {
					3,
					results
				});
			}

		}

		public void processdonesub(int success, string text)
		{
			_XMLDOC.LoadXml(text);
			frm.RichTextBox1.Text = frm.Beautify(_XMLDOC);


			if (success == 1) {
				Interaction.MsgBox("Successfully Sent");
				frm.btn_upload.Enabled = false;
			} else if (success == 2) {
				Interaction.MsgBox("Batch Did not SUBMIT Successfully READ ERROR RESULTS", MsgBoxStyle.Critical);
				frm.merror = text;
			} else if (success == 3) {
				Interaction.MsgBox("Batch Did not complete Successfully", MsgBoxStyle.Critical);
				frm.merror = text;
			}
            
            this.Close();
			frm.iscomplete = true;
            
            if(!keepopen)
            { 
			    frm.Close();
            }
		}
		private string getbatchstatus(int batchid, ref string results)
		{
			string strURI = string.Empty;
			strURI = _url + "/v1/batches/" + batchid;
			Console.WriteLine(strURI);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURI);
			string authinfo = null;
			authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));
			request.Headers["Authorization"] = "Basic " + authinfo;
			request.Method = System.Net.WebRequestMethods.Http.Get;
			string result = null;
			try {
				using (WebResponse response = request.GetResponse()) {
					using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
						result = reader.ReadToEnd();

					}
				}
				results = result;


				return parsexml(result, "hasErrors");
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return "";
		}
		private int createbatch()
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;
			Uri address = null;
			StringBuilder data = null;
			byte[] byteData = null;
			Stream postStream = null;

			address = new Uri(_url + "/v1/batches");

			string authinfo = null;
			authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));

			// Create the web request  
			request = (HttpWebRequest)WebRequest.Create(address);
			request.Headers["Authorization"] = "Basic " + authinfo;
			request.Method = "POST";
			request.ContentType = "text/plain";
			try {
				response =(HttpWebResponse ) request.GetResponse();
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
				Console.Write(s);
				return Convert.ToInt32(parsexml(s, "id"));
				//    c2m.StatusPick.jobStatus = parsexml(s, "status")
				//MsgBox(s)

			} finally {
				// If c2m.jobid = 0 Then
				//            c2m.StatusPick.jobStatus = 99
				//End If
				//If Not response Is Nothing Then response.Close()
			}
		}
		private void submitbatch(int batchid)
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;
			Uri address = null;

			Stream postStream = null;

			address = new Uri(_url + "/v1/batches/" + batchid);

			string authinfo = null;
			authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));

			// Create the web request  
			request = (HttpWebRequest)WebRequest.Create(address);
			request.Headers["Authorization"] = "Basic " + authinfo;
			request.Method = "POST";

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
			} finally {
			}

		}

		private void uploadxml(int batchid)
		{
			string strURI = string.Empty;
			strURI = _url + "/v1/batches/" + batchid;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURI);
			string authinfo = null;
			authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));
			request.Headers["Authorization"] = "Basic " + authinfo;
			request.Accept = "application/xml";
			request.Method = "PUT";
			using (MemoryStream ms = new MemoryStream()) {
				_XMLDOC.Save(ms);
				request.ContentLength = ms.Length;
				ms.WriteTo(request.GetRequestStream());
			}
			string result = null;

			using (WebResponse response = request.GetResponse()) {
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					result = reader.ReadToEnd();
				}
			}

			Console.WriteLine(result);
		}

		private void uploadPDF(int batchid)
		{
			WebClient client = new WebClient();

			string strURI = string.Empty;
			strURI = _url + "/v1/batches/" + batchid;
			string authinfo = null;
			authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));
			client.Headers["Authorization"] = "Basic " + authinfo;
			client.Headers.Add("Content-Type", "application/pdf");
			//Dim sentXml As Byte() = System.Text.Encoding.ASCII.GetBytes(_XMLDOC.OuterXml)

			FileInfo fInfo = new FileInfo(_file);

			long numBytes = fInfo.Length;

			FileStream fStream = new FileStream(_file, FileMode.Open, FileAccess.Read);

			BinaryReader br = new BinaryReader(fStream);

			byte[] data = br.ReadBytes(Convert.ToInt32(numBytes));

			// Show the number of bytes in the array.


			br.Close();

			fStream.Close();




			byte[] response = client.UploadData(strURI, "PUT", data);

			Console.WriteLine(System.Text.Encoding.Default.GetString(response));


			//Console.WriteLine(response.ToString())
		}
		private string parsexml(string strxml, string lookfor)
		{

			string s = "99";

			// Create an XmlReader
			using (XmlReader reader = XmlReader.Create(new StringReader(strxml))) {

				//            reader.ReadToFollowing(lookfor)
				//reader.MoveToFirstAttribute()
				//Dim genre As String = reader.Value
				//output.AppendLine("The genre value: " + genre)

				reader.ReadToFollowing(lookfor);
				s = reader.ReadElementContentAsString();
				reader.Close();



			}

			return s;



		}

		public enum mode
		{
			test,
			live
		}

        private void frm_Click2Mail_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.TopMost = true;
            this.lbl_count.Text = _file;
            System.Threading.Thread worker = new System.Threading.Thread(webrequestdo);
            worker.IsBackground = true;
            worker.Start();
        }

       
    }
}
        