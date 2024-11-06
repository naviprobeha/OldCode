using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreJobLines : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader dataWhseActivityHeader;

        public StoreJobLines(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader)
        {
            InitializeComponent();
            
            this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;
            this.label1.Text = this.label1.Text + " " + dataWhseActivityHeader.no;

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
            DataSet dataSet = dataWhseActivityLines.getDataSet(dataWhseActivityHeader);

            jobLineGrid.DataSource = dataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void StoreJobLines_Load(object sender, EventArgs e)
        {
            updateGrid();

        }
    }
}