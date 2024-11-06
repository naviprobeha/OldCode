using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class SynchSettings : Form
    {
        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private Agent agent;

        public SynchSettings(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            dataSetup = new DataSetup(smartDatabase);
            agent = new Agent(smartDatabase);


        }


        private void SynchSettings_Closing(object sender, CancelEventArgs e)
        {
            dataSetup.host = this.hostBox.Text;
            dataSetup.receiver = this.receiverBox.Text;
            dataSetup.synchMethod = this.synchMethod.SelectedIndex;
            dataSetup.locationCode = this.locationCodeBox.Text;
            dataSetup.allowDecimal = this.allowDecimal.Checked;

            agent.agentId = this.agentIdBox.Text;
            agent.userName = this.userIdBox.Text;
            agent.password = this.passwordBox.Text;

            dataSetup.save();
            agent.save();
        }

        private void SynchSettings_Load(object sender, EventArgs e)
        {
            dataSetup.refresh();
            agent.refresh();

            this.hostBox.Text = dataSetup.host;
            this.receiverBox.Text = dataSetup.receiver;
            this.synchMethod.SelectedIndex = dataSetup.synchMethod;
            this.locationCodeBox.Text = dataSetup.locationCode;
            this.allowDecimal.Checked = dataSetup.allowDecimal;

            this.agentIdBox.Text = agent.agentId;
            this.userIdBox.Text = agent.userName;
            this.passwordBox.Text = agent.password;
        }

        private void hostBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void receiverBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void agentIdBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void userIdBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void passwordBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void locationCodeBox_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }
    }
}