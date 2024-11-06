using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class OrganizationLocations
	{

		public OrganizationLocations()
		{

		}

		public OrganizationLocation getEntry(Database database, string organizationNo, string shippingCustomerNo)
		{
			OrganizationLocation organizationLocation = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Shipping Customer No], [Name] FROM [Organization Location] WHERE [Organization No] = '"+organizationNo+"' AND [Shipping Customer No] = '"+shippingCustomerNo+"'");
			if (dataReader.Read())
			{
				organizationLocation = new OrganizationLocation(dataReader);
			}
			
			dataReader.Close();
			
			return organizationLocation;
		}

		public DataSet getDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Shipping Customer No], [Name] FROM [Organization Location] WHERE [Organization No] = '"+organizationNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organizationLocation");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSetEntry(Database database, string organizationNo, string shippingCustomerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Shipping Customer No], [Name] FROM [Organization Location] WHERE [Organization No] = '"+organizationNo+"' AND [Shipping Customer No] = '"+shippingCustomerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organizationLocation");
			adapter.Dispose();

			return dataSet;
		}

	}
}
