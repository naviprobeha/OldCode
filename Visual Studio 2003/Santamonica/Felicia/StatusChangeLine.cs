using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for StatusChangeLine.
	/// </summary>
	public class StatusChangeLine : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox userNameBox;
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox containerQtyBox;
		private System.Windows.Forms.TextBox tripMeterBox;
		private System.Windows.Forms.Label label4;
	
		private Status status;

		public StatusChangeLine(Status status)
		{
			this.status = status;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

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
			this.userNameBox = new System.Windows.Forms.TextBox();
			this.containerQtyBox = new System.Windows.Forms.TextBox();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tripMeterBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			// 
			// userNameBox
			// 
			this.userNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.userNameBox.Location = new System.Drawing.Point(8, 72);
			this.userNameBox.Size = new System.Drawing.Size(152, 26);
			this.userNameBox.Text = "";
			// 
			// containerQtyBox
			// 
			this.containerQtyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.containerQtyBox.Location = new System.Drawing.Point(8, 120);
			this.containerQtyBox.Size = new System.Drawing.Size(152, 26);
			this.containerQtyBox.Text = "";
			// 
			// statusBox
			// 
			this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusBox.Location = new System.Drawing.Point(8, 24);
			this.statusBox.Size = new System.Drawing.Size(152, 26);
			this.statusBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Text = "Aktuell status:";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(168, 88);
			this.button3.Size = new System.Drawing.Size(144, 32);
			this.button3.Text = "Stämpla ut";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(168, 48);
			this.button2.Size = new System.Drawing.Size(144, 32);
			this.button2.Text = "Rast";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(168, 8);
			this.button1.Size = new System.Drawing.Size(144, 32);
			this.button1.Text = "Stämpla in";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 104);
			this.label2.Size = new System.Drawing.Size(144, 20);
			this.label2.Text = "Antal containers på flaket:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Text = "Inloggad som:";
			// 
			// tripMeterBox
			// 
			this.tripMeterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tripMeterBox.Location = new System.Drawing.Point(8, 168);
			this.tripMeterBox.Size = new System.Drawing.Size(152, 26);
			this.tripMeterBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 152);
			this.label4.Size = new System.Drawing.Size(144, 20);
			this.label4.Text = "Trippmätare";
			// 
			// StatusChangeLine
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tripMeterBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.userNameBox);
			this.Controls.Add(this.containerQtyBox);
			this.Controls.Add(this.statusBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Text = "Ändra status";

		}
		#endregion

		private void updateForm()
		{
					

			statusBox.Text = status.getStatusText();
			userNameBox.Text = status.mobileUserName;
			containerQtyBox.Text = status.countContainers().ToString();

			tripMeterBox.Text = status.tripMeter.ToString() + " km";

			//button4.Text = "Lasta container";
			//if (containerNoBox.Text != "") button4.Text = "Lossa container";


		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status.status = 1;	
			this.Close();

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status.status = 2;
			this.Close();

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			status.status = 0;
			status.mobileUserName = "";
			this.Close();

		}


	}
}
