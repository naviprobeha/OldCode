using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for StoreForm.
	/// </summary>
	public class StoreItemQuantity : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
	
		private SmartDatabase smartDatabase;
		private int status;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox quantityBox;
		private System.Windows.Forms.TextBox scanPackageBox;
		private System.Windows.Forms.TextBox handleUnitBox;
		private System.Windows.Forms.Label label1;
		private DataSetup dataSetup;


		public StoreItemQuantity(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.dataSetup = new DataSetup(smartDatabase);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//


			this.scanPackageBox.Focus();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.quantityBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.scanPackageBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.handleUnitBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.handleUnitBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.quantityBox);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Location = new System.Drawing.Point(0, 88);
			this.panel1.Size = new System.Drawing.Size(240, 96);
			// 
			// quantityBox
			// 
			this.quantityBox.Location = new System.Drawing.Point(16, 64);
			this.quantityBox.Size = new System.Drawing.Size(208, 20);
			this.quantityBox.Text = "";
			this.quantityBox.GotFocus += new System.EventHandler(this.quantityBox_GotFocus);
			this.quantityBox.TextChanged += new System.EventHandler(this.quantityBox_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 48);
			this.label7.Text = "Antal:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(16, 8);
			this.label4.Size = new System.Drawing.Size(128, 20);
			this.label4.Text = "Scanna kolli ID:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.scanPackageBox);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Location = new System.Drawing.Point(0, 32);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// scanPackageBox
			// 
			this.scanPackageBox.Location = new System.Drawing.Point(16, 29);
			this.scanPackageBox.Size = new System.Drawing.Size(208, 20);
			this.scanPackageBox.Text = "";
			this.scanPackageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanLocationBox_KeyPress);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Sätt antal";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(16, 192);
			this.button2.Size = new System.Drawing.Size(208, 64);
			this.button2.Text = "Huvudmeny";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// handleUnitBox
			// 
			this.handleUnitBox.Location = new System.Drawing.Point(16, 24);
			this.handleUnitBox.ReadOnly = true;
			this.handleUnitBox.Size = new System.Drawing.Size(208, 20);
			this.handleUnitBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Text = "Kolli ID:";
			// 
			// StoreItemQuantity
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Text = "Sätt antal";

		}
		#endregion

		private void label3_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		public int getStatus()
		{
			return status;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 2;
			this.Close();
		}


		private void scanLocationBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				this.handleUnitBox.Text = scanPackageBox.Text;
				this.scanPackageBox.Text = "";
				this.quantityBox.Text = "";

				Sound sound = new Sound(Sound.SOUND_TYPE_OK);
				DataItemUnit dataItemUnit = new DataItemUnit(handleUnitBox.Text, smartDatabase);
				quantityBox.Text = ""+dataItemUnit.quantity;

				this.quantityBox.Focus();
			}
		
		}


		private void quantityBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void quantityBox_GotFocus(object sender, System.EventArgs e)
		{
			if (handleUnitBox.Text != "")
			{
				QuantityForm quantityForm = new QuantityForm(handleUnitBox.Text, "Hanteringsenhet", smartDatabase);
				quantityForm.setCaption("Antal", "ID");
				quantityForm.setValue(quantityBox.Text);
				quantityForm.ShowDialog();
				if (quantityForm.getStatus() == 1)
				{
					quantityBox.Text = quantityForm.getValue();
					DataItemUnit dataItemUnit = new DataItemUnit(handleUnitBox.Text, smartDatabase);
					dataItemUnit.quantity = float.Parse(quantityBox.Text);
					dataItemUnit.commit();
					Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
				}

				quantityForm.Dispose();
			}
			scanPackageBox.Focus();
			
		}
	}
}
