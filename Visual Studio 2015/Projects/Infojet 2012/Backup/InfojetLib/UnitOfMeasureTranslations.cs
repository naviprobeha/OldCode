using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for ShippingAgents.
    /// </summary>
    public class UnitOfMeasureTranslations
    {
        public UnitOfMeasureTranslations()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Hashtable getTranslations(Infojet infojetContext, string languageCode)
        {

            Hashtable unitOfMeasureTable = (Hashtable)System.Web.HttpContext.Current.Session["unitOfMeasureTable_"+languageCode];

            if (unitOfMeasureTable == null)
            {
                unitOfMeasureTable = new Hashtable();

                SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT u.[Code], t.[Description], u.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Unit of Measure") + "] u LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Unit of Measure Translation") + "] t ON t.[Code] = u.[Code] AND t.[Language Code] = '" + infojetContext.languageCode + "'");
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                    if (description == "") description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();

                    string code = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    unitOfMeasureTable.Add(code, description);

                    i++;
                }

                System.Web.HttpContext.Current.Session.Add("unitOfMeasureTable_"+languageCode, unitOfMeasureTable);
            }

            return unitOfMeasureTable;
        }
    }
}
