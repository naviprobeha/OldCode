using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;

namespace Update
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Update : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox message;

		private string url = "";
		private System.Windows.Forms.Label caption;
		private System.Windows.Forms.Button button1;

		public Update()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			if (!getConfiguration())
			{
				Application.Exit();
			}

		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		public void run()
		{
			getUpdateInfo();
		}

		public void downloadFile(string fileUrl, string fileName)
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(fileUrl);
			req.Method = "GET";

			try
			{
				WebResponse response = req.GetResponse();

				Stream stream = response.GetResponseStream();

				int cbRead = 0;

				message.Text = "Laddar ner "+fileName+"...";
				Application.DoEvents();

				FileStream wrt = new FileStream(fileName, FileMode.Create);

				Byte[] data = new Byte[1024];

				long totalSize = response.ContentLength;
				int received = 0;
				
				cbRead = stream.Read(data, 0, 1024);
				while(cbRead > 0)
				{
					wrt.Write(data, 0, cbRead);

					received = received + cbRead;
					int progress = (int)((received / totalSize) * 100);
					progressBar1.Value = progress;
					Application.DoEvents();

					cbRead = stream.Read(data, 0, 1024);
				}

				wrt.Close();

			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.ToString());
			}

			Cursor.Current = Cursors.Default;
			Cursor.Hide();


		}



		private bool getConfiguration()
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load("\\Program Files\\Update\\update.xml");

				XmlElement docElement = xmlDoc.DocumentElement;

				caption.Text = docElement.GetElementsByTagName("title").Item(0).FirstChild.Value;
				this.Text = caption.Text;
				url = docElement.GetElementsByTagName("url").Item(0).FirstChild.Value;
				return true;
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
				return false;
			}

		}

		private void getUpdateInfo()
		{
			message.Text = "Letar efter uppdateringar...";
			Application.DoEvents();


			XmlDocument xmlDoc = new XmlDocument();

			//try
			//{
				xmlDoc.Load(url);



				XmlElement docElement = xmlDoc.DocumentElement;

				XmlNodeList xmlNodeList = docElement.GetElementsByTagName("file");
				int i = 0;
				while (i < xmlNodeList.Count)
				{
					XmlElement fileElement = ((XmlElement)xmlNodeList.Item(i));
					string fileUrl = fileElement.GetElementsByTagName("url").Item(0).FirstChild.Value;
					string fileName = fileElement.GetElementsByTagName("name").Item(0).FirstChild.Value;
	
					downloadFile(fileUrl, fileName);

					Application.DoEvents();
					i++;
				}
			//}
			//catch(Exception e)
			//{
			//	message.Text = "Inga uppdateringar funna...";
			//	Thread.Sleep(2000);
			//}

			message.Text = "Nedladdning klar.";

		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Update));
			this.message = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.caption = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// message
			// 
			this.message.Location = new System.Drawing.Point(16, 48);
			this.message.Size = new System.Drawing.Size(288, 20);
			this.message.Text = "";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 88);
			this.progressBar1.Size = new System.Drawing.Size(288, 20);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 160);
			this.pictureBox1.Size = new System.Drawing.Size(40, 56);
			// 
			// caption
			// 
			this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.caption.Location = new System.Drawing.Point(16, 16);
			this.caption.Size = new System.Drawing.Size(232, 20);
			this.caption.Text = "Uppdatering av SmartOrder";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(200, 160);
			this.button1.Size = new System.Drawing.Size(104, 40);
			this.button1.Text = "Uppdatera";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Update
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.caption);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.message);
			this.MinimizeBox = false;
			this.Text = "Uppdatering av SmartOrder";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Update());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.getUpdateInfo();

			Application.Exit();
		}
	}
}
