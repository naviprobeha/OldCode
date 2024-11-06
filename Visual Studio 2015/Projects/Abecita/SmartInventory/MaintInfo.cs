using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintInfo : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private Status status;

        public MaintInfo(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataSetup = new DataSetup(smartDatabase);

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            QuantityForm quantityForm = new QuantityForm(itemNoBox.Text, descriptionBox.Text, smartDatabase);
            quantityForm.setValue(sumQuantityBox.Text);
            quantityForm.ShowDialog();
            if (quantityForm.getStatus() == 1)
            {
                sumQuantityBox.Text = quantityForm.getValue();
                setQuantity();
            }

            quantityForm.Dispose();
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
                this.heIdBox.Text = "";
                this.descriptionBox.Text = "";
                this.binCodeBox.Text = "";

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

            binDepartment.Items.Clear();

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
                        statusBox.Text = "Tom";
                    }
                    else
                    {

                        if (status.binContentCollection.Count > 1)
                        {
                            int i = 0;
                            binDepartment.Items.Clear();
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

        private void setQuantity()
        {
            listBox1.Visible = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            button2.Enabled = false;
            Service synchService = new Service("setQuantity", smartDatabase);
            synchService.setLogger(this);

            DataBinContent dataBinContent = new DataBinContent();
            dataBinContent.binCode = this.binCodeBox.Text;
            dataBinContent.locationCode = dataSetup.locationCode;
            dataBinContent.itemNo = this.itemNoBox.Text;
            dataBinContent.description = this.descriptionBox.Text;
            dataBinContent.handleUnit = this.heIdBox.Text;
            dataBinContent.quantity = float.Parse(this.sumQuantityBox.Text);

            synchService.serviceRequest.setServiceArgument(dataBinContent);

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


                    Status currentStatus = serviceResponse.status;

                    if (currentStatus.binContentCollection.Count == 0)
                    {
                        //System.Windows.Forms.MessageBox.Show("Fatalt fel...");
                    }
                    else
                    {

                        DataBinContent binContent = (DataBinContent)currentStatus.binContentCollection[0];


                        itemNoBox.Text = binContent.itemNo;
                        descriptionBox.Text = binContent.description;
                        sumQuantityBox.Text = binContent.quantity.ToString();
                        heIdBox.Text = binContent.handleUnit;

                        if (binDepartment.SelectedIndex > -1)
                            ((DataBinContent)status.binContentCollection[binDepartment.SelectedIndex]).quantity = binContent.quantity;
                        else
                            ((DataBinContent)status.binContentCollection[0]).quantity = binContent.quantity;


                        DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
                        if (itemStore.exists)
                        {
                            statusBox.Text = "Uttagen - Flytt";
                        }
                        else
                        {
                            if (binContent.status != "")
                            {
                                statusBox.Text = binContent.status;
                            }
                            else
                            {
                                statusBox.Text = "Upptagen";
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

            button1.Enabled = true;
            button2.Enabled = true;

            this.scanBinBox.Focus();

        }

        #region Logger Members

        public void write(string message)
        {
            listBox1.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void MaintInfo_Load(object sender, EventArgs e)
        {
            scanBinBox.Focus();

        }

        private void binDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void binDepartment_SelectedValueChanged(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}