using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LoadedContainers.
	/// </summary>
	public class LoadedContainers : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label header;
		private System.Windows.Forms.DataGrid containerLoadGrid;
	
		private SmartDatabase smartDatabase;
		private Status status;
		private ArrayList unloadContainerList;
		private ArrayList loadContainerList;
		private ArrayList lineOrderContainerList;
		private int locationType;
		private System.Windows.Forms.Button button3;
		private string locationCode;
		private int documentType;
		private string documentNo;
		private System.Windows.Forms.DataGridTableStyle containerLoadTable;
		private System.Windows.Forms.DataGridTextBoxColumn containerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn statusTextCol;
		private int formStatus;
		private System.Windows.Forms.Button unloadButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button loadButton;

		public LoadedContainers(SmartDatabase smartDatabase, Status status)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
			this.status = status;
			this.unloadContainerList = new ArrayList();
			this.loadContainerList = new ArrayList();
			this.lineOrderContainerList = new ArrayList();

			this.locationType = 3;
			this.locationCode = "";

		}

		public LoadedContainers(SmartDatabase smartDatabase, Status status, int locationType, string locationCode, int documentType, string documentNo)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
			this.status = status;
			this.unloadContainerList = new ArrayList();
			this.loadContainerList = new ArrayList();
			this.lineOrderContainerList = new ArrayList();

			this.locationType = locationType;
			this.locationCode = locationCode;
			this.documentType = documentType;
			this.documentNo = documentNo;

		}

		public void setLineOrderContainers(ArrayList lineOrderContainerList)
		{
			this.lineOrderContainerList = lineOrderContainerList;
		}

		public ArrayList getLoadedContainers()
		{
			return loadContainerList;
		}

		public void setUnloadContainers(ArrayList unloadContainerList)
		{
			this.unloadContainerList = unloadContainerList;
		}

		public void hideLoadContainerButton()
		{
			this.loadButton.Visible = false;
		}

		public void hideUnLoadContainerButton()
		{
			this.unloadButton.Visible = false;
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
			this.header = new System.Windows.Forms.Label();
			this.containerLoadGrid = new System.Windows.Forms.DataGrid();
			this.containerLoadTable = new System.Windows.Forms.DataGridTableStyle();
			this.containerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.statusTextCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button2 = new System.Windows.Forms.Button();
			this.unloadButton = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.loadButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// header
			// 
			this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.header.Location = new System.Drawing.Point(5, 3);
			this.header.Size = new System.Drawing.Size(307, 20);
			this.header.Text = "Containers på flaket";
			// 
			// containerLoadGrid
			// 
			this.containerLoadGrid.Location = new System.Drawing.Point(8, 24);
			this.containerLoadGrid.Size = new System.Drawing.Size(208, 136);
			this.containerLoadGrid.TableStyles.Add(this.containerLoadTable);
			// 
			// containerLoadTable
			// 
			this.containerLoadTable.GridColumnStyles.Add(this.containerNoCol);
			this.containerLoadTable.GridColumnStyles.Add(this.statusTextCol);
			this.containerLoadTable.MappingName = "containerLoad";
			// 
			// containerNoCol
			// 
			this.containerNoCol.HeaderText = "Containernr";
			this.containerNoCol.MappingName = "containerNo";
			this.containerNoCol.NullText = "";
			this.containerNoCol.Width = 75;
			// 
			// statusTextCol
			// 
			this.statusTextCol.HeaderText = "Status";
			this.statusTextCol.MappingName = "statusText";
			this.statusTextCol.NullText = "";
			this.statusTextCol.Width = 100;
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(224, 168);
			this.button2.Size = new System.Drawing.Size(88, 40);
			this.button2.Text = "OK";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// unloadButton
			// 
			this.unloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.unloadButton.Location = new System.Drawing.Point(224, 24);
			this.unloadButton.Size = new System.Drawing.Size(88, 40);
			this.unloadButton.Text = "Lossa";
			this.unloadButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(128, 168);
			this.button3.Size = new System.Drawing.Size(88, 40);
			this.button3.Text = "Avbryt";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// loadButton
			// 
			this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.loadButton.Location = new System.Drawing.Point(224, 72);
			this.loadButton.Size = new System.Drawing.Size(88, 40);
			this.loadButton.Text = "Lasta ny";
			this.loadButton.Click += new System.EventHandler(this.button4_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(8, 168);
			this.button1.Size = new System.Drawing.Size(88, 40);
			this.button1.Text = "Service";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// LoadedContainers
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.loadButton);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.unloadButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.containerLoadGrid);
			this.Controls.Add(this.header);
			this.Text = "Container på flaket";
			this.Load += new System.EventHandler(this.LoadedContainers_Load);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			string confirmationText = "";
			int loadContainerCount = loadContainerList.Count + lineOrderContainerList.Count;

			if ((loadButton.Visible) && (unloadButton.Visible)) confirmationText = "Du kommer att lasta "+loadContainerCount+" containers samt lossa "+unloadContainerList.Count+" containers. Riktigt?";
			if ((loadButton.Visible) && (!unloadButton.Visible)) confirmationText = "Du kommer att lasta "+loadContainerCount+" containers. Riktigt?";
			if ((!loadButton.Visible) && (unloadButton.Visible)) confirmationText = "Du kommer att lossa "+unloadContainerList.Count+" containers. Riktigt?";

			if (MessageBox.Show(confirmationText, "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				unloadContainers();
				loadContainers();	

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				this.formStatus = 1;
				this.Close();
			}
		}

		private void updateGrid()
		{
			DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
			DataSet containerLoadDataSet = dataContainerLoads.getDataSet();

			int j = 0;
			while (j < lineOrderContainerList.Count)
			{
				DataRow dataRow = containerLoadDataSet.Tables[0].NewRow();
				dataRow["containerNo"] = lineOrderContainerList[j].ToString();
				containerLoadDataSet.Tables[0].Rows.Add(dataRow);

				j++;
			}

			int k = 0;
			while (k < loadContainerList.Count)
			{
				DataRow dataRow = containerLoadDataSet.Tables[0].NewRow();
				dataRow["containerNo"] = loadContainerList[k].ToString();
				containerLoadDataSet.Tables[0].Rows.Add(dataRow);

				k++;
			}

			int i = 0;

			if (containerLoadDataSet.Tables[0].Rows.Count > 0)
			{
				while(i < containerLoadDataSet.Tables[0].Rows.Count)
				{
					DataRow dataRow = containerLoadDataSet.Tables[0].Rows[i];
					dataRow["statusText"] = "";
					if (unloadContainerList.Contains(containerLoadDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) dataRow["statusText"] = "Lossa";
					if (loadContainerList.Contains(containerLoadDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) dataRow["statusText"] = "Lasta";
					if (lineOrderContainerList.Contains(containerLoadDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) dataRow["statusText"] = "Linjeorder";

					containerLoadDataSet.Tables[0].Rows[i].AcceptChanges();

					i++;
				}

			}

			containerLoadGrid.DataSource = containerLoadDataSet.Tables[0];

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ((!lineOrderContainerList.Contains(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString())) && (!loadContainerList.Contains(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString())))
			{
				if (unloadContainerList.Contains(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString()))
				{
					unloadContainerList.Remove(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString());
				}
				else
				{
					unloadContainerList.Add(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString());
				}
				updateGrid();
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.formStatus = 0;
			this.Close();
		}

		public int getFormStatus()
		{
			return formStatus;
		}

		private void unloadContainers()
		{
			int i = 0;
			while(i < unloadContainerList.Count)
			{
				DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
				dataContainerEntry.containerNo = unloadContainerList[i].ToString();
				dataContainerEntry.entryDateTime = DateTime.Now;
				dataContainerEntry.type = 1;  // UnLoad Container
				dataContainerEntry.positionX = status.rt90x;
				dataContainerEntry.positionY = status.rt90y;
				dataContainerEntry.locationType = this.locationType;
				dataContainerEntry.locationCode = this.locationCode;
				dataContainerEntry.documentType = this.documentType;
				dataContainerEntry.documentNo = this.documentNo;
				dataContainerEntry.commit();

				DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
				dataContainerLoads.unloadContainer(unloadContainerList[i].ToString());

				i++;
			}
		}

		private void loadContainers()
		{
			int i = 0;
			while(i < lineOrderContainerList.Count)
			{
				DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
				dataContainerEntry.containerNo = lineOrderContainerList[i].ToString();
				dataContainerEntry.entryDateTime = DateTime.Now;
				dataContainerEntry.type = 0;  // Load Container
				dataContainerEntry.positionX = status.rt90x;
				dataContainerEntry.positionY = status.rt90y;
				dataContainerEntry.locationType = this.locationType;
				dataContainerEntry.locationCode = this.locationCode;
				dataContainerEntry.documentType = this.documentType;
				dataContainerEntry.documentNo = this.documentNo;
				dataContainerEntry.commit();

				DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
				dataContainerLoads.loadContainer(lineOrderContainerList[i].ToString());

				i++;
			}

			i = 0;
			while(i < loadContainerList.Count)
			{
				DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
				dataContainerEntry.containerNo = loadContainerList[i].ToString();
				dataContainerEntry.entryDateTime = DateTime.Now;
				dataContainerEntry.type = 0;  // Load Container
				dataContainerEntry.positionX = status.rt90x;
				dataContainerEntry.positionY = status.rt90y;
				dataContainerEntry.locationType = this.locationType;
				dataContainerEntry.locationCode = this.locationCode;
				dataContainerEntry.documentType = this.documentType;
				dataContainerEntry.documentNo = this.documentNo;
				dataContainerEntry.commit();

				DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
				dataContainerLoads.loadContainer(loadContainerList[i].ToString());

				i++;
			}
		}


		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((lineOrderContainerList != null) && (lineOrderContainerList.Count > 0))
			{
				if (MessageBox.Show("Linjeordern innehåller redan "+lineOrderContainerList.Count+" containers. Vill du lasta ytterliggare containers?", "Fråga", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}
			}

			Keyboard keyboard = new Keyboard(30);
			keyboard.setStartTab(1);
			keyboard.setHeaderText("Ange containernr");
			keyboard.ShowDialog();

			if (keyboard.getInputString() != "")
			{
				string containerNo = keyboard.getInputString();
				loadContainerList.Add(containerNo);
			}
			
			keyboard.Dispose();

			this.updateGrid();

		}

		private void LoadedContainers_Load(object sender, System.EventArgs e)
		{
			this.updateGrid();
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			if ((!lineOrderContainerList.Contains(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString())) && (!loadContainerList.Contains(containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString())))
			{
				if (MessageBox.Show("Vill du service-rapportera container "+containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString()+"?", "Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
					dataContainerEntry.containerNo = containerLoadGrid[containerLoadGrid.CurrentRowIndex, 0].ToString();
					dataContainerEntry.entryDateTime = DateTime.Now;
					dataContainerEntry.type = 3;  // Service
					dataContainerEntry.positionX = this.status.rt90x;
					dataContainerEntry.positionY = this.status.rt90y;
					dataContainerEntry.locationType = locationType;
					dataContainerEntry.locationCode = locationCode;
					dataContainerEntry.commit();

				}
			}
		}
	}
}
