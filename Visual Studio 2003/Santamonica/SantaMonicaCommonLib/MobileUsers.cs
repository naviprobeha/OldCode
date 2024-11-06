using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class MobileUsers
	{

		public MobileUsers()
		{

		}

		public MobileUser getEntry(Database database, string organizationNo, int entryNo)
		{
			MobileUser mobileUser = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Name], [Password] FROM [Mobile User] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				mobileUser = new MobileUser(dataReader);
			}
			
			dataReader.Close();
			
			return mobileUser;
		}

		public DataSet getDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Name], [Password] FROM [Mobile User] WHERE [Organization No] = '"+organizationNo+"' ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "mobileUser");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSetEntry(Database database, int entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Name], [Password] FROM [Mobile User] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "mobileUser");
			adapter.Dispose();

			return dataSet;
		}

	}
}
