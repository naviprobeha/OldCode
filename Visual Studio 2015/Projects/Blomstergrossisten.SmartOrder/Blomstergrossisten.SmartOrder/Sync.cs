using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class Sync : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private string serviceName;
        private bool fullSynch;
        private bool abort;

        public Sync(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.serviceName = "singleSynchronization";
            this.fullSynch = true;
        }

        public Sync(SmartDatabase smartDatabase, string serviceName)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.serviceName = serviceName;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private bool synchronize()
        {
            bool sendResult = sendOrders();

            if (!fullSynch) return sendResult;
            if (!sendResult) return false;

            bool errors = false;

            if (sendResult)
            {
                write("--- Synkronisering ---");
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = false;

                Service synchService = new Service(serviceName, smartDatabase);
                synchService.setLogger(this);

                int count = 1;

                while ((!errors) && (count > 0) && (!abort))
                {
                    smartDatabase.clearErrorFlag();

                    //synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

                    ServiceResponse serviceResponse = synchService.performService();
                    count = serviceResponse.publication.header.synchEntries;
                    this.syncCountBox.Text = count.ToString();

                    if (serviceResponse != null)
                    {
                        if (serviceResponse.hasErrors)
                        {
                            System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                            errors = true;
                        }
                        else
                        {
                            if (!smartDatabase.checkErrorFlag()) ackSynchronization(serviceResponse.publication.header.entryNo);
                        }
                    }
                    else
                    {
                        errors = true;
                    }
                    if (smartDatabase.checkErrorFlag()) errors = true;
                }

                if (errors)
                {
                    write("Synkroniseringen misslyckades.");
                }
                else
                {
                    write("Synkronisering klar.");
                }


                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

            }

            if (fullSynch)
            {
                write("Raderar gamla artiklar.");
                DataItems dataItems = new DataItems(smartDatabase);
                dataItems.deleteOldItems();
            }

            return (!errors);
        }

        private bool sendOrders()
        {
            write("--- Skickar order ---");
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            Service synchService = new Service("sendOrders", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                DataSalesHeaders salesHeaders = new DataSalesHeaders(smartDatabase);

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Sändingen misslyckades.");
                    write("Raderar skickade order.");
                    salesHeaders.deleteReadySalesHeaders();

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    return false;
                }
                else
                {
                    write("Raderar skickade order.");
                    salesHeaders.deleteReadySalesHeaders();

                    write("Sändning klar.");

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    return true;

                }
            }
            else
            {
                write("Sändningen misslyckades.");

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

                return false;
            }


        }


        private void ackSynchronization(string entryNo)
        {
            Service synchService = new Service("ackSynck", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(new AckSynckEntry(entryNo));

            ServiceResponse serviceResponse = synchService.performService();

        }

        public void write(string message)
        {
            if (listBox1.Items.Count > 100) listBox1.Items.RemoveAt(0);

            listBox1.Items.Add(message);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            Application.DoEvents();
        }

        private void Sync_Load(object sender, EventArgs e)
        {
            if (!fullSynch)
            {
                Timer timer = new Timer();
                timer.Interval = 10;
                timer.Tick += new EventHandler(timer_SelectAll);
                timer.Enabled = true;

            }
        }

        public void timer_SelectAll(object sender, System.EventArgs e)
        {
            ((Timer)sender).Enabled = false;
            button1_Click_1(sender, null);

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            synchronize();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.abort = true;		
        }

        private void Sync_Closing(object sender, CancelEventArgs e)
        {
            listBox1.Dispose();
        }



        #region Logger Members

        void Logger.write(string message)
        {
            if (listBox1.Items.Count > 100) listBox1.Items.RemoveAt(0);

            listBox1.Items.Add(message);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            Application.DoEvents();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}