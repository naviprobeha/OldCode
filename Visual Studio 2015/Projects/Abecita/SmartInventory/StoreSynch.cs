using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StoreSynch : Form, Logger
    {
        private SmartDatabase smartDatabase;

        public StoreSynch(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            synchronize();
            openRegisterForm();
        }

        private void synchronize()
        {
            write("--- Synkronisering ---");
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            button2.Enabled = false;
            Service synchService = new Service("synchronization", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Synkroniseringen misslyckades.");
                }
                write("Synkronisering klar.");

            }
            else
            {
                write("Synkroniseringen misslyckades.");
            }
            button1.Enabled = true;
            button2.Enabled = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }


        #region Logger Members

        public void write(string message)
        {
            listBox1.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void button2_Click_1(object sender, EventArgs e)
        {
            openRegisterForm();

        }

        private void openRegisterForm()
        {
            StoreFreq storeFreq = new StoreFreq(smartDatabase);
            storeFreq.ShowDialog();

            if (System.Windows.Forms.MessageBox.Show("Synkronisera med Navision?", "Synkronisera", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                synchronize();

            }

            this.Close();

        }
    }
}