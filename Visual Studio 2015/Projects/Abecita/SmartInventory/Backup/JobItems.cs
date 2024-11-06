using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for PlanStoreJob.
	/// </summary>
	public class JobItems : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scanLocationBox;
		private System.Windows.Forms.Label label1;
	
		private System.Windows.Forms.DataGrid lineGrid;
		private System.Windows.Forms.DataGridTableStyle lineTable;
		private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;

		private SmartDatabase smartDatabase;
		private DataWhseActivityHeader dataWhseActivityHeader;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.TextBox scanBinBox;
		private int freq;
		private DataSetup dataSetup;

		public JobItems(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader, int freq)
		{
			this.smartDatabase = smartDatabase;
			this.dataWhseActivityHeader = dataWhseActivityHeader;
			this.freq = freq;
			this.dataSetup = new DataSetup(smartDatabase);

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			label1.Text = label1.Text + " " + dataWhseActivityHeader.no;

			switch (freq)
			{
				case 1: 
				{
					label2.Text = "Frekvensklass: Högfrekvent";
					break;
				}
				case 2: 
				{
					label2.Text = "Frekvensklass: Mellanfrekvent";
					break;
				}
				case 3: 
				{
					label2.Text = "Frekvensklass: Lågfrekvent";
					break;
				}
				case 4: 
				{
					label2.Text = "Frekvensklass: Mkt lågfrekvent";
					break;
				}
				case 5: 
				{
					label2.Text = "Frekvensklass: Storvolymigt";
					break;
				}
			}

			updateGrid();
			this.scanBinBox.Focus();
		}

		private void updateGrid()
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
			DataSet dataSet = dataWhseActivityLines.getJobDataSet(this.dataWhseActivityHeader, this.freq, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);

			lineGrid.DataSource = dataSet.Tables[0];

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
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
			this.button2 = new System.Windows.Forms.Button();
			this.lineGrid = new System.Windows.Forms.DataGrid();
			this.lineTable = new System.Windows.Forms.DataGridTableStyle();
			this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.scanBinBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.scanLocationBox = new System.Windows.Forms.TextBox();
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 232);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// lineGrid
			// 
			this.lineGrid.Location = new System.Drawing.Point(0, 48);
			this.lineGrid.Size = new System.Drawing.Size(240, 120);
			this.lineGrid.TableStyles.Add(this.lineTable);
			this.lineGrid.Text = "jobGrid";
			// 
			// lineTable
			// 
			this.lineTable.GridColumnStyles.Add(this.locationCodeCol);
			this.lineTable.GridColumnStyles.Add(this.binCodeCol);
			this.lineTable.GridColumnStyles.Add(this.itemNoCol);
			this.lineTable.GridColumnStyles.Add(this.quantityCol);
			this.lineTable.MappingName = "whseActivityLine";
			// 
			// locationCodeCol
			// 
			this.locationCodeCol.HeaderText = "Lagerställe";
			this.locationCodeCol.MappingName = "locationCode";
			this.locationCodeCol.NullText = "";
			// 
			// binCodeCol
			// 
			this.binCodeCol.HeaderText = "Lagerplats";
			this.binCodeCol.MappingName = "binCode";
			this.binCodeCol.NullText = "";
			this.binCodeCol.Width = 100;
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "itemNo";
			this.itemNoCol.NullText = "";
			this.itemNoCol.Width = 100;
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Uttag - Batch:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Size = new System.Drawing.Size(208, 20);
			this.label2.Text = "Frekvensklass:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.scanBinBox);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Location = new System.Drawing.Point(0, 168);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// scanBinBox
			// 
			this.scanBinBox.Location = new System.Drawing.Point(16, 29);
			this.scanBinBox.Size = new System.Drawing.Size(208, 20);
			this.scanBinBox.Text = "";
			this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(16, 8);
			this.label5.Size = new System.Drawing.Size(208, 20);
			this.label5.Text = "Scanna hanteringsenhet:";
			// 
			// scanLocationBox
			// 
			this.scanLocationBox.Text = "";
			// 
			// JobItems
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.lineGrid);
			this.Controls.Add(this.label1);
			this.Text = "Påfyllning - Uttag";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				//DataBin dataBin = new DataBin(dataSetup.locationCode, this.scanBinBox.Text);
				DataItemUnit dataItemUnit = new DataItemUnit(scanBinBox.Text);
				//DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, dataBin, smartDatabase);
				DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, 0, dataItemUnit, smartDatabase);
				if (dataWhseActivityLine.exists)
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
					
					DataWhseActivityLine dataWhseActivityPlaceLine = new DataWhseActivityLine(dataWhseActivityLine, smartDatabase);

					dataWhseActivityLine.status = 1;
					dataWhseActivityPlaceLine.status = 1;

					dataWhseActivityLine.commit();
					dataWhseActivityPlaceLine.commit();

					this.scanBinBox.Text = "";
					updateGrid();

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
					this.scanBinBox.Text = "";
				}
			}
		}
	}
}
