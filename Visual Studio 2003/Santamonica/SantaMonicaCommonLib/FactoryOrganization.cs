using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class FactoryOrganization
	{

		public string factoryNo;
		public int orderType;
		public int type;
		public string code;
		public int sortOrder;

		public FactoryOrganization()
		{

		}

		public FactoryOrganization(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.factoryNo = dataReader.GetValue(0).ToString();
			this.type = int.Parse(dataReader.GetValue(1).ToString());
			this.code = dataReader.GetValue(2).ToString();
			this.sortOrder = dataReader.GetInt32(3);
		}

		public FactoryOrganization(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.factoryNo = dataRow.ItemArray.GetValue(0).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.code = dataRow.ItemArray.GetValue(2).ToString();
			this.sortOrder = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
		}

		public void save(Database database)
		{

			SqlDataReader dataReader = database.query("SELECT [Factory No] FROM [Shipping Customer Organization] WHERE [Factory No] = '"+factoryNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Factory Organization] SET [Sort Order] = '"+sortOrder+"' WHERE [Factory No] = '"+factoryNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"' AND");

			}
			else
			{
				dataReader.Close();

				database.nonQuery("INSERT INTO [Factory Organization] ([Factory No], [Type], [Code], [Sort Order]) VALUES ('"+factoryNo+"','"+type+"','"+code+"','"+this.sortOrder+"')");
			}

			
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Factory Organization] WHERE [Factory No] = '"+factoryNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"'");
		}

		public string getType()
		{
			if (type == 0) return "Transportör";
			if (type == 1) return "Bil";
			return "";
		}


		public Organization getOrganization(Database database)
		{
			if (type == 0)
			{
				Organizations organizations = new Organizations();
				return organizations.getOrganization(database, this.code);
			}
			return null;
		}

		public Agent getAgent(Database database)
		{
			if (type == 1)
			{
				Agents agents = new Agents();
				return agents.getAgent(database, this.code);
			}
			return null;
		}

	}
}
