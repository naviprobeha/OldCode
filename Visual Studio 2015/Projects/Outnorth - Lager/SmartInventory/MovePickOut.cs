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
    public partial class MovePickOut : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;

        private bool okToShowQtyForm;
        private string wagonCode;

        public MovePickOut(SmartDatabase smartDatabase, Configuration configuration, string wagonCode)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.configuration = configuration;

            binBox.Focus();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            //button2.Top = 256;
            //button2.Left = 127;

            this.wagonCode = wagonCode;

            okToShowQtyForm = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void itemNoBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void qtyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void qtyBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void scanBox_GotFocus_1(object sender, EventArgs e)
        {

        }

        private void scanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void binBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void binBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                binCaption.Text = binBox.Text;
                binBox.Text = "";

                e.Handled = true;
                scanBox.Focus();
            }

        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                eanCaption.Text = scanBox.Text;

                e.Handled = true;

                scanBox.Text = "";

                takeOutItem(1);                                    

            }

        }

 
        private void qtyBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void qtyBox_GotFocus_1(object sender, EventArgs e)
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

                int oldQty = qty;

                QtyPad qtyPad = new QtyPad(dataPickLine, qty);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    qty = qtyPad.getValue();
                    qtyBox.Text = qty.ToString();

                    int diff = qty - oldQty;
                    takeOutItem(diff);
                }

                

                qtyPad.Dispose();
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void takeOutItem(int quantity)
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = binCaption.Text;
            dataInventoryItem.eanCode = eanCaption.Text;
            dataInventoryItem.wagonCode = this.wagonCode;
            
            dataInventoryItem.quantity = quantity;

            dataInventoryItem = NAVComm.takeOutItem(configuration, smartDatabase, this, dataInventoryItem);
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
                this.eanCaption.Text = "";

                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

            }

            scanBox.Focus();

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        private void binBox_GotFocus(object sender, EventArgs e)
        {
            binCaption.Text = "";
        }

    }
}