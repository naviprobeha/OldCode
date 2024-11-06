using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for MaintMoveJobs.
	/// </summary>
	public class MaintSaveItems : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid jobGrid;
		private System.Windows.Forms.DataGridTableStyle jobLines;
		private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn handleUnitCol;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.TextBox scanHEidBox;
		private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private DataWhseItemStore selectedJob;

		public MaintSaveItems(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			updateGrid();
		}

		private void updateGrid()
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			DataWhseItemStores dataWhseItemStores = new DataWhseItemStores(smartDatabase);
			DataSet dataSet = dataWhseItemStores.getDataSet();

			jobGrid.DataSource = dataSet.Tables[0];

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
			this.scanHEidBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.jobGrid = new System.Windows.Forms.DataGrid();
			this.jobLines = new System.Windows.Forms.DataGridTableStyle();
			this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.handleUnitCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.scanHEidBox);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Location = new System.Drawing.Point(0, 167);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// scanHEidBox
			// 
			this.scanHEidBox.Location = new System.Drawing.Point(16, 29);
			this.scanHEidBox.Size = new System.Drawing.Size(208, 20);
			this.scanHEidBox.Text = "";
			this.scanHEidBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanHEidBox_KeyPress);
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
			this.label2.Location = new System.Drawing.Point(8, 23);
			this.label2.Size = new System.Drawing.Size(208, 20);
			this.label2.Text = "Inlagring";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 232);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// jobGrid
			// 
			this.jobGrid.Location = new System.Drawing.Point(0, 47);
			this.jobGrid.Size = new System.Drawing.Size(240, 120);
			this.jobGrid.TableStyles.Add(this.jobLines);
			this.jobGrid.Text = "jobGrid";
			this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click);
			// 
			// jobLines
			// 
			this.jobLines.GridColumnStyles.Add(this.locationCodeCol);
			this.jobLines.GridColumnStyles.Add(this.binCodeCol);
			this.jobLines.GridColumnStyles.Add(this.handleUnitCol);
			this.jobLines.GridColumnStyles.Add(this.itemNoCol);
			this.jobLines.GridColumnStyles.Add(this.quantityCol);
			this.jobLines.MappingName = "whseItemStore";
			// 
			// binCodeCol
			// 
			this.binCodeCol.HeaderText = "Lagerplats";
			this.binCodeCol.MappingName = "binCode";
			this.binCodeCol.NullText = "";
			this.binCodeCol.Width = 70;
			// 
			// handleUnitCol
			// 
			this.handleUnitCol.HeaderText = "Hanteringsenhet";
			this.handleUnitCol.MappingName = "handleUnitId";
			this.handleUnitCol.NullText = "";
			this.handleUnitCol.Width = 80;
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
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 7);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Lagervård - Omflyttning";
			// 
			// locationCodeCol
			// 
			this.locationCodeCol.HeaderText = "Lagerställe";
			this.locationCodeCol.MappingName = "locationCode";
			this.locationCodeCol.NullText = "(null)";
			this.locationCodeCol.Width = 30;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.Text = "Editera";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Ta bort";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// MaintSaveItems
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.jobGrid);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Lagervård - Omflyttning";
			this.Load += new System.EventHandler(this.MaintSaveItems_Load);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void MaintSaveItems_Load(object sender, System.EventArgs e)
		{
			this.scanHEidBox.Focus();
		}

		private void scanHEidBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				DataWhseItemStore dataWhseItemStore = new DataWhseItemStore(scanHEidBox.Text, smartDatabase);

				if (dataWhseItemStore.exists)
				{
					MaintSaveItem maintSaveItem = new MaintSaveItem(dataWhseItemStore, smartDatabase);
					maintSaveItem.ShowDialog();
					updateGrid();
				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
				}
				this.scanHEidBox.Text = "";
				this.scanHEidBox.Focus();
			}
		
		}

		private void jobGrid_Click(object sender, System.EventArgs e)
		{
			if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
			{
				selectedJob = new DataWhseItemStore(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), jobGrid[jobGrid.CurrentRowIndex, 1].ToString(), smartDatabase);
			}			
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if (selectedJob != null)
			{
				System.Windows.Forms.MessageBox.Show("Lägg tillbaka lådan på plats "+selectedJob.binCode+".");
				selectedJob.delete();
				updateGrid();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja en rad först.");
			}
			scanHEidBox.Focus();
		}


	}
}
