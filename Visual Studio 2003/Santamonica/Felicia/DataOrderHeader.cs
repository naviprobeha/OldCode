using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataOrder.
	/// </summary>
	public class DataOrderHeader
	{

		public string organizationNo;
		public int entryNo;
		public DateTime shipDate;
		public string customerNo;
		public string customerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string cellPhoneNo;
		public string productionSite;
		public string dairyCode;
		public string dairyNo;
		public int payment;
		public int status;
		public string reference;
		public string mobileUserName;
		public string containerNo;
		public int shipOrderEntryNo;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public int positionX;
		public int positionY;

		public string comments;
		public string agentCode;

		public DataCustomer dataCustomer;

		private string updateMethod;
		private SmartDatabase smartDatabase;
		

		public DataOrderHeader(SmartDatabase smartDatabase, int no)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = no;
			getFromDb();
		}


		public DataOrderHeader(SmartDatabase smartDatabase, DataCustomer dataCustomer, string customerShipAddressNo)
		{
			this.smartDatabase = smartDatabase;

			fromCustomer(dataCustomer);
			this.shipOrderEntryNo = 0;

			applyCustomerShipAddress(customerShipAddressNo);

			commit();

		}


		public void fromCustomer(DataCustomer dataCustomer)
		{
			this.organizationNo = dataCustomer.organizationNo;
			this.shipDate = DateTime.Now;
			this.customerNo = dataCustomer.no;
			this.customerName = dataCustomer.name;
			this.address = dataCustomer.address;
			this.address2 = dataCustomer.address2;
			this.postCode = dataCustomer.postCode;
			this.city = dataCustomer.city;
			this.productionSite = dataCustomer.productionSite;
			this.dairyCode = dataCustomer.dairyCode;
			this.dairyNo = dataCustomer.dairyNo;
			this.reference = "";
			this.mobileUserName = "";
			this.phoneNo = dataCustomer.phoneNo;
			this.cellPhoneNo = dataCustomer.cellPhoneNo;
			if (cellPhoneNo.Length > 20) cellPhoneNo = cellPhoneNo.Substring(1, 20);
			

			this.positionX = dataCustomer.positionX;
			this.positionY = dataCustomer.positionY;

			this.dataCustomer = dataCustomer;

		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM orderHeader WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM orderHeader WHERE entryNo = '"+entryNo+"'");
				}

				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE orderHeader SET organizationNo = '"+organizationNo+"', shipDate = '"+shipDate.ToString("yyyy-MM-dd")+"', customerNo = '"+customerNo+"', customerName = '"+customerName+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', countryCode = '"+countryCode+"', phoneNo = '"+phoneNo+"', cellPhoneNo = '"+cellPhoneNo+"', productionSite = '"+productionSite+"', status = '"+status+"', positionX = '"+positionX+"', positionY = '"+positionY+"', payment = '"+payment+"', dairyCode = '"+dairyCode+"', dairyNo = '"+dairyNo+"', reference = '"+reference+"', mobileUserName = '"+mobileUserName+"', containerNo = '"+containerNo+"', shipOrderEntryNo = '"+shipOrderEntryNo+"', comments = '"+comments+"', agentCode = '"+agentCode+"', customerShipAddressNo = '"+this.customerShipAddressNo+"', shipName = '"+this.shipName+"', shipAddress = '"+this.shipAddress+"', shipAddress2 = '"+this.shipAddress2+"', shipPostCode = '"+this.shipPostCode+"', shipCity = '"+this.shipCity+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO orderHeader (organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, productionSite, status, positionX, positionY, payment, dairyCode, dairyNo, reference, mobileUserName, containerNo, shipOrderEntryNo, comments, agentCode, customerShipAddressNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity) VALUES ('"+organizationNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+customerNo+"','"+customerName+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+cellPhoneNo+"','"+productionSite+"','"+status+"','"+positionX+"','"+positionY+"', '"+payment+"','"+dairyCode+"','"+dairyNo+"','"+reference+"','"+mobileUserName+"','"+containerNo+"','"+shipOrderEntryNo+"', '"+comments+"', '"+agentCode+"', '"+this.customerShipAddressNo+"', '"+this.shipName+"', '"+this.shipAddress+"', '"+this.shipAddress2+"', '"+this.shipPostCode+"', '"+this.shipCity+"')");
					dataReader = smartDatabase.query("SELECT entryNo FROM orderHeader WHERE entryNo = @@IDENTITY");
					if (dataReader.Read())
					{
						this.entryNo = dataReader.GetInt32(0);

					}
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, productionSite, status, positionX, positionY, payment, dairyCode, dairyNo, reference, mobileUserName, containerNo, shipOrderEntryNo, comments, agentCode, customerShipAddressNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity FROM orderHeader WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.shipDate = dataReader.GetDateTime(2);
					this.customerNo = dataReader.GetValue(3).ToString();
					this.customerName = dataReader.GetValue(4).ToString();
					this.address = dataReader.GetValue(5).ToString();
					this.address2 = dataReader.GetValue(6).ToString();
					this.postCode = dataReader.GetValue(7).ToString();
					this.city = dataReader.GetValue(8).ToString();	
					this.countryCode = dataReader.GetValue(9).ToString();
					this.phoneNo = dataReader.GetValue(10).ToString();
					this.cellPhoneNo = dataReader.GetValue(11).ToString();
					this.productionSite = dataReader.GetValue(12).ToString();
					this.status = dataReader.GetInt32(13);
					this.positionX = dataReader.GetInt32(14);
					this.positionY = dataReader.GetInt32(15);
					this.payment = dataReader.GetInt32(16);
					this.dairyCode = dataReader.GetValue(17).ToString();
					this.dairyNo = dataReader.GetValue(18).ToString();
					this.reference = dataReader.GetValue(19).ToString();
					this.mobileUserName = dataReader.GetValue(20).ToString();
					this.containerNo = dataReader.GetValue(21).ToString();
					this.shipOrderEntryNo = dataReader.GetInt32(22);
					this.comments = dataReader.GetValue(23).ToString();
					this.agentCode = dataReader.GetValue(24).ToString();

					this.customerShipAddressNo = dataReader.GetValue(25).ToString();
					this.shipName = dataReader.GetValue(26).ToString();
					this.shipAddress = dataReader.GetValue(27).ToString();
					this.shipAddress2 = dataReader.GetValue(28).ToString();
					this.shipPostCode = dataReader.GetValue(29).ToString();
					this.shipCity = dataReader.GetValue(30).ToString();

					this.dataCustomer = new DataCustomer(smartDatabase, customerNo);

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

		public string getTotalAmount()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT Str(SUM(totalAmount), 8, 2) as sumAmount FROM orderLine WHERE orderEntryNo = '"+this.entryNo+"'");

			string sumAmount = "0.00";
			if (dataReader.Read())
			{
				try
				{
					sumAmount = dataReader.GetValue(0).ToString();
					dataReader.Dispose();
					if (sumAmount == "") return "0.00";
					return sumAmount;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return sumAmount;

		}


		private void applyCustomerShipAddress(string customerShipAddressNo)
		{
			if (customerShipAddressNo != "")
			{
				DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(smartDatabase, int.Parse(customerShipAddressNo));

				this.customerShipAddressNo = customerShipAddressNo;
				this.shipName = dataCustomerShipAddress.name;
				this.shipAddress = dataCustomerShipAddress.address;
				this.shipAddress2 = dataCustomerShipAddress.address2;
				this.shipPostCode = dataCustomerShipAddress.postCode;
				this.shipCity = dataCustomerShipAddress.city;
				this.productionSite = dataCustomerShipAddress.productionSite;

			}
			else
			{
				this.shipName = this.customerName;
				this.shipAddress = this.address;
				this.shipAddress2 = this.address2;
				this.shipPostCode = this.postCode;
				this.shipCity = this.city;

			}
		}
	}
}
