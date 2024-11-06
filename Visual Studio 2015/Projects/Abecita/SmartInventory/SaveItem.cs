using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class SaveItem : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private int freq;
        private DataWhseActivityLine dataWhseActivityLine;
        private DataSetup dataSetup;
        
        public SaveItem(SmartDatabase smartDatabase, int freq, DataWhseActivityLine dataWhseActivityLine)
        {
            this.smartDatabase = smartDatabase;
            this.freq = freq;
            this.dataSetup = new DataSetup(smartDatabase);

            this.dataWhseActivityLine = dataWhseActivityLine;
            
            InitializeComponent();

            switch (freq)
            {
                case 1:
                    {
                        label4.Text = "Frekvensklass: Högfrekvent";
                        break;
                    }
                case 2:
                    {
                        label4.Text = "Frekvensklass: Mellanfrekvent";
                        break;
                    }
                case 3:
                    {
                        label4.Text = "Frekvensklass: Lågfrekvent";
                        break;
                    }
                case 4:
                    {
                        label4.Text = "Frekvensklass: Mkt lågfrekvent";
                        break;
                    }
                case 5:
                    {
                        label4.Text = "Frekvensklass: Storvolymigt";
                        break;
                    }
            }


            this.heIdBox.Text = dataWhseActivityLine.handleUnitId;
            this.itemNoBox.Text = dataWhseActivityLine.itemNo;
            this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();

            this.scanLocationBox.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanLocationBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                DataBin dataBin = new DataBin(dataSetup.locationCode, scanLocationBox.Text, smartDatabase);
                if (dataBin.exists)
                {
                    if (dataBin.blocking == 1)
                    {
                        //this.binCodeBox.Text = dataBin.code;
                        this.statusBox.Text = "Spärrad";
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        this.scanLocationBox.Text = "";
                    }
                    else
                    {
                        //this.binCodeBox.Text = dataBin.code;
                        this.scanLocationBox.Text = "";

                        if (!dataBin.empty)
                        {
                            this.statusBox.Text = "Upptagen";
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        }
                        else
                        {
                            this.dataWhseActivityLine.binCode = dataBin.code;

                            if (requestStatus(dataWhseActivityLine))
                            {
                                this.dataWhseActivityLine.commit();
                                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                                this.Close();
                            }
                            else
                            {
                                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                                this.scanLocationBox.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                    this.scanLocationBox.Text = "";
                }

            }

        }


        private bool requestStatus(DataWhseActivityLine dataWhseActivityLine)
        {
            listBox1.Visible = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service synchService = new Service("jobMoveEnd", smartDatabase);
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