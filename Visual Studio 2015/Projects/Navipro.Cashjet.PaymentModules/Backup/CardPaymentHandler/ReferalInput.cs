using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    public partial class ReferalInput : Form
    {
        private int status;

        public ReferalInput(string referalLabel)
        {
            InitializeComponent();

            label1.Text = referalLabel;

        }

        public int getStatus()
        {
            return status;
        }

        public string getValue()
        {
            if (status == 1) return referalText.Text;
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status = 1;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = 0;
            Close();
        }
    }
}
