using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
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
            this.dataSetup = smartDatabase.getSetup();
            this.agent = dataSetup.getAgent();

        }

        private void SynchSettings_Closing(object sender, CancelEventArgs e)
        {
            dataSetup.host = this.hostBox.Text;
            dataSetup.receiver = this.receiverBox.Text;

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

            this.agentIdBox.Text = agent.agentId;
            this.userIdBox.Text = agent.userName;
            this.passwordBox.Text = agent.password;

        }
    }
}