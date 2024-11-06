using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Navipro.Lync.CallLogger
{
    public partial class StartForm : Form, LyncUtilities.LyncApplication
    {
        private Navipro.Lync.LyncUtilities.LyncUtility lyncUtil;

        public StartForm()
        {
            InitializeComponent();

            bool success = getLyncInstance();
            while (!success)
            {
                if (System.Windows.Forms.MessageBox.Show("Var vänlig starta Lync först. Klicka sedan OK nedan.", "Lync samtalsloggning", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false) == System.Windows.Forms.DialogResult.Cancel)
                {
                    break;
                }
                success = getLyncInstance();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);


            WindowState = FormWindowState.Minimized;
            Hide();

            if (lyncUtil == null) Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        #region LyncApplication Members

        public void fireIncomingConversationEvent(string uri, string responseGroupUri)
        {
            sendData(uri, responseGroupUri, 0);
        }

        public void fireDisconnectedConversationEvent(string uri, string responseGroupUri)
        {
            sendData(uri, responseGroupUri, 1);
        }

        public void fireIncomingAlertEvent(string uri, string responseGroupUri)
        {
            sendData(uri, responseGroupUri, 2);
        }

        private void sendData(string uri, string responseGroupUri, int mode)
        {
            try
            {
                
                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.DownloadString(System.Configuration.ConfigurationManager.AppSettings["serverUrl"] + "?sipAddress=" + urlEncode(System.Configuration.ConfigurationManager.AppSettings["sip"]) + "&uri=" + urlEncode(uri) + "&responseGroup=" + urlEncode(responseGroupUri) + "&mode=" + mode);
                
                
            }
            catch (Exception e) {

                System.Windows.Forms.MessageBox.Show("Error: " + e.Message + " (" + System.Configuration.ConfigurationManager.AppSettings["serverUrl"] + "?sipAddress=" + urlEncode(System.Configuration.ConfigurationManager.AppSettings["sip"]) + "&uri=" + urlEncode(uri) + "&responseGroup=" + urlEncode(responseGroupUri) + "&mode=" + mode+")");
            }
        }

        #endregion

        private bool getLyncInstance()
        {
            try
            {
                lyncUtil = new LyncUtilities.LyncUtility(this);
                return true;
            }
            catch (Exception) { }

            return false;

        }

        private string urlEncode(string url)
        {
            //url = url.Replace("@", "%40");

            return url;
        }
    }
}
