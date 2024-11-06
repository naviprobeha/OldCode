using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ItemList.
	/// </summary>
	public class ItemList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox searchWhatBox;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.DataGridTableStyle itemTable;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.DataGrid itemGrid;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionBox;

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private DataItem returnDataItem = null;

		public ItemList(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			searchWhatBox.SelectedIndex = 0;

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
			this.itemGrid = new System.Windows.Forms.DataGrid();
			this.itemTable = new System.Windows.Forms.DataGridTableStyle();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionBox = new System.Windows.Forms.DataGridTextBoxColumn();
			this.searchWhatBox = new System.Windows.Forms.ComboBox();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			// 
			// itemGrid
			// 
			this.itemGrid.Location = new System.Drawing.Point(8, 40);
			this.itemGrid.Size = new System.Drawing.Size(304, 128);
			this.itemGrid.TableStyles.Add(this.itemTable);
			// 
			// itemTable
			// 
			this.itemTable.GridColumnStyles.Add(this.noCol);
			this.itemTable.GridColumnStyles.Add(this.descriptionBox);
			this.itemTable.MappingName = "item";
			// 
			// noCol
			// 
			this.noCol.HeaderText = "Artikelnr";
			this.noCol.MappingName = "no";
			this.noCol.NullText = "";
			this.noCol.Width = 100;
			// 
			// descriptionBox
			// 
			this.descriptionBox.HeaderText = "Beskrivning";
			this.descriptionBox.MappingName = "description";
			this.descriptionBox.NullText = "";
			this.descriptionBox.Width = 200;
			// 
			// searchWhatBox
			// 
			this.searchWhatBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular);
			this.searchWhatBox.Items.Add("Artikelnr");
			this.searchWhatBox.Items.Add("Beskrivning");
			this.searchWhatBox.Location = new System.Drawing.Point(8, 8);
			this.searchWhatBox.Size = new System.Drawing.Size(136, 26);
			this.searchWhatBox.SelectedIndexChanged += new System.EventHandler(this.searchWhatBox_SelectedIndexChanged);
			// 
			// inputBox
			// 
			this.inputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular);
			this.inputBox.Location = new System.Drawing.Point(152, 8);
			this.inputBox.ReadOnly = true;
			this.inputBox.Size = new System.Drawing.Size(160, 24);
			this.inputBox.Text = "";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(160, 176);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 176);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(88, 176);
			this.button3.Size = new System.Drawing.Size(64, 32);
			this.button3.Text = "Sök";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(48, 176);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(8, 176);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// ItemList
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.inputBox);
			this.Controls.Add(this.searchWhatBox);
			this.Controls.Add(this.itemGrid);
			this.Text = "Artikellista";
			this.Load += new System.EventHandler(this.ItemList_Load);

		}
		#endregion

		private void ItemList_Load(object sender, System.EventArgs e)
		{
		
		}

		private void updateGrid()
		{
			DataItems dataItems = new DataItems(this.smartDatabase);
			DataSet itemDataSet = dataItems.getAvailableDataSet(searchWhatBox.SelectedIndex, inputBox.Text);
			itemGrid.DataSource = itemDataSet.Tables[0];
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Keyboard keyboard = new Keyboard(30);
			keyboard.ShowDialog();

			if (searchWhatBox.SelectedIndex == 0) keyboard.setStartTab(1);

			inputBox.Text = keyboard.getInputString();
			keyboard.Dispose();

			updateGrid();
		}

		private void searchWhatBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (itemGrid.CurrentRowIndex > -1)
			{

				returnDataItem = new DataItem(smartDatabase, itemGrid[itemGrid.CurrentRowIndex, 0].ToString());

			}

			this.Close();

		}

		public DataItem getItem()
		{
			return returnDataItem;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.itemGrid.DataSource != null) && (((DataTable)this.itemGrid.DataSource).Rows.Count > 0))
			{
				if (this.itemGrid.CurrentRowIndex > 0)
				{
					this.itemGrid.CurrentRowIndex = this.itemGrid.CurrentRowIndex - 1;
					this.itemGrid.Select(this.itemGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.itemGrid.DataSource != null) && (((DataTable)this.itemGrid.DataSource).Rows.Count > 0))
			{
				if (this.itemGrid.CurrentRowIndex < (((DataTable)this.itemGrid.DataSource).Rows.Count -1))
				{
					this.itemGrid.CurrentRowIndex = this.itemGrid.CurrentRowIndex + 1;
					this.itemGrid.Select(this.itemGrid.CurrentRowIndex);
				}
			}
		}

	}
}
