using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataLineOrderContainers
	{
		private SmartDatabase smartDatabase;

		public DataLineOrderContainers(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet(int lineOrderEntryNo)
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, lineOrderEntryNo, containerNo, categoryCode, weight, containerTypeCode, cat.description FROM lineOrderContainer lc LEFT JOIN category cat ON cat.code = lc.categoryCode LEFT JOIN container c ON lc.containerNo = c.no WHERE lineOrderEntryNo = '"+lineOrderEntryNo+"' ORDER BY containerNo");
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;

		}

		public ArrayList getContainerList(int lineOrderEntryNo)
		{
			ArrayList containerList = new ArrayList();

			SqlCeDataReader dataReader = smartDatabase.query("SELECT containerNo FROM lineOrderContainer lc, container c WHERE lineOrderEntryNo = '"+lineOrderEntryNo+"' AND lc.containerNo = c.no ORDER BY containerNo");
			
			while(dataReader.Read())
			{
				containerList.Add(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			return containerList;
		}
	}
}
