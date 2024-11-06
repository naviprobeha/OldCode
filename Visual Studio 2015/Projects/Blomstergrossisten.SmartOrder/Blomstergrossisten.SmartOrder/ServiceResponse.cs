using System;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for ServiceResponse.
    /// </summary>
    public class ServiceResponse
    {
        private Publication publicationObject;
        private Inventory inventoryObject;
        private Error errorObject;
        private bool hasErrorsValue;
        private XmlElement responseDocument;

        private Logger logger;

        public ServiceResponse()
        {
            hasErrorsValue = false;
            //
            // TODO: Add constructor logic here
            //
        }

        public ServiceResponse(XmlElement serviceResponseElement, SmartDatabase smartDatabase, Logger logger)
        {
            this.logger = logger;
            hasErrorsValue = false;
            responseDocument = serviceResponseElement;
            fromDOM(serviceResponseElement, smartDatabase);
        }

        public Publication publication
        {
            get
            {
                return publicationObject;
            }
        }

        public Inventory inventory
        {
            get
            {
                return inventoryObject;
            }
        }

        public Error error
        {
            get
            {
                return errorObject;
            }
        }

        public bool hasErrors
        {
            get
            {
                return hasErrorsValue;
            }
        }

        public XmlElement responseDoc
        {
            get
            {
                return responseDocument;
            }
        }

        public void fromDOM(XmlElement serviceResponseElement, SmartDatabase smartDatabase)
        {
            XmlNodeList publications = serviceResponseElement.GetElementsByTagName("PUBLICATION");
            if (publications.Count > 0)
            {
                publicationObject = new Publication((XmlElement)publications.Item(0), smartDatabase, logger);
            }

            XmlNodeList inventories = serviceResponseElement.GetElementsByTagName("INVENTORY");
            if (inventories.Count > 0)
            {
                inventoryObject = new Inventory((XmlElement)inventories.Item(0), smartDatabase, logger);
            }

            XmlNodeList errors = serviceResponseElement.GetElementsByTagName("ERROR");
            if (errors.Count > 0)
            {
                errorObject = new Error((XmlElement)errors.Item(0));
                hasErrorsValue = true;
            }
        }

    }
}
