using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreItem : Form
    {
        private SmartDatabase smartDatabase;
        private int status;
        private DataBin selectedBin;
        private DataSetup dataSetup;

        public StoreItem(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataSetup = new DataSetup(smartDatabase);
            this.scanBinBox.Focus();
        }

 
        private void button1_Click_1(object sender, EventArgs e)
        {
            status = 1;
            this.Close();

        }

        public int getStatus()
        {
            return status;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = 2;
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
                    DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(packageScanBox.Text, smartDatabase);
                    if (dataWhseActivityLine.exists)
                    {
                        dataWhseActivityLine.binCode = selectedBin.code;
                        dataWhseActivityLine.locationCode = selectedBin.locationCode;
                        dataWhseActivityLine.commit();

                        //selectedBin.empty = 0;
                        //selectedBin.commit();

                        Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                        this.binCodeBox.Text = selectedBin.code;
                        this.itemNoBox.Text = dataWhseActivityLine.itemNo;
                        this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();
                        this.statusBox.Text = "Registrerad";

                        this.scanBinBox.Text = "";
                        this.packageScanBox.Text = "";
                        this.scanBinBox.Focus();
                    }
                    else
                    {
                        this.packageScanBox.Text = "";
                        Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                        this.scanBinBox.Focus();
                    }

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

    }
}