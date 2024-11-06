using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreJobs : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader selectedJob;

        public StoreJobs(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;

        }

 

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseActivityHeaders dataWhseActivityHeaders = new DataWhseActivityHeaders(smartDatabase);
            DataSet dataSet = dataWhseActivityHeaders.getDataSet(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL);

            jobGrid.DataSource = dataSet.Tables[0];

            if (selectedJob != null)
            {
                int i = 0;
                bool found = false;

                while ((i < jobGrid.BindingContext[jobGrid.DataSource, ""].Count) && !found)
                {
                    if (jobGrid[i, 0].ToString().Equals(selectedJob.no))
                    {
                        jobGrid.CurrentRowIndex = i;
                        jobGrid.Select(i);
                        found = true;
                    }
                    i++;
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void jobGrid_Click(object sender, EventArgs e)
        {
            if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
            {
                selectedJob = new DataWhseActivityHeader(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), 1);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (selectedJob != null)
            {
                StoreJobLines storeJobLines = new StoreJobLines(smartDatabase, selectedJob);
                storeJobLines.ShowDialog();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
            }
        }


    }
}