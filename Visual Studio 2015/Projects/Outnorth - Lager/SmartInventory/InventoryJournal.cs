﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class InventoryJournal : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;

        private string _userNo;
        private bool okToShowQtyForm;
        private string currentEanCode;

        public InventoryJournal(SmartDatabase smartDatabase, Configuration configuration, string userNo)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.configuration = configuration;

            binBox.Focus();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            _userNo = userNo;
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
                setQuantity(false, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            finish();
            this.Close();
        }

        private void setQuantity(bool setQuantity, int quantity)
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = binBox.Text;
            dataInventoryItem.eanCode = currentEanCode;
            dataInventoryItem.setQuantity = setQuantity;
            dataInventoryItem.userNo = _userNo;

            if (setQuantity) dataInventoryItem.quantity = quantity;

            dataInventoryItem = NAVComm.addPhysInventoryItemQty(configuration, smartDatabase, this, dataInventoryItem);
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

        private void finish()
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = "EXIT";
            dataInventoryItem.eanCode = "000000";
            dataInventoryItem.userNo = _userNo;
            dataInventoryItem.quantity = 10;
            dataInventoryItem.setQuantity = false;

            dataInventoryItem = NAVComm.addPhysInventoryItemQty(configuration, smartDatabase, this, dataInventoryItem);

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
                    setQuantity(true, qty);
                }


                qtyPad.Dispose();
                scanBox.Focus();
            }
        }

        private void scanBox_GotFocus_1(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }
    }
}