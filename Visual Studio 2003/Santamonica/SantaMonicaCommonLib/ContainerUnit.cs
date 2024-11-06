using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ContainerUnit
	{

		public string code;
		public string description;
		public int volumeFactor;
		public int calculationType;


		private string updateMethod;

		public ContainerUnit(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.volumeFactor = int.Parse(dataReader.GetValue(2).ToString());
			this.calculationType = int.Parse(dataReader.GetValue(3).ToString());

			updateMethod = "";

		}

		public ContainerUnit(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
			this.volumeFactor = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.calculationType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());

			updateMethod = "";

		}

		public ContainerUnit()
		{

			this.code = "";
			this.description = "";
			this.volumeFactor = 0;
			this.calculationType = 0;
		}

		public void save(Database database)
		{

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container Unit] WHERE [Code] = '"+code+"'");

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Container Unit] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container Unit] SET [Description] = '"+description+"', [Volume Factor] = '"+volumeFactor.ToString()+"', [Calculation Type] = '"+calculationType.ToString()+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container Unit] ([Code], [Description], [Volume Factor], [Calculation Type]) VALUES ('"+code+"','"+description+"','"+volumeFactor.ToString()+"','"+calculationType.ToString()+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on container unit update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
