using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for CustomerShipAddresses.
	/// </summary>
	public class CustomerShipAddresses
	{
		public CustomerShipAddresses()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public CustomerShipAddress getEntry(Database database, string organizationNo, string customerNo, string entryNo)
		{
			CustomerShipAddress customerShipAddress = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Customer No], [Entry No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Position X], [Position Y], [Direction Comment], [Direction Comment 2], [Phone No], [Production Site] FROM [Customer Ship Address] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				customerShipAddress = new CustomerShipAddress(dataReader);
			}
			
			dataReader.Close();
			return customerShipAddress;
		}

		public DataSet getDataSet(Database database, string organizationNo, string customerNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Customer No], [Entry No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Position X], [Position Y], [Direction Comment], [Direction Comment 2], [Phone No], [Production Site] FROM [Customer Ship Address] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' ORDER BY Name");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customerShipAddress");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, int entryNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Customer No], [Entry No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Position X], [Position Y], [Direction Comment], [Direction Comment 2], [Phone No], [Production Site] FROM [Customer Ship Address] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customerShipAddress");
			adapter.Dispose();

			return dataSet;

		}

	}
}
