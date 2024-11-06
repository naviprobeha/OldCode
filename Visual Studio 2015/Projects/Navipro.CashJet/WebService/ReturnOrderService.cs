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
    public class ReturnOrderService
    {
        private Configuration configuration;
        private Database database;

        public ReturnOrderService(Configuration configuration)
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

            XmlElement orderNoElement = (XmlElement)xmlDocument.SelectSingleNode("nav/serviceRequest/serviceArgument/orderNo");
            if (orderNoElement != null)
            {
                string orderNo = orderNoElement.FirstChild.Value;

                composeOrderInfo(ref responseElement, orderNo);

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

        private void composeOrderInfo(ref XmlElement responseElement, string orderNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Order No_], [Sell-to Customer No_], [Order Date] FROM [" + database.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [Order No_] = @orderNo");
            databaseQuery.addStringParameter("@orderNo", orderNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string invoiceNo = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                XmlElement orderElement = addElement(responseElement, "order", "", "");
                addAttribute(orderElement, "no", dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                addAttribute(orderElement, "customerNo", dataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                addAttribute(orderElement, "orderDate", dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Substring(0, 10));

                XmlElement linesElement = addElement(orderElement, "lines", "", "");

                databaseQuery = database.prepare("SELECT [Line No_], [Type], [No_], [Variant Code], [Location Code], [Description], [Quantity], [Quantity], [Unit Price], [Line Discount Amount], [Line Amount], [Shipment Date] FROM [" + database.getTableName("Sales Invoice Line") + "] WITH (NOLOCK) WHERE [Document No_] = @invoiceNo");
                databaseQuery.addStringParameter("@invoiceNo", invoiceNo, 20);


                dataAdapter = databaseQuery.executeDataAdapterQuery();

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    XmlElement lineElement = addElement(linesElement, "line", "", "");
                    addAttribute(lineElement, "lineNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
 
                    addElement(lineElement, "type", dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), "");
                    addElement(lineElement, "no", dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString(), "");
                    addElement(lineElement, "variantCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString(), "");
                    addElement(lineElement, "locationCode", dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString(), "");
                    addElement(lineElement, "description", dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString(), "");
                    addElement(lineElement, "quantity", dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString(), "");
                    addElement(lineElement, "quantityShipped", dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString(), "");
                    addElement(lineElement, "unitPrice", dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString(), "");
                    addElement(lineElement, "lineDiscountAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString(), "");
                    addElement(lineElement, "lineAmount", dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString(), "");
                    addElement(lineElement, "plannedShipmentDate", dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString().Substring(0, 10), "");

                    i++;
                }
            }

        }


    }
}
