using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Inventory.
	/// </summary>
	public class InventoryForm : System.Windows.Forms.Form, Logger
	{
		private DataItem dataItem;
		private SmartDatabase smartDatabase;

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox colorBox;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Label label1;
	
		public InventoryForm(DataItem dataItem, SmartDatabase smartDatabase)
		{
			this.dataItem = dataItem;
			this.smartDatabase = smartDatabase;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;


			DataColors dataColors = new DataColors(smartDatabase, dataItem);
			DataSet dataSet = dataColors.getDataSet();
			int i = 0;
			while(i < dataSet.Tables[0].Rows.Count)
			{
				colorBox.Items.Add(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
				i++;
			}
			

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
			this.button1 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.colorBox = new System.Windows.Forms.ComboBox();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(168, 200);
			this.button1.Size = new System.Drawing.Size(64, 20);
			this.button1.Text = "Fråga";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 56);
			this.listBox1.Size = new System.Drawing.Size(224, 132);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 200);
			this.label1.Size = new System.Drawing.Size(160, 56);
			this.label1.Text = "Klicka på knappen till höger för att fråga efter lagersaldo på aktuell artikel.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 8);
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.Text = "Färg:";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(104, 20);
			this.descriptionBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(64, 20);
			this.itemNoBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 8);
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.Text = "Beskrivning:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 8);
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.Text = "Artikelnr:";
			// 
			// colorBox
			// 
			this.colorBox.Location = new System.Drawing.Point(192, 24);
			this.colorBox.Size = new System.Drawing.Size(40, 21);
			// 
			// Inventory
			// 
			this.Controls.Add(this.colorBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Lagersaldo";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			button1.Enabled = false;
			Service synchService = new Service("inventoryRequest", smartDatabase);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(dataItem);

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Förfrågan misslyckades.");
				}
				else
				{
					write("Förfrågan klar.");
					System.Windows.Forms.MessageBox.Show(serviceResponse.inventory.item.no+": "+serviceResponse.inventory.item.inventory, "Lagersaldo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
			}
			button1.Enabled = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		
		}
		#region Logger Members

		public void write(string message)
		{
			listBox1.Items.Add(message);
			Application.DoEvents();
		}

		#endregion
	}
}
