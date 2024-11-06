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
    public partial class Receive : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private int _documentType;
        private string _documentNo;
        private DataSet purchLineDataSet;
        

        public Receive(Configuration configuration, SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentType = documentType;
            this._documentNo = documentNo;

            label1.Text = Translation.translate(configuration.languageCode, "EAN:");
            label2.Text = Translation.translate(configuration.languageCode, "Scanna varje artikels EAN-kod.");

            updateGrid();

            itemNoCol.HeaderText = Translation.translate(configuration.languageCode, "Artikelnr");
            variantCodeCol.HeaderText = Translation.translate(configuration.languageCode, "Variantkod");
            descriptionCol.HeaderText = Translation.translate(configuration.languageCode, "Beskrivning");
            unitOfMeasureCol.HeaderText = Translation.translate(configuration.languageCode, "Enhet");
            quantityCol.HeaderText = Translation.translate(configuration.languageCode, "Antal");
            quantityReceivedCol.HeaderText = Translation.translate(configuration.languageCode, "Inlev. antal");
            quantityToReceiveCol.HeaderText = Translation.translate(configuration.languageCode, "Antal att inlev.");

            button1.Text = Translation.translate(configuration.languageCode, "Avbryt");
            button11.Text = Translation.translate(configuration.languageCode, "Bokför");
            button2.Text = Translation.translate(configuration.languageCode, "Ändra");

            this.Text = Translation.translate(configuration.languageCode, "Receive");
        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            int currentRow = purchLineGrid.CurrentRowIndex;

            purchLineDataSet = DataPurchaseLine.getDataSet(smartDatabase, _documentType, _documentNo);

            purchLineGrid.DataSource = purchLineDataSet.Tables[0];

            if (currentRow > 0)
            {
                purchLineGrid.CurrentRowIndex = currentRow;
            }

            int qty = DataPurchaseLine.sumQuantity(smartDatabase, _documentType, _documentNo);
            int qtyToReceive = DataPurchaseLine.sumQuantityToReceive(smartDatabase, _documentType, _documentNo);
            int qtyReceived = DataPurchaseLine.sumQuantityReceived(smartDatabase, _documentType, _documentNo);
            qtyBox.Text = ((qty - qtyReceived) - qtyToReceive).ToString();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }

   
        #region Logger Members

        public void write(string message)
        {
            //logViewBox.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                if (DataPurchaseLine.checkLineExists(smartDatabase, _documentType, _documentNo, eanBox.Text))
                {
                    DataPurchaseLine dataPurchaseLine = DataPurchaseLine.getAvailableLineFromEan(smartDatabase, _documentType, _documentNo, eanBox.Text);
                    if (dataPurchaseLine != null)
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                        dataPurchaseLine.quantityToReceive = dataPurchaseLine.quantityToReceive + 1;
                        dataPurchaseLine.save();

                        DataScanLine.updatePurchase(smartDatabase, dataPurchaseLine);

                        updateGrid();
                    }
                    else
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        dataPurchaseLine = DataPurchaseLine.getLineFromEan(smartDatabase, _documentType, _documentNo, eanBox.Text);
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader med den här artikeln är redan inlevererade:") + " " + dataPurchaseLine.itemNo+", "+dataPurchaseLine.variantCode);
                    }
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns ingen artikel med den här EAN-koden på ordern:") + " " + eanBox.Text);
                }

                e.Handled = true;
                eanBox.Text = "";
                eanBox.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (purchLineGrid.BindingContext[purchLineGrid.DataSource, ""].Count > 0)
            {
                int entryNo = (int)purchLineDataSet.Tables[0].Rows[purchLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
                DataPurchaseLine dataPurchaseLine = DataPurchaseLine.getLineFromEntryNo(smartDatabase, entryNo);

                QtyPad qtyPad = new QtyPad(configuration, smartDatabase, dataPurchaseLine.itemNo, dataPurchaseLine.variantCode, dataPurchaseLine.description, dataPurchaseLine.quantityToReceive);

                qtyPad.ShowDialog();
                if (qtyPad.getStatus() == 1)
                {
                    //if (qtyPad.getQuantity() <= (dataPurchaseLine.quantity - dataPurchaseLine.quantityReceived))
                    //{

                        dataPurchaseLine.quantityToReceive = qtyPad.getQuantity();
                        dataPurchaseLine.save();

                        DataScanLine.updatePurchase(smartDatabase, dataPurchaseLine);

                        updateGrid();
                    //}
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "För hög kvantitet."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    //}

                }

                qtyPad.Dispose();

            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga orderrader."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
            eanBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Vill du bokföra inleveransen?"), Translation.translate(configuration.languageCode, "Bokföring"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                Service service = new Service("postPurchaseReceipt", smartDatabase, configuration);
                service.setLogger(this);
                service.serviceRequest.setServiceArgument(new Document(smartDatabase, 0, _documentType, _documentNo, purchLineDataSet));
                ServiceResponse serviceResponse = service.performService();

                if (serviceResponse != null)
                {
                    if (serviceResponse.hasErrors)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        System.Windows.Forms.Cursor.Hide();

                        System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    }
                    else
                    {
                        DataScanLine.deleteDocument(smartDatabase, 0, _documentType, _documentNo);
                        DataPurchaseLine.delete(smartDatabase, _documentType, _documentNo);

                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        System.Windows.Forms.Cursor.Hide();

                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Inleveransen bokförd."));

                        this.Close();
                    }
                }


            }

        }


    }
}