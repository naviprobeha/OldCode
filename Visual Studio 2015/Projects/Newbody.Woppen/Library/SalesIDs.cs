using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// Klassen innehåller funktioner som returnerar ett eller flera försäljnings-ID'n, antingen som dataset eller som SalesIDCollection-objekt.
    /// </summary>
    public class SalesIDs
    {
        /// <summary>
        /// Blank konstruktor.
        /// </summary>
        public SalesIDs()
        {
        }

        /// <summary>
        /// Sökfunktion som söker på kundnr eller del av kundnr. Returnerar ett SalesIDCollection-objekt med försäljnings-ID'n som matchar sökningen.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="customerNoFilter">Kundnrfilter, helt kundnr eller del av kundnr.</param>
        /// <returns>En hårt typad lista av SalesID-objekt.</returns>
        public SalesIDCollection searchSalesIdCollection(Navipro.Infojet.Lib.Infojet infojetContext, string customerNoFilter)
        {
            SalesIDCollection salesIdCollection = new SalesIDCollection();

            string customerNoQuery = "";
            if (customerNoFilter != "") customerNoQuery = "AND [Kund] LIKE '%" + customerNoFilter + "%'";


            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[FörsäljningsID], s.[Försäljningskoncept], s.[Antal säljare], s.[Visningskasse], s.[Artikelsortiment], s.[Ordervecka Slutorder], s.[Stängningsdatum], s.[Benämning], s.[Kund], s.[Contact Web User Account No_], s.[User Reg_ Web User Account No_], s.[Next Ordertype], wu.[Name], wu.[User-ID], wu.[Password] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s, [" + infojetContext.systemDatabase.getTableName("Web User Account") + "] wu WHERE s.[Contact Web User Account No_] <> '' AND s.[Contact Web User Account No_] = wu.[No_] "+customerNoQuery);
  
            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesID salesId = new SalesID(infojetContext, dataSet.Tables[0].Rows[i]);
                salesId.contactName = dataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString();
                salesId.contactUserId = dataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString();
                salesId.contactPassword = dataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString();

                salesIdCollection.Add(salesId);

                i++;
            }

            return salesIdCollection;
        }

        /// <summary>
        /// Returnerar ett dataset med försäljnings-ID'n där kontaktpersonen matchar sökningen. (Föråldrad. Bör ej användas.)
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="webUserAccount">Kontaktpersonens användarkonto.</param>
        /// <returns>Dataset</returns>
        public DataSet getContactSalesIds(Database database, WebUserAccount webUserAccount)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Mobile] FROM [" + database.getTableName("FörsäljningsID") + "] WHERE (([Contact Web User Account No_] = @webUserAccountNo) OR ([Sub Cont Web User Account No_] = @webUserAccountNo)) AND [Kund] = @customerNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccount.no, 20);
            databaseQuery.addStringParameter("@customerNo", webUserAccount.customerNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }



        public bool checkIfNonAgreedFidsExists(Database database, WebUserAccount webUserAccount)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [FörsäljningsID] FROM [" + database.getTableName("FörsäljningsID") + "] WHERE [Contact Web User Account No_] = @webUserAccountNo AND [Kund] = @customerNo AND [Stängningsdatum] > GETDATE() AND [Agreement Approved Date] < '1900-01-01'");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccount.no, 20);
            databaseQuery.addStringParameter("@customerNo", webUserAccount.customerNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0) return true;
            return false;
        }

        /// <summary>
        /// Returnerar ett SalesIDCollection-objekt med försäljnings-ID'n där kontaktpersonen matchar sökningen.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="webUserAccount">Kontaktpersonens användarkonto.</param>
        /// <returns>En hårt typad lista av SalesID-objekt.</returns>
        public SalesIDCollection getContactSalesIdCollection(Navipro.Infojet.Lib.Infojet infojetContext, WebUserAccount webUserAccount)
        {
            SalesIDCollection salesIdCollection = new SalesIDCollection();

            DataSet dataSet = getContactSalesIds(infojetContext.systemDatabase, webUserAccount);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesID salesId = new SalesID(infojetContext, dataSet.Tables[0].Rows[i]);
                //salesId.cleanUpSalesPersons();
                salesIdCollection.Add(salesId);

                i++;
            }

            return salesIdCollection;
        }

        /// <summary>
        /// Returnerar ett SalesIDCollection-objekt med försäljnings-ID'n där startdatum matchar sökningen.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="startDate">Startdatum.</param>
        /// <returns>En hårt typad lista av SalesID-objekt.</returns>
        public SalesIDCollection getSalesIdsPerStartDate(Navipro.Infojet.Lib.Infojet infojetContext, DateTime startDate)
        {
            SalesIDCollection salesIdCollection = new SalesIDCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Mobile] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [Kund] = @customerNo AND [Leveransdatum startorder] = @startDate");
            databaseQuery.addStringParameter("@customerNo", infojetContext.userSession.customer.no, 20);
            databaseQuery.addDateTimeParameter("@startDate", startDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                SalesID salesId = new SalesID(infojetContext, dataReader);
                salesIdCollection.Add(salesId);

            }

            dataReader.Close();

            return salesIdCollection;
        }

        /// <summary>
        /// Returnerar det försäljnings-ID som en säljare är kopplad till.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="webUserAccount">Kontaktpersonens användarkonto.</param>
        /// <returns>Ett SalesID-objekt.</returns>
        public SalesID getSalesPersonSalesId(Navipro.Infojet.Lib.Infojet infojetContext, WebUserAccount webUserAccount)
        {
            SalesID salesId = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[FörsäljningsID], s.[Försäljningskoncept], s.[Antal säljare], s.[Visningskasse], s.[Artikelsortiment], s.[Ordervecka Slutorder], s.[Stängningsdatum], s.[Benämning], s.[Kund], s.[Contact Web User Account No_], s.[User Reg_ Web User Account No_], s.[Next Ordertype], s.[Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Mobile] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] w WITH (NOLOCK) WHERE w.[Web User Account] = @webUserAccountNo AND w.[FÖrsäljningsID] = s.[FörsäljningsID] AND s.[Kund] = @customerNo");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccount.no, 20);
            databaseQuery.addStringParameter("@customerNo", webUserAccount.customerNo, 20);

            SqlDataReader sqlDataReader = databaseQuery.executeQuery();
            if (sqlDataReader.Read())
            {
                salesId = new SalesID(infojetContext, sqlDataReader);
            }

            sqlDataReader.Close();

            return salesId;
        }

        /// <summary>
        /// Returnerar det försäljnings-ID som angivet registreringskonto är kopplad till.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="webUserAccount">Registeringskontot.</param>
        /// <returns>Ett SalesID-objekt.</returns>
        public SalesID getUserRegSalesId(Navipro.Infojet.Lib.Infojet infojetContext, WebUserAccount webUserAccount)
        {
            SalesID salesId = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Mobile] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [User Reg_ Web User Account No_] = @webUserAccountNo AND [Kund] = @customerNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccount.no, 20);
            databaseQuery.addStringParameter("@customerNo", webUserAccount.customerNo, 20);

            SqlDataReader sqlDataReader = databaseQuery.executeQuery();
            if (sqlDataReader.Read())
            {
                salesId = new SalesID(infojetContext, sqlDataReader);
            }

            sqlDataReader.Close();

            return salesId;
        }

        /// <summary>
        /// Kontrollerar samtliga orderrader så att dessa ingår i något försäljnings-ID i listan.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="salesIdList">Lista på försäljnings-ID'n.</param>
        public void checkCartLines(Navipro.Infojet.Lib.Infojet infojetContext, SalesIDCollection salesIdList)
        {
            WebCartLines webCartLines = new WebCartLines(infojetContext.systemDatabase);
            DataSet cartDataSet = webCartLines.getCartLines(infojetContext.sessionId);
            int i = 0;
            while (i < cartDataSet.Tables[0].Rows.Count)
            {
                WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);
                bool correctSalesId = false;
                int j = 0;
                while (j < salesIdList.Count)
                {
                    if (salesIdList[j].code == webCartLine.extra2) correctSalesId = true;
                    j++;
                }

                if (!correctSalesId)
                {
                    infojetContext.systemDatabase.nonQuery("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Session ID] = '1' WHERE [Entry No_] = '" + webCartLine.entryNo + "'");
                }

                i++;
            }

        }
    }
}
