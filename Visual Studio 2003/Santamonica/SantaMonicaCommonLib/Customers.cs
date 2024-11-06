using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Customers.
	/// </summary>
	public class Customers
	{
		private string searchCustomerNo = "";
		private string searchRegNo = "";
		private string searchName = "";
		private string searchProductionNo = "";
		private string searchPhoneNo = "";
		private string searchCity = "";
		private string searchPaymentType = "";
		private bool callCenter = false;
		private string organizationNo = "";

		public Customers(XmlElement tableElement, Organization organization, Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, organization, database);
		}

		public Customers()
		{

		}

		public Customer getEntry(Database database, string organizationNo, string no)
		{
			Customer customer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");
			if (dataReader.Read())
			{
				customer = new Customer(dataReader);
			}
			
			dataReader.Close();
			return customer;
		}

		public Customer findFirstCustomer(Database database, string no)
		{
			Customer customer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE [No] = '"+no+"' AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = '1')");
			if (dataReader.Read())
			{
				customer = new Customer(dataReader);
			}
			
			dataReader.Close();
			return customer;
		}

		public Customer findFirstCustomerByPhoneNo(Database database, string phoneNo)
		{
			Customer customer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE [Cell Phone No] = '"+phoneNo+"' OR [Phone No] = '"+phoneNo+"'");
			if (dataReader.Read())
			{
				customer = new Customer(dataReader);
			}
			
			dataReader.Close();
			return customer;
		}

		public Customer findPhoneNo(Database database, string phoneNo)
		{
			Customer customer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE (REPLACE(REPLACE([Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Cell Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Contact Name], ' ', ''), '-', '') LIKE '%"+phoneNo+"%')");
			if (dataReader.Read())
			{
				customer = new Customer(dataReader);
			}
			
			dataReader.Close();
			return customer;
		}

		public Customer findCallCenterPhoneNo(Database database, string phoneNo)
		{
			Customer customer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE (REPLACE(REPLACE([Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Cell Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Contact Name], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = 1)");
			if (dataReader.Read())
			{
				customer = new Customer(dataReader);
			}
			
			dataReader.Close();
			return customer;
		}

		public DataSet getCustomerOrganization(Database database, string customerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No] FROM [Customer] WHERE [Hide] = 0 AND [No] = '"+customerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getCallCenterCustomerOrganization(Database database, string customerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No] FROM [Customer] WHERE [Hide] = 0 AND [No] = '"+customerNo+"' AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = 1)");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string organizationNo)
		{
			string query = "SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [Position X], [Position Y], [Blocked], [Force Cash Payment], [Organization No], [Production Site] FROM [Customer] WHERE [Hide] = 0 AND [Organization No] = '"+organizationNo+"' "+this.searchCustomerNo+this.searchName+this.searchPhoneNo+this.searchProductionNo+this.searchRegNo+this.searchCity+this.searchPaymentType+" ORDER BY Name";
			if (callCenter) query = "SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [Position X], [Position Y], [Blocked], [Force Cash Payment], [Organization No], [Production Site] FROM [Customer] WHERE [Hide] = 0 "+this.organizationNo+this.searchCustomerNo+this.searchName+this.searchPhoneNo+this.searchProductionNo+this.searchRegNo+this.searchCity+this.searchPaymentType+" ORDER BY Name";

			SqlDataAdapter adapter = database.dataAdapterQuery(query);

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getListDataSet(Database database, string organizationNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [Position X], [Position Y], [Blocked] FROM [Customer] WHERE [Hide] = 0 AND [Blocked] = 0 AND [Organization No] = '"+organizationNo+"' "+this.searchCustomerNo+this.searchName+this.searchPhoneNo+this.searchProductionNo+this.searchRegNo+this.searchCity+this.searchPaymentType+" ORDER BY Name");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}



		public DataSet getMapDataSet(Database database, string organizationNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE [Organization No] = '"+organizationNo+"' AND ([Position X] > 0 OR [Position Y] > 0)");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}

		public void fromDOM(XmlElement tableElement, Organization organization, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				Customer customer = new Customer(record, organization, database, true);

				i++;
			}
		}

		public DataSet getDataSetEntry(Database database, string organizationNo, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Dairy No], [Position X], [Position Y], [Price Group Code], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Editable], [Direction Comment], [Direction Comment 2] FROM [Customer] WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getUpdatedDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Price Group Code], [Editable], [Updated], [Unverified] FROM [Customer] WHERE [Updated] = '1'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customer");
			adapter.Dispose();

			return dataSet;

		}


		public void setSearchCriteria(string customerNo, string registrationNo, string name, string city, string productionNo, string phoneNo, string paymentType)
		{
			if (customerNo != "") this.searchCustomerNo = " AND UPPER([No]) LIKE UPPER('%"+customerNo+"%')";
			if (registrationNo != "") this.searchRegNo = " AND UPPER([Registration No]) LIKE UPPER('%"+registrationNo+"%')";
			if (name != "") this.searchName = " AND UPPER([Name]) LIKE UPPER('%"+name+"%')";
			if (productionNo != "") this.searchProductionNo = " AND UPPER([Production Site]) LIKE UPPER('%"+productionNo+"%')";
			if (phoneNo != "") this.searchPhoneNo = " AND (UPPER([Phone No]) LIKE UPPER('%"+phoneNo+"%') OR UPPER([Cell Phone No]) LIKE UPPER('%"+phoneNo+"%') OR UPPER([Contact Name]) LIKE UPPER('%"+phoneNo+"%'))";
			if (city != "") this.searchCity = " AND UPPER([City]) LIKE UPPER('%"+city+"%')";
			if (paymentType != "") this.searchPaymentType = " AND [Force Cash Payment] = '1'";
		}

		public void setCallCenter(string organizationNo)
		{
			this.callCenter = true;
			if (organizationNo != "") 
			{
				this.organizationNo = " AND [Organization No] = '"+organizationNo+"'";
			}
			else
			{
				this.organizationNo = " AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = '1')";
			}
		}

		public ArrayList matchCustomersToShipOrder(Database database, ShipOrder shipOrder)
		{
			ArrayList customerList = new ArrayList();
			ArrayList customerNoList = new ArrayList();

			if (shipOrder.productionSite.Length < 6) shipOrder.productionSite = shipOrder.productionSite.PadLeft(6, '0');
			shipOrder.phoneNo = shipOrder.phoneNo.Replace(" ", "");
			shipOrder.cellPhoneNo = shipOrder.cellPhoneNo.Replace(" ", "");
			if (shipOrder.phoneNo == "") shipOrder.phoneNo = "###########";
			if (shipOrder.cellPhoneNo == "") shipOrder.cellPhoneNo = "###########";

			SqlDataReader dataReader = database.query("SELECT c.[Organization No], c.[No], c.[Name], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Country Code], c.[Phone No], c.[Cell Phone No], c.[Fax No], c.[E-mail], c.[Production Site], c.[Registration No], c.[Person No], c.[Position X], c.[Position Y], c.[Contact Name], c.[Dairy No], c.[Dairy Code], c.[Hide], c.[Bill-to Customer No], c.[Blocked], c.[Force Cash Payment], c.[Direction Comment], c.[Direction Comment 2], c.[Price Group Code], c.[Editable], c.[Updated], c.[Unverified] FROM [Customer] c, [Organization] o WHERE c.[Organization No] = o.[No] AND o.[Call Center Member] = 1 AND (c.[Production Site] = '"+shipOrder.productionSite+"' OR c.[Phone No] = '"+shipOrder.phoneNo+"' OR c.[Cell Phone No] = '"+shipOrder.cellPhoneNo+"' OR c.[Cell Phone No] = '"+shipOrder.phoneNo+"' OR c.[Phone No] = '"+shipOrder.cellPhoneNo+"')");
			while (dataReader.Read())
			{
				if (!customerNoList.Contains(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString()))
				{
					customerList.Add(new Customer(dataReader));
					customerNoList.Add(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString());
				}
			}		
			dataReader.Close();
			

			dataReader = database.query("SELECT c.[Organization No], c.[No], c.[Name], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Country Code], c.[Phone No], c.[Cell Phone No], c.[Fax No], c.[E-mail], c.[Production Site], c.[Registration No], c.[Person No], c.[Position X], c.[Position Y], c.[Contact Name], c.[Dairy No], c.[Dairy Code], c.[Hide], c.[Bill-to Customer No], c.[Blocked], c.[Force Cash Payment], c.[Direction Comment], c.[Direction Comment 2], c.[Price Group Code], c.[Editable], c.[Updated], c.[Unverified] FROM [Customer] c, [Organization] o WHERE c.[Organization No] = o.[No] AND o.[Call Center Member] = 1 AND UPPER(c.[Address]) = '"+shipOrder.shipAddress.ToUpper()+"' AND UPPER(c.[City]) = '"+shipOrder.shipCity.ToUpper()+"'");
			while (dataReader.Read())
			{
				if (!customerNoList.Contains(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString()))
				{
					customerList.Add(new Customer(dataReader));
					customerNoList.Add(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString());
				}
			}		
			dataReader.Close();

			if (customerList.Count == 0)
			{
				if (shipOrder.shipAddress.Length > 6)
				{
					dataReader = database.query("SELECT c.[Organization No], c.[No], c.[Name], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Country Code], c.[Phone No], c.[Cell Phone No], c.[Fax No], c.[E-mail], c.[Production Site], c.[Registration No], c.[Person No], c.[Position X], c.[Position Y], c.[Contact Name], c.[Dairy No], c.[Dairy Code], c.[Hide], c.[Bill-to Customer No], c.[Blocked], c.[Force Cash Payment], c.[Direction Comment], c.[Direction Comment 2], c.[Price Group Code], c.[Editable], c.[Updated], c.[Unverified] FROM [Customer] c, [Organization] o WHERE c.[Organization No] = o.[No] AND o.[Call Center Member] = 1 AND UPPER(c.[Address]) LIKE '%"+shipOrder.shipAddress.Substring(1, 6).ToUpper()+"%' AND UPPER(c.[City]) = '"+shipOrder.shipCity.ToUpper()+"'");
					while (dataReader.Read())
					{
						if (!customerNoList.Contains(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString()))
						{
							customerList.Add(new Customer(dataReader));
							customerNoList.Add(dataReader.GetValue(0).ToString()+"-"+dataReader.GetValue(1).ToString());
						}
					}		
					dataReader.Close();
				}
			}

			if (shipOrder.phoneNo == "###########") shipOrder.phoneNo = "";
			if (shipOrder.cellPhoneNo == "###########") shipOrder.cellPhoneNo = "";

			return customerList;
		}

	}
}
