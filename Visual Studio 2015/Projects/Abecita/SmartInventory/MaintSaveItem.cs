using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class MaintSaveItem : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataWhseItemStore dataWhseItemStore;

        public MaintSaveItem(DataWhseItemStore dataWhseItemStore, SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.dataWhseItemStore = dataWhseItemStore;
            this.smartDatabase = smartDatabase;
            this.handleUnitIdBox.Text = dataWhseItemStore.handleUnitId;
            this.itemNoBox.Text = dataWhseItemStore.itemNo;
            this.sumQuantityBox.Text = dataWhseItemStore.quantity.ToString();

            listBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MaintSaveItem_Load(object sender, EventArgs e)
        {
            this.scanBinBox.Focus();
        }

        private void scanBinBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '>')
            {
                e.Handled = true;

                dataWhseItemStore.toBinCode = scanBinBox.Text;
                dataWhseItemStore.commit();


                if (performTransfer(dataWhseItemStore))
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                    dataWhseItemStore.delete();
                    this.Close();

                }
                else
                {
                    dataWhseItemStore.toBinCode = "";
                    dataWhseItemStore.commit();

                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                    this.scanBinBox.Text = "";
                    this.scanBinBox.Focus();
                }
            }
        }


        private bool performTransfer(DataWhseItemStore dataWhseItemStore)
        {
            listBox1.Visible = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            Service synchService = new Service("moveHandleUnit", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(dataWhseItemStore);

            ServiceResponse serviceResponse = synchService.performService();


            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Förfrågan misslyckades.");

                }
                else
                {
                    write("Förfrågan klar.");
                    listBox1.Visible = false;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();
                    return true;
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
            return false;

        }

        #region Logger Members

        public void write(string message)
        {
            listBox1.Items.Add(message);
            Application.DoEvents();           
        }

        #endregion
    }
}