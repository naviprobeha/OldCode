using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class ItemUnitOfMeasures
	{

		public ItemUnitOfMeasures()
		{

		}

		public ItemUnitOfMeasure getEntry(Database database, string itemNo, string unitOfMeasureCode)
		{
			ItemUnitOfMeasure itemUnitOfMeasure = null;
			
			SqlDataReader dataReader = database.query("SELECT [Item No], [Unit Of Measure Code], [Quantity] FROM [Item Unit Of Measure] WHERE [Item No] = '"+itemNo+"' AND [Unit Of Measure Code] = '"+unitOfMeasureCode+"'");
			if (dataReader.Read())
			{
				itemUnitOfMeasure = new ItemUnitOfMeasure(dataReader);
			}
			
			dataReader.Close();
			
			return itemUnitOfMeasure;
		}

		public DataSet getDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Item No], [Unit Of Measure Code], [Quantity] FROM [Item Unit Of Measure] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemUnitOfMeasure");
			adapter.Dispose();

			return dataSet;

		}

	}
}
