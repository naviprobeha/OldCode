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
	public class DataOrganizationLocation
	{
		public string shippingCustomerNo;
		public string name;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataOrganizationLocation(SmartDatabase smartDatabase, string shippingCustomerNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.shippingCustomerNo = shippingCustomerNo;
			getFromDb();
		}

		public DataOrganizationLocation(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			shippingCustomerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			name = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
	
		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM organizationLocation WHERE shippingCustomerNo = '"+shippingCustomerNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM organizationLocation WHERE shippingCustomerNo = '"+shippingCustomerNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE organizationLocation SET name = '"+name+"' WHERE shippingCustomerNo = '"+shippingCustomerNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO organizationLocation (shippingCustomerNo, name) VALUES ('"+shippingCustomerNo+"','"+name+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT shippingCustomerNo, name FROM organizationLocation WHERE shippingCustomerNo = '"+shippingCustomerNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.shippingCustomerNo = dataReader.GetValue(0).ToString();
					this.name = dataReader.GetValue(1).ToString();

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
