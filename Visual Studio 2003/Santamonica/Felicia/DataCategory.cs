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
	public class DataCategory
	{
		public string code;
		public string description;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataCategory(SmartDatabase smartDatabase, string code)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.code = code;
			getFromDb();
		}

		public DataCategory(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			code = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			description = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
	
		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM category WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM category WHERE code = '"+code+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE category SET code = '"+code+"', description = '"+description+"' WHERE code = '"+code+"'");
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
					smartDatabase.nonQuery("INSERT INTO category (code, description) VALUES ('"+code+"','"+description+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT code, description FROM category WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				try
				{
					this.code = dataReader.GetValue(0).ToString();
					this.description = dataReader.GetValue(1).ToString();

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
