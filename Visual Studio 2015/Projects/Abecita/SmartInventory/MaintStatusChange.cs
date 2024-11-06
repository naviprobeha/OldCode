using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintStatusChange : Form
    {
        private SmartDatabase smartDatabase;
        private int mode;

        public MaintStatusChange(SmartDatabase smartDatabase, int mode)
        {
            this.smartDatabase = smartDatabase;
            this.mode = mode;
            
            InitializeComponent();

            if (mode == 1)
            {
                label5.Text = "Spärra lagerplats";
            }
            else
            {
                label5.Text = "Frisläpp lagerplats";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

 
    }
}