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
    public partial class PickItem : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private string _documentNo;
        private DataPickLine _currentPickLine;
        private bool okToShowQtyForm = false;

        public PickItem(Configuration configuration, SmartDatabase smartDatabase, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = documentNo;

            _currentPickLine = DataPickLine.getFirstLine(smartDatabase, documentNo);
            updateView();

            button5.Visible = false;
            placeBinBox.Visible = false;

            button2.Location = new Point(4, 267);
            button2.Visible = true;
        }

        private void updateView()
        {
            if (_currentPickLine != null)
            {

                this.pickListNo.Text = _documentNo;
                this.noOfLinesBox.Text = _currentPickLine.count.ToString();
                this.brandBox.Text = _currentPickLine.brand;
                this.descriptionBox.Text = _currentPickLine.description;
                this.description2Box.Text = _currentPickLine.description2;
                this.totalBox.Text = _currentPickLine.totalQty.ToString();
                this.currentOrderBox.Text = _currentPickLine.quantity.ToString();
                this.pickedBox.Text = _currentPickLine.pickedQty.ToString();

                if (_currentPickLine.action == 0)
                {
                    this.binBox.Text = _currentPickLine.binCode;
                    this.placeBinBox.Text = _currentPickLine.placeBinCode;
                    label11.Visible = false;
                }
                if (_currentPickLine.action == 1)
                {
                    this.binBox.Text = _currentPickLine.placeBinCode;
                    this.placeBinBox.Text = _currentPickLine.binCode;
                    label11.Visible = true;
                }

                if (_currentPickLine.pickedQty == _currentPickLine.quantity)
                {
                    button5.Visible = true;
                    placeBinBox.Visible = true;
                    button2.Visible = false;

                    if ((_currentPickLine.action == 0) && ((_currentPickLine.inventory - _currentPickLine.pickedQty) <= 0))
                    {
                        if (System.Windows.Forms.MessageBox.Show("Saldot på " + _currentPickLine.binCode + ", artikel " + _currentPickLine.itemNo + " " + _currentPickLine.variantCode + " är 0. Riktigt?", "Inventering", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            System.Windows.Forms.MessageBox.Show("Räkna samtliga förekomster av artikeln på vagnen samt på plockplatsen!");

                            InventoryQuick invQuick = new InventoryQuick(smartDatabase, configuration);
                            invQuick.ShowDialog();

                            invQuick.Dispose();

                        }
                        else
                        {
                            markZeroInventoryQuantity();

                        }
                    }
                }



                scanBox.Focus();
            }
        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                DataItemCrossReference dataItemCrossRef = DataItemCrossReference.getItem(smartDatabase, this._documentNo, scanBox.Text);
                if (dataItemCrossRef != null)
                {
                    if ((dataItemCrossRef.itemNo == _currentPickLine.itemNo) && (dataItemCrossRef.variantCode == _currentPickLine.variantCode))
                    {
                        _currentPickLine.usedEanCode = dataItemCrossRef.crossReferenceCode;

                        if (_currentPickLine.pickedQty < _currentPickLine.quantity)
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                            _currentPickLine.pickedQty++;

                            updateView();
                        }
                        else
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                            System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Orderantalet är redan uppnått."));

                        }
                    }
                    else
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel artikel eller variant."));
                    }
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel artikel eller variant (EAN)."));
                }

                e.Handled = true;
                scanBox.Text = "";
                scanBox.Focus();
            }

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (_currentPickLine != null)
            {
                okToShowQtyForm = false;

                _currentPickLine.picked = true;
                _currentPickLine.placed = true;

                if (_currentPickLine.action == 1) _currentPickLine.placeBinCode = "";

                _currentPickLine.save();

                button5.Visible = false;
                placeBinBox.Visible = false;
                button2.Visible = true;

                reportPick();

                _currentPickLine = DataPickLine.getFirstLine(smartDatabase, _documentNo);
                updateView();

                okToShowQtyForm = false;

            }
            if (_currentPickLine == null)
            {
                this.Close();
            }

            
        }

        private bool reportPick()
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            bool result = NAVComm.reportPickedLine(configuration, smartDatabase, this, _currentPickLine);

            logViewList.Visible = false;

            return result;

        }


        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           
        }

        #endregion

        private void pickedBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void pickedBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pickedBox_GotFocus(object sender, EventArgs e)
        {
            if ((okToShowQtyForm) && (_currentPickLine.action == 0))
            {

                okToShowQtyForm = false;

                int qty = 0;
                try
                {
                    qty = int.Parse(pickedBox.Text);
                }
                catch (Exception) { }

                QtyPad qtyPad = new QtyPad(_currentPickLine, qty);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    if (qtyPad.getValue() <= _currentPickLine.quantity)
                    {
                        _currentPickLine.pickedQty = qtyPad.getValue();
                        pickedBox.Text = _currentPickLine.pickedQty.ToString();
                        updateView();
                        return;
                    }
                    else
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Orderantalet är redan uppnått."));
                    }
                }


                qtyPad.Dispose();
                scanBox.Focus();

            }
        }

        private void scanBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _currentPickLine.usedEanCode = _currentPickLine.getEanCode();
            

            PickOptions pickOptions = new PickOptions(smartDatabase, configuration, _currentPickLine);
            pickOptions.ShowDialog();

            if (pickOptions.checkFetchStatus())
            {
                logViewList.Items.Clear();
                logViewList.Visible = true;
                logViewList.Width = 240;
                logViewList.Height = 240;
                logViewList.Top = 24;
                logViewList.Left = 0;


                NAVComm.getFirstPickLine(configuration, smartDatabase, this, _currentPickLine.documentNo);

                logViewList.Visible = false;
              
                _currentPickLine = DataPickLine.getFirstLine(smartDatabase, _documentNo);
                updateView();
            }

            pickOptions.Dispose();
        }

        private void markZeroInventoryQuantity()
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = _currentPickLine.binCode;
            dataInventoryItem.eanCode = _currentPickLine.usedEanCode;
            dataInventoryItem.quantity = 0;

            NAVComm.logPhysInventory(configuration, smartDatabase, this, dataInventoryItem);

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _currentPickLine.pickedQty = 0;
            updateView();

            button5.Visible = false;
            button2.Visible = true;
            placeBinBox.Visible = false;

        }

    }
}