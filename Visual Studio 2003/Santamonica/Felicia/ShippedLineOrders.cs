using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ShippedLineOrders.
	/// </summary>
	public class ShippedLineOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid lineOrderGrid;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridTableStyle lineOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.Label label9;
	
		private SmartDatabase smartDatabase;
		private Status agentStatus;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private DataSet lineOrderDataSet;

		public ShippedLineOrders(SmartDatabase smartDatabase, Status agentStatus)
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

			DataLineOrders dataLineOrders = new DataLineOrders(smartDatabase);
			lineOrderDataSet = dataLineOrders.getHistoryDataSet();

			int i = 0;
			while(i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				DataRow row = lineOrderDataSet.Tables[0].Rows[i];
				row["qtyContainers"] = dataLineOrders.countContainers(int.Parse(lineOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())).ToString();

				lineOrderDataSet.Tables[0].Rows[i].AcceptChanges();
				i++;

			}


			lineOrderGrid.DataSource = lineOrderDataSet.Tables[0];

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
			this.label9 = new System.Windows.Forms.Label();
			this.lineOrderGrid = new System.Windows.Forms.DataGrid();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.lineOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Lastade linjeorder";
			// 
			// lineOrderGrid
			// 
			this.lineOrderGrid.Location = new System.Drawing.Point(8, 32);
			this.lineOrderGrid.Size = new System.Drawing.Size(304, 128);
			this.lineOrderGrid.TableStyles.Add(this.lineOrderTable);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 168);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Stäng";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(160, 168);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Visa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lineOrderTable
			// 
			this.lineOrderTable.GridColumnStyles.Add(this.noCol);
			this.lineOrderTable.GridColumnStyles.Add(this.nameCol);
			this.lineOrderTable.GridColumnStyles.Add(this.cityCol);
			this.lineOrderTable.MappingName = "lineOrder";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "shippingCustomerName";
			this.nameCol.NullText = "";
			this.nameCol.Width = 130;
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			this.cityCol.Width = 100;
			// 
			// noCol
			// 
			this.noCol.HeaderText = "Nr";
			this.noCol.MappingName = "entryNo";
			this.noCol.NullText = "";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(120, 168);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(80, 168);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// ShippedLineOrders
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lineOrderGrid);
			this.Controls.Add(this.label9);
			this.Text = "Lastade linjeorder";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (lineOrderGrid.CurrentRowIndex > -1)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataLineOrder dataLineOrder = new DataLineOrder(smartDatabase, int.Parse(lineOrderDataSet.Tables[0].Rows[lineOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				LineOrder lineOrder = new LineOrder(smartDatabase, dataLineOrder, agentStatus);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				lineOrder.ShowDialog();
				lineOrder.Dispose();

				updateGrid();
			}
		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.lineOrderGrid.DataSource != null) && (((DataTable)this.lineOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.lineOrderGrid.CurrentRowIndex > 0)
				{
					this.lineOrderGrid.CurrentRowIndex = this.lineOrderGrid.CurrentRowIndex - 1;
					this.lineOrderGrid.Select(this.lineOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.lineOrderGrid.DataSource != null) && (((DataTable)this.lineOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.lineOrderGrid.CurrentRowIndex < (((DataTable)this.lineOrderGrid.DataSource).Rows.Count -1))
				{
					this.lineOrderGrid.CurrentRowIndex = this.lineOrderGrid.CurrentRowIndex + 1;
					this.lineOrderGrid.Select(this.lineOrderGrid.CurrentRowIndex);
				}
			}

		}
	}
}
