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
    public partial class PickUndoItem : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private string _documentNo;
        private DataPickLine _currentPickLine;


        public PickUndoItem(Configuration configuration, SmartDatabase smartDatabase, DataPickLine _dataPickLine)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = _dataPickLine.documentNo;

            _currentPickLine = _dataPickLine;

            button5.Visible = false;
            binBox.Visible = false;

            updateView();

        }

        private void updateView()
        {
            if (_currentPickLine != null)
            {

                this.pickListNo.Text = _documentNo;
                this.noOfLinesBox.Text = _currentPickLine.count.ToString();
                this.binBox.Text = _currentPickLine.binCode;
                this.brandBox.Text = _currentPickLine.brand;
                this.descriptionBox.Text = _currentPickLine.description;
                this.description2Box.Text = _currentPickLine.description2;
                this.currentOrderBox.Text = _currentPickLine.pickedQty.ToString();
                this.pickedBox.Text = _currentPickLine.undoQty.ToString();
                this.placeBinBox.Text = _currentPickLine.placeBinCode;

                if (_currentPickLine.undoQty == _currentPickLine.pickedQty)
                {
                    button5.Visible = true;
                    binBox.Visible = true;
                }

                scanBox.Focus();
            }
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

        private void scanBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (_currentPickLine != null)
            {
                _currentPickLine.picked = true;
                _currentPickLine.placed = true;
                _currentPickLine.save();

                button5.Visible = false;
                placeBinBox.Visible = false;

                reportUndoPick();

                _currentPickLine = DataPickLine.getFirstLine(smartDatabase, _documentNo);
                updateView();
            }
            if (_currentPickLine == null)
            {
                button5.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void scanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                DataItemCrossReference dataItemCrossRef = DataItemCrossReference.getItem(smartDatabase, this._documentNo, scanBox.Text);
                if (dataItemCrossRef != null)
                {
                    if ((dataItemCrossRef.itemNo == _currentPickLine.itemNo) && (dataItemCrossRef.variantCode == _currentPickLine.variantCode))
                    {
                        if (_currentPickLine.undoQty < _currentPickLine.pickedQty)
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                            _currentPickLine.undoQty++;

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
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel artikel eller variant."));
                }

                e.Handled = true;
                scanBox.Text = "";
                scanBox.Focus();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool reportUndoPick()
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            bool result = NAVComm.reportUndoLine(configuration, smartDatabase, this, _currentPickLine);

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

        private void button5_Click(object sender, EventArgs e)
        {
            if (_currentPickLine != null)
            {
                _currentPickLine.picked = true;
                _currentPickLine.placed = true;
                _currentPickLine.save();

                button5.Visible = false;
                placeBinBox.Visible = false;

                reportUndoPick();

                _currentPickLine = DataPickLine.getFirstLine(smartDatabase, _documentNo);
                updateView();
            }
            if (_currentPickLine == null)
            {
                button5.Visible = false;
            }
        }
    }
}