using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class ShippingCustomerOrganization
	{

		public string shippingCustomerNo;
		public int orderType;
		public int type;
		public string code;
		public int sortOrder;

		public ShippingCustomerOrganization()
		{

		}

		public ShippingCustomerOrganization(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.shippingCustomerNo = dataReader.GetValue(0).ToString();
			this.type = int.Parse(dataReader.GetValue(1).ToString());
			this.code = dataReader.GetValue(2).ToString();
			this.sortOrder = dataReader.GetInt32(3);
			this.orderType = int.Parse(dataReader.GetValue(4).ToString());
		}

		public ShippingCustomerOrganization(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.shippingCustomerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.code = dataRow.ItemArray.GetValue(2).ToString();
			this.sortOrder = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.orderType = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
		}

		public void save(Database database)
		{

			SqlDataReader dataReader = database.query("SELECT [Shipping Customer No] FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"' AND [Order Type] = '"+orderType+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Shipping Customer Organization] SET [Sort Order] = '"+sortOrder+"' WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"' AND [Order Type] = '"+orderType+"'");

			}
			else
			{
				dataReader.Close();

				database.nonQuery("INSERT INTO [Shipping Customer Organization] ([Shipping Customer No], [Type], [Code], [Sort Order], [Order Type]) VALUES ('"+shippingCustomerNo+"','"+type+"','"+code+"','"+this.sortOrder+"', '"+this.orderType+"')");
			}

			
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"' AND [Order Type] = '"+orderType+"'");
		}

		public string getType()
		{
			if (type == 0) return "Transportör";
			if (type == 1) return "Bil";
			return "";
		}

		public string getOrderType()
		{
			if (orderType == 0) return "Linjeorder";
			if (orderType == 1) return "Fabriksorder";
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
