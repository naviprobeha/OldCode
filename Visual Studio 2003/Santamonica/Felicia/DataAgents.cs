using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataAgents
	{
		private SmartDatabase smartDatabase;

		public DataAgents(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet()
		{

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT code, description, organizationNo FROM agent");
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;


		}
	}
}
