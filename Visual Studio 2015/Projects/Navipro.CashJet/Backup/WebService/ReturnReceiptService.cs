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
using System.Xml;
using System.Data.SqlClient;

namespace Navipro.CashJet.WebService
{
    public class ReturnReceiptService
    {
        private Configuration configuration;
        private Database database;

        public ReturnReceiptService(Configuration configuration)
        {
            this.configuration = configuration;
            this.database = new Database(configuration);
        }

        public XmlDocument performService(XmlDocument xmlDocument)
        {
            XmlDocument responseDoc = new XmlDocument();
            responseDoc.LoadXml("<nav/>");
            XmlElement responseDocElement = responseDoc.DocumentElement;
            XmlElement responseElement = addElement(responseDocElement, "serviceResponse", "", "");
            responseElement = addElement(responseElement, "receipts", "", "");
            
            XmlElement receiptNoElement = (XmlElement)xmlDocument.SelectSingleNode("nav/serviceRequest/serviceArgument/receiptNo");
            if (receiptNoElement != null)
            {
                string receiptNo = receiptNoElement.FirstChild.Value;
            
                composeReceiptInfo(ref responseElement, receiptNo);

                DataSet returnReceiptDataSet = getOpenReturnReceipts(receiptNo);
                int i = 0;
                while (i < returnReceiptDataSet.Tables[0].Rows.Count)
                {
                    string returnReceiptNo = returnReceiptDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    composeReceiptInfo(ref responseElement, returnReceiptNo);

                    i++;
                }

                DataSet postedReturnReceiptDataSet = getPostedReturnReceipts(receiptNo);
                i = 0;
                while (i < postedReturnReceiptDataSet.Tables[0].Rows.Count)
                {
                    string returnReceiptNo = postedReturnReceiptDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    composeReceiptInfo(ref responseElement, returnReceiptNo);

                    i++;
                }

            }

            return responseDoc;
        }

        private XmlElement addElement(XmlElement xmlElement, string name, string value, string nameSpace)
        {
            XmlElement newElement = xmlElement.OwnerDocument.CreateElement(name, nameSpace);
            if (value != "")
            {
                XmlText xmlText = xmlElement.OwnerDocument.CreateTextNode(value);
                newElement.AppendChild(xmlText);
            }
            xmlElement.AppendChild(newElement);

            return newElement;
        }

        private void addAttribute(XmlElement xmlElement, string name, string value)
        {
            XmlAttribute xmlAttribute = xmlElement.OwnerDocument.CreateAttribute(name);
            xmlAttribute.Value = value;
            xmlElement.Attributes.Append(xmlAttribute);
        }

        private DataSet getOpenReturnReceipts(string receiptNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT h.[Registered No_] FROM [" + database.getTableName("Cash Receipt") + "] h, [" + database.getTableName("Cash Receipt Line") + "] l WHERE h.[No_] = l.[Receipt No_] AND l.[Return Receipt No_] = @receiptNo");
            databaseQuery.addStringParameter("@receiptNo", receiptNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;

        }

        private DataSet getPostedReturnReceipts(string receiptNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT [Receipt No_] FROM [" + database.getTableName("Posted Cash Receipt Line") + "] WHERE [Return Receipt No_] = @receiptNo");
            databaseQuery.addStringParameter("@receiptNo", receiptNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;

        }

        private void composeReceiptInfo(ref XmlElement responseElement, string receiptNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Customer No_], [Customer Name], [User-ID], [Cash Register ID], [Registered Date], [Registered Time], [Printed], [Registered No_] FROM [" + database.getTableName("Cash Receipt") + "] WITH (NOLOCK) WHERE [Registered No_] = @receiptNo");
            databaseQuery.addStringParameter("@receiptNo", receiptNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                XmlElement receiptElement = addElement(responseElement, "receipt", "", "");
                addAttribute(receiptElement, "no", dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                addElement(receiptElement, "customerNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), "");
                addElement(receiptElement, "customerName", dataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(), "");
                addElement(receiptElement, "userId", dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(), "");
                addElement(receiptElement, "cashRegisterId", dataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(), "");
                addElement(receiptElement, "registeredDate", DateTime.Parse(dataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()).ToString("yyyy-MM-dd"), "");
                addElement(receiptElement, "registeredTime", DateTime.Parse(dataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()).ToString("HH:mm:dd"), "");
                addElement(receiptElement, "printed", dataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(), "");
                addElement(receiptElement, "registeredNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(), "");

                XmlElement linesElement = addElement(receiptElement, "lines", "", "");

                databaseQuery = database.prepare("SELECT [Line No_], [Cash Register ID], [Line Type], [Sales Type], [Sales No_], [Variant Code], [Description], [Unit of Measure Code], [Item Category Code], [Product Group Code], [Quantity], [Unit Price], [Unit Price Incl_ VAT], [Discount %], [Line Discount], [VAT %], [VAT Amount], [Amount], [Amount Incl_ VAT], [Payment Type], [Payment Reference No_], [Return Receipt No_], [Return Receipt Line No_], [Return Code], [Return Description], [Total Discount %], [Total Discount Amount], [Line Discount %], [Indent], [VAT Posting Group], [Location Code], [Discount Type], [Unit Price Not Bound], [No Return] FROM [" + database.getTableName("Cash Receipt Line") + "] WITH (NOLOCK) WHERE [Receipt No_] = @receiptNo AND [Void] = 0");
                databaseQuery.addStringParameter("@receiptNo", receiptNo, 20);

                dataAdapter = databaseQuery.executeDataAdapterQuery();

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    XmlElement lineElement = addElement(linesElement, "line", "", "");
                    addAttribute(lineElement, "lineNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
                    addAttribute(lineElement, "cashRegisterId", dataSet.Tables[i].Rows[0].ItemArray.GetValue(1).ToString());

                    addElement(lineElement, "lineType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString(), "");
                    addElement(lineElement, "salesType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString(), "");
                    addElement(lineElement, "salesNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString(), "");
                    addElement(lineElement, "variantCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString(), "");
                    addElement(lineElement, "description", dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString(), "");
                    addElement(lineElement, "unitOfMeasureCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString(), "");
                    addElement(lineElement, "itemCategoryCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString(), "");
                    addElement(lineElement, "productGroupCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString(), "");
                    addElement(lineElement, "quantity", dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString(), "");
                    addElement(lineElement, "unitPrice", dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString(), "");
                    addElement(lineElement, "unitPriceInclVat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString(), "");
                    addElement(lineElement, "discountProcent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString(), "");
                    addElement(lineElement, "lineDiscount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString(), "");
                    addElement(lineElement, "vat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(15).ToString(), "");
                    addElement(lineElement, "vatAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(16).ToString(), "");
                    addElement(lineElement, "amount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString(), "");
                    addElement(lineElement, "amountInclVat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString(), "");
                    addElement(lineElement, "paymentType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString(), "");
                    addElement(lineElement, "paymentReferenceNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString(), "");
                    addElement(lineElement, "returnReceiptNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString(), "");
                    addElement(lineElement, "returnReceiptLineNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(22).ToString(), "");
                    addElement(lineElement, "returnCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(23).ToString(), "");
                    addElement(lineElement, "returnDescription", dataSet.Tables[0].Rows[i].ItemArray.GetValue(24).ToString(), "");
                    addElement(lineElement, "totalDiscount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(25).ToString(), "");
                    addElement(lineElement, "totalDiscountAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(26).ToString(), "");
                    addElement(lineElement, "lineDiscountProcent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(27).ToString(), "");
                    addElement(lineElement, "indent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(28).ToString(), "");
                    addElement(lineElement, "vatPostingGroup", dataSet.Tables[0].Rows[i].ItemArray.GetValue(29).ToString(), "");
                    addElement(lineElement, "locationCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(30).ToString(), "");
                    addElement(lineElement, "discountType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(31).ToString(), "");

                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(32).ToString() == "1")
                    {
                        addElement(lineElement, "unitPriceNotBound", "TRUE", "");
                    }
                    else
                    {
                        addElement(lineElement, "unitPriceNotBound", "FALSE", "");
                    }

                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(33).ToString() == "1")
                    {
                        addElement(lineElement, "noReturn", "TRUE", "");
                    }
                    else
                    {
                        addElement(lineElement, "noReturn", "FALSE", "");
                    }

                    i++;
                }
            }
            else
            {
                
                databaseQuery = database.prepare("SELECT [No_], [Customer No_], [Customer Name], [User-ID], [Cash Register ID], [Registered Date], [Registered Time], [Printed], [Journal No_] FROM [" + database.getTableName("Posted Cash Receipt") + "] WITH (NOLOCK) WHERE [No_] = @receiptNo");
                databaseQuery.addStringParameter("@receiptNo", receiptNo, 20);

                dataAdapter = databaseQuery.executeDataAdapterQuery();

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    
                    XmlElement receiptElement = addElement(responseElement, "receipt", "", "");
                    addAttribute(receiptElement, "no", dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    addElement(receiptElement, "customerNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), "");
                    addElement(receiptElement, "customerName", dataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(), "");
                    addElement(receiptElement, "userId", dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(), "");
                    addElement(receiptElement, "cashRegisterId", dataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(), "");
                    addElement(receiptElement, "registeredDate", DateTime.Parse(dataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()).ToString("yyyy-MM-dd"), "");
                    addElement(receiptElement, "registeredTime", DateTime.Parse(dataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()).ToString("HH:mm:dd"), "");
                    addElement(receiptElement, "printed", dataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(), "");
                    addElement(receiptElement, "registeredNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), "");
                    addElement(receiptElement, "journalNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(), "");

                    XmlElement linesElement = addElement(receiptElement, "lines", "", "");

                    databaseQuery = database.prepare("SELECT [Line No_], [Cash Register ID], [Line Type], [Sales Type], [Sales No_], [Variant Code], [Description], [Unit of Measure Code], [Item Category Code], [Product Group Code], [Quantity], [Unit Price], [Unit Price Incl_ VAT], [Discount %], [Line Discount], [VAT %], [VAT Amount], [Amount], [Amount Incl_ VAT], [Payment Type], [Payment Reference No_], [Return Receipt No_], [Return Receipt Line No_], [Return Code], [Return Description], [Total Discount %], [Total Discount Amount], [Line Discount %], [Indent], [VAT Posting Group], [Location Code], [Discount Type], [Unit Price Not Bound], [No Return] FROM [" + database.getTableName("Posted Cash Receipt Line") + "] WITH (NOLOCK) WHERE [Receipt No_] = @receiptNo AND [Void] = 0");
                    databaseQuery.addStringParameter("@receiptNo", receiptNo, 20); 

                    dataAdapter = databaseQuery.executeDataAdapterQuery();

                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    int i = 0;
                    while (i < dataSet.Tables[0].Rows.Count)
                    {
                        XmlElement lineElement = addElement(linesElement, "line", "", "");
                        addAttribute(lineElement, "lineNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
                        addAttribute(lineElement, "cashRegisterId", dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());

                        addElement(lineElement, "lineType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString(), "");
                        addElement(lineElement, "salesType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString(), "");
                        addElement(lineElement, "salesNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString(), "");
                        addElement(lineElement, "variantCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString(), "");
                        addElement(lineElement, "description", dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString(), "");
                        addElement(lineElement, "unitOfMeasureCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString(), "");
                        addElement(lineElement, "itemCategoryCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString(), "");
                        addElement(lineElement, "productGroupCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString(), "");
                        addElement(lineElement, "quantity", dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString(), "");
                        addElement(lineElement, "unitPrice", dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString(), "");
                        addElement(lineElement, "unitPriceInclVat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString(), "");
                        addElement(lineElement, "discountProcent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString(), "");
                        addElement(lineElement, "lineDiscount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString(), "");
                        addElement(lineElement, "vat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(15).ToString(), "");
                        addElement(lineElement, "vatAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(16).ToString(), "");
                        addElement(lineElement, "amount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString(), "");
                        addElement(lineElement, "amountInclVat", dataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString(), "");
                        addElement(lineElement, "paymentType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString(), "");
                        addElement(lineElement, "paymentReferenceNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString(), "");
                        addElement(lineElement, "returnReceiptNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString(), "");
                        addElement(lineElement, "returnReceiptLineNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(22).ToString(), "");
                        addElement(lineElement, "returnCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(23).ToString(), "");
                        addElement(lineElement, "returnDescription", dataSet.Tables[0].Rows[i].ItemArray.GetValue(24).ToString(), "");
                        addElement(lineElement, "totalDiscount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(25).ToString(), "");
                        addElement(lineElement, "totalDiscountAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(26).ToString(), "");
                        addElement(lineElement, "lineDiscountProcent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(27).ToString(), "");
                        addElement(lineElement, "indent", dataSet.Tables[0].Rows[i].ItemArray.GetValue(28).ToString(), "");
                        addElement(lineElement, "vatPostingGroup", dataSet.Tables[0].Rows[i].ItemArray.GetValue(29).ToString(), "");
                        addElement(lineElement, "locationCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(30).ToString(), "");
                        addElement(lineElement, "discountType", dataSet.Tables[0].Rows[i].ItemArray.GetValue(31).ToString(), "");

                        if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(32).ToString() == "1")
                        {
                            addElement(lineElement, "unitPriceNotBound", "TRUE", "");
                        }
                        else
                        {
                            addElement(lineElement, "unitPriceNotBound", "FALSE", "");
                        }

                        if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(33).ToString() == "1")
                        {
                            addElement(lineElement, "noReturn", "TRUE", "");
                        }
                        else
                        {
                            addElement(lineElement, "noReturn", "FALSE", "");
                        }

                        i++;
                    }
                }

            }
        }

        
    }
}
