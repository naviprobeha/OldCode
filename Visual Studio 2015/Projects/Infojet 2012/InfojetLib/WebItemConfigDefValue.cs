using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebItemConfigDefValue
    {
        private string _webConfigModelNo;
        private string _optionCode;
        private int _type;
        private string _value;
        private string _description;
        private int _sortOrder;
        private string _webConfigRuleCode;
        private bool _hide;

        private Infojet infojetContext;

        public WebItemConfigDefValue(Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;

            _webConfigModelNo = dataReader.GetValue(0).ToString();
            _optionCode = dataReader.GetValue(1).ToString();
            _type = int.Parse(dataReader.GetValue(2).ToString());
            _value = dataReader.GetValue(3).ToString();
            _description = dataReader.GetValue(4).ToString();
            _sortOrder = dataReader.GetInt32(5);
            _webConfigRuleCode = dataReader.GetValue(6).ToString();

        }

        public WebItemConfigDefValue(Infojet infojetContext, WebItemConfigDefValue webItemConfigDefValue)
        {
            this.infojetContext = infojetContext;

            _webConfigModelNo = webItemConfigDefValue.webConfigModelNo;
            _optionCode = webItemConfigDefValue.optionCode;
            _type = webItemConfigDefValue.type;
            _value = webItemConfigDefValue.value;
            _description = webItemConfigDefValue.description;
            _sortOrder = webItemConfigDefValue.sortOrder;
            _webConfigRuleCode = webItemConfigDefValue.webConfigRuleCode;

        }

        public string webConfigModelNo { get { return _webConfigModelNo; } }
        public string optionCode { get { return _optionCode; } }
        public int type { get { return _type; } }
        public string value { get { return _value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int sortOrder { get { return _sortOrder; } }
        public string webConfigRuleCode { get { return _webConfigRuleCode; } }
        public bool hide { get { return _hide; } set { _hide = value; } }

        

        public static WebItemConfigDefValueCollection getConfigDefValues(Infojet infojetContext, string webConfigModelNo)
        {
            WebItemConfigDefValueCollection webItemConfigDefValueCollection = new WebItemConfigDefValueCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT v.[Web Config Model No_], v.[Option Code], v.[Type], v.[Value], v.[Description], v.[Sort Order], v.[Web Config Rule Code], t.[Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Value") + "] v LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Trans") + "] t ON t.[Translation Type] = 1 AND v.[Option Code] = t.[Option Code] AND v.[Type] = t.[Type] AND v.[Value] = t.[Value] AND t.[Language Code] = @languageCode WHERE v.[Web Config Model No_] = @webConfigModelNo ORDER BY v.[Option Code], v.[Sort Order]");
            databaseQuery.addStringParameter("webConfigModelNo", webConfigModelNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                WebItemConfigDefValue webItemConfigDefValue = new WebItemConfigDefValue(infojetContext, dataReader);
                if (!dataReader.IsDBNull(7)) webItemConfigDefValue.description = dataReader.GetValue(7).ToString();

                webItemConfigDefValueCollection.Add(webItemConfigDefValue);
            }
            dataReader.Close();

            return webItemConfigDefValueCollection;
        }

        public static WebItemConfigDefValue getConfigDefValue(Infojet infojetContext, string webConfigModelNo, string optionCode, int type, string value)
        {
            WebItemConfigDefValue webItemConfigDefValue = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Config Model No_], [Option Code], [Type], [Value], [Description], [Sort Order], [Web Config Rule Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Value") + "] WHERE [Web Config Model No_] = @webConfigModelNo AND [Option Code] = @optionCode AND [Type] = @type AND [Value] = @value");
            databaseQuery.addStringParameter("webConfigModelNo", webConfigModelNo, 20);
            databaseQuery.addStringParameter("optionCode", optionCode, 20);
            databaseQuery.addIntParameter("type", type);
            databaseQuery.addStringParameter("value", value, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webItemConfigDefValue = new WebItemConfigDefValue(infojetContext, dataReader);
            }
            dataReader.Close();

            return webItemConfigDefValue;
        }

        public static float calcConfigDefValuePrice(Infojet infojetContext, WebItemConfigHeader webItemConfigHeader, string optionCode, int type, string value)
        {
            string customerPriceGroup = "";
            if (infojetContext.userSession != null) customerPriceGroup = infojetContext.userSession.customer.customerPriceGroup;

            string customerNo = "";
            if (infojetContext.userSession != null) customerNo = infojetContext.userSession.customer.no;


            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Config Rule Code], [Unit Price], [Additional] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Price") + "] WHERE [Web Config Model No_] = @webConfigModelNo AND (([Customer Type] = 0) OR ([Customer Type] = 1 AND [Customer Code] = @customerPriceGroup) OR ([Customer Type] = 2 AND [Customer Code] = @customerNo)) AND [Option Code] = @optionCode AND [Option Value Type] = @optionValueType AND [Option Value Code] = @optionValueCode AND ([Starting Date] = '1753-01-01' OR [Starting Date] < GETDATE()) AND ([Ending Date] = '1753-01-01' OR [Ending Date] > GETDATE()) AND [Additional] = 0 ORDER BY [Unit Price]");
            databaseQuery.addStringParameter("webConfigModelNo", webItemConfigHeader.webConfigModelNo, 20);
            databaseQuery.addStringParameter("customerPriceGroup", customerPriceGroup, 20);
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("optionCode", optionCode, 20);
            databaseQuery.addIntParameter("optionValueType", type);
            databaseQuery.addStringParameter("optionValueCode", value, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);
               

            float amount = 0;
            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                string ruleCode = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                float unitPrice = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());

                if (ruleCode != "")
                {
                    if (WebConfigRule.evaluateRule(infojetContext, webItemConfigHeader, ruleCode))
                    {
                        if ((unitPrice < amount) || (amount == 0)) amount = unitPrice;
                    }
                }
                else
                {
                    if ((unitPrice < amount) || (amount == 0)) amount = unitPrice;
                }

                i++;
            }
            

            //Additional fees
            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Config Rule Code], [Unit Price], [Additional] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Price") + "] WHERE [Web Config Model No_] = @webConfigModelNo AND (([Customer Type] = 0) OR ([Customer Type] = 1 AND [Customer Code] = @customerPriceGroup) OR ([Customer Type] = 2 AND [Customer Code] = @customerNo)) AND [Option Code] = @optionCode AND [Option Value Type] = @optionValueType AND [Option Value Code] = @optionValueCode AND ([Starting Date] = '1753-01-01' OR [Starting Date] < GETDATE()) AND ([Ending Date] = '1753-01-01' OR [Ending Date] > GETDATE()) AND [Additional] = 1 ORDER BY [Unit Price]");
            databaseQuery.addStringParameter("webConfigModelNo", webItemConfigHeader.webConfigModelNo, 20);
            databaseQuery.addStringParameter("customerPriceGroup", customerPriceGroup, 20);
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("optionCode", optionCode, 20);
            databaseQuery.addIntParameter("optionValueType", type);
            databaseQuery.addStringParameter("optionValueCode", value, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);


            i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                string ruleCode = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                float unitPrice = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());

                if (ruleCode != "")
                {
                    if (WebConfigRule.evaluateRule(infojetContext, webItemConfigHeader, ruleCode))
                    {
                        amount = amount + unitPrice;
                    }
                }
                else
                {
                    amount = amount + unitPrice;
                }

                i++;
            }

            return amount;

        }

        public static float calcConfigDefValueDiscount(Infojet infojetContext, WebItemConfigHeader webItemConfigHeader, string optionCode, int type, string value)
        {
            string customerPriceGroup = "";
            if (infojetContext.userSession != null) customerPriceGroup = infojetContext.userSession.customer.customerPriceGroup;

            string customerNo = "";
            if (infojetContext.userSession != null) customerNo = infojetContext.userSession.customer.no;


            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Config Rule Code], "+infojetContext.systemDatabase.convertField("[Discount %]")+" FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Discount") + "] WHERE ([Web Config Model No_] = @webConfigModelNo OR [Web Config Model No_] = '') AND (([Customer Type] = 0) OR ([Customer Type] = 1 AND [Customer Code] = @customerPriceGroup) OR ([Customer Type] = 2 AND [Customer Code] = @customerNo)) AND ([Option Code] = @optionCode OR [Option Code] = '') AND ([Option Value Type] = @optionValueType OR [Option Value Type] = 0) AND ([Option Value Code] = @optionValueCode OR [Option Value Code] = '') AND ([Starting Date] = '1753-01-01' OR [Starting Date] < GETDATE()) AND ([Ending Date] = '1753-01-01' OR [Ending Date] > GETDATE()) ORDER BY "+infojetContext.systemDatabase.convertField("[Discount %]"));
            databaseQuery.addStringParameter("webConfigModelNo", webItemConfigHeader.webConfigModelNo, 20);
            databaseQuery.addStringParameter("customerPriceGroup", customerPriceGroup, 20);
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("optionCode", optionCode, 20);
            databaseQuery.addIntParameter("optionValueType", type);
            databaseQuery.addStringParameter("optionValueCode", value, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();


            while (dataReader.Read())
            {
                string ruleCode = dataReader.GetValue(0).ToString();
                float discount = float.Parse(dataReader.GetValue(1).ToString());

                if (ruleCode != "")
                {
                    if (WebConfigRule.evaluateRule(infojetContext, webItemConfigHeader, ruleCode))
                    {
                        dataReader.Close();
                        return discount;
                    }
                }
                else
                {
                    dataReader.Close();
                    return discount;
                }

            }
            dataReader.Close();

            return 0;

        }
    }
}
