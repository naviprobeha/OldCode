using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ContainerType
	{

		public string code;
		public string description;
		public float weight;
		public float volume;

		public string unitCode;

		private string updateMethod;

		public ContainerType(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.weight = float.Parse(dataReader.GetValue(2).ToString());
			this.volume = float.Parse(dataReader.GetValue(3).ToString());
			this.unitCode = dataReader.GetValue(4).ToString();

			updateMethod = "";

		}

		public ContainerType()
		{

			this.code = "";
			this.description = "";
			this.weight = 0;
			this.volume = 0;
			this.unitCode = "";
		}

		public void save(Database database)
		{

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container Type] WHERE [Code] = '"+code+"'");

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Container Type] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container Type] SET [Description] = '"+description+"', [Weight] = '"+weight.ToString()+"', [Volume] = '"+volume.ToString()+"', [Unit Code] = '"+unitCode+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container Type] ([Code], [Description], [Weight], [Volume], [Unit Code]) VALUES ('"+code+"','"+description+"','"+weight.ToString()+"','"+volume.ToString()+"', '"+unitCode+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on container type update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
