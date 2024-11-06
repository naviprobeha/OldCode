using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for Synchronize.
	/// </summary>
	public class JobSynch : System.Windows.Forms.Form, SmartInventory.Logger
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;

		private SmartDatabase smartDatabase;
	
		public JobSynch(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
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
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(224, 40);
			this.label1.Text = "Klicka \'Hämta\' för att hämta uppdrag ifrån Navision.";
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
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(8, 200);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Hämta";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(128, 200);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.Text = "Gå vidare";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// StoreSynch
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Hämta uppdrag";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			synchronize();
			openRegisterForm();
		}

		private void synchronize()
		{
			write("--- Synkronisering ---");
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			button1.Enabled = false;
			button2.Enabled = false;
			Service synchService = new Service("synchronization", smartDatabase);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Synkroniseringen misslyckades.");
				}
				write("Synkronisering klar.");

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
			openRegisterForm();
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

		private void openRegisterForm()
		{
			Jobs jobs = new Jobs(smartDatabase);
			jobs.ShowDialog();
			jobs.Dispose();
			
			if (System.Windows.Forms.MessageBox.Show("Synkronisera med Navision?", "Synkronisera", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
			{
				synchronize();

			}
			
			this.Close();

		}
	}
}
