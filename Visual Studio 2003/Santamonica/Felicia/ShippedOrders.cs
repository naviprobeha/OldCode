using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ShipedOrders.
	/// </summary>
	public class ShippedOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.DataGridTextBoxColumn statusCol;
		private SmartDatabase smartDatabase;
		private DataSet shipOrderDataSet;
		private DataSet historyShipOrderDataSet;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.DataGrid shipOrderGrid;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.DataGrid historyShipOrderGrid;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridTableStyle historyShipOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn historyNameCol;
		private System.Windows.Forms.DataGridTextBoxColumn historyCityCol;
		private System.Windows.Forms.DataGridTextBoxColumn historyStatusCol;
		private System.Windows.Forms.DataGridTableStyle todayShipOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn todayNameCol;
		private System.Windows.Forms.DataGridTextBoxColumn todayCityCol;
		private System.Windows.Forms.DataGridTextBoxColumn todayStatusCol;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private Status agentStatus;
	
		public ShippedOrders(SmartDatabase smartDatabase, Status agentStatus)
		{
			this.smartDatabase = smartDatabase;
			this.agentStatus = agentStatus;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			updateGrid();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		private void updateGrid()
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
			shipOrderDataSet = dataShipOrders.getShippedDataSet(true);
			historyShipOrderDataSet = dataShipOrders.getShippedDataSet(false);
			

			int i = 0;
			while(i < shipOrderDataSet.Tables[0].Rows.Count)
			{
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "0")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Otilldelad";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Nej tack";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "2")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Kanske";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "3")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Uppköad";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "4")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Ny";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "5")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Ja tack";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "6")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Lastad";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "7")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Kanske";
				}
				if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "8")
				{
					DataRow row = shipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Nej tack";
				}
				shipOrderDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;
			}

			i = 0;
			while(i < historyShipOrderDataSet.Tables[0].Rows.Count)
			{
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "0")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Otilldelad";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Nej tack";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "2")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Kanske";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "3")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Uppköad";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "4")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Ny";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "5")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Ja tack";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "6")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Lastad";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "7")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Kanske";
				}
				if (historyShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "8")
				{
					DataRow row = historyShipOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "Nej tack";
				}
				historyShipOrderDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;
			}

			shipOrderGrid.DataSource = shipOrderDataSet.Tables[0];
			historyShipOrderGrid.DataSource = historyShipOrderDataSet.Tables[0];

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
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.statusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.shipOrderGrid = new System.Windows.Forms.DataGrid();
			this.todayShipOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.todayNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.todayCityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.todayStatusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.statusLabel = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.historyShipOrderGrid = new System.Windows.Forms.DataGrid();
			this.historyShipOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.historyNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.historyCityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.historyStatusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
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
			this.cityCol.Width = 100;
			// 
			// statusCol
			// 
			this.statusCol.HeaderText = "Status";
			this.statusCol.MappingName = "statusText";
			this.statusCol.NullText = "";
			this.statusCol.Width = 100;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 214);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button5);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.button9);
			this.tabPage1.Controls.Add(this.shipOrderGrid);
			this.tabPage1.Controls.Add(this.statusLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Dagens";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(96, 144);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(56, 144);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
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
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// shipOrderGrid
			// 
			this.shipOrderGrid.Location = new System.Drawing.Point(-3, 32);
			this.shipOrderGrid.Size = new System.Drawing.Size(320, 104);
			this.shipOrderGrid.TableStyles.Add(this.todayShipOrderTable);
			this.shipOrderGrid.Text = "dataGrid1";
			// 
			// todayShipOrderTable
			// 
			this.todayShipOrderTable.GridColumnStyles.Add(this.todayNameCol);
			this.todayShipOrderTable.GridColumnStyles.Add(this.todayCityCol);
			this.todayShipOrderTable.GridColumnStyles.Add(this.todayStatusCol);
			this.todayShipOrderTable.MappingName = "shipOrder";
			// 
			// todayNameCol
			// 
			this.todayNameCol.HeaderText = "Namn";
			this.todayNameCol.MappingName = "customerName";
			this.todayNameCol.NullText = "";
			this.todayNameCol.Width = 100;
			// 
			// todayCityCol
			// 
			this.todayCityCol.HeaderText = "Ort";
			this.todayCityCol.MappingName = "city";
			this.todayCityCol.NullText = "";
			this.todayCityCol.Width = 100;
			// 
			// todayStatusCol
			// 
			this.todayStatusCol.HeaderText = "Status";
			this.todayStatusCol.MappingName = "statusText";
			this.todayStatusCol.NullText = "";
			this.todayStatusCol.Width = 80;
			// 
			// statusLabel
			// 
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusLabel.Location = new System.Drawing.Point(5, 3);
			this.statusLabel.Size = new System.Drawing.Size(208, 20);
			this.statusLabel.Text = "Dagens lastade körorder";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button6);
			this.tabPage2.Controls.Add(this.button7);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.historyShipOrderGrid);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 188);
			this.tabPage2.Text = "Tidigare";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(96, 144);
			this.button6.Size = new System.Drawing.Size(32, 32);
			this.button6.Text = ">";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(56, 144);
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.Text = "<";
			this.button7.Click += new System.EventHandler(this.button7_Click);
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
			// historyShipOrderGrid
			// 
			this.historyShipOrderGrid.Location = new System.Drawing.Point(-3, 32);
			this.historyShipOrderGrid.Size = new System.Drawing.Size(320, 104);
			this.historyShipOrderGrid.TableStyles.Add(this.historyShipOrderTable);
			this.historyShipOrderGrid.Text = "dataGrid1";
			// 
			// historyShipOrderTable
			// 
			this.historyShipOrderTable.GridColumnStyles.Add(this.historyNameCol);
			this.historyShipOrderTable.GridColumnStyles.Add(this.historyCityCol);
			this.historyShipOrderTable.GridColumnStyles.Add(this.historyStatusCol);
			this.historyShipOrderTable.MappingName = "shipOrder";
			// 
			// historyNameCol
			// 
			this.historyNameCol.HeaderText = "Namn";
			this.historyNameCol.MappingName = "customerName";
			this.historyNameCol.NullText = "";
			this.historyNameCol.Width = 100;
			// 
			// historyCityCol
			// 
			this.historyCityCol.HeaderText = "Ort";
			this.historyCityCol.MappingName = "city";
			this.historyCityCol.NullText = "";
			this.historyCityCol.Width = 100;
			// 
			// historyStatusCol
			// 
			this.historyStatusCol.HeaderText = "Status";
			this.historyStatusCol.MappingName = "statusText";
			this.historyStatusCol.NullText = "";
			this.historyStatusCol.Width = 80;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(5, 3);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Tidigare lastade körorder";
			// 
			// ShippedOrders
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Lastade körorder";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (shipOrderGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(shipOrderDataSet.Tables[0].Rows[shipOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				ShipOrder shipOrder = new ShipOrder(smartDatabase, dataShipOrder, agentStatus);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipOrder.ShowDialog();
				shipOrder.Dispose();

				updateGrid();
			}
		
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (historyShipOrderGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(historyShipOrderDataSet.Tables[0].Rows[historyShipOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				ShipOrder shipOrder = new ShipOrder(smartDatabase, dataShipOrder, agentStatus);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipOrder.ShowDialog();
				shipOrder.Dispose();

				updateGrid();
			}		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.shipOrderGrid.DataSource != null) && (((DataTable)this.shipOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.shipOrderGrid.CurrentRowIndex > 0)
				{
					this.shipOrderGrid.CurrentRowIndex = this.shipOrderGrid.CurrentRowIndex - 1;
					this.shipOrderGrid.Select(this.shipOrderGrid.CurrentRowIndex);
				}
			}
		
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.shipOrderGrid.DataSource != null) && (((DataTable)this.shipOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.shipOrderGrid.CurrentRowIndex < (((DataTable)this.shipOrderGrid.DataSource).Rows.Count -1))
				{
					this.shipOrderGrid.CurrentRowIndex = this.shipOrderGrid.CurrentRowIndex + 1;
					this.shipOrderGrid.Select(this.shipOrderGrid.CurrentRowIndex);
				}
			}
		
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			if ((this.historyShipOrderGrid.DataSource != null) && (((DataTable)this.historyShipOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.historyShipOrderGrid.CurrentRowIndex > 0)
				{
					this.historyShipOrderGrid.CurrentRowIndex = this.historyShipOrderGrid.CurrentRowIndex - 1;
					this.historyShipOrderGrid.Select(this.historyShipOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if ((this.historyShipOrderGrid.DataSource != null) && (((DataTable)this.historyShipOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.historyShipOrderGrid.CurrentRowIndex < (((DataTable)this.historyShipOrderGrid.DataSource).Rows.Count -1))
				{
					this.historyShipOrderGrid.CurrentRowIndex = this.historyShipOrderGrid.CurrentRowIndex + 1;
					this.historyShipOrderGrid.Select(this.historyShipOrderGrid.CurrentRowIndex);
				}
			}

		}
	}
}
