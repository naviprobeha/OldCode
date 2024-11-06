using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navipro.CashJet.AddIns
{
    public partial class Keyboard : Form
    {
        public Keyboard()
        {
            InitializeComponent();
            
        }

        public void init(string input)
        {
            textBox1.Text = input;
        }


        public string getInput()
        {
            return textBox1.Text;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "<-")
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 2);
                return;
            }
            if (((Button)sender).Text == "")
            {
                textBox1.Text = textBox1.Text + " ";
                return;
            }
            textBox1.Text = textBox1.Text + ((Button)sender).Text;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
