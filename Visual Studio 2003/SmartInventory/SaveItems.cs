using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for SaveJobs.
	/// </summary>
	public class SaveItems : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox heIdBox;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGridTableStyle jobLineTable;
		private System.Windows.Forms.DataGrid lineGrid;

		private SmartDatabase smartDatabase;
		private DataWhseActivityHeader dataWhseActivityHeader;
		private System.Windows.Forms.DataGridTextBoxColumn handleUnitIdCol;
		private int freq;

		public SaveItems(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader, int freq)
		{
			this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;
			this.freq = freq;
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
			heIdBox.Focus();
		}


		private void updateGrid()
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
			DataSet dataSet = dataWhseActivityLines.getJobDataSet(this.dataWhseActivityHeader, this.freq, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);

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
			this.panel3 = new System.Windows.Forms.Panel();
			this.heIdBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.lineGrid = new System.Windows.Forms.DataGrid();
			this.jobLineTable = new System.Windows.Forms.DataGridTableStyle();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.handleUnitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.heIdBox);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Location = new System.Drawing.Point(0, 167);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// heIdBox
			// 
			this.heIdBox.Location = new System.Drawing.Point(16, 29);
			this.heIdBox.Size = new System.Drawing.Size(208, 20);
			this.heIdBox.Text = "";
			this.heIdBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.heIdBox_KeyPress);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(16, 8);
			this.label5.Size = new System.Drawing.Size(208, 20);
			this.label5.Text = "Scanna HE ID:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Size = new System.Drawing.Size(208, 20);
			this.label2.Text = "Frekvensklass:";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 231);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// lineGrid
			// 
			this.lineGrid.Location = new System.Drawing.Point(0, 47);
			this.lineGrid.Size = new System.Drawing.Size(240, 120);
			this.lineGrid.TableStyles.Add(this.jobLineTable);
			// 
			// jobLineTable
			// 
			this.jobLineTable.GridColumnStyles.Add(this.handleUnitIdCol);
			this.jobLineTable.GridColumnStyles.Add(this.itemNoCol);
			this.jobLineTable.GridColumnStyles.Add(this.quantityCol);
			this.jobLineTable.MappingName = "whseActivityLine";
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "itemNo";
			this.itemNoCol.NullText = "";
			this.itemNoCol.Width = 75;
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			this.quantityCol.Width = 75;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Inlagring - Batch:";
			// 
			// handleUnitIdCol
			// 
			this.handleUnitIdCol.HeaderText = "Hanteringsenhet";
			this.handleUnitIdCol.MappingName = "handleUnitId";
			this.handleUnitIdCol.NullText = "";
			this.handleUnitIdCol.Width = 100;
			// 
			// SaveItems
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.lineGrid);
			this.Controls.Add(this.label1);
			this.Text = "Påfyllning - Inlagring";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    
		}

		private void heIdBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				DataItemUnit dataItemUnit = new DataItemUnit(heIdBox.Text);

				DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, DataWhseActivityLines.WHSE_ACTION_PLACE, dataItemUnit, smartDatabase);
				if (dataWhseActivityLine.exists)
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_OK);
					
					SaveItem saveItem = new SaveItem(smartDatabase, freq, dataWhseActivityLine);
					saveItem.ShowDialog();

					heIdBox.Text = "";
					updateGrid();
					heIdBox.Focus();
				}
				else
				{
					heIdBox.Text = "";
					Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
				}
			}
			
		}
	}
}
