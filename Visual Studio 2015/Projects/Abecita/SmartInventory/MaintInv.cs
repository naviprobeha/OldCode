using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintInv : Form
    {
        private SmartDatabase smartDatabase;

        public MaintInv(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;

            InitializeComponent();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sumQuantityBox_GotFocus(object sender, EventArgs e)
        {
            QuantityForm quantityForm = new QuantityForm(itemNoBox.Text, descriptionBox.Text, smartDatabase);
            quantityForm.setValue(sumQuantityBox.Text);
            quantityForm.ShowDialog();
            if (quantityForm.getStatus() == 1)
            {
                sumQuantityBox.Text = quantityForm.getValue();
            }
            locationBox.Focus();
        }
    }
}