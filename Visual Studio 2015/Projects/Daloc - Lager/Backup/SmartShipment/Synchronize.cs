using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Synchronize.
	/// </summary>
	public class Synchronize : System.Windows.Forms.Form, Logger
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		private DataSetup dataSetup;

		private SmartDatabase smartDatabase;
		private string serviceName;
		private bool fullSynch;

		private int status;

		private System.Threading.Timer closingTimer;
	
		public Synchronize(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.serviceName = "synchronization";
			this.fullSynch = true;
			this.dataSetup = smartDatabase.getSetup();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public Synchronize(SmartDatabase smartDatabase, DataSetup dataSetup, string serviceName)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.serviceName = serviceName;
			this.dataSetup = dataSetup;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(224, 40);
			this.label1.Text = "Klicka på knappen nedan för att starta synkroniseringen.";
			this.label1.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 56);
			this.listBox1.Size = new System.Drawing.Size(224, 132);
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(128, 200);
			this.button1.Size = new System.Drawing.Size(104, 20);
			this.button1.Text = "Synkronisera";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(128, 224);
			this.button2.Size = new System.Drawing.Size(104, 20);
			this.button2.Text = "Importera";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Synchronize
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Synkronisera";
			this.Load += new System.EventHandler(this.Synchronize_Load);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			write("--- Synkronisering ---");
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			button1.Enabled = false;
			button2.Enabled = false;
			Service synchService = new Service(serviceName, smartDatabase, dataSetup);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				DataSalesHeaders salesHeaders = new DataSalesHeaders(smartDatabase);

				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Synkroniseringen misslyckades.");
					write("Raderar skickade order.");
					salesHeaders.deleteReadySalesHeaders();
				}
				else
				{
					write("Raderar skickade order.");
					salesHeaders.deleteReadySalesHeaders();

					write("Synkronisering klar.");
					status = 1;
				}
			}
			else
			{
				write("Synkroniseringen misslyckades.");
			}


			button1.Enabled = true;
			button2.Enabled = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		}
		
		public void write(string message)
		{
			listBox1.Items.Add(message);
			Application.DoEvents();
		}

		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label1_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			button1.Enabled = false;
			button2.Enabled = false;

			importPackage();	

			button1.Enabled = true;
			button2.Enabled = true;
		}

		private void importPackage()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Xml files (*.xml)|*.xml";
			
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				System.Windows.Forms.Cursor.Show();

				write("Importerar "+openFileDialog.FileName);
				
				XmlDocument packageXml = new XmlDocument();
				packageXml.Load(openFileDialog.FileName);
				
				if (packageXml.DocumentElement != null)
				{
					XmlElement publication = packageXml.DocumentElement;
					if (publication != null)
					{
						Publication publicationObject = new Publication(publication, smartDatabase, this);
					}

				}

				int i = 1;
				string fileName = openFileDialog.FileName.Replace(".xml", "_"+i+".xml");
				while (File.Exists(fileName))
				{
					packageXml = new XmlDocument();
					packageXml.Load(fileName);
				
					if (packageXml.DocumentElement != null)
					{
						XmlElement publication = packageXml.DocumentElement;
						if (publication != null)
						{
							Publication publicationObject = new Publication(publication, smartDatabase, this);
						}

					}

					i++;
					fileName = openFileDialog.FileName.Replace(".xml", "_"+i+".xml");
				}

				write("Import klar.");

				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				System.Windows.Forms.Cursor.Hide();

			}
		}

		private void Synchronize_Load(object sender, System.EventArgs e)
		{
			status = 0;

			if (!fullSynch)
			{
				button1_Click(null, null);

				if (status == 1)
				{
					//
				}
			}
		}

		public void doClose(Object state)
		{
			this.Close();
		}
	}
}
