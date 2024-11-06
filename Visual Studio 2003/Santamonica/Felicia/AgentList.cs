using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class AgentList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label headerLabel;

		private SmartDatabase smartDatabase;
		private string selectedAgent;
		private DataSet agentDataSet;
		private System.Windows.Forms.DataGridTableStyle agentTable;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private System.Windows.Forms.DataGridTextBoxColumn codeCol;
		private System.Windows.Forms.DataGrid agentGrid;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private int status;
	
		public AgentList(SmartDatabase smartDatabase)
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
			this.headerLabel = new System.Windows.Forms.Label();
			this.agentGrid = new System.Windows.Forms.DataGrid();
			this.agentTable = new System.Windows.Forms.DataGridTableStyle();
			this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(5, 3);
			this.headerLabel.Size = new System.Drawing.Size(219, 20);
			this.headerLabel.Text = "Bilar";
			// 
			// agentGrid
			// 
			this.agentGrid.Location = new System.Drawing.Point(8, 32);
			this.agentGrid.Size = new System.Drawing.Size(304, 128);
			this.agentGrid.TableStyles.Add(this.agentTable);
			this.agentGrid.Text = "agentGrid";
			// 
			// agentTable
			// 
			this.agentTable.GridColumnStyles.Add(this.codeCol);
			this.agentTable.GridColumnStyles.Add(this.descriptionCol);
			this.agentTable.MappingName = "agent";
			// 
			// codeCol
			// 
			this.codeCol.HeaderText = "Kod";
			this.codeCol.MappingName = "code";
			this.codeCol.NullText = "";
			// 
			// descriptionCol
			// 
			this.descriptionCol.HeaderText = "Beskrivning";
			this.descriptionCol.MappingName = "description";
			this.descriptionCol.NullText = "";
			this.descriptionCol.Width = 280;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 168);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(160, 168);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(48, 168);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(8, 168);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// AgentList
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.agentGrid);
			this.Controls.Add(this.headerLabel);
			this.Text = "Logga in";

		}
		#endregion


		private void updateGrid()
		{
			DataAgents dataAgents = new DataAgents(this.smartDatabase);
			agentDataSet = dataAgents.getDataSet();
			agentGrid.DataSource = agentDataSet.Tables[0];
		}

		public string getSelectedAgent()
		{
			return selectedAgent;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.selectedAgent = agentDataSet.Tables[0].Rows[agentGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();
			this.status = 1;
			this.Close();

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.status = 0;
			this.Close();
		}

		public int getStatus()
		{
			return this.status;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.agentGrid.DataSource != null) && (((DataTable)this.agentGrid.DataSource).Rows.Count > 0))
			{
				if (this.agentGrid.CurrentRowIndex > 0)
				{
					this.agentGrid.CurrentRowIndex = this.agentGrid.CurrentRowIndex - 1;
					this.agentGrid.Select(this.agentGrid.CurrentRowIndex);
				}
			}
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.agentGrid.DataSource != null) && (((DataTable)this.agentGrid.DataSource).Rows.Count > 0))
			{
				if (this.agentGrid.CurrentRowIndex < (((DataTable)this.agentGrid.DataSource).Rows.Count -1))
				{
					this.agentGrid.CurrentRowIndex = this.agentGrid.CurrentRowIndex + 1;
					this.agentGrid.Select(this.agentGrid.CurrentRowIndex);
				}
			}		
		}

	}
}
