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
    public partial class StartForm : Form
    {
        private SmartDatabase smartDatabase;
        private DataCustomer selectedCustomer = null;

        public StartForm(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;

            InitializeComponent();
            
            updateGrid();
        }

        private void updateGrid()
        {
            DataVisitList dataVisitList = new DataVisitList(smartDatabase);
            System.Data.DataSet customerDataSet = dataVisitList.getDataSet();
            visitGrid.DataSource = customerDataSet.Tables[0];
        }

        private void visitGrid_Click(object sender, EventArgs e)
        {
            if (visitGrid.BindingContext[visitGrid.DataSource, ""].Count > 0)
            {
                selectedCustomer = new DataCustomer(visitGrid[visitGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool found = false;

            if (visitGrid.BindingContext[visitGrid.DataSource, ""] != null)
            {
                int i = 0;
                while ((i < visitGrid.BindingContext[visitGrid.DataSource, ""].Count) && (!found))
                {
                    if (visitGrid[i, 1].ToString().Length >= searchBox.Text.Length)
                    {
                        if (visitGrid[i, 1].ToString().Substring(0, searchBox.Text.Length).ToUpper() == searchBox.Text.ToUpper())
                        {
                            visitGrid.CurrentRowIndex = i;
                            found = true;
                        }
                    }
                    i++;
                }
                if (!found)
                {
                    visitGrid.CurrentRowIndex = 0;
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

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(smartDatabase);
            menu.ShowDialog();

            menu.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (visitGrid.BindingContext[visitGrid.DataSource, ""].Count > 0)
            {
                selectedCustomer = new DataCustomer(visitGrid[visitGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
            }

            if (selectedCustomer == null)
            {
                MessageBox.Show("Du måste välja en kund först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (DateTime.Today.Year < 2009)
                {
                    MessageBox.Show("Dagens datum stämmer inte.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                DataSalesHeader dataSalesHeader = new DataSalesHeader(selectedCustomer, smartDatabase);

                Order order = new Order(dataSalesHeader, smartDatabase, false);
                order.ShowDialog();
                order.Dispose();
            }

            //reInitDatabase();

            GC.Collect();
            Application.DoEvents();



            visitGrid.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PriceScan priceScan = new PriceScan(smartDatabase);
            priceScan.ShowDialog();
            priceScan.Dispose();
        }


    }
}