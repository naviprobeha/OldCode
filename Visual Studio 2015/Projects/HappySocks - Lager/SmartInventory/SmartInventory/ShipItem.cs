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
    public partial class ShipItem : Form
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private int _documentType;
        private string _documentNo;
        private DataSalesLine currentSalesLine;
        private int _currentCarton;
        private bool okToShowQtyForm;

        public ShipItem(Configuration configuration, SmartDatabase smartDatabase, int documentType, string documentNo, int entryNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentType = documentType;
            this._documentNo = documentNo;

            label1.Text = Translation.translate(configuration.languageCode, "EAN:");
            label2.Text = Translation.translate(configuration.languageCode, "Scanna varje artikels EAN-kod.");

            label1.Text = Translation.translate(configuration.languageCode, "Artikelnr");
            label3.Text = Translation.translate(configuration.languageCode, "Variantkod");
            label4.Text = Translation.translate(configuration.languageCode, "Beskrivning");
            //unitOfMeasureCol.HeaderText = Translation.translate(configuration.languageCode, "Enhet");
            label5.Text = Translation.translate(configuration.languageCode, "Totalt att plocka");
            label6.Text = Translation.translate(configuration.languageCode, "Plockat antal");
            //quantityToShipCol.HeaderText = Translation.translate(configuration.languageCode, "Antal att lev.");
            label8.Text = Translation.translate(configuration.languageCode, "Kartong");


            button1.Text = Translation.translate(configuration.languageCode, "Föregående");
            button11.Text = Translation.translate(configuration.languageCode, "Nästa");
            button2.Text = Translation.translate(configuration.languageCode, "Avbryt");
            button3.Text = Translation.translate(configuration.languageCode, "Kartonger");
            //button4.Text = Translation.translate(configuration.languageCode, "Föreg.");

            this.Text = Translation.translate(configuration.languageCode, "Ship");

            if (entryNo > 0)
            {
                currentSalesLine = DataSalesLine.getLineFromEntryNo(smartDatabase, documentType, documentNo, entryNo);
            }
            else
            {
                currentSalesLine = DataSalesLine.getFirstLine(smartDatabase, documentType, documentNo);
            }


            updateView();

        }

        private void updateView()
        {
            itemNoBox.Text = currentSalesLine.itemNo;
            variantCodeBox.Text = currentSalesLine.variantCode;
            descriptionBox.Text = currentSalesLine.description;
            quantityBox.Text = (currentSalesLine.quantity - currentSalesLine.quantityShipped).ToString();
            pickedQtyBox.Text = currentSalesLine.quantityToShip.ToString();

            DataSalesLineCarton dataSalesLineCarton = DataSalesLineCarton.getCurrentCarton(smartDatabase, _documentType, _documentNo, currentSalesLine.lineNo);
            if (dataSalesLineCarton != null)
            {
                _currentCarton = int.Parse(dataSalesLineCarton.cartonNo);
            }
            else
            {
                dataSalesLineCarton = DataSalesLineCarton.getLastCarton(smartDatabase, _documentType, _documentNo);
                if (dataSalesLineCarton != null)
                {
                    _currentCarton = int.Parse(dataSalesLineCarton.cartonNo);
                }
                else
                {
                    _currentCarton = 1;
                }
            }
            cartonBox.Text = _currentCarton.ToString();
            

            eanBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                if (currentSalesLine.ean.Contains(eanBox.Text))
                {
                    if (currentSalesLine.quantityToShip < currentSalesLine.quantity)
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                        currentSalesLine.quantityToShip = currentSalesLine.quantityToShip + 1;
                        currentSalesLine.save();

                        if (!DataSalesLineCarton.cartonExists(smartDatabase, _documentType, _documentNo, currentSalesLine.lineNo, _currentCarton.ToString()))
                        {
                            DataSalesLineCarton.createCarton(smartDatabase, _documentType, _documentNo, currentSalesLine.lineNo, _currentCarton.ToString(), currentSalesLine.quantityToShip);
                        }

                        DataScanLine.updateSales(smartDatabase, currentSalesLine);

                        if (currentSalesLine.quantityToShip == currentSalesLine.quantity)
                        {
                            DataSalesLine nextSalesLine = DataSalesLine.getNextLine(smartDatabase, currentSalesLine);
                            if (nextSalesLine != null) currentSalesLine = nextSalesLine;

                            if (nextSalesLine == null)
                            {
                                nextSalesLine = DataSalesLine.getFirstUnPickedLine(smartDatabase, currentSalesLine.documentType, currentSalesLine.documentNo);
                                if (nextSalesLine == null)
                                {
                                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är plockade!"));
                                    this.Close();
                                }
                                else
                                {
                                    currentSalesLine = nextSalesLine;
                                }
                            }
                        }


                        updateView();
                    }
                    else
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla kvantiteter är redan plockade av den här artikeln."));
                    }
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Fel EAN-kod."));
                }

                e.Handled = true;
                eanBox.Text = "";
                eanBox.Focus();
            }

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            DataSalesLine nextSalesLine = DataSalesLine.getNextLine(smartDatabase, currentSalesLine);
            if (nextSalesLine != null) currentSalesLine = nextSalesLine;

            if (nextSalesLine == null)
            {
                nextSalesLine = DataSalesLine.getFirstUnPickedLine(smartDatabase, currentSalesLine.documentType, currentSalesLine.documentNo);
                if (nextSalesLine == null)
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är plockade!"));
                    this.Close();
                }
                else
                {
                    currentSalesLine = nextSalesLine;
                }
            }
            updateView();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataSalesLine prevSalesLine = DataSalesLine.getPrevLine(smartDatabase, currentSalesLine);
            if (prevSalesLine != null) currentSalesLine = prevSalesLine;

            updateView();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pickedQtyBox_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void pickedQtyBox_GotFocus(object sender, EventArgs e)
        {
            if (okToShowQtyForm)
            {
                if (currentSalesLine.quantityToShip > 0)
                {
                    okToShowQtyForm = false;

                    QtyPad qtyPad = new QtyPad(configuration, smartDatabase, currentSalesLine.itemNo, currentSalesLine.variantCode, currentSalesLine.description, float.Parse(pickedQtyBox.Text));
                    qtyPad.ShowDialog();

                    if (qtyPad.getStatus() == 1)
                    {
                        if (qtyPad.getQuantity() <= currentSalesLine.quantity)
                        {
                            currentSalesLine.quantityToShip = qtyPad.getQuantity();
                            if (currentSalesLine.carton == "") currentSalesLine.carton = _currentCarton.ToString();
                            currentSalesLine.save();

                            DataScanLine.updateSales(smartDatabase, currentSalesLine);

                            if (currentSalesLine.quantityToShip == currentSalesLine.quantity)
                            {
                                DataSalesLine nextSalesLine = DataSalesLine.getNextLine(smartDatabase, currentSalesLine);
                                if (nextSalesLine != null) currentSalesLine = nextSalesLine;

                                if (nextSalesLine == null)
                                {
                                    nextSalesLine = DataSalesLine.getFirstUnPickedLine(smartDatabase, currentSalesLine.documentType, currentSalesLine.documentNo);
                                    if (nextSalesLine == null)
                                    {
                                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är plockade!"));
                                        this.Close();
                                    }
                                    else
                                    {
                                        currentSalesLine = nextSalesLine;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                            System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Du kan bara plocka antalet ")+currentSalesLine.quantity);

                        }

                        updateView();
                    }

                }
                eanBox.Focus();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Dela raden?"), Translation.translate(configuration.languageCode, "Kartong"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                _currentCarton++;
                DataSalesLineCarton.createCarton(smartDatabase, _documentType, _documentNo, currentSalesLine.lineNo, _currentCarton.ToString(), currentSalesLine.quantityToShip + 1);

            }

            ShipCartonList shipCartonList = new ShipCartonList(configuration, smartDatabase, currentSalesLine);
            shipCartonList.ShowDialog();

            shipCartonList.Dispose();

            updateView();

        }

        private void button4_Click(object sender, EventArgs e)
        {   
            int carton = 0;
            try
            {
                carton = int.Parse(currentSalesLine.carton);
            }
            catch (Exception)
            {
            }

            if (carton > 0)
            {
                carton--;
                currentSalesLine.carton = carton.ToString();
                _currentCarton = carton;
                currentSalesLine.save();
                updateView();
            }
        }

        private void eanBox_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }

    }
}