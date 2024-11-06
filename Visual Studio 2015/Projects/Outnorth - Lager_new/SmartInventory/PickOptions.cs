﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class PickOptions : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private DataPickLine _currentPickLine;
        private bool _fetchNewLines;

        public PickOptions(SmartDatabase smartDatabase, Configuration configuration, DataPickLine currentPickLine)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.configuration = configuration;
            this._currentPickLine = currentPickLine;

            logViewList.Items.Clear();
            logViewList.Visible = false;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Lägg tillbaka de ej registrerade artiklarna på hyllan (" + _currentPickLine.pickedQty + " st). Fortsätta?", "Återlagring", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                logViewList.Visible = true;

                setZeroQuantity();

                NAVComm.reportUnPickableLine(configuration, smartDatabase, this, _currentPickLine);
                logViewList.Visible = false;
                _fetchNewLines = true;
                this.Close();
            }
        }

        private void setZeroQuantity()
        {
            DataInventoryItem dataInventoryItem = new DataInventoryItem(smartDatabase);
            dataInventoryItem.binCode = _currentPickLine.binCode;
            dataInventoryItem.eanCode = _currentPickLine.usedEanCode;

            dataInventoryItem.quantity = 0;

            NAVComm.setPhysInventoryItemQty(configuration, smartDatabase, this, dataInventoryItem);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryQuick invQuick = new InventoryQuick(smartDatabase, configuration);
            invQuick.ShowDialog();

            invQuick.Dispose();
        }

        public bool checkFetchStatus()
        {
            return _fetchNewLines;
        }

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           

        }

        #endregion
    }
}