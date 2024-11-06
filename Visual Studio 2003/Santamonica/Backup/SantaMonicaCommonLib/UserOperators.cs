using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Operators.
	/// </summary>
	public class UserOperators
	{
		public UserOperators()
		{
			//
			// TODO: Add constructor logic here
			//

		}

		public UserOperator getOperator(Database database, string userId, string password)
		{
			UserOperator userOperator = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Name], [System Role Code], [Phone User ID], [Phone Password] FROM [Operator] WHERE [User ID] = '"+userId+"' AND [Password] = '"+password+"'");
			if (dataReader.Read())
			{
				userOperator = new UserOperator(dataReader);
			}

			dataReader.Close();
			return userOperator;

		}

		public UserOperator getOperator(Database database, string userId)
		{
			UserOperator userOperator = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Name], [System Role Code], [Phone User ID], [Phone Password] FROM [Operator] WHERE [User ID] = '"+userId+"'");
			if (dataReader.Read())
			{
				userOperator = new UserOperator(dataReader);
			}

			dataReader.Close();
			return userOperator;

		}

		public DataSet getPhoneOperators(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [User ID], [Name], [System Role Code], [Phone User ID], [Phone Password] FROM [Operator] WHERE [Phone User ID] <> ''");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "operator");
			adapter.Dispose();

			return dataSet;

		}

	}
}
