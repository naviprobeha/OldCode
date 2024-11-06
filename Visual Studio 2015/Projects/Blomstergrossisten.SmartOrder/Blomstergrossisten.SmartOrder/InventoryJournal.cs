using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SmartOrder
{
    public partial class InventoryJournal : Form, Logger
    {
        private SmartDatabase smartDatabase;

        private bool okToShowQtyForm;
        private string currentEanCode;

        public InventoryJournal(SmartDatabase smartDatabase)
        {
            InitializeComponent();


            this.smartDatabase = smartDatabase;

            this.scanBox.Focus();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                currentEanCode = scanBox.Text;
                scanBox.Text = "";
                setQuantity(false, 0);
            }
        }

        private void setQuantity(bool setQuantity, int quantity)
        {
            logViewList.Visible = true;


            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.eanCode = currentEanCode;
            dataInventoryItem.setQuantity = setQuantity;

            if (setQuantity) dataInventoryItem.quantity = quantity;

            dataInventoryItem = addPhysInventoryItemQty(smartDatabase, this, dataInventoryItem);
            if (dataInventoryItem != null)
            {
                this.itemNoBox.Text = dataInventoryItem.itemNo;
                this.descriptionBox.Text = dataInventoryItem.description;
                this.description2Box.Text = dataInventoryItem.description2;
                this.qtyBox.Text = dataInventoryItem.quantity.ToString();

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

            }
            else
            {
                this.itemNoBox.Text = "";
                this.descriptionBox.Text = "";
                this.description2Box.Text = "";
                this.qtyBox.Text = "";
                currentEanCode = "";

            }

            scanBox.Text = "";
            scanBox.Focus();

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        public DataInventoryItem addPhysInventoryItemQty(SmartDatabase smartDatabase, Logger logger, DataInventoryItem dataInventoryItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service service = new Service("addPhysInventoryItemQty", smartDatabase);
            service.setLogger(logger);
            service.serviceRequest.setServiceArgument(dataInventoryItem);

            ServiceResponse serviceResponse = service.performService();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Show();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.description);
                }
                else
                {
                    XmlElement docLineElement = (XmlElement)serviceResponse.responseDoc.SelectSingleNode("serviceResponse/docLine");

                    dataInventoryItem = new DataInventoryItem(smartDatabase);


                    XmlElement itemNoElement = (XmlElement)docLineElement.SelectSingleNode("itemNo");
                    if (itemNoElement.FirstChild != null)
                    {
                        dataInventoryItem.itemNo = itemNoElement.FirstChild.Value;
                    }
                    XmlElement descriptionElement = (XmlElement)docLineElement.SelectSingleNode("description");
                    if (descriptionElement.FirstChild != null)
                    {
                        dataInventoryItem.description = descriptionElement.FirstChild.Value;
                    }
                    XmlElement description2Element = (XmlElement)docLineElement.SelectSingleNode("description2");
                    if (description2Element.FirstChild != null)
                    {
                        dataInventoryItem.description2 = description2Element.FirstChild.Value;
                    }
                    XmlElement quantityElement = (XmlElement)docLineElement.SelectSingleNode("quantity");
                    if (quantityElement.FirstChild != null)
                    {
                        dataInventoryItem.quantity = float.Parse(quantityElement.FirstChild.Value);
                    }

                    return dataInventoryItem;

                }

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            return null;

        }


        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void scanBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

        private void qtyBox_GotFocus_1(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                okToShowQtyForm = false;

                DataItem dataItem = new DataItem(itemNoBox.Text, smartDatabase);

                QuantityForm qtyPad = new QuantityForm(dataItem);
                qtyPad.setValue(qtyBox.Text);
                qtyPad.ShowDialog();

                if (qtyPad.getStatus() == 1)
                {
                    qtyBox.Text = qtyPad.getValue("{0:f}");

                    int qty = 0;
                    try
                    {
                        qty = int.Parse(qtyBox.Text);
                    }
                    catch (Exception) { }

                    setQuantity(true, qty);
                }


                qtyPad.Dispose();
                scanBox.Focus();
            }
        }
    }
}