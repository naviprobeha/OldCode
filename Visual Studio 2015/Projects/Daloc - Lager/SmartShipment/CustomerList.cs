using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class CustomerList : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private DataCustomers dataCustomers;
        private DataCustomer selectedCustomer;
        private DataCustomer returnCustomer;
        private DataSetup dataSetup;
        private CreditCheck creditData;
        private int currentCol;

        public CustomerList(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataCustomers = new DataCustomers(smartDatabase);
            dataSetup = new DataSetup(smartDatabase);


            serviceLog.Width = 240;
            serviceLog.Height = 184;
            serviceLog.Visible = false;

            this.searchCustomer.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void customerGrid_Click_1(object sender, EventArgs e)
        {

        }

        private void searchCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchCustomer_GotFocus(object sender, EventArgs e)
        {

        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            System.Data.DataSet customerDataSet = dataCustomers.getDataSet();
            customerGrid.DataSource = customerDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }

        private void searchCustomer_TextChanged_1(object sender, EventArgs e)
        {
            bool found = false;

            if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
            {
                int i = 0;
                while ((i < customerGrid.BindingContext[customerGrid.DataSource, ""].Count) && (!found))
                {
                    if (customerGrid[i, currentCol].ToString().Length >= searchCustomer.Text.Length)
                    {
                        if (customerGrid[i, currentCol].ToString().Substring(0, searchCustomer.Text.Length).ToUpper() == searchCustomer.Text.ToUpper())
                        {
                            customerGrid.CurrentRowIndex = i;
                            found = true;
                        }
                    }
                    i++;
                }
                if (!found)
                {
                    customerGrid.CurrentRowIndex = 0;
                }
            }
        }

        private void searchCustomer_GotFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Du måste välja en kund först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (checkCreditLimit())
                {
                    returnCustomer = selectedCustomer;
                    this.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Kreditprövningen misslyckades.", "Kreditprövning");
                }
            }
        }

        private bool checkCreditLimit()
        {
            serviceLog.Items.Clear();
            serviceLog.Items.Add("Kreditprövning...");
            serviceLog.Visible = true;
            serviceLog.Width = 480;
            serviceLog.Height = 340;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            bool error = false;

            button1.Enabled = false;
            button2.Enabled = false;

            Service synchService = new Service("creditRequest", smartDatabase, dataSetup);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(selectedCustomer);

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Förfrågan misslyckades.");
                    error = true;
                }
                else
                {
                    write("Förfrågan klar.");
                    this.creditData = serviceResponse.creditCheck;
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
                error = true;
            }

            button1.Enabled = true;
            button2.Enabled = true;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            serviceLog.Visible = false;

            if (error == true) return false;

            if (creditData.status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataCustomer getCustomer()
        {
            return returnCustomer;
        }

        #region Logger Members

        public void write(string message)
        {
            serviceLog.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void button2_Click_1(object sender, EventArgs e)
        {
            returnCustomer = null;
            this.Close();
        }

        private void customerGrid_Click(object sender, EventArgs e)
        {
            if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
            {
                currentCol = customerGrid.CurrentCell.ColumnNumber;
                selectedCustomer = new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
              
            }
        }

        private void CustomerList_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}