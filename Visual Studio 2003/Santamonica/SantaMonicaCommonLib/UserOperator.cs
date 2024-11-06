using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class UserOperator
	{

		public string userId;
		public string name;
		public string systemRoleCode;
		public string phoneUserId;
		public string phonePassword;

		public UserOperator(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.userId = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.systemRoleCode = dataReader.GetValue(2).ToString();
			this.phoneUserId = dataReader.GetValue(3).ToString();
			this.phonePassword = dataReader.GetValue(4).ToString();
		}

		public UserOperator(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.userId = dataRow.ItemArray.GetValue(0).ToString();
			this.name = dataRow.ItemArray.GetValue(1).ToString();
			this.systemRoleCode = dataRow.ItemArray.GetValue(2).ToString();
			this.phoneUserId = dataRow.ItemArray.GetValue(3).ToString();
			this.phonePassword = dataRow.ItemArray.GetValue(4).ToString();
		}


		public System.Collections.ArrayList getRelations(Database database)
		{
			System.Collections.ArrayList relationList = new System.Collections.ArrayList();

			SqlDataReader dataReader = database.query("SELECT [Organization No], [Operator User ID], [Role Code] FROM [Organization Operator] WHERE [Operator User ID] = '"+userId+"' ORDER BY [Sort Order]");
			while(dataReader.Read())
			{
				OrganizationOperator orgOp = new OrganizationOperator(dataReader);
				relationList.Add(orgOp);
			}
			
			dataReader.Close();

			return relationList;

			

		}

		public Organization getOrganization(Database database, string no)
		{
			Organization organization = null;

			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Navision User ID], [Navision Password], [Stop Fee], [Navision Vendor No], [Stop Item No] FROM [Organization] o, [Organization Operator] p WHERE o.[No] = '"+no+"' AND o.[No] = p.[Organization No] AND p.[Operator User ID] = '"+this.userId+"'");
			if (dataReader.Read())
			{
				organization = new Organization(database, dataReader);			
			}
			
			dataReader.Close();

			return organization;			

		}

		public bool checkSecurity(Database database, Organization organization, string pageName)
		{

			bool securityCheck = false;

			SqlDataReader dataReader = database.query("SELECT [Operator User ID] FROM [Menu Item] m, [Role Menu Item] rm, [Organization Operator] o WHERE m.[Target] = '"+pageName+"' AND m.[Code] = rm.[Menu Item Code] AND rm.[Role Code] = o.[Role Code] AND o.[Organization No] = '"+organization.no+"' AND o.[Operator User ID] = '"+this.userId+"'");
			if (dataReader.Read())
			{
				securityCheck = true;
			}
			
			dataReader.Close();

			return securityCheck;
		}
	}
}
