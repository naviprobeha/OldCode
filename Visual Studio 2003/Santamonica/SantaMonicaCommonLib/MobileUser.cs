using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class MobileUser
	{
		public int entryNo;
		public string organizationNo;
		public string name;
		public string passWord;

		public string updateMethod;

		public MobileUser(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.organizationNo = dataReader.GetValue(1).ToString();
			this.name = dataReader.GetValue(2).ToString();
			this.passWord = dataReader.GetValue(3).ToString();

		}

		public MobileUser(string organizationNo)
		{
			this.organizationNo = organizationNo;
		}

		public void delete(Database database)
		{

			updateMethod = "D";
			save(database);
		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();


			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Mobile User] WHERE [Entry No] = '"+entryNo+"'");
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, 8, this.entryNo.ToString(), 2);
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_LINE, 8, this.entryNo.ToString(), 2);
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_TANK, 8, this.entryNo.ToString(), 2);

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Name] FROM [Mobile User] WHERE [Entry No] = '"+entryNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Mobile User] SET [Organization No] = '"+organizationNo+"', [Name] = '"+name+"', [Password] = '"+passWord+"' WHERE [Entry No] = '"+entryNo+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Mobile User] ([Organization No], [Name], [Password]) VALUES ('"+organizationNo+"','"+name+"','"+passWord+"')");
					entryNo = (int)database.getInsertedSeqNo();
				}

				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, 8, this.entryNo.ToString(), 0);
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_LINE, 8, this.entryNo.ToString(), 0);
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_TANK, 8, this.entryNo.ToString(), 0);

			}
		}

	}
}
