using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class SalesLineDiscount
	{
		private Database database;

		public string code;
		public string salesCode;
		public string currencyCode;
		public DateTime startingDate;
		public float lineDiscount;
		public int salesType;
		public float minimumQuantity;
		public DateTime endingDate;
		public int type;
		public string unitOfMeasureCode;
		public string variantCode;

		public SalesLineDiscount(Database database, int type, string code, int salesType, string salesCode, DateTime startingDate, string currencyCode, string variantCode, string unitOfMeasureCode, float minimumQuantity)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.type = type;
			this.code = code;
			this.salesType = salesType;
			this.salesCode = salesCode;
			this.startingDate = startingDate;
			this.currencyCode = currencyCode;
			this.variantCode = variantCode;
			this.unitOfMeasureCode = unitOfMeasureCode;
			this.minimumQuantity = minimumQuantity;

			getFromDatabase();
		}

		public SalesLineDiscount(Database database, SqlDataReader dataReader)
		{
			this.database = database;
			
			readData(dataReader);
		}

		private void readData(SqlDataReader dataReader)
		{

			if (dataReader != null)
			{
				this.code = dataReader.GetValue(0).ToString();
				this.salesCode = dataReader.GetValue(1).ToString();
				this.currencyCode = dataReader.GetValue(2).ToString();
				this.startingDate = DateTime.Parse(dataReader.GetValue(3).ToString());
				this.lineDiscount = float.Parse(dataReader.GetValue(4).ToString());

				this.salesType = int.Parse(dataReader.GetValue(5).ToString());
				this.minimumQuantity = float.Parse(dataReader.GetValue(6).ToString());
				this.endingDate = DateTime.Parse(dataReader.GetValue(7).ToString());
				this.type = int.Parse(dataReader.GetValue(8).ToString());
				this.unitOfMeasureCode = dataReader.GetValue(9).ToString();
				this.variantCode = dataReader.GetValue(10).ToString();
			}
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Sales Code], [Currency Code], [Starting Date], [Line Discount %], [Sales Type], [Minimum Quantity], [Ending Date], [Type], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Line Discount") + "] WHERE [Type] = @type AND [Code] = @code AND [Sales Type] = @salesType AND [Sales Code] = @salesCode AND [Starting Date] = @startingDate AND [Currency Code] = @currencyCode AND [Variant Code] = @variantCode AND [Unit Of Measure Code] = @unitOfMeasureCode AND [Minimum Quantity] = @minimumQuantity");
            databaseQuery.addSmallIntParameter("type", type);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addSmallIntParameter("salesType", salesType);
            databaseQuery.addStringParameter("salesCode", salesCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", startingDate);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);
            databaseQuery.addStringParameter("unitOfMeasureCode", unitOfMeasureCode, 20);
            databaseQuery.addDecimalParameter("minimumQuantity", minimumQuantity);

            SqlDataReader dataReader = databaseQuery.executeQuery();

			if (dataReader.Read())
			{
				readData(dataReader);
			}

			dataReader.Close();


		}


	}
}
