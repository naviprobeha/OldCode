using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataContainerLoad
	{

		public string containerNo;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataContainerLoad(SmartDatabase smartDatabase, string containerNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.containerNo = containerNo;

		}


		public void commit()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM containerLoad WHERE containerNo = '"+containerNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM containerLoad WHERE containerNo = '"+containerNo+"'");
				}
				else
				{
					
					try
					{
						//smartDatabase.nonQuery("UPDATE containerLoad SET .. WHERE containerNo = '"+containerNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				
				}
			}
			else
			{
				try
				{
					smartDatabase.nonQuery("INSERT INTO containerLoad (containerNo) VALUES ('"+containerNo+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}


			}
			dataReader.Dispose();	
		}


		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT containerNo FROM containerLoad WHERE containerNo = '"+containerNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.containerNo = dataReader.GetValue(0).ToString();

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
