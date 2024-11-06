using System;
using System.Xml;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataCustomer
	{	
		private string noValue;
		private string nameValue;
		private string addressValue;
		private string address2Value;
		private string zipCodeValue;
		private string cityValue;
		private string priceGroupCodeValue;
		private int blockedValue;
	
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
				if (fieldNo.FirstChild.Value.Equals("23")) priceGroupCodeValue = fieldValue.Replace("'", "");
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
						smartDatabase.nonQuery("UPDATE customer SET name = '"+nameValue+"', address = '"+addressValue+"', address2 = '"+address2Value+"', zipCode = '"+zipCodeValue+"', city = '"+cityValue+"', priceGroupCode = '"+priceGroupCodeValue+"', blocked = '"+blockedValue+"' WHERE no = '"+noValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO customer (no, name, address, address2, zipCode, city, priceGroupCode, blocked) VALUES ('"+noValue+"','"+nameValue+"','"+addressValue+"','"+address2Value+"','"+zipCodeValue+"','"+cityValue+"','"+priceGroupCodeValue+"','"+blockedValue+"')");
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
