using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class SalesInvoiceHeader
    {
        private string _no;
        private string _sellToCustomerNo;
        private string _billToCustomerNo;
        private string _billToName;
        private string _billToName2;
        private string _billToAddress;
        private string _billToAddress2;
        private string _billToCity;
        private string _billToContact;
        private string _yourReference;
        private string _shipToName;
        private string _shipToName2;
        private string _shipToAddress;
        private string _shipToAddress2;
        private string _shipToCity;
        private string _shipToContact;
        private DateTime _postingDate;
        private DateTime _dueDate;
        private DateTime _documentDate;
        private string _shortcutDimension1Code;
        private string _shortcutDimension2Code;
        private string _customerPostingGroup;
        private string _currencyCode;
        private bool _pricesIncludingVat;
        private string _vatRegistrationNo;
        private string _sellToCustomerName;
        private string _sellToCustomerName2;
        private string _sellToAddress;
        private string _sellToAddress2;
        private string _sellToCity;
        private string _sellToContact;
        private string _billToPostCode;
        private string _sellToPostCode;
        private string _shipToPostCode;
        private string _externalDocumentNo;
        private string _noteOfGoods;
        private string _ocrNo;
        private SalesInvoiceLineCollection _salesInvoiceLines;

        public SalesInvoiceHeader() { }

        public SalesInvoiceHeader(SqlDataReader dataReader)
        {
            _no = dataReader.GetValue(0).ToString();
            _sellToCustomerNo = dataReader.GetValue(1).ToString();
            _billToCustomerNo = dataReader.GetValue(2).ToString();
            _billToName = dataReader.GetValue(3).ToString();
            _billToName2 = dataReader.GetValue(4).ToString();
            _billToAddress = dataReader.GetValue(5).ToString();
            _billToAddress2 = dataReader.GetValue(6).ToString();
            _billToCity = dataReader.GetValue(7).ToString();
            _billToContact = dataReader.GetValue(8).ToString();
            _yourReference = dataReader.GetValue(9).ToString();
            _shipToName = dataReader.GetValue(10).ToString();
            _shipToName2 = dataReader.GetValue(11).ToString();
            _shipToAddress = dataReader.GetValue(12).ToString();
            _shipToAddress2 = dataReader.GetValue(13).ToString();
            _shipToCity = dataReader.GetValue(14).ToString();
            _shipToContact = dataReader.GetValue(15).ToString();
            _postingDate = dataReader.GetDateTime(16);
            _dueDate = dataReader.GetDateTime(17);
            _documentDate = dataReader.GetDateTime(18);
            _shortcutDimension1Code = dataReader.GetValue(19).ToString();
            _shortcutDimension2Code = dataReader.GetValue(20).ToString();
            _customerPostingGroup = dataReader.GetValue(21).ToString();
            _currencyCode = dataReader.GetValue(22).ToString();
            
            _pricesIncludingVat = false;
            if (dataReader.GetValue(23).ToString() == "1") _pricesIncludingVat = true;

            _vatRegistrationNo = dataReader.GetValue(24).ToString();
            _sellToCustomerName = dataReader.GetValue(25).ToString();
            _sellToCustomerName2 = dataReader.GetValue(26).ToString();
            _sellToAddress = dataReader.GetValue(27).ToString();
            _sellToAddress2 = dataReader.GetValue(28).ToString();
            _sellToCity = dataReader.GetValue(29).ToString();
            _sellToContact = dataReader.GetValue(30).ToString();
            _billToPostCode = dataReader.GetValue(31).ToString();
            _sellToPostCode = dataReader.GetValue(32).ToString();
            _shipToPostCode = dataReader.GetValue(33).ToString();
            _externalDocumentNo = dataReader.GetValue(34).ToString();
            _noteOfGoods = dataReader.GetValue(35).ToString();
            _ocrNo = dataReader.GetValue(36).ToString();

        }

        public SalesInvoiceHeader(DataRow dataRow)
        {
            _no = dataRow.ItemArray.GetValue(0).ToString();
            _sellToCustomerNo = dataRow.ItemArray.GetValue(1).ToString();
            _billToCustomerNo = dataRow.ItemArray.GetValue(2).ToString();
            _billToName = dataRow.ItemArray.GetValue(3).ToString();
            _billToName2 = dataRow.ItemArray.GetValue(4).ToString();
            _billToAddress = dataRow.ItemArray.GetValue(5).ToString();
            _billToAddress2 = dataRow.ItemArray.GetValue(6).ToString();
            _billToCity = dataRow.ItemArray.GetValue(7).ToString();
            _billToContact = dataRow.ItemArray.GetValue(8).ToString();
            _yourReference = dataRow.ItemArray.GetValue(9).ToString();
            _shipToName = dataRow.ItemArray.GetValue(10).ToString();
            _shipToName2 = dataRow.ItemArray.GetValue(11).ToString();
            _shipToAddress = dataRow.ItemArray.GetValue(12).ToString();
            _shipToAddress2 = dataRow.ItemArray.GetValue(13).ToString();
            _shipToCity = dataRow.ItemArray.GetValue(14).ToString();
            _shipToContact = dataRow.ItemArray.GetValue(15).ToString();
            _postingDate = DateTime.Parse(dataRow.ItemArray.GetValue(16).ToString());
            _dueDate = DateTime.Parse(dataRow.ItemArray.GetValue(17).ToString());
            _documentDate = DateTime.Parse(dataRow.ItemArray.GetValue(18).ToString());
            _shortcutDimension1Code = dataRow.ItemArray.GetValue(19).ToString();
            _shortcutDimension2Code = dataRow.ItemArray.GetValue(20).ToString();
            _customerPostingGroup = dataRow.ItemArray.GetValue(21).ToString();
            _currencyCode = dataRow.ItemArray.GetValue(22).ToString();

            _pricesIncludingVat = false;
            if (dataRow.ItemArray.GetValue(23).ToString() == "1") _pricesIncludingVat = true;

            _vatRegistrationNo = dataRow.ItemArray.GetValue(24).ToString();
            _sellToCustomerName = dataRow.ItemArray.GetValue(25).ToString();
            _sellToCustomerName2 = dataRow.ItemArray.GetValue(26).ToString();
            _sellToAddress = dataRow.ItemArray.GetValue(27).ToString();
            _sellToAddress2 = dataRow.ItemArray.GetValue(28).ToString();
            _sellToCity = dataRow.ItemArray.GetValue(29).ToString();
            _sellToContact = dataRow.ItemArray.GetValue(30).ToString();
            _billToPostCode = dataRow.ItemArray.GetValue(31).ToString();
            _sellToPostCode = dataRow.ItemArray.GetValue(32).ToString();
            _shipToPostCode = dataRow.ItemArray.GetValue(33).ToString();
            _externalDocumentNo = dataRow.ItemArray.GetValue(34).ToString();
            _noteOfGoods = dataRow.ItemArray.GetValue(35).ToString();
            _ocrNo = dataRow.ItemArray.GetValue(36).ToString();

        }

        public string no { get { return _no; } set { _no = value; } }
        public string sellToCustomerNo { get { return _sellToCustomerNo; } set { _sellToCustomerNo = value; } }
        public string billToCustomerNo { get { return _billToCustomerNo; } set { _billToCustomerNo = value; } }
        public string billToName { get { return _billToName; } set { _billToName = value; } }
        public string billToName2 { get { return _billToName2; } set { _billToName2 = value; } }
        public string billToAddress { get { return _billToAddress; } set { _billToAddress = value; } }
        public string billToAddress2 { get { return _billToAddress2; } set { _billToAddress2 = value; } }
        public string billToCity { get { return _billToCity; } set { _billToCity = value; } }
        public string billToContact { get { return _billToContact; } set { _billToContact = value; } }
        public string yourReference { get { return _yourReference; } set { _yourReference = value; } }
        public string shipToName { get { return _shipToName; } set { _shipToName = value; } }
        public string shipToName2 { get { return _shipToName2; } set { _shipToName2 = value; } }
        public string shipToAddress { get { return _shipToAddress; } set { _shipToAddress = value; } }
        public string shipToAddress2 { get { return _shipToAddress2; } set { _shipToAddress2 = value; } }
        public string shipToCity { get { return _shipToCity; } set { _shipToCity = value; } }
        public string shipToContact { get { return _shipToContact; } set { _shipToContact = value; } }
        public DateTime postingDate { get { return _postingDate; } set { _postingDate = value; } }
        public DateTime dueDate { get { return _dueDate; } set { _dueDate = value; } }
        public DateTime documentDate { get { return _documentDate; } set { _documentDate = value; } }
        public string shortcutDimension1Code { get { return _shortcutDimension1Code; } set { _shortcutDimension1Code = value; } }
        public string shortcutDimension2Code { get { return _shortcutDimension2Code; } set { _shortcutDimension2Code = value; } }
        public string customerPostingGroup { get { return _customerPostingGroup; } set { _customerPostingGroup = value; } }
        public string currencyCode { get { return _currencyCode; } set { _currencyCode = value; } }
        public bool pricesIncludingVat { get { return _pricesIncludingVat; } set { _pricesIncludingVat = value; } }
        public string vatRegistrationNo { get { return _vatRegistrationNo; } set { _vatRegistrationNo = value; } }
        public string sellToCustomerName { get { return _sellToCustomerName; } set { _sellToCustomerName = value; } }
        public string sellToCustomerName2 { get { return _sellToCustomerName2; } set { _sellToCustomerName2 = value; } }
        public string sellToAddress { get { return _sellToAddress; } set { _sellToAddress = value; } }
        public string sellToAddress2 { get { return _sellToAddress2; } set { _sellToAddress2 = value; } }
        public string sellToCity { get { return _sellToCity; } set { _sellToCity = value; } }
        public string sellToContact { get { return _sellToContact; } set { _sellToContact = value; } }
        public string billToPostCode { get { return _billToPostCode; } set { _billToPostCode = value; } }
        public string sellToPostCode { get { return _sellToPostCode; } set { _sellToPostCode = value; } }
        public string shipToPostCode { get { return _shipToPostCode; } set { _shipToPostCode = value; } }
        public string externalDocumentNo { get { return _externalDocumentNo; } set { _externalDocumentNo = value; } }
        public string noteOfGoods { get { return _noteOfGoods; } set { _noteOfGoods = value; } }
        public string ocrNo { get { return _ocrNo; } set { _ocrNo = value; } }
        public SalesInvoiceLineCollection salesInvoiceLines { get { return _salesInvoiceLines; } set { _salesInvoiceLines = value; } }


        public static SalesInvoiceHeader getEntry(Database database, string no)
        {
            SalesInvoiceHeader salesInvoiceHeader = null;

            string dateFilter = "";
            if (database.getCompanyName() != "EuroPark Svenska AB") dateFilter = "AND [Posting Date] >= '2017-04-01'";

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], "+
            "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], "+
            "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], "+
            "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Invoice Header") + "] WHERE [No_] = @no "+dateFilter);

            databaseQuery.addStringParameter("no", no, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);


            if (dataSet.Tables[0].Rows.Count > 0)
            {
                salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[0]);
                salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getInvoiceLines(database, salesInvoiceHeader.no);
            }
            else
            {
                databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], " +
                "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], " +
                "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], " +
                "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Cr_Memo Header") + "] WHERE [No_] = @no "+dateFilter);

                databaseQuery.addStringParameter("no", no, 20);

                dataAdapter = databaseQuery.executeDataAdapterQuery();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[0]);
                    salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getCreditMemoLines(database, salesInvoiceHeader.no);
                }

            }

            return salesInvoiceHeader;
        }

        public static SalesInvoiceHeader getOcrEntry(Database database, string ocrNo)
        {
            SalesInvoiceHeader salesInvoiceHeader = null;

            string dateFilter = "";
            if (database.getCompanyName() != "EuroPark Svenska AB") dateFilter = "AND [Posting Date] >= '2017-04-01'";

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], " +
            "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], " +
            "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], " +
            "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Invoice Header") + "] WHERE [OCR nr] = @no " + dateFilter);

            databaseQuery.addStringParameter("no", ocrNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);


            if (dataSet.Tables[0].Rows.Count > 0)
            {
                salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[0]);
                salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getInvoiceLines(database, salesInvoiceHeader.no);
            }
            else
            {
                databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], " +
                "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], " +
                "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], " +
                "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Cr_Memo Header") + "] WHERE [OCR nr] = @no " + dateFilter);

                databaseQuery.addStringParameter("no", ocrNo, 20);

                dataAdapter = databaseQuery.executeDataAdapterQuery();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[0]);
                    salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getCreditMemoLines(database, salesInvoiceHeader.no);
                }

            }

            return salesInvoiceHeader;
        }


        public static SalesInvoiceHeaderCollection getEntries(Database database, string billToCustomerNo, DateTime fromDate, DateTime toDate)
        {
            if (database.getCompanyName() != "EuroPark Svenska AB") if (fromDate < DateTime.Parse("2017-04-01 00:00:00")) fromDate = DateTime.Parse("2017-04-01 00:00:00");

            SalesInvoiceHeaderCollection salesInvoiceHeaderCollection = new SalesInvoiceHeaderCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], " +
            "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], " +
            "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], " +
            "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Invoice Header") + "] WHERE [Bill-to Customer No_] = @billToCustomerNo AND [Posting Date] >= @fromDate AND [Posting Date] <= @toDate");

            databaseQuery.addStringParameter("billToCustomerNo", billToCustomerNo, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesInvoiceHeader salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[i]);
                salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getInvoiceLines(database, salesInvoiceHeader.no);
                salesInvoiceHeaderCollection.Add(salesInvoiceHeader);
                i++;

            }

            databaseQuery = database.prepare("SELECT [No_], [Sell-to Customer No_], [Bill-to Customer No_], [Bill-to Name], [Bill-to Name 2], [Bill-to Address], [Bill-to Address 2], [Bill-to City], [Bill-to Contact], [Your Reference], " +
            "[Ship-to Name], [Ship-to Name 2], [Ship-to Address], [Ship-to Address 2], [Ship-to City], [Ship-to Contact], [Posting Date], [Due Date], [Document Date], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Customer Posting Group], [Currency Code], [Prices Including VAT], " +
            "[VAT Registration No_], [Sell-to Customer Name], [Sell-to Customer Name 2], [Sell-to Address], [Sell-to Address 2], [Sell-to City], [Sell-to Contact], [Bill-to Post Code], [Sell-to Post Code], [Ship-to Post Code], [External Document No_], " +
            "[Note of Goods], [OCR nr] FROM [" + database.getTableName("Sales Cr_Memo Header") + "] WHERE [Bill-to Customer No_] = @billToCustomerNo AND [Posting Date] >= @fromDate AND [Posting Date] <= @toDate");

            databaseQuery.addStringParameter("billToCustomerNo", billToCustomerNo, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesInvoiceHeader salesInvoiceHeader = new SalesInvoiceHeader(dataSet.Tables[0].Rows[i]);
                salesInvoiceHeader.salesInvoiceLines = SalesInvoiceLine.getCreditMemoLines(database, salesInvoiceHeader.no);
                salesInvoiceHeaderCollection.Add(salesInvoiceHeader);
                i++;

            }


            return salesInvoiceHeaderCollection;
        }
    }
}
