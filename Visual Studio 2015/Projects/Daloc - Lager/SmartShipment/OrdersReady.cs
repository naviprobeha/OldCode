using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class OrdersReady : Form
    {
        private DataSet orderDataSet;
        private DataSet readyOrderDataSet;
        private DataSetup dataSetup;
        private DataSalesHeaders dataSalesHeaders;
        private SmartDatabase smartDatabase;

        public OrdersReady(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            this.dataSetup = smartDatabase.getSetup();
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            dataSalesHeaders = new DataSalesHeaders(smartDatabase);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (orderGrid.BindingContext[orderGrid.DataSource, ""].Count > 0)
            {
                DataTable dataTable = (DataTable)orderGrid.DataSource;

                DataSalesHeader dataSalesHeader = new DataSalesHeader((int)dataTable.Rows[orderGrid.CurrentRowIndex].ItemArray.GetValue(0), smartDatabase);

                OrderHeader order = new OrderHeader(smartDatabase, dataSalesHeader);
                order.ShowDialog();

                order.Dispose();

                updateGrid();
            }
        }

        private void orderGrid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void updateGrid()
        {
            orderDataSet = dataSalesHeaders.getDataSet(false);
            orderGrid.DataSource = orderDataSet.Tables[0];
        }

        private void OrdersReady_Load(object sender, EventArgs e)
        {
            updateGrid();

            orderGrid.Focus();

        }

        private void orderGrid_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) button1_Click_1(sender, null);
        }
    }
}