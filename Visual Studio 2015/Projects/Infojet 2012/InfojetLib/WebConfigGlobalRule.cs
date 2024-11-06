using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebConfigGlobalRule
    {
        private string _webConfigModelNo;
        private string _webConfigRuleCode;
        private string _optionCode;
        private int _type;
        private string _value;
        private string _description;
        private int _sortOrder;
        private int _action;
           
        private Infojet infojetContext;


        public WebConfigGlobalRule(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._webConfigModelNo = dataRow.ItemArray.GetValue(0).ToString();
            this._webConfigRuleCode = dataRow.ItemArray.GetValue(1).ToString();
            this._optionCode = dataRow.ItemArray.GetValue(2).ToString();
            this._type = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this._value = dataRow.ItemArray.GetValue(4).ToString();
            this._description = dataRow.ItemArray.GetValue(5).ToString();
            this._sortOrder = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this._action = int.Parse(dataRow.ItemArray.GetValue(7).ToString());
        }

        public string webConfigModelNo { get { return _webConfigModelNo; } set { } }
        public string webConfigRuleCode { get { return _webConfigRuleCode; } set { } }
        public string optionCode { get { return _optionCode; } set { } }
        public int type { get { return _type; } set { } }
        public string value { get { return _value; } set { } }
        public string description { get { return _description; } set { } }
        public int sortOrder { get { return _sortOrder; } set { } }
        public int action { get { return _action; } set { } }


        public static DataSet getGlobalRules(Infojet infojetContext, string webConfigModelNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Config Model No_], [Web Config Rule Code], [Option Code], [Type], [Value], [Description], [Sort Order], [Action] FROM [" + infojetContext.systemDatabase.getTableName("Web Config Global Rule") + "] WHERE [Web Config Model No_] = @webConfigModelNo ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("@webConfigModelNo", webConfigModelNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;


        }

        public static void processGlobalRules(Infojet infojetContext, WebItemConfigHeader webItemConfigHeader)
        {
            DataSet ruleDataSet = getGlobalRules(infojetContext, webItemConfigHeader.webConfigModelNo);

            int i = 0;
            while (i < ruleDataSet.Tables[0].Rows.Count)
            {
                WebConfigGlobalRule webConfigGlobalRule = new WebConfigGlobalRule(infojetContext, ruleDataSet.Tables[0].Rows[i]);

                if (WebConfigRule.evaluateRule(infojetContext, webItemConfigHeader, webConfigGlobalRule.webConfigRuleCode))
                {
                   
                    WebItemConfigLine webItemConfigLine = webItemConfigHeader.getWebItemConfigLine(webConfigGlobalRule.optionCode);

                    if (webConfigGlobalRule.action == 0)
                    {
                        webItemConfigLine.visible = true;
                    }
                    if (webConfigGlobalRule.action == 1)
                    {
                        webItemConfigLine.visible = false;
                        webItemConfigLine.optionValue = "";
                    }
                    if (webConfigGlobalRule.action == 2)
                    {

                        webItemConfigLine.setValue(webConfigGlobalRule.type, webConfigGlobalRule.value);
                        webItemConfigLine.updateLineAmount(webItemConfigHeader);

                        if (webItemConfigLine.method == 0)
                        {
                            ((DropDownList)webItemConfigLine.control).Text = webItemConfigLine.type + ";" + webItemConfigLine.optionValue;
                        }
                        if (webItemConfigLine.method == 1)
                        {
                            ((TextBox)webItemConfigLine.control).Text = webItemConfigLine.type + ";" + webItemConfigLine.optionValue;
                        }

                    }
                 
                }


                i++;
            }



        }




    }
}
