using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreFreq : Form
    {
        private SmartDatabase smartDatabase;
        private int qtyFreq1;
        private int qtyFreq2;
        private int qtyFreq3;
        private int qtyFreq4;

        public StoreFreq(SmartDatabase smartDatabase)
        {
            InitializeComponent();
            this.smartDatabase = smartDatabase;
            updateButtons();
        }

        private void updateButtons()
        {
            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);

            qtyFreq1 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 1, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
            qtyFreq2 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 2, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
            qtyFreq3 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 3, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
            qtyFreq4 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 4, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);

            label3.Text = "Hög (" + qtyFreq1 + ")";
            label4.Text = "Mellan (" + qtyFreq2 + ")";
            label5.Text = "Låg (" + qtyFreq3 + ")";
            label6.Text = "Mkt låg (" + qtyFreq4 + ")";
        }


        private void showStoreForm()
        {
            StoreItem storeItem = new StoreItem(smartDatabase);
            storeItem.ShowDialog();
            if (storeItem.getStatus() == 2)
            {
                this.Close();
            }
            else
            {
                updateButtons();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showStoreForm();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void menuItem3_Click(object sender, EventArgs e)
        {
            Bins bins = new Bins(smartDatabase);
            bins.ShowDialog();

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            StoreJobs storeJobs = new StoreJobs(smartDatabase);
            storeJobs.ShowDialog();
        }


    }
}