using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Navipro.Infojet.Lib;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class ConsumerSale
    {
        private string _orderingNo;
        private Consumer _consumer;
        private CartItem[] _cartItemArray;
        private int _noOfCartLines = 0;
        private bool _paid;

        public ConsumerSale()
        {
            _consumer = new Consumer();
            _cartItemArray = new CartItem[1000];
        }

        public void reOrderWebCartLines()
        {
            CartItem[] newCartItemArray = new CartItem[_noOfCartLines];

            int i = 0;
            while (i < _noOfCartLines)
            {
                newCartItemArray[i] = _cartItemArray[i];
                i++;
            }

            _cartItemArray = newCartItemArray;
        }

        public Consumer consumer { get { return _consumer; } set { _consumer = value; } }
        public CartItem[] cartItemArray { get { return _cartItemArray; } set { _cartItemArray = value; } }
        public int noOfCartLines { get { return _noOfCartLines; } set { _noOfCartLines = value; } }
        public string orderingNo { get { return _orderingNo; } set { _orderingNo = value; } }
        public bool paid { get { return _paid; } set { _paid = value; } }

        public static ConsumerSale[] getDataSetArray(Navipro.Infojet.Lib.Infojet infojetContext, string salesId, string webUserAccountNo)
        {
            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);
            
            bool cartReleased = currentSalesID.checkReleasedCart(webUserAccountNo);

            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            string webUserAccountNoQuery = "";
            if (webUserAccountNo != "") webUserAccountNoQuery = "AND sc.[Web User Account No_] = @webUserAccountNo";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], cl.[Entry No_], cl.[Item No_], cl.[Unit Of Measure Code], cl.[Unit Price], cl.[Quantity], cl.[Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code], c.[Entry No_], c.[Name], c.[Phone No_], c.[E-Mail], c.[OCR No_], i.[Description], i.[Size], p.[Paid] FROM [" + infojetContext.systemDatabase.getTableName("SalesID Consumer") + "] sc LEFT JOIN [" + infojetContext.systemDatabase.getTableName("SalesID Consumer Paid") + "] p ON p.[Web User Account No_] = sc.[Web User Account No_] AND p.[Sales ID] = sc.[Sales ID] AND p.[Consumer Entry No_] = sc.[Consumer Entry No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] cl ON sc.[Sales ID] = cl.[Extra 2] AND sc.[Consumer Entry No_] = cl.[Extra 4] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Consumer") + "] c ON sc.[Consumer Entry No_] = c.[Entry No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) ON i.[No_] = cl.[Item No_] WHERE sc.[Sales ID] = @salesId " + webUserAccountNoQuery + " ORDER BY sc.[Consumer Entry No_], cl.[Entry No_]");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            ConsumerSale[] consumerSaleArray = new ConsumerSale[1000];
            ConsumerSale currentConsumerSale = new ConsumerSale();

            int i = 0;
            int consumerSalesEntryNo = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                CartItem cartItem = null;

                int entryNo = 0;
                if (int.TryParse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), out entryNo))
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, dataSet.Tables[0].Rows[i]);
                    cartItem = new CartItem(webCartLine);
                    cartItem.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(22).ToString();
                    
                    if (giftCardTable.Contains(cartItem.itemNo))
                    {
                        cartItem.amount = (cartItem.unitPrice * cartItem.quantity) + (+ currentSalesID.profit * (int)giftCardTable[cartItem.itemNo] * cartItem.quantity);
                    }
                    else
                    {
                        cartItem.amount = (cartItem.unitPrice + currentSalesID.profit) * cartItem.quantity;
                    }

                }


                Consumer consumer = new Consumer();

                consumer.entryNo = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString());
                consumer.name = dataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString();
                consumer.phoneNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString();
                consumer.email = dataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString();
                consumer.ocrNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString();

                if (consumer.entryNo != currentConsumerSale.consumer.entryNo)
                {
                    ConsumerSale consumerSale = new ConsumerSale();
                    consumerSale.orderingNo = salesId + "-" + consumer.entryNo;
                    consumerSale.consumer = consumer;
                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(24).ToString() == "1") consumerSale.paid = true;

                    if (cartItem != null) consumerSale.cartItemArray[consumerSale.noOfCartLines] = cartItem;

                    currentConsumerSale = consumerSale;

                    if (cartReleased)
                    {
                        if (currentConsumerSale.cartItemArray[0] != null)
                        {
                            consumerSaleArray[consumerSalesEntryNo] = currentConsumerSale;
                            currentConsumerSale.noOfCartLines++;
                            consumerSalesEntryNo++;
                        }
                    }
                    else
                    {
                        consumerSaleArray[consumerSalesEntryNo] = currentConsumerSale;
                        currentConsumerSale.noOfCartLines++;
                        consumerSalesEntryNo++;
                    }

                }
                else
                {
                    if (cartItem != null) currentConsumerSale.cartItemArray[currentConsumerSale.noOfCartLines] = cartItem;
                    currentConsumerSale.noOfCartLines++;
                }

                i++;
            }


            ConsumerSale[] newConsumerSaleArray = new ConsumerSale[consumerSalesEntryNo];

            int j = 0;

            i = 0;
            while (i < consumerSalesEntryNo)
            {
                if ((consumerSaleArray[i].cartItemArray.Length > 0) || (!cartReleased))
                {
                    newConsumerSaleArray[j] = consumerSaleArray[i];
                    newConsumerSaleArray[j].reOrderWebCartLines();
                    j++;
                }
                i++;
            }


            return newConsumerSaleArray;
        }


        public static CartItemCollection getCartItemCollection(Navipro.Infojet.Lib.Infojet infojetContext, string salesId, string webUserAccountNo)
        {
            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            bool cartReleased = currentSalesID.checkReleasedCart(webUserAccountNo);

            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            string webUserAccountNoQuery = "";
            if (webUserAccountNo != "") webUserAccountNoQuery = "AND cl.[Web User Account No] = @webUserAccountNo";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], cl.[Entry No_], cl.[Item No_], cl.[Unit Of Measure Code], cl.[Unit Price], cl.[Quantity], cl.[Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code], i.[Description], i.[Size] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] cl LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) ON i.[No_] = cl.[Item No_] WHERE cl.[Extra 2] = @salesId " + webUserAccountNoQuery);
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            CartItemCollection cartItemCollection = new CartItemCollection();
          

            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                CartItem cartItem = null;
                
                int entryNo = 0;
                if (int.TryParse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), out entryNo))
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, dataSet.Tables[0].Rows[i]);
                    cartItem = new CartItem(webCartLine);
                    cartItem.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString();

                    if (giftCardTable.Contains(cartItem.itemNo))
                    {
                        cartItem.amount = (cartItem.unitPrice * cartItem.quantity) + (+currentSalesID.profit * (int)giftCardTable[cartItem.itemNo] * cartItem.quantity);
                    }
                    else
                    {
                        cartItem.amount = (cartItem.unitPrice + currentSalesID.profit) * cartItem.quantity;
                    }

                    cartItemCollection.Add(cartItem);
                }



                i++;
            }


            return cartItemCollection;
        }



        public static ConsumerSale[] getHistoryDataSetArray(Navipro.Infojet.Lib.Infojet infojetContext, string salesId, string webUserAccountNo)
        {
            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            string webUserAccountNoQuery = "";
            if (webUserAccountNo != "") webUserAccountNoQuery = "AND wl.[Web User Account No_] = @webUserAccountNo";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT wl.[Document No_], wl.[Line No_], wl.[Item No_], wl.[Unit Of Measure Code], wl.[Unit Price], wl.[Quantity], wl.[Amount], wl.[Extra 1], wl.[Extra 2], wl.[Extra 3], wl.[Extra 4], wl.[Extra 5], wl.[Web User Account No_], '' as reference, GETDATE() as fromDate, GETDATE() as toDate, '', c.[Entry No_], c.[Name], c.[Phone No_], c.[E-Mail], c.[OCR No_], i.[Description], i.[Size], p.[Paid] FROM [" + infojetContext.systemDatabase.getTableName("Consumer") + "] c LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] wl ON c.[Entry No_] = wl.[Extra 4] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) ON i.[No_] = wl.[Item No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("SalesID Consumer Paid") + "] p ON p.[Web User Account No_] = wl.[Web User Account No_] AND p.[Sales ID] = wl.[Extra 2] AND p.[Consumer Entry No_] = wl.[Extra 4] WHERE wl.[Extra 2] = @salesId " + webUserAccountNoQuery + " ORDER BY c.[Entry No_], wl.[Line No_]");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            ConsumerSale[] consumerSaleArray = new ConsumerSale[1000];
            ConsumerSale currentConsumerSale = new ConsumerSale();

            int i = 0;
            int consumerSalesEntryNo = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                CartItem cartItem = null;

                try
                {
                    int entryNo = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                    WebCartLine webCartLine = new WebCartLine(infojetContext, dataSet.Tables[0].Rows[i]);
                    cartItem = new CartItem(webCartLine);
                    cartItem.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(22).ToString();

                    if (giftCardTable.Contains(cartItem.itemNo))
                    {
                        cartItem.amount = (cartItem.unitPrice * cartItem.quantity) + (currentSalesID.profit * (int)giftCardTable[cartItem.itemNo] * cartItem.quantity);
                    }
                    else
                    {
                        cartItem.amount = (cartItem.unitPrice + currentSalesID.profit) * cartItem.quantity;
                    }

                }
                catch (Exception) { }


                Consumer consumer = new Consumer();
                consumer.entryNo = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString());
                consumer.name = dataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString();
                consumer.phoneNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString();
                consumer.email = dataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString();
                consumer.ocrNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString();

                if (consumer.entryNo != currentConsumerSale.consumer.entryNo)
                {
                    ConsumerSale consumerSale = new ConsumerSale();
                    consumerSale.consumer = consumer;
                    consumerSale.cartItemArray[consumerSale.noOfCartLines] = cartItem;
                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(24).ToString() == "1") consumerSale.paid = true;
                    currentConsumerSale = consumerSale;

                    if (currentConsumerSale.cartItemArray[0] != null)
                    {
                        consumerSaleArray[consumerSalesEntryNo] = currentConsumerSale;
                        currentConsumerSale.noOfCartLines++;
                        consumerSalesEntryNo++;
                    }

                }
                else
                {
                    currentConsumerSale.cartItemArray[currentConsumerSale.noOfCartLines] = cartItem;
                    currentConsumerSale.noOfCartLines++;
                }

                i++;
            }

            bool cartReleased = currentSalesID.checkReleasedCart(webUserAccountNo);

            ConsumerSale[] newConsumerSaleArray = new ConsumerSale[consumerSalesEntryNo];
            i = 0;
            int j = 0;
            while (i < consumerSalesEntryNo)
            {
                if ((consumerSaleArray[i].cartItemArray.Length > 0) || (!cartReleased))
                {
                    newConsumerSaleArray[j] = consumerSaleArray[i];
                    newConsumerSaleArray[j].reOrderWebCartLines();
                    j++;
                }
                i++;
            }

            return newConsumerSaleArray;
        }


    }
}
