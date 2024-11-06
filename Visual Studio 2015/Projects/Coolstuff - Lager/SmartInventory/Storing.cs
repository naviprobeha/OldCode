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
    public partial class Storing : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet storeListDataSet;

        private string _documentNo = "";

        public Storing(Configuration configuration, SmartDatabase smartDatabase, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = documentNo;

            updateView();

        }

        private void updateView()
        {
            this.putAwayNoBox.Text = _documentNo;
            int noOfLines = DataStoreLine.countUnhandlesLines(smartDatabase, _documentNo);
            this.noOfLinesBox.Text = noOfLines.ToString();

            DataSet storeLineDataSet = DataStoreLine.getDataSet(smartDatabase, _documentNo);
            storeLinesGrid.DataSource = storeLineDataSet.Tables[0];
            

            scanBinBox.Focus();

            if (noOfLines == 0)
            {
                System.Windows.Forms.MessageBox.Show("Inlagringsuppdraget är klart.");
                this.Close();
            }
        }

        private void scanBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                DataStoreLine dataStoreLine = DataStoreLine.getFirstLine(smartDatabase, _documentNo, scanBinBox.Text);
                if (dataStoreLine == null)
                {
                    //if (DataStoreLine.containsEmptyBins(smartDatabase, _documentNo))
                    //{
                        dataStoreLine = new DataStoreLine(smartDatabase);
                        dataStoreLine.documentNo = _documentNo;
                    //}
                    //else
                    //{
                    //    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    //    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Lagerplatsen finns inte på uppdraget."));
                    //}
                }

                if (dataStoreLine != null)
                {
                    StoreItem storeItem = new StoreItem(configuration, smartDatabase, dataStoreLine, scanBinBox.Text);
                    storeItem.ShowDialog();

                    storeItem.Dispose();

                    updateView();
                }


                e.Handled = true;
                scanBinBox.Text = "";
                scanBinBox.Focus();
            }


        }
    }
}