using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class SalesPrice
	{
		private Database database;

		public string itemNo;
		public string salesCode;
		public string currencyCode;
		public DateTime startingDate;
		public float unitPrice;
		public bool priceIncludesVat;
		public int salesType;
		public float minimumQuantity;
		public DateTime endingDate;
		public string unitOfMeasureCode;
		public string variantCode;

		public SalesPrice(Database database, string itemNo, int salesType, string salesCode, DateTime startingDate, string currencyCode, string variantCode, string unitOfMeasureCode, float minimumQuantity)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.itemNo = itemNo;
			this.salesType = salesType;
			this.salesCode = salesCode;
			this.startingDate = startingDate;
			this.currencyCode = currencyCode;
			this.variantCode = variantCode;
			this.unitOfMeasureCode = unitOfMeasureCode;
			this.minimumQuantity = minimumQuantity;

			getFromDatabase();
		}

		public SalesPrice(Database database, SqlDataReader dataReader)
		{
			this.database = database;
			
			readData(dataReader);
		}

		private void readData(SqlDataReader dataReader)
		{

			if (dataReader != null)
			{
				this.itemNo = dataReader.GetValue(0).ToString();
				this.salesCode = dataReader.GetValue(1).ToString();
				this.currencyCode = dataReader.GetValue(2).ToString();
				this.startingDate = DateTime.Parse(dataReader.GetValue(3).ToString());
				this.unitPrice = float.Parse(dataReader.GetValue(4).ToString());

				this.priceIncludesVat = false;
				if (dataReader.GetValue(1).ToString() == "5") this.priceIncludesVat = true;

				this.salesType = int.Parse(dataReader.GetValue(6).ToString());
				this.minimumQuantity = float.Parse(dataReader.GetValue(7).ToString());
				this.endingDate = DateTime.Parse(dataReader.GetValue(8).ToString());
				this.unitOfMeasureCode = dataReader.GetValue(9).ToString();
				this.variantCode = dataReader.GetValue(10).ToString();
			}
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM ["+database.getTableName("Sales Price")+"] WHERE [Item No_] = '"+this.itemNo+"' AND [Sales Type] = '"+this.salesType+"' AND [Sales Code] = '"+this.salesCode+"' AND [Starting Date] = '"+this.startingDate.ToString("yyyy-MM-dd")+"' AND [Currency Code] = '"+this.currencyCode+"' AND [Variant Code] = '"+this.variantCode+"' AND [Unit Of Measure Code] = '"+this.unitOfMeasureCode+"' AND [Minimum Quantity] = '"+this.minimumQuantity+"'");
			if (dataReader.Read())
			{
				readData(dataReader);
			}

			dataReader.Close();


		}


		public string formatPrice(string currencyCode)
		{

			return database.formatCurrency(this.unitPrice, currencyCode);
		}
	}
}
