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
    public partial class CustomerList : Form
    {
        private SmartDatabase smartDatabase;
        private DataCustomers dataCustomers;
        private DataVisitList dataVisitList;
        private DataSetup dataSetup;
        private DataCustomer selectedCustomer;

        public CustomerList(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataCustomers = new DataCustomers(smartDatabase);
            dataVisitList = new DataVisitList(smartDatabase);
            dataSetup = new DataSetup(smartDatabase);

           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            System.Data.DataSet customerDataSet = dataCustomers.getDataSet();
            customerGrid.DataSource = customerDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }

        private void customerGrid_Click(object sender, EventArgs e)
        {
            if (customerGrid[customerGrid.CurrentRowIndex, 0].ToString().Equals("Ja"))
            {
                customerGrid[customerGrid.CurrentRowIndex, 0] = "";
                dataVisitList.remove(new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString()));
            }
            else
            {
                customerGrid[customerGrid.CurrentRowIndex, 0] = "Ja";
                dataVisitList.add(new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString()));
            }

            if (customerGrid.BindingContext[customerGrid.DataSource, ""].Count > 0)
            {
                selectedCustomer = new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString(), smartDatabase);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool found = false;

            if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
            {
                int i = 0;
                while ((i < customerGrid.BindingContext[customerGrid.DataSource, ""].Count) && (!found))
                {
                    if (customerGrid[i, 2].ToString().Length >= searchCustomer.Text.Length)
                    {
                        if (customerGrid[i, 2].ToString().Substring(0, searchCustomer.Text.Length).ToUpper() == searchCustomer.Text.ToUpper())
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

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;

        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((customerGrid.BindingContext[customerGrid.DataSource, ""].Count == 0) || (customerGrid.CurrentRowIndex == -1))
            {
                MessageBox.Show("Du måste välja en kund först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                selectedCustomer = new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString(), smartDatabase);
                /*
                DataSalesHeader dataSalesHeader = new DataSalesHeader(selectedCustomer, smartDatabase);
                if (dataSetup.spiraEnabled)
                {
                    Order order = new Order(dataSalesHeader, smartDatabase, false);
                    order.ShowDialog();
                }
                else
                {
                    OrderSimple order = new OrderSimple(dataSalesHeader, smartDatabase, false);
                    order.ShowDialog();
                }
                 * */
            }
            customerGrid.Focus();
		
        }

        private void CustomerList_Closing(object sender, CancelEventArgs e)
        {
            customerGrid.Dispose();

        }
    }
}