using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for OrderIds.
	/// </summary>
	public class OrderIds : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.DataGrid idGrid;
		private System.Windows.Forms.Label itemNoLabel;
		private System.Windows.Forms.DataGridTableStyle idTable;
		private System.Windows.Forms.DataGridTextBoxColumn unitIdCol;
		private System.Windows.Forms.Button button2;
		private DataOrderLine dataOrderLine;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.DataGridTextBoxColumn bseCol;
		private System.Windows.Forms.DataGridTextBoxColumn postMortemCol;
		private DataSet orderLineIdDataSet;
		private DataOrderHeader dataOrderHeader;
	
		public OrderIds(SmartDatabase smartDatabase, DataOrderHeader dataOrderHeader, DataOrderLine dataOrderLine)
		{
			this.smartDatabase = smartDatabase;
			this.dataOrderLine = dataOrderLine;
			this.dataOrderHeader = dataOrderHeader;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoLabel.Text = itemNoLabel.Text + " " + dataOrderLine.description;
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
			DataOrderLineIds dataOrderLineIds = new DataOrderLineIds(smartDatabase);
			orderLineIdDataSet = dataOrderLineIds.getOrderLineDataSet(dataOrderLine);

			idGrid.DataSource = orderLineIdDataSet.Tables[0];

			int i = 0;
			if (orderLineIdDataSet.Tables[0].Rows.Count > 0)
			{
				while(i < orderLineIdDataSet.Tables[0].Rows.Count)
				{
					if (orderLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1") idGrid[i, 1] = "Ja";
					if (orderLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() == "1") idGrid[i, 2] = "Ja";

					i++;
				}
			}

		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.idGrid = new System.Windows.Forms.DataGrid();
			this.idTable = new System.Windows.Forms.DataGridTableStyle();
			this.unitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.itemNoLabel = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.bseCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.postMortemCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// idGrid
			// 
			this.idGrid.Location = new System.Drawing.Point(8, 24);
			this.idGrid.Size = new System.Drawing.Size(304, 136);
			this.idGrid.TableStyles.Add(this.idTable);
			// 
			// idTable
			// 
			this.idTable.GridColumnStyles.Add(this.unitIdCol);
			this.idTable.GridColumnStyles.Add(this.bseCol);
			this.idTable.GridColumnStyles.Add(this.postMortemCol);
			this.idTable.MappingName = "shipmentLineId";
			// 
			// unitIdCol
			// 
			this.unitIdCol.HeaderText = "Identitetsnr";
			this.unitIdCol.MappingName = "unitId";
			this.unitIdCol.NullText = "";
			this.unitIdCol.Width = 200;
			// 
			// itemNoLabel
			// 
			this.itemNoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.itemNoLabel.Location = new System.Drawing.Point(5, 3);
			this.itemNoLabel.Size = new System.Drawing.Size(307, 20);
			this.itemNoLabel.Text = "Identiteter:";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(8, 168);
			this.button3.Size = new System.Drawing.Size(72, 40);
			this.button3.Text = "Nytt";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(168, 168);
			this.button1.Size = new System.Drawing.Size(72, 40);
			this.button1.Text = "Ta bort";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(248, 168);
			this.button2.Size = new System.Drawing.Size(64, 40);
			this.button2.Text = "Klar";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(88, 168);
			this.button4.Size = new System.Drawing.Size(72, 40);
			this.button4.Text = "Ändra";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// bseCol
			// 
			this.bseCol.HeaderText = "Provt.";
			this.bseCol.MappingName = "bseTestingText";
			this.bseCol.NullText = "";
			// 
			// postMortemCol
			// 
			this.postMortemCol.HeaderText = "Obd.";
			this.postMortemCol.MappingName = "postMortemText";
			this.postMortemCol.NullText = "";
			// 
			// OrderIds
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.itemNoLabel);
			this.Controls.Add(this.idGrid);
			this.Text = "Identiteter";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (((DataTable)(idGrid.DataSource)).Rows.Count > 0)
			{
				DataOrderLineId dataOrderLineId = new DataOrderLineId(smartDatabase, int.Parse(orderLineIdDataSet.Tables[0].Rows[idGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				dataOrderLineId.delete();
				updateGrid();
			}
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			string unitId = "SE "+dataOrderHeader.productionSite;

			DataItem dataItem = new DataItem(smartDatabase, dataOrderLine.itemNo);
			if (dataItem.idGroupCode == "1")
			{
				IdType1Pad idPad = new IdType1Pad();
				idPad.ShowDialog();
				unitId = idPad.getInputString();
				idPad.Dispose();
			}
			if (dataItem.idGroupCode == "")
			{
				Keyboard keyboard = new Keyboard(20);
				keyboard.setStartTab(1);
				keyboard.ShowDialog();
				unitId = keyboard.getInputString();
				keyboard.Dispose();
			}

			if (unitId != "")
			{
				DataOrderLineId dataOrderLineId = new DataOrderLineId(smartDatabase, dataOrderLine);
				dataOrderLineId.unitId = unitId;
				dataOrderLineId.commit();

				OrderId orderId = new OrderId(smartDatabase, dataOrderLine, dataOrderLineId);
				orderId.ShowDialog();
				if (orderId.getResult() == 0) dataOrderLineId.delete();
				orderId.Dispose();

				updateGrid();
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (((DataTable)(idGrid.DataSource)).Rows.Count > 0)
			{
				DataOrderLineId dataOrderLineId = new DataOrderLineId(smartDatabase, int.Parse(orderLineIdDataSet.Tables[0].Rows[idGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));

				OrderId orderId = new OrderId(smartDatabase, dataOrderLine, dataOrderLineId);
				orderId.ShowDialog();
				orderId.Dispose();

				updateGrid();
			}

		}
	}
}
