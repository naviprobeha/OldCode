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
    public partial class ReceiptList : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet receiptListDataSet;

        private string _documentNo = "";
        private string _userIdCode = "";

        public ReceiptList(Configuration configuration, SmartDatabase smartDatabase)
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

            receiptListDataSet = DataReceiptHeader.getDataSet(smartDatabase);

            receiptListGrid.DataSource = receiptListDataSet.Tables[0];

            userBox.DataSource = configuration.userCollection;
            userBox.DisplayMember = "name";

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }


        public Document getDocument()
        {
            if (_documentNo != "")
            {
                Document document = new Document(0, _documentNo);
                document.userId = _userIdCode;
                return document;
            }
            return null;
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (receiptListGrid.BindingContext[receiptListGrid.DataSource, ""].Count > 0)
            {
                _userIdCode = configuration.getUserNoFromName(userBox.Text);
                if (_userIdCode == "-")
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Du måste välja en användare i rutan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }
                _documentNo = receiptListDataSet.Tables[0].Rows[receiptListGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();                
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga inleveransuppdrag i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddItemToReceiptWorksheet addItemToReceiptWksh = new AddItemToReceiptWorksheet(configuration, smartDatabase);
            addItemToReceiptWksh.ShowDialog();

            string whseDocNo = addItemToReceiptWksh.getWhseDocNo();
            addItemToReceiptWksh.Dispose();


            if (whseDocNo != "")
            {
                _documentNo = whseDocNo;
                this.Close();

            }

        }
    }
}