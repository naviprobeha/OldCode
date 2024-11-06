using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LineJournalList.
	/// </summary>
	public class LineJournalList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid lineJournalGrid;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label9;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Timer timer;
		private Status status;
		private DataSet lineJournalDataSet;
		private System.Windows.Forms.DataGridTableStyle lineJournalTable;
		private System.Windows.Forms.DataGridTextBoxColumn shipDateCol;
		private System.Windows.Forms.DataGridTextBoxColumn arrivalFactoryCol;
		private System.Windows.Forms.DataGridTextBoxColumn statusCol;
		private System.Windows.Forms.DataGridTextBoxColumn entryNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn noLoadedOrdersCol;
		private DataLineJournal currentLineJournal;

		public LineJournalList(SmartDatabase smartDatabase, Status status)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.status = status;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			updateGrid(this, null);

			timer = new System.Windows.Forms.Timer();
			timer.Tick +=new EventHandler(timer_Tick);
			timer.Interval = 15000;
			timer.Enabled = true;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		private void updateGrid(object sender, EventArgs e)
		{
			DataLineJournals dataLineJournals = new DataLineJournals(this.smartDatabase);
			DataLineOrders dataLineOrders = new DataLineOrders(this.smartDatabase);
			lineJournalDataSet = dataLineJournals.getDataSet();


			int i = 0;
			while(i < lineJournalDataSet.Tables[0].Rows.Count)
			{
				DataLineJournal dataLineJournal = new DataLineJournal(smartDatabase, int.Parse(lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));

				if (lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "5")
				{
					DataRow row = lineJournalDataSet.Tables[0].Rows[i];
					row["statusText"] = "Ny";
				}
				if (lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "6")
				{
					DataRow row = lineJournalDataSet.Tables[0].Rows[i];
					row["statusText"] = "Bekräftad";
				}
				if (lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "7")
				{
					DataRow row = lineJournalDataSet.Tables[0].Rows[i];
					row["statusText"] = "Lossad";
				}

				//ArrayList containerList = dataLineJournal.getContainerList();

				DataRow rowContainers = lineJournalDataSet.Tables[0].Rows[i];
				rowContainers["noOfLoadedOrders"] = dataLineOrders.countLoadedOrders(dataLineJournal).ToString();

				lineJournalDataSet.Tables[0].Rows[i].AcceptChanges();

				i++;
			}

			lineJournalGrid.DataSource = lineJournalDataSet.Tables[0];


		}

		public DataLineJournal getLineJournal()
		{
			return currentLineJournal;	
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label9 = new System.Windows.Forms.Label();
			this.lineJournalGrid = new System.Windows.Forms.DataGrid();
			this.lineJournalTable = new System.Windows.Forms.DataGridTableStyle();
			this.entryNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.shipDateCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.arrivalFactoryCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.noLoadedOrdersCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.statusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Rutter";
			// 
			// lineJournalGrid
			// 
			this.lineJournalGrid.Location = new System.Drawing.Point(8, 32);
			this.lineJournalGrid.Size = new System.Drawing.Size(304, 128);
			this.lineJournalGrid.TableStyles.Add(this.lineJournalTable);
			// 
			// lineJournalTable
			// 
			this.lineJournalTable.GridColumnStyles.Add(this.entryNoCol);
			this.lineJournalTable.GridColumnStyles.Add(this.shipDateCol);
			this.lineJournalTable.GridColumnStyles.Add(this.arrivalFactoryCol);
			this.lineJournalTable.GridColumnStyles.Add(this.noLoadedOrdersCol);
			this.lineJournalTable.GridColumnStyles.Add(this.statusCol);
			this.lineJournalTable.MappingName = "lineJournal";
			// 
			// entryNoCol
			// 
			this.entryNoCol.HeaderText = "Nr";
			this.entryNoCol.MappingName = "entryNo";
			this.entryNoCol.NullText = "";
			// 
			// shipDateCol
			// 
			this.shipDateCol.HeaderText = "Datum";
			this.shipDateCol.MappingName = "shipDate";
			this.shipDateCol.NullText = "";
			this.shipDateCol.Width = 70;
			// 
			// arrivalFactoryCol
			// 
			this.arrivalFactoryCol.HeaderText = "Till";
			this.arrivalFactoryCol.MappingName = "arrivalFactoryCode";
			this.arrivalFactoryCol.NullText = "";
			this.arrivalFactoryCol.Width = 80;
			// 
			// noLoadedOrdersCol
			// 
			this.noLoadedOrdersCol.HeaderText = "Lastade order";
			this.noLoadedOrdersCol.MappingName = "noOfLoadedOrders";
			this.noLoadedOrdersCol.NullText = "";
			this.noLoadedOrdersCol.Width = 60;
			// 
			// statusCol
			// 
			this.statusCol.HeaderText = "Status";
			this.statusCol.MappingName = "statusText";
			this.statusCol.NullText = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 168);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Välj";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(160, 168);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Stäng";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(56, 168);
			this.button3.Size = new System.Drawing.Size(96, 32);
			this.button3.Text = "Rapportera";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// LineJournalList
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lineJournalGrid);
			this.Controls.Add(this.label9);
			this.Text = "Rutter";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.LineJournalList_Closing);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (lineJournalDataSet.Tables[0].Rows.Count > 0)
			{
				if (lineJournalGrid.CurrentRowIndex > -1)
				{
				
					currentLineJournal = new DataLineJournal(smartDatabase, int.Parse(lineJournalGrid[lineJournalGrid.CurrentRowIndex, 0].ToString()));

					this.Close();
				}
				else
				{
					MessageBox.Show("Fel", "Du måste välja en rutt.");
				}
			}
			else
			{
				MessageBox.Show("Fel", "Du måste välja en rutt.");
			}		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (lineJournalGrid.CurrentRowIndex > -1)
			{

				currentLineJournal = new DataLineJournal(smartDatabase, int.Parse(lineJournalDataSet.Tables[0].Rows[lineJournalGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				LineJournalReport lineJournalReport = new LineJournalReport(smartDatabase, status, currentLineJournal);
				lineJournalReport.ShowDialog();

				lineJournalReport.Dispose();

				updateGrid(this, null);
			}
			else
			{
				MessageBox.Show("Fel", "Du måste välja en rutt.");
			}
			
		}


		private void LineJournalList_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timer.Enabled = false;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			updateGrid(this, null);
		}
	}
}
