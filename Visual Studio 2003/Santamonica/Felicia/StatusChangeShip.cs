using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for StatusChange.
	/// </summary>
	public class StatusChangeShip : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox containerNoBox;
		private System.Windows.Forms.TextBox userNameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox arrivalTimeBox;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button2;

		private Status status;
	
		public StatusChangeShip(Status status)
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
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.containerNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.userNameBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.arrivalTimeBox = new System.Windows.Forms.TextBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(168, 8);
			this.button1.Size = new System.Drawing.Size(144, 32);
			this.button1.Text = "Stämpla in";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(168, 88);
			this.button3.Size = new System.Drawing.Size(144, 32);
			this.button3.Text = "Stämpla ut";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Text = "Aktuell status:";
			this.label1.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
			// 
			// statusBox
			// 
			this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusBox.Location = new System.Drawing.Point(8, 24);
			this.statusBox.Size = new System.Drawing.Size(152, 26);
			this.statusBox.Text = "";
			this.statusBox.TextChanged += new System.EventHandler(this.statusBox_TextChanged);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(168, 128);
			this.button4.Size = new System.Drawing.Size(144, 32);
			this.button4.Text = "Lasta container";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// containerNoBox
			// 
			this.containerNoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.containerNoBox.Location = new System.Drawing.Point(8, 120);
			this.containerNoBox.Size = new System.Drawing.Size(152, 26);
			this.containerNoBox.Text = "";
			this.containerNoBox.TextChanged += new System.EventHandler(this.containerNoBox_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 104);
			this.label2.Text = "Containernr:";
			this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
			// 
			// userNameBox
			// 
			this.userNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.userNameBox.Location = new System.Drawing.Point(8, 72);
			this.userNameBox.Size = new System.Drawing.Size(152, 26);
			this.userNameBox.Text = "";
			this.userNameBox.TextChanged += new System.EventHandler(this.userNameBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Text = "Inloggad som:";
			this.label3.ParentChanged += new System.EventHandler(this.label3_ParentChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 152);
			this.label4.Size = new System.Drawing.Size(136, 20);
			this.label4.Text = "Klockslag för avlastning:";
			this.label4.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
			// 
			// arrivalTimeBox
			// 
			this.arrivalTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.arrivalTimeBox.Location = new System.Drawing.Point(8, 168);
			this.arrivalTimeBox.Size = new System.Drawing.Size(152, 26);
			this.arrivalTimeBox.Text = "";
			this.arrivalTimeBox.TextChanged += new System.EventHandler(this.arrivalTimeBox_TextChanged);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(168, 168);
			this.button5.Size = new System.Drawing.Size(144, 32);
			this.button5.Text = "Klockslag lossn.";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(168, 48);
			this.button2.Size = new System.Drawing.Size(144, 32);
			this.button2.Text = "Rast";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// StatusChangeShip
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.arrivalTimeBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.userNameBox);
			this.Controls.Add(this.containerNoBox);
			this.Controls.Add(this.button4);
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

		private void button4_Click(object sender, System.EventArgs e)
		{	
			if (button4.Text == "Lossa container")
			{
				if (MessageBox.Show("Lossa container "+status.containerNo+"?", "Lossa container", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					status.unLoadContainer();
				}
			}

			if (button4.Text == "Lasta container")
			{
				status.loadContainer();
			}


			updateForm();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (status.containerNo != "")
			{

				TimePad timePad = new TimePad();
				timePad.setDateTime(status.getData().arrivalTime);
				timePad.ShowDialog();

				DateTime arrivalTime = timePad.getDateTime();

				timePad.Dispose();

				if (arrivalTime.Year > 1753) status.setArrivalTime(arrivalTime);

				updateForm();
			}
			else
			{
				MessageBox.Show("Du måste lasta en container först.", "Fel");
			}
		}

		private void updateForm()
		{
			statusBox.Text = status.getStatusText();
			userNameBox.Text = status.mobileUserName;
			containerNoBox.Text = status.containerNo;

			arrivalTimeBox.Text = "";

			if ((status.getData().arrivalTime.Year > 2000) && (status.containerNo != ""))
			{
				arrivalTimeBox.Text = status.getData().arrivalTime.ToString("yyyy-MM-dd HH:mm");
			}

			button4.Text = "Lasta container";
			if (containerNoBox.Text != "") button4.Text = "Lossa container";


		}

		private void arrivalTimeBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label4_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label3_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void userNameBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label2_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void containerNoBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void statusBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label1_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
