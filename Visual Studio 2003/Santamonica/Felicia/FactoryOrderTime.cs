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
	public class FactoryOrderTime : System.Windows.Forms.Form
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
		private int formStatus;
		private System.Windows.Forms.TextBox dateTimeBox;
		private System.Windows.Forms.TextBox durationBox;
		private int newStatus;

		private DateTime currentDateTime;
		private int loadDuration;
		private System.Windows.Forms.TextBox waitDurationBox;
		private int waitDuration;
		private int reasonValue;
		private System.Windows.Forms.Label label1;
		private int extraDist;
		private System.Windows.Forms.Label label3;
		private int extraTime;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.TextBox extraDistBox;
		private System.Windows.Forms.TextBox extraTimeBox;
		private string reasonText;


		public FactoryOrderTime(SmartDatabase smartDatabase, Status status, DataFactoryOrder dataFactoryOrder, int newStatus)
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

			currentDateTime = DateTime.Now;

			if (newStatus == 3)
			{
				label2.Text = "Klockslag lastning:";
				label5.Text = "Lastningstid:";
				loadDuration = dataFactoryOrder.loadDuration;
				waitDuration = dataFactoryOrder.loadWaitDuration;
			}

			if (newStatus == 4)
			{
				label2.Text = "Klockslag lossning:";
				label5.Text = "Lossningstid:";
				loadDuration = dataFactoryOrder.dropDuration;
				waitDuration = dataFactoryOrder.dropWaitDuration;				
			}

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
			this.dateTimeBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.durationBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.waitDurationBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.extraDistBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.extraTimeBox = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Återrapportera Biomalorder";
			// 
			// dateTimeBox
			// 
			this.dateTimeBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.dateTimeBox.Location = new System.Drawing.Point(8, 48);
			this.dateTimeBox.ReadOnly = true;
			this.dateTimeBox.Size = new System.Drawing.Size(240, 20);
			this.dateTimeBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.Text = "Klockslag lastning:";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(128, 176);
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
			// durationBox
			// 
			this.durationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.durationBox.Location = new System.Drawing.Point(8, 88);
			this.durationBox.ReadOnly = true;
			this.durationBox.Size = new System.Drawing.Size(80, 20);
			this.durationBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.Text = "Lasttid:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(168, 72);
			this.label6.Size = new System.Drawing.Size(64, 20);
			this.label6.Text = "Väntetid:";
			// 
			// waitDurationBox
			// 
			this.waitDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.waitDurationBox.Location = new System.Drawing.Point(168, 88);
			this.waitDurationBox.ReadOnly = true;
			this.waitDurationBox.Size = new System.Drawing.Size(80, 20);
			this.waitDurationBox.Text = "";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(256, 76);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Ändra";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(96, 76);
			this.button3.Size = new System.Drawing.Size(64, 32);
			this.button3.Text = "Ändra";
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(256, 37);
			this.button5.Size = new System.Drawing.Size(64, 32);
			this.button5.Text = "Ändra";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 112);
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.Text = "Extra mil:";
			// 
			// extraDistBox
			// 
			this.extraDistBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.extraDistBox.Location = new System.Drawing.Point(8, 128);
			this.extraDistBox.ReadOnly = true;
			this.extraDistBox.Size = new System.Drawing.Size(80, 20);
			this.extraDistBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(168, 112);
			this.label3.Size = new System.Drawing.Size(80, 20);
			this.label3.Text = "Extra tid (min):";
			// 
			// extraTimeBox
			// 
			this.extraTimeBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.extraTimeBox.Location = new System.Drawing.Point(168, 128);
			this.extraTimeBox.ReadOnly = true;
			this.extraTimeBox.Size = new System.Drawing.Size(80, 20);
			this.extraTimeBox.Text = "";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(96, 116);
			this.button6.Size = new System.Drawing.Size(64, 32);
			this.button6.Text = "Ändra";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(256, 116);
			this.button7.Size = new System.Drawing.Size(64, 32);
			this.button7.Text = "Ändra";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// FactoryOrderTime
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.extraTimeBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.extraDistBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.waitDurationBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.durationBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dateTimeBox);
			this.Controls.Add(this.label9);
			this.Text = "Lasta / Lossa Biomal";

		}
		#endregion


		private void button1_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Är tidsuppgifterna korrekta?", "Återrapportering", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				if (newStatus == 3)
				{
					dataFactoryOrder.shipDate = currentDateTime;
					dataFactoryOrder.shipTime = currentDateTime;
					dataFactoryOrder.loadDuration = loadDuration;
					dataFactoryOrder.loadWaitDuration = waitDuration;
					dataFactoryOrder.loadReasonValue = reasonValue;
					dataFactoryOrder.loadReasonText = reasonText;

				}
				if (newStatus == 4)
				{
					dataFactoryOrder.arrivalDateTime = currentDateTime;
					dataFactoryOrder.dropDuration = loadDuration;
					dataFactoryOrder.dropWaitDuration = waitDuration;
					dataFactoryOrder.dropReasonValue = reasonValue;
					dataFactoryOrder.dropReasonText = reasonText;
				}

				dataFactoryOrder.extraDist = extraDist;
				dataFactoryOrder.extraTime = extraTime;

				dataFactoryOrder.commit();
			
				this.formStatus = 1;
				this.Close();
			}
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
			this.dateTimeBox.Text = currentDateTime.ToString("yyyy-MM-dd HH:mm");
			this.durationBox.Text = loadDuration + " min";
			this.waitDurationBox.Text = waitDuration + " min";
			if (this.waitDuration > 0) this.waitDurationBox.Text = this.waitDurationBox.Text + " ("+this.reasonText+")"; 

			this.extraDistBox.Text = extraDist + " mil";
			this.extraTimeBox.Text = extraTime + " min";
		}


		private void button2_Click(object sender, System.EventArgs e)
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

				if (waitDuration > 0)
				{
					FactoryOrderReason factoryOrderReason = new FactoryOrderReason();
					factoryOrderReason.ShowDialog();
					this.reasonValue = factoryOrderReason.getReasonValue();
					this.reasonText = factoryOrderReason.getReasonText();
					factoryOrderReason.Dispose();
				}
			}
			
			numPad.Dispose();

			updateForm();

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			TimePad timePad = new TimePad();
			timePad.setDateTime(currentDateTime);

			timePad.ShowDialog();

			if (timePad.getDateTime().Year > 1753) currentDateTime = timePad.getDateTime();

			timePad.Dispose();

			updateForm();
		
		}

		private void button3_Click_1(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(loadDuration.ToString());
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					loadDuration = int.Parse(numPad.getInputString());
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
			}
			
			numPad.Dispose();

			updateForm();

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(extraDist.ToString());
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					extraDist = int.Parse(numPad.getInputString());
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
			}
			
			numPad.Dispose();

			updateForm();

		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(extraTime.ToString());
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					extraTime = int.Parse(numPad.getInputString());
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
			}
			
			numPad.Dispose();

			updateForm();


		}
	}
}
