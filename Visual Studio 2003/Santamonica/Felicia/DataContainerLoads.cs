using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataContainerEntries.
	/// </summary>
	public class DataContainerLoads
	{
		private SmartDatabase smartDatabase;

		public DataContainerLoads(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT containerNo, ' ' as statusText FROM containerLoad");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerLoad");
			adapter.Dispose();

			return dataSet;
		
		}

		public void loadContainer(string containerNo)
		{
			DataContainerLoad dataContainerLoad = new DataContainerLoad(smartDatabase, containerNo);
			dataContainerLoad.commit();


		}

		public void unloadContainer(string containerNo)
		{
			DataContainerLoad dataContainerLoad = new DataContainerLoad(smartDatabase, containerNo);
			dataContainerLoad.delete();

		}

		public int countContainers()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM containerLoad");
			if (dataReader.Read())
			{
				return int.Parse(dataReader.GetValue(0).ToString());
			}

			return 0;

		}
	}
}
