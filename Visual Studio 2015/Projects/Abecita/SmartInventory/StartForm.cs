using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class StartForm : Form
    {
        private SmartDatabase smartDatabase;
        private const string dbFileName = "\\Flash File Store\\SmartInventory.sdf";


        static void Main()
        {
            Application.Run(new StartForm());
        }

        public StartForm()
        {
            InitializeComponent();

            smartDatabase = new SmartDatabase(dbFileName);
            if (!smartDatabase.init())
            {
                smartDatabase.createDatabase();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StoreItemQuantity storeItemQuantity = new StoreItemQuantity(smartDatabase);
            storeItemQuantity.ShowDialog();
            storeItemQuantity.Dispose();
        }

        private void menu1_Click(object sender, EventArgs e)
        {
            StoreSynch storeSynch = new StoreSynch(smartDatabase);
            storeSynch.ShowDialog();
            storeSynch.Dispose();
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            JobSynch jobSynch = new JobSynch(smartDatabase);
            jobSynch.ShowDialog();
            jobSynch.Dispose();
        }

        private void menu3_Click(object sender, EventArgs e)
        {
            Maintenance maint = new Maintenance(smartDatabase);
            maint.ShowDialog();
            maint.Dispose();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Synchronize synchronize = new Synchronize(smartDatabase);
            synchronize.ShowDialog();
            synchronize.Dispose();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            SynchSettings synchSettings = new SynchSettings(smartDatabase);
            synchSettings.ShowDialog();
            synchSettings.Dispose();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StoreItemInstant storeItemInstant = new StoreItemInstant(smartDatabase);
            storeItemInstant.ShowDialog();
            storeItemInstant.Dispose();

        }

    }
}