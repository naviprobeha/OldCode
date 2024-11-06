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
    public partial class StoreItem : Form, Logger
    {

        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private DataStoreLine _currentStoreLine;
        private string _binCode;

        private bool okToShowQtyForm;


        public StoreItem(Configuration configuration, SmartDatabase smartDatabase, DataStoreLine currentStoreLine, string binCode)
        {
            InitializeComponent();
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._currentStoreLine = currentStoreLine;
            this._binCode = binCode;

            _currentStoreLine.pickedQty = _currentStoreLine.quantity;

            updateView();

            okToShowQtyForm = true;

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            button2.Visible = false;
        }

        private void updateView()
        {
            this.putAwayNoBox.Text = _currentStoreLine.documentNo;
            this.binCodeBox.Text = _binCode;
            this.descriptionBox.Text = _currentStoreLine.description;
            this.description2Box.Text = _currentStoreLine.description2;
            this.qtyBox.Text = _currentStoreLine.pickedQty.ToString();

            if (_currentStoreLine.itemNo == "")
            {
                //scanEanBox.Visible = true;
                //scanItemLabel.Visible = true;
                //scanEanBox.Focus();
            }
            scanEanBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void qtyBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataPickLine dataPickLine = new DataPickLine(smartDatabase);
                dataPickLine.description = descriptionBox.Text;
                dataPickLine.description2 = description2Box.Text;


                //QtyPad qtyPad = new QtyPad(dataPickLine, (int)_currentStoreLine.pickedQty);
                QtyPad qtyPad = new QtyPad(dataPickLine, (float)_currentStoreLine.pickedQty);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    if (qtyPad.getValue() > _currentStoreLine.quantity)
                    {
                        System.Windows.Forms.MessageBox.Show("Max. antal på den här raden är " + _currentStoreLine.quantity);
                    }
                    else
                    {
                        _currentStoreLine.pickedQty = qtyPad.getValue2();
                    }
                    updateView();
                }


                qtyPad.Dispose();
                this.scanEanBox.Focus();
                //button2.Focus();


            }
        }

        private void report()
        {
            logViewList.Visible = true;

            
            if (NAVComm.reportStoredLine(configuration, smartDatabase, this, _currentStoreLine))
            {

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                this.Close();
            }
            else
            {

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

        private void button2_Click(object sender, EventArgs e)
        {
            report();


        }

        private void scanEanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                DataItemCrossReference dataItemCrossRef = DataItemCrossReference.getItem(smartDatabase, _currentStoreLine.documentNo, scanEanBox.Text);
                if (dataItemCrossRef != null)
                {
                    //if (this._currentStoreLine.itemNo == "")
                    //{
                        DataStoreLine dataStoreLine = DataStoreLine.getFirstItemLine(smartDatabase, _currentStoreLine.documentNo, dataItemCrossRef.itemNo, dataItemCrossRef.variantCode);
                        if (dataStoreLine != null)
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                            dataStoreLine.binCode = this._binCode;
                            _currentStoreLine = dataStoreLine;
                            _currentStoreLine.pickedQty = _currentStoreLine.quantity;

                            updateView();

                            button2.Visible = true;
                        }
                        else
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                            System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Ingen artikel funnen."));

                        }
                    /*
                    }
                    else
                    {                       
                        if ((_currentStoreLine.itemNo == dataItemCrossRef.itemNo) && (_currentStoreLine.variantCode == dataItemCrossRef.variantCode))
                        {
                            button2.Visible = true;
                        }
                        else
                        {
                            _currentStoreLine = 
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                            System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel artikel eller variant."));

                        }
                    }
                     */
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel artikel eller variant."));
                }

                e.Handled = true;
                scanEanBox.Text = "";
                scanEanBox.Focus();
            }
        }

        private void qtyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void scanEanBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }
    }
}