using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for PeriodReport.
	/// </summary>
	public class PeriodReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox fromDateBox;
		private System.Windows.Forms.Label orderNo3Label;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox toDateBox;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox mobileUserBox;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;

		private SmartDatabase smartDatabase;
	
		public PeriodReport(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			int i = 0;
			while (i <= 14)
			{
				DateTime aDate = DateTime.Now.AddDays((14-i)*-1);
				fromDateBox.Items.Add(aDate.ToString("yyyy-MM-dd"));
				toDateBox.Items.Add(aDate.ToString("yyyy-MM-dd"));
				
				i++;
			}

			fromDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
			toDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");


			mobileUserBox.Items.Add("Alla");
			mobileUserBox.Text = "Alla";

			DataMobileUsers dataMobileUsers = new DataMobileUsers(smartDatabase);
			DataSet mobileUserDataSet = dataMobileUsers.getDataSet();

			int j = 0;
			while (j < mobileUserDataSet.Tables[0].Rows.Count)
			{
				mobileUserBox.Items.Add(mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());

				j++;
			}
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
			this.fromDateBox = new System.Windows.Forms.ComboBox();
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.toDateBox = new System.Windows.Forms.ComboBox();
			this.button8 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.mobileUserBox = new System.Windows.Forms.ComboBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			// 
			// fromDateBox
			// 
			this.fromDateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular);
			this.fromDateBox.Location = new System.Drawing.Point(8, 48);
			this.fromDateBox.Size = new System.Drawing.Size(216, 24);
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Utskrift av periodrapport";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(112, 20);
			this.label1.Text = "Från och med datum:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Size = new System.Drawing.Size(104, 20);
			this.label2.Text = "Till och med datum:";
			// 
			// toDateBox
			// 
			this.toDateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular);
			this.toDateBox.Location = new System.Drawing.Point(8, 96);
			this.toDateBox.Size = new System.Drawing.Size(216, 24);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(232, 176);
			this.button8.Size = new System.Drawing.Size(80, 32);
			this.button8.Text = "Skriv ut";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(144, 176);
			this.button1.Size = new System.Drawing.Size(80, 32);
			this.button1.Text = "Tillbaka";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(280, 40);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(240, 40);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(280, 88);
			this.button2.Size = new System.Drawing.Size(32, 32);
			this.button2.Text = ">";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(240, 88);
			this.button3.Size = new System.Drawing.Size(32, 32);
			this.button3.Text = "<";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 128);
			this.label3.Size = new System.Drawing.Size(104, 20);
			this.label3.Text = "Chaufför:";
			// 
			// mobileUserBox
			// 
			this.mobileUserBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular);
			this.mobileUserBox.Location = new System.Drawing.Point(8, 144);
			this.mobileUserBox.Size = new System.Drawing.Size(216, 24);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(240, 136);
			this.button6.Size = new System.Drawing.Size(32, 32);
			this.button6.Text = "<";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(280, 136);
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.Text = ">";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// PeriodReport
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.mobileUserBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.toDateBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.orderNo3Label);
			this.Controls.Add(this.fromDateBox);
			this.Controls.Add(this.label1);
			this.Text = "Utskrift av periodrapport";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			Printer printer = new Printer(smartDatabase);

			if (!printer.init())
			{
				MessageBox.Show("Kan ej få kontakt med skrivaren. Kontrollera skrivaren.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DateTime fromDate = DateTime.Parse(fromDateBox.Text);
				DateTime toDate = DateTime.Parse(toDateBox.Text);

				string mobileUser = mobileUserBox.Text;
				if (mobileUser == "Alla") mobileUser = "";

				printer.printPeriodReport(fromDate, toDate, mobileUser);
				printer.close();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				this.Close();

			}

		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (fromDateBox.SelectedIndex > 0)
			{
				fromDateBox.SelectedIndex = fromDateBox.SelectedIndex - 1;
			}
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (fromDateBox.SelectedIndex < fromDateBox.Items.Count-1)
			{
				fromDateBox.SelectedIndex = fromDateBox.SelectedIndex + 1;
			}

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (toDateBox.SelectedIndex > 0)
			{
				toDateBox.SelectedIndex = toDateBox.SelectedIndex - 1;
			}

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (toDateBox.SelectedIndex < toDateBox.Items.Count-1)
			{
				toDateBox.SelectedIndex = toDateBox.SelectedIndex + 1;
			}

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if (mobileUserBox.SelectedIndex > 0)
			{
				mobileUserBox.SelectedIndex = mobileUserBox.SelectedIndex - 1;
			}

		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			if (mobileUserBox.SelectedIndex < mobileUserBox.Items.Count-1)
			{
				mobileUserBox.SelectedIndex = mobileUserBox.SelectedIndex + 1;
			}

		}
	}
}
