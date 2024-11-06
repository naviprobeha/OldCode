using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebUserAccountRelation
	{
		private Database database;

		public string no;
		public string webSiteCode;

		public WebUserAccountRelation(Database database, SqlDataReader dataReader)
		{
			this.database = database;

			readData(dataReader);
		}

		public WebUserAccountRelation(Database database, DataRow dataRow)
		{
			this.database = database;

			this.no = dataRow.ItemArray.GetValue(0).ToString();
			this.webSiteCode = dataRow.ItemArray.GetValue(1).ToString();
		}

		private void readData(SqlDataReader dataReader)
		{

			no = dataReader.GetValue(0).ToString();
			webSiteCode = dataReader.GetValue(1).ToString();
				

		}


	}
}
