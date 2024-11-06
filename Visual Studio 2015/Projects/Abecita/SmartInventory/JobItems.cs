using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class JobItems : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader dataWhseActivityHeader;
        private int freq;
        private DataSetup dataSetup;

        public JobItems(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader, int freq)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;
            this.freq = freq;
            this.dataSetup = new DataSetup(smartDatabase);


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
            this.scanBinBox.Focus();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
            DataSet dataSet = dataWhseActivityLines.getJobDataSet(this.dataWhseActivityHeader, this.freq, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);

            lineGrid.DataSource = dataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                //DataBin dataBin = new DataBin(dataSetup.locationCode, this.scanBinBox.Text);
                DataItemUnit dataItemUnit = new DataItemUnit(scanBinBox.Text);
                //DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, dataBin, smartDatabase);
                DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataWhseActivityHeader, freq, 0, dataItemUnit, smartDatabase);
                if ((dataWhseActivityLine.exists) && (requestStatus(dataWhseActivityLine)))
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                    DataWhseActivityLine dataWhseActivityPlaceLine = new DataWhseActivityLine(dataWhseActivityLine, smartDatabase);

                    dataWhseActivityLine.status = 1;
                    dataWhseActivityPlaceLine.status = 1;

                    dataWhseActivityLine.commit();
                    dataWhseActivityPlaceLine.commit();


                    this.scanBinBox.Text = "";
                    updateGrid();

                    this.scanBinBox.Focus();

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    this.scanBinBox.Text = "";
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool requestStatus(DataWhseActivityLine dataWhseActivityLine)
        {
            listBox1.Visible = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service synchService = new Service("jobMoveStart", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(dataWhseActivityLine);

            bool result = false;

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Förfrågan misslyckades.");
                }
                else
                {
                    write("Förfrågan klar.");
                    listBox1.Visible = false;
                    listBox1.Items.Clear();
                    result = true;
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            return result;
        }

        #region Logger Members

        public void write(string message)
        {
            listBox1.Items.Add(message);
            Application.DoEvents();

        }

        #endregion
    }
}