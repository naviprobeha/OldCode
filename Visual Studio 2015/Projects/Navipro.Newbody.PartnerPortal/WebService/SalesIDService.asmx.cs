using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using Navipro.Infojet.Lib;
using Navipro.Newbody.PartnerPortal.Library;

namespace Navipro.Newbody.PartnerPortal.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://infojet.navipro.se/newbody/partnerportal")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SalesIDService : System.Web.Services.WebService
    {
 
        [WebMethod(EnableSession = true)]
        public SalesID getSalesID(string code)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            return new SalesID(infojetContext, code);
        }

        [WebMethod(EnableSession = true)]
        public Product[] getProducts(string webSiteCode, string salesIdCode, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdCode);
            Product[] productArray = salesId.getProductArray(infojetContext, languageCode);

            Global.finishLogging(infojetContext, "getProducts");

            infojetContext.systemDatabase.close();

            return productArray;
        }

        [WebMethod(EnableSession = true)]
        public ItemCategory[] getItemCategories(string salesIdCode, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdCode);
            ItemCategory[] itemCategoryArray = ItemCategory.getDataSetArray(infojetContext.systemDatabase, salesId);

            Global.finishLogging(infojetContext, "getItemCategories");
            infojetContext.systemDatabase.close();

            return itemCategoryArray;
        }

        [WebMethod(EnableSession = true)]
        public Consumer[] getConsumers(string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
           
            Consumer[] consumerArray = Consumer.getDataSetArray(infojetContext.systemDatabase, webUserAccountNo);

            Global.finishLogging(infojetContext, "getConsumers");
            infojetContext.systemDatabase.close();

            return consumerArray;
        }

        [WebMethod(EnableSession = true)]
        public Consumer createConsumer(Consumer consumer)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            consumer = Consumer.createConsumer(infojetContext.systemDatabase, consumer);

            Global.finishLogging(infojetContext, "createConsumer");
            infojetContext.systemDatabase.close();

            return consumer;
        }

        [WebMethod(EnableSession = true)]
        public void updateConsumer(Consumer consumer)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer.updateConsumer(infojetContext.systemDatabase, consumer);

            Global.finishLogging(infojetContext, "updateConsumer");
            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void deleteConsumer(Consumer consumer)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer.deleteConsumer(infojetContext.systemDatabase, consumer);

            Global.finishLogging(infojetContext, "deleteConsumer");
            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public ConsumerSale[] getConsumerSales(string salesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ConsumerSale[] consumerSaleArray = ConsumerSale.getDataSetArray(infojetContext, salesId, webUserAccountNo);

            Global.finishLogging(infojetContext, "getConsumerSales");
            infojetContext.systemDatabase.close();

            return consumerSaleArray;
        }

        [WebMethod(EnableSession = true)]
        public ConsumerSale[] getHistoryConsumerSales(string salesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            //if (webUserAccountNo == currentSalesId.contactWebUserAccountNo) webUserAccountNo = "";

            ConsumerSale[] consumerSaleArray = ConsumerSale.getHistoryDataSetArray(infojetContext, salesId, webUserAccountNo);

            Global.finishLogging(infojetContext, "getHistoryConsumerSales");
            infojetContext.systemDatabase.close();

            return consumerSaleArray;
        }


        [WebMethod(EnableSession = true)]
        public ConsumerSale[] getSalesIdConsumerSales(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ConsumerSale[] consumerSaleArray = ConsumerSale.getDataSetArray(infojetContext, salesId, "");

            Global.finishLogging(infojetContext, "getSalesIdConsumerSales");
            infojetContext.systemDatabase.close();

            return consumerSaleArray;
        }

        [WebMethod(EnableSession = true)]
        public ConsumerSale[] getSalesIdHistoryConsumerSales(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ConsumerSale[] consumerSaleArray = ConsumerSale.getHistoryDataSetArray(infojetContext, salesId, "");

            Global.finishLogging(infojetContext, "getSalesIdHistoryConsumerSales");
            infojetContext.systemDatabase.close();

            return consumerSaleArray;
        }

        [WebMethod(EnableSession = true)]
        public void addToCart(string salesId, string webUserAccountNo, int consumerEntryNo, string itemNo, float quantity)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            CartHandler cartHandler = new CartHandler(infojetContext.systemDatabase, System.Web.HttpContext.Current.Session.SessionID, infojetContext);

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            SalesID currentSalesId = new SalesID(infojetContext, salesId);

            webUserAccount.customerNo = currentSalesId.customerNo;

            infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);

            cartHandler.addItemToCart(itemNo, quantity.ToString(), false, "", salesId, "", consumerEntryNo.ToString(), "", "", webUserAccount, DateTime.MinValue, DateTime.MinValue, currentSalesId.customerNo, infojetContext.currencyCode);

            Global.finishLogging(infojetContext, "addToCart");
            infojetContext.systemDatabase.close();
        }




        [WebMethod(EnableSession = true)]
        public void removeFromCart(string salesId, string webUserAccountNo, int consumerEntryNo, int webCartLineEntryNo)
        {
            Global.init();
            
            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @webCartLineEntryNo AND [Extra 2] = @salesId AND [Extra 4] = @consumerEntryNo AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.addStringParameter("@consumerEntryNo", consumerEntryNo.ToString(), 20);
            databaseQuery.addIntParameter("@webCartLineEntryNo", webCartLineEntryNo);

            databaseQuery.execute();

            Global.finishLogging(infojetContext, "removeFromCart");

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public SalesIDCollection getWebUserAccountSalesIDs(string webUserAccountNo)
        {
            SalesIDCollection salesIDCollection = (SalesIDCollection)Newbody.PartnerPortal.Library.GlobalCache.checkCache("getWebUserAccountSalesIDs", webUserAccountNo);
            if (salesIDCollection != null) return salesIDCollection;

            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            salesIDCollection = SalesID.getSalesIds(infojetContext, webUserAccountNo);

            Global.finishLogging(infojetContext, "getWebUserAccountSalesIDs");

            infojetContext.systemDatabase.close();

            Newbody.PartnerPortal.Library.GlobalCache.cacheObject("getWebUserAccountSalesIDs", webUserAccountNo, salesIDCollection, 15);

            return salesIDCollection;


        }

        [WebMethod(EnableSession = true)]
        public SalesIDCollection getAllSalesIDs(string customerNo)
        {
            SalesIDCollection salesIDCollection = (SalesIDCollection)Newbody.PartnerPortal.Library.GlobalCache.checkCache("getAllSalesIDs", customerNo);
            if (salesIDCollection != null) return salesIDCollection;

            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            salesIDCollection = SalesID.getAllSalesIds(infojetContext, customerNo);

            Global.finishLogging(infojetContext, "getAllSalesIDs");

            infojetContext.systemDatabase.close();

            Newbody.PartnerPortal.Library.GlobalCache.cacheObject("getAllSalesIDs", customerNo, salesIDCollection, 15);

            return salesIDCollection;

        }



        [WebMethod(EnableSession = true)]
        public Consumer[] getSalesIdConsumers(string webUserAccountNo, string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer[] consumerArray = Consumer.getSalesIDDataSetArray(infojetContext.systemDatabase, webUserAccountNo, salesId);

            Global.finishLogging(infojetContext, "getSalesIdConsumers");

            infojetContext.systemDatabase.close();

            return consumerArray;
        }

        [WebMethod(EnableSession = true)]
        public DocumentCollection getSalesIdDocuments(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);

            DocumentCollection docCollection = currentSalesId.getDocuments();

            Global.finishLogging(infojetContext, "getDocuments");

            infojetContext.systemDatabase.close();

            return docCollection;
        }

        [WebMethod(EnableSession = true)]
        public void addConsumerToSalesId(string salesId, Consumer consumer)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer.addConsumerToSalesId(infojetContext.systemDatabase, salesId, consumer);

            Global.finishLogging(infojetContext, "addConsumerToSalesId");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void setConsumerPaid(string salesId, Consumer consumer, bool paid)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer.setPaid(infojetContext.systemDatabase, salesId, consumer, paid);

            Global.finishLogging(infojetContext, "setConsumerPaid");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void removeConsumerFromSalesId(string salesId, Consumer consumer)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, consumer.webUserAccountNo);
            if (salesIdSalesPerson.getStatus() < 2)
            {
                Consumer.removeConsumerFromSalesId(infojetContext.systemDatabase, salesId, consumer);
            }

            Global.finishLogging(infojetContext, "removeConsumerFromSalesId");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void releaseCart(string webUserAccountNo, string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, webUserAccountNo);
            salesIdSalesPerson.releaseCart();

            Global.finishLogging(infojetContext, "releaseCart");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void undoReleaseCart(string webUserAccountNo, string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, webUserAccountNo);
            salesIdSalesPerson.undoReleaseCart();

            Global.finishLogging(infojetContext, "undoReleaseCart");

            infojetContext.systemDatabase.close();

        }


        [WebMethod(EnableSession = true)]
        public WebInfoMessageHeader[] getMessages(int type, string webUserAccountNo, string salesIdNo, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdNo);
            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            WebInfoMessageHeader[] webInfoMessageHeaderArray = WebInfoMessageHeader.getMessageArray(infojetContext, type, salesId, webUserAccount, true, languageCode);

            Global.finishLogging(infojetContext, "getMessages");

            infojetContext.systemDatabase.close();

            return webInfoMessageHeaderArray;
        }

        [WebMethod(EnableSession = true)]
        public WebInfoMessageHeader[] getAllMessages(int type, string webUserAccountNo, string salesIdNo, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdNo);
            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            WebInfoMessageHeader[] webInfoMessageHeaderArray = WebInfoMessageHeader.getMessageArray(infojetContext, type, salesId, webUserAccount, false, languageCode);

            Global.finishLogging(infojetContext, "getAllMessages");

            infojetContext.systemDatabase.close();

            return webInfoMessageHeaderArray;
        }

        [WebMethod(EnableSession = true)]
        public SalesIDSalesPersonCollection getSalesIDSalesPersons(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDSalesPersonCollection salesIDSalesPersonCollection = SalesIDSalesPerson.getSalesPersons(infojetContext, salesId);

            Global.finishLogging(infojetContext, "getSalesIDSalesPersons");

            infojetContext.systemDatabase.close();

            return salesIDSalesPersonCollection;
        }


        [WebMethod(EnableSession = true)]
        public UserSession userProfile_authenticate(string userId, string password)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccounts webUserAccounts = new WebUserAccounts(infojetContext.systemDatabase);
            WebUserAccount webUserAccount = webUserAccounts.getUserAccount(userId, password);
            //if (systemHandler.authenticate(infojetContext, userId, password))
            if (webUserAccount != null)
            {

                //UserSession userSession = infojetContext.userSession;
                UserSession userSession = new UserSession();
                userSession.webUserAccount = webUserAccount;
                Customer customer = new Customer(infojetContext, webUserAccount.customerNo);
                userSession.customer = customer;
                userSession.customer.updateSalesPerson();

                

                userSession.webUserAccount.companyRole = "SP";
                if (SalesID.checkIsContactPerson(infojetContext, userSession.webUserAccount.no))
                {
                    userSession.webUserAccount.companyRole = "KP";
                }

                if (SalesID.checkIsSubContactPerson(infojetContext, userSession.webUserAccount.no))
                {
                    userSession.webUserAccount.companyRole = "GP";
                }
                
                System.Collections.ArrayList webPageList = new System.Collections.ArrayList();

                if (SalesID.unConfirmedAgreementsExists(infojetContext, userSession.webUserAccount.no))
                {
                    webPageList = infojetContext.webSite.getWebPagesByCategory("CONF AGREEMENT", userSession.webUserAccount.no);
                }
                else
                {
                    if ((userSession.webUserAccount.email == "") || (userSession.webUserAccount.cellPhoneNo == "") ||
                        (userSession.webUserAccount.email == null) || (userSession.webUserAccount.cellPhoneNo == null))
                    {
                        webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.myProfileCategoryCode, userSession.webUserAccount.no);
                    }
                    else
                    {
                        webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.authenticatedCategoryCode, userSession.webUserAccount.no);
                    }
                }

                if (webPageList.Count > 0)
                {
                    WebPage webPage = (WebPage)webPageList[0];
                    userSession.startPageCode = webPage.code;
                }

                Global.finishLogging(infojetContext, "userProfile_authenticate");

                infojetContext.systemDatabase.close();

                return userSession;
            }

            infojetContext.systemDatabase.close();

            return null;

        }

        [WebMethod(EnableSession = true)]
        public void getTotalSoldPackages(string salesId, out int soldPackages, out float soldAmount)
        {
            getSalesPersonTotalSoldPackages(salesId, "", out soldPackages, out soldAmount);
        }

        [WebMethod(EnableSession = true)]
        public void getSalesPersonTotalSoldPackages(string salesId, string webUserAccountNo, out int soldPackages, out float soldAmount)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            currentSalesId.updateSoldPackages(webUserAccountNo);

            soldPackages = currentSalesId.soldPackages;
            soldAmount = currentSalesId.soldAmount;

            Global.finishLogging(infojetContext, "getSalesPersonTotalSoldPackages");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public SalesIDSalesPerson getSalesIDSalesPerson(string salesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, webUserAccount);
            salesIdSalesPerson.updateSoldPackages(infojetContext);

            Global.finishLogging(infojetContext, "getSalesIDSalesPerson");

            infojetContext.systemDatabase.close();

            return salesIdSalesPerson;
        }

        [WebMethod(EnableSession = true)]
        public UserResponse createSalesPerson(string salesId, string name, string email, string phoneNo, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount();
            webUserAccount.name = name;
            webUserAccount.email = email;
            webUserAccount.userId = email;
            webUserAccount.phoneNo = phoneNo;
            webUserAccount.languageCode = languageCode;

            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson();
            salesIdSalesPerson.salesId = salesId;
            salesIdSalesPerson.webUserAccount = webUserAccount;

            UserResponse userResponse = new UserResponse();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            if (SalesIDSalesPerson.countSalesPersons(infojetContext, salesId) >= currentSalesId.noOfSalesPersons)
            {
                userResponse.result = false;
                userResponse.errorMessage = infojetContext.translate("TOO MANY SP").Replace("%1", currentSalesId.noOfSalesPersons.ToString());
                return userResponse;
            }


			ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createSalesPerson", salesIdSalesPerson));
            if (!appServerConnection.processRequest())
            {
                userResponse.result = false;
                userResponse.errorMessage = appServerConnection.getLastError();
            }
            else
            {
                userResponse.result = true;
            }


            Global.finishLogging(infojetContext, "createSalesPerson");

            infojetContext.systemDatabase.close();

            return userResponse;
        }

        [WebMethod(EnableSession = true)]
        public bool deleteSalesPerson(string currentSalesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            bool result = false;

            SalesID salesId = new SalesID(infojetContext, currentSalesId);
            if (salesId.contactWebUserAccountNo != webUserAccountNo)
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, currentSalesId, webUserAccountNo);

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "deleteSalesPerson", salesIdSalesPerson));
                result = appServerConnection.processRequest();

            }

            Global.finishLogging(infojetContext, "deleteSalesPerson");

            infojetContext.systemDatabase.close();

            return result;
        }

        [WebMethod(EnableSession = true)]
        public WebShipmentMethodCollection getShipmentMethodsSingle(string salesId, string shipToCountryCode, string shipToPostCode, string languageCode)
        {
            string[] salesIdArray = new string[1];
            salesIdArray[0] = salesId;

            return getShipmentMethods(salesIdArray, shipToCountryCode, shipToPostCode, languageCode);
        }

        [WebMethod(EnableSession = true)]
        public PackingImage getPackingImageSingle(string salesId, string languageCode)
        {
            string[] salesIdArray = new string[1];
            salesIdArray[0] = salesId;

            return getPackingImage(salesIdArray, languageCode);
        }


        [WebMethod(EnableSession = true)]
        public WebShipmentMethodCollection getShipmentMethods(string[] salesIdArray, string shipToCountryCode, string shipToPostCode, string languageCode)
        {
            if (shipToCountryCode == null) shipToCountryCode = "";
            if (shipToPostCode == null) shipToPostCode = "";

            WebShipmentMethodCollection webShipmentMethodCollection = new WebShipmentMethodCollection();

            if (salesIdArray == null)
            {
                return webShipmentMethodCollection;
            }


            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            int i = 0;
            int totalPackages = 0;
            float totalAmount = 0;
            while (i < salesIdArray.Length)
            {
                SalesID currentSalesId = new SalesID(infojetContext, salesIdArray[i]);
                currentSalesId.updateSoldPackages("");
                totalPackages = totalPackages + currentSalesId.soldPackages;
                totalAmount = totalAmount + currentSalesId.soldAmount;
                i++;
            }

            SalesID currentSalesId2 = new SalesID(infojetContext, salesIdArray[0]);
            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, currentSalesId2.contactWebUserAccountNo);

            infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);

            WebShipmentMethods webShipmentMethods = new WebShipmentMethods(infojetContext);
            webShipmentMethodCollection = webShipmentMethods.getWebShipmentMethodCollection(infojetContext.webSite.code, shipToCountryCode, shipToPostCode, totalPackages, totalAmount, 0, 0, languageCode);

            bool freeFreight = currentSalesId2.checkReorderingFreeFreight();

            i = 0;
            while (i < webShipmentMethodCollection.Count)
            {
                //Priserna underhålls inkl moms.
                webShipmentMethodCollection[i].amountInclVat = webShipmentMethodCollection[i].amount;
                if (freeFreight) webShipmentMethodCollection[i].amountInclVat = 0;

                if (!SalesID.checkShipmentMethod(infojetContext, webShipmentMethodCollection[i], shipToPostCode))
                {
                    webShipmentMethodCollection.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            Global.finishLogging(infojetContext, "getShipmentMethods");

            infojetContext.systemDatabase.close();

            return webShipmentMethodCollection;
        }


        [WebMethod(EnableSession = true)]
        public SalesIDAgreementCollection getAgreement(string webUserAccountNo, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDCollection salesIdCollection = SalesID.getSalesIds(infojetContext, webUserAccountNo);

            if (salesIdCollection.Count > 0)
            {
                SalesID currentSalesId = salesIdCollection[salesIdCollection.Count - 1];

                SalesIDAgreementCollection salesIdAgreementCollection = currentSalesId.getAgreement(infojetContext, languageCode);

                Global.finishLogging(infojetContext, "getAgreement");

                infojetContext.systemDatabase.close();

                return salesIdAgreementCollection;
            }

            Global.finishLogging(infojetContext, "getAgreement");

            infojetContext.systemDatabase.close();
            return null;
        }

        [WebMethod(EnableSession = true)]
        public bool approveAgreement(string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "confirmAgreement", webUserAccount, webUserAccount));
            bool result = appServerConnection.processRequest();

            Global.finishLogging(infojetContext, "approveAgreement");

            infojetContext.systemDatabase.close();

            return result;
        }

        [WebMethod(EnableSession = true)]
        public int checkAvailableSalesPersonQuota(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);

            int result = currentSalesId.checkAvailableSalesPersonQuota();

            Global.finishLogging(infojetContext, "checkAvailableSalesPersonQuota");

            infojetContext.systemDatabase.close();

            return result;
        }

        [WebMethod(EnableSession = true)]
        public WebPage getStartPage(string webUserAccountNo)
        {
            Global.init();
            
            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            ArrayList webPageList = null;
     

            if (SalesID.unConfirmedAgreementsExists(infojetContext, webUserAccountNo))
            {
                webPageList = infojetContext.webSite.getWebPagesByCategory("CONF AGREEMENT", webUserAccountNo);
            }
            else
            {
                if ((webUserAccount.email == "") || (webUserAccount.cellPhoneNo == "") ||
                    (webUserAccount.email == null) || (webUserAccount.cellPhoneNo == null))
                {
                    webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.myProfileCategoryCode, webUserAccountNo);
                }
                else
                {
                    webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.authenticatedCategoryCode, webUserAccountNo);
                }
            }

            Global.finishLogging(infojetContext, "getStartPage");

            infojetContext.systemDatabase.close();

            if (webPageList.Count > 0)
            {
                WebPage webPage = (WebPage)webPageList[0];
                return webPage;
            }

            return null;

        }

        [WebMethod(EnableSession = true)]
        public PackingImage getPackingImage(string[] salesIdArray, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            int i = 0;
            int totalPackages = 0;
            while (i < salesIdArray.Length)
            {
                SalesID currentSalesId = new SalesID(infojetContext, salesIdArray[i]);
                currentSalesId.updateSoldPackages("");
                totalPackages = totalPackages + currentSalesId.soldPackages;
                i++;
            }

            PackingImage packingImage = PackingImage.getPackingImage(infojetContext, totalPackages, languageCode);

            Global.finishLogging(infojetContext, "getPackingImage");

            infojetContext.systemDatabase.close();

            return packingImage;
        }

        [WebMethod(EnableSession = true)]
        public SalesIDStatistics getSalesIDStatistics(string customerNo, DateTime startDate)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDStatistics salesIDStatistics = SalesID.getStatistics(infojetContext, customerNo, startDate);

            Global.finishLogging(infojetContext, "getSalesIDStatistics");

            infojetContext.systemDatabase.close();

            return salesIDStatistics;
        }

        [WebMethod(EnableSession = true)]
        public void setSalesPersonPaymentReceived(string salesId, string webUserAccountNo, bool paymentReceived)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesIDSalesPerson salesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, webUserAccountNo);
            salesPerson.setPaymentReceived(paymentReceived);

            Global.finishLogging(infojetContext, "setSalesPersonPaymentReceived");

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public bool checkSalesIdOrderExists(string salesIdCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdCode);
            bool orderExists = salesId.checkOrderSent();

            Global.finishLogging(infojetContext, "checkSalesIdOrderExists");

            infojetContext.systemDatabase.close();

            return orderExists;
        }

        [WebMethod(EnableSession = true)]
        public Contact getContactInformation(string salesIdCode, string webUserAccountNo)
        {      
            Global.init();

            Contact contact = new Contact();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = new SalesID(infojetContext, salesIdCode);
            if (salesId.isPrimaryContactPerson(webUserAccountNo))
            {
                salesId.updateSalesPerson();

                //Customer customer = new Customer(infojetContext, salesId.customerNo);
                //customer.updateSalesPerson();

                contact.name = salesId.salesPerson.name;
                contact.phoneNo = salesId.salesPerson.phoneNo;
                contact.email = salesId.salesPerson.email;
            }
            else
            {
                contact.name = salesId.contactWebUserAccount.name;
                contact.phoneNo = salesId.contactWebUserAccount.phoneNo;
                contact.email = salesId.contactWebUserAccount.email;
            }

            Global.finishLogging(infojetContext, "getContactInformation");

            infojetContext.systemDatabase.close();

            return contact;
        }



        [WebMethod(EnableSession = true)]
        public OrderResponse sendOrder(string[] salesIdArray, string webUserAccountNo, WebCartHeader webCartHeader)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            string salesIds = "";

            foreach (string salesIdCode in salesIdArray)
            {
                SalesID salesID = new SalesID(infojetContext, salesIdCode);
                salesID.applySessionIdToSalesIdCart(infojetContext.sessionId);
                webUserAccount.customerNo = salesID.customerNo;
                salesIds = salesIds + ", " + salesIdCode;
            }



            infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);

            WebCartHeader webCartHeaderToSend = new WebCartHeader(infojetContext, infojetContext.sessionId);
            webCartHeaderToSend.setUserSession(infojetContext.userSession);


            webCartHeaderToSend.shipToName = webCartHeader.shipToName;
            webCartHeaderToSend.shipToName2 = webCartHeader.shipToName2;
            webCartHeaderToSend.shipToAddress2 = webCartHeader.shipToAddress;
            webCartHeaderToSend.shipToAddress = webCartHeader.shipToAddress2;
            webCartHeaderToSend.shipToPostCode = webCartHeader.shipToPostCode;
            webCartHeaderToSend.shipToCity = webCartHeader.shipToCity;

            webCartHeaderToSend.message = webCartHeader.message;
            webCartHeaderToSend.phoneNo = webCartHeader.phoneNo;
            webCartHeaderToSend.email = webCartHeader.email;


            WebShipmentMethod webShipmentMethod = new WebShipmentMethod(infojetContext, infojetContext.webSite.code, webCartHeader.webShipmentMethodCode);
            webCartHeaderToSend.applyWebShipmentMethod(webShipmentMethod);

            webCartHeaderToSend.freightFee = webCartHeader.freightFee;
            webCartHeaderToSend.webShipmentMethod.glAccountNo = "3520";

            
            WebCartLines webCartLines = new WebCartLines(infojetContext);
            webCartHeaderToSend.setCartLines(SalesID.getCartLines(infojetContext, salesIdArray, infojetContext.webSite.code));


            OrderResponse orderResponse = new OrderResponse();

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createOrder", webCartHeaderToSend));
            if (!appServerConnection.processRequest())
            {
                orderResponse.errorMessage = appServerConnection.getLastError();
                orderResponse.result = false;
            }
            else
            {
                orderResponse.orderNo = appServerConnection.serviceResponse.orderNo;
                orderResponse.result = true;

                foreach (string salesIdCode in salesIdArray)
                {
                    Consumer.removeAllConsumersFromSalesId(infojetContext.systemDatabase, salesIdCode);
                    SalesIDSalesPerson.clearAllPaymentReceived(infojetContext, salesIdCode);
                }

                webCartHeaderToSend.delete();
                
            }

            Global.finishLogging(infojetContext, "sendOrder "+salesIds);

            infojetContext.systemDatabase.close();

            return orderResponse;
        }

        [WebMethod(EnableSession = true)]
        public string getWebUserAccountBankAccountNo(string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            string accountNo = webUserAccount.getHistoryProfileValue("", "ACCOUNTNO");

            Global.finishLogging(infojetContext, "getWebUserAccountBankAccountNo");

            infojetContext.systemDatabase.close();

            return accountNo;
        }


        [WebMethod(EnableSession = true)]
        public BankDetails getWebUserAccountBankDetails(string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            BankDetails bankDetails = new BankDetails();
            
            string accountNo = webUserAccount.getHistoryProfileValue("", "ACCOUNTNO");
            string swish = webUserAccount.getHistoryProfileValue("", "SWISH");

            bankDetails.bankAccountNo = accountNo;
            if ((swish.ToUpper() == "YES") || (swish.ToUpper() == "JA")) bankDetails.swish = true;
            
            Global.finishLogging(infojetContext, "getWebUserAccountBankAccountNo");

            infojetContext.systemDatabase.close();

            return bankDetails;
        }


        [WebMethod(EnableSession = true)]
        public int getSalesIDStatus(string salesId)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);

            int status = currentSalesId.getStatus(infojetContext);

            Global.finishLogging(infojetContext, "getSalesIDStatus");

            infojetContext.systemDatabase.close();

            return status;
        }

        [WebMethod(EnableSession = true)]
        public void setAdditionalOrderMode(string salesId, bool active)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            currentSalesId.setAdditionalOrderMode(active);
            

            infojetContext.systemDatabase.close();

        }


        [WebMethod(EnableSession = true)]
        public WebUserAccount getWebUserAccount(string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            Global.finishLogging(infojetContext, "getWebUserAccount");

            infojetContext.systemDatabase.close();

            return webUserAccount;
        }

        [WebMethod(EnableSession = true)]
        public void setGroupContactPerson(string salesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            currentSalesId.setGroupContactPerson(webUserAccountNo);

            Global.finishLogging(infojetContext, "setGroupContactPerson");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public void inviteSalesPerson(string salesId, string name, string phoneNo, string email, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID currentSalesId = new SalesID(infojetContext, salesId);
            currentSalesId.inviteSalesPerson(name, email, phoneNo, languageCode);

            Global.finishLogging(infojetContext, "inviteSalesPerson");

            infojetContext.systemDatabase.close();

        }

        [WebMethod(EnableSession = true)]
        public string getSalesIdFromControlNo(string controlNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SalesID salesId = SalesID.getSalesIdFromControlNo(infojetContext, controlNo);

            Global.finishLogging(infojetContext, "getSalesIdFromControlNo");

            infojetContext.systemDatabase.close();

            return salesId.code;
        }

        [WebMethod(EnableSession = true)]
        public Consumer getConsumerFromOcr(string ocrNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Consumer consumer = Consumer.getConsumerFromOCR(infojetContext.systemDatabase, ocrNo);

            Global.finishLogging(infojetContext, "getConsumerFromOcr");

            infojetContext.systemDatabase.close();

            return consumer;
        }


        [WebMethod(EnableSession = true)]
        public Byte[] getDocument(int documentType, string documentNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            Byte[] byteArray = Document.getByteArray(infojetContext, documentType, documentNo);

            if (byteArray == null)
            {
                Document document = new Document();
                document.documentType = documentType;
                document.documentNo = documentNo;


                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "requestDocument", document));
                appServerConnection.processRequest();

                byteArray = Document.getByteArray(infojetContext, documentType, documentNo);
            }
            infojetContext.systemDatabase.close();

            return byteArray;
        }

        [WebMethod(EnableSession = true)]
        public GiftVoucher getVoucher(string no, string controlNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            GiftVoucher giftVoucher = GiftVoucher.getGiftVoucher(infojetContext, no, controlNo);

            infojetContext.systemDatabase.close();

            return giftVoucher;
        }


        [WebMethod(EnableSession = true)]
        public ProductCollection getVoucherProducts()
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ProductCollection productCollection = Product.getVoucherProducts(infojetContext);

            infojetContext.systemDatabase.close();

            return productCollection;
        }

        [WebMethod(EnableSession = true)]
        public void submitVoucher(GiftVoucher giftVoucher)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "submitVoucher", giftVoucher));
            if (!appServerConnection.processRequest())
            {
                infojetContext.systemDatabase.close();
                throw new Exception(appServerConnection.getLastError());
            }
           
            infojetContext.systemDatabase.close();

            
        }



        [WebMethod(EnableSession = true)]
        public void simple_addToCart(string salesId, string webUserAccountNo, string itemNo, float quantity)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            CartHandler cartHandler = new CartHandler(infojetContext.systemDatabase, System.Web.HttpContext.Current.Session.SessionID, infojetContext);

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
            SalesID currentSalesId = new SalesID(infojetContext, salesId);

            webUserAccount.customerNo = currentSalesId.customerNo;

            infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);

            cartHandler.addItemToCart(itemNo, quantity.ToString(), false, "", salesId, "", "", "", "", webUserAccount, DateTime.MinValue, DateTime.MinValue, currentSalesId.customerNo, infojetContext.currencyCode);

            Global.finishLogging(infojetContext, "addToCart");
            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public CartItemCollection simple_getCartLines(string salesId, string webUserAccountNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            CartItemCollection cartItemCollection = ConsumerSale.getCartItemCollection(infojetContext, salesId, webUserAccountNo);

            Global.finishLogging(infojetContext, "getConsumerSales");
            infojetContext.systemDatabase.close();

            return cartItemCollection;
        }


        [WebMethod(EnableSession = true)]
        public OrderResponse simple_sendOrder(string[] salesIdArray, string webUserAccountNo, string shipToName, string shipToName2, string shipToAddress, string shipToAddress2, string shipToPostCode, string shipToCity, string shipToCountryCode, string message, string phoneNo, string email, string webShipmentMethodCode, float freightFee)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            string salesIds = "";

            foreach (string salesIdCode in salesIdArray)
            {
                SalesID salesID = new SalesID(infojetContext, salesIdCode);
                salesID.applySessionIdToSalesIdCart(infojetContext.sessionId);
                webUserAccount.customerNo = salesID.customerNo;
                salesIds = salesIds + ", " + salesIdCode;
            }


            infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);

            WebCartHeader webCartHeaderToSend = new WebCartHeader(infojetContext, infojetContext.sessionId);
            webCartHeaderToSend.setUserSession(infojetContext.userSession);


            webCartHeaderToSend.shipToName = shipToName;
            webCartHeaderToSend.shipToName2 = shipToName2;
            webCartHeaderToSend.shipToAddress2 = shipToAddress;
            webCartHeaderToSend.shipToAddress = shipToAddress2;
            webCartHeaderToSend.shipToPostCode = shipToPostCode;
            webCartHeaderToSend.shipToCity = shipToCity;
            webCartHeaderToSend.shipToCountryCode = shipToCountryCode;

            webCartHeaderToSend.message = message;
            webCartHeaderToSend.phoneNo = phoneNo;
            webCartHeaderToSend.email = email;


            WebShipmentMethod webShipmentMethod = new WebShipmentMethod(infojetContext, infojetContext.webSite.code, webShipmentMethodCode);
            webCartHeaderToSend.applyWebShipmentMethod(webShipmentMethod);

            webCartHeaderToSend.freightFee = freightFee;
            webCartHeaderToSend.webShipmentMethod.glAccountNo = "3520";


            WebCartLines webCartLines = new WebCartLines(infojetContext);
            webCartHeaderToSend.setCartLines(SalesID.getCartLines(infojetContext, salesIdArray, infojetContext.webSite.code));


            OrderResponse orderResponse = new OrderResponse();

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createOrder", webCartHeaderToSend));
            if (!appServerConnection.processRequest())
            {
                orderResponse.errorMessage = appServerConnection.getLastError();
                orderResponse.result = false;
            }
            else
            {
                orderResponse.orderNo = appServerConnection.serviceResponse.orderNo;
                orderResponse.result = true;

                webCartHeaderToSend.deleteLines();
                webCartHeaderToSend.delete();
                
            }

            Global.finishLogging(infojetContext, "sendOrder " + salesIds);

            infojetContext.systemDatabase.close();

            return orderResponse;
        }

        [WebMethod(EnableSession = true)]
        public void simple_removeFromCart(string salesId, string webUserAccountNo, int webCartLineEntryNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @webCartLineEntryNo AND [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.addIntParameter("@webCartLineEntryNo", webCartLineEntryNo);

            databaseQuery.execute();

            Global.finishLogging(infojetContext, "removeFromCart");

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public void simple_updateCartLine(string salesId, string webUserAccountNo, int webCartLineEntryNo, string itemNo, float quantity)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Item No_] = @itemNo, [Quantity] = @quantity WHERE [Entry No_] = @webCartLineEntryNo AND [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.addIntParameter("@webCartLineEntryNo", webCartLineEntryNo);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addDecimalParameter("@quantity", quantity);

            databaseQuery.execute();

            Global.finishLogging(infojetContext, "updateCartLine");

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public UserResponse registerSalesPerson(string salesId, string name, string email, string phoneNo, string userId, string password, bool sendSms, bool sendEmail, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebForm webForm = new WebForm(infojetContext, "NEWBODY", "USER_REG");

            ArrayList keyList = new ArrayList();
            ArrayList valueList = new ArrayList();
            ArrayList fileList = new ArrayList();

            
            keyList.Add("NAME");
            valueList.Add(name);

            keyList.Add("EMAIL");
            valueList.Add(email);

            keyList.Add("CELLPHONENO");
            valueList.Add(phoneNo);

            keyList.Add("USERID");
            valueList.Add(userId);

            keyList.Add("PASSWORD");
            valueList.Add(password);

            keyList.Add("SALESID");
            valueList.Add(salesId);

            keyList.Add("SENDSMS");
            if (sendSms)
            {
                valueList.Add("Yes");
            }
            else
            {
                valueList.Add("No");
            }

            keyList.Add("SENDEMAIL");
            if (sendEmail)
            {
                valueList.Add("Yes");
            }
            else
            {
                valueList.Add("No");
            }

            Navipro.Infojet.Lib.WebFormDocument webFormDoc = new WebFormDocument(webForm, keyList, valueList, fileList);


            UserResponse userResponse = new UserResponse();

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createSalesUser", webFormDoc));
            if (!appServerConnection.processRequest())
            {
                userResponse.result = false;
                userResponse.errorMessage = appServerConnection.getLastError();
            }
            else
            {
                userResponse.result = true;
            }


            Global.finishLogging(infojetContext, "createSalesUser");

            infojetContext.systemDatabase.close();

            return userResponse;
        }


    }
}
