using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ExtendedTextLines.
	/// </summary>
	public class ExtendedTextLines
	{
		private Database database;

		public ExtendedTextLines(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getTextLines(ExtendedTextHeader extendedTextHeader)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Table Name], [No_], [Language Code], [Text No_], [Line No_], [Text] FROM [" + database.getTableName("Extended Text Line") + "] WHERE [Table Name] = @tableName AND [No_] = @no AND [Language Code] = @languageCode AND [Text No_] = @textNo ORDER BY [Line No_]");
            databaseQuery.addIntParameter("tableName", extendedTextHeader.tableName);
            databaseQuery.addStringParameter("no", extendedTextHeader.no, 20);
            databaseQuery.addStringParameter("languageCode", extendedTextHeader.languageCode, 20);
            databaseQuery.addIntParameter("textNo", extendedTextHeader.textNo);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

	}
}
