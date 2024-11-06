using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Navipro.SmartInventory
{
    public partial class ReceivePONo : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private int _documentType = 0;
        private string _documentNo = "";

        public ReceivePONo(Configuration configuration, SmartDatabase smartDatabase)
        {
            InitializeComponent();

           
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            
            label2.Text = Translation.translate(configuration.languageCode, "Ange inköpsordernr.");
            label1.Text = Translation.translate(configuration.languageCode, "Inköpsordernr:");
            button11.Text = Translation.translate(configuration.languageCode, "OK");
            button12.Text = Translation.translate(configuration.languageCode, "Avbryt");


            button13.Text = configuration.purchaseOrderNoPrefix;
            poNoBox.Focus();
            
        }

        public void keyPressed(string key)
        {
            if (key == "CL")
            {
                poNoBox.Text = "";
            }
            else
            {
                poNoBox.Text = poNoBox.Text + key;
            }

            poNoBox.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            keyPressed(((Button)sender).Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            logViewBox.Items.Clear();
            logViewBox.Visible = true;

            bool result = NAVComm.getPurchaseOrder(configuration, smartDatabase, this, 0, poNoBox.Text);

            _documentNo = poNoBox.Text;
            _documentType = DataPurchaseLine.getDocumentType(smartDatabase, poNoBox.Text);

            logViewBox.Visible = false;
            if (result) this.Close();

        }



        private void button12_Click(object sender, EventArgs e)
        {
            poNoBox.Text = "";
            this.Close();
        }

        public Document getDocument()
        {
            if (_documentNo != "")
            {
                return new Document(_documentType, _documentNo);
            }
            return null;
        }

 


        #region Logger Members

        public void write(string message)
        {
            logViewBox.Items.Add(message);
            Application.DoEvents();
        }

        #endregion
    }
}