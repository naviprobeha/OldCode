using System;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for MenuItem.
	/// </summary>
	public class MenuItem
	{
		public string code;
		public string name;
		public string target;
		public int sortOrder;

		private Database database;

		public MenuItem(Database database, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

			this.code = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.target = dataReader.GetValue(2).ToString();
			this.sortOrder = int.Parse(dataReader.GetValue(3).ToString());

		}
	}
}
