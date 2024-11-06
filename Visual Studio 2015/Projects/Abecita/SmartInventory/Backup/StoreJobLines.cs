using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for StoreJobs.
	/// </summary>
	public class StoreJobLines : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.DataGrid jobLineGrid;
		private System.Windows.Forms.DataGridTableStyle jobLineTable;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGridTextBoxColumn handleUnitIdCol;
		private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn zoneCol;
		private System.Windows.Forms.DataGridTextBoxColumn freqCol;
		private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;

		private DataWhseActivityHeader dataWhseActivityHeader;

		public StoreJobLines(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader)
		{
			this.smartDatabase = smartDatabase;
			this.dataWhseActivityHeader = dataWhseActivityHeader;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.label1.Text = this.label1.Text + " " + dataWhseActivityHeader.no;
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
			this.jobLineGrid = new System.Windows.Forms.DataGrid();
			this.jobLineTable = new System.Windows.Forms.DataGridTableStyle();
			this.label1 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.handleUnitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.zoneCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.freqCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// jobLineGrid
			// 
			this.jobLineGrid.Location = new System.Drawing.Point(0, 32);
			this.jobLineGrid.Size = new System.Drawing.Size(240, 208);
			this.jobLineGrid.TableStyles.Add(this.jobLineTable);
			this.jobLineGrid.Text = "jobGrid";
			// 
			// jobLineTable
			// 
			this.jobLineTable.GridColumnStyles.Add(this.itemNoCol);
			this.jobLineTable.GridColumnStyles.Add(this.quantityCol);
			this.jobLineTable.GridColumnStyles.Add(this.handleUnitIdCol);
			this.jobLineTable.GridColumnStyles.Add(this.locationCodeCol);
			this.jobLineTable.GridColumnStyles.Add(this.zoneCol);
			this.jobLineTable.GridColumnStyles.Add(this.freqCol);
			this.jobLineTable.GridColumnStyles.Add(this.binCodeCol);
			this.jobLineTable.MappingName = "whseActivityLine";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(152, 20);
			this.label1.Text = "Uppdrag:";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.Text = "Visa";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Rader";
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "itemNo";
			this.itemNoCol.NullText = "";
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// handleUnitIdCol
			// 
			this.handleUnitIdCol.HeaderText = "Hanteringsenhet";
			this.handleUnitIdCol.MappingName = "handleUnitId";
			this.handleUnitIdCol.NullText = "";
			// 
			// locationCodeCol
			// 
			this.locationCodeCol.HeaderText = "Lagerställe";
			this.locationCodeCol.MappingName = "locationCode";
			this.locationCodeCol.NullText = "";
			// 
			// zoneCol
			// 
			this.zoneCol.HeaderText = "Zon";
			this.zoneCol.MappingName = "zone";
			this.zoneCol.NullText = "";
			// 
			// freqCol
			// 
			this.freqCol.HeaderText = "Frekvens";
			this.freqCol.MappingName = "freq";
			this.freqCol.NullText = "";
			// 
			// binCodeCol
			// 
			this.binCodeCol.HeaderText = "Lagerplats";
			this.binCodeCol.MappingName = "binCode";
			this.binCodeCol.NullText = "";
			// 
			// StoreJobLines
			// 
			this.Controls.Add(this.jobLineGrid);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Inlagringsuppdrag";
			this.Load += new System.EventHandler(this.StoreJobs_Load);

		}
		#endregion

		private void StoreJobs_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void updateGrid()
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
			DataSet dataSet = dataWhseActivityLines.getDataSet(dataWhseActivityHeader);

			jobLineGrid.DataSource = dataSet.Tables[0];

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		}

	}
}
