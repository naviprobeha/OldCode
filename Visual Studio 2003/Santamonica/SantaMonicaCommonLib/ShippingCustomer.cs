using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShippingCustomer.
	/// </summary>
	public class ShippingCustomer
	{
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
		public string email;

		public string productionSite;
		public string registrationNo;

		public bool hide;
		public int blocked;

		public int positionX;
		public int positionY;

		public string directionComment;
		public string directionComment2;

		public string priceGroupCode;
		public string routeGroupCode;
		public int priority;

		public string preferedFactoryNo;

		public string reasonCode;

		private string updateMethod;

		public ShippingCustomer(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.address = dataReader.GetValue(2).ToString();
			this.address2 = dataReader.GetValue(3).ToString();
			this.postCode = dataReader.GetValue(4).ToString();
			this.city = dataReader.GetValue(5).ToString();
			this.countryCode = dataReader.GetValue(6).ToString();
			this.phoneNo = dataReader.GetValue(7).ToString();
			this.cellPhoneNo = dataReader.GetValue(8).ToString();
			this.faxNo = dataReader.GetValue(9).ToString();
			this.email = dataReader.GetValue(10).ToString();
			this.priceGroupCode = dataReader.GetValue(11).ToString();
			this.productionSite = dataReader.GetValue(12).ToString();
			this.registrationNo = dataReader.GetValue(13).ToString();

			this.contactName = dataReader.GetValue(14).ToString();

			this.hide = false;
			if (dataReader.GetValue(15).ToString() == "1") this.hide = true;

			this.blocked = dataReader.GetInt32(16);

			this.directionComment = dataReader.GetValue(17).ToString();
			this.directionComment2 = dataReader.GetValue(18).ToString();

			this.positionX = dataReader.GetInt32(19);
			this.positionY = dataReader.GetInt32(20);

			this.routeGroupCode = dataReader.GetValue(21).ToString();
			this.priority = dataReader.GetInt32(22);

			this.preferedFactoryNo = dataReader.GetValue(23).ToString();

			this.reasonCode = dataReader.GetValue(24).ToString();
		}

		public ShippingCustomer(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataRow.ItemArray.GetValue(0).ToString();
			this.name = dataRow.ItemArray.GetValue(1).ToString();
			this.address = dataRow.ItemArray.GetValue(2).ToString();
			this.address2 = dataRow.ItemArray.GetValue(3).ToString();
			this.postCode = dataRow.ItemArray.GetValue(4).ToString();
			this.city = dataRow.ItemArray.GetValue(5).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(6).ToString();
			this.phoneNo = dataRow.ItemArray.GetValue(7).ToString();
			this.cellPhoneNo = dataRow.ItemArray.GetValue(8).ToString();
			this.faxNo = dataRow.ItemArray.GetValue(9).ToString();
			this.email = dataRow.ItemArray.GetValue(10).ToString();
			this.priceGroupCode = dataRow.ItemArray.GetValue(11).ToString();
			this.productionSite = dataRow.ItemArray.GetValue(12).ToString();
			this.registrationNo = dataRow.ItemArray.GetValue(13).ToString();

			this.contactName = dataRow.ItemArray.GetValue(14).ToString();

			this.hide = false;
			if (dataRow.ItemArray.GetValue(15).ToString() == "1") this.hide = true;

			this.blocked = int.Parse(dataRow.ItemArray.GetValue(16).ToString());

			this.directionComment = dataRow.ItemArray.GetValue(17).ToString();
			this.directionComment2 = dataRow.ItemArray.GetValue(18).ToString();

			this.positionX = int.Parse(dataRow.ItemArray.GetValue(19).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(20).ToString());

			this.routeGroupCode = dataRow.ItemArray.GetValue(21).ToString();
			this.priority = int.Parse(dataRow.ItemArray.GetValue(22).ToString());

			this.preferedFactoryNo = dataRow.ItemArray.GetValue(23).ToString();

			this.reasonCode = dataRow.ItemArray.GetValue(24).ToString();
		}


		public ShippingCustomer(XmlElement element, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(element);
			if (saveValues) save(database);

		}

		public ShippingCustomer()
		{
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
				no = key.FirstChild.Value.Substring(startPos,endPos-startPos); 
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) no = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("2")) name = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("5")) address = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("6")) address2 = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("91")) postCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("7")) city = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("8")) contactName = fieldValue.Replace("'", "");

				if (fieldNo.FirstChild.Value.Equals("35")) countryCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("9")) phoneNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("70004")) cellPhoneNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("84")) faxNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("102")) email = fieldValue.Replace("'", "");

				if (fieldNo.FirstChild.Value.Equals("70002")) productionSite = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("86")) registrationNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("23")) priceGroupCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("39")) blocked = int.Parse(fieldValue.Replace("'", ""));

				if (fieldNo.FirstChild.Value.Equals("70005")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.hide = true;
					}
				}

				i++;
			}

		}

		public void save(Database database)
		{
			int hideVal = 0;
			if (this.hide == true) hideVal = 1;

			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Shipping Customer] WHERE [No] = '"+no+"'");

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [No] FROM [Shipping Customer] WHERE [No] = '"+no+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Shipping Customer] SET [Name] = '"+name+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Cell Phone No] = '"+cellPhoneNo+"', [Fax No] = '"+faxNo+"', [E-mail] = '"+email+"', [Production Site] = '"+productionSite+"', [Registration No] = '"+registrationNo+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Contact Name] = '"+contactName+"', [Price Group Code] = '"+priceGroupCode+"', [Hide] = '"+hideVal+"', [Blocked] = '"+this.blocked+"', [Direction Comment] = '"+this.directionComment+"', [Direction Comment 2] = '"+this.directionComment2+"', [Route Group Code] = '"+this.routeGroupCode+"', [Priority] = '"+priority+"', [Prefered Factory No] = '"+preferedFactoryNo+"', [Reason Code] = '"+reasonCode+"' WHERE [No] = '"+this.no+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Shipping Customer] ([No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No],  [Position X], [Position Y], [Contact Name], [Price Group Code], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code]) VALUES ('"+no+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+cellPhoneNo+"','"+faxNo+"','"+email+"','"+productionSite+"','"+registrationNo+"','"+positionX+"','"+positionY+"','"+contactName+"','"+priceGroupCode+"','"+hideVal+"','"+blocked+"', '"+directionComment+"', '"+directionComment2+"', '"+routeGroupCode+"', '"+priority+"', '"+preferedFactoryNo+"', '"+reasonCode+"')");
					}
				

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on shippingCustomer update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public bool sameAs(ShippingCustomer compareShippingCustomer)
		{
			if (compareShippingCustomer.name != name) return false;
			if (compareShippingCustomer.address != address) return false;
			if (compareShippingCustomer.address2 != address2) return false;
			if (compareShippingCustomer.postCode != postCode) return false;
			if (compareShippingCustomer.city != city) return false;
			if (compareShippingCustomer.contactName != contactName) return false;

			if (compareShippingCustomer.countryCode != countryCode) return false;
			if (compareShippingCustomer.phoneNo != phoneNo) return false;
			if (compareShippingCustomer.cellPhoneNo != cellPhoneNo) return false;
			if (compareShippingCustomer.faxNo != faxNo) return false;
			if (compareShippingCustomer.email != email) return false;

			if (compareShippingCustomer.productionSite != productionSite) return false;
			if (compareShippingCustomer.registrationNo != registrationNo) return false;
			if (compareShippingCustomer.priceGroupCode != priceGroupCode) return false;
			if (compareShippingCustomer.blocked != blocked) return false;

			if (compareShippingCustomer.hide != hide) return false;

			return true;
		}

		public DataSet getTransportingOrganizations(Database database, int orderType)
		{
			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			return shippingCustomerOrganizations.getShippingCustomerDataSet(database, this.no, orderType);
		}

		public DataSet getUsers(Database database)
		{
			ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
			return shippingCustomerUsers.getDataSet(database, this.no);
		}

		public DataSet getSchedules(Database database)
		{
			ShippingCustomerSchedules shippingCustomerSchedules = new ShippingCustomerSchedules();
			return shippingCustomerSchedules.getShippingCustomerDataSet(database, this.no);
		}

		public string getPreferedFactoryName(Database database)
		{
			Factories factories = new Factories();
			Factory factory = factories.getEntry(database, this.preferedFactoryNo);
			if (factory != null) return factory.no+", "+factory.name;
			return "";
		}
	}
}
