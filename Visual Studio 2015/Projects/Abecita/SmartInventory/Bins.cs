using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class Bins : Form
    {
        private SmartDatabase smartDatabase;

        public Bins(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
        }

        private void jobGrid_Click(object sender, EventArgs e)
        {

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataBins dataBins = new DataBins(smartDatabase);
            DataSet dataSet = dataBins.getDataSet();

            binGrid.DataSource = dataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void Bins_Load(object sender, EventArgs e)
        {
            updateGrid();
        }

    }
}