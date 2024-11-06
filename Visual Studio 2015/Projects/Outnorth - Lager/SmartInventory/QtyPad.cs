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
        public int status;

        public QtyPad(DataPickLine dataPickLine, int quantity)
        {
            InitializeComponent();

            descriptionBox.Text = dataPickLine.description;
            description2Box.Text = dataPickLine.description2;
            qtyBox.Text = quantity.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            status = 0;
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
        }

        public int getValue()
        {
            try
            {
                return int.Parse(qtyBox.Text);
            }
            catch (Exception) { }

            return 0;
        }

        public int getStatus()
        {
            return status;
        }
    }

}