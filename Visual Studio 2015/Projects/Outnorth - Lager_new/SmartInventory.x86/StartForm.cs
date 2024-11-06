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


        public StartForm()
        {
            InitializeComponent();

            configuration = new Configuration();

            //shipBtn.Text = Translation.translate(configuration.languageCode, "Utleverans");
            //receiveBtn.Text = Translation.translate(configuration.languageCode, "Inleverans");
            //physInvBtn.Text = Translation.translate(configuration.languageCode, "Inventering");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region Logger Members

        public void write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();           
        }

        #endregion

 
 
 
        private void button2_Click(object sender, EventArgs e)
        {
            InventoryOption invOption = new InventoryOption(configuration);
            invOption.ShowDialog();

            invOption.Dispose();
        }


    }
}