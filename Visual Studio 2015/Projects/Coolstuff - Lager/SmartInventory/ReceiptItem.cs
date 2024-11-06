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
    public partial class ReceiptItem : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private DataReceiptLine _currentReceiptLine;

        private bool okToShowQtyForm;
        private int _currentItem = 1;
        private int _itemCount;


        public ReceiptItem(Configuration configuration, SmartDatabase smartDatabase, DataReceiptLine currentReceiptLine)
        {
            InitializeComponent();
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._currentReceiptLine = currentReceiptLine;

            updateView();

            okToShowQtyForm = true;

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

        }

        private void updateView()
        {
            _itemCount = DataReceiptLine.countItemLines(smartDatabase, _currentReceiptLine.documentNo, _currentReceiptLine.itemNo, _currentReceiptLine.variantCode);

            label1.Text = "Inleverans - Artikel (" + _currentItem + "/" + _itemCount + ")";

            this.receiptNoBox.Text = _currentReceiptLine.documentNo;
            this.itemNoBox.Text = _currentReceiptLine.itemNo;
            this.descriptionBox.Text = _currentReceiptLine.description;
            this.weightBox.Text = _currentReceiptLine.weight.ToString();
            this.lengthBox.Text = _currentReceiptLine.length.ToString();
            this.heightBox.Text = _currentReceiptLine.height.ToString();
            this.widthBox.Text = _currentReceiptLine.width.ToString();
            this.qtyBox.Text = _currentReceiptLine.quantity.ToString();
            this.qtyToReceiveBox.Text = _currentReceiptLine.qtyToReceive.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button2_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }


        private void report()
        {
            logViewList.Visible = true;

            /*
            if (NAVComm.reportStoredLine(configuration, smartDatabase, this, _currentStoreLine))
            {

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                this.Close();
            }
            else
            {

                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

            }
            */
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

        private void button2_Click(object sender, EventArgs e)
        {
            report();


        }

 

        private void qtyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void scanEanBox_GotFocus(object sender, EventArgs e)
        {
            
        }

        private void qtyToReceiveBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = _currentReceiptLine.description;
                dataPickLine.description2 = _currentReceiptLine.description2;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentReceiptLine.qtyToReceive);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    if (qtyPad.getValue() > _currentReceiptLine.quantity)
                    {
                        System.Windows.Forms.MessageBox.Show("Max. antal på den här raden är " + _currentReceiptLine.quantity);
                    }
                    else
                    {
                        _currentReceiptLine.qtyToReceive = qtyPad.getValue2();
                    }
                    updateView();
                }


                qtyPad.Dispose();


            }

            itemNoBox.Focus();
        }

        private void itemNoBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void weightBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = _currentReceiptLine.description;
                dataPickLine.description2 = _currentReceiptLine.description2;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentReceiptLine.weight);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    _currentReceiptLine.weight = qtyPad.getValue2();
                    updateView();
                }


                qtyPad.Dispose();
            }

            itemNoBox.Focus();
        }

        private void lengthBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = _currentReceiptLine.description;
                dataPickLine.description2 = _currentReceiptLine.description2;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentReceiptLine.length);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    _currentReceiptLine.length = qtyPad.getValue2();
                    updateView();
                }


                qtyPad.Dispose();
            }

            itemNoBox.Focus();
        }

        private void widthBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = _currentReceiptLine.description;
                dataPickLine.description2 = _currentReceiptLine.description2;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentReceiptLine.width);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    _currentReceiptLine.width = qtyPad.getValue2();
                    updateView();
                }


                qtyPad.Dispose();
            }

            itemNoBox.Focus();
        }

        private void heightBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = _currentReceiptLine.description;
                dataPickLine.description2 = _currentReceiptLine.description2;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentReceiptLine.height);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    _currentReceiptLine.height = qtyPad.getValue2();
                    updateView();
                }


                qtyPad.Dispose();
            }

            itemNoBox.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            _currentReceiptLine.save();

            NAVComm.reportReceiptLine(configuration, smartDatabase, this, _currentReceiptLine);

            logViewList.Visible = false;

            if (_itemCount > _currentItem)
            {
                _currentItem = _currentItem + 1;
                _currentReceiptLine = DataReceiptLine.getNextItemLine(smartDatabase, _currentReceiptLine.documentNo, _currentReceiptLine.itemNo, _currentReceiptLine.variantCode, _currentReceiptLine.lineNo);
                updateView();
            }
            else
            {
                Close();
            }

        }


        #region Logger Members

        void Logger.write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           

        }

        #endregion
    }
}