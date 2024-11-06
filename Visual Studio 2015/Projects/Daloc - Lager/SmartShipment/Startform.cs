using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class Smartshipment : Form
    {
        private SmartDatabase smartDatabase;
        private const string dbFileName = "\\Flash File Store\\SmartShipment.sdf";
        
        public Smartshipment()
        {
            InitializeComponent();
            smartDatabase = new SmartDatabase(dbFileName);

            if (!smartDatabase.init())
            {
                smartDatabase.createDatabase();
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            SynchSettings synchSettings = new SynchSettings(smartDatabase);
            synchSettings.ShowDialog();
            synchSettings.Dispose();
            smartDatabase.getSetup().refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderHeader orderHeader = new OrderHeader(smartDatabase);
            orderHeader.ShowDialog();
            orderHeader.Dispose();

        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders(smartDatabase);
            orders.ShowDialog();
            orders.Dispose();

        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            Synchronize synchronize = new Synchronize(smartDatabase);
            synchronize.ShowDialog();
            synchronize.Dispose();

        }

        static void Main()
        {
            Application.Run(new Smartshipment());
        }

    }
}