using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class ConsumerRelations
	{
		public ConsumerRelations()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet(Database database, int type, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Type], [No], [Priority], [Travel Time], [Category Code], [Quantity] FROM [Consumer Relation] WHERE [Type] = '"+type+"' AND [No] = '"+no+"' ORDER BY [Priority]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerRelation");
			adapter.Dispose();

			return dataSet;

		}

		public ConsumerRelation getEntry(Database database, string consumerNo, int type, string no)
		{
			ConsumerRelation consumerRelation = null;

			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Type], [No], [Priority], [Travel Time], [Category Code], [Quantity] FROM [Consumer Relation] WHERE [Type] = '"+type+"' AND [No] = '"+no+"' AND [Consumer No] = '"+consumerNo+"'");
			if (dataReader.Read())
			{
				consumerRelation = new ConsumerRelation(dataReader);

			}

			dataReader.Close();

			return consumerRelation;

		}

	}
}
