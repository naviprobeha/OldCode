using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Orders.
	/// </summary>
	public class Shipments : System.Windows.Forms.Form
	{
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.DataGrid shipmentGrid;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid readyGrid;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label2;
		private DataSet shipmentDataSet;
		private DataSet readyDataSet;
		private System.Windows.Forms.TabControl tab;
		private System.Windows.Forms.DataGrid sentGrid;
		private System.Windows.Forms.DataGridTableStyle readyTable;
		private System.Windows.Forms.DataGridTextBoxColumn noReadyCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameReadyCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityReadyCol;
		private System.Windows.Forms.DataGridTableStyle shipmentTable;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.DataGridTextBoxColumn shipDateCol;
		private System.Windows.Forms.DataGridTextBoxColumn shipDateReadyCol;
		private System.Windows.Forms.DataGridTableStyle sentTable;
		private System.Windows.Forms.DataGridTextBoxColumn noSentCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameSentCol;
		private System.Windows.Forms.DataGridTextBoxColumn citySentCol;
		private System.Windows.Forms.DataGridTextBoxColumn shipDateSentCol;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private DataSet sentDataSet;
		private Status agentStatus;

		public Shipments(SmartDatabase smartDatabase, Status agentStatus)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.smartDatabase = smartDatabase;
			this.agentStatus = agentStatus;
			updateGrid();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		public void updateGrid()
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			DataShipmentHeaders dataShipmentHeaders = new DataShipmentHeaders(smartDatabase);
			shipmentDataSet = dataShipmentHeaders.getDataSet(0);
			readyDataSet = dataShipmentHeaders.getDataSet(1);
			sentDataSet = dataShipmentHeaders.getDataSet(2);

			int i = 0;
			while(i < shipmentDataSet.Tables[0].Rows.Count)
			{
				DataRow row = shipmentDataSet.Tables[0].Rows[i];
				row["no"] = smartDatabase.getSetup().agentId +"-"+ shipmentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
				
				shipmentDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;
			}

			i = 0;
			while(i < readyDataSet.Tables[0].Rows.Count)
			{
				DataRow row = readyDataSet.Tables[0].Rows[i];
				row["no"] = smartDatabase.getSetup().agentId +"-"+ readyDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
				
				readyDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;
			}

			i = 0;
			while(i < sentDataSet.Tables[0].Rows.Count)
			{
				DataRow row = sentDataSet.Tables[0].Rows[i];
				row["no"] = smartDatabase.getSetup().agentId +"-"+ sentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
				
				sentDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;
			}

			shipmentGrid.DataSource = shipmentDataSet.Tables[0];
			readyGrid.DataSource = readyDataSet.Tables[0];
			sentGrid.DataSource = sentDataSet.Tables[0];
			

			Cursor.Current = Cursors.Default;
			Cursor.Hide();

		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tab = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button6 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.shipmentGrid = new System.Windows.Forms.DataGrid();
			this.shipmentTable = new System.Windows.Forms.DataGridTableStyle();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.shipDateCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.readyGrid = new System.Windows.Forms.DataGrid();
			this.readyTable = new System.Windows.Forms.DataGridTableStyle();
			this.noReadyCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameReadyCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityReadyCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.shipDateReadyCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.sentGrid = new System.Windows.Forms.DataGrid();
			this.sentTable = new System.Windows.Forms.DataGridTableStyle();
			this.noSentCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameSentCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.citySentCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.shipDateSentCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// tab
			// 
			this.tab.Controls.Add(this.tabPage1);
			this.tab.Controls.Add(this.tabPage2);
			this.tab.Controls.Add(this.tabPage3);
			this.tab.SelectedIndex = 0;
			this.tab.Size = new System.Drawing.Size(322, 214);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button6);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.button9);
			this.tabPage1.Controls.Add(this.statusLabel);
			this.tabPage1.Controls.Add(this.shipmentGrid);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Under bearbetning";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(8, 144);
			this.button6.Size = new System.Drawing.Size(120, 32);
			this.button6.Text = "Periodrapport";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(136, 144);
			this.button1.Size = new System.Drawing.Size(80, 32);
			this.button1.Text = "Visa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(224, 144);
			this.button9.Size = new System.Drawing.Size(80, 32);
			this.button9.Text = "Tillbaka";
			this.button9.Click += new System.EventHandler(this.button9_Click_1);
			// 
			// statusLabel
			// 
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusLabel.Location = new System.Drawing.Point(5, 3);
			this.statusLabel.Size = new System.Drawing.Size(208, 20);
			this.statusLabel.Text = "Registrerade följesedlar";
			// 
			// shipmentGrid
			// 
			this.shipmentGrid.Location = new System.Drawing.Point(-3, 32);
			this.shipmentGrid.Size = new System.Drawing.Size(320, 104);
			this.shipmentGrid.TableStyles.Add(this.shipmentTable);
			// 
			// shipmentTable
			// 
			this.shipmentTable.GridColumnStyles.Add(this.noCol);
			this.shipmentTable.GridColumnStyles.Add(this.nameCol);
			this.shipmentTable.GridColumnStyles.Add(this.cityCol);
			this.shipmentTable.GridColumnStyles.Add(this.shipDateCol);
			this.shipmentTable.MappingName = "shipmentHeader";
			// 
			// noCol
			// 
			this.noCol.HeaderText = "Nr";
			this.noCol.MappingName = "no";
			this.noCol.NullText = "";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "customerName";
			this.nameCol.NullText = "";
			this.nameCol.Width = 100;
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			// 
			// shipDateCol
			// 
			this.shipDateCol.HeaderText = "Datum";
			this.shipDateCol.MappingName = "shipDate";
			this.shipDateCol.NullText = "";
			this.shipDateCol.Width = 100;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button10);
			this.tabPage2.Controls.Add(this.button11);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.readyGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 188);
			this.tabPage2.Text = "Klara att skicka";
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button10.Location = new System.Drawing.Point(96, 144);
			this.button10.Size = new System.Drawing.Size(32, 32);
			this.button10.Text = ">";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button11.Location = new System.Drawing.Point(56, 144);
			this.button11.Size = new System.Drawing.Size(32, 32);
			this.button11.Text = "<";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(136, 144);
			this.button2.Size = new System.Drawing.Size(80, 32);
			this.button2.Text = "Visa";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(224, 144);
			this.button3.Size = new System.Drawing.Size(80, 32);
			this.button3.Text = "Tillbaka";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(5, 3);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Registrerade följesedlar";
			// 
			// readyGrid
			// 
			this.readyGrid.Location = new System.Drawing.Point(-3, 32);
			this.readyGrid.Size = new System.Drawing.Size(320, 104);
			this.readyGrid.TableStyles.Add(this.readyTable);
			// 
			// readyTable
			// 
			this.readyTable.GridColumnStyles.Add(this.noReadyCol);
			this.readyTable.GridColumnStyles.Add(this.nameReadyCol);
			this.readyTable.GridColumnStyles.Add(this.cityReadyCol);
			this.readyTable.GridColumnStyles.Add(this.shipDateReadyCol);
			this.readyTable.MappingName = "shipmentHeader";
			// 
			// noReadyCol
			// 
			this.noReadyCol.HeaderText = "Nr";
			this.noReadyCol.MappingName = "no";
			this.noReadyCol.NullText = "";
			// 
			// nameReadyCol
			// 
			this.nameReadyCol.HeaderText = "Namn";
			this.nameReadyCol.MappingName = "customerName";
			this.nameReadyCol.NullText = "";
			this.nameReadyCol.Width = 100;
			// 
			// cityReadyCol
			// 
			this.cityReadyCol.HeaderText = "Ort";
			this.cityReadyCol.MappingName = "city";
			this.cityReadyCol.NullText = "";
			this.cityReadyCol.Width = 100;
			// 
			// shipDateReadyCol
			// 
			this.shipDateReadyCol.HeaderText = "Datum";
			this.shipDateReadyCol.MappingName = "shipDate";
			this.shipDateReadyCol.NullText = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.button7);
			this.tabPage3.Controls.Add(this.button8);
			this.tabPage3.Controls.Add(this.button4);
			this.tabPage3.Controls.Add(this.button5);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.sentGrid);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 188);
			this.tabPage3.Text = "Skickade";
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(96, 144);
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.Text = ">";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(56, 144);
			this.button8.Size = new System.Drawing.Size(32, 32);
			this.button8.Text = "<";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(136, 144);
			this.button4.Size = new System.Drawing.Size(80, 32);
			this.button4.Text = "Visa";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(224, 144);
			this.button5.Size = new System.Drawing.Size(80, 32);
			this.button5.Text = "Tillbaka";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(5, 3);
			this.label2.Size = new System.Drawing.Size(208, 20);
			this.label2.Text = "Registrerade följesedlar";
			// 
			// sentGrid
			// 
			this.sentGrid.Location = new System.Drawing.Point(-3, 32);
			this.sentGrid.Size = new System.Drawing.Size(320, 104);
			this.sentGrid.TableStyles.Add(this.sentTable);
			// 
			// sentTable
			// 
			this.sentTable.GridColumnStyles.Add(this.noSentCol);
			this.sentTable.GridColumnStyles.Add(this.nameSentCol);
			this.sentTable.GridColumnStyles.Add(this.citySentCol);
			this.sentTable.GridColumnStyles.Add(this.shipDateSentCol);
			this.sentTable.MappingName = "shipmentHeader";
			// 
			// noSentCol
			// 
			this.noSentCol.HeaderText = "Nr";
			this.noSentCol.MappingName = "no";
			this.noSentCol.NullText = "";
			// 
			// nameSentCol
			// 
			this.nameSentCol.HeaderText = "Namn";
			this.nameSentCol.MappingName = "customerName";
			this.nameSentCol.NullText = "";
			this.nameSentCol.Width = 100;
			// 
			// citySentCol
			// 
			this.citySentCol.HeaderText = "Ort";
			this.citySentCol.MappingName = "city";
			this.citySentCol.NullText = "";
			this.citySentCol.Width = 100;
			// 
			// shipDateSentCol
			// 
			this.shipDateSentCol.HeaderText = "Datum";
			this.shipDateSentCol.MappingName = "shipDate";
			this.shipDateSentCol.NullText = "";
			// 
			// Shipments
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tab);
			this.Text = "Orders";

		}
		#endregion

		private void button9_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void shipmentGrid_Click(object sender, System.EventArgs e)
		{
		

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (shipmentGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, int.Parse(shipmentDataSet.Tables[0].Rows[shipmentGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				Shipment shipment = new Shipment(smartDatabase, dataShipmentHeader, agentStatus, true);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipment.ShowDialog();
				shipment.Dispose();

				updateGrid();
			}
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (readyGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, int.Parse(readyDataSet.Tables[0].Rows[readyGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				Shipment shipment = new Shipment(smartDatabase, dataShipmentHeader, agentStatus, true);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipment.ShowDialog();
				shipment.Dispose();

				updateGrid();
			}
		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (sentGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, int.Parse(sentDataSet.Tables[0].Rows[sentGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				Shipment shipment = new Shipment(smartDatabase, dataShipmentHeader, agentStatus, true);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipment.ShowDialog();
				shipment.Dispose();

				updateGrid();
			}
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button9_Click_1(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			PeriodReport periodReport = new PeriodReport(smartDatabase);
			periodReport.ShowDialog();
			periodReport.Dispose();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			if ((this.sentGrid.DataSource != null) && (((DataTable)this.sentGrid.DataSource).Rows.Count > 0))
			{
				if (this.sentGrid.CurrentRowIndex < (((DataTable)this.sentGrid.DataSource).Rows.Count -1))
				{
					this.sentGrid.CurrentRowIndex = this.sentGrid.CurrentRowIndex + 1;
					this.sentGrid.Select(this.sentGrid.CurrentRowIndex);
				}
			}

		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			if ((this.sentGrid.DataSource != null) && (((DataTable)this.sentGrid.DataSource).Rows.Count > 0))
			{
				if (this.sentGrid.CurrentRowIndex > 0)
				{
					this.sentGrid.CurrentRowIndex = this.sentGrid.CurrentRowIndex - 1;
					this.sentGrid.Select(this.sentGrid.CurrentRowIndex);
				}
			}

		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			if ((this.readyGrid.DataSource != null) && (((DataTable)this.readyGrid.DataSource).Rows.Count > 0))
			{
				if (this.readyGrid.CurrentRowIndex < (((DataTable)this.readyGrid.DataSource).Rows.Count -1))
				{
					this.readyGrid.CurrentRowIndex = this.readyGrid.CurrentRowIndex + 1;
					this.readyGrid.Select(this.readyGrid.CurrentRowIndex);
				}
			}

		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			if ((this.readyGrid.DataSource != null) && (((DataTable)this.readyGrid.DataSource).Rows.Count > 0))
			{
				if (this.readyGrid.CurrentRowIndex > 0)
				{
					this.readyGrid.CurrentRowIndex = this.readyGrid.CurrentRowIndex - 1;
					this.readyGrid.Select(this.readyGrid.CurrentRowIndex);
				}
			}


		}


	}
}
