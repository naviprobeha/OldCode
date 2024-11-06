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
    public partial class PickCreate : Form, Logger
    {
        private DataPickConfig _dataPickConfig;
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private string _pickListNo = "";

        public PickCreate(Configuration configuration, SmartDatabase smartDatabase, DataPickConfig dataPickConfig)
        {
            InitializeComponent();

            this._dataPickConfig = dataPickConfig;
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            updateView();

            
        }

        public string pickListNo { get { return _pickListNo; } }

        private void updateView()
        {
            
            if (_dataPickConfig != null)
            {

                qtyPickBox.Text = _dataPickConfig.countPickOrders.ToString();
                qtyBulkBox.Text = _dataPickConfig.countBulkOrders.ToString();


                //System.Windows.Forms.MessageBox.Show("User count: " + _dataPickConfig.userCollection.Count);
                userBox.DataSource = _dataPickConfig.userCollection;
                userBox.DisplayMember = "name";

                regionBox.DataSource = _dataPickConfig.shipmentHeaderCollection;
                regionBox.DisplayMember = "name";
                
            }
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 
        private void fromBinBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateConfig(false);
        }

        private void updateConfig(bool createPickHeaders)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            _dataPickConfig.userNo = _dataPickConfig.getUserNoFromName(userBox.Text);
            _dataPickConfig.fromBin = fromBinBox.Text;
            _dataPickConfig.toBin = toBinBox.Text;
            _dataPickConfig.createPickHeaders = createPickHeaders;
            _dataPickConfig.wagonNo = wagonBox.Text;
            _dataPickConfig.shipmentNo = _dataPickConfig.getShipmentNoFromName(regionBox.Text);

            try
            {
                _dataPickConfig.maxOrderCount = int.Parse(maxOrderCount.Text);
            }
            catch (Exception) { }



            _dataPickConfig = NAVComm.updatePickConfig(configuration, smartDatabase, this, _dataPickConfig);

            logViewList.Visible = false;

            updateView();


        }

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void WagonBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                int i = 0;
                while (i < _dataPickConfig.wagonCollection.Count)
                {
                    DataWagon dataWagon = _dataPickConfig.wagonCollection[i];
                    if (dataWagon.code == wagonBox.Text)
                    {
                        maxOrderCount.Text = dataWagon.noOfOrders.ToString();
                        return;
                    }
                    i++;
                }

                e.Handled = true;
                wagonBox.Text = "";
                wagonBox.Focus();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (userBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Du måste ange plockanvändare.");
                return;
            }
            if (wagonBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Du måste ange plockvagn.");
                return;
            }

            updateConfig(true);

            if (_dataPickConfig != null)
            {
                if ((_dataPickConfig.createdPickListNo != null) && (_dataPickConfig.createdPickListNo != ""))
                {
                    _pickListNo = _dataPickConfig.createdPickListNo;

                    this.Close();
                }
            }

            updateView();
        }
    }
}