using System;
using System.Xml;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// OrderItem illustrerar en post i varukorgen i olika lägen. Varukorgen läses ut i OrderItems ifrån klassen SalesId.
    /// </summary>
    public class Document : ServiceArgument
    {
        private int _documentType;
        private string _documentNo = "";
        private string _orderNo = "";
        private string _shipmentNo = "";
        private DateTime _orderDate;
        private bool _orderPdfExists;
        private bool _invoicePdfExists;
        private bool _shipmentPdfExists;
        private bool _packingSlipPdfExists;
        private string _shippingAgentCode = "";
        private string _packageTrackingNo = "";
        private string _trackingUrl = "";

        /// <summary>
        /// Konstruktor som bygger upp en post utifrån en rad i varukorgen.
        /// </summary>
        /// <param name="webCartLine">Motsvarar en rad i varukorgen.</param>
        public Document()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string documentNo { get { if (_documentNo != null) return _documentNo; return ""; } set { _documentNo = value; } }
        public string orderNo { get { if (_orderNo != null) return _orderNo; return ""; } set { _orderNo = value; } }
        public string shipmentNo { get { if (_shipmentNo != null) return _shipmentNo; return ""; } set { _shipmentNo = value; } }
        public DateTime orderDate { get { return _orderDate; } set { _orderDate = value; } }
        public bool orderPdfExists { get { return _orderPdfExists; } set { _orderPdfExists = value; } }
        public bool invoicePdfExists { get { return _invoicePdfExists; } set { _invoicePdfExists = value; } }
        public bool shipmentPdfExists { get { return _shipmentPdfExists; } set { _shipmentPdfExists = value; } }
        public bool packingSlipPdfExists { get { return _packingSlipPdfExists; } set { _packingSlipPdfExists = value; } }
        public string trackingUrl { get { return _trackingUrl; } set { _trackingUrl = value; } }
        public string shippingAgentCode { get { return _shippingAgentCode; } set { _shippingAgentCode = value; } }
        public string packageTrackingNo { get { if (_packageTrackingNo != null) return _packageTrackingNo; return ""; } set { _packageTrackingNo = value; } }

        public static byte[] getByteArray(Navipro.Infojet.Lib.Infojet infojetContext, int documentType, string documentNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Document] FROM [" + infojetContext.systemDatabase.getTableName("Web Requested Document") + "] WHERE [Document Type] = @documentType AND [Document No_] = @documentNo");
            databaseQuery.addIntParameter("documentType", documentType);
            databaseQuery.addStringParameter("documentNo", documentNo, 20);


            byte[] imageByteArray = (byte[])databaseQuery.executeScalar();
            return imageByteArray;

        }


        #region ServiceArgument Members

        System.Xml.XmlElement ServiceArgument.toDOM(System.Xml.XmlDocument xmlDoc)
        {
            XmlElement containerElement = xmlDoc.CreateElement("document");

            containerElement.SetAttribute("documentType", documentType.ToString());
            containerElement.SetAttribute("documentNo", documentNo);

            return containerElement;
        }

        #endregion
    }
}
