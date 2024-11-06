using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ExtendedTextHeaders.
	/// </summary>
	public class ExtendedTextHeaders
	{
		private Database database;

		public ExtendedTextHeaders(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getAllTexts(int tableName, string no, string languageCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Table Name], [No_], [Language Code], [Text No_], [Starting Date], [Ending Date], [All Language Codes] FROM [" + database.getTableName("Extended Text Header") + "] WHERE [Table Name] = @tableName AND [No_] = @no AND ([Language Code] = @languageCode OR [All Language Codes] = 1) AND [Publish On Web] = 1 AND ([Starting Date] <= GETDATE() AND ([Ending Date] >= GETDATE() OR [Ending Date] = '1753-01-01')) ORDER BY [Text No_]");
            databaseQuery.addIntParameter("tableName", tableName);
            databaseQuery.addStringParameter("no", no, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

           
		}

		public ExtendedTextHeader getItemText(string itemNo, string languageCode)
		{
			DataSet textDataSet = getAllTexts(2, itemNo, languageCode);

			if (textDataSet.Tables[0].Rows.Count > 0)
			{
				ExtendedTextHeader extTextHeader = new ExtendedTextHeader(database, textDataSet.Tables[0].Rows[0]);
				return extTextHeader;

			}

			return null;
		}
	}
}
