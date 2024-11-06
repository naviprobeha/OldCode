using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for RoleMenuItems.
	/// </summary>
	public class RoleMenuItems
	{
		public RoleMenuItems()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getUserMenuItemDataSet(Database database, string organizationNo, string userId)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Code], m.[Name], m.[Target], m.[Sort Order] FROM [Menu Item] m, [Role Menu Item] rm, [Organization Operator] o WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Operator User ID] = '"+userId+"' AND o.[Role Code] = rm.[Role Code] AND rm.[Menu Item Code] = m.[Code] ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "menuItem");
			adapter.Dispose();

			return dataSet;

		}

		public MenuItem getFirstUserMenuItem(Database database, string organizationNo, string userId)
		{
			SqlDataReader dataReader = database.query("SELECT m.[Code], m.[Name], m.[Target], m.[Sort Order] FROM [Menu Item] m, [Role Menu Item] rm, [Organization Operator] o WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Operator User ID] = '"+userId+"' AND o.[Role Code] = rm.[Role Code] AND rm.[Menu Item Code] = m.[Code] ORDER BY [Sort Order]");

			MenuItem menuItem = null;

			if (dataReader.Read())
			{
				menuItem = new MenuItem(database, dataReader);

			}
			
			dataReader.Close();

			return menuItem;

		}

		public DataSet getUserMenuShortcutDataSet(Database database, string organizationNo, string userId)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Code], m.[Name], m.[Target], m.[Sort Order] FROM [Menu Item] m, [Role Menu Item] rm, [Organization Operator] o, [Operator Menu Shortcut] s WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Operator User ID] = '"+userId+"' AND o.[Role Code] = rm.[Role Code] AND rm.[Menu Item Code] = m.[Code] AND s.[Operator User ID] = o.[Operator User ID] AND s.[Menu Item Code] = m.[Code] ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "menuItem");
			adapter.Dispose();

			return dataSet;

		}
	}
}
