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
    public partial class MoveStoreItem : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private string _binCode;
        private string _wagonCode;
        private string _eanCode;

        private bool okToShowQtyForm;


        public MoveStoreItem(Configuration configuration, SmartDatabase smartDatabase, string wagonCode, string binCode)
        {
            InitializeComponent();
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            _binCode = binCode;
            _wagonCode = wagonCode;

            binBox.Text = _binCode;
            wagonBox.Text = _wagonCode;

            okToShowQtyForm = true;

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            okToShowQtyForm = true;

            scanEanBox.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_GotFocus(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void qtyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void qtyBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void scanEanBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void scanEanBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            logViewList.Visible = true;
            NAVComm.getMoveStoreLines(configuration, smartDatabase, this, _wagonCode);
            logViewList.Visible = false;


            this.Close();
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

                    storeItem(qty - oldQty);
                }


                qtyPad.Dispose();
                scanEanBox.Focus();
            }


        }

        private void storeItem(float quantity)
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = _binCode;
            dataInventoryItem.eanCode = _eanCode;
            dataInventoryItem.wagonCode = _wagonCode;

            dataInventoryItem.quantity = quantity;

            dataInventoryItem = NAVComm.storeItem(configuration, smartDatabase, this, dataInventoryItem);
            if (dataInventoryItem != null)
            {
                this.descriptionBox.Text = dataInventoryItem.description;
                this.description2Box.Text = dataInventoryItem.description2;
                this.qtyBox.Text = dataInventoryItem.quantity.ToString();

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

            }
            else
            {
                this.descriptionBox.Text = "";
                this.description2Box.Text = "";
                this.qtyBox.Text = "";

                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

            }

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void scanBinBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void scanEanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;
                _eanCode = scanEanBox.Text;
                scanEanBox.Text = "";

                storeItem(1);

                scanEanBox.Focus();
            }

        }
    }
}