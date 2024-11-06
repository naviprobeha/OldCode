using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataItems
	{
		private SmartDatabase smartDatabase;

		public DataItems(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet(int searchWhat, string searchString)
		{
			string searchQuery = "";

			if (searchWhat == 0)
			{
				searchQuery = "AND no LIKE '"+searchString+"%'";
			}
			if (searchWhat == 1)
			{
				searchQuery = "AND description LIKE '"+searchString+"%'";
			}

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter1 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 1 "+searchQuery);
			adapter1.Fill(dataSet, "item");
			adapter1.Dispose();

			SqlCeDataAdapter adapter2 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 2 "+searchQuery);
			adapter2.Fill(dataSet, "item");
			adapter2.Dispose();

			SqlCeDataAdapter adapter3 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 3 "+searchQuery);
			adapter3.Fill(dataSet, "item");
			adapter3.Dispose();

			SqlCeDataAdapter adapter4 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 4 "+searchQuery);
			adapter4.Fill(dataSet, "item");
			adapter4.Dispose();

			return dataSet;


		}

		public DataSet getPutToDeathDataSet()
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE putToDeath = 1");
			adapter.Fill(dataSet, "item");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getAvailableDataSet(int searchWhat, string searchString)
		{
			string searchQuery = "";

			if (searchWhat == 0)
			{
				searchQuery = "AND no LIKE '"+searchString+"%'";
			}
			if (searchWhat == 1)
			{
				searchQuery = "AND description LIKE '"+searchString+"%'";
			}

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter1 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 1 "+searchQuery+" AND availableInMobile = 1");
			adapter1.Fill(dataSet, "item");
			adapter1.Dispose();

			SqlCeDataAdapter adapter2 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 2 "+searchQuery+" AND availableInMobile = 1");
			adapter2.Fill(dataSet, "item");
			adapter2.Dispose();

			SqlCeDataAdapter adapter3 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 3 "+searchQuery+" AND availableInMobile = 1");
			adapter3.Fill(dataSet, "item");
			adapter3.Dispose();

			SqlCeDataAdapter adapter4 = smartDatabase.dataAdapterQuery("SELECT no, description, unitPrice FROM item WHERE LEN(no) = 4 "+searchQuery+" AND availableInMobile = 1");
			adapter4.Fill(dataSet, "item");
			adapter4.Dispose();

			return dataSet;


		}
	}
}
