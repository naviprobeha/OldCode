using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataMobileUsers
	{
		private SmartDatabase smartDatabase;

		public DataMobileUsers(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet()
		{

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, name, password FROM mobileUser");
			adapter.Fill(dataSet, "mobileUser");
			adapter.Dispose();

			return dataSet;


		}
	}
}
