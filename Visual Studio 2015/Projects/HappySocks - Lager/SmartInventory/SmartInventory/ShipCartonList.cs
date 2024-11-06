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
    public partial class ShipCartonList : Form
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private DataSalesLine _currentSalesLine;
        private DataSet cartonDataSet;

        public ShipCartonList(Configuration configuration, SmartDatabase smartDatabase, DataSalesLine dataSalesLine)
        {
            InitializeComponent();

            label1.Text = Translation.translate(configuration.languageCode, "Artikelnr");
            label3.Text = Translation.translate(configuration.languageCode, "Variantkod");
            label2.Text = Translation.translate(configuration.languageCode, "Kartonger");
            button11.Text = Translation.translate(configuration.languageCode, "Stäng");
            button1.Text = Translation.translate(configuration.languageCode, "Radera");
            button2.Text = Translation.translate(configuration.languageCode, "Ändra");

            cartonCol.HeaderText = Translation.translate(configuration.languageCode, "Nr");
            splitOnQtyCol.HeaderText = Translation.translate(configuration.languageCode, "Dela vid antal");

            
            this.smartDatabase = smartDatabase;
            this.configuration = configuration;
            this._currentSalesLine = dataSalesLine;

            updateView();
        }

        private void updateView()
        {
            itemNoBox.Text = _currentSalesLine.itemNo;
            variantCodeBox.Text = _currentSalesLine.variantCode;


            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            cartonDataSet = DataSalesLineCarton.getDataSet(smartDatabase, _currentSalesLine.documentType, _currentSalesLine.documentNo, _currentSalesLine.lineNo);

            cartonGrid.DataSource = cartonDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (cartonGrid.BindingContext[cartonGrid.DataSource, ""].Count > 0)
            {
                if (MessageBox.Show(Translation.translate(configuration.languageCode, "Radera kartong?"), Translation.translate(configuration.languageCode, "Radera"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int entryNo = int.Parse(cartonDataSet.Tables[0].Rows[cartonGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString());

                    DataSalesLineCarton.deleteEntry(smartDatabase, entryNo);
                    updateView();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga rader i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cartonGrid.BindingContext[cartonGrid.DataSource, ""].Count > 0)
            {
                CartonPad cartonPad = new CartonPad(configuration, smartDatabase, cartonDataSet.Tables[0].Rows[cartonGrid.CurrentRowIndex].ItemArray.GetValue(4).ToString());
                cartonPad.ShowDialog();
                if (cartonPad.getStatus() == 1)
                {
                    int entryNo = int.Parse(cartonDataSet.Tables[0].Rows[cartonGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString());

                    DataSalesLineCarton.updateCartonNo(smartDatabase, entryNo, cartonPad.getCartonNo());

                    updateView();
                }
            }
        }
    }
}