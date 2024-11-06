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
    public partial class ConfirmBox : Form
    {
        private int result;

        public ConfirmBox()
        {
            InitializeComponent();

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
            button3.BackColor = Color.FromKnownColor(KnownColor.LightGray);
            button3.ForeColor = Color.FromName("black");

        }

        public void init(string title, string message, string button1, string button2, string button3)
        {
            this.Text = title;
            this.messageTitle.Text = message;
            this.button1.Text = button1;
            this.button2.Text = button2;
            this.button3.Text = button3;

            if (button3 == "") this.button3.Visible = false;
        }

        public int getResult()
        {
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = 2;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result = 3;
            this.Close();
        }
    }
}
