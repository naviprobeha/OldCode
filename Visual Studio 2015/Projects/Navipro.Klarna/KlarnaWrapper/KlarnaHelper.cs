using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klarna.Core;
using System.Runtime.InteropServices;
using System.Runtime;

namespace Navipro.KlarnaAPI.Wrapper
{
    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-93F1-1329ADB3FF2F";
        public const string intfguid = "D030D214-C984-496a-87F7-41832C115F2F";
        public const string eventguid = "D030D214-C984-496a-87F7-41832C115F6F";


        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IKlarnaHelper
    {
        [DispId(1)]
        string activateInvoice(int eId, string secret, string invoiceNo, int pClass);
        [DispId(2)]
        double getInvoiceAmount(int eId, string secret, string invoiceNo);
        [DispId(3)]
        bool checkChangeReservation(int eId, string secret, string reservationNo, double amount);
        [DispId(4)]
        string activateReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass);
        [DispId(5)]
        void addArticle(int quantity, string itemNo, string description, double unitPriceInclVat, double vat, double discount);
        [DispId(6)]
        void setBillingAddress(string email, string phoneNo, string cellPhoneNo, string firstName, string lastName, string name2, string address, string postCode, string city, string countryCode, string houseNo, string houseExt);
        [DispId(7)]
        void setShippingAddress(string email, string phoneNo, string cellPhoneNo, string firstName, string lastName, string name2, string address, string postCode, string city, string countryCode, string houseNo, string houseExt);
        [DispId(8)]
        void setTestMode(bool testMode);
        [DispId(9)]
        string createReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass);
        [DispId(10)]
        void setRegion(string languageCode, string currencyCode);
        [DispId(11)]
        string creditPart(int eId, string secret, string invoiceNo, string creditNo);
        [DispId(12)]
        string emailInvoice(int eId, string secret, string invoiceNo);
        [DispId(13)]
        string splitReservation(int eId, string secret, string reservationNo, double amount);
        [DispId(14)]
        string activateSplittedReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass);
        [DispId(15)]
        bool cancelReservation(int eId, string secret, string reservationNo);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.KlarnaAPI.Wrapper"), ClassInterface(ClassInterfaceType.None)]
    public class KlarnaHelper : IKlarnaHelper
    {
        private InvoiceLineCollection invoiceLineCollection;
        private Address billingAddress;
        private Address shippingAddress;
        private bool testMode;
        private string languageCode;
        private string currencyCode;

        public KlarnaHelper()
        {
        }

        public string activateInvoice(int eId, string secret, string invoiceNo, int pClass)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;
            
            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {                 
                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"                
                
            });
            

            return klarna.ActivateInvoice(invoiceNo, pClass);

        }

        public double getInvoiceAmount(int eId, string secret, string invoiceNo)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;
            
            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {
                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            return klarna.InvoiceAmount(invoiceNo);

        }

        public string createReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;

            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {
                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            klarna.SetAddress(new BillingAddress(billingAddress.email, billingAddress.phoneNo, billingAddress.cellPhoneNo, billingAddress.firstName, billingAddress.lastName, billingAddress.name2, billingAddress.address, billingAddress.postCode, billingAddress.city, billingAddress.country, billingAddress.houseNo, billingAddress.houseExt));
            klarna.SetAddress(new ShippingAddress(shippingAddress.email, shippingAddress.phoneNo, billingAddress.cellPhoneNo, shippingAddress.firstName, shippingAddress.lastName, shippingAddress.name2, shippingAddress.address, shippingAddress.postCode, shippingAddress.city, shippingAddress.country, shippingAddress.houseNo, shippingAddress.houseExt));

            int i = 0;
            while (i < invoiceLineCollection.Count)
            {
                klarna.AddArticle(invoiceLineCollection[i].quantity, invoiceLineCollection[i].itemNo, invoiceLineCollection[i].description, invoiceLineCollection[i].unitPriceInclVat, invoiceLineCollection[i].vat, invoiceLineCollection[i].discount, API.GoodsIs.IncVAT);
                i++;
            }

            return klarna.ReserveAmount(pno, API.Gender.Male, API.Flag.RSRVSendByEmail, pclass)[0];

        }

        public bool checkChangeReservation(int eId, string secret, string reservationNo, double amount)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;

            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {
                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            return klarna.ChangeReservation(reservationNo, amount, API.Flag.NewAmount);

        }

        public string emailInvoice(int eId, string secret, string invoiceNo)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {

                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            return klarna.EmailInvoice(invoiceNo);

        }

        public string activateReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {
                
                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            klarna.SetAddress(new BillingAddress(billingAddress.email, billingAddress.phoneNo, billingAddress.cellPhoneNo, billingAddress.firstName, billingAddress.lastName, billingAddress.name2, billingAddress.address, billingAddress.postCode, billingAddress.city, billingAddress.country, billingAddress.houseNo, billingAddress.houseExt));
            klarna.SetAddress(new ShippingAddress(shippingAddress.email, shippingAddress.phoneNo, billingAddress.cellPhoneNo, shippingAddress.firstName, shippingAddress.lastName, shippingAddress.name2, shippingAddress.address, shippingAddress.postCode, shippingAddress.city, shippingAddress.country, shippingAddress.houseNo, shippingAddress.houseExt));

            int i = 0;
            while (i < invoiceLineCollection.Count)
            {
                klarna.AddArticle(invoiceLineCollection[i].quantity, invoiceLineCollection[i].itemNo, invoiceLineCollection[i].description, invoiceLineCollection[i].unitPriceInclVat, invoiceLineCollection[i].vat, invoiceLineCollection[i].discount, API.GoodsIs.IncVAT);
                i++;
            }

            klarna.ClientIP = "0.0.0.0";

            return klarna.ActivateReservation(pno, reservationNo, API.Gender.Male, "", API.Flag.NoFlag, pclass, getEncoding())[1];

        }

        public string splitReservation(int eId, string secret, string reservationNo, double amount)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {

                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            klarna.ClientIP = "0.0.0.0";
            
            return klarna.SplitReservation(reservationNo, amount, API.Flag.NoFlag)[0].ToString();


        }

        public string activateSplittedReservation(int eId, string secret, string reservationNo, string pno, string orderNo, int pclass)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {

                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            klarna.SetAddress(new BillingAddress(billingAddress.email, billingAddress.phoneNo, billingAddress.cellPhoneNo, billingAddress.firstName, billingAddress.lastName, billingAddress.name2, billingAddress.address, billingAddress.postCode, billingAddress.city, billingAddress.country, billingAddress.houseNo, billingAddress.houseExt));
            klarna.SetAddress(new ShippingAddress(shippingAddress.email, shippingAddress.phoneNo, billingAddress.cellPhoneNo, shippingAddress.firstName, shippingAddress.lastName, shippingAddress.name2, shippingAddress.address, shippingAddress.postCode, shippingAddress.city, shippingAddress.country, shippingAddress.houseNo, shippingAddress.houseExt));

            int i = 0;
            while (i < invoiceLineCollection.Count)
            {
                klarna.AddArticle(invoiceLineCollection[i].quantity, invoiceLineCollection[i].itemNo, invoiceLineCollection[i].description, invoiceLineCollection[i].unitPriceInclVat, invoiceLineCollection[i].vat, invoiceLineCollection[i].discount, API.GoodsIs.IncVAT);
                i++;
            }

            klarna.ClientIP = "0.0.0.0";

            return klarna.ActivateReservation(pno, reservationNo, API.Gender.Male, "", API.Flag.RSRVPreserveReservation, pclass, getEncoding())[1];

        }

        public void addArticle(int quantity, string itemNo, string description, double unitPriceInclVat, double vat, double discount)
        {
            if (invoiceLineCollection == null) invoiceLineCollection = new InvoiceLineCollection();
            InvoiceLine invoiceLine = new InvoiceLine();
            invoiceLine.quantity = quantity;
            invoiceLine.itemNo = itemNo;
            invoiceLine.description = description;
            invoiceLine.unitPriceInclVat = unitPriceInclVat;
            invoiceLine.vat = vat;
            invoiceLine.discount = discount;

            invoiceLineCollection.Add(invoiceLine);

        }

        public void setBillingAddress(string email, string phoneNo, string cellPhoneNo, string firstName, string lastName, string name2, string address, string postCode, string city, string countryCode, string houseNo, string houseExt)
        {
            billingAddress = new Address();
            billingAddress.email = email;
            billingAddress.phoneNo = phoneNo;
            billingAddress.firstName = firstName;
            billingAddress.lastName = lastName;
            billingAddress.name2 = name2;
            billingAddress.address = address;
            billingAddress.postCode = postCode;
            billingAddress.city = city;
            billingAddress.countryCode = countryCode;
            billingAddress.cellPhoneNo = cellPhoneNo;
            billingAddress.houseNo = houseNo;
            billingAddress.houseExt = houseExt;

          
        }

        public void setShippingAddress(string email, string phoneNo, string cellPhoneNo, string firstName, string lastName, string name2, string address, string postCode, string city, string countryCode, string houseNo, string houseExt)
        {
            shippingAddress = new Address();
            shippingAddress.email = email;
            shippingAddress.phoneNo = phoneNo;
            shippingAddress.firstName = firstName;
            shippingAddress.lastName = lastName;
            shippingAddress.name2 = name2;
            shippingAddress.address = address;
            shippingAddress.postCode = postCode;
            shippingAddress.city = city;
            shippingAddress.countryCode = countryCode;
            shippingAddress.cellPhoneNo = cellPhoneNo;
            shippingAddress.houseNo = houseNo;
            shippingAddress.houseExt = houseExt;

        }

        public void setTestMode(bool testMode)
        {
            this.testMode = testMode;
        }

        private API.Language getLanguage()
        {
            if (languageCode == "SWE") return API.Language.Swedish;
            if (languageCode == "DAN") return API.Language.Danish;
            if (languageCode == "GER") return API.Language.German;
            if (languageCode == "FIN") return API.Language.Finnish;
            if (languageCode == "DUT") return API.Language.Dutch;
            if (languageCode == "NOR") return API.Language.Norwegian;
            return API.Language.Swedish;
        }

        private API.Encoding getEncoding()
        {
            if (languageCode == "SWE") return API.Encoding.Swedish;
            if (languageCode == "DAN") return API.Encoding.Danish;
            if (languageCode == "GER") return API.Encoding.German;
            if (languageCode == "FIN") return API.Encoding.Finnish;
            if (languageCode == "DUT") return API.Encoding.Dutch;
            if (languageCode == "NOR") return API.Encoding.Norwegian;
            return API.Encoding.Swedish;
        }

 
        private API.Currency getCurrency()
        {
            if (currencyCode == "SEK") return API.Currency.SEK;
            if (currencyCode == "DKK") return API.Currency.DKK;
            if (currencyCode == "EUR") return API.Currency.EUR;
            if (currencyCode == "NOK") return API.Currency.NOK;
            return API.Currency.SEK;
        }

        public void setRegion(string languageCode, string currencyCode)
        {
            this.languageCode = languageCode;
            this.currencyCode = currencyCode;

        }

        public string creditPart(int eId, string secret, string invoiceNo, string creditNo)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {

                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

            int i = 0;
            while (i < invoiceLineCollection.Count)
            {
                klarna.AddArticle(invoiceLineCollection[i].quantity, invoiceLineCollection[i].itemNo, invoiceLineCollection[i].description, invoiceLineCollection[i].unitPriceInclVat, invoiceLineCollection[i].vat, invoiceLineCollection[i].discount, API.GoodsIs.IncVAT);
                i++;
            }

            return klarna.CreditPart(invoiceNo, creditNo);
        }

        public bool cancelReservation(int eId, string secret, string reservationNo)
        {
            API.KlarnaServer mode = API.KlarnaServer.Live;
            if (testMode) mode = API.KlarnaServer.Beta;


            API klarna = new API();
            klarna.Config(new Klarna.Core.KlarnaConfig()
            {

                EID = eId,
                Secret = secret,
                Country = billingAddress.country,
                Language = getLanguage(),
                Currency = getCurrency(),
                Encoding = getEncoding(),
                //PNOEncoding = getEncoding(),
                // API.KlarnaServer.Beta or API.KlarnaServer.Live, depending
                // on which server your eid is associated with
                Mode = mode,
                PCStorage = "xml",
                PCURI = @"/tmp/pclasses.xml"
            });

 

            return klarna.CancelReservation(reservationNo);
        }
    }
}
