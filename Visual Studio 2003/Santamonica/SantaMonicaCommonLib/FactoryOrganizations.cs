using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class FactoryOrganizations
	{
		public FactoryOrganizations()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getFactoryDataSet(Database database, string factoryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Type], [Code], [Sort Order] FROM [Factory Organization] WHERE [Factory No] = '"+factoryNo+"' ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrganization");
			adapter.Dispose();

			return dataSet;

		}


		public FactoryOrganization getEntry(Database database, string factoryNo, int type, string code)
		{
			FactoryOrganization factoryOrganization = null;

			SqlDataReader dataReader = database.query("SELECT [Factory No], [Type], [Code], [Sort Order] FROM [Factory Organization] WHERE [Factory No] = '"+factoryNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"'");
			if (dataReader.Read())
			{
				factoryOrganization = new FactoryOrganization(dataReader);

			}

			dataReader.Close();

			return factoryOrganization;

		}

		public Agent getFactoryAgent(Database database, string factoryNo)
		{
			DataSet dataSet = this.getFactoryDataSet(database, factoryNo);

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryOrganization factoryOrganization = new FactoryOrganization(dataSet.Tables[0].Rows[i]);

				if (factoryOrganization.type == 0)
				{
					Agents agents = new Agents();
					DataSet agentDataSet = agents.getDataSet(database, factoryOrganization.code, Agents.TYPE_TANK);
					if (agentDataSet.Tables[0].Rows.Count > 0)
					{
						return new Agent(agentDataSet.Tables[0].Rows[0]);
					}
				}

				if (factoryOrganization.type == 1)
				{
					Agents agents = new Agents();
					return agents.getAgent(database, factoryOrganization.code);
				}

				i++;
			}

			return null;
		}

	}
}
