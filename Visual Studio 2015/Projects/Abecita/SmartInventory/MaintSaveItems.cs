using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintSaveItems : Form
    {
        private SmartDatabase smartDatabase;
        private DataWhseItemStore selectedJob;

        public MaintSaveItems(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            InitializeComponent();

            updateGrid();

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            DataWhseItemStores dataWhseItemStores = new DataWhseItemStores(smartDatabase);
            DataSet dataSet = dataWhseItemStores.getDataSet();

            jobGrid.DataSource = dataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MaintSaveItems_Load(object sender, EventArgs e)
        {
            this.scanHEidBox.Focus();
        }

        private void scanHEidBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                DataWhseItemStore dataWhseItemStore = new DataWhseItemStore(scanHEidBox.Text, smartDatabase);

                if (dataWhseItemStore.exists)
                {
                    MaintSaveItem maintSaveItem = new MaintSaveItem(dataWhseItemStore, smartDatabase);
                    maintSaveItem.ShowDialog();
                    updateGrid();
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                }
                this.scanHEidBox.Text = "";
                this.scanHEidBox.Focus();
            }
		
        }

        private void jobGrid_Click(object sender, EventArgs e)
        {
            if (jobGrid.BindingContext[jobGrid.DataSource, ""].Count > 0)
            {
                selectedJob = new DataWhseItemStore(jobGrid[jobGrid.CurrentRowIndex, 0].ToString(), jobGrid[jobGrid.CurrentRowIndex, 1].ToString(), smartDatabase);
            }	
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (selectedJob != null)
            {
                System.Windows.Forms.MessageBox.Show("Lägg tillbaka lådan på plats " + selectedJob.binCode + ".");
                selectedJob.delete();
                updateGrid();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja en rad först.");
            }
            scanHEidBox.Focus();
        }

 
    }
}