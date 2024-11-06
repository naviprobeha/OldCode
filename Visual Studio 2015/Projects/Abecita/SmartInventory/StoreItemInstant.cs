using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreItemInstant : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataBin selectedBin;
        private DataSetup dataSetup;
        private Status status;

        public StoreItemInstant(SmartDatabase smartDatabase)
        {
            InitializeComponent();
            this.smartDatabase = smartDatabase;
            this.dataSetup = new DataSetup(smartDatabase);
            this.scanBinBox.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                this.statusBox.Text = "";
                this.itemNoBox.Text = "";
                this.sumQuantityBox.Text = "";
                this.binCodeBox.Text = "";

                DataBin dataBin = new DataBin(dataSetup.locationCode, scanBinBox.Text, smartDatabase);
                if (dataBin.exists)
                {
                    if (dataBin.blocking == 1)
                    {
                        this.binCodeBox.Text = dataBin.code;
                        this.statusBox.Text = "Spärrad";
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        this.scanBinBox.Text = "";
                    }
                    else
                    {
                        this.binCodeBox.Text = dataBin.code;
                        this.scanBinBox.Text = "";

                        DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataBin, smartDatabase);
                        if ((!dataBin.inComplete) && (dataWhseActivityLine.exists))
                        {
                            this.itemNoBox.Text = dataWhseActivityLine.itemNo;
                            this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();
                            this.statusBox.Text = "Upptagen";
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        }
                        else
                        {
                            if (!dataBin.empty)
                            {
                                this.statusBox.Text = "Upptagen";
                                Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                            }
                            else
                            {
                                selectedBin = dataBin;
                                this.packageScanBox.Focus();
                                Sound sound = new Sound(Sound.SOUND_TYPE_OK);

                            }
                        }
                    }

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                    this.scanBinBox.Text = "";
                }
            }


        }

        private void packageScanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                if (selectedBin != null)
                {
                    DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(0);
                    dataWhseActivityLine.locationCode = selectedBin.locationCode;
                    dataWhseActivityLine.binCode = selectedBin.code;
                    dataWhseActivityLine.handleUnitId = packageScanBox.Text;

                    requestStatus(dataWhseActivityLine);

                    this.scanBinBox.Text = "";
                    this.packageScanBox.Text = "";
                    this.scanBinBox.Focus();

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                    this.scanBinBox.Text = "";
                    this.packageScanBox.Text = "";
                    this.scanBinBox.Focus();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.packageScanBox.Focus();

        }

        private void requestStatus(DataWhseActivityLine dataWhseActivityLine)
        {
            listBox1.Visible = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            Service synchService = new Service("instantStoring", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(dataWhseActivityLine);

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

                    status = serviceResponse.status;

                    if (status.binContentCollection.Count == 0)
                    {
                        statusBox.Text = "Låda saknas";
                    }
                    else
                    {

                        DataBinContent binContent = (DataBinContent)status.binContentCollection[0];
                        binCodeBox.Text = binContent.binCode;
                        itemNoBox.Text = binContent.itemNo;
                        sumQuantityBox.Text = binContent.quantity.ToString();
                        //heIdBox.Text = binContent.handleUnit;

                        statusBox.Text = binContent.status;
                    }
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            button1.Enabled = true;

            this.scanBinBox.Focus();


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