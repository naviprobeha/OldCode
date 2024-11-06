using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Maps.
	/// </summary>
	public class Maps
	{
		public Maps()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Map getMap(Database database, string organizationNo, string code)
		{
			Map map = null;
			
			SqlDataReader dataReader = database.query("SELECT m.[Code], m.[Description], m.[Position Top], m.[Position Left], m.[Position Bottom], m.[Position Right] FROM [Map] m, [Organization Map] o WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Map Code] = m.[Code] AND m.[Code] = '"+code+"'");
			if (dataReader.Read())
			{
				map = new Map(dataReader);
			}
			
			dataReader.Close();
			return map;

		}

		public DataSet getOrganizationMaps(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Code], m.[Description], m.[Position Top], m.[Position Left], m.[Position Right], m.[Position Bottom] FROM [Map] m, [Organization Map] o WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Map Code] = m.[Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "map");
			adapter.Dispose();

			return dataSet;


		}

		public DataSet getAgentMaps(Database database, string agentCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Code], m.[Description], m.[Position Top], m.[Position Left], m.[Position Right], m.[Position Bottom] FROM [Map] m, [Organization Map] o, [Organization Agent] a WHERE o.[Organization No] = a.[Organization No] AND o.[Map Code] = m.[Code] AND a.[Agent Code] = '"+agentCode+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "map");
			adapter.Dispose();

			return dataSet;


		}

		public DataSet getDataSetEntry(Database database, string code)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Position Top], [Position Left], [Position Right], [Position Bottom] FROM [Map] WHERE [Code] = '"+code+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "map");
			adapter.Dispose();

			return dataSet;


		}
	}
}
