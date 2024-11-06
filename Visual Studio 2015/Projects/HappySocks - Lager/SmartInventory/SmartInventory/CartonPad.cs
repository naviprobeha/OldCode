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
    public partial class CartonPad : Form
    {
        private int _status;
        private string _cartonNo;

        public CartonPad(Configuration configuration, SmartDatabase smartDatabase, string cartonNo)
        {
            InitializeComponent();

            label3.Text = Translation.translate(configuration.languageCode, "Kartongnr");
            label2.Text = Translation.translate(configuration.languageCode, "Ange ett nytt kartongnr");

            button11.Text = Translation.translate(configuration.languageCode, "OK");
            button12.Text = Translation.translate(configuration.languageCode, "Avbryt");

            _cartonNo = cartonNo;
            cartonNoBox.Text = _cartonNo;

            this.Text = Translation.translate(configuration.languageCode, "Carton");
        }


        public void keyPressed(string key)
        {
            if (key == "CL")
            {
                cartonNoBox.Text = "0";
            }
            else
            {
                if (cartonNoBox.Text != "0")
                {
                    cartonNoBox.Text = cartonNoBox.Text + key;
                }
                else
                {
                    cartonNoBox.Text = key;
                }
            }

            cartonNoBox.Focus();
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

        public string getCartonNo()
        {
            return cartonNoBox.Text;
        }

        public int getStatus()
        {
            return _status;
        }

        private void button11_Click(object sender, EventArgs e)
        {
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
            _status = 1;
            this.Close();

        }
    }
}