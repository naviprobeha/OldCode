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
    public partial class ItemBinContentList : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet binContentListDataSet;

        private string _documentNo = "";
        private int _lineNo = 0;
        private string _binCode = "";        


        public ItemBinContentList(Configuration configuration, SmartDatabase smartDatabase, string documentNo, int lineNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = documentNo;
            this._lineNo = lineNo;
            updateGrid();
        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            binContentListDataSet = DataItemBinContent.getDataSet(smartDatabase);

            binContentListGrid.DataSource = binContentListDataSet.Tables[0];
            
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

        public string getValue()
        {
            try
            {
                return (string)binContentListDataSet.Tables[0].Rows[binContentListGrid.CurrentRowIndex].ItemArray.GetValue(0);
            }
            catch (Exception) { }

            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (binContentListGrid.BindingContext[binContentListGrid.DataSource, ""].Count > 0)
            {                
                _binCode = binContentListDataSet.Tables[0].Rows[binContentListGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();

                //System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Lagerplats: " + _binCode + ", Plocklista: " + _documentNo + ", Rad:" + _lineNo + "."));
                //smartDatabase.nonQuery("UPDATE pickLine SET binCode = '" + _binCode + "' WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
                
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga lagerplatser i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}