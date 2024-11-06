using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Navipro.SmartInventory
{
    public class DocumentLine : ServiceArgument
    {
        private int _documentType;
        private string _documentNo;
        private string _eanCode;
        private float _quantity;
        private string _agentId;
        private int _lineNo;


        public DocumentLine(string agentId, string eanCode, float quantity)
        {
            _agentId = agentId;
            _eanCode = eanCode;
            _quantity = quantity;
        }

        public DocumentLine(int documentType, string documentNo, int lineNo, string agentId, string eanCode, float quantity)
        {
            _documentType = documentType;
            _documentNo = documentNo;
            _agentId = agentId;
            _eanCode = eanCode;
            _quantity = quantity;
            _lineNo = lineNo;
        }

        public string eanCode { get { return _eanCode; } }

        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
        {
            XmlElement docLineElement = xmlDocumentContext.CreateElement("documentLine");

            docLineElement.SetAttribute("documentType", _documentType.ToString());
            docLineElement.SetAttribute("documentNo", _documentNo);

            docLineElement.SetAttribute("agentId", _agentId);
            docLineElement.SetAttribute("eanCode", _eanCode);
            docLineElement.SetAttribute("lineNo", _lineNo.ToString());
            docLineElement.SetAttribute("quantity", _quantity.ToString());

            return docLineElement;

        }

        #endregion
    }
}
