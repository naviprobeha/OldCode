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
    public class SalesInvoiceLine
    {

        private string _documentNo;
        private int _lineNo;
        private int _type;
        private string _no;
        private string _description;
        private string _description2;
        private decimal _quantity;
        private decimal _unitPrice;
        private decimal _vatProc;
        private string _shortcutDimension1Code;
        private string _shortcutDimension2Code;
        private decimal _lineAmount;
        private string _contractNo;

        public SalesInvoiceLine() { }

        public SalesInvoiceLine(SqlDataReader dataReader)
        {
            _documentNo = dataReader.GetValue(0).ToString();
            _lineNo = dataReader.GetInt32(1);
            _type = dataReader.GetInt32(2);
            _no = dataReader.GetValue(3).ToString();
            _description = dataReader.GetValue(4).ToString();
            _description2 = dataReader.GetValue(5).ToString();
            _quantity = dataReader.GetDecimal(6);
            _unitPrice = dataReader.GetDecimal(7);
            _vatProc = dataReader.GetDecimal(8);
            _shortcutDimension1Code = dataReader.GetValue(9).ToString();
            _shortcutDimension2Code = dataReader.GetValue(10).ToString();
            _lineAmount = dataReader.GetDecimal(11);
            _contractNo = dataReader.GetValue(12).ToString();

        }

        public SalesInvoiceLine(DataRow dataRow)
        {
            _documentNo = dataRow.ItemArray.GetValue(0).ToString();
            _lineNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            _type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            _no = dataRow.ItemArray.GetValue(3).ToString();
            _description = dataRow.ItemArray.GetValue(4).ToString();
            _description2 = dataRow.ItemArray.GetValue(5).ToString();
            _quantity = decimal.Parse(dataRow.ItemArray.GetValue(6).ToString());
            _unitPrice = decimal.Parse(dataRow.ItemArray.GetValue(7).ToString());
            _vatProc = decimal.Parse(dataRow.ItemArray.GetValue(8).ToString());
            _shortcutDimension1Code = dataRow.ItemArray.GetValue(9).ToString();
            _shortcutDimension2Code = dataRow.ItemArray.GetValue(10).ToString();
            _lineAmount = decimal.Parse(dataRow.ItemArray.GetValue(11).ToString());
            _contractNo = dataRow.ItemArray.GetValue(12).ToString();

        }

        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string no { get { return _no; } set { _no = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public decimal quantity { get { return _quantity; } set { _quantity = value; } }
        public decimal unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public decimal vatProc { get { return _vatProc; } set { _vatProc = value; } }
        public string shortcutDimension1Code { get { return _shortcutDimension1Code; } set { _shortcutDimension1Code = value; } }
        public string shortcutDimension2Code { get { return _shortcutDimension2Code; } set { _shortcutDimension2Code = value; } }
        public decimal lineAmount { get { return _lineAmount; } set { _lineAmount = value; } }
        public string contractNo { get { return _contractNo; } set { _contractNo = value; } }


        public static SalesInvoiceLineCollection getInvoiceLines(Database database, string salesInvoiceHeaderNo)
        {
            SalesInvoiceLineCollection salesInvoiceLineCollection = new SalesInvoiceLineCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Document No_], [Line No_], [Type], [No_], [Description], [Description 2], [Quantity], [Unit Price], [VAT %], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Line Amount], [Contract No_] FROM [" + database.getTableName("Sales Invoice Line") + "] WHERE [Document No_] = @salesInvoiceHeaderNo");

            databaseQuery.addStringParameter("salesInvoiceHeaderNo", salesInvoiceHeaderNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesInvoiceLine salesInvoiceLine = new SalesInvoiceLine(dataSet.Tables[0].Rows[i]);
                salesInvoiceLineCollection.Add(salesInvoiceLine);

                i++;
            }

            return salesInvoiceLineCollection;
        }

        public static SalesInvoiceLineCollection getCreditMemoLines(Database database, string salesCrMemoHeaderNo)
        {
            SalesInvoiceLineCollection salesInvoiceLineCollection = new SalesInvoiceLineCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Document No_], [Line No_], [Type], [No_], [Description], [Description 2], [Quantity], [Unit Price], [VAT %], [Shortcut Dimension 1 Code], [Shortcut Dimension 2 Code], [Line Amount], [Contract No_] FROM [" + database.getTableName("Sales Cr_Memo Line") + "] WHERE [Document No_] = @salesCrMemoHeaderNo");

            databaseQuery.addStringParameter("salesCrMemoHeaderNo", salesCrMemoHeaderNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesInvoiceLine salesInvoiceLine = new SalesInvoiceLine(dataSet.Tables[0].Rows[i]);
                salesInvoiceLineCollection.Add(salesInvoiceLine);

                i++;
            }

            return salesInvoiceLineCollection;
        }

    }

}
