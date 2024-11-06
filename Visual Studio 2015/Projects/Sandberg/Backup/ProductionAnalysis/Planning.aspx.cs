using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Navipro.Base.Common;
using Navipro.Sandberg.Common;

namespace ProductionAnalysis
{
    public partial class Planning : System.Web.UI.Page
    {

        protected DataSet itemJournalLineDataSet;
        private Database database;

        private string journalTemplateName;
        private string journalBatchName;
        protected int noOfSlots;
        protected string link;
        protected string inQueue;
        protected string outQueue;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Base.Common.Configuration configuration = new Navipro.Base.Common.Configuration();
            configuration.setConfigValue("navCompanyName", ConfigurationSettings.AppSettings["WEB_CompanyName"]);
            configuration.setConfigValue("serverName", ConfigurationSettings.AppSettings["DB_ServerName"]);
            configuration.setConfigValue("database", ConfigurationSettings.AppSettings["DB_DataSource"]);
            configuration.setConfigValue("userName", ConfigurationSettings.AppSettings["DB_UserName"]);
            configuration.setConfigValue("password", ConfigurationSettings.AppSettings["DB_Password"]);
            
            this.journalTemplateName = ConfigurationSettings.AppSettings["ITEM_JournalTemplateName"];
            this.journalBatchName = ConfigurationSettings.AppSettings["ITEM_JournalBatchName"];
            this.link = ConfigurationSettings.AppSettings["ITEM_Link"];

            this.inQueue = ConfigurationSettings.AppSettings["MSMQ_inQueue"];
            this.outQueue = ConfigurationSettings.AppSettings["MSMQ_outQueue"];

            database = new Database(null, configuration);

            updateData();

        }

        private void updateData()
        {
            if (!IsPostBack)
            {
                typeList.Items.Clear();
                typeList.Items.Add("Pappers- / Non-woventapeter");
                typeList.Items.Add("Limtryck");
            }

            refreshItemJournalLines();

            ItemJournalLines itemJournalLines = new ItemJournalLines();

            itemJournalLines.setItemFilter("500", "999");

            if (typeList.SelectedItem.Value == "Pappers- / Non-woventapeter")
            {
                itemJournalLines.setItemFilter("500", "999");
                header.Text = "STORK";
                noOfSlots = 16;
            }
            if (typeList.SelectedItem.Value == "Limtryck")
            {
                itemJournalLines.setItemFilter("400", "499");
                header.Text = "OLBRICH";
                noOfSlots = 6;
            }

            itemJournalLineDataSet = itemJournalLines.getDataSet(database, journalTemplateName, journalBatchName);

            this.noOfItems.Text = itemJournalLineDataSet.Tables[0].Rows.Count.ToString();

            DataSet runDataSet = itemJournalLines.getDoublesDataSet(database, journalTemplateName, journalBatchName);
            this.noOfRuns.Text = (itemJournalLineDataSet.Tables[0].Rows.Count + runDataSet.Tables[0].Rows.Count).ToString();
        }

        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshItemJournalLines()
        {
            AppServerTransporter appServerTransporter = new AppServerTransporter(inQueue, outQueue, 10000);
            appServerTransporter.transport(new ServiceRequest("", "updatePlanning", null));
        }

    }
}
