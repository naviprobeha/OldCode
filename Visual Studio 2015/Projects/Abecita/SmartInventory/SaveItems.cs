using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class SaveItems : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader dataWhseActivityHeader;
        private int freq;

        public SaveItems(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader, int freq)
        {
            this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;
            this.freq = freq;
            
            InitializeComponent();

            label1.Text = label1.Text + " " + dataWhseActivityHeader.no;

            switch (freq)
            {
                case 1:
                    {
                        label2.Text = "Frekvensklass: Högfrekvent";
                        break;
                    }
                case 2:
                    {
                        label2.Text = "Frekvensklass: Mellanfrekvent";
                        break;
                    }
                case 3:
                    {
                        label2.Text = "Frekvensklass: Lågfrekvent";
                        break;
                    }
                case 4:
                    {
                        label2.Text = "Frekvensklass: Mkt lågfrekvent";
                        break;
                    }
                case 5:
                    {
                        label2.Text = "Frekvensklass: Storvolymigt";
                        break;
                    }
            }


            updateGrid();
            heIdBox.Focus();

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
            DataSet dataSet = dataWhseActivityLines.getJobDataSet(this.dataWhseActivityHeader, this.freq, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);

            lineGrid.DataSource = dataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void heIdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                DataItemUnit dataItemUnit = new DataItemUnit(heIdBox.Text);

                DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, DataWhseActivityLines.WHSE_ACTION_PLACE, dataItemUnit, smartDatabase);
                if (dataWhseActivityLine.exists)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_OK);

                    SaveItem saveItem = new SaveItem(smartDatabase, freq, dataWhseActivityLine);
                    saveItem.ShowDialog();

                    heIdBox.Text = "";
                    updateGrid();
                    heIdBox.Focus();
                }
                else
                {
                    heIdBox.Text = "";
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                }
            }

        }


    }
}