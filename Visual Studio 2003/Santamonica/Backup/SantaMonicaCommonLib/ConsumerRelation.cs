using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class ConsumerRelation
	{
		public string consumerNo;
		public int type;
		public string no;
		public int priority;
		public int travelTime;
		public string categoryCode;
		public int quantity;

		public ConsumerRelation()
		{

		}

		public ConsumerRelation(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.consumerNo = dataReader.GetValue(0).ToString();
			this.type = int.Parse(dataReader.GetValue(1).ToString());
			this.no = dataReader.GetValue(2).ToString();
			this.priority = dataReader.GetInt32(3);
			this.travelTime = dataReader.GetInt32(4);
			this.categoryCode = dataReader.GetValue(5).ToString();
			this.quantity = dataReader.GetInt32(6);
		}

		public ConsumerRelation(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.consumerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.no = dataRow.ItemArray.GetValue(2).ToString();
			this.priority = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.travelTime = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.categoryCode = dataRow.ItemArray.GetValue(5).ToString();
			this.quantity = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
		}

		public void save(Database database)
		{

			SqlDataReader dataReader = database.query("SELECT [No] FROM [Consumer Relation] WHERE [Consumer No] = '"+consumerNo+"' AND [Type] = '"+type+"' AND [No] = '"+no+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Consumer Relation] SET [Priority] = '"+priority+"', [Travel Time] = '"+this.travelTime+"', [Category Code] = '"+this.categoryCode+"', [Quantity] = '"+quantity+"' WHERE [Consumer No] = '"+consumerNo+"' AND [Type] = '"+type+"' AND [No] = '"+no+"'");

			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Consumer Relation] ([Consumer No], [Type], [No], [Priority], [Travel Time], [Category Code], [Quantity]) VALUES ('"+consumerNo+"','"+type+"','"+no+"','"+this.priority+"','"+travelTime+"', '"+this.categoryCode+"', '"+this.quantity+"')");
			}

			
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Consumer Relation] WHERE [Consumer No] = '"+consumerNo+"' AND [Type] = '"+type+"' AND [No] = '"+no+"'");
		}

		public Consumer getConsumer(Database database)
		{
			Consumers consumers = new Consumers();
			return consumers.getEntry(database, this.consumerNo);

		}
	}
}
