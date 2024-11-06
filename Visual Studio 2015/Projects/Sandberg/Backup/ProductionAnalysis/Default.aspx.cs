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
using Navipro.Sandberg.Common;
using Navipro.Base.Common;

namespace ProductionAnalysis
{
    public partial class _Default : System.Web.UI.Page
    {
        protected int mode;
        protected DataSet productionEntryDataSet;
        protected CommentLines commentLines;
        protected Database database;

        protected float color1Value;
        protected float color2Value;
        protected float color3Value;
        protected float color4Value;
        protected float color5Value;
        protected float color6Value;
        protected float color7Value;
        protected float color8Value;
        protected float primaValue;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Base.Common.Configuration configuration = new Navipro.Base.Common.Configuration();
            configuration.setConfigValue("navCompanyName", ConfigurationSettings.AppSettings["WEB_CompanyName"]);
            configuration.setConfigValue("serverName", ConfigurationSettings.AppSettings["DB_ServerName"]);
            configuration.setConfigValue("database", ConfigurationSettings.AppSettings["DB_DataSource"]);
            configuration.setConfigValue("userName", ConfigurationSettings.AppSettings["DB_UserName"]);
            configuration.setConfigValue("password", ConfigurationSettings.AppSettings["DB_Password"]);

            database = new Database(null, configuration);

            commentLines = new CommentLines();

            dataPanel.Visible = false;
            updateData();

        }

        private void updateData()
        {
            if (mode == 1)
            {
                dataPanel.Visible = true;

                ProductionEntries productionEntries = new ProductionEntries();
                productionEntryDataSet = productionEntries.getDataSet(database, itemNoBox.Text);
                if (productionEntryDataSet.Tables[0].Rows.Count == 0)
                {
                    string baseItemNo = itemNoBox.Text;
                    if (baseItemNo.Contains("-"))
                    {
                        baseItemNo = baseItemNo.Substring(0, baseItemNo.IndexOf("-"));

                        productionEntryDataSet = productionEntries.getDataSet(database, baseItemNo);

                    }

                }

                string calcItemNoText = calcItemNo.Text;

                calcItemNo.Items.Clear();
                calcItemNo.Items.Add(new ListItem("Alla", itemNoBox.Text));
                int i = 0;
                while (i < productionEntryDataSet.Tables[0].Rows.Count)
                {
                    ProductionEntry productionEntry = new ProductionEntry(productionEntryDataSet.Tables[0].Rows[i]);                    
                    calcItemNo.Items.Add(new ListItem(productionEntry.itemNo));

                    i++;
                }

                if (calcItemNoText != "") calcItemNo.Text = calcItemNoText;
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            mode = 1;
            calcItemNo.Items.Clear();
            updateData();
        }

        protected void calcBtn_Click(object sender, EventArgs e)
        {

            mode = 1;
            
            float qtyMaterial = 0;

            try
            {
                qtyMaterial = float.Parse(qtyOfKgMaterial.Text);
            }
            catch(Exception) {}

            if (qtyMaterial > 0)
            {
                calculateColorConsumption(qtyMaterial);
            }

            updateData();
        }

        protected void calculateColorConsumption(float qtyMaterial)
        {
            ProductionEntries productionEntries = new ProductionEntries();
            DataSet productionEntryDataSet = productionEntries.getDataSet(database, calcItemNo.Text);
            int i = 0;
            while (i < productionEntryDataSet.Tables[0].Rows.Count)
            {
                ProductionEntry productionEntry = new ProductionEntry(productionEntryDataSet.Tables[0].Rows[i]);
                color1Value = color1Value + (productionEntry.color1Kg / productionEntry.kgOfPaper);
                color2Value = color2Value + (productionEntry.color2Kg / productionEntry.kgOfPaper);
                color3Value = color3Value + (productionEntry.color3Kg / productionEntry.kgOfPaper);
                color4Value = color4Value + (productionEntry.color4Kg / productionEntry.kgOfPaper);
                color5Value = color5Value + (productionEntry.color5Kg / productionEntry.kgOfPaper);
                color6Value = color6Value + (productionEntry.color6Kg / productionEntry.kgOfPaper);
                color7Value = color7Value + (productionEntry.color7Kg / productionEntry.kgOfPaper);
                color8Value = color8Value + (productionEntry.color8Kg / productionEntry.kgOfPaper);
                primaValue = primaValue + productionEntry.primaPercent;
                i++;
            }

            color1Value = color1Value / productionEntryDataSet.Tables[0].Rows.Count;
            color2Value = color2Value / productionEntryDataSet.Tables[0].Rows.Count;
            color3Value = color3Value / productionEntryDataSet.Tables[0].Rows.Count;
            color4Value = color4Value / productionEntryDataSet.Tables[0].Rows.Count;
            color5Value = color5Value / productionEntryDataSet.Tables[0].Rows.Count;
            color6Value = color6Value / productionEntryDataSet.Tables[0].Rows.Count;
            color7Value = color7Value / productionEntryDataSet.Tables[0].Rows.Count;
            color8Value = color8Value / productionEntryDataSet.Tables[0].Rows.Count;
            primaValue = primaValue / productionEntryDataSet.Tables[0].Rows.Count;

            color1Value = color1Value * qtyMaterial;
            color2Value = color2Value * qtyMaterial;
            color3Value = color3Value * qtyMaterial;
            color4Value = color4Value * qtyMaterial;
            color5Value = color5Value * qtyMaterial;
            color6Value = color6Value * qtyMaterial;
            color7Value = color7Value * qtyMaterial;
            color8Value = color8Value * qtyMaterial;

        }

        protected void itemNoBox_TextChanged(object sender, EventArgs e)
        {
            mode = 1;
            calcItemNo.Items.Clear();
            updateData();

        }

    }
}
