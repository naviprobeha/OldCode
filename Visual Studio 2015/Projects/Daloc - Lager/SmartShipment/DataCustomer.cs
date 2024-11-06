using System;
using System.Xml;
using System.Data.SqlServerCe;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataCustomer : ServiceArgument
	{	
		private string noValue;
		private string nameValue;
		private string addressValue;
		private string address2Value;
		private string zipCodeValue;
		private string cityValue;
		private string priceGroupCodeValue;
		private string customerDiscountGroupValue;
		private int blockedValue;
		private string contactValue;
		private string phoneNoValue;
	
		private string updateMethod;
		private SmartDatabase smartDatabase;
		
		public DataCustomer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataCustomer(XmlElement customerElement, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(customerElement);				
			if (updateDb) commit();
		}

		public DataCustomer(string no)
		{
			noValue = no;
		}

		public DataCustomer(string no, SmartDatabase smartDatabase)
		{
			noValue = no;
			this.smartDatabase = smartDatabase;
			getFromDb();
		}

		public string no
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

		public string priceGroupCode
		{
			get
			{
				return priceGroupCodeValue;
			}
			set
			{
				priceGroupCodeValue = value;
			}
		}

		public int blocked
		{
			get
			{
				return blockedValue;
			}
			set
			{
				blockedValue = value;
			}
		}

		public string customerDiscountGroup
		{
			get
			{
				return customerDiscountGroupValue;
			}
			set
			{
				customerDiscountGroupValue = value;
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
				return phoneNoValue;
			}
			set
			{
				phoneNoValue = value;
			}
		}

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			if (this.updateMethod.Equals("D"))
			{
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				noValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) noValue = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("2")) nameValue = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("5")) addressValue = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("6")) address2Value = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("91")) zipCodeValue = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("7")) cityValue = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("23")) priceGroupCodeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("34")) customerDiscountGroupValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("8")) contactValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("9")) phoneNoValue = fieldValue;

				if (fieldNo.FirstChild.Value.Equals("39")) 
				{
					if (fieldValue == "TRUE") blockedValue = 1;
				}
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM customer WHERE no = '"+noValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("D"))
				{
					smartDatabase.nonQuery("DELETE FROM customer WHERE no = '"+noValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE customer SET name = '"+nameValue+"', address = '"+addressValue+"', address2 = '"+address2Value+"', zipCode = '"+zipCodeValue+"', city = '"+cityValue+"', priceGroupCode = '"+priceGroupCodeValue+"', blocked = '"+blockedValue+"', customerDiscountGroup = '"+customerDiscountGroupValue+"', contact = '"+contactValue+"', phoneNo = '"+phoneNoValue+"' WHERE no = '"+noValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO customer (no, name, address, address2, zipCode, city, priceGroupCode, blocked, customerDiscountGroup, contact, phoneNo) VALUES ('"+noValue+"','"+nameValue+"','"+addressValue+"','"+address2Value+"','"+zipCodeValue+"','"+cityValue+"','"+priceGroupCodeValue+"','"+blockedValue+"','"+customerDiscountGroupValue+"','"+contactValue+"','"+phoneNoValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM customer WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.nameValue = (string)dataReader.GetValue(1);
					this.addressValue = (string)dataReader.GetValue(2);
					this.address2Value = (string)dataReader.GetValue(3);
					this.zipCodeValue = (string)dataReader.GetValue(4);
					this.city = (string)dataReader.GetValue(5);
					this.priceGroupCodeValue = (string)dataReader.GetValue(6);
					this.customerDiscountGroupValue = (string)dataReader.GetValue(8);
					this.contactValue = (string)dataReader.GetValue(10);
					this.phoneNoValue = (string)dataReader.GetValue(11);
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
		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataItem.toDOM implementation
			XmlElement topElement = xmlDocumentContext.CreateElement("CUSTOMER");
			
			XmlElement noElement = xmlDocumentContext.CreateElement("NO");
			noElement.AppendChild(xmlDocumentContext.CreateTextNode(no));

			XmlElement nameElement = xmlDocumentContext.CreateElement("NAME");
			nameElement.AppendChild(xmlDocumentContext.CreateTextNode(name));

			topElement.AppendChild(noElement);
			topElement.AppendChild(nameElement);
			
			return topElement;
		}

		#endregion
	}
}
