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
    public partial class MoveOption : Form, Logger
    {
        private SmartDatabase smartDatabase;
        private Configuration configuration;
        
        public MoveOption(SmartDatabase smartDatabase, Configuration configuration)
        {
            InitializeComponent();

            logViewList.Width = 240;
            logViewList.Height = 240;
            logViewList.Top = 24;
            logViewList.Left = 0;


            this.smartDatabase = smartDatabase;
            this.configuration = configuration;

            this.wagonBox.Focus();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wagonBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Du måste scanna en vagn.");
                return;
            }

            //NAVComm.checkPickWagon(configuration, smartDatabase, this, _currentPickLine);

            MovePickOut movePickOut = new MovePickOut(smartDatabase, configuration, wagonBox.Text);
            movePickOut.ShowDialog();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (wagonBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Du måste scanna en vagn.");
                return;
            }

            showStoreForm();
        }

        private void showStoreForm()
        {
            logViewList.Items.Clear();
            logViewList.Visible = true;

            DataStoreLine.deleteAll(smartDatabase, wagonBox.Text);

            bool result = NAVComm.getMoveStoreLines(configuration, smartDatabase, this, wagonBox.Text);

            logViewList.Visible = false;

            if (result)
            {
                if (DataStoreLine.countUnhandlesLines(smartDatabase, wagonBox.Text) > 0)
                {
                    MoveStore moveStore = new MoveStore(configuration, smartDatabase, wagonBox.Text);
                    moveStore.ShowDialog();

                    moveStore.Dispose();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Vagnen är tom."));

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
            this.Close();
        }
    }
}