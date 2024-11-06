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
    public partial class StartForm : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;

        public StartForm()
        {
            InitializeComponent();

            configuration = new Configuration();
            smartDatabase = new SmartDatabase("\\SmartInventory.sdf");
            if (!smartDatabase.init()) smartDatabase.createDatabase();

            shipBtn.Text = Translation.translate(configuration.languageCode, "Utleverans");
            receiveBtn.Text = Translation.translate(configuration.languageCode, "Inleverans");
            physInvBtn.Text = Translation.translate(configuration.languageCode, "Inventering");
        }

        private void receiveBtn_Click(object sender, EventArgs e)
        {
            ReceivePONo receivePONo = new ReceivePONo(configuration, smartDatabase);
            receivePONo.ShowDialog();

            Document document = receivePONo.getDocument();

            receivePONo.Dispose();

            if (document != null)
            {
                Receive receive = new Receive(configuration, smartDatabase, document.documentType, document.documentNo);
                receive.ShowDialog();

                receive.Dispose();            

            }

        }

 

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void shipBtn_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            bool result = NAVComm.getSalesOrders(configuration, smartDatabase, this);

            logViewList.Visible = false;

            if (result)
            {
                ShipList shipList = new ShipList(configuration, smartDatabase);
                shipList.ShowDialog();

                Document document = shipList.getDocument();

                shipList.Dispose();

                if (document != null)
                {
                    logViewList.Items.Clear();
                    logViewList.Visible = true;

                    result = NAVComm.getSalesOrder(configuration, smartDatabase, this, document.documentType, document.documentNo);

                    logViewList.Visible = false;

                    if (result)
                    {
                        Ship ship = new Ship(configuration, smartDatabase, document.documentType, document.documentNo);
                        ship.ShowDialog();

                        ship.Dispose();
                    }


                }
            }
        }

        private void physInvBtn_Click(object sender, EventArgs e)
        {
            PhysInventory physInventory = new PhysInventory(configuration, smartDatabase);
            physInventory.ShowDialog();
        }
    }
}