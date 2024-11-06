using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemCampainMembers.
	/// </summary>
	public class WebItemCampainMembers
	{
		private Infojet infojetContext;

        public WebItemCampainMembers(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

		public DataSet getCampainMembers(string webSiteCode, string webItemCampainCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Type], [No_] as Code FROM [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Campain Code] = @webItemCampainCode ORDER BY [Sort Order], [No_]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemCampainCode", webItemCampainCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                if (type == 1)
                {
                    WebModel webModel = new WebModel(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                    dataSet.Tables[0].Rows[i][0] = webModel.getDefaultItemNo();
                }

                i++;
            }

			return(dataSet);

		}


	}
}
