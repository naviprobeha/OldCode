using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ContainerServiceEntry
	{

		public int entryNo;
		public string containerNo;
		public DateTime entryDateTime;
		public string userId;
		public string factoryNo;

		private string updateMethod;

		public ContainerServiceEntry()
		{

		}

		public ContainerServiceEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.containerNo = dataReader.GetValue(1).ToString();
			
			DateTime entryDate = dataReader.GetDateTime(2);
			DateTime entryTime = dataReader.GetDateTime(3);
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.userId = dataReader.GetValue(4).ToString();
			this.factoryNo = dataReader.GetValue(5).ToString();

			updateMethod = "";

		}

		public ContainerServiceEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.containerNo = dataRow.ItemArray.GetValue(1).ToString();
			
			DateTime entryDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			DateTime entryTime = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.userId = dataRow.ItemArray.GetValue(4).ToString();
			this.factoryNo = dataRow.ItemArray.GetValue(5).ToString();

			updateMethod = "";

		}




		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container Service Entry] WHERE [Entry No] = '"+entryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Container Service Entry] WHERE [Entry No] = '"+entryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container Service Entry] SET [Container No] = '"+containerNo+"', [Entry Date] = '"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Entry Time] = '"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"', [User ID] = '"+this.userId+"', [Factory No] = '"+this.factoryNo+"' WHERE [Entry No] = '"+entryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container Service Entry] ([Container No], [Entry Date], [Entry Time], [User ID], [Factory No]) VALUES ('"+this.containerNo+"','"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"','"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.userId+"', '"+this.factoryNo+"')");
						entryNo = (int)database.getInsertedSeqNo();
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on container service entry update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public Container getContainer(Database database)
		{
			Containers containers = new Containers();
			return containers.getEntry(database, this.containerNo);

		}

	}
}
