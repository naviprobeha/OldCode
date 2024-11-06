using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintMoveItem : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private Status status;

        public MaintMoveItem(SmartDatabase smartDatabase)
        {
            InitializeComponent();
            this.smartDatabase = smartDatabase;
            dataSetup = new DataSetup(smartDatabase);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void binDepartment_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void descriptionBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MaintMoveItem_Load(object sender, EventArgs e)
        {
            scanBinBox.Focus();
        }

        private void scanBinBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                clearAllFields();

                DataBin dataBin = new DataBin(dataSetup.locationCode, scanBinBox.Text, smartDatabase);
                if (dataBin.exists)
                {
                    if (dataBin.blocking == 1)
                    {
                        this.binCodeBox.Text = dataBin.code;
                        this.statusBox.Text = "Spärrad";
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    }
                    else
                    {
                        this.binCodeBox.Text = dataBin.code;

                        requestStatus(dataBin);

                    }

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                }
                this.scanBinBox.Text = "";
            }

        }

        private void requestStatus(DataBin dataBin)
        {
            listBox1.Visible = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            button2.Enabled = false;
            Service synchService = new Service("statusRequest", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(dataBin);

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
                        System.Windows.Forms.MessageBox.Show("Lagerplats " + dataBin.code + " finns inte.");
                    }
                    else
                    {

                        if (status.binContentCollection.Count > 1)
                        {
                            int i = 0;
                            while (i < status.binContentCollection.Count)
                            {
                                binDepartment.Items.Add("" + (i + 1));
                                i++;
                            }
                            binDepartment.SelectedIndex = 0;
                        }
                        else
                        {
                            DataBinContent binContent = (DataBinContent)status.binContentCollection[0];
                            binCodeBox.Text = binContent.binCode;
                            itemNoBox.Text = binContent.itemNo;
                            descriptionBox.Text = binContent.description;
                            sumQuantityBox.Text = binContent.quantity.ToString();
                            heIdBox.Text = binContent.handleUnit;

                            DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
                            if (itemStore.exists)
                            {
                                statusBox.Text = "Uttagen - Flytt";
                                button1.Enabled = false;
                            }
                            else
                            {
                                if (binContent.status != "")
                                {
                                    statusBox.Text = binContent.status;
                                    button1.Enabled = false;
                                }
                                else
                                {
                                    statusBox.Text = "Upptagen";
                                    button1.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            button2.Enabled = true;

            this.scanBinBox.Focus();

        }

        private void binDepartment_SelectedValueChanged_1(object sender, EventArgs e)
        {
            DataBinContent currentBinContent = (DataBinContent)status.binContentCollection[binDepartment.SelectedIndex];

            this.binCodeBox.Text = currentBinContent.binCode;
            this.itemNoBox.Text = currentBinContent.itemNo;
            this.descriptionBox.Text = currentBinContent.description;
            this.sumQuantityBox.Text = currentBinContent.quantity.ToString();
            this.heIdBox.Text = currentBinContent.handleUnit;

            DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
            if (itemStore.exists)
            {
                statusBox.Text = "Uttagen - Flytt";
                button1.Enabled = false;
            }
            else
            {
                if (currentBinContent.status != "")
                {
                    statusBox.Text = currentBinContent.status;
                    button1.Enabled = false;
                }
                else
                {
                    statusBox.Text = "Upptagen";
                    button1.Enabled = true;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
            itemStore.itemNo = itemNoBox.Text;
            itemStore.handleUnitId = heIdBox.Text;
            itemStore.quantity = float.Parse(sumQuantityBox.Text);
            itemStore.commit();

            Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

            clearAllFields();
            scanBinBox.Focus();

        }

        private void clearAllFields()
        {
            this.statusBox.Text = "";
            this.itemNoBox.Text = "";
            this.sumQuantityBox.Text = "";
            this.binCodeBox.Text = "";
            this.heIdBox.Text = "";
            this.descriptionBox.Text = "";
            this.binCodeBox.Text = "";

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