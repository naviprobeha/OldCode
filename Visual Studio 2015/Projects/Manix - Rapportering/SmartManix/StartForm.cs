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

            DataUserCollection dataUserCollection = NAVComm.getUsers(configuration, smartDatabase, this);
            if (dataUserCollection == null) dataUserCollection = new DataUserCollection();

            logViewList.Visible = false;

            Users users = new Users(configuration, smartDatabase, dataUserCollection);
            users.ShowDialog();


            DataUser dataUser = users.getUser();

            users.Dispose();

            if (dataUser != null)
            {
                showReportForm(dataUser);
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

        }

        private void showReportForm(DataUser dataUser)
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;


            bool result = NAVComm.verifyUser(configuration, smartDatabase, this, dataUser);

            logViewList.Visible = false;

            if (result)
            {
                ReportItem reportItem = new ReportItem(configuration, smartDatabase, dataUser);
                reportItem.ShowDialog();

                reportItem.Dispose();
            }

        }

 
        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
 
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}