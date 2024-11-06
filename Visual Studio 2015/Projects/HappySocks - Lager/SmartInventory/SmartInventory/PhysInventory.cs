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
    public partial class PhysInventory : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;

        public PhysInventory(Configuration configuration, SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            label1.Text = Translation.translate(configuration.languageCode, "EAN:");
            label2.Text = Translation.translate(configuration.languageCode, "Scanna varje artikels EAN-kod.");

            itemNoLabel.Text = Translation.translate(configuration.languageCode, "Artikelnr");
            variantCodeLabel.Text = Translation.translate(configuration.languageCode, "Variantkod");
            descLabel.Text = Translation.translate(configuration.languageCode, "Beskrivning");
            unitOfMeasureLabel.Text = Translation.translate(configuration.languageCode, "Enhet");
            quantityLabel.Text = Translation.translate(configuration.languageCode, "Antal");

            button1.Text = Translation.translate(configuration.languageCode, "Stäng");

            this.Text = Translation.translate(configuration.languageCode, "Phys. Inventory");
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void eanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;
                logViewBox.Visible = true;


                DataSalesLine dataSalesLine = NAVComm.addPhysInventoryItemQty(configuration, smartDatabase, this, eanBox.Text, 1);
                if (dataSalesLine != null)
                {
                    this.itemNoBox.Text = dataSalesLine.itemNo;
                    this.variantCodeBox.Text = dataSalesLine.variantCode;
                    this.descBox.Text = dataSalesLine.description;
                    this.qtyBox.Text = dataSalesLine.quantity.ToString();
                    this.unitOfMeasureBox.Text = dataSalesLine.unitOfMeasure;

                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                }
                else
                {
                    this.itemNoBox.Text = "";
                    this.variantCodeBox.Text = "";
                    this.descBox.Text = "";
                    this.qtyBox.Text = "";
                    this.unitOfMeasureBox.Text = "";

                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                }

                e.Handled = true;
                eanBox.Text = "";
                eanBox.Focus();

                logViewBox.Visible = false;
                logViewBox.Items.Clear();
            }
        }

        #region Logger Members

        public void write(string message)
        {
            logViewBox.Items.Add(message);
            Application.DoEvents();            
        }

        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}