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
	public class Bins : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.DataGrid binGrid;
		private System.Windows.Forms.DataGridTableStyle binTable;
		private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn codeCol;
		private System.Windows.Forms.DataGridTextBoxColumn zoneCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn blockingCol;
		private System.Windows.Forms.DataGridTextBoxColumn emptyCol;

		public Bins(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.binGrid = new System.Windows.Forms.DataGrid();
			this.binTable = new System.Windows.Forms.DataGridTableStyle();
			this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.zoneCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.blockingCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.emptyCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// binGrid
			// 
			this.binGrid.Location = new System.Drawing.Point(0, 32);
			this.binGrid.Size = new System.Drawing.Size(240, 208);
			this.binGrid.TableStyles.Add(this.binTable);
			this.binGrid.Text = "binGrid";
			this.binGrid.Click += new System.EventHandler(this.jobGrid_Click);
			// 
			// binTable
			// 
			this.binTable.GridColumnStyles.Add(this.locationCodeCol);
			this.binTable.GridColumnStyles.Add(this.codeCol);
			this.binTable.GridColumnStyles.Add(this.zoneCodeCol);
			this.binTable.GridColumnStyles.Add(this.blockingCol);
			this.binTable.GridColumnStyles.Add(this.emptyCol);
			this.binTable.MappingName = "bin";
			// 
			// locationCodeCol
			// 
			this.locationCodeCol.HeaderText = "Lagerplats";
			this.locationCodeCol.MappingName = "locationCode";
			this.locationCodeCol.NullText = "";
			// 
			// codeCol
			// 
			this.codeCol.HeaderText = "Kod";
			this.codeCol.MappingName = "code";
			this.codeCol.NullText = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(120, 20);
			this.label1.Text = "Lagerplatser";
			// 
			// zoneCodeCol
			// 
			this.zoneCodeCol.HeaderText = "Zon";
			this.zoneCodeCol.MappingName = "zoneCode";
			this.zoneCodeCol.NullText = "";
			// 
			// blockingCol
			// 
			this.blockingCol.HeaderText = "Låst";
			this.blockingCol.MappingName = "blocking";
			this.blockingCol.NullText = "";
			// 
			// emptyCol
			// 
			this.emptyCol.HeaderText = "Tom";
			this.emptyCol.MappingName = "empty";
			this.emptyCol.NullText = "";
			// 
			// Bins
			// 
			this.Controls.Add(this.binGrid);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Lagerplatser";
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

			DataBins dataBins = new DataBins(smartDatabase);
			DataSet dataSet = dataBins.getDataSet();

			binGrid.DataSource = dataSet.Tables[0];

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		}

		private void jobGrid_Click(object sender, System.EventArgs e)
		{
		}

	}
}
