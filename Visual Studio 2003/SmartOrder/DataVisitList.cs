using System;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataVisitList.
	/// </summary>
	public class DataVisitList
	{
		private SmartDatabase smartDatabase;

		public DataVisitList(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}


		public void add(DataCustomer dataCustomer)
		{
			try
			{
				smartDatabase.nonQuery("INSERT INTO activeCustomer (customerNo) VALUES ('"+dataCustomer.no+"')");

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

		public void remove(DataCustomer dataCustomer)
		{
			try
			{
				smartDatabase.nonQuery("DELETE FROM activeCustomer WHERE customerNo = '"+dataCustomer.no+"'");
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

		public bool check(DataCustomer dataCustomer)
		{
			
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM activeCustomer WHERE customerNo = '"+dataCustomer.no+"'");

				if (dataReader.Read())
				{
					dataReader.Dispose();
					return true;
				}
				dataReader.Dispose();
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

			return false;

		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter customerAdapter = smartDatabase.dataAdapterQuery("SELECT a.customerNo, c.name FROM activeCustomer a, customer c WHERE a.customerNo = c.no");
			
			DataSet customerDataSet = new DataSet();
			customerAdapter.Fill(customerDataSet, "activeCustomer");
			customerAdapter.Dispose();

			return customerDataSet;
		}

	}
}
