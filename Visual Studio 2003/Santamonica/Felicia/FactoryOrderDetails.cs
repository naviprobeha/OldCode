using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LineJournalReportDistance.
	/// </summary>
	public class FactoryOrderDetails : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label9;
		
		private SmartDatabase smartDatabase;
		private DataFactoryOrder dataFactoryOrder;
		private Status status;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TextBox quantityBox;
		private System.Windows.Forms.TextBox realQuantityBox;
		private System.Windows.Forms.TextBox levelBox;
		private int formStatus;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TextBox phValueBox;
		private int newStatus;

		public FactoryOrderDetails(SmartDatabase smartDatabase, Status status, DataFactoryOrder dataFactoryOrder, int newStatus)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.smartDatabase = smartDatabase;
			this.dataFactoryOrder = dataFactoryOrder;
			this.status = status;

			this.newStatus = newStatus;

			updateForm();
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
			this.label9 = new System.Windows.Forms.Label();
			this.quantityBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.realQuantityBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.levelBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.phValueBox = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Lasta / Lossa Biomal";
			// 
			// quantityBox
			// 
			this.quantityBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.quantityBox.Location = new System.Drawing.Point(8, 48);
			this.quantityBox.ReadOnly = true;
			this.quantityBox.Size = new System.Drawing.Size(96, 20);
			this.quantityBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.Text = "Lastat antal:";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(224, 136);
			this.button4.Size = new System.Drawing.Size(88, 32);
			this.button4.Text = "Avbryt";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(224, 176);
			this.button1.Size = new System.Drawing.Size(88, 32);
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// realQuantityBox
			// 
			this.realQuantityBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.realQuantityBox.Location = new System.Drawing.Point(8, 88);
			this.realQuantityBox.ReadOnly = true;
			this.realQuantityBox.Size = new System.Drawing.Size(96, 20);
			this.realQuantityBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.Text = "Lossat antal:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 112);
			this.label6.Size = new System.Drawing.Size(104, 20);
			this.label6.Text = "Nivå efter lossning:";
			// 
			// levelBox
			// 
			this.levelBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.levelBox.Location = new System.Drawing.Point(8, 128);
			this.levelBox.ReadOnly = true;
			this.levelBox.Size = new System.Drawing.Size(96, 20);
			this.levelBox.Text = "";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(112, 116);
			this.button2.Size = new System.Drawing.Size(88, 32);
			this.button2.Text = "Ändra";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(112, 77);
			this.button3.Size = new System.Drawing.Size(88, 32);
			this.button3.Text = "Ändra";
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(112, 37);
			this.button5.Size = new System.Drawing.Size(88, 32);
			this.button5.Text = "Ändra";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 152);
			this.label1.Size = new System.Drawing.Size(104, 20);
			this.label1.Text = "PH-värde:";
			// 
			// phValueBox
			// 
			this.phValueBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.phValueBox.Location = new System.Drawing.Point(8, 168);
			this.phValueBox.ReadOnly = true;
			this.phValueBox.Size = new System.Drawing.Size(96, 20);
			this.phValueBox.Text = "";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(112, 156);
			this.button6.Size = new System.Drawing.Size(88, 32);
			this.button6.Text = "Ändra";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// FactoryOrderDetails
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.phValueBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.levelBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.realQuantityBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.quantityBox);
			this.Controls.Add(this.label9);
			this.Text = "Lasta / Lossa Biomal";

		}
		#endregion


		private void button1_Click(object sender, System.EventArgs e)
		{
			this.formStatus = 1;
			this.Close();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.formStatus = 0;
			this.Close();
		}

		public int getFormStatus()
		{
			return this.formStatus;
		}

		private void updateForm()
		{
			if (newStatus == 3)
			{
				this.realQuantityBox.Visible = false;
				this.levelBox.Visible = false;
				this.label5.Visible = false;
				this.label6.Visible = false;
				button2.Visible = false;
				button3.Visible = false;
			}
			if (newStatus == 4)
			{
				this.button5.Visible = false;
			}

			this.quantityBox.Text = (dataFactoryOrder.quantity * 1000).ToString("0") + " kg";
			this.realQuantityBox.Text = (dataFactoryOrder.realQuantity * 1000).ToString("0") + " kg";
			this.levelBox.Text = (dataFactoryOrder.consumerLevel * 1000).ToString("0") + " kg";
			this.phValueBox.Text = dataFactoryOrder.phValueShipping.ToString();

		}


		private void button2_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString((dataFactoryOrder.consumerLevel * 1000).ToString("0"));
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataFactoryOrder.consumerLevel = float.Parse(numPad.getInputString()) / 1000;
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataFactoryOrder.commit();
			}
			
			numPad.Dispose();

			updateForm();

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString((dataFactoryOrder.quantity * 1000).ToString("0"));
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataFactoryOrder.quantity = float.Parse(numPad.getInputString()) / 1000;
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataFactoryOrder.commit();
			}
			
			numPad.Dispose();

			updateForm();
		
		}

		private void button3_Click_1(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString((dataFactoryOrder.realQuantity * 1000).ToString("0"));
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataFactoryOrder.realQuantity = float.Parse(numPad.getInputString()) / 1000;
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataFactoryOrder.commit();
			}
			
			numPad.Dispose();

			updateForm();

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad(true);
			numPad.setInputString(dataFactoryOrder.phValueShipping.ToString());
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataFactoryOrder.phValueShipping = float.Parse(numPad.getInputString());
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataFactoryOrder.commit();
			}
			
			numPad.Dispose();

			updateForm();

		
		}
	}
}
