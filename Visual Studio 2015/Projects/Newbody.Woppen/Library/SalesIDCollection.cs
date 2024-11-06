using System;
using System.Collections;
using System.Linq;
using System.Text;
using Navipro.Infojet.Lib;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// En lista av ett antal SalesID-objekt.
    /// </summary>
    public class SalesIDCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett SalesID-objekt.</returns>
        public SalesID this[int index]
        {
            get { return (SalesID)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett SalesID-objekt i listan.
        /// </summary>
        /// <param name="salesId">Ett SalesID-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(SalesID salesId)
        {
            return (List.Add(salesId));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet SalesID-objekt.
        /// </summary>
        /// <param name="salesId">Ett SalesID-objekt.</param>
        /// <returns>Heltal.</returns>
        public int IndexOf(SalesID salesId)
        {
            return (List.IndexOf(salesId));
        }

        /// <summary>
        /// Lägger in ett SalesID-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="salesId">SalesID-objekt att lägga till i listan.</param>
        public void Insert(int index, SalesID salesId)
        {
            List.Insert(index, salesId);
        }

        /// <summary>
        /// Tar bort ett SalesID-objekt ur listan.
        /// </summary>
        /// <param name="salesId">SalesID-objekt att radera.</param>
        public void Remove(SalesID salesId)
        {
            List.Remove(salesId);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet SalesID-objekt.
        /// </summary>
        /// <param name="salesId">SalesID-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(SalesID salesId)
        {
            int i = 0;
            while (i < List.Count)
            {
                if (((SalesID)List[i]).code == salesId.code) return true;
                i++;
            }

            return false;
        }

        /// <summary>
        /// Märker upp samtliga SalesID-objekt i listan med rätt URL till resp. detaljsida.
        /// </summary>
        /// <param name="pageUrl">Länk</param>
        public void setPageUrl(string pageUrl)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((SalesID)List[i]).pageUrl = pageUrl + "&salesId=" + ((SalesID)List[i]).code;
                i++;
            }

        }

        /// <summary>
        /// Räknar ut det totala antalet paket att beställa (sålda paket - visningspaket + ej sålda visningspaket) för samtliga försäljnings-ID'n i listan. Funktionen tar inte hänsyn till visningspaketen på artikelnivå. (Föråldrad. Bör ej användas.)
        /// </summary>
        /// <returns>Ett heltal</returns>
        public int calcPackages()
        {
            int packages = 0;
            int i = 0;
            while (i < List.Count)
            {
                packages = packages + ((SalesID)List[i]).soldPackages - ((SalesID)List[i]).showCasePackages + ((SalesID)List[i]).returnPackages;
                i++;
            }

            return packages;
        }


        /// <summary>
        /// Räknar ut det toala antalet sålda paket för samtliga försäljnings-ID'n i listan.
        /// </summary>
        /// <returns>Ett heltal.</returns>
        public int calcSoldPackages()
        {
            int packages = 0;
            int i = 0;
            while (i < List.Count)
            {
                packages = packages + ((SalesID)List[i]).soldPackages;
                i++;
            }

            return packages;
        }

        public int calcSoldPackagesInclGiftCards()
        {

            int packages = 0;
            int i = 0;
            while (i < List.Count)
            {
                packages = packages + ((SalesID)List[i]).getSoldPackagesInclGiftCards();
                i++;
            }

            return packages;
        }

        /// <summary>
        /// Räknar ut det totala antalet paket att beställa för samtliga försäljnings-ID'n i listan. Beräkningen räknar bort visningspaket på artikelnivå.
        /// </summary>
        /// <returns>Ett heltal.</returns>
        public int calcPackagesToOrder()
        {
            int packages = 0;
            int i = 0;
            while (i < List.Count)
            {
                float totalQuantity = 0;
                float totalAmount = 0;
                ((SalesID)List[i]).getSalesIdCartLines(out totalQuantity, out totalAmount);
                packages = packages + (int)totalQuantity;
                i++;
            }

            return packages;


        }



        /// <summary>
        /// Räknar ut det totala orderbelopp samt orderantalet inkl. sålda visningspaket för samtliga försäljnings-ID'n i listan.
        /// </summary>
        /// <param name="quantity">Totalt orderantal.</param>
        /// <param name="amount">Totalt orderbelopp.</param>
        public void calcAmount(out float quantity, out float amount)
        {
            amount = 0;
            quantity = 0;
            int i = 0;
            while (i < List.Count)
            {
                OrderItemCollection tempOrderItemCollection = ((SalesID)List[i]).cartLines;

                amount = amount + ((SalesID)List[i]).totalAmount;
                quantity = quantity + ((SalesID)List[i]).totalQuantity;
                i++;
            }

        }

        /// <summary>
        /// Räknar ut den totala vinsten för samtliga försäljnings-ID'n i listan.
        /// </summary>
        /// <param name="profit">Vinsten.</param>
        public void calcProfit(out float profit)
        {
            profit = 0;
            int i = 0;
            while (i < List.Count)
            {
                OrderItemCollection tempOrderItemCollection = ((SalesID)List[i]).cartLines;

                profit = profit + ((SalesID)List[i]).profit;
                i++;
            }

        }

        /// <summary>
        /// Räknar ut det totala fakturerade orderantalet samt orderbeloppet för samtliga försäljnings-ID'n i listan.
        /// </summary>
        /// <param name="quantity">Totalt orderantal.</param>
        /// <param name="amount">Totalt orderbelopp.</param>
        public void calcHistoryAmount(out float quantity, out float amount)
        {
            amount = 0;
            quantity = 0;
            int i = 0;
            while (i < List.Count)
            {
                OrderItemCollection tempOrderItemCollection = ((SalesID)List[i]).historyLines;

                amount = amount + ((SalesID)List[i]).totalHistoryAmount;
                quantity = quantity + ((SalesID)List[i]).totalHistoryQuantity;
                i++;
            }


        }

        /// <summary>
        /// Räknar ut den totala, fakturerade, vinsten för samtliga försäljnings-ID'n i listan.
        /// </summary>
        /// <param name="profit">Vinsten.</param>
        public void calcHistoryProfit(out float profit)
        {
            profit = 0;
            int i = 0;
            while (i < List.Count)
            {
                OrderItemCollection tempOrderItemCollection = ((SalesID)List[i]).historyLines;

                profit = profit + ((SalesID)List[i]).historyProfit;
                i++;
            }

        }

        /// <summary>
        /// Räknar ut det totala, inskickade, orderantalet och orderbeloppet för samtliga försäljnings-ID'n i listan. Gäller fakturerat som ej fakturerat.
        /// </summary>
        /// <param name="quantity">Totalt orderantal.</param>
        /// <param name="amount">Totalt orderbelopp.</param>
        public void calcSentAmount(out float quantity, out float amount)
        {
            amount = 0;
            quantity = 0;
            int i = 0;
            while (i < List.Count)
            {
                OrderItemCollection tempOrderItemCollection = ((SalesID)List[i]).sentLines;

                amount = amount + ((SalesID)List[i]).totalSentAmount;
                quantity = quantity + ((SalesID)List[i]).totalSentQuantity;
                i++;
            }


        }

        public void calcSentAmount2(out float quantity, out float packages, out float amount, out float profitAmount)
        {
            amount = 0;
            quantity = 0;
            packages = 0;
            profitAmount = 0;

            int i = 0;
            while (i < List.Count)
            {
                amount = amount + ((SalesID)List[i]).totalSentAmount;
                quantity = quantity + ((SalesID)List[i]).totalSentQuantity;
                packages = packages + ((SalesID)List[i]).totalSentPackages;
                profitAmount = profitAmount + ((SalesID)List[i]).totalProfitAmount;

                i++;
            }


        }


        public void applySoldPackages(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            string salesIdQuery = "";
            string salesIdQuery2 = "";
            int i = 0;
            while (i < Count)
            {
                if (salesIdQuery != "") salesIdQuery = salesIdQuery + " OR ";
                if (salesIdQuery2 != "") salesIdQuery2 = salesIdQuery2 + " OR ";
                salesIdQuery = salesIdQuery + "[FörsäljningsID] = '" + this[i].code + "'";
                salesIdQuery2 = salesIdQuery2 + "[Extra 2] = '" + this[i].code + "'";

                i++;
            }
            salesIdQuery = "(" + salesIdQuery + ")";
            salesIdQuery2 = "(" + salesIdQuery2 + ")";

            Database database = infojetContext.systemDatabase;

            Hashtable userHistoryPackageTable = new Hashtable();
            Hashtable userHistoryItemTable = new Hashtable();

            DatabaseQuery databaseQuery2 = database.prepare("SELECT [FörsäljningsID], [Web User Account No_], [Ordered Qty], [Item No_] FROM [" + database.getTableName("Web Order Line") + "] WHERE " + salesIdQuery + " AND [Förpackningsmaterial] = 0");

            SqlDataReader dataReader = databaseQuery2.executeQuery();
            while (dataReader.Read())
            {
                string salesIdCode = dataReader.GetValue(0).ToString();
                string webUserAccountNo = dataReader.GetValue(1).ToString();
                
                int historyPackages = 0;
                int historyItems = 0;

                if (userHistoryPackageTable.Contains(salesIdCode + "_" + webUserAccountNo)) historyPackages = int.Parse(userHistoryPackageTable[salesIdCode + "_" + webUserAccountNo].ToString());
                if (userHistoryItemTable.Contains(salesIdCode + "_" + webUserAccountNo)) historyItems = int.Parse(userHistoryItemTable[salesIdCode + "_" + webUserAccountNo].ToString());

                if (giftCardTable.Contains(dataReader.GetValue(3).ToString()))
                {
                    if (!dataReader.IsDBNull(0)) historyPackages = historyPackages + ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(3).ToString()]);
                }
                else
                {
                    if (!dataReader.IsDBNull(0)) historyPackages = historyPackages + (int)float.Parse(dataReader.GetValue(2).ToString());
                }
                if (!dataReader.IsDBNull(0)) historyItems = historyItems + (int)float.Parse(dataReader.GetValue(2).ToString());

                if (!userHistoryPackageTable.Contains(salesIdCode + "_" + webUserAccountNo)) 
                    userHistoryPackageTable.Add(salesIdCode + "_" + webUserAccountNo, historyPackages);
                else
                    userHistoryPackageTable[salesIdCode + "_" + webUserAccountNo] = historyPackages;

                if (!userHistoryItemTable.Contains(salesIdCode + "_" + webUserAccountNo))
                    userHistoryItemTable.Add(salesIdCode + "_" + webUserAccountNo, historyItems);
                else
                    userHistoryItemTable[salesIdCode + "_" + webUserAccountNo] = historyItems;
            }

            dataReader.Close();


            databaseQuery2 = database.prepare("SELECT [Extra 2], [Web User Account No], [Quantity], [Item No_] FROM [" + database.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE " + salesIdQuery2);

            dataReader = databaseQuery2.executeQuery();
            while (dataReader.Read())
            {
                string salesIdCode = dataReader.GetValue(0).ToString();
                string webUserAccountNo = dataReader.GetValue(1).ToString();
                int historyPackages = 0;
                int historyItems = 0;

                if (userHistoryPackageTable.Contains(salesIdCode + "_" + webUserAccountNo)) historyPackages = int.Parse(userHistoryPackageTable[salesIdCode + "_" + webUserAccountNo].ToString());
                if (userHistoryItemTable.Contains(salesIdCode + "_" + webUserAccountNo)) historyItems = int.Parse(userHistoryItemTable[salesIdCode + "_" + webUserAccountNo].ToString());

                if (giftCardTable.Contains(dataReader.GetValue(3).ToString()))
                {
                    if (!dataReader.IsDBNull(0)) historyPackages = historyPackages + ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(3).ToString()]);
                }
                else
                {
                    if (!dataReader.IsDBNull(0)) historyPackages = historyPackages + (int)float.Parse(dataReader.GetValue(2).ToString());
                }
                if (!dataReader.IsDBNull(0)) historyItems = historyItems + (int)float.Parse(dataReader.GetValue(2).ToString());

                if (!userHistoryPackageTable.Contains(salesIdCode + "_" + webUserAccountNo)) 
                    userHistoryPackageTable.Add(salesIdCode + "_" + webUserAccountNo, historyPackages);
                else
                    userHistoryPackageTable[salesIdCode + "_" + webUserAccountNo] = historyPackages;

                if (!userHistoryItemTable.Contains(salesIdCode + "_" + webUserAccountNo))
                    userHistoryItemTable.Add(salesIdCode + "_" + webUserAccountNo, historyItems);
                else
                    userHistoryItemTable[salesIdCode + "_" + webUserAccountNo] = historyItems;

            }
            dataReader.Close();

            i = 0;
            while (i < Count)
            {
                this[i].userHistoryPackageTable = userHistoryPackageTable;
                this[i].userHistoryItemTable = userHistoryItemTable;
                i++;
            }

        }


        public void applySalesIdSentLines(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            string salesIdQuery = "";
            string salesIdQuery2 = "";
            string profitQuery = "";
            int i = 0;
            while (i < Count)
            {
                if (salesIdQuery != "") salesIdQuery = salesIdQuery + " OR ";
                if (salesIdQuery2 != "") salesIdQuery2 = salesIdQuery2 + " OR ";
                if (profitQuery != "") profitQuery = profitQuery + " OR ";
                salesIdQuery = salesIdQuery + "[FörsäljningsID] = '" + this[i].code + "'";
                salesIdQuery2 = salesIdQuery2 + "[Extra 2] = '" + this[i].code + "'";
                profitQuery = profitQuery + "[FörsäljningsID] = '" + this[i].code + "'";

                i++;
            }
            salesIdQuery = "(" + salesIdQuery + ")";
            salesIdQuery2 = "(" + salesIdQuery2 + ")";



            Hashtable profitTable = new Hashtable();
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID], [Profit] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE " + profitQuery);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string salesId = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                string profitText = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                profitTable.Add(salesId, profitText);

                i++;
            }



            Hashtable sentLinesTable = new Hashtable();
            Hashtable totalAmountTable = new Hashtable();
            Hashtable totalQuantityTable = new Hashtable();
            Hashtable totalPackagesTable = new Hashtable();
            Hashtable totalProfitTable = new Hashtable();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], [FörsäljningsID], SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WHERE " + salesIdQuery + " AND [Förpackningsmaterial] = 0 GROUP BY [FörsäljningsID], [Item No_] ORDER BY [FörsäljningsID], [Item No_]");

            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);


            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(dataSet, infojetContext, true, false);


            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string salesId = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                
                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                WebCartLine webCartLine = new WebCartLine(null, null, item);

                webCartLine.quantity = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }


                OrderItemCollection orderItemCollection = (OrderItemCollection)sentLinesTable[salesId];
                if (orderItemCollection == null)
                {
                    orderItemCollection = new OrderItemCollection();
                    sentLinesTable.Add(salesId, orderItemCollection);
                }

                int entryNo = findItemInCollection(orderItemCollection, webCartLine);

                OrderItem orderItem = new OrderItem(webCartLine);
                if (entryNo > -1)
                {
                    orderItem = orderItemCollection[entryNo];
                }
                else
                {
                    entryNo = orderItemCollection.Add(orderItem);
                    orderItem.quantity = 0;
                }

                orderItem.quantity = orderItem.quantity + webCartLine.quantity;
                orderItem.description = item.description;

                orderItem.amount = orderItem.unitPrice * orderItem.quantity;

                orderItemCollection[entryNo] = orderItem;

                //orderItem.amount = orderItem.amount * item.getVatFactor(infojetContext.userSession.customer);
                //orderItem.unitPrice = orderItem.unitPrice * item.getVatFactor(infojetContext.userSession.customer);

                if (totalAmountTable[salesId] == null) totalAmountTable.Add(salesId, (float)0);
                if (totalQuantityTable[salesId] == null) totalQuantityTable.Add(salesId, (float)0);
                if (totalProfitTable[salesId] == null) totalProfitTable.Add(salesId, (float)0);
                if (totalPackagesTable[salesId] == null) totalPackagesTable.Add(salesId, (float)0);

                
                sentLinesTable[salesId] = orderItemCollection;
                
                float totalQuantity = (float)totalQuantityTable[salesId];
                totalQuantity = totalQuantity + webCartLine.quantity;
                totalQuantityTable[salesId] = totalQuantity;

                float totalAmount = (float)totalAmountTable[salesId];
                totalAmount = totalAmount + (orderItem.unitPrice * webCartLine.quantity);
                totalAmountTable[salesId] = totalAmount;


                float totalProfit = (float)totalProfitTable[salesId];
                float totalPackages = (float)totalPackagesTable[salesId];

                if (giftCardTable.Contains(webCartLine.itemNo))
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo];
                    orderItem.profitText = orderItem.quantityPackages + " x " + (float.Parse(profitTable[salesId].ToString())) + " = " + String.Format("{0:n2}", ((int)giftCardTable[webCartLine.itemNo] * webCartLine.quantity * (float.Parse(profitTable[salesId].ToString()))));
                    totalProfit = totalProfit + ((int)giftCardTable[webCartLine.itemNo]*webCartLine.quantity*(float.Parse(profitTable[salesId].ToString())));

                    totalPackages = totalPackages + (webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo]);

                }
                else
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity;
                    orderItem.profitText = webCartLine.quantity + " x " + (float.Parse(profitTable[salesId].ToString())) + " = " + String.Format("{0:n2}", (webCartLine.quantity * (float.Parse(profitTable[salesId].ToString()))));
                    totalProfit = totalProfit + (webCartLine.quantity * (float.Parse(profitTable[salesId].ToString())));

                    totalPackages = totalPackages + webCartLine.quantity;

                }

                totalProfitTable[salesId] = totalProfit;
                totalPackagesTable[salesId] = totalPackages;



                i++;
            }

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], [Extra 2], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE "+salesIdQuery2+" GROUP BY [Extra 2], [Item No_] ORDER BY [Extra 2], [Item No_]");

            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            itemInfoTable = items.getItemInfo(dataSet, infojetContext, true, false);


            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string salesId = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                WebCartLine webCartLine = new WebCartLine(null, null, item);

                webCartLine.quantity = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }

                OrderItemCollection orderItemCollection = (OrderItemCollection)sentLinesTable[salesId];
                if (orderItemCollection == null)
                {
                    orderItemCollection = new OrderItemCollection();
                    sentLinesTable.Add(salesId, orderItemCollection);
                }

                int entryNo = findItemInCollection(orderItemCollection, webCartLine);

                OrderItem orderItem = new OrderItem(webCartLine);                
                if (entryNo > -1)
                {
                    orderItem = orderItemCollection[entryNo];
                }
                else
                {
                    entryNo = orderItemCollection.Add(orderItem);
                    orderItem.quantity = 0;
                }

                orderItem.quantity = orderItem.quantity + webCartLine.quantity;
                orderItem.description = item.description;
                if (orderItem.quantityToOrder < 0) orderItem.quantityToOrder = 0;

                orderItem.amount = orderItem.unitPrice * orderItem.quantity;

                orderItemCollection[entryNo] = orderItem;

                //orderItem.amount = orderItem.amount * item.getVatFactor(infojetContext.userSession.customer);
                //orderItem.unitPrice = orderItem.unitPrice * item.getVatFactor(infojetContext.userSession.customer);

                if (totalAmountTable[salesId] == null) totalAmountTable.Add(salesId, (float)0);
                if (totalQuantityTable[salesId] == null) totalQuantityTable.Add(salesId, (float)0);
                if (totalProfitTable[salesId] == null) totalProfitTable.Add(salesId, (float)0);
                if (totalPackagesTable[salesId] == null) totalPackagesTable.Add(salesId, (float)0);

                sentLinesTable[salesId] = orderItemCollection;


                float totalQuantity = (float)totalQuantityTable[salesId];
                totalQuantity = totalQuantity + webCartLine.quantity;
                totalQuantityTable[salesId] = totalQuantity;

                float totalAmount = (float)totalAmountTable[salesId];
                totalAmount = totalAmount + (orderItem.unitPrice * webCartLine.quantity);
                totalAmountTable[salesId] = totalAmount;

                float totalProfit = (float)totalProfitTable[salesId];
                float totalPackages = (float)totalPackagesTable[salesId];

                if (giftCardTable.Contains(webCartLine.itemNo))
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo];
                    orderItem.profitText = orderItem.quantityPackages + " x " + (float.Parse(profitTable[salesId].ToString())) + " = " + String.Format("{0:n2}", ((int)giftCardTable[webCartLine.itemNo] * webCartLine.quantity * (float.Parse(profitTable[salesId].ToString()))));
                    totalProfit = totalProfit + ((int)giftCardTable[webCartLine.itemNo] * webCartLine.quantity * (float.Parse(profitTable[salesId].ToString())));

                    totalPackages = totalPackages + (webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo]);

                }
                else
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity;
                    orderItem.profitText = webCartLine.quantity + " x " + (float.Parse(profitTable[salesId].ToString())) + " = " + String.Format("{0:n2}", (webCartLine.quantity * (float.Parse(profitTable[salesId].ToString()))));
                    totalProfit = totalProfit + (webCartLine.quantity * (float.Parse(profitTable[salesId].ToString())));

                    totalPackages = totalPackages + webCartLine.quantity;

                }

                totalProfitTable[salesId] = totalProfit;
                totalPackagesTable[salesId] = totalPackages;

                i++;
            }


            i = 0;
            while (i < Count)
            {
                this[i].sentLinesCollection = (OrderItemCollection)sentLinesTable[this[i].code];
                if (this[i].sentLinesCollection == null) this[i].sentLinesCollection = new OrderItemCollection();
                if (totalAmountTable[this[i].code] != null) this[i].totalSentAmount = (float)totalAmountTable[this[i].code];
                if (totalQuantityTable[this[i].code] != null) this[i].totalSentQuantity = (float)totalQuantityTable[this[i].code];
                if (totalProfitTable[this[i].code] != null) this[i].totalProfitAmount = (float)totalProfitTable[this[i].code];
                if (totalPackagesTable[this[i].code] != null) this[i].totalSentPackages = (float)totalPackagesTable[this[i].code];
                i++;
            }

           
        }

        private int findItemInCollection(OrderItemCollection orderItemCollection, WebCartLine webCartLine)
        {
            int i = 0;
            while (i < orderItemCollection.Count)
            {
                if (orderItemCollection[i].itemNo == webCartLine.itemNo)
                {
                    return i;
                }
                i++;
            }

            return -1;

        }
    }
}
