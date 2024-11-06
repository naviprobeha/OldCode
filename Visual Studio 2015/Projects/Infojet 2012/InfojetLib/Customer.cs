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
		private Infojet infojetContext;

		private string _no = "";
        private string _name = "";
        private string _name2 = "";
        private string _address = "";
        private string _address2 = "";
        private string _postCode = "";
        private string _city = "";
        private string _countryCode = "";

        private string _currencyCode = "";
        private string _customerPriceGroup = "";
        private string _customerDiscGroup = "";

        private string _locationCode = "";
        private string _shippingAgentCode = "";
        private string _shipmentMethodCode = "";
        private string _shippingAgentServiceCode = "";

        private string _contactName = "";
        private string _phoneNo = "";
        private string _email = "";

        private string _vatBusPostingGroup = "";
        private float _creditLimitLcy;

        private string _salesPersonCode = "";
        private SalesPerson _salesPerson;

        private bool _pricesIncludingVAT = false;

        public Customer()
        {
        }

		public Customer(Infojet infojetContext, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
				 
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
            _creditLimitLcy = float.Parse(dataRow.ItemArray.GetValue(19).ToString());

        }

		private void getFromDatabase()
		{
            SqlDataReader dataReader = null;

            try
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Currency Code], [Customer Price Group], [Country_Region Code], [Location Code], [Customer Disc_ Group], [Shipping Agent Code], [Shipment Method Code], [Shipping Agent Service Code], [Contact], [Phone No_], [E-Mail], [VAT Bus_ Posting Group], [Credit Limit (LCY)], [Salesperson Code], [Prices Including VAT] FROM [" + infojetContext.systemDatabase.getTableName("Customer") + "] WHERE [No_] = @no");
                databaseQuery.addStringParameter("no", no, 20);
                dataReader = databaseQuery.executeQuery();
            }
            catch (Exception)
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Currency Code], [Customer Price Group], [Country Code], [Location Code], [Customer Disc_ Group], [Shipping Agent Code], [Shipment Method Code], [Shipping Agent Service Code], [Contact], [Phone No_], [E-Mail], [VAT Bus_ Posting Group], [Credit Limit (LCY)], [Salesperson Code], [Prices Including VAT] FROM [" + infojetContext.systemDatabase.getTableName("Customer") + "] WHERE [No_] = @no");
                databaseQuery.addStringParameter("no", no, 20);
                dataReader = databaseQuery.executeQuery();
            }

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
                _creditLimitLcy = float.Parse(dataReader.GetValue(19).ToString());
                _salesPersonCode = dataReader.GetValue(20).ToString();
               
                _pricesIncludingVAT = false;
                if (dataReader.GetValue(21).ToString() == "1") _pricesIncludingVAT = true;
			}

			dataReader.Close();


		}

		public void refresh()
		{
			getFromDatabase();
		}

        public float getCreditLimit()
        {
            float creditLimit = creditLimitLcy;

            if ((infojetContext.currencyCode != "") && (infojetContext.currencyCode != infojetContext.generalLedgerSetup.lcyCode))
            {
                CurrencyExchangeRates currencyExchangeRates = new CurrencyExchangeRates(infojetContext.systemDatabase);
                CurrencyExchangeRate currencyExchangeRate = currencyExchangeRates.getCurrentExchangeRate(infojetContext.currencyCode);

                if (currencyExchangeRate != null)
                {
                    creditLimit = (creditLimit / (currencyExchangeRate.relationalExchRateAmount / currencyExchangeRate.exchangeRateAmount));
                }
            }

            return creditLimit;
        }

        public float getVatFactor(string vatProdPostingGroup)
        {

            float vatFactor = 0;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT "+infojetContext.systemDatabase.convertField("[VAT %]")+" FROM [" + infojetContext.systemDatabase.getTableName("VAT Posting Setup") + "] p, [" + infojetContext.systemDatabase.getTableName("Customer") + "] c WHERE p.[VAT Bus_ Posting Group] = c.[VAT Bus_ Posting Group] AND c.[No_] = @customerNo AND p.[VAT Prod_ Posting Group] = @vatProductPostingGroup AND p.[VAT Calculation Type] = 0");
            databaseQuery.addStringParameter("customerNo", no, 20);
            databaseQuery.addStringParameter("vatProductPostingGroup", vatProdPostingGroup, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;


        }

        public void updateSalesPerson()
        {
            salesPerson = new SalesPerson(infojetContext, salesPersonCode);
        }

        public string no { get { return _no; } set { _no = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string name2 { get { return _name2; } set { _name2 = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string currencyCode { get { return _currencyCode; } set { _currencyCode = value; } }
        public string customerPriceGroup { get { return _customerPriceGroup; } set { _customerPriceGroup = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string locationCode { get { return _locationCode; } set { _locationCode = value; } }
        public string customerDiscGroup { get { return _customerDiscGroup; } set { _customerDiscGroup = value; } }
        public string shippingAgentCode { get { return _shippingAgentCode; } set { _shippingAgentCode = value; } }
        public string shipmentMethodCode { get { return _shipmentMethodCode; } set { _shipmentMethodCode = value; } }
        public string shippingAgentServiceCode { get { return _shippingAgentServiceCode; } set { _shippingAgentServiceCode = value; } }
        public string contactName { get { return _contactName; } set { _contactName = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string vatBusPostingGroup { get { return _vatBusPostingGroup; } set { _vatBusPostingGroup = value; } }
        public float creditLimitLcy { get { return _creditLimitLcy; } set { _creditLimitLcy = value; } }
        public string salesPersonCode { get { return _salesPersonCode; } set { _salesPersonCode = value; } }
        public SalesPerson salesPerson { get { return _salesPerson; } set { _salesPerson = value; } }
        public bool pricesIncludingVAT { get { return _pricesIncludingVAT; } set { _pricesIncludingVAT = value; } }

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
