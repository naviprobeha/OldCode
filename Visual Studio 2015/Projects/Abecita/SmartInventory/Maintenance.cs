using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class Maintenance : Form
    {
        private SmartDatabase smartDatabase;

        public Maintenance(SmartDatabase smartDatabase)
        {
            InitializeComponent();
            this.smartDatabase = smartDatabase;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menu2_Click(object sender, EventArgs e)
        {

        }

        private void menu1_Click(object sender, EventArgs e)
        {

        }

        private void menu1_Click_1(object sender, EventArgs e)
        {
            MaintInfo maintInfo = new MaintInfo(smartDatabase);
            maintInfo.ShowDialog();
            maintInfo.Dispose();
        }

        private void menu2_Click_1(object sender, EventArgs e)
        {
            MaintMove maintMove = new MaintMove(smartDatabase);
            maintMove.ShowDialog();
            maintMove.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(smartDatabase);
            inventory.ShowDialog();
            inventory.Dispose();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            StoreItemInstant storeItemInstant = new StoreItemInstant(smartDatabase);
            storeItemInstant.ShowDialog();
            storeItemInstant.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}