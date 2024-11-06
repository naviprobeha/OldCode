using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class FactoryType
	{

		public string code;
		public string description;

		private string updateMethod;

		public FactoryType(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();

			updateMethod = "";

		}


		public void save(Database database)
		{

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Factory Type] WHERE [Code] = '"+code+"'");

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Factory Type] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Factory Type] SET [Description] = '"+description+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Factory Type] ([Code], [Description]) VALUES ('"+code+"','"+description+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on pickup factory type update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
