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

            //shipBtn.Text = Translation.translate(configuration.languageCode, "Utleverans");
            //receiveBtn.Text = Translation.translate(configuration.languageCode, "Inleverans");
            //physInvBtn.Text = Translation.translate(configuration.languageCode, "Inventering");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            bool result = NAVComm.getPickLists(configuration, smartDatabase, this);

            logViewList.Visible = false;

            if (result)
            {
                PickList pickList = new PickList(configuration, smartDatabase);
                pickList.ShowDialog();


                Document document = pickList.getDocument();

                pickList.Dispose();

                if (document != null)
                {
                    showPickForm(document.documentNo);  
                }
            }
        }

        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;

            DataPickConfig dataPickConfig = NAVComm.updatePickConfig(configuration, smartDatabase, this, null);

            logViewList.Visible = false;

            if (dataPickConfig != null)
            {
                PickCreate pickCreate = new PickCreate(configuration, smartDatabase, dataPickConfig);
                pickCreate.ShowDialog();

                if (pickCreate.pickListNo != "")
                {
                    pickCreate.Dispose();

                    showPickForm(pickCreate.pickListNo);
                   
                }
                else
                {
                    pickCreate.Dispose();
                }
            }
        }

        private void showPickForm(string pickListNo)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;


            bool result = NAVComm.getFirstPickLine(configuration, smartDatabase, this, pickListNo);

            logViewList.Visible = false;

            if (result)
            {
                if (DataPickLine.countUnpickedLines(smartDatabase, pickListNo) > 0)
                {
                    PickItem pickItem = new PickItem(configuration, smartDatabase, pickListNo);
                    pickItem.ShowDialog();

                    pickItem.Dispose();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är redan plockade."));

                }
            }

        }

        private void showStoreForm(string putAwayNo)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;


            bool result = NAVComm.getStoreLines(configuration, smartDatabase, this, putAwayNo);

            logViewList.Visible = false;

            if (result)
            {
                if (DataStoreLine.countUnhandlesLines(smartDatabase, putAwayNo) > 0)
                {
                    Storing storing = new Storing(configuration, smartDatabase, putAwayNo);
                    storing.ShowDialog();

                    storing.Dispose();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Alla rader är redan placerade."));

                }
            }

        }

        private void showReceiptForm(Document document)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;


            bool result = NAVComm.getReceiptLines(configuration, smartDatabase, this, document);

            logViewList.Visible = false;

            if (result)
            {
                ReceiptWorksheet receiptWorksheet = new ReceiptWorksheet(configuration, smartDatabase, document.documentNo);
                receiptWorksheet.ShowDialog();

                receiptWorksheet.Dispose();
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            DataPickConfig dataPickConfig = NAVComm.updatePickConfig(configuration, smartDatabase, this, null);

            logViewList.Visible = false;

            if (dataPickConfig != null)
            {
                InventoryOption invOption = new InventoryOption(smartDatabase, configuration, dataPickConfig);
                invOption.ShowDialog();

                invOption.Dispose();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            bool result = NAVComm.getReceiptLists(configuration, smartDatabase, this);

            logViewList.Visible = false;

            if (result)
            {
                ReceiptList receiptList = new ReceiptList(configuration, smartDatabase);
                receiptList.ShowDialog();


                Document document = receiptList.getDocument();

                receiptList.Dispose();

                if (document != null)
                {
                    showReceiptForm(document);
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            bool result = NAVComm.getRetailShipmentList(configuration, smartDatabase, this);

            logViewList.Visible = false;

            if (result)
            {
                DataPickConfig dataPickConfig = NAVComm.updatePickConfig(configuration, smartDatabase, this, null);

                PickRetailCreate pickRetailCreate = new PickRetailCreate(configuration, smartDatabase, dataPickConfig);
                pickRetailCreate.ShowDialog();

                if (pickRetailCreate.pickListNo != "")
                {
                    pickRetailCreate.Dispose();

                    showPickForm(pickRetailCreate.pickListNo);

                }
                else
                {
                    pickRetailCreate.Dispose();
                }


            }

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MoveOption moveOption = new MoveOption(smartDatabase, configuration);
            moveOption.ShowDialog();

            moveOption.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;
            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            bool result = NAVComm.getStoreLists(configuration, smartDatabase, this);

            logViewList.Visible = false;

            if (result)
            {
                StoreList storeList = new StoreList(configuration, smartDatabase);
                storeList.ShowDialog();


                Document document = storeList.getDocument();

                storeList.Dispose();

                if (document != null)
                {
                    showStoreForm(document.documentNo);
                }
            }
        }
    }
}