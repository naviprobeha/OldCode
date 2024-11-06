using System;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Category.
	/// </summary>
	public class Category
	{
		public string code;
		public string description;
		public int sortOrder;
		public bool outgoing;

		private string updateMethod;

		public Category(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.sortOrder = dataReader.GetInt32(2);
			
			outgoing = false;
			if (dataReader.GetValue(3).ToString() == "1") outgoing = true;

			updateMethod = "";
		}

		public void save(Database database)
		{
			int outgoingVal = 0;
			if (outgoing) outgoingVal = 1;

			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Category] WHERE [Code] = '"+code+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Category] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Category] SET [Description] = '"+description+"', [Sort Order] = '"+sortOrder+"', [Outgoing] = '"+outgoingVal+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Category] ([Code], [Description], [Sort Order], [Outgoing]) VALUES ('"+code+"','"+this.description+"','"+this.sortOrder+"', '"+outgoingVal+"')");
					}
				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on category update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


		public bool higherThan(Category category)
		{
			if (this.sortOrder < category.sortOrder) return true;
			return false;
		}
	}
}
