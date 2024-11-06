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
    public partial class Ship : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private int _documentType;
        private string _documentNo;
        private DataSet salesLineDataSet;


        public Ship(Configuration configuration, SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentType = documentType;
            this._documentNo = documentNo;

            //label1.Text = Translation.translate(configuration.languageCode, "EAN:");
            label2.Text = Translation.translate(configuration.languageCode, "Order: ")+_documentNo;

            updateGrid();

            itemNoCol.HeaderText = Translation.translate(configuration.languageCode, "Artikelnr");
            variantCodeCol.HeaderText = Translation.translate(configuration.languageCode, "Variantkod");
            descriptionCol.HeaderText = Translation.translate(configuration.languageCode, "Beskrivning");
            unitOfMeasureCol.HeaderText = Translation.translate(configuration.languageCode, "Enhet");
            quantityCol.HeaderText = Translation.translate(configuration.languageCode, "Antal");
            quantityShippedCol.HeaderText = Translation.translate(configuration.languageCode, "Lev. antal");
            quantityToShipCol.HeaderText = Translation.translate(configuration.languageCode, "Antal att lev.");

            button1.Text = Translation.translate(configuration.languageCode, "Avbryt");
            button11.Text = Translation.translate(configuration.languageCode, "Bokför");
            button2.Text = Translation.translate(configuration.languageCode, "Plocka");

            this.Text = Translation.translate(configuration.languageCode, "Ship");
        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            int currentRow = salesLineGrid.CurrentRowIndex;

            salesLineDataSet = DataSalesLine.getDataSet(smartDatabase, _documentType, _documentNo);

            salesLineGrid.DataSource = salesLineDataSet.Tables[0];

            if (currentRow > 0)
            {
                salesLineGrid.CurrentRowIndex = currentRow;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }


        #region Logger Members

        public void write(string message)
        {

            Application.DoEvents();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int entryNo = 0;
            if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
            {
                entryNo = (int)salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
            }

            ShipItem shipItem = new ShipItem(configuration, smartDatabase, _documentType, _documentNo, entryNo);
            shipItem.ShowDialog();
            shipItem.Dispose();

            updateGrid();
        }


        private void button11_Click(object sender, EventArgs e)
        {
            int qty = DataSalesLine.sumQuantity(smartDatabase, _documentType, _documentNo);
            int qtyToShip = DataSalesLine.sumQuantityToShip(smartDatabase, _documentType, _documentNo);

            if (System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Vill du bokföra leveransen?")+" ("+qtyToShip+" / "+qty+")", Translation.translate(configuration.languageCode, "Bokföring"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                Service service = new Service("postSalesShipment", smartDatabase, configuration);
                service.setLogger(this);
                service.serviceRequest.setServiceArgument(new Document(smartDatabase, 1, _documentType, _documentNo, salesLineDataSet));
                ServiceResponse serviceResponse = service.performService();

                if (serviceResponse != null)
                {
                    if (serviceResponse.hasErrors)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        System.Windows.Forms.Cursor.Hide();

                        System.Windows.Forms.MessageBox.Show(serviceResponse.status.description);
                    }
                    else
                    {
                        DataScanLine.deleteDocument(smartDatabase, 0, _documentType, _documentNo);
                        DataSalesLineCarton.deleteDocument(smartDatabase, _documentType, _documentNo);
                        DataSalesLine.delete(smartDatabase, _documentType, _documentNo);

                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        System.Windows.Forms.Cursor.Hide();

                        System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Leveransen bokförd."));

                        this.Close();
                    }
                }
            }
        }


    }
}