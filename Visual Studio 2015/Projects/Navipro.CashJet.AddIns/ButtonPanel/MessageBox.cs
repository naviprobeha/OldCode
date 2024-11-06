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
    public partial class MessageBox : Form
    {
        public MessageBox()
        {
            InitializeComponent();

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.BackColor = Color.FromName("darkblue");
            button1.ForeColor = Color.FromName("white");

        }

        public void init(string title, string message)
        {
            this.Text = title;
            this.messageTitle.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
