using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for PlanStore.
	/// </summary>
	public class JobFreq : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridTableStyle jobTable;

		private int qtyFreq1;
		private int qtyFreq2;
		private int qtyFreq3;
		private int qtyFreq4;
	
		private SmartDatabase smartDatabase;
		private DataWhseActivityHeader dataWhseActivityHeader;

		public JobFreq(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader)
		{
			this.smartDatabase = smartDatabase;
			this.dataWhseActivityHeader = dataWhseActivityHeader;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			label1.Text = label1.Text + " " + dataWhseActivityHeader.no;

			updateButtons();
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
			this.jobTable = new System.Windows.Forms.DataGridTableStyle();
			this.button6 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			// 
			// jobTable
			// 
			this.jobTable.MappingName = "job";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button6.Location = new System.Drawing.Point(8, 231);
			this.button6.Size = new System.Drawing.Size(224, 32);
			this.button6.Text = "Tillbaka";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Uttag - Batch:";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button5.Location = new System.Drawing.Point(120, 192);
			this.button5.Size = new System.Drawing.Size(112, 32);
			this.button5.Text = "Storvolym";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button4.Location = new System.Drawing.Point(120, 152);
			this.button4.Size = new System.Drawing.Size(112, 32);
			this.button4.Text = "Mkt låg";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button3.Location = new System.Drawing.Point(120, 112);
			this.button3.Size = new System.Drawing.Size(112, 32);
			this.button3.Text = "Låg";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(120, 72);
			this.button2.Size = new System.Drawing.Size(112, 32);
			this.button2.Text = "Mellan";
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(120, 32);
			this.button1.Size = new System.Drawing.Size(112, 32);
			this.button1.Text = "Hög";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Size = new System.Drawing.Size(104, 184);
			this.label2.Text = "Välj önskad frekvensklass genom att peka på respektive knapp här till höger.";
			// 
			// JobFreq
			// 
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Text = "Påfyllning - Uttag";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			showJobItems(1);
		}

		private void showJobItems(int freq)
		{
			JobItems jobItems = new JobItems(smartDatabase, dataWhseActivityHeader, freq);
			jobItems.ShowDialog();
			updateButtons();
			jobItems.Dispose();
		}

		private void button2_Click_1(object sender, System.EventArgs e)
		{
			showJobItems(2);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			showJobItems(3);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			showJobItems(4);
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			showJobItems(5);
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void updateButtons()
		{
			DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);

			qtyFreq1 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 1, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
			qtyFreq2 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 2, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
			qtyFreq3 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 3, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
			qtyFreq4 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 4, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);

			button1.Text = "Hög ("+qtyFreq1+")";
			button2.Text = "Mellan ("+qtyFreq2+")";
			button3.Text = "Låg ("+qtyFreq3+")";
			button4.Text = "Mkt låg ("+qtyFreq4+")";
			button5.Text = "Storvol (0)";
		}


	}
}
