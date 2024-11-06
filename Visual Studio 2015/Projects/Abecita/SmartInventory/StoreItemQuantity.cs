using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreItemQuantity : Form
    {
        private SmartDatabase smartDatabase;
        private int status;
        private DataSetup dataSetup;

        public StoreItemQuantity(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataSetup = new DataSetup(smartDatabase);

            this.scanPackageBox.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = 2;
            this.Close();
        }

        public int getStatus()
        {
            return status;
        }

        private void scanPackageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                this.handleUnitBox.Text = scanPackageBox.Text;
                this.scanPackageBox.Text = "";
                this.quantityBox.Text = "";

                Sound sound = new Sound(Sound.SOUND_TYPE_OK);
                DataItemUnit dataItemUnit = new DataItemUnit(handleUnitBox.Text, smartDatabase);
                quantityBox.Text = "" + dataItemUnit.quantity;

                this.quantityBox.Focus();
            }
        }

        private void quantityBox_GotFocus(object sender, EventArgs e)
        {
            if (handleUnitBox.Text != "")
            {
                QuantityForm quantityForm = new QuantityForm(handleUnitBox.Text, "Hanteringsenhet", smartDatabase);
                quantityForm.setCaption("Antal", "ID");
                quantityForm.setValue(quantityBox.Text);
                quantityForm.ShowDialog();
                if (quantityForm.getStatus() == 1)
                {
                    quantityBox.Text = quantityForm.getValue();
                    DataItemUnit dataItemUnit = new DataItemUnit(handleUnitBox.Text, smartDatabase);
                    dataItemUnit.quantity = float.Parse(quantityBox.Text);
                    dataItemUnit.commit();
                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                }

                quantityForm.Dispose();
            }
            scanPackageBox.Focus();
        }

    }
}