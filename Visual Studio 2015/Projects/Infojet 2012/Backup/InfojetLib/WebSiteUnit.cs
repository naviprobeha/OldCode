using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PostCode.
	/// </summary>
	public class WebSiteUnit
	{
		public string webSiteCode;
		public string unitOfMeasureCode;
		public bool allowDecimalQuantity;
		private Database database;

		public WebSiteUnit(Database database, string webSiteCode, string unitOfMeasureCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.webSiteCode = webSiteCode;
			this.unitOfMeasureCode = unitOfMeasureCode;

			getFromDatabase();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Unit Of Measure Code], [Allow Decimal Quantity] FROM [" + database.getTableName("Web Site Unit") + "] WHERE [Web Site Code] = @webSiteCode AND [Unit Of Measure Code] = @unitOfMeasureCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("unitOfMeasureCode", unitOfMeasureCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				unitOfMeasureCode = dataReader.GetValue(1).ToString();

				allowDecimalQuantity = false;
				if (dataReader.GetValue(2).ToString() == "1") allowDecimalQuantity = true;

			}

			dataReader.Close();


		}

	}
}
