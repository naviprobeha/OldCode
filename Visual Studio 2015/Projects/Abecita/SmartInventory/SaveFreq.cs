using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class SaveFreq : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader dataWhseActivityHeader;

        private int qtyFreq1;
        private int qtyFreq2;
        private int qtyFreq3;
        private int qtyFreq4;

        public SaveFreq(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader)
        {
            this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;

            InitializeComponent();

            label1.Text = label1.Text + " " + dataWhseActivityHeader.no;

            updateButtons();

        }

        private void updateButtons()
        {
            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);

            qtyFreq1 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 1, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);
            qtyFreq2 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 2, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);
            qtyFreq3 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 3, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);
            qtyFreq4 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 4, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_HANDLED, true);

            button1.Text = "Hög (" + qtyFreq1 + ")";
            button2.Text = "Mellan (" + qtyFreq2 + ")";
            button3.Text = "Låg (" + qtyFreq3 + ")";
            button4.Text = "Mkt låg (" + qtyFreq4 + ")";
            button5.Text = "Storvol (0)";
        }

        private void showSaveItems(int zone)
        {
            SaveItems saveItems = new SaveItems(smartDatabase, dataWhseActivityHeader, zone);
            saveItems.ShowDialog();
            updateButtons();
            saveItems.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSaveItems(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSaveItems(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showSaveItems(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showSaveItems(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showSaveItems(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}