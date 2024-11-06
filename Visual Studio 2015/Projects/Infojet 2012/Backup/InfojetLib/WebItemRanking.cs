using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebItemRanking
    {
        public WebItemRanking()
        {

        }

        public DataSet getTopTenDataSet(Infojet infojetContext, string webSiteCode)
        {

            // Used to fetch items and convert them to the class Item.
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_], i.[Description], i.[Description 2], i.[Unit Price], i.[Sales Unit of Measure], i.[Manufacturer Code], i.[Lead Time Calculation], i.[Unit List Price], i.[Item Disc_ Group], '' as infoPage, (r.[Type]+1) as Type, r.[No_], 0 as sortOrder, t.[Description], t.[Description 2], mt.[Description], mt.[Description 2] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Ranking") + "] r LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON r.[No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = r.[No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] mt ON mt.[Web Model No_] = r.[No_] AND mt.[Language Code] = @languageCode WHERE r.[Web Site Code] = @webSiteCode ORDER BY [Ranking]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());
                if (type == 2)
                {
                    WebModel webModel = new WebModel(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
                    dataSet.Tables[0].Rows[i][0] = webModel.getDefaultItemNo();
                    dataSet.Tables[0].Rows[i][1] = webModel.description;
                    dataSet.Tables[0].Rows[i][2] = webModel.description2;

                }

                i++;
            }


            return (dataSet);

        }

    }
}
