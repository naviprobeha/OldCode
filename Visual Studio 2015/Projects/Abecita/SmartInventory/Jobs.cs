using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class Jobs : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseActivityHeader selectedJob;

        public Jobs(SmartDatabase smartDatabase)
        {
            InitializeComponent();
            this.smartDatabase = smartDatabase;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void jobGrid_Click(object sender, EventArgs e)
        {

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseActivityHeaders dataWhseActivityHeaders = new DataWhseActivityHeaders(smartDatabase);
            DataSet dataSet = dataWhseActivityHeaders.getDataSet(6);

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
            else
            {
                if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
                {
                    selectedJob = new DataWhseActivityHeader(jobGrid[0, 0].ToString(), DataWhseActivityHeaders.WHSE_TYPE_MOVEMENT);
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (selectedJob != null)
            {
                JobFreq jobFreq = new JobFreq(smartDatabase, selectedJob);
                jobFreq.ShowDialog();
                jobFreq.Dispose();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (selectedJob != null)
            {
                SaveFreq saveFreq = new SaveFreq(smartDatabase, selectedJob);
                saveFreq.ShowDialog();
                saveFreq.Dispose();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja ett uppdrag.");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void jobGrid_Click_1(object sender, EventArgs e)
        {
            if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
            {
                selectedJob = new DataWhseActivityHeader(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), DataWhseActivityHeaders.WHSE_TYPE_MOVEMENT);
            }		

        }

        private void Jobs_Load(object sender, EventArgs e)
        {
            this.updateGrid();
        }
    }
}