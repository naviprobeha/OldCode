using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataCustomer
	{

		public string organizationNo;
		public string no;
		public string name;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string contactName;
		public string phoneNo;
		public string cellPhoneNo;
		public string faxNo;
		public string eMail;
		public string productionSite;
		public string registrationNo;
		public string personNo;
		public string dairyNo;
		public int positionX;
		public int positionY;
		public DateTime lastUpdated;
		public string priceGroupCode;
		public string billToCustomerNo;
		public bool blocked;
		public bool hide;
		public string dairyCode;
		public bool forceCashPayment;
		public bool modifyable;

		public string directionComment;
		public string directionComment2;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataCustomer(SmartDatabase smartDatabase, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.no = no;
			getFromDb();
		}

		public DataCustomer(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			no = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			name = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			contactName = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			faxNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
			eMail = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
			registrationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
			personNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
			dairyNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(18).ToString());
			priceGroupCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
			dairyCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() == "1") hide = true;
			billToCustomerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString() != "0") blocked = true;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString() == "1") forceCashPayment = true;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "1") modifyable = true;
			directionComment = dataset.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
			directionComment2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
			

			lastUpdated = DateTime.Now;
		}


		public void commit()
		{

			int hideVal = 0;
			int blockedVal = 0;
			int forceCashPaymentVal = 0;
			int modifyableVal = 0;

			if (hide) hideVal = 1;
			if (blocked) blockedVal = 1;
			if (forceCashPayment) forceCashPaymentVal = 1;
			if (modifyable) modifyableVal = 1;
			if (hide) updateMethod = "D";
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM customer WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM customer WHERE no = '"+no+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE customer SET organizationNo = '"+organizationNo+"', name = '"+name+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', countryCode = '"+countryCode+"', contactName = '"+contactName+"', phoneNo = '"+phoneNo+"', cellPhoneNo = '"+cellPhoneNo+"', faxNo = '"+faxNo+"', eMail = '"+eMail+"', productionSite = '"+productionSite+"', registrationNo = '"+registrationNo+"', personNo = '"+personNo+"', dairyNo = '"+dairyNo+"', positionX = '"+positionX+"', positionY = '"+positionY+"', lastUpdated = GETDATE(), priceGroupCode = '"+priceGroupCode+"', dairyCode = '"+dairyCode+"', hide = '"+hideVal+"', billToCustomerNo = '"+billToCustomerNo+"', blocked = '"+blockedVal+"', forceCashPayment = '"+forceCashPaymentVal+"', modifyable = '"+modifyableVal+"', directionComment = '"+directionComment+"', directionComment2 = '"+directionComment2+"' WHERE no = '"+no+"'");
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
					smartDatabase.nonQuery("INSERT INTO customer (no, organizationNo, name, address, address2, postCode, city, countryCode, contactName, phoneNo, cellPhoneNo, faxNo, eMail, productionSite, registrationNo, personNo, dairyNo, positionX, positionY, lastUpdated, priceGroupCode, dairyCode, hide, billToCustomerNo, blocked, forceCashPayment, modifyable, directionComment, directionComment2) VALUES ('"+no+"','"+organizationNo+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+contactName+"','"+phoneNo+"','"+cellPhoneNo+"','"+faxNo+"','"+eMail+"','"+productionSite+"','"+registrationNo+"','"+personNo+"','"+dairyNo+"','"+positionX+"','"+positionY+"', GETDATE(), '"+priceGroupCode+"','"+dairyCode+"','"+hideVal+"','"+billToCustomerNo+"','"+blockedVal+"','"+forceCashPaymentVal+"', '"+modifyableVal+"', '"+directionComment+"', '"+directionComment2+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no, organizationNo, name, address, address2, postCode, city, countryCode, contactName, phoneNo, cellPhoneNo, faxNo, eMail, productionSite, registrationNo, personNo, dairyNo, positionX, positionY, lastUpdated, priceGroupCode, dairyCode, hide, billToCustomerNo, blocked, forceCashPayment, modifyable, directionComment, directionComment2 FROM customer WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetValue(0).ToString();
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.name = dataReader.GetValue(2).ToString();
					this.address = dataReader.GetValue(3).ToString();
					this.address2 = dataReader.GetValue(4).ToString();
					this.postCode = dataReader.GetValue(5).ToString();
					this.city = dataReader.GetValue(6).ToString();
					this.countryCode = dataReader.GetValue(7).ToString();
					this.contactName = dataReader.GetValue(8).ToString();
					this.phoneNo = dataReader.GetValue(9).ToString();
					this.cellPhoneNo = dataReader.GetValue(10).ToString();
					this.faxNo = dataReader.GetValue(11).ToString();
					this.eMail = dataReader.GetValue(12).ToString();
					this.productionSite = dataReader.GetValue(13).ToString();
					this.registrationNo = dataReader.GetValue(14).ToString();
					this.personNo = dataReader.GetValue(15).ToString();
					this.dairyNo = dataReader.GetValue(16).ToString();
					
					this.positionX = dataReader.GetInt32(17);
					this.positionY = dataReader.GetInt32(18);

					this.lastUpdated = dataReader.GetDateTime(19);

					this.priceGroupCode = dataReader.GetValue(20).ToString();
					this.dairyCode = dataReader.GetValue(21).ToString();

					if (dataReader.GetInt32(22) == 1) this.hide = true;

					this.billToCustomerNo = dataReader.GetValue(23).ToString();

					if (dataReader.GetInt32(24) == 1) this.blocked = true;
					if (dataReader.GetInt32(25) == 1) this.forceCashPayment = true;
					if (dataReader.GetInt32(26) == 1) this.modifyable = true;

					this.directionComment = dataReader.GetValue(27).ToString();
					this.directionComment2 = dataReader.GetValue(28).ToString();

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

		public bool hasShipAddresses()
		{
			DataCustomerShipAddresses dataCustomerShipAddresses = new DataCustomerShipAddresses(smartDatabase);
			DataSet customerShipAddressDataSet = dataCustomerShipAddresses.getDataSet(this.no);
			if (customerShipAddressDataSet.Tables[0].Rows.Count > 0) return true;

			return false;

		}
	}
}
