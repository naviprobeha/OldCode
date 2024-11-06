using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Colors.
	/// </summary>
	public class ColorList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.DataGrid colorGrid;
		private System.Windows.Forms.DataGridTableStyle colorTable;
		private System.Windows.Forms.DataGridTextBoxColumn codeCol;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		
		private DataColors dataColors;
		private SmartDatabase smartDatabase;
		private DataSet colorDataSet;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private DataItem dataItem;
		private DataColor selectedColor;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private int status;
	
		public ColorList(SmartDatabase smartDatabase, DataItem dataItem)
		{
			this.smartDatabase = smartDatabase;
			this.dataColors = new DataColors(smartDatabase, dataItem);
			this.dataItem = dataItem;
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.colorGrid = new System.Windows.Forms.DataGrid();
			this.colorTable = new System.Windows.Forms.DataGridTableStyle();
			this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			// 
			// colorGrid
			// 
			this.colorGrid.Location = new System.Drawing.Point(0, 56);
			this.colorGrid.Size = new System.Drawing.Size(240, 176);
			this.colorGrid.TableStyles.Add(this.colorTable);
			this.colorGrid.Text = "dataGrid1";
			this.colorGrid.Click += new System.EventHandler(this.colorGrid_Click);
			// 
			// colorTable
			// 
			this.colorTable.GridColumnStyles.Add(this.codeCol);
			this.colorTable.GridColumnStyles.Add(this.descriptionCol);
			this.colorTable.MappingName = "color";
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
			this.descriptionCol.Width = 100;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 240);
			this.button1.Text = "Nästa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(72, 240);
			this.button2.Size = new System.Drawing.Size(80, 20);
			this.button2.Text = "Föregående";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(152, 20);
			this.descriptionBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(64, 20);
			this.itemNoBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 8);
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.Text = "Beskrivning:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.Text = "Artikelnr:";
			// 
			// ColorList
			// 
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.colorGrid);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder - Färger";
			this.Load += new System.EventHandler(this.Colors_Load);

		}
		#endregion

		private void updateGrid()
		{
			colorDataSet = dataColors.getDataSet();
			colorGrid.DataSource = colorDataSet.Tables[0];

			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;

			if (selectedColor != null)
			{
				int i = 0;
				bool found = false;

				while ((i < colorGrid.BindingContext[colorGrid.DataSource, ""].Count) && !found)
				{
					if (colorGrid[i, 0].ToString().Equals(selectedColor.code))
					{
						colorGrid.CurrentRowIndex = i;
						colorGrid.Select(i);
						found = true;
					}
					i++;
				}
			}

		}

		public DataColor getSelected()
		{
			return selectedColor;
		}

		public void setSelected(DataColor dataColor)
		{
			selectedColor = dataColor;
		}

		public int getStatus()
		{
			return status;
		}

		private void Colors_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (selectedColor == null) 
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja en färg.");
			}
			else
			{
				status = 1;
				this.Close();
			}		
		}

		private void colorGrid_Click(object sender, System.EventArgs e)
		{
			selectedColor = new DataColor(colorGrid[colorGrid.CurrentRowIndex, 0].ToString(), smartDatabase);	
		}

	}
}
