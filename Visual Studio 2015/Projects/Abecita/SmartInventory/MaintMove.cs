using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintMove : Form
    {
        private SmartDatabase smartDatabase;

        public MaintMove(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
        }

 
        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaintMoveItem maintMoveItem = new MaintMoveItem(smartDatabase);
            maintMoveItem.ShowDialog();
            maintMoveItem.Dispose();

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaintSaveItems maintSaveItems = new MaintSaveItems(smartDatabase);
            maintSaveItems.ShowDialog();
            maintSaveItems.Dispose();

            this.Close();
        }
    }
}