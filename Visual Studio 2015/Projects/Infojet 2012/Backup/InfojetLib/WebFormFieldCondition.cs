using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebFormFieldCondition
    {
        private Infojet infojetContext;

        public string webSiteCode;
        public string webFormCode;
        public string webFormFieldCode;
        public int sourceType;
        public string sourceCode;
        public int conditionOperator;
        public string conditionValue;
        public int function;

        public WebFormFieldCondition()
        {
        }

        public WebFormFieldCondition(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
            this.webFormFieldCode = dataRow.ItemArray.GetValue(1).ToString();
            this.webSiteCode = dataRow.ItemArray.GetValue(2).ToString();
            this.function = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.sourceType = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.sourceCode = dataRow.ItemArray.GetValue(5).ToString();
            this.conditionOperator = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this.conditionValue = dataRow.ItemArray.GetValue(7).ToString();
        }


        public bool evaluate(Infojet infojetContext)
        {
            if (sourceType == 0)
            {
                //Field
                return false;
            }
            if (sourceType == 1)
            {
                float cartAmountCondition = float.Parse(conditionValue.Replace(" ", "").Replace(",", "").Replace(".", ""));
                if (infojetContext.currencyCode != "") cartAmountCondition = CurrencyExchangeRates.convertCurrency(infojetContext.systemDatabase, infojetContext.currencyCode, cartAmountCondition);
                
                if ((conditionOperator == 0) && (infojetContext.cartHandler.getTotalCartAmount() < cartAmountCondition)) return true;
                if ((conditionOperator == 1) && (infojetContext.cartHandler.getTotalCartAmount() == cartAmountCondition)) return true;
                if ((conditionOperator == 2) && (infojetContext.cartHandler.getTotalCartAmount() > cartAmountCondition)) return true;
                if ((conditionOperator == 3) && (infojetContext.cartHandler.getTotalCartAmount() != cartAmountCondition)) return true;
                return false;
            }
            return false;
        }
    }
}
