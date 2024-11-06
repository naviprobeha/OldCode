using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class PickItemLines : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet pickItemLinesDataSet;
        private string _documentNo = "";

        public PickItemLines(Configuration configuration, SmartDatabase smartDatabase, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = documentNo;
 
            updateView();
        }

        private void updateView()
        {
            this.pickListNo.Text = _documentNo;
            int noOfLines = DataPickItemLines.countUnhandlesLines(smartDatabase, _documentNo);
            this.noOfLinesBox.Text = noOfLines.ToString();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            pickItemLinesDataSet = DataPickItemLines.getDataSet(smartDatabase, _documentNo);
            pickLinesGrid.DataSource = pickItemLinesDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            /*
            this.pickListNo.Text = _documentNo;
            int noOfLines = DataPickItemLines.countUnhandlesLines(smartDatabase, _documentNo);
            this.noOfLinesBox.Text = noOfLines.ToString();

            DataSet pickItemListDataSet = DataPickItemLines.getDataSet(smartDatabase, _documentNo);
            pickLinesGrid.DataSource = pickItemListDataSet.Tables[0];

            //DataSet pickList = DataPickLine.getDataSetAll(smartDatabase, _documentNo);
            //pickLinesGrid.DataSource = pickList.Tables[0];

            if (noOfLines == 0)
            {
                System.Windows.Forms.MessageBox.Show("Plocklistan kan inte vara tom.");
                this.Close();
            }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_ParentChanged(object sender, EventArgs e)
        {

        }

    }
}