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
	public class LineJournalReportDistance : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox measuredDistanceBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox actualDistanceBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label9;
		
		private SmartDatabase smartDatabase;
		private DataLineJournal dataLineJournal;
		private Status status;
		private System.Windows.Forms.TextBox fromBox;
		private System.Windows.Forms.TextBox toBox;
		private System.Windows.Forms.TextBox noOfOrdersBox;
		private System.Windows.Forms.TextBox calculatedDistanceBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox singleDistanceBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox trailerDistanceBox;
		private int formStatus;

		public LineJournalReportDistance(SmartDatabase smartDatabase, Status status, DataLineJournal dataLineJournal)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.smartDatabase = smartDatabase;
			this.dataLineJournal = dataLineJournal;
			this.status = status;

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
			this.label13 = new System.Windows.Forms.Label();
			this.measuredDistanceBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.actualDistanceBox = new System.Windows.Forms.TextBox();
			this.fromBox = new System.Windows.Forms.TextBox();
			this.toBox = new System.Windows.Forms.TextBox();
			this.noOfOrdersBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.calculatedDistanceBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.singleDistanceBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.trailerDistanceBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Slutför rutt";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(112, 72);
			this.label13.Size = new System.Drawing.Size(96, 16);
			this.label13.Text = "Körd sträcka:";
			this.label13.ParentChanged += new System.EventHandler(this.label13_ParentChanged);
			// 
			// measuredDistanceBox
			// 
			this.measuredDistanceBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.measuredDistanceBox.Location = new System.Drawing.Point(112, 88);
			this.measuredDistanceBox.ReadOnly = true;
			this.measuredDistanceBox.Size = new System.Drawing.Size(96, 20);
			this.measuredDistanceBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(216, 72);
			this.label1.Size = new System.Drawing.Size(96, 20);
			this.label1.Text = "Verklig sträcka:";
			// 
			// actualDistanceBox
			// 
			this.actualDistanceBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.actualDistanceBox.Location = new System.Drawing.Point(216, 88);
			this.actualDistanceBox.ReadOnly = true;
			this.actualDistanceBox.Size = new System.Drawing.Size(96, 20);
			this.actualDistanceBox.Text = "";
			// 
			// fromBox
			// 
			this.fromBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.fromBox.Location = new System.Drawing.Point(8, 48);
			this.fromBox.ReadOnly = true;
			this.fromBox.Size = new System.Drawing.Size(96, 20);
			this.fromBox.Text = "";
			// 
			// toBox
			// 
			this.toBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.toBox.Location = new System.Drawing.Point(112, 48);
			this.toBox.ReadOnly = true;
			this.toBox.Size = new System.Drawing.Size(96, 20);
			this.toBox.Text = "";
			// 
			// noOfOrdersBox
			// 
			this.noOfOrdersBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.noOfOrdersBox.Location = new System.Drawing.Point(216, 48);
			this.noOfOrdersBox.ReadOnly = true;
			this.noOfOrdersBox.Size = new System.Drawing.Size(96, 20);
			this.noOfOrdersBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.Text = "Från:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(112, 32);
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.Text = "Till:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(216, 32);
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.Text = "Antal order:";
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
			// calculatedDistanceBox
			// 
			this.calculatedDistanceBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.calculatedDistanceBox.Location = new System.Drawing.Point(8, 88);
			this.calculatedDistanceBox.ReadOnly = true;
			this.calculatedDistanceBox.Size = new System.Drawing.Size(96, 20);
			this.calculatedDistanceBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.Text = "Beräknad sträcka:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 112);
			this.label6.Size = new System.Drawing.Size(96, 20);
			this.label6.Text = "Sträcka singel:";
			// 
			// singleDistanceBox
			// 
			this.singleDistanceBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.singleDistanceBox.Location = new System.Drawing.Point(8, 128);
			this.singleDistanceBox.ReadOnly = true;
			this.singleDistanceBox.Size = new System.Drawing.Size(96, 20);
			this.singleDistanceBox.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 152);
			this.label7.Size = new System.Drawing.Size(96, 20);
			this.label7.Text = "Sträcka bil+släp:";
			// 
			// trailerDistanceBox
			// 
			this.trailerDistanceBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.trailerDistanceBox.Location = new System.Drawing.Point(8, 168);
			this.trailerDistanceBox.ReadOnly = true;
			this.trailerDistanceBox.Size = new System.Drawing.Size(96, 20);
			this.trailerDistanceBox.Text = "";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(112, 117);
			this.button2.Size = new System.Drawing.Size(88, 32);
			this.button2.Text = "Ändra";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(112, 157);
			this.button3.Size = new System.Drawing.Size(88, 32);
			this.button3.Text = "Ändra";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// LineJournalReportDistance
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.trailerDistanceBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.singleDistanceBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.calculatedDistanceBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.noOfOrdersBox);
			this.Controls.Add(this.toBox);
			this.Controls.Add(this.fromBox);
			this.Controls.Add(this.measuredDistanceBox);
			this.Controls.Add(this.actualDistanceBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label9);
			this.Text = "Slutför rutt";

		}
		#endregion

		private void label13_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Rapportera väntetid?", "Väntetid", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				ReportWaitTime lineJournalWaitTime = new ReportWaitTime("Väntetid lastning");
				lineJournalWaitTime.ShowDialog();

				dataLineJournal.dropWaitTime = lineJournalWaitTime.getValue();
				dataLineJournal.commit();

				lineJournalWaitTime.Dispose();

			}

			if (MessageBox.Show("Du kommer att slutföra rutten. Är du säker?", "Slutförning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
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
			this.fromBox.Text = dataLineJournal.departureFactoryCode;
			this.toBox.Text = dataLineJournal.arrivalFactoryCode;
			this.noOfOrdersBox.Text = dataLineJournal.countLineOrders().ToString();
			this.calculatedDistanceBox.Text = dataLineJournal.calculatedDistance.ToString("0")+" km";
			this.measuredDistanceBox.Text = dataLineJournal.measuredDistance.ToString("0")+" km";
			this.actualDistanceBox.Text = dataLineJournal.reportedDistance.ToString("0")+" km";
			this.measuredDistanceBox.Text = status.tripMeter+" km";
			this.singleDistanceBox.Text = dataLineJournal.reportedDistanceSingle.ToString("0")+" km";
			this.trailerDistanceBox.Text = dataLineJournal.reportedDistanceTrailer.ToString("0")+" km";

		}


		private void button2_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(this.singleDistanceBox.Text);
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataLineJournal.reportedDistanceSingle = float.Parse(numPad.getInputString());
					dataLineJournal.reportedDistance = dataLineJournal.reportedDistanceSingle + dataLineJournal.reportedDistanceTrailer;
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataLineJournal.commit();
			}
			
			numPad.Dispose();

			updateForm();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(this.trailerDistanceBox.Text);
			numPad.ShowDialog();

			if (numPad.getInputString() != "")
			{
				try
				{
					dataLineJournal.reportedDistanceTrailer = float.Parse(numPad.getInputString());
					dataLineJournal.reportedDistance = dataLineJournal.reportedDistanceSingle + dataLineJournal.reportedDistanceTrailer;
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}
				dataLineJournal.commit();
			}
			
			numPad.Dispose();

			updateForm();

		}
	}
}
