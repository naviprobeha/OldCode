using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataOrganization
	{

		public string no;
		public string name;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;

		public string phoneNo;
		public string faxNo;
		public string eMail;

		public string contactName;

		public float stopFee;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataOrganization(SmartDatabase smartDatabase, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.no = no;
			this.name = no;
			this.address = "";
			this.address2 = "";
			this.postCode = "";
			this.city = "";
			this.countryCode = "";
			this.phoneNo = "";
			this.faxNo = "";
			this.eMail = "";
			this.contactName = "";

			getFromDb();
		}

		public DataOrganization(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			no = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			name = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			faxNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			eMail = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			contactName = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			stopFee = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());

		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM organization WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM organization WHERE no = '"+no+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE organization SET name = '"+name+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', countryCode = '"+countryCode+"', contactName = '"+contactName+"', phoneNo = '"+phoneNo+"', faxNo = '"+faxNo+"', eMail = '"+eMail+"', stopFee = '"+stopFee+"' WHERE no = '"+no+"'");
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
					smartDatabase.nonQuery("INSERT INTO organization (no, name, address, address2, postCode, city, countryCode, contactName, phoneNo, faxNo, eMail, stopFee) VALUES ('"+no+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+contactName+"','"+phoneNo+"','"+faxNo+"','"+eMail+"','"+stopFee+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no, name, address, address2, postCode, city, countryCode, contactName, phoneNo, faxNo, eMail, stopFee FROM organization WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetValue(0).ToString();
					this.name = dataReader.GetValue(1).ToString();
					this.address = dataReader.GetValue(2).ToString();
					this.address2 = dataReader.GetValue(3).ToString();
					this.postCode = dataReader.GetValue(4).ToString();
					this.city = dataReader.GetValue(5).ToString();
					this.countryCode = dataReader.GetValue(6).ToString();
					this.contactName = dataReader.GetValue(7).ToString();
					this.phoneNo = dataReader.GetValue(8).ToString();
					this.faxNo = dataReader.GetValue(9).ToString();
					this.eMail = dataReader.GetValue(10).ToString();
					
					this.stopFee = dataReader.GetFloat(11);

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
