using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class ShipList : Form
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private DataSet shipListDataSet;
        private string _documentNo = "";
        private int _documentType  = 0;

        public ShipList(Configuration configuration, SmartDatabase smartDatabase)
        {
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            InitializeComponent();

            label2.Text = Translation.translate(configuration.languageCode, "Välj order.");
            button1.Text = Translation.translate(configuration.languageCode, "Avbryt");
            button11.Text = Translation.translate(configuration.languageCode, "Select");

            noCol.HeaderText = Translation.translate(configuration.languageCode, "Nr");
            customerNoCol.HeaderText = Translation.translate(configuration.languageCode, "Kundnr");
            customerNameCol.HeaderText = Translation.translate(configuration.languageCode, "Namn");
            addressCol.HeaderText = Translation.translate(configuration.languageCode, "Adress");
            cityCol.HeaderText = Translation.translate(configuration.languageCode, "Ort");
            countryCodeCol.HeaderText = Translation.translate(configuration.languageCode, "Land");
            orderDateCol.HeaderText = Translation.translate(configuration.languageCode, "Orderdatum");
            totalQtyCol.HeaderText = Translation.translate(configuration.languageCode, "Totalt antal");

            this.Text = Translation.translate(configuration.languageCode, "Ship");

            updateGrid();
        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            shipListDataSet = DataSalesHeader.getDataSet(smartDatabase);

            salesHeaderGrid.DataSource = shipListDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public Document getDocument()
        {
            if (_documentNo != "")
            {
                return new Document(_documentType, _documentNo);
            }
            return null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (salesHeaderGrid.BindingContext[salesHeaderGrid.DataSource, ""].Count > 0)
            {
                _documentType = int.Parse(shipListDataSet.Tables[0].Rows[salesHeaderGrid.CurrentRowIndex].ItemArray.GetValue(1).ToString());
                _documentNo = shipListDataSet.Tables[0].Rows[salesHeaderGrid.CurrentRowIndex].ItemArray.GetValue(2).ToString();
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga order i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
        }
    }
}