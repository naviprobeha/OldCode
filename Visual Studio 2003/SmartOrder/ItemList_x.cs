using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for ItemList.
	/// </summary>
	public class ItemList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid itemGrid;
		private System.Windows.Forms.MainMenu mainMenu1;
	
		private SmartDatabase smartDatabase;
		private DataItems dataItems;
		private System.Windows.Forms.Button button1;
		private DataItem selectedItem;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox seasonBox;
		private System.Windows.Forms.ComboBox prodGroupBox;
		private System.Windows.Forms.Button button3;
		
		private int status;

		public ItemList(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.dataItems = new DataItems(smartDatabase);

			status = 0;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			DataProductGroups productGroups = new DataProductGroups(smartDatabase);
			DataSet productGroupsDataSet = productGroups.getDataSet();

			prodGroupBox.Items.Add("Alla");
			
			int i = 0;
			while (i < productGroupsDataSet.Tables[0].Rows.Count)
			{
				prodGroupBox.Items.Add((string)productGroupsDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
				i++;
			}

			DataSeasons seasons = new DataSeasons(smartDatabase);
			DataSet seasonsDataSet = seasons.getDataSet();

			seasonBox.Items.Add("Alla");
			
			i = 0;
			while (i < seasonsDataSet.Tables[0].Rows.Count)
			{
				seasonBox.Items.Add((string)seasonsDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
				i++;
			}

			prodGroupBox.Text = "Alla";
			seasonBox.Text = "Alla";
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
			this.itemGrid = new System.Windows.Forms.DataGrid();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.seasonBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.prodGroupBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// itemGrid
			// 
			this.itemGrid.Location = new System.Drawing.Point(0, 48);
			this.itemGrid.Size = new System.Drawing.Size(240, 184);
			this.itemGrid.Click += new System.EventHandler(this.itemGrid_Click_1);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(168, 240);
			this.button1.Size = new System.Drawing.Size(64, 20);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(96, 240);
			this.button2.Size = new System.Drawing.Size(64, 20);
			// 
			// seasonBox
			// 
			this.seasonBox.Location = new System.Drawing.Point(8, 24);
			this.seasonBox.Size = new System.Drawing.Size(100, 21);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(100, 16);
			// 
			// prodGroupBox
			// 
			this.prodGroupBox.Location = new System.Drawing.Point(136, 24);
			this.prodGroupBox.Size = new System.Drawing.Size(100, 21);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(136, 8);
			this.label2.Size = new System.Drawing.Size(100, 16);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(16, 240);
			// 
			// ItemList
			// 
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.prodGroupBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.seasonBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.itemGrid);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder - Artiklar";

		}
		#endregion

		private void ItemList_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void updateGrid()
		{
			System.Data.DataSet itemDataSet = dataItems.getDataSet(seasonBox.Text, prodGroupBox.Text);
			itemGrid.DataSource = itemDataSet.Tables[0];

			if (selectedItem != null)
			{
				int i = 0;
				bool found = false;

				while ((i < itemGrid.BindingContext[itemGrid.DataSource, ""].Count) && !found)
				{
					if (itemGrid[i, 0].ToString().Equals(selectedItem.no))
					{
						itemGrid.CurrentRowIndex = i;
						itemGrid.Select(i);
						found = true;
					}
					i++;
				}
			}
		}

		private void itemGrid_Click(object sender, System.EventArgs e)
		{
			if (itemGrid.BindingContext[itemGrid.DataSource, ""].Count > 0)
			{
				selectedItem = new DataItem(itemGrid[itemGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
			}
		}

		public DataItem getSelected()
		{
			return selectedItem;
		}

		public void setSelected(DataItem dataItem)
		{
			selectedItem = dataItem;
		}

		public int getStatus()
		{
			return status;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (selectedItem == null) 
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja en artikel.");
			}
			else
			{
				status = 1;
				this.Close();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void seasonBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void prodGroupBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			updateGrid();

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			InventoryForm inventory = new InventoryForm(selectedItem, smartDatabase);
			inventory.ShowDialog();
		}

	}
}
