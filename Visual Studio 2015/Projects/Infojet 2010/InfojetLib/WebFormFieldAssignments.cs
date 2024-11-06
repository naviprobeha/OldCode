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

        public DataSet getWebFormFieldAssignments(string webFormCode, string webFormFieldCode)
        {
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Form Code], [Web Form Field Code], [Assign Web Form Field Code], [Assign Type], [Assign Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Assignment") + "] WHERE [Web Form Code] = '" + webFormCode + "' AND REPLACE([Web Form Field Code], ' ', '_') = '" + webFormFieldCode + "'");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public string getAssignmentFields(string webFormCode, string webFormFieldCode)
        {
            string fields = "";

            DataSet dataSet = getWebFormFieldAssignments(webFormCode, webFormFieldCode);
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
