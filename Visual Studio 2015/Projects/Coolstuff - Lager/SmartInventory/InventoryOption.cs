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
    public partial class InventoryOption : Form
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        private DataPickConfig _dataPickConfig;

        
        public InventoryOption(SmartDatabase smartDatabase, Configuration configuration, DataPickConfig dataPickConfig)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.configuration = configuration;
            this._dataPickConfig = dataPickConfig;

            updateView();
        }

        private void updateView()
        {

            if (_dataPickConfig != null)
            {
                userBox.DataSource = _dataPickConfig.userCollection;
                userBox.DisplayMember = "name";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryJournal invJournal = new InventoryJournal(smartDatabase, configuration, _dataPickConfig.getUserNoFromName(userBox.Text));
            invJournal.ShowDialog();

            invJournal.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InventoryQuick invQuick = new InventoryQuick(smartDatabase, configuration);
            invQuick.ShowDialog();

            invQuick.Dispose();
        }

        private void binBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}