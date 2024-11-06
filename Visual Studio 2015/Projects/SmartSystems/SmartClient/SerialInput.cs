using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartSystems.SmartClient
{
    public partial class SerialInput : Form
    {
        private TextBox currentBox;
        private string serialNo;


        public SerialInput()
        {
            InitializeComponent();

            currentBox = no1;
        }

        private void keyPress(string key)
        {
            if (key != "C")
            {
                if (currentBox.Text.Length == 5) currentBox = getNextBox();
                if (currentBox.Text.Length < 5)
                {
                    currentBox.Text = currentBox.Text + key;
                }
            }
            else
            {
                if (currentBox.Text.Length == 0) currentBox = getPrevBox();
                if (currentBox.Text.Length > 0)
                {
                    currentBox.Text = currentBox.Text.Substring(0, currentBox.Text.Length - 1);
                }

            }
        }

        private TextBox getNextBox()
        {
            if (currentBox == no1) return no2;
            if (currentBox == no2) return no3;
            if (currentBox == no3) return no4;
            if (currentBox == no4) return no5;
            return currentBox;

        }

        private TextBox getPrevBox()
        {
            if (currentBox == no5) return no4;
            if (currentBox == no4) return no3;
            if (currentBox == no3) return no2;
            if (currentBox == no2) return no1;
            return currentBox;
        }



        private void colorButton13_Click(object sender, EventArgs e)
        {
            keyPress("0");
        }

        private void colorButton1_Click(object sender, EventArgs e)
        {
            keyPress("1");
        }

        private void colorButton2_Click(object sender, EventArgs e)
        {
            keyPress("2");
        }

        private void colorButton7_Click(object sender, EventArgs e)
        {
            keyPress("3");
        }

        private void colorButton3_Click(object sender, EventArgs e)
        {
            keyPress("4");
        }

        private void colorButton4_Click(object sender, EventArgs e)
        {
            keyPress("5");
        }

        private void colorButton8_Click(object sender, EventArgs e)
        {
            keyPress("6");
        }

        private void colorButton5_Click(object sender, EventArgs e)
        {
            keyPress("7");
        }

        private void colorButton6_Click(object sender, EventArgs e)
        {
            keyPress("8");
        }

        private void colorButton9_Click(object sender, EventArgs e)
        {
            keyPress("9");
        }

        private void colorButton10_Click(object sender, EventArgs e)
        {
            keyPress("C");
        }

        private void colorButton12_Click(object sender, EventArgs e)
        {
            if ((no1.Text.Length != 5) || (no2.Text.Length != 5) || (no3.Text.Length != 5) || (no4.Text.Length != 5) || (no5.Text.Length != 5))
            {
                MessageBox.Show("Ett korrekt serienr måste anges (xxxxx-xxxxx-xxxxx-xxxxx-xxxxx).");
            }
            else
            {
                serialNo = no1.Text + "-" + no2.Text + "-" + no3.Text + "-" + no4.Text + "-" + no5.Text;
                this.Close();
            }

        }

        private void colorButton11_Click(object sender, EventArgs e)
        {
            serialNo = "";
            no1.Text = "";
            no2.Text = "";
            no3.Text = "";
            no4.Text = "";
            no5.Text = "";
            currentBox = no1;

        }

        public string getSerialNo()
        {
            return this.serialNo;
        }

    }
}