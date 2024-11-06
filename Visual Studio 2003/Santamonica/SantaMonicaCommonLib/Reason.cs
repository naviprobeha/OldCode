using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Reason
	{
		public string code;
		public string description;

		public string updateMethod;

		public Reason(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
		}

		public Reason(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
		}


		public void save(Database database)
		{
			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Reason] WHERE [Code] = '"+code+"'");

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Code] FROM [Reason] WHERE [Code] = '"+code+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Reason] SET [Description] = '"+description+"' WHERE [Code] = '"+code+"'");

				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Reason] ([Code], [Description]) VALUES ('"+code+"','"+description+"')");
				}

			}
		}


	}
}
