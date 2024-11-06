using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LineOrderWaitTime.
	/// </summary>
	public class ReportWaitTime : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox waitDurationBox;
		private System.Windows.Forms.Label label9;

		private int waitDuration;
		private System.Windows.Forms.Button button1;


		public ReportWaitTime(string headerText)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			label9.Text = headerText;
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
			this.button5 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.waitDurationBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Återrapportera Linjeorder";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(216, 44);
			this.button5.Size = new System.Drawing.Size(96, 32);
			this.button5.Text = "Ändra";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.Text = "Väntetid:";
			// 
			// waitDurationBox
			// 
			this.waitDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.waitDurationBox.Location = new System.Drawing.Point(8, 56);
			this.waitDurationBox.ReadOnly = true;
			this.waitDurationBox.Size = new System.Drawing.Size(200, 20);
			this.waitDurationBox.Text = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(224, 176);
			this.button1.Size = new System.Drawing.Size(88, 32);
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// LineOrderWaitTime
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.waitDurationBox);
			this.Controls.Add(this.label9);
			this.Text = "Återrapportera linjeorder";

		}
		#endregion

		private void button5_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(waitDuration.ToString());
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					waitDuration = int.Parse(numPad.getInputString());
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}

			}
			
			numPad.Dispose();

			updateForm();

		}

		private void updateForm()
		{
			this.waitDurationBox.Text = waitDuration + " minuter";

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		public int getValue()
		{
			return this.waitDuration;
		}
	}
}
