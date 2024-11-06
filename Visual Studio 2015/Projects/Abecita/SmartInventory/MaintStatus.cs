using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintStatus : Form
    {
        private SmartDatabase smartDatabase;

        public MaintStatus(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            InitializeComponent();
        }



        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaintStatusChange maintStatusChange = new MaintStatusChange(smartDatabase, 2);
            maintStatusChange.ShowDialog();
            maintStatusChange.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaintStatusChange maintStatusChange = new MaintStatusChange(smartDatabase, 1);
            maintStatusChange.ShowDialog();
            maintStatusChange.Dispose();
        }
    }
}