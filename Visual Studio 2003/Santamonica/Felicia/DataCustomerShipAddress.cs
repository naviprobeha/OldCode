using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataCustomerShipAddress
	{

		public string organizationNo;
		public string customerNo;
		public int entryNo;
		public string name;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string contactName;
		public string phoneNo;
		public string productionSite;
		
		public string directionComment;
		public string directionComment2;

		public int positionX;
		public int positionY;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataCustomerShipAddress(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataCustomerShipAddress(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			customerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			name = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			contactName = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
	
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());

			directionComment = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			directionComment2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
			productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
		}


		public void commit()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM customerShipAddress WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM customerShipAddress WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE customerShipAddress SET organizationNo = '"+organizationNo+"', customerNo = '"+customerNo+"', name = '"+name+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', countryCode = '"+countryCode+"', contactName = '"+contactName+"', directionComment = '"+directionComment+"', directionComment2 = '"+directionComment2+"', positionX = '"+positionX+"', positionY = '"+positionY+"', phoneNo = '"+phoneNo+"', productionSite = '"+productionSite+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO customerShipAddress (entryNo, organizationNo, customerNo, name, address, address2, postCode, city, countryCode, contactName, directionComment, directionComment2, positionX, positionY, phoneNo, productionSite) VALUES ('"+entryNo+"','"+organizationNo+"','"+customerNo+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+contactName+"','"+directionComment+"','"+directionComment2+"','"+positionX+"','"+positionY+"','"+phoneNo+"','"+productionSite+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, customerNo, name, address, address2, postCode, city, countryCode, contactName, positionX, positionY, directionComment, directionComment2, phoneNo, productionSite FROM customerShipAddress WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.customerNo = dataReader.GetValue(2).ToString();
					this.name = dataReader.GetValue(3).ToString();
					this.address = dataReader.GetValue(4).ToString();
					this.address2 = dataReader.GetValue(5).ToString();
					this.postCode = dataReader.GetValue(6).ToString();
					this.city = dataReader.GetValue(7).ToString();
					this.countryCode = dataReader.GetValue(8).ToString();
					this.contactName = dataReader.GetValue(9).ToString();
					
					this.positionX = dataReader.GetInt32(10);
					this.positionY = dataReader.GetInt32(11);

					this.directionComment = dataReader.GetValue(12).ToString();
					this.directionComment2 = dataReader.GetValue(13).ToString();

					this.phoneNo = dataReader.GetValue(14).ToString();
					this.productionSite = dataReader.GetValue(15).ToString();


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

		public void deleteAll()
		{
			smartDatabase.nonQuery("DELETE FROM customerShipAddress");
		}

	}
}
