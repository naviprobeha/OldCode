using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Navipro.SmartInventory
{
    public class Document : ServiceArgument
    {
        private int _type;
        private int _documentType;
        private string _documentNo;
        private DataSet _dataSet;
        private int _mode;
        private SmartDatabase smartDatabase;

        public Document(int documentType, string documentNo)
        {
            _documentType = documentType;
            _documentNo = documentNo;
            _mode = 0;
        }

        public Document(SmartDatabase smartDatabase, int type, int documentType, string documentNo, DataSet dataSet)
        {
            this.smartDatabase = smartDatabase;
            _type = type;
            _documentType = documentType;
            _documentNo = documentNo;
            _dataSet = dataSet;
            _mode = 1;
        }

        public int documentType { get { return _documentType; } }
        public string documentNo { get { return _documentNo; } }

        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
        {
            if (_mode == 0)
            {
                XmlElement orderElement = xmlDocumentContext.CreateElement("order");

                XmlElement documentTypeElement = xmlDocumentContext.CreateElement("documentType");
                XmlText documentTypeText = xmlDocumentContext.CreateTextNode(_documentType.ToString());
                documentTypeElement.AppendChild(documentTypeText);

                XmlElement documentNoElement = xmlDocumentContext.CreateElement("documentNo");
                XmlText documentNoText = xmlDocumentContext.CreateTextNode(_documentNo);
                documentNoElement.AppendChild(documentNoText);

                orderElement.AppendChild(documentTypeElement);
                orderElement.AppendChild(documentNoElement);

                return orderElement;
            }
            if (_mode == 1)
            {
                XmlElement orderElement = xmlDocumentContext.CreateElement("order");

                orderElement.SetAttribute("documentType", _documentType.ToString());
                orderElement.SetAttribute("no", _documentNo);

                XmlElement linesElement = xmlDocumentContext.CreateElement("lines");

                int i = 0;
                while (i < _dataSet.Tables[0].Rows.Count)
                {
                    XmlElement lineElement = xmlDocumentContext.CreateElement("line");

                    if (_type == 0)
                    {
                        DataPurchaseLine dataPurchaseLine = new DataPurchaseLine(null, _dataSet.Tables[0].Rows[i]);

                        lineElement.SetAttribute("lineNo", dataPurchaseLine.lineNo.ToString());


                        XmlElement itemNoElement = xmlDocumentContext.CreateElement("itemNo");
                        XmlText itemNoText = xmlDocumentContext.CreateTextNode(dataPurchaseLine.itemNo);
                        itemNoElement.AppendChild(itemNoText);
                        lineElement.AppendChild(itemNoElement);

                        XmlElement variantCodeElement = xmlDocumentContext.CreateElement("variantCode");
                        XmlText variantCodeText = xmlDocumentContext.CreateTextNode(dataPurchaseLine.variantCode);
                        variantCodeElement.AppendChild(variantCodeText);
                        lineElement.AppendChild(variantCodeElement);

                        XmlElement quantityElement = xmlDocumentContext.CreateElement("quantityToReceive");
                        XmlText quantityText = xmlDocumentContext.CreateTextNode(dataPurchaseLine.quantityToReceive.ToString());
                        quantityElement.AppendChild(quantityText);
                        lineElement.AppendChild(quantityElement);

                    }

                    if (_type == 1)
                    {
                        DataSalesLine dataSalesLine = new DataSalesLine(null, _dataSet.Tables[0].Rows[i]);

                        lineElement.SetAttribute("lineNo", dataSalesLine.lineNo.ToString());


                        XmlElement itemNoElement = xmlDocumentContext.CreateElement("itemNo");
                        XmlText itemNoText = xmlDocumentContext.CreateTextNode(dataSalesLine.itemNo);
                        itemNoElement.AppendChild(itemNoText);
                        lineElement.AppendChild(itemNoElement);

                        XmlElement variantCodeElement = xmlDocumentContext.CreateElement("variantCode");
                        XmlText variantCodeText = xmlDocumentContext.CreateTextNode(dataSalesLine.variantCode);
                        variantCodeElement.AppendChild(variantCodeText);
                        lineElement.AppendChild(variantCodeElement);

                        XmlElement quantityElement = xmlDocumentContext.CreateElement("quantity");
                        XmlText quantityText = xmlDocumentContext.CreateTextNode(dataSalesLine.quantity.ToString());
                        quantityElement.AppendChild(quantityText);
                        lineElement.AppendChild(quantityElement);

                        XmlElement quantityToShipElement = xmlDocumentContext.CreateElement("quantityToShip");
                        XmlText quantityToShipText = xmlDocumentContext.CreateTextNode(dataSalesLine.quantityToShip.ToString());
                        quantityToShipElement.AppendChild(quantityToShipText);
                        lineElement.AppendChild(quantityToShipElement);

                        XmlElement cartonsElement = xmlDocumentContext.CreateElement("cartons");

                        DataSet cartonDataSet = DataSalesLineCarton.getDataSet(smartDatabase, dataSalesLine.documentType, dataSalesLine.documentNo, dataSalesLine.lineNo);
                        int j = 0;
                        while (j < cartonDataSet.Tables[0].Rows.Count)
                        {
                            XmlElement cartonElement = xmlDocumentContext.CreateElement("carton");
                            cartonElement.SetAttribute("no", cartonDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString());
                            cartonElement.SetAttribute("splitOnQty", cartonDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString());

                            cartonsElement.AppendChild(cartonElement);

                            j++;
                        }
                        lineElement.AppendChild(cartonsElement);
                    }

                    linesElement.AppendChild(lineElement);

                    i++;
                }


                orderElement.AppendChild(linesElement);

                return orderElement;


            }

            return null;
        }

        #endregion
    }
}
