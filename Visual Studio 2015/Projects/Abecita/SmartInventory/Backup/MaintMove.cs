using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for MaintMove.
	/// </summary>
	public class MaintMove : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button1;
	
		private SmartDatabase smartDatabase;

		public MaintMove(SmartDatabase smartDatabase)
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
			this.label2 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Size = new System.Drawing.Size(104, 184);
			this.label2.Text = "Välj uttag eller inlagring genom att klicka på knapparna till höger.";
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
			this.label1.Text = "Lagervård - Omflyttning";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button5.Location = new System.Drawing.Point(120, 136);
			this.button5.Size = new System.Drawing.Size(112, 88);
			this.button5.Text = "Inlagring";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(120, 32);
			this.button1.Size = new System.Drawing.Size(112, 88);
			this.button1.Text = "Uttag";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MaintMove
			// 
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button1);
			this.Text = "Lagervård - Omflyttning";

		}
		#endregion

		private void button6_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MaintMoveItem maintMoveItem = new MaintMoveItem(smartDatabase);
			maintMoveItem.ShowDialog();
			maintMoveItem.Dispose();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			MaintSaveItems maintSaveItems = new MaintSaveItems(smartDatabase);
			maintSaveItems.ShowDialog();
			maintSaveItems.Dispose();
					
			this.Close();

		}
	}
}
