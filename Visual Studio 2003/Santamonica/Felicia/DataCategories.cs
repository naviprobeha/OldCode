using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataCategories
	{
		private SmartDatabase smartDatabase;

		public DataCategories(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet()
		{

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT code, description FROM category");
			adapter.Fill(dataSet, "category");
			adapter.Dispose();

			return dataSet;


		}
	}
}
