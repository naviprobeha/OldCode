using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navipro.Cashjet.PaymentModules.CardPaymentListener
{
    public partial class SysTray : Form
    {
        private CommHandler commHandler;
        public SysTray()
        {
            InitializeComponent();
            commHandler = new CommHandler();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);


            WindowState = FormWindowState.Minimized;
            Hide();

            commHandler.start();
        }

        private void avslutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commHandler.close();
            this.Close();
            Application.Exit();

        }


    }
}
