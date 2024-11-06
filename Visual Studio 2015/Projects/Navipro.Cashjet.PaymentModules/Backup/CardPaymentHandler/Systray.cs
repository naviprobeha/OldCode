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
    public partial class Systray : Form
    {
        private CommHandler commHandler;
        public Systray()
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
