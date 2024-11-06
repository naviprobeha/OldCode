using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class Setup : Form
    {
        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private Agent agent;

        public Setup(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataSetup = new DataSetup(smartDatabase);
            agent = new Agent(smartDatabase);

        }

        private void Setup_Closing(object sender, CancelEventArgs e)
        {
            dataSetup.host = this.hostBox.Text;
            dataSetup.receiver = this.receiverBox.Text;

            dataSetup.spiraEnabled = spiraEnabled.Checked;
            dataSetup.spiraDelimiter = delimiterBox.Text;

            agent.agentId = this.agentIdBox.Text;
            agent.userName = this.userIdBox.Text;
            agent.password = this.passwordBox.Text;

            
            dataSetup.itemScannerEnabled = scannerEnabled.Checked;
            dataSetup.deliveryDateToday = deliveryDateToday.Checked;

            dataSetup.askSynchronization = askSynchronization.Checked;
            dataSetup.askPostingMethod = askPostingMethod.Checked;

            dataSetup.itemSearchMethod = itemSearchMethod.SelectedIndex;

            
            dataSetup.showOrderItemsBaseUnit = showBaseUnit.Checked;
            dataSetup.showOrderItemsDeliveryDate = showDeliveryDate.Checked;
            dataSetup.showOrderItemsItemNo = showItemNo.Checked;
            dataSetup.showOrderItemsVariant = showVariant.Checked;

            dataSetup.useDynamicPrices = useDynPrices.Checked;
            
            //dataSetup.itemDeletionDays = int.Parse(itemDeletionDaysBox.Text);

            dataSetup.printOnLocalPrinter = this.printOnLocalPrinter.Checked;
            

            dataSetup.save();
            agent.save();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            dataSetup.refresh();
            agent.refresh();

            this.hostBox.Text = dataSetup.host;
            this.receiverBox.Text = dataSetup.receiver;

            
            this.spiraEnabled.Checked = dataSetup.spiraEnabled;
            this.delimiterBox.Text = dataSetup.spiraDelimiter;
            this.deliveryDateToday.Checked = dataSetup.deliveryDateToday;
            
            this.agentIdBox.Text = agent.agentId;
            this.userIdBox.Text = agent.userName;
            this.passwordBox.Text = agent.password;
            
            this.scannerEnabled.Checked = dataSetup.itemScannerEnabled;
            this.askPostingMethod.Checked = dataSetup.askPostingMethod;
            this.askSynchronization.Checked = dataSetup.askSynchronization;

            this.itemSearchMethod.SelectedIndex = dataSetup.itemSearchMethod;
            
            this.showBaseUnit.Checked = dataSetup.showOrderItemsBaseUnit;
            this.showItemNo.Checked = dataSetup.showOrderItemsItemNo;
            this.showVariant.Checked = dataSetup.showOrderItemsVariant;
            this.showDeliveryDate.Checked = dataSetup.showOrderItemsDeliveryDate;

            this.useDynPrices.Checked = dataSetup.useDynamicPrices;
            
            //this.itemDeletionDaysBox.Text = dataSetup.itemDeletionDays.ToString();

            this.printOnLocalPrinter.Checked = dataSetup.printOnLocalPrinter;
             
        }

        private void hostBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void receiverBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void agentIdBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void agentIdBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void userIdBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void passwordBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void delimiterBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}