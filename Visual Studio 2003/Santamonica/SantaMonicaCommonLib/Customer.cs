using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Customer.
	/// </summary>
	public class Customer
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
		public string email;

		public string productionSite;
		public string registrationNo;
		public string personNo;
		public string dairyCode;
		public string dairyNo;

		public bool hide;
		public int blocked;
		public bool forceCashPayment;

		public int positionX;
		public int positionY;

		public string directionComment;
		public string directionComment2;

		public string priceGroupCode;
		public string billToCustomerNo;
		public bool editable;

		public bool updated;
		public bool unverified;

		private string updateMethod;


		public Customer(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.no = dataReader.GetValue(1).ToString();
			this.name = dataReader.GetValue(2).ToString();
			this.address = dataReader.GetValue(3).ToString();
			this.address2 = dataReader.GetValue(4).ToString();
			this.postCode = dataReader.GetValue(5).ToString();
			this.city = dataReader.GetValue(6).ToString();
			this.countryCode = dataReader.GetValue(7).ToString();
			this.phoneNo = dataReader.GetValue(8).ToString();
			this.cellPhoneNo = dataReader.GetValue(9).ToString();
			this.faxNo = dataReader.GetValue(10).ToString();
			this.email = dataReader.GetValue(11).ToString();
			this.productionSite = dataReader.GetValue(12).ToString();
			this.registrationNo = dataReader.GetValue(13).ToString();
			this.personNo = dataReader.GetValue(14).ToString();

			this.positionX = dataReader.GetInt32(15);
			this.positionY = dataReader.GetInt32(16);

			this.contactName = dataReader.GetValue(17).ToString();
			this.dairyNo = dataReader.GetValue(18).ToString();
			this.dairyCode = dataReader.GetValue(19).ToString();

			this.hide = false;
			if (dataReader.GetValue(20).ToString() == "1") this.hide = true;

			this.billToCustomerNo = dataReader.GetValue(21).ToString();
			this.blocked = dataReader.GetInt32(22);

			this.forceCashPayment = false;
			if (dataReader.GetValue(23).ToString() == "1") this.forceCashPayment = true;

			this.directionComment = dataReader.GetValue(24).ToString();
			this.directionComment2 = dataReader.GetValue(25).ToString();

			this.priceGroupCode = dataReader.GetValue(26).ToString();

			this.editable = false;
			if (dataReader.GetValue(27).ToString() == "1") this.editable = true;

			this.updated = false;
			if (dataReader.GetValue(28).ToString() == "1") this.updated = true;

			this.unverified = false;
			if (dataReader.GetValue(29).ToString() == "1") this.unverified = true;

		}

		public Customer(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataRow.ItemArray.GetValue(0).ToString();
			this.no = dataRow.ItemArray.GetValue(1).ToString();
			this.name = dataRow.ItemArray.GetValue(2).ToString();
			this.address = dataRow.ItemArray.GetValue(3).ToString();
			this.address2 = dataRow.ItemArray.GetValue(4).ToString();
			this.postCode = dataRow.ItemArray.GetValue(5).ToString();
			this.city = dataRow.ItemArray.GetValue(6).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(7).ToString();
			this.phoneNo = dataRow.ItemArray.GetValue(8).ToString();
			this.cellPhoneNo = dataRow.ItemArray.GetValue(9).ToString();
			this.faxNo = dataRow.ItemArray.GetValue(10).ToString();
			this.email = dataRow.ItemArray.GetValue(11).ToString();
			this.productionSite = dataRow.ItemArray.GetValue(12).ToString();
			this.registrationNo = dataRow.ItemArray.GetValue(13).ToString();
			this.personNo = dataRow.ItemArray.GetValue(14).ToString();

			this.positionX = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(16).ToString());

			this.contactName = dataRow.ItemArray.GetValue(17).ToString();
			this.dairyNo = dataRow.ItemArray.GetValue(18).ToString();
			this.dairyCode = dataRow.ItemArray.GetValue(19).ToString();

			this.hide = false;
			if (dataRow.ItemArray.GetValue(20).ToString() == "1") this.hide = true;

			this.billToCustomerNo = dataRow.ItemArray.GetValue(21).ToString();
			this.blocked = int.Parse(dataRow.ItemArray.GetValue(22).ToString());

			this.forceCashPayment = false;
			if (dataRow.ItemArray.GetValue(23).ToString() == "1") this.forceCashPayment = true;

			this.directionComment = dataRow.ItemArray.GetValue(24).ToString();
			this.directionComment2 = dataRow.ItemArray.GetValue(25).ToString();

			this.priceGroupCode = dataRow.ItemArray.GetValue(26).ToString();

			this.editable = false;
			if (dataRow.ItemArray.GetValue(27).ToString() == "1") this.editable = true;

			this.updated = false;
			if (dataRow.ItemArray.GetValue(28).ToString() == "1") this.updated = true;

			this.unverified = false;
			if (dataRow.ItemArray.GetValue(29).ToString() == "1") this.unverified = true;

		}

		public Customer(XmlElement element, Organization organization, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			organizationNo = organization.no;

			name = "";
			address = "";
			address2 = "";
			postCode = "";
			city = "";
			contactName = "";

			countryCode = "";
			phoneNo = "";
			cellPhoneNo = "";
			faxNo = "";
			email = "";

			productionSite = "";
			registrationNo = "";
			dairyCode = "";
			dairyNo = "";
			priceGroupCode = "";
			billToCustomerNo = "";
			blocked = 0;

			hide = false;
			forceCashPayment = false;

			fromDOM(element);
			if (saveValues) save(database);

		}

		public Customer()
		{
			this.name = "";
			this.address = "";
			this.address2 = "";
			this.postCode = "";
			this.city = "";
			this.phoneNo = "";			
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
				if (fieldNo.FirstChild.Value.Equals("70000")) dairyCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("70001")) dairyNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("23")) priceGroupCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("45")) billToCustomerNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("39")) blocked = int.Parse(fieldValue.Replace("'", ""));

				if (fieldNo.FirstChild.Value.Equals("70005")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.hide = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70006")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.forceCashPayment = true;
					}
				}

				i++;
			}

		}

		public void save(Database database)
		{
			if (phoneNo.Length > 20) phoneNo = phoneNo.Substring(1, 20);
			int hideVal = 0;
			if (this.hide == true) hideVal = 1;
			int forceCashVal = 0;
			if (this.forceCashPayment == true) forceCashVal = 1;
			int editableVal = 0;
			if (this.editable == true) editableVal = 1;
			int updatedVal = 0;
			if (this.updated == true) updatedVal = 1;
			int unverifiedVal = 0;
			if (this.unverified == true) unverifiedVal = 1;

			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Customer] WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");
					synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, 1, this.no, 2);

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [No] FROM Customer WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Customer] SET [Name] = '"+name+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Cell Phone No] = '"+cellPhoneNo+"', [Fax No] = '"+faxNo+"', [E-mail] = '"+email+"', [Production Site] = '"+productionSite+"', [Registration No] = '"+registrationNo+"', [Person No] = '"+personNo+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Contact Name] = '"+contactName+"', [Dairy No] = '"+dairyNo+"', [Price Group Code] = '"+priceGroupCode+"', [Dairy Code] = '"+dairyCode+"', [Hide] = '"+hideVal+"', [Bill-to Customer No] = '"+this.billToCustomerNo+"', [Blocked] = '"+this.blocked+"', [Force Cash Payment] = '"+forceCashVal+"', [Direction Comment] = '"+this.directionComment+"', [Direction Comment 2] = '"+this.directionComment2+"', [Editable] = '"+editableVal+"', [Updated] = '"+updatedVal+"', [Unverified] = '"+unverifiedVal+"' WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Customer] ([Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Production Site], [Registration No], [Person No], [Position X], [Position Y], [Contact Name], [Dairy No], [Price Group Code], [Dairy Code], [Hide], [Bill-to Customer No], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Editable], [Updated], [Unverified]) VALUES ('"+organizationNo+"','"+no+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+cellPhoneNo+"','"+faxNo+"','"+email+"','"+productionSite+"','"+registrationNo+"','"+personNo+"','"+positionX+"','"+positionY+"','"+contactName+"','"+dairyNo+"','"+priceGroupCode+"','"+dairyCode+"','"+hideVal+"','"+billToCustomerNo+"','"+blocked+"','"+forceCashVal+"', '"+directionComment+"', '"+directionComment2+"', '"+editableVal+"', '"+updatedVal+"', '"+unverifiedVal+"')");
					}
					synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, 1, this.no, 0);

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on customer update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public bool sameAs(Customer compareCustomer)
		{
			if (compareCustomer.name != name) return false;
			if (compareCustomer.address != address) return false;
			if (compareCustomer.address2 != address2) return false;
			if (compareCustomer.postCode != postCode) return false;
			if (compareCustomer.city != city) return false;
			if (compareCustomer.contactName != contactName) return false;

			if (compareCustomer.countryCode != countryCode) return false;
			if (compareCustomer.phoneNo != phoneNo) return false;
			if (compareCustomer.cellPhoneNo != cellPhoneNo) return false;
			if (compareCustomer.faxNo != faxNo) return false;
			if (compareCustomer.email != email) return false;

			if (compareCustomer.productionSite != productionSite) return false;
			if (compareCustomer.registrationNo != registrationNo) return false;
			if (compareCustomer.dairyCode != dairyCode) return false;
			if (compareCustomer.dairyNo != dairyNo) return false;
			if (compareCustomer.priceGroupCode != priceGroupCode) return false;
			if (compareCustomer.billToCustomerNo != billToCustomerNo) return false;
			if (compareCustomer.blocked != blocked) return false;

			if (compareCustomer.hide != hide) return false;
			if (compareCustomer.forceCashPayment != forceCashPayment) return false;

			return true;
		}

		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			XmlElement customerElement = xmlDoc.CreateElement("CUSTOMER");

			XmlElement noElement = xmlDoc.CreateElement("NO");
			noElement.AppendChild(xmlDoc.CreateTextNode(this.no));
			customerElement.AppendChild(noElement);

			XmlElement organizationNoElement = xmlDoc.CreateElement("ORGANIZATION_NO");
			organizationNoElement.AppendChild(xmlDoc.CreateTextNode(this.organizationNo));
			customerElement.AppendChild(organizationNoElement);

			XmlElement productionSiteElement = xmlDoc.CreateElement("PRODUCTION_SITE");
			productionSiteElement.AppendChild(xmlDoc.CreateTextNode(this.productionSite));
			customerElement.AppendChild(productionSiteElement);

			XmlElement phoneNoElement = xmlDoc.CreateElement("PHONE_NO");
			phoneNoElement.AppendChild(xmlDoc.CreateTextNode(this.phoneNo));
			customerElement.AppendChild(phoneNoElement);

			XmlElement cellPhoneNoElement = xmlDoc.CreateElement("CELL_PHONE_NO");
			cellPhoneNoElement.AppendChild(xmlDoc.CreateTextNode(this.cellPhoneNo));
			customerElement.AppendChild(cellPhoneNoElement);

			XmlElement dairyCodeElement = xmlDoc.CreateElement("DAIRY_CODE");
			dairyCodeElement.AppendChild(xmlDoc.CreateTextNode(this.dairyCode));
			customerElement.AppendChild(dairyCodeElement);

			XmlElement dairyNoElement = xmlDoc.CreateElement("DAIRY_NO");
			dairyNoElement.AppendChild(xmlDoc.CreateTextNode(this.dairyNo));
			customerElement.AppendChild(dairyNoElement);

			return customerElement;


		}

		public void setUpdated()
		{
			this.updated = true;
		}


	}
}
