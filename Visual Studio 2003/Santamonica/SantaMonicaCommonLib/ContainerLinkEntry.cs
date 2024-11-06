using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ContainerLinkEntry
	{

		public int containerSericeEntryNo;
		public int containerEntryNo;
		public int linkType;

		private string updateMethod;

		public ContainerLinkEntry()
		{

		}

		public ContainerLinkEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.containerSericeEntryNo = dataReader.GetInt32(0);
			this.containerEntryNo = dataReader.GetInt32(1);
			this.linkType = int.Parse(dataReader.GetValue(2).ToString());
			
			updateMethod = "";

		}

		public ContainerLinkEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.containerSericeEntryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.containerEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.linkType = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			
			updateMethod = "";

		}




		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container Link Entry] WHERE [Container Entry No] = '"+this.containerEntryNo+"' AND [Container Service Entry No] = '"+this.containerSericeEntryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Container Entry No] FROM [Container Link Entry] WHERE [Container Entry No] = '"+this.containerEntryNo+"' AND [Container Service Entry No] = '"+this.containerSericeEntryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container Link Entry] SET [Link Type] = '"+this.linkType+"' WHERE [Container Entry No] = '"+this.containerEntryNo+"' AND [Container Service Entry No] = '"+this.containerSericeEntryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container Link Entry] ([Container Service Entry No], [Container Entry No], [Link Type]) VALUES ('"+this.containerSericeEntryNo+"','"+this.containerEntryNo+"','"+this.linkType+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on container link entry update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
