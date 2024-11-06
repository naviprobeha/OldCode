using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebFormFields
	{
		private Database database;


		public WebFormFields(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getWebFormFields(string webSiteCode, string webFormCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Form Code], [Code], [Field Name], [Field Type], [Field Length], [Sort Order], [Placement], [Field Size], [Required], [Connection Type], [Value Table No_], [Value Table Name], [Value Code Field Name], [Value Description Field Name], [Value Description 2 Field Name], [Read Only], [Value Description 3 Field Name], [Web Site Code], [Expression], [Transfer To Order Text Line] FROM [" + database.getTableName("Web Form Field") + "] WHERE [Web Form Code] = @webFormCode AND [Web Site Code] = @webSiteCode ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

        public WebFormField[] getWebFormFieldArray(Infojet infojetContext, string webSiteCode, string webFormCode)
        {
            DataSet webFormFieldDataSet = getWebFormFields(webSiteCode, webFormCode);
            WebFormField[] webFormFieldArray = new WebFormField[webFormFieldDataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);
                
                webFormFieldArray[i] = webFormField;

                i++;
            }

            return webFormFieldArray;
        }

	}
}
