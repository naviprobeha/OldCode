using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebLayoutRows.
    /// </summary>
    public class WebFormFieldAssignments
    {
        private Infojet infojetContext;


        public WebFormFieldAssignments(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //

            this.infojetContext = infojetContext;

        }

        public DataSet getWebFormFieldAssignments(string webSiteCode, string webFormCode, string webFormFieldCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Web Form Field Code], [Assign Web Form Field Code], [Assign Type], [Assign Value], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Assignment") + "] WHERE [Web Form Code] = @webFormCode AND REPLACE([Web Form Field Code], ' ', '_') = @webFormFieldCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public string getAssignmentFields(string webSiteCode, string webFormCode, string webFormFieldCode)
        {
            string fields = "";

            DataSet dataSet = getWebFormFieldAssignments(webSiteCode, webFormCode, webFormFieldCode);
            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebFormFieldAssignment webFormFieldAssignment = new WebFormFieldAssignment(infojetContext, dataSet.Tables[0].Rows[i]);

                if (webFormFieldAssignment.assignType == 0)
                {
                    if (fields != "") fields = fields + ", ";
                    fields = fields + "[" + webFormFieldAssignment.assignValue + "]";
                }

                i++;
            }

            return fields;
        }

    }
}
