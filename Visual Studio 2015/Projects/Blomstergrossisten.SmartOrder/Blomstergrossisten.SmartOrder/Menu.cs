using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class Menu : Form
    {
        private SmartDatabase smartDatabase;

        public Menu(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerList customerList = new CustomerList(smartDatabase);
            customerList.ShowDialog();
            customerList.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders(smartDatabase);
            orders.ShowDialog();
            orders.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sync sync = new Sync(smartDatabase);
            sync.ShowDialog();

            sync.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Setup setup = new Setup(smartDatabase);
            setup.ShowDialog();

            setup.Dispose();
        }
    }
}