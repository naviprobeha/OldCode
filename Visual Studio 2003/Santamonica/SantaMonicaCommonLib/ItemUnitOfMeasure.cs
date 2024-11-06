using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemUnitOfMeasure
	{
		public string itemNo;
		public string unitOfMeasureCode;
		public decimal quantity;

		public string updateMethod;

		public ItemUnitOfMeasure(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.itemNo = dataReader.GetValue(0).ToString();
			this.unitOfMeasureCode = dataReader.GetValue(1).ToString();
			this.quantity = dataReader.GetDecimal(2);

		}


		public void save(Database database)
		{

			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Item Unit Of Measure] WHERE [Item No] = '"+itemNo+"' AND [Unit Of Measure Code] = '"+this.unitOfMeasureCode+"'");
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Item No] FROM [Item Unit Of Measure] WHERE [Item No] = '"+itemNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Item Unit Of Measure] SET [Quantity] = '"+quantity.ToString().Replace(",", ".")+"' WHERE [Item No] = '"+itemNo+"' AND [Unit Of Measure Code] = '"+this.unitOfMeasureCode+"'");

				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Item Unit Of Measure] ([Item No], [Unit Of Measure Code], [Quantity]) VALUES ('"+itemNo+"','"+unitOfMeasureCode+"','"+quantity.ToString().Replace(",", ".")+"')");
				}

			}
		}

	
	}
}
