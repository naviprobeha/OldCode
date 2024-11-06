using System;
using System.Xml;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for NewUser.
    /// </summary>
    public class UserProfile : ServiceArgument
    {
        public int userType;
        public string no = "";
        public string name = "";
        public string name2 = "";
        public string address = "";
        public string address2 = "";
        public string postCode = "";
        public string city = "";
        public string countryCode = "";
        public string phoneNo = "";
        public string email = "";
        public string password = "";
        public string registrationNo = "";
        public string personNo = "";

        public string invoiceName = "";
        public string invoiceName2 = "";
        public string invoiceAddress = "";
        public string invoiceAddress2 = "";
        public string invoicePostCode = "";
        public string invoiceCity = "";
        public string invoiceCountryCode = "";


        public string webLoginCode = "";

        private Hashtable extraFieldsTable;

        public UserProfile()
        {
            //
            // TODO: Add constructor logic here
            //
            extraFieldsTable = new Hashtable();
        }

        public void addField(string fieldCode, string fieldValue)
        {
            extraFieldsTable.Add(fieldCode, fieldValue);
        }

        public XmlElement toDOM(XmlDocument xmlDoc)
        {
            XmlElement userElement = xmlDoc.CreateElement("user");

            XmlElement webLoginCodeElement = xmlDoc.CreateElement("webLoginCode");
            webLoginCodeElement.AppendChild(xmlDoc.CreateTextNode(webLoginCode));
            userElement.AppendChild(webLoginCodeElement);

            XmlElement userTypeElement = xmlDoc.CreateElement("userType");
            userTypeElement.AppendChild(xmlDoc.CreateTextNode(userType.ToString()));
            userElement.AppendChild(userTypeElement);

            XmlElement noElement = xmlDoc.CreateElement("no");
            noElement.AppendChild(xmlDoc.CreateTextNode(no));
            userElement.AppendChild(noElement);

            XmlElement nameElement = xmlDoc.CreateElement("name");
            nameElement.AppendChild(xmlDoc.CreateTextNode(name));
            userElement.AppendChild(nameElement);

            XmlElement name2Element = xmlDoc.CreateElement("name2");
            name2Element.AppendChild(xmlDoc.CreateTextNode(name2));
            userElement.AppendChild(name2Element);

            XmlElement addressElement = xmlDoc.CreateElement("address");
            addressElement.AppendChild(xmlDoc.CreateTextNode(address));
            userElement.AppendChild(addressElement);

            XmlElement address2Element = xmlDoc.CreateElement("address2");
            address2Element.AppendChild(xmlDoc.CreateTextNode(address2));
            userElement.AppendChild(address2Element);

            XmlElement postCodeElement = xmlDoc.CreateElement("postCode");
            postCodeElement.AppendChild(xmlDoc.CreateTextNode(postCode));
            userElement.AppendChild(postCodeElement);

            XmlElement cityElement = xmlDoc.CreateElement("city");
            cityElement.AppendChild(xmlDoc.CreateTextNode(city));
            userElement.AppendChild(cityElement);

            XmlElement countryCodeElement = xmlDoc.CreateElement("countryCode");
            countryCodeElement.AppendChild(xmlDoc.CreateTextNode(countryCode));
            userElement.AppendChild(countryCodeElement);

            XmlElement invoiceNameElement = xmlDoc.CreateElement("invoiceName");
            invoiceNameElement.AppendChild(xmlDoc.CreateTextNode(invoiceName));
            userElement.AppendChild(invoiceNameElement);

            XmlElement invoiceName2Element = xmlDoc.CreateElement("invoiceName2");
            invoiceName2Element.AppendChild(xmlDoc.CreateTextNode(invoiceName2));
            userElement.AppendChild(invoiceName2Element);

            XmlElement invoiceAddressElement = xmlDoc.CreateElement("invoiceAddress");
            invoiceAddressElement.AppendChild(xmlDoc.CreateTextNode(invoiceAddress));
            userElement.AppendChild(invoiceAddressElement);

            XmlElement invoiceAddress2Element = xmlDoc.CreateElement("invoiceAddress2");
            invoiceAddress2Element.AppendChild(xmlDoc.CreateTextNode(invoiceAddress2));
            userElement.AppendChild(invoiceAddress2Element);

            XmlElement invoicePostCodeElement = xmlDoc.CreateElement("invoicePostCode");
            invoicePostCodeElement.AppendChild(xmlDoc.CreateTextNode(invoicePostCode));
            userElement.AppendChild(invoicePostCodeElement);

            XmlElement invoiceCityElement = xmlDoc.CreateElement("invoiceCity");
            invoiceCityElement.AppendChild(xmlDoc.CreateTextNode(invoiceCity));
            userElement.AppendChild(invoiceCityElement);

            XmlElement invoiceCountryCodeElement = xmlDoc.CreateElement("invoiceCountryCode");
            invoiceCountryCodeElement.AppendChild(xmlDoc.CreateTextNode(invoiceCountryCode));
            userElement.AppendChild(invoiceCountryCodeElement);

            XmlElement phoneNoElement = xmlDoc.CreateElement("phoneNo");
            phoneNoElement.AppendChild(xmlDoc.CreateTextNode(phoneNo));
            userElement.AppendChild(phoneNoElement);

            XmlElement registrationNoElement = xmlDoc.CreateElement("registrationNo");
            registrationNoElement.AppendChild(xmlDoc.CreateTextNode(registrationNo));
            userElement.AppendChild(registrationNoElement);

            XmlElement personNoElement = xmlDoc.CreateElement("personNo");
            personNoElement.AppendChild(xmlDoc.CreateTextNode(personNo));
            userElement.AppendChild(personNoElement);

            XmlElement emailElement = xmlDoc.CreateElement("email");
            emailElement.AppendChild(xmlDoc.CreateTextNode(email));
            userElement.AppendChild(emailElement);

            XmlElement passwordElement = xmlDoc.CreateElement("password");
            passwordElement.AppendChild(xmlDoc.CreateTextNode(password));
            userElement.AppendChild(passwordElement);

            XmlElement additionalFieldsElement = xmlDoc.CreateElement("additionalFields");

            IDictionaryEnumerator enumerator = extraFieldsTable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                XmlElement fieldElement = xmlDoc.CreateElement("field");

                XmlElement fieldCodeElement = xmlDoc.CreateElement("code");
                fieldCodeElement.AppendChild(xmlDoc.CreateTextNode(enumerator.Key.ToString()));
                fieldElement.AppendChild(fieldCodeElement);

                XmlElement fieldValueElement = xmlDoc.CreateElement("value");
                fieldValueElement.AppendChild(xmlDoc.CreateTextNode(enumerator.Value.ToString()));
                fieldElement.AppendChild(fieldValueElement);

                additionalFieldsElement.AppendChild(fieldElement);
            }

            userElement.AppendChild(additionalFieldsElement);


            return userElement;
        }
    }
}
