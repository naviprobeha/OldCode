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
    public partial class StoreList : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet storeListDataSet;

        private string _documentNo = "";

        public StoreList(Configuration configuration, SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            updateGrid();

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            storeListDataSet = DataStoreHeader.getDataSet(smartDatabase);

            storeListGrid.DataSource = storeListDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }


        public Document getDocument()
        {
            if (_documentNo != "")
            {
                return new Document(0, _documentNo);
            }
            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (storeListGrid.BindingContext[storeListGrid.DataSource, ""].Count > 0)
            {
                _documentNo = storeListDataSet.Tables[0].Rows[storeListGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga inlagringar i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}