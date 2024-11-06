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
    public partial class InventoryQuick : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;

        private bool okToShowQtyForm;
        private string currentEanCode;

        public InventoryQuick(SmartDatabase smartDatabase, Configuration configuration)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.configuration = configuration;

            binBox.Focus();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            button2.Top = 256;
            button2.Left = 127;
        }

        private void scanBox_GotFocus(object sender, EventArgs e)
        {
        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pickedBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pickedBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void pickedBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void binBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;
                scanBox.Focus();
            }
        }

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void scanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                currentEanCode = scanBox.Text;
                scanBox.Text = "";

                logViewList.Visible = true;


                DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
                dataInventoryItem.binCode = binBox.Text;
                dataInventoryItem.eanCode = currentEanCode;
               

                dataInventoryItem = NAVComm.getPhysInventoryItemQty(configuration, smartDatabase, this, dataInventoryItem);
                if (dataInventoryItem != null)
                {
                    this.itemNoBox.Text = dataInventoryItem.itemNo;
                    this.variantCodeBox.Text = dataInventoryItem.variantCode;
                    this.descriptionBox.Text = dataInventoryItem.description;
                    this.description2Box.Text = dataInventoryItem.description2;
                    this.brandBox.Text = dataInventoryItem.brand;
                    this.qtyBox.Text = dataInventoryItem.quantity.ToString();

                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                }
                else
                {
                    this.itemNoBox.Text = "";
                    this.variantCodeBox.Text = "";
                    this.descriptionBox.Text = "";
                    this.description2Box.Text = "";
                    this.brandBox.Text = "";
                    this.qtyBox.Text = "";
                    currentEanCode = "";

                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                }

                button2.Visible = true;
                scanBox.Text = "";
                scanBox.Focus();


                logViewList.Visible = false;
                logViewList.Items.Clear();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setQuantity(int quantity)
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = binBox.Text;
            dataInventoryItem.eanCode = currentEanCode;
            
            dataInventoryItem.quantity = quantity;

            dataInventoryItem = NAVComm.setPhysInventoryItemQty(configuration, smartDatabase, this, dataInventoryItem);
            if (dataInventoryItem != null)
            {
                this.itemNoBox.Text = dataInventoryItem.itemNo;
                this.variantCodeBox.Text = dataInventoryItem.variantCode;
                this.descriptionBox.Text = dataInventoryItem.description;
                this.description2Box.Text = dataInventoryItem.description2;
                this.brandBox.Text = dataInventoryItem.brand;
                this.qtyBox.Text = dataInventoryItem.quantity.ToString();

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

            }
            else
            {
                this.itemNoBox.Text = "";
                this.variantCodeBox.Text = "";
                this.descriptionBox.Text = "";
                this.description2Box.Text = "";
                this.brandBox.Text = "";
                this.qtyBox.Text = "";
                currentEanCode = "";

            }

            scanBox.Text = "";
            scanBox.Focus();

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.itemNoBox.Text = "";
            this.variantCodeBox.Text = "";
            this.descriptionBox.Text = "";
            this.description2Box.Text = "";
            this.brandBox.Text = "";
            this.qtyBox.Text = "";
            this.binBox.Text = "";
            this.binBox.Focus();
            button2.Visible = false;
        }

        private void qtyBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                int qty = 0;
                try
                {
                    qty = int.Parse(qtyBox.Text);
                }
                catch (Exception) { }

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = descriptionBox.Text;
                dataPickLine.description2 = description2Box.Text;
               

                QtyPad qtyPad = new QtyPad(dataPickLine, qty);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    qty = qtyPad.getValue();
                    qtyBox.Text = qty.ToString();
                }


                qtyPad.Dispose();
                scanBox.Focus();
            }
        }

        private void scanBox_GotFocus_1(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int qty = 0;
            try
            {
                qty = int.Parse(qtyBox.Text);
                setQuantity(qty);
            }
            catch (Exception) { }

            this.itemNoBox.Text = "";
            this.variantCodeBox.Text = "";
            this.descriptionBox.Text = "";
            this.description2Box.Text = "";
            this.brandBox.Text = "";
            this.qtyBox.Text = "";
            this.scanBox.Focus();

            this.button2.Visible = false;
        }

        private void itemNoBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}