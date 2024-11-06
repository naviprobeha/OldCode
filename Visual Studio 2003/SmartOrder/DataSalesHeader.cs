using System;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataOrder.
	/// </summary>
	public class DataSalesHeader
	{
		private int noValue;
		private string customerNoValue;
		private string nameValue;
		private string addressValue;
		private string address2Value;
		private string zipCodeValue;
		private string cityValue;
		private string contactValue;
		private string phoneValue;

		private string deliveryCodeValue;
		private string deliveryNameValue;
		private string deliveryAddressValue;
		private string deliveryAddress2Value;
		private string deliveryZipCodeValue;
		private string deliveryCityValue;
		private string deliveryContactValue;

		private int readyValue;

		private SmartDatabase smartDatabase;

		public DataSalesHeader(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			readyValue = 0;
		}

		public DataSalesHeader(int no, SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = no;
			this.smartDatabase = smartDatabase;
			getFromDb();
		}

		public DataSalesHeader(DataCustomer dataCustomer, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.customerNo = dataCustomer.no;
			this.name = dataCustomer.name;
			this.address = dataCustomer.address;
			this.zipCode = dataCustomer.zipCode;
			this.city = dataCustomer.city;
			this.readyValue = 0;
		}

		public int no
		{
			get
			{
				return noValue;
			}
			set
			{
				noValue = value;
			}
		}

		public string customerNo
		{
			get
			{
				return customerNoValue;
			}
			set
			{
				customerNoValue = value;
			}
		}

		public string name
		{
			get
			{
				return nameValue;
			}
			set
			{
				nameValue = value;
			}
		}

		public string address
		{
			get
			{
				return addressValue;
			}
			set
			{
				addressValue = value;
			}
		}

		public string address2
		{
			get
			{
				return address2Value;
			}
			set
			{
				address2Value = value;
			}
		}

		public string zipCode
		{
			get
			{
				return zipCodeValue;
			}
			set
			{
				zipCodeValue = value;
			}
		}

		public string city
		{
			get
			{
				return cityValue;
			}
			set
			{
				cityValue = value;
			}
		}

		public string deliveryCode
		{
			get
			{
				return deliveryCodeValue;
			}
			set
			{
				deliveryCodeValue = value;
			}
		}

		public string deliveryName
		{
			get
			{
				return deliveryNameValue;
			}
			set
			{
				deliveryNameValue = value;
			}
		}

		public string deliveryAddress
		{
			get
			{
				return deliveryAddressValue;
			}
			set
			{
				deliveryAddressValue = value;
			}
		}

		public string deliveryAddress2
		{
			get
			{
				return deliveryAddress2Value;
			}
			set
			{
				deliveryAddress2Value = value;
			}
		}

		public string deliveryZipCode
		{
			get
			{
				return deliveryZipCodeValue;
			}
			set
			{
				deliveryZipCodeValue = value;
			}
		}

		public string deliveryCity
		{
			get
			{
				return deliveryCityValue;
			}
			set
			{
				deliveryCityValue = value;
			}
		}

		public string deliveryContact
		{
			get
			{
				return deliveryContactValue;
			}
			set
			{
				deliveryContactValue = value;
			}
		}

		public string contact
		{
			get
			{
				return contactValue;
			}
			set
			{
				contactValue = value;
			}
		}

		public string phoneNo
		{
			get
			{
				return phoneValue;
			}
			set
			{
				phoneValue = value;
			}
		}

		public bool ready
		{
			get
			{
				if (readyValue == 1) return true;
				return false;
			}
			set
			{
				if (value == true) 
					readyValue = 1;
				else
					readyValue = 0;
			}
		}

		public void getNextNo()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no FROM salesHeader ORDER BY no DESC");

			int nextNo = 1;

			if (dataReader.Read())
			{
				try
				{
					nextNo = (int)dataReader.GetValue(0);
					nextNo++;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();		
			noValue = nextNo;
		}

		public void save()
		{
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE no = '"+no+"'");

				if (!dataReader.Read())
				{
					smartDatabase.nonQuery("INSERT INTO salesHeader (customerNo, name, address, address2, zipCode, city, deliveryCode, deliveryName, deliveryAddress, deliveryAddress2, deliveryZipCode, deliveryCity, deliveryContact, contact, phoneNo, ready) VALUES ('"+customerNo+"', '"+name+"', '"+address+"', '"+address2+"', '"+zipCode+"', '"+city+"', '"+deliveryCode+"', '"+deliveryName+"', '"+deliveryAddress+"', '"+deliveryAddress2+"', '"+deliveryZipCode+"', '"+deliveryCity+"', '"+deliveryContact+"', '"+contactValue+"', '"+phoneValue+"', '"+readyValue+"')");
				}
				else
				{
					smartDatabase.nonQuery("UPDATE salesHeader SET customerNo = '"+customerNo+"', name = '"+name+"', address = '"+address+"', address2 = '"+address2+"', zipCode = '"+zipCode+"', city = '"+city+"', deliveryCode = '"+deliveryCode+"', deliveryName = '"+deliveryName+"', deliveryAddress = '"+deliveryAddress+"', deliveryAddress2 = '"+deliveryAddress2+"', deliveryZipCode = '"+deliveryZipCode+"', deliveryCity = '"+deliveryCity+"', deliveryContact = '"+deliveryContact+"', contact = '"+contact+"', phoneNo = '"+phoneNo+"', ready = '"+readyValue+"' WHERE no = '"+no+"'");
					dataReader.Close();
				}
				dataReader.Dispose();

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

		public bool getFromDb()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.customerNo = (string)dataReader.GetValue(1);
					this.name = (string)dataReader.GetValue(2);
					this.address = (string)dataReader.GetValue(3);
					this.address2 = (string)dataReader.GetValue(4);
					this.zipCode = (string)dataReader.GetValue(5);
					this.city = (string)dataReader.GetValue(6);
					this.deliveryCode = (string)dataReader.GetValue(7);
					this.deliveryName = (string)dataReader.GetValue(8);
					this.deliveryAddress = (string)dataReader.GetValue(9);
					this.deliveryAddress2 = (string)dataReader.GetValue(10);
					this.deliveryZipCode = (string)dataReader.GetValue(11);
					this.deliveryCity = (string)dataReader.GetValue(12);
					this.deliveryContact = (string)dataReader.GetValue(13);
					if (dataReader.GetValue(14) != null)
					{
						//System.Windows.Forms.MessageBox.Show((string)dataReader.GetValue(14));
						this.readyValue = (int)dataReader.GetValue(14);
					}
					else
					{
						this.readyValue = 0;
					}
					this.contact = (string)dataReader.GetValue(15);
					this.phoneNo = (string)dataReader.GetValue(16);

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


	}
}
