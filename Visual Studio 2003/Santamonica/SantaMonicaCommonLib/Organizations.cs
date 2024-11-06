using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class Organizations
	{
		public Organizations()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Organization getOrganization(Database database, string no)
		{
			return getOrganization(database, no, false);
		}

		public Organization getOrganization(Database database, string no, bool navisionVendorNo)
		{
			Organization organization = null;

			SqlDataReader dataReader = null;

			if (navisionVendorNo)
			{
				dataReader = database.query("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] WHERE [Navision Vendor No] = '"+no+"'");
			}
			else
			{
				dataReader = database.query("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] WHERE [No] = '"+no+"'");
			}

			if (dataReader.Read())
			{
				organization = new Organization(database, dataReader);
			}

			dataReader.Close();
			return organization;

		}

		public DataSet getDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organization");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string syncGroupCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] WHERE [Sync Group Code] = '"+syncGroupCode+"' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organization");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getCallCenterMemberDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] WHERE [Call Center Member] = '1' ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organization");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Stop Fee], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals] FROM [Organization] WHERE [No] = '"+no+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "organization");
			adapter.Dispose();

			return dataSet;

		}
	}
}
