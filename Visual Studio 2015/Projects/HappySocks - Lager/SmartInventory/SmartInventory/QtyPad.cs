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
    public partial class QtyPad : Form
    {
        private int _status;
        private float _originalQuantity;
        private int _addMode;
        
        public QtyPad(Configuration configuration, SmartDatabase smartDatabase, string itemNo, string variantCode, string description, float quantity)
        {
            InitializeComponent();

            label1.Text = Translation.translate(configuration.languageCode, "Artikelnr");
            label2.Text = Translation.translate(configuration.languageCode, "Variantkod");
            label3.Text = Translation.translate(configuration.languageCode, "Antal");
            label4.Text = Translation.translate(configuration.languageCode, "Beskrivning");

            button11.Text = Translation.translate(configuration.languageCode, "Ersätt");
            button12.Text = Translation.translate(configuration.languageCode, "Avbryt");
            button13.Text = Translation.translate(configuration.languageCode, "Addera");

            itemNoBox.Text = itemNo;
            variantCodeBox.Text = variantCode;
            descriptionBox.Text = description;
            _originalQuantity = quantity;
            qtyBox.Text = ((int)quantity).ToString();

            this.Text = Translation.translate(configuration.languageCode, "Ange antal");
        }


        public void keyPressed(string key)
        {
            if (key == "CL")
            {
                qtyBox.Text = "0";
            }
            else
            {
                if (qtyBox.Text != "0")
                {
                    qtyBox.Text = qtyBox.Text + key;
                }
                else
                {
                    qtyBox.Text = key;
                }
            }

            qtyBox.Focus();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        public float getQuantity()
        {
            try
            {
                if (_addMode == 0) return float.Parse(qtyBox.Text);
                if (_addMode == 1) return _originalQuantity + float.Parse(qtyBox.Text);
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int getStatus()
        {
            return _status;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _addMode = 0;
            _status = 1;
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _status = 0;
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _addMode = 1;
            _status = 1;
            this.Close();

        }
    }
}