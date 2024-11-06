using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LineJournalReport.
	/// </summary>
	public class LineJournalReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		
		private SmartDatabase smartDatabase;
		private Status status;
		private DataLineJournal dataLineJournal;

		public LineJournalReport(SmartDatabase smartDatabase, Status status, DataLineJournal dataLineJournal)
		{
			//
			// Required for Windows Form Designer support
			//
			this.smartDatabase = smartDatabase;
			this.status = status;
			this.dataLineJournal = dataLineJournal;

			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			label9.Text = "Återrapportering rutt "+dataLineJournal.entryNo;
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Återrapportering";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(208, 48);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Lossa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(208, 88);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.Text = "Lasta";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(208, 128);
			this.button3.Size = new System.Drawing.Size(104, 32);
			this.button3.Text = "Slutför";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(208, 176);
			this.button4.Size = new System.Drawing.Size(104, 32);
			this.button4.Text = "Avbryt";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Size = new System.Drawing.Size(192, 32);
			this.label1.Text = "För att lossa fulla containers på fabriken, klicka på Lossa.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Size = new System.Drawing.Size(192, 32);
			this.label2.Text = "För att fylla på med tomma containers, klicka på Lasta.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 128);
			this.label3.Size = new System.Drawing.Size(192, 48);
			this.label3.Text = "För att slutföra rutten och återrapportera antal mil, klicka på Slutför.";
			// 
			// LineJournalReport
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label9);
			this.Text = "Återrapportering av rutt";

		}
		#endregion

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			LoadedContainers loadedContainers = new LoadedContainers(smartDatabase, status, 2, dataLineJournal.arrivalFactoryCode, 2, dataLineJournal.entryNo.ToString());
			loadedContainers.hideLoadContainerButton();
			loadedContainers.setUnloadContainers(dataLineJournal.getContainerList());
			loadedContainers.ShowDialog();

			int formStatus = loadedContainers.getFormStatus();

			loadedContainers.Dispose();

			if (formStatus == 1) 
			{
				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);

				ArrayList lineOrderList = dataLineJournal.getLoadedLineOrders();
				int i = 0;
				while (i < lineOrderList.Count)
				{
					DataLineOrder dataLineOrder = new DataLineOrder(smartDatabase, int.Parse(lineOrderList[i].ToString()));
					dataLineOrder.status = 10;
					dataLineOrder.commit();

					dataSyncActions.addSyncAction(7, 0, dataLineOrder.entryNo.ToString());
					
					i++;
				}

				dataLineJournal.status = 7;
				dataLineJournal.commit();

				dataSyncActions.addSyncAction(8, 0, dataLineJournal.entryNo.ToString());


				this.Close();
			}																																																							 

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			LoadedContainers loadedContainers = new LoadedContainers(smartDatabase, status, 2, dataLineJournal.arrivalFactoryCode, 2, dataLineJournal.entryNo.ToString());
			loadedContainers.hideUnLoadContainerButton();
			loadedContainers.ShowDialog();

			int formStatus = loadedContainers.getFormStatus();

			loadedContainers.Dispose();

			if (formStatus == 1) 
			{
				this.Close();
			}																																																							 
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			/*
			if (dataLineJournal.status < 7)
			{
				MessageBox.Show("Du måste lossa samtliga containers först.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				return;
			}
			*/
			if (dataLineJournal.status > 7)
			{
				MessageBox.Show("Rutten är redan slutförd.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				return;
			}

			LineJournalReportDistance lineJournalReportDistance = new LineJournalReportDistance(smartDatabase, status, dataLineJournal);
			lineJournalReportDistance.ShowDialog();

			int formStatus = lineJournalReportDistance.getFormStatus();

			lineJournalReportDistance.Dispose();

			if (formStatus == 1)
			{
				dataLineJournal.status = 8;
				dataLineJournal.measuredDistance = status.tripMeter;
				dataLineJournal.commit();

				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
				dataSyncActions.addSyncAction(8, 0, dataLineJournal.entryNo.ToString());

				status.clearTripMeter();

				this.Close();
			}
		}
	}
}
