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
	public class ShippedFactoryOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label9;
	
		private SmartDatabase smartDatabase;
		private Status agentStatus;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.DataGrid factoryOrderGrid;
		private System.Windows.Forms.DataGridTableStyle factoryOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn dateCol;
		private System.Windows.Forms.DataGridTextBoxColumn factoryNameCol;
		private System.Windows.Forms.DataGridTextBoxColumn consumerNameCol;
		private DataSet factoryOrderDataSet;

		public ShippedFactoryOrders(SmartDatabase smartDatabase, Status agentStatus)
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

			DataFactoryOrders dataFactoryOrders = new DataFactoryOrders(smartDatabase);
			factoryOrderDataSet = dataFactoryOrders.getHistoryDataSet();


			factoryOrderGrid.DataSource = factoryOrderDataSet.Tables[0];

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
			this.factoryOrderGrid = new System.Windows.Forms.DataGrid();
			this.factoryOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.dateCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.factoryNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.consumerNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Körda biomalorder";
			// 
			// factoryOrderGrid
			// 
			this.factoryOrderGrid.Location = new System.Drawing.Point(8, 32);
			this.factoryOrderGrid.Size = new System.Drawing.Size(304, 128);
			this.factoryOrderGrid.TableStyles.Add(this.factoryOrderTable);
			this.factoryOrderGrid.Click += new System.EventHandler(this.factoryOrderGrid_Click);
			// 
			// factoryOrderTable
			// 
			this.factoryOrderTable.GridColumnStyles.Add(this.dateCol);
			this.factoryOrderTable.GridColumnStyles.Add(this.factoryNameCol);
			this.factoryOrderTable.GridColumnStyles.Add(this.consumerNameCol);
			this.factoryOrderTable.MappingName = "factoryOrder";
			// 
			// dateCol
			// 
			this.dateCol.HeaderText = "Datum";
			this.dateCol.MappingName = "shipDate";
			this.dateCol.NullText = "";
			this.dateCol.Width = 70;
			// 
			// factoryNameCol
			// 
			this.factoryNameCol.HeaderText = "Från";
			this.factoryNameCol.MappingName = "factoryName";
			this.factoryNameCol.NullText = "";
			this.factoryNameCol.Width = 120;
			// 
			// consumerNameCol
			// 
			this.consumerNameCol.HeaderText = "Till";
			this.consumerNameCol.MappingName = "consumerName";
			this.consumerNameCol.NullText = "";
			this.consumerNameCol.Width = 110;
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
			// ShippedFactoryOrders
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.factoryOrderGrid);
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
			if (factoryOrderDataSet.Tables[0].Rows.Count > 0)
			{
				if (factoryOrderGrid.CurrentRowIndex > -1)
				{
					Cursor.Current = Cursors.WaitCursor;
					Cursor.Show();

					DataFactoryOrder dataFactoryOrder = new DataFactoryOrder(smartDatabase, int.Parse(factoryOrderDataSet.Tables[0].Rows[factoryOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
					FactoryOrder factoryOrder = new FactoryOrder(smartDatabase, dataFactoryOrder, agentStatus);

					Cursor.Current = Cursors.Default;
					Cursor.Hide();

					factoryOrder.ShowDialog();
					factoryOrder.Dispose();

					updateGrid();
				}
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.factoryOrderGrid.DataSource != null) && (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.factoryOrderGrid.CurrentRowIndex > 0)
				{
					this.factoryOrderGrid.CurrentRowIndex = this.factoryOrderGrid.CurrentRowIndex - 1;
					this.factoryOrderGrid.Select(this.factoryOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.factoryOrderGrid.DataSource != null) && (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.factoryOrderGrid.CurrentRowIndex < (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count -1))
				{
					this.factoryOrderGrid.CurrentRowIndex = this.factoryOrderGrid.CurrentRowIndex + 1;
					this.factoryOrderGrid.Select(this.factoryOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void factoryOrderGrid_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
