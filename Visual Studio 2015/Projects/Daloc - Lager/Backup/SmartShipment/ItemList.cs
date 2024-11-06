using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for ItemList.
	/// </summary>
	public class ItemList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGrid itemGrid;
		private System.Windows.Forms.DataGridTableStyle itemTable;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private System.Windows.Forms.Button button1;

		private SmartDatabase smartDatabase;
		private DataItems dataItems;	
		private DataSetup dataSetup;
		private DataItem selectedItem;

		public ItemList(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			dataItems = new DataItems(smartDatabase);
			dataSetup = new DataSetup(smartDatabase);
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
			this.itemGrid = new System.Windows.Forms.DataGrid();
			this.itemTable = new System.Windows.Forms.DataGridTableStyle();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// itemGrid
			// 
			this.itemGrid.Size = new System.Drawing.Size(240, 216);
			this.itemGrid.TableStyles.Add(this.itemTable);
			this.itemGrid.Text = "itemGrid";
			this.itemGrid.Click += new System.EventHandler(this.itemGrid_Click);
			// 
			// itemTable
			// 
			this.itemTable.GridColumnStyles.Add(this.itemNoCol);
			this.itemTable.GridColumnStyles.Add(this.descriptionCol);
			this.itemTable.MappingName = "item";
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "no";
			this.itemNoCol.NullText = "";
			this.itemNoCol.Width = 100;
			// 
			// descriptionCol
			// 
			this.descriptionCol.HeaderText = "Beskrivning";
			this.descriptionCol.MappingName = "description";
			this.descriptionCol.NullText = "";
			this.descriptionCol.Width = 150;
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 224);
			this.button2.Size = new System.Drawing.Size(104, 40);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(128, 224);
			this.button1.Size = new System.Drawing.Size(104, 40);
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ItemList
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.itemGrid);
			this.Text = "ItemList";
			this.Load += new System.EventHandler(this.ItemList_Load);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (selectedItem == null)
			{
				MessageBox.Show("Du måste välja en artikel först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
				this.Close();
			}

		}

		public DataItem getItem()
		{
			return selectedItem;
		}

		private void ItemList_Load(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();
			
			System.Data.DataSet itemDataSet = dataItems.getDataSet();
			itemGrid.DataSource = itemDataSet.Tables[0];

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void itemGrid_Click(object sender, System.EventArgs e)
		{
			if (itemGrid.BindingContext[itemGrid.DataSource, ""].Count > 0)
			{
				selectedItem = new DataItem(itemGrid[itemGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
			}

		}
	}
}
