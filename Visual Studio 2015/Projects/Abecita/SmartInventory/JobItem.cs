using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class JobItem : Form
    {
        private SmartDatabase smartDatabase;
        private int zone;

        public JobItem(SmartDatabase smartDatabase, int zone)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.zone = zone;

            switch (zone)
            {
                case 1:
                    {
                        label4.Text = "Frekvensklass: Högfrekvent";
                        break;
                    }
                case 2:
                    {
                        label4.Text = "Frekvensklass: Mellanfrekvent";
                        break;
                    }
                case 3:
                    {
                        label4.Text = "Frekvensklass: Lågfrekvent";
                        break;
                    }
                case 4:
                    {
                        label4.Text = "Frekvensklass: Mkt lågfrekvent";
                        break;
                    }
                case 5:
                    {
                        label4.Text = "Frekvensklass: Storvolymigt";
                        break;
                    }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}