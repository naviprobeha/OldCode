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
	public class ShipmentIds : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.DataGrid idGrid;
		private System.Windows.Forms.Label itemNoLabel;
		private System.Windows.Forms.DataGridTableStyle idTable;
		private System.Windows.Forms.DataGridTextBoxColumn unitIdCol;
		private System.Windows.Forms.Button button2;
		private DataShipmentLine dataShipmentLine;
		private DataShipmentHeader dataShipmentHeader;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.DataGridTextBoxColumn reMarkUnitIdCol;
		private System.Windows.Forms.DataGridTextBoxColumn bseCol;
		private System.Windows.Forms.DataGridTextBoxColumn postMortemCol;
		private DataSet shipmentLineIdDataSet;
	
		public ShipmentIds(SmartDatabase smartDatabase, DataShipmentHeader dataShipmentHeader, DataShipmentLine dataShipmentLine)
		{
			this.smartDatabase = smartDatabase;
			this.dataShipmentLine = dataShipmentLine;
			this.dataShipmentHeader = dataShipmentHeader;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoLabel.Text = itemNoLabel.Text + " " + dataShipmentLine.description;
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
			DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
			shipmentLineIdDataSet = dataShipmentLineIds.getShipmentLineDataSet(dataShipmentLine);

			idGrid.DataSource = shipmentLineIdDataSet.Tables[0];

			int i = 0;
			if (shipmentLineIdDataSet.Tables[0].Rows.Count > 0)
			{
				while(i < shipmentLineIdDataSet.Tables[0].Rows.Count)
				{
					if (shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() == "1") idGrid[i, 2] = "Ja";
					if (shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "1") idGrid[i, 3] = "Ja";

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
			this.reMarkUnitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
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
			this.idTable.GridColumnStyles.Add(this.reMarkUnitIdCol);
			this.idTable.GridColumnStyles.Add(this.bseCol);
			this.idTable.GridColumnStyles.Add(this.postMortemCol);
			this.idTable.MappingName = "shipmentLineId";
			// 
			// unitIdCol
			// 
			this.unitIdCol.HeaderText = "Identitetsnr";
			this.unitIdCol.MappingName = "unitId";
			this.unitIdCol.NullText = "";
			this.unitIdCol.Width = 100;
			// 
			// reMarkUnitIdCol
			// 
			this.reMarkUnitIdCol.HeaderText = "Reservm.";
			this.reMarkUnitIdCol.MappingName = "reMarkUnitId";
			this.reMarkUnitIdCol.NullText = "";
			this.reMarkUnitIdCol.Width = 100;
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
			this.button4.Click += new System.EventHandler(this.button4_Click_1);
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
			// ShipmentIds
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
				DataShipmentLineId dataShipmentLineId = new DataShipmentLineId(smartDatabase, int.Parse(shipmentLineIdDataSet.Tables[0].Rows[idGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				if ((dataShipmentLineId.bseTesting) || (dataShipmentLineId.postMortem))
				{
					string question = "";
					if (dataShipmentLineId.bseTesting) question = "ID:t som kommer att raderas är en provtagning. Är du säker?";
					if (dataShipmentLineId.postMortem) question = "ID:t som kommer att raderas är en obduktion. Är du säker?";
					if ((dataShipmentLineId.postMortem) && (dataShipmentLineId.postMortem)) question = "ID:t som kommer att raderas är en provtagning och en obduktion. Är du säker?";
					if (MessageBox.Show(question, "Varning", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						dataShipmentLineId.delete();
					}
				}
				else
				{
					dataShipmentLineId.delete();
				}
				updateGrid();
			}
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			
			string unitId = "SE "+dataShipmentHeader.productionSite;

			DataItem dataItem = new DataItem(smartDatabase, dataShipmentLine.itemNo);
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
				DataShipmentLineId dataShipmentLineId = new DataShipmentLineId(smartDatabase, dataShipmentLine);
				dataShipmentLineId.unitId = unitId;
				dataShipmentLineId.type = 0;
				dataShipmentLineId.commit();

				ShipmentId shipmentId = new ShipmentId(smartDatabase, dataShipmentLine, dataShipmentLineId);
				shipmentId.ShowDialog();
				if (shipmentId.getResult() == 0) dataShipmentLineId.delete();
				shipmentId.Dispose();

				updateGrid();
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			Keyboard keyboard = new Keyboard(20);
			keyboard.setStartTab(1);
			keyboard.ShowDialog();
			string unitId = keyboard.getInputString();
			keyboard.Dispose();

			if (unitId != "")
			{
				DataShipmentLineId dataShipmentLineId = new DataShipmentLineId(smartDatabase, dataShipmentLine);
				dataShipmentLineId.unitId = unitId;
				dataShipmentLineId.type = 1;
				dataShipmentLineId.commit();

				updateGrid();
			}

		}

		private void button4_Click_1(object sender, System.EventArgs e)
		{
			if (((DataTable)(idGrid.DataSource)).Rows.Count > 0)
			{
				DataShipmentLineId dataShipmentLineId = new DataShipmentLineId(smartDatabase, int.Parse(shipmentLineIdDataSet.Tables[0].Rows[idGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				
				ShipmentId shipmentId = new ShipmentId(smartDatabase, dataShipmentLine, dataShipmentLineId);
				shipmentId.ShowDialog();
				shipmentId.Dispose();

				updateGrid();
			}

		}
	}
}
