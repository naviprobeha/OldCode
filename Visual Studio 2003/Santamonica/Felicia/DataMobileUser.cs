using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;
using System.IO;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataMobileUser
	{
		public int entryNo;
		public string organizationNo;
		public string name;
		public string password;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataMobileUser(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataMobileUser(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			name = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			password = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
	
		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM mobileUser WHERE entryNo = '"+entryNo.ToString()+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM mobileUser WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE mobileUser SET organizationNo = '"+organizationNo+"', name = '"+name+"', password = '"+password+"' WHERE entryNo = '"+entryNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				if (updateMethod != "D")
				{
					try
					{
						smartDatabase.nonQuery("INSERT INTO mobileUser (entryNo, organizationNo, name, password) VALUES ('"+entryNo+"','"+organizationNo+"','"+name+"','"+password+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, name, password FROM mobileUser WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.name = dataReader.GetValue(2).ToString();
					this.password = dataReader.GetValue(3).ToString();

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
