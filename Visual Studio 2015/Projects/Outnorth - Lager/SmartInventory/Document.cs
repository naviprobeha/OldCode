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
        private int _documentType;
        private string _documentNo;
        private string _orderNo;

        public Document(int documentType, string documentNo)
        {
            _documentType = documentType;
            _documentNo = documentNo;
        }


        public int documentType { get { return _documentType; } }
        public string documentNo { get { return _documentNo; } }
        public string orderNo { get { return _orderNo; } set { _orderNo = value; } }

        #region ServiceArgument Members


        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
        {

            XmlElement docElement = xmlDocumentContext.CreateElement("document");

            XmlElement documentTypeElement = xmlDocumentContext.CreateElement("documentType");
            XmlText documentTypeText = xmlDocumentContext.CreateTextNode(_documentType.ToString());
            documentTypeElement.AppendChild(documentTypeText);

            XmlElement documentNoElement = xmlDocumentContext.CreateElement("documentNo");
            XmlText documentNoText = xmlDocumentContext.CreateTextNode(_documentNo);
            documentNoElement.AppendChild(documentNoText);

            XmlElement orderNoElement = xmlDocumentContext.CreateElement("orderNo");
            XmlText orderNoText = xmlDocumentContext.CreateTextNode(_orderNo);
            documentNoElement.AppendChild(orderNoText);

            docElement.AppendChild(documentTypeElement);
            docElement.AppendChild(documentNoElement);
            docElement.AppendChild(orderNoElement);

            return docElement;

        }

         
        #endregion
    }
}
