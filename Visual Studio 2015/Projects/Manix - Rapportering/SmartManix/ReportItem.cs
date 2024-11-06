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
    public partial class ReportItem : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        private DataUser _dataUser;
        private DataReportItem _dataReportItem;

        private bool okToShowQtyForm;
        private int _currentItem = 1;
        private int _itemCount;


        public ReportItem(Configuration configuration, SmartDatabase smartDatabase, DataUser dataUser)
        {
            InitializeComponent();
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._dataUser = dataUser;
            this._dataReportItem = new DataReportItem();

            updateView();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

        }

        private void updateView()
        {
            userBox.Text = _dataUser.code + " " + _dataUser.name;

            if (_dataReportItem != null)
            {
                prodOrderNoBox.Text = _dataReportItem.prodOrderNo;
                prodOrderLineNoBox.Text = _dataReportItem.prodOrderLineNo.ToString();
                itemNoBox.Text = _dataReportItem.itemNo;
                descriptionBox.Text = _dataReportItem.description;
                statusBox.Text = _dataReportItem.status;
            }

            scanBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button2_GotFocus(object sender, EventArgs e)
        {
            okToShowQtyForm = true;
        }


        private void report()
        {
            logViewList.Visible = true;

            _dataReportItem.id = scanBox.Text;
            _dataReportItem.userCode = _dataUser.code;
            scanBox.Text = "";
            
            if (NAVComm.reportItem(configuration, smartDatabase, this, ref _dataReportItem))
            {

                Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
                
            }
            else
            {

                Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

            }

            updateView();

            logViewList.Visible = false;
            logViewList.Items.Clear();
        }


    
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

 

        #region Logger Members

        void Logger.write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           

        }

        #endregion

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;

                report();

            }
        }
    }
}