using System;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Category.
	/// </summary>
	public class AgentStorageGroup
	{
		public string code;
		public string description;
		public int noOfContainers;
		public int volumeStorage;

		private string updateMethod;

		public AgentStorageGroup(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.noOfContainers = dataReader.GetInt32(2);
			this.volumeStorage = dataReader.GetInt32(3);

			updateMethod = "";
		}

		public void save(Database database)
		{
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Agent Storage Group] WHERE [Code] = '"+code+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Agent Storage Group] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Agent Storage Group] SET [Description] = '"+description+"', [No Of Containers] = '"+this.noOfContainers+"', [Volume Storage] = '"+this.volumeStorage+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Agent Storage Group] ([Code], [Description], [No Of Containers], [Volume Storage]) VALUES ('"+code+"','"+this.description+"','"+this.noOfContainers+"', '"+this.volumeStorage+"')");
					}
				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on agent storage group update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
