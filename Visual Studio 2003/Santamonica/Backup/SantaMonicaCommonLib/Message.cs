using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	public class Message
	{
		public string organizationNo;
		public int entryNo;
		public string fromName;
		public string message;

		private string updateMethod;

		public Message(string organizationNo)
		{
			this.organizationNo = organizationNo;
		}

		public Message(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.fromName = dataReader.GetValue(2).ToString();
			this.message = dataReader.GetValue(3).ToString();

		}


		public void save(Database database)
		{
			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Message] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM Message WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Message] SET [From Name] = '"+fromName+"', [Message] = '"+message+"' WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Message] ([Organization No], [From Name], [Message]) VALUES ('"+organizationNo+"','"+fromName+"','"+message+"')");

					this.entryNo = (int)database.getInsertedSeqNo();
				}
			}
		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}
	}
}
