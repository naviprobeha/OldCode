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
    public partial class InputBox : Form
    {
        private int result;

        public InputBox()
        {
            InitializeComponent();
            textBox.Focus();

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.BackColor = Color.FromName("darkblue");
            button1.ForeColor = Color.FromName("white");

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.White;
            button2.BackColor = Color.FromName("darkblue");
            button2.ForeColor = Color.FromName("white");

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = Color.White;
            button3.BackColor = Color.FromName("darkblue");
            button3.ForeColor = Color.FromName("white");


        }

        public void init(string title, string message, string button1, string button2)
        {
            this.Text = title;
            this.messageTitle.Text = message;
            this.button1.Text = button1;
            this.button2.Text = button2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = 2;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = 1;
            this.Close();
        }

        public int getResult()
        {
            return result;
        }

        public string getInputText()
        {
            return textBox.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageControl messageControl = new MessageControl();
            textBox.Text = messageControl.showKeyboard(textBox.Text);

        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, null);   
            }
        }

    }
}
