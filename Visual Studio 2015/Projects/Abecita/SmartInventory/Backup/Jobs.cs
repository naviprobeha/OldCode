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
	public class Jobs : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid jobGrid;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.DataGridTableStyle jobTable;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
		private DataWhseActivityHeader selectedJob;

		public Jobs(SmartDatabase smartDatabase)
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
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.jobGrid = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.jobTable = new System.Windows.Forms.DataGridTableStyle();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button3.Location = new System.Drawing.Point(128, 192);
			this.button3.Size = new System.Drawing.Size(104, 32);
			this.button3.Text = "Inlagring";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 232);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(8, 192);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Uttag";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// jobGrid
			// 
			this.jobGrid.Location = new System.Drawing.Point(0, 32);
			this.jobGrid.Size = new System.Drawing.Size(240, 152);
			this.jobGrid.TableStyles.Add(this.jobTable);
			this.jobGrid.Text = "jobGrid";
			this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(120, 20);
			this.label1.Text = "Påfyllningsuppdrag";
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
			// Jobs
			// 
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.jobGrid);
			this.Controls.Add(this.label1);
			this.Text = "Påfyllningsuppdrag";
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
			DataSet dataSet = dataWhseActivityHeaders.getDataSet(6);

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
			else
			{
				if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0) 
				{
					selectedJob = new DataWhseActivityHeader(jobGrid[0, 0].ToString(), DataWhseActivityHeaders.WHSE_TYPE_MOVEMENT);
				}
			}

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (selectedJob != null)
			{
				JobFreq jobFreq = new JobFreq(smartDatabase, selectedJob);
				jobFreq.ShowDialog();
				jobFreq.Dispose();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (selectedJob != null)
			{
				SaveFreq saveFreq = new SaveFreq(smartDatabase, selectedJob);
				saveFreq.ShowDialog();
				saveFreq.Dispose();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void jobGrid_Click(object sender, System.EventArgs e)
		{
			if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
			{
				selectedJob = new DataWhseActivityHeader(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), DataWhseActivityHeaders.WHSE_TYPE_MOVEMENT);
			}		
		}
	}
}
