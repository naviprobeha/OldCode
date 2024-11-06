using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class ItemList : Form
    {
        private SmartDatabase smartDatabase;
        private DataItems dataItems;
        private DataSetup dataSetup;
        private DataItem selectedItem;

        public ItemList(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataItems = new DataItems(smartDatabase);
            dataSetup = new DataSetup(smartDatabase);
        
        }


        private void button1_Click_1(object sender, EventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void itemGrid_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            System.Data.DataSet itemDataSet = dataItems.getDataSet();
            itemGrid.DataSource = itemDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }

        private void itemGrid_Click_1(object sender, EventArgs e)
        {
            if (itemGrid.BindingContext[itemGrid.DataSource, ""].Count > 0)
            {
                selectedItem = new DataItem(itemGrid[itemGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
            }
        }

        public DataItem getItem()
        {
            return selectedItem;
        }

    }
}