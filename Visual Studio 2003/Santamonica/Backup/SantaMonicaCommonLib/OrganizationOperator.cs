using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class OrganizationOperator
	{

		public string organizationNo;
		public string userId;
		public string roleCode;

		public OrganizationOperator(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.organizationNo = dataReader.GetValue(0).ToString();
			this.userId = dataReader.GetValue(1).ToString();
			this.roleCode = dataReader.GetValue(2).ToString();

		}


	}
}
