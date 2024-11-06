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
    public partial class MoveStore : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet storeListDataSet;

        private string _wagonCode = "";

        public MoveStore(Configuration configuration, SmartDatabase smartDatabase, string wagonCode)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._wagonCode = wagonCode;

            updateView();

        }

        private void updateView()
        {
            this.wagonBox.Text = _wagonCode;
            int noOfLines = DataStoreLine.countUnhandlesLines(smartDatabase, _wagonCode);
            this.noOfLinesBox.Text = noOfLines.ToString();

            DataSet storeLineDataSet = DataStoreLine.getDataSet(smartDatabase, _wagonCode);
            storeLinesGrid.DataSource = storeLineDataSet.Tables[0];
            

            scanBin.Focus();

            if (noOfLines == 0)
            {
                System.Windows.Forms.MessageBox.Show("Vagnen är tom.");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanEanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                MoveStoreItem moveStoreItem = new MoveStoreItem(configuration, smartDatabase, _wagonCode, scanBin.Text);
                moveStoreItem.ShowDialog();

                moveStoreItem.Dispose();
                updateView();

                scanBin.Text = "";
                scanBin.Focus();
            }

        }
    }
}