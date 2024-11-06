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
    public partial class PickRetailCreate : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet shipmentListDataSet;

        private string _documentNo = "";
        private string _pickListNo = "";
        private DataPickConfig _dataPickConfig;

        public PickRetailCreate(Configuration configuration, SmartDatabase smartDatabase, DataPickConfig dataPickConfig)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            _dataPickConfig = dataPickConfig;

            updateGrid();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            shipmentListDataSet = DataShipmentHeader.getDataSet(smartDatabase);

            shipmentListGrid.DataSource = shipmentListDataSet.Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            if (_dataPickConfig != null)
            {
                userBox.DataSource = _dataPickConfig.userCollection;
                userBox.DisplayMember = "name";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (shipmentListGrid.BindingContext[shipmentListGrid.DataSource, ""].Count > 0)
            {
                _documentNo = shipmentListDataSet.Tables[0].Rows[shipmentListGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();
                createPickList();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga plocksedlar i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }

        }

        private void createPickList()
        {
            logViewList.Visible = true;


            _dataPickConfig.shipmentNo = _documentNo;
            _dataPickConfig.wagonNo = wagonBox.Text;
            _dataPickConfig.userNo = _dataPickConfig.getUserNoFromName(userBox.Text);

            DataPickConfig dataPickConfig = NAVComm.createRetailPickList(configuration, smartDatabase, this, _dataPickConfig);
            if (dataPickConfig != null)
            {
                _pickListNo = dataPickConfig.createdPickListNo;
                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                this.Close();

            }
            else
            {
                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
            }

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }

        public string pickListNo { get { return _pickListNo; } }


        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
            
        }

        #endregion

        private void WagonBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}