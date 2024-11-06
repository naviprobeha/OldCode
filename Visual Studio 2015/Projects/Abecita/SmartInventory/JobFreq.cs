using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class JobFreq : Form
    {
        private int qtyFreq1;
        private int qtyFreq2;
        private int qtyFreq3;
        private int qtyFreq4;

        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader dataWhseActivityHeader;

        public JobFreq(SmartDatabase smartDatabase, DataWhseActivityHeader dataWhseActivityHeader)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataWhseActivityHeader = dataWhseActivityHeader;

            label1.Text = label1.Text + " " + dataWhseActivityHeader.no;

            updateButtons();

        }

 
        private void showJobItems(int freq)
        {
            JobItems jobItems = new JobItems(smartDatabase, dataWhseActivityHeader, freq);
            jobItems.ShowDialog();
            updateButtons();
            jobItems.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            showJobItems(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showJobItems(2);

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            showJobItems(3);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            showJobItems(4);

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            showJobItems(5);

        }

        private void updateButtons()
        {
            DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);

            qtyFreq1 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 1, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
            qtyFreq2 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 2, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
            qtyFreq3 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 3, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);
            qtyFreq4 = dataWhseActivityLines.countLines(dataWhseActivityHeader, 4, DataWhseActivityLines.WHSE_ACTION_TAKE, DataWhseActivityLines.WHSE_STATUS_NONE, false);

            button1.Text = "Hög (" + qtyFreq1 + ")";
            button2.Text = "Mellan (" + qtyFreq2 + ")";
            button3.Text = "Låg (" + qtyFreq3 + ")";
            button4.Text = "Mkt låg (" + qtyFreq4 + ")";
            button5.Text = "Storvol (0)";
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}