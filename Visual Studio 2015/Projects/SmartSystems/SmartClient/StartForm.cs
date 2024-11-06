using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Navipro.SmartSystems.SmartLibrary;

namespace Navipro.SmartSystems.SmartClient
{
    public partial class StartForm : Form, Logger
    {
        Timer timer = new Timer();
        Configuration internalConfiguration;
        XmlConfiguration xmlConfiguration;

        Modules modules;

        public StartForm()
        {
            InitializeComponent();

            internalConfiguration = new Configuration();
            xmlConfiguration = new XmlConfiguration();

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            init();            
        }

        private void init()
        {
            showActivity("Läser modulkonfiguration...");
            modules = new Modules();

            showActivity("Läser konfiguration...");
            if (xmlConfiguration.loadConfiguration(internalConfiguration))
            {
                showActivity("Server: " + internalConfiguration.getConfigValue("serverUrl"));

                SmartRegistration smartRegistration = new SmartRegistration(internalConfiguration, this);
                if (smartRegistration.performRegistration())
                {
                    xmlConfiguration.saveConfiguration(internalConfiguration);

                    SmartUpdate smartUpdate = new SmartUpdate(internalConfiguration, this);
                    if (smartUpdate.performUpdate(modules))
                    {

                    }
                }

                loadCore();

            }
            else
            {
                showActivity("Kunde inte läsa konfigurationen.");
            }

        }

        private void loadCore()
        {
            Hashtable coreTable = modules.getModuleTable(0);

            IDictionaryEnumerator coreTableEnum = coreTable.GetEnumerator();
            if (coreTableEnum.MoveNext())
            {
                Module core = (Module)coreTableEnum.Value;

                showActivity("Startar "+core.name);


            }
            else
            {
                showActivity("Ingen kärna installerad.");
            }




        }

        private void showActivity(string text)
        {
            if (logLabel3.Text != "")
            {
                if (logLabel2.Text != "")
                {
                    logLabel1.Text = logLabel2.Text;
                }
                logLabel2.Text = logLabel3.Text;
            }
            if (text.Length > 50) text = text.Substring(0, 35) + "...";
            logLabel3.Text = text;

            Application.DoEvents();
        }

        private void updateActivity(string text)
        {
            logLabel3.Text = logLabel3.Text + " "+ text;

            Application.DoEvents();
        }

 
        #region Logger Members

        public void write(string source, int level, string message)
        {
            if (level == 0) showActivity(message);
            if (level == 1) updateActivity(message);
        }

        #endregion
    }
}