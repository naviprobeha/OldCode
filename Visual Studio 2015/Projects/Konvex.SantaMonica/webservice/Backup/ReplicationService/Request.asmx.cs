using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Konvex.SmartShipping.Goldfinger;
using System.Collections.Generic;

namespace Konvex.SmartShipping.ReplicationService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://smartshipping.konvex.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Request : System.Web.Services.WebService
    {
        private Konvex.SmartShipping.Goldfinger.Goldfinger goldfinger;

		public Request()
		{
            this.goldfinger = new Konvex.SmartShipping.Goldfinger.Goldfinger();
			this.goldfinger.init();
		}

        [WebMethod]
        public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
        {
            int noOfEntries = this.goldfinger.reportStatus(agentCode, rt90x, rt90y, heading, speed, height, status, userName, tripMeter);
            this.goldfinger.dispose();
            return noOfEntries;
        }

        [WebMethod]
        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
        {
            this.goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void setShipOrderStatusEx(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment)
        {
            this.goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, comment, 0);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void setShipOrderStatusEx2(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment, int distance)
        {
            this.goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, comment, distance);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public SynchEntryCollection getSynchEntries(string agentCode)
        {
            SynchEntryCollection synchEntryCollection = this.goldfinger.getSynchRecord(agentCode);
            this.goldfinger.dispose();
            return synchEntryCollection;
        }

        [WebMethod]
        public void setMessageStatus(string agentCode, int messageEntryNo, int status)
        {
            this.goldfinger.setMessageStatus(agentCode, messageEntryNo, status);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void ackSynchEntry(string agentCode, int[] synchEntryNoList)
        {
            this.goldfinger.ackSynchRecord(synchEntryNoList);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void assignShipOrder(string agentCode, string no, string newAgentCode)
        {
            this.goldfinger.assignShipOrder(agentCode, no, newAgentCode);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void reportError(string agentCode, string message)
        {
            this.goldfinger.reportError(agentCode, message);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void createContainerEntry(string agentCode, string containerNo, int type, int documentType, string documentNo, DateTime entryDateTime, DateTime estimatedArrivalDateTime, int locationType, string locationCode, int positionX, int positionY, int sourceType, string sourceCode, float weight)
        {
            this.goldfinger.createContainerEntry(agentCode, containerNo, type, documentType, documentNo, entryDateTime, estimatedArrivalDateTime, locationType, locationCode, positionX, positionY, sourceType, sourceCode, weight);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void createShipment(string agentCode, Konvex.SmartShipping.DataObjects.ShipmentHeader shipmentHeader)
        {
            this.goldfinger.createShipment(agentCode, shipmentHeader);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void createOrder(string agentCode, Konvex.SmartShipping.DataObjects.OrderHeader orderHeader)
        {
            this.goldfinger.createOrder(agentCode, orderHeader);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public int fetchLastInvoiceNo(string agentCode)
        {
            int invoiceNo = this.goldfinger.fetchLastInvoiceNo(agentCode);
            this.goldfinger.dispose();

            return invoiceNo;
        }

        [WebMethod]
        public void initAgentDatabase(string agentCode)
        {
            this.goldfinger.initAgentDatabase(agentCode);
            this.goldfinger.dispose();

        }

        [WebMethod]
        public void reportContainerForService(string agentCode, string containerNo)
        {
            this.goldfinger.reportContainerForService(agentCode, containerNo);
            this.goldfinger.dispose();

        }

        [WebMethod]
        public Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection getDistancesByDate(DateTime fromDate, DateTime toDate)
        {
            Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection collection = this.goldfinger.getDistancesByDate(fromDate, toDate);
            this.goldfinger.dispose();

            return collection;
        }

        [WebMethod]
        public void setLineOrderContainers(string agentCode, string no, List<string> containerStringList)
        {
            this.goldfinger.setLineOrderContainers(agentCode, no, containerStringList);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime)
        {
            this.goldfinger.setLineOrderStatus(agentCode, no, status, positionX, positionY, shipTime, loadWaitTime);
            this.goldfinger.dispose();
        }


        [WebMethod]
        public void reportLineJournal(string agentCode, Konvex.SmartShipping.DataObjects.LineJournal lineJournal)
        {
            this.goldfinger.reportLineJournal(agentCode, lineJournal);
            this.goldfinger.dispose();
        }

        [WebMethod]
        public Konvex.SmartShipping.DataObjects.LineJournalCollection getLineJournals(string agentCode, DateTime shipDate)
        {
            Konvex.SmartShipping.DataObjects.LineJournalCollection collection = this.goldfinger.getLineJournals(agentCode, shipDate);
            this.goldfinger.dispose();

            return collection;
        }


        [WebMethod]
        public Konvex.SmartShipping.DataObjects.LineOrderCollection getLineOrders(string agentCode)
        {
            Konvex.SmartShipping.DataObjects.LineOrderCollection collection = this.goldfinger.getLineOrders(agentCode);
            this.goldfinger.dispose();

            return collection;
        }
     
    }
}
