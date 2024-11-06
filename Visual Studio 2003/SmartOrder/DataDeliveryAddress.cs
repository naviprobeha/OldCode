using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataDeliveryAddress.
	/// </summary>
	public class DataDeliveryAddress
	{
		private int noValue;
		private string customerNoValue;
		private string codeValue;
		private string nameValue;
		private string addressValue;
		private string address2Value;
		private string zipCodeValue;
		private string cityValue;
		private string contactValue;
		
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataDeliveryAddress()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataDeliveryAddress(XmlElement customerElement, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(customerElement);				
			if (updateDb) commit();
		}

		public DataDeliveryAddress(string customerNo, string code)
		{
			customerNoValue = customerNo;
			codeValue = code;
		}

		public DataDeliveryAddress(string customerNo, string code, SmartDatabase smartDatabase)
		{
			customerNoValue = customerNo;
			codeValue = code;
			this.smartDatabase = smartDatabase;
			getFromDb();
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


		public string code
		{
			get
			{
				return codeValue;
			}
			set
			{
				codeValue = value;
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

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("METHOD");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			if (this.updateMethod.Equals("DELETE"))
			{
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				customerNoValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				startPos = key.FirstChild.Value.IndexOf("(", startPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", endPos+1);
				codeValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("FIELD");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) customerNoValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("2")) codeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("3")) nameValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("5")) addressValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("6")) address2Value = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("7")) cityValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("8")) contactValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("91")) zipCodeValue = fieldValue;
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM deliveryAddress WHERE customerNo = '"+customerNoValue+"' AND code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("DELETE"))
				{
					smartDatabase.nonQuery("DELETE FROM deliveryAddress WHERE customerNo = '"+customerNoValue+"' AND code = '"+codeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE deliveryAddress SET name = '"+nameValue+"', address = '"+addressValue+"', address2 = '"+address2Value+"', zipCode = '"+zipCodeValue+"', city = '"+cityValue+"', contact = '"+contactValue+"' WHERE customerNo = '"+customerNoValue+"' AND code = '"+codeValue+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				getNextNo();
				try
				{
					smartDatabase.nonQuery("INSERT INTO deliveryAddress (no, customerNo, code, name, address, address2, zipCode, city, contact) VALUES ('"+noValue+"','"+customerNoValue+"','"+codeValue+"','"+nameValue+"','"+addressValue+"','"+address2Value+"','"+zipCodeValue+"','"+cityValue+"','"+contactValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM deliveryAddress WHERE customerNo = '"+customerNoValue+"' AND code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = (int)dataReader.GetValue(0);
					this.customerNo = (string)dataReader.GetValue(1);
					this.code = (string)dataReader.GetValue(2);
					this.name = (string)dataReader.GetValue(3);
					this.address = (string)dataReader.GetValue(4);
					this.address2 = (string)dataReader.GetValue(5);
					this.zipCode = (string)dataReader.GetValue(6);
					this.city = (string)dataReader.GetValue(7);
					this.contact = (string)dataReader.GetValue(8);

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

		public void getNextNo()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no FROM deliveryAddress ORDER BY no DESC");

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

	}
}
