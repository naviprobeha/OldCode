using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataOrganizationLocations
	{
		private SmartDatabase smartDatabase;

		public DataOrganizationLocations(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet()
		{

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT shippingCustomerNo, name FROM organizationLocation");
			adapter.Fill(dataSet, "organizationLocation");
			adapter.Dispose();

			return dataSet;


		}

		public int count()
		{

			int count = 0;

			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM organizationLocation");

			if (dataReader.Read())
			{
				count = dataReader.GetInt32(0);				
			}

			dataReader.Close();

			return count;
		}


	}
}
