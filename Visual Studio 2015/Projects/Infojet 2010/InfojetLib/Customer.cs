using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Customer : ServiceArgument
	{
		private Database database;

		private string _no;
        private string _name;
        private string _name2;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;

        private string _currencyCode;
        private string _customerPriceGroup;
        private string _customerDiscGroup;

        private string _locationCode;
        private string _shippingAgentCode;
        private string _shipmentMethodCode;
        private string _shippingAgentServiceCode;

        private string _contactName;
        private string _phoneNo;
        private string _email;

        private string _vatBusPostingGroup;

		public Customer(Database database, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this._no = no;

			getFromDatabase();
		}

        public Customer(Database database, DataRow dataRow)
        {
            _no = dataRow.ItemArray.GetValue(0).ToString();
            _name = dataRow.ItemArray.GetValue(1).ToString();
            _name2 = dataRow.ItemArray.GetValue(2).ToString();
            _address = dataRow.ItemArray.GetValue(3).ToString();
            _address2 = dataRow.ItemArray.GetValue(4).ToString();
            _postCode = dataRow.ItemArray.GetValue(5).ToString();
            _city = dataRow.ItemArray.GetValue(6).ToString();
            _currencyCode = dataRow.ItemArray.GetValue(7).ToString();
            _customerPriceGroup = dataRow.ItemArray.GetValue(8).ToString();
            _countryCode = dataRow.ItemArray.GetValue(9).ToString();
            _locationCode = dataRow.ItemArray.GetValue(10).ToString();
            _customerDiscGroup = dataRow.ItemArray.GetValue(11).ToString();
            _shippingAgentCode = dataRow.ItemArray.GetValue(12).ToString();
            _shipmentMethodCode = dataRow.ItemArray.GetValue(13).ToString();
            _shippingAgentServiceCode = dataRow.ItemArray.GetValue(14).ToString();
            _contactName = dataRow.ItemArray.GetValue(15).ToString();
            _phoneNo = dataRow.ItemArray.GetValue(16).ToString();
            _email = dataRow.ItemArray.GetValue(17).ToString();
            _vatBusPostingGroup = dataRow.ItemArray.GetValue(18).ToString();


        }

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Currency Code], [Customer Price Group], [Country_Region Code], [Location Code], [Customer Disc_ Group], [Shipping Agent Code], [Shipment Method Code], [Shipping Agent Service Code], [Contact], [Phone No_], [E-Mail], [VAT Bus_ Posting Group] FROM [" + database.getTableName("Customer") + "] WHERE [No_] = @no");
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

			if (dataReader.Read())
			{

				_no = dataReader.GetValue(0).ToString();
				_name = dataReader.GetValue(1).ToString();
				_name2 = dataReader.GetValue(2).ToString();
				_address = dataReader.GetValue(3).ToString();
				_address2 = dataReader.GetValue(4).ToString();
				_postCode = dataReader.GetValue(5).ToString();
				_city = dataReader.GetValue(6).ToString();
				_currencyCode = dataReader.GetValue(7).ToString();
				_customerPriceGroup = dataReader.GetValue(8).ToString();
				_countryCode = dataReader.GetValue(9).ToString();
				_locationCode = dataReader.GetValue(10).ToString();
				_customerDiscGroup = dataReader.GetValue(11).ToString();
				_shippingAgentCode = dataReader.GetValue(12).ToString();
				_shipmentMethodCode = dataReader.GetValue(13).ToString();
				_shippingAgentServiceCode = dataReader.GetValue(14).ToString();
				_contactName = dataReader.GetValue(15).ToString();
				_phoneNo = dataReader.GetValue(16).ToString();
				_email = dataReader.GetValue(17).ToString();
                _vatBusPostingGroup = dataReader.GetValue(18).ToString();
			}

			dataReader.Close();


		}

		public void refresh()
		{
			getFromDatabase();
		}

        public string no { get { return _no; } }
        public string name { get { return _name; } }
        public string name2 { get { return _name2; } }
        public string address { get { return _address; } }
        public string address2 { get { return _address2; } }
        public string postCode { get { return _postCode; } }
        public string city { get { return _city; } }
        public string currencyCode { get { return _currencyCode; } }
        public string customerPriceGroup { get { return _customerPriceGroup; } }
        public string countryCode { get { return _countryCode; } }
        public string locationCode { get { return _locationCode; } }
        public string customerDiscGroup { get { return _customerDiscGroup; } }
        public string shippingAgentCode { get { return _shippingAgentCode; } }
        public string shipmentMethodCode { get { return _shipmentMethodCode; } }
        public string shippingAgentServiceCode { get { return _shippingAgentServiceCode; } }
        public string contactName { get { return _contactName; } }
        public string phoneNo { get { return _phoneNo; } }
        public string email { get { return _email; } }
        public string vatBusPostingGroup { get { return _vatBusPostingGroup; } }

		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add Customer.toDOM implementation
			XmlElement xmlCustomerElement = xmlDoc.CreateElement("customer");
			XmlAttribute noAttribute = xmlDoc.CreateAttribute("no");
			noAttribute.Value = no;
			xmlCustomerElement.Attributes.Append(noAttribute);

			return xmlCustomerElement;
		}

		#endregion
	}
}
