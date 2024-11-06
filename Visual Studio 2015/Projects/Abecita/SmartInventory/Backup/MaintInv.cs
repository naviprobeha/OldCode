using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for MaintInv.
	/// </summary>
	public class MaintInv : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox heIdBox;
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox locationBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
	
		private SmartDatabase smartDatabase;

		public MaintInv(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.button2 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.heIdBox = new System.Windows.Forms.TextBox();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.locationBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 232);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 7);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Lagervård - Inventering";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.heIdBox);
			this.panel1.Controls.Add(this.statusBox);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.descriptionBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 88);
			this.panel1.Size = new System.Drawing.Size(240, 128);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 83);
			this.label8.Size = new System.Drawing.Size(64, 20);
			this.label8.Text = "Status:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 35);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Beskrivning:";
			// 
			// heIdBox
			// 
			this.heIdBox.Location = new System.Drawing.Point(88, 104);
			this.heIdBox.ReadOnly = true;
			this.heIdBox.Size = new System.Drawing.Size(136, 20);
			this.heIdBox.Text = "";
			// 
			// statusBox
			// 
			this.statusBox.Location = new System.Drawing.Point(88, 80);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(136, 20);
			this.statusBox.Text = "";
			// 
			// sumQuantityBox
			// 
			this.sumQuantityBox.Location = new System.Drawing.Point(88, 56);
			this.sumQuantityBox.Size = new System.Drawing.Size(136, 20);
			this.sumQuantityBox.Text = "";
			this.sumQuantityBox.GotFocus += new System.EventHandler(this.sumQuantityBox_GotFocus);
			this.sumQuantityBox.TextChanged += new System.EventHandler(this.sumQuantityBox_TextChanged);
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(88, 32);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(136, 20);
			this.descriptionBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(88, 8);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(136, 20);
			this.itemNoBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 59);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Saldo:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 11);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Artikel:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 107);
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.Text = "HE ID:";
			// 
			// locationBox
			// 
			this.locationBox.Location = new System.Drawing.Point(88, 64);
			this.locationBox.ReadOnly = true;
			this.locationBox.Size = new System.Drawing.Size(136, 20);
			this.locationBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 64);
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.Text = "Lagerplats:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 24);
			this.label7.Size = new System.Drawing.Size(192, 32);
			this.label7.Text = "För att ändra aktuellt antal klicka i saldo-rutan nedan.";
			// 
			// MaintInv
			// 
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.locationBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Text = "Lagervård - Information";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void sumQuantityBox_TextChanged(object sender, System.EventArgs e)
		{
		}

		private void sumQuantityBox_GotFocus(object sender, System.EventArgs e)
		{
			QuantityForm quantityForm = new QuantityForm(itemNoBox.Text, descriptionBox.Text, smartDatabase);
			quantityForm.setValue(sumQuantityBox.Text);
			quantityForm.ShowDialog();
			if (quantityForm.getStatus() == 1)
			{
				sumQuantityBox.Text = quantityForm.getValue();
			}
			locationBox.Focus();
		
		}
	}
}
