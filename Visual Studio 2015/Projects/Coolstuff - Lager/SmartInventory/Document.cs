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
        private int _lineNo;
        private string _userId;

        public Document(int documentType, string documentNo)
        {
            _documentType = documentType;
            _documentNo = documentNo;
        }

        public Document(int documentType, string documentNo, int lineNo)
        {
            _documentType = documentType;
            _documentNo = documentNo;
            _lineNo = lineNo;
        }

        public int documentType { get { return _documentType; } }
        public string documentNo { get { return _documentNo; } }
        public string orderNo { get { return _orderNo; } set { _orderNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public string userId { get { return _userId; } set { _userId = value; } }

        #region ServiceArgument Members


        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
        {

            XmlElement docElement = xmlDocumentContext.CreateElement("document");

            docElement.SetAttribute("documentNo", documentNo);
            docElement.SetAttribute("lineNo", lineNo.ToString());

            XmlElement documentTypeElement = xmlDocumentContext.CreateElement("documentType");
            XmlText documentTypeText = xmlDocumentContext.CreateTextNode(_documentType.ToString());
            documentTypeElement.AppendChild(documentTypeText);

            XmlElement documentNoElement = xmlDocumentContext.CreateElement("documentNo");
            XmlText documentNoText = xmlDocumentContext.CreateTextNode(_documentNo);
            documentNoElement.AppendChild(documentNoText);

            XmlElement orderNoElement = xmlDocumentContext.CreateElement("orderNo");
            XmlText orderNoText = xmlDocumentContext.CreateTextNode(_orderNo);
            orderNoElement.AppendChild(orderNoText);

            XmlElement lineNoElement = xmlDocumentContext.CreateElement("lineNo");
            XmlText lineNoText = xmlDocumentContext.CreateTextNode(_lineNo.ToString());
            lineNoElement.AppendChild(lineNoText);

            XmlElement userIdElement = xmlDocumentContext.CreateElement("userId");
            XmlText userIdText = xmlDocumentContext.CreateTextNode(_userId);
            userIdElement.AppendChild(userIdText);



            docElement.AppendChild(documentTypeElement);
            docElement.AppendChild(documentNoElement);
            docElement.AppendChild(orderNoElement);
            docElement.AppendChild(lineNoElement);
            docElement.AppendChild(userIdElement);

            return docElement;

        }

         
        #endregion
    }
}
