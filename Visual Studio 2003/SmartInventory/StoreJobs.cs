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
	public class StoreJobs : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid jobGrid;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.DataGridTableStyle jobTable;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private DataWhseActivityHeader selectedJob;

		public StoreJobs(SmartDatabase smartDatabase)
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
			this.jobGrid = new System.Windows.Forms.DataGrid();
			this.jobTable = new System.Windows.Forms.DataGridTableStyle();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			// 
			// jobGrid
			// 
			this.jobGrid.Location = new System.Drawing.Point(0, 32);
			this.jobGrid.Size = new System.Drawing.Size(240, 208);
			this.jobGrid.TableStyles.Add(this.jobTable);
			this.jobGrid.Text = "jobGrid";
			this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click);
			// 
			// jobTable
			// 
			this.jobTable.GridColumnStyles.Add(this.noCol);
			this.jobTable.GridColumnStyles.Add(this.noOfLinesCol);
			this.jobTable.MappingName = "whseActivity";
			// 
			// noCol
			// 
			this.noCol.HeaderText = "Batch-ID";
			this.noCol.MappingName = "no";
			this.noCol.NullText = "";
			this.noCol.Width = 100;
			// 
			// noOfLinesCol
			// 
			this.noOfLinesCol.HeaderText = "Antal rader";
			this.noOfLinesCol.MappingName = "noOfLines";
			this.noOfLinesCol.NullText = "";
			this.noOfLinesCol.Width = 100;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(120, 20);
			this.label1.Text = "Inlagringsuppdrag";
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
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// StoreJobs
			// 
			this.Controls.Add(this.jobGrid);
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

			DataWhseActivityHeaders dataWhseActivityHeaders = new DataWhseActivityHeaders(smartDatabase);
			DataSet dataSet = dataWhseActivityHeaders.getDataSet(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL);

			jobGrid.DataSource = dataSet.Tables[0];

			if (selectedJob != null)
			{
				int i = 0;
				bool found = false;

				while ((i < jobGrid.BindingContext[jobGrid.DataSource, ""].Count) && !found)
				{
					if (jobGrid[i, 0].ToString().Equals(selectedJob.no))
					{
						jobGrid.CurrentRowIndex = i;
						jobGrid.Select(i);
						found = true;
					}
					i++;
				}
			}

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		}

		private void jobGrid_Click(object sender, System.EventArgs e)
		{
			if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
			{
				selectedJob = new DataWhseActivityHeader(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), 1);
			}		
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if (selectedJob != null)
			{
				StoreJobLines storeJobLines = new StoreJobLines(smartDatabase, selectedJob);
				storeJobLines.ShowDialog();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
			}
		
		}
	}
}
