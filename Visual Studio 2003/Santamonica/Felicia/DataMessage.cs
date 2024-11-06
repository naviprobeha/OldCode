using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataMessage
	{
		public int entryNo;
		public string organizationNo;
		public string fromName;
		public string message;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataMessage(SmartDatabase smartDatabase, int entryNo, bool readFromDb)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			if (readFromDb) getFromDb();
		}


		public DataMessage(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
			fromName = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			message = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();

		}


		public void commit()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM message WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM message WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE message SET entryNo = '"+entryNo.ToString()+"', organizationNo = '"+organizationNo.ToString()+"', fromName = '"+fromName.ToString()+"', message = '"+message+"' WHERE entryNo = '"+entryNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				if ((updateMethod == null) || (!updateMethod.Equals("D")))
				{
					try
					{
						smartDatabase.nonQuery("INSERT INTO message (entryNo, organizationNo, fromName, message) VALUES ('"+entryNo.ToString()+"','"+organizationNo+"','"+fromName+"','"+message+"')");
					
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			dataReader.Dispose();	
		}
		

		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM message WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.fromName = dataReader.GetValue(2).ToString();
					this.message = dataReader.GetValue(3).ToString();
					
					dataReader.Dispose();
					return true;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return false;
			
		}

		public void delete()
		{
			this.updateMethod = "D";
			commit();
		}

	}
}
