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
    public partial class Orders : Form
    {
        private DataSet orderDataSet;
        private DataSet readyOrderDataSet;
        private DataSetup dataSetup;
        private DataSalesHeaders dataSalesHeaders;
        private SmartDatabase smartDatabase;

        public Orders(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            InitializeComponent();

            dataSalesHeaders = new DataSalesHeaders(smartDatabase);
            dataSetup = new DataSetup(smartDatabase);
        }

        private void updateGrid()
        {
            readyOrderDataSet = dataSalesHeaders.getDataSet(true);
            orderGrid2.DataSource = readyOrderDataSet.Tables[0];
            orderDataSet = dataSalesHeaders.getDataSet(false);
            orderGrid1.DataSource = orderDataSet.Tables[0];
        }

        private void releaseBtn_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Orders_Load(object sender, EventArgs e)
        {
            updateGrid();

            orderGrid1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (orderGrid1.BindingContext[orderGrid1.DataSource, ""].Count > 0)
            {
                DataTable dataTable = (DataTable)orderGrid1.DataSource;

                DataSalesHeader dataSalesHeader = new DataSalesHeader((int)dataTable.Rows[orderGrid1.CurrentRowIndex].ItemArray.GetValue(0), smartDatabase);
                Order order = new Order(dataSalesHeader, smartDatabase, true);
                order.ShowDialog();
                updateGrid();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (orderGrid2.BindingContext[orderGrid2.DataSource, ""].Count > 0)
            {
                DataTable dataTable = (DataTable)orderGrid2.DataSource;

                DataSalesHeader dataSalesHeader = new DataSalesHeader((int)dataTable.Rows[orderGrid2.CurrentRowIndex].ItemArray.GetValue(0), smartDatabase);
                Order order = new Order(dataSalesHeader, smartDatabase, true);
                order.ShowDialog();
                updateGrid();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}