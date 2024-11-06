using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomers.
	/// </summary>
	public class DataCustomers
	{
		private SmartDatabase smartDatabase;

		public DataCustomers(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		/*
		public DataSet getDataSet(int searchWhat, string searchString)
		{
			string searchQuery = "";

			if (searchWhat == 0)
			{
				searchQuery = "AND ((c.name LIKE '%"+searchString+"%') OR (s.name LIKE '%"+searchString+"%'))";
			}
			if (searchWhat == 1)
			{
				searchQuery = "AND ((c.city LIKE '%"+searchString+"%') OR (s.city LIKE '%"+searchString+"%'))";
			}
			if (searchWhat == 2)
			{
				searchQuery = "AND registrationNo LIKE '%"+searchString+"%'";
			}
			if (searchWhat == 3)
			{
				searchQuery = "AND personNo LIKE '"+searchString+"%'";
			}
			if (searchWhat == 4)
			{
				searchQuery = "AND ((c.productionSite LIKE '%"+searchString+"%') OR (s.productionSite LIKE '%"+searchString+"%'))";
			}
			if (searchWhat == 5)
			{
				searchQuery = "AND ((c.phoneNo LIKE '%"+searchString+"%') OR (c.cellPhoneNo LIKE '%"+searchString+"%') OR (s.phoneNo LIKE '%"+searchString+"%'))";
			}
			if (searchWhat == 6)
			{
				searchQuery = "AND c.no LIKE '"+searchString+"%'";
			}
			if (searchWhat == 7)
			{
				searchQuery = "AND ((c.address LIKE '"+searchString+"%') OR (s.address LIKE '%"+searchString+"%'))";
			}

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM customer c LEFT JOIN customerShipAddress s ON c.no = s.customerNo WHERE blocked = 0 AND hide = 0 "+searchQuery);
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;
		}

		*/
		
		public DataSet getDataSet(int searchWhat, string searchString)
		{
			string searchQuery = "";

			if (searchWhat == 0)
			{
				searchQuery = "AND (c.name LIKE '%"+searchString+"%')";
			}
			if (searchWhat == 1)
			{
				searchQuery = "AND (c.city LIKE '%"+searchString+"%')";
			}
			if (searchWhat == 2)
			{
				searchQuery = "AND registrationNo LIKE '%"+searchString+"%'";
			}
			if (searchWhat == 3)
			{
				searchQuery = "AND personNo LIKE '"+searchString+"%'";
			}
			if (searchWhat == 4)
			{
				searchQuery = "AND (c.productionSite LIKE '%"+searchString+"%')";
			}
			if (searchWhat == 5)
			{
				searchQuery = "AND ((c.phoneNo LIKE '%"+searchString+"%') OR (c.cellPhoneNo LIKE '%"+searchString+"%'))";
			}
			if (searchWhat == 6)
			{
				searchQuery = "AND c.no LIKE '"+searchString+"%'";
			}
			if (searchWhat == 7)
			{
				searchQuery = "AND (c.address LIKE '"+searchString+"%')";
			}

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM customer c WHERE blocked = 0 AND hide = 0 "+searchQuery);
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;
		}


	}
}
