using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataAgent
	{

		public string code;
		public string description;
		public string organizationNo;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataAgent(SmartDatabase smartDatabase, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.code = code;
			getFromDb();
		}

		public DataAgent(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			code = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			description = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM agent WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM agent WHERE code = '"+code+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE agent SET description = '"+description+"', organizationNo = '"+organizationNo+"' WHERE code = '"+code+"'");
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
					smartDatabase.nonQuery("INSERT INTO agent (code, description, organizationNo) VALUES ('"+code+"','"+description+"','"+organizationNo+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT code, description, organizationNo FROM agent WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				try
				{
					this.code = dataReader.GetValue(0).ToString();
					this.description = dataReader.GetValue(1).ToString();
					this.organizationNo = dataReader.GetValue(2).ToString();

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
