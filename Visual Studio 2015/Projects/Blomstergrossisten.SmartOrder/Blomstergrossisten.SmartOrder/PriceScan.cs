using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class PriceScan : Form
    {
        private SmartDatabase smartDatabase;
        private bool showPad;
        private DataItem currentDataItem;

        public PriceScan(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            scanCode.Focus();

            showPad = true;
        }

        private void scanCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void scanBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                currentDataItem = null;

                currentDataItem = new DataItem(scanCode.Text, smartDatabase, true);
                if (currentDataItem.barCodeFound)
                {
                    itemNoBox.Text = currentDataItem.no;
                    descriptionBox.Text = currentDataItem.description;
                    quantityBox.Text = currentDataItem.defaultQuantity.ToString();

                    unitPriceBox.Text = String.Format("{0:f}", currentDataItem.price);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    scanCode.Text = "";
                    scanCode.Focus();

                }
                else
                {

                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                    scanCode.Text = "";

                    itemNoBox.Text = "";
                    descriptionBox.Text = "";
                    quantityBox.Text = "";
                    unitPriceBox.Text = "";

                    scanCode.Focus();

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();


                }

            }
        }

        private void quantityBox_GotFocus(object sender, EventArgs e)
        {
        }

        private void unitPriceBox_GotFocus(object sender, EventArgs e)
        {
            if (showPad)
            {
                showPad = false;
                QuantityForm quantityForm = new QuantityForm(currentDataItem);
                quantityForm.setCaption("A-pris:");
                quantityForm.setValue(unitPriceBox.Text);
                quantityForm.ShowDialog();
                if (quantityForm.getStatus() == 1)
                {
                    unitPriceBox.Text = quantityForm.getValue("{0:f}");

                    if (unitPriceBox.Text == "") unitPriceBox.Text = "0";
                }
                quantityForm.Dispose();
                updateItemPrice();
            }
            scanCode.Focus();
        }

        private void scanCode_GotFocus(object sender, EventArgs e)
        {
            showPad = true;
        }

        private void updateItemPrice()
        {
            currentDataItem.price = float.Parse(unitPriceBox.Text);
            currentDataItem.commit();


            Service synchService = new Service("updateItemPrice", smartDatabase);
            synchService.setLogger(null);

            synchService.serviceRequest.setServiceArgument(currentDataItem);

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                }
            }

        }

    }
}