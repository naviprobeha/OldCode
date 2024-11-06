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
using System.IO;

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

		public Request()
		{

		}

        [WebMethod]
        public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            int noOfEntries = goldfinger.reportStatus(agentCode, rt90x, rt90y, heading, speed, height, status, userName, tripMeter);
            goldfinger.dispose();
            return noOfEntries;
        }

        [WebMethod]
        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime);
            goldfinger.dispose();
        }

        [WebMethod]
        public void setShipOrderStatusEx(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, comment, 0);
            goldfinger.dispose();
        }

        [WebMethod]
        public void setShipOrderStatusEx2(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment, int distance)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, comment, distance);
            goldfinger.dispose();
        }

        [WebMethod]
        public SynchEntryCollection getSynchEntries(string agentCode)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            SynchEntryCollection synchEntryCollection = goldfinger.getSynchRecord(agentCode);
            goldfinger.dispose();
            return synchEntryCollection;
        }

        [WebMethod]
        public void setMessageStatus(string agentCode, int messageEntryNo, int status)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setMessageStatus(agentCode, messageEntryNo, status);
            goldfinger.dispose();
        }

        [WebMethod]
        public void ackSynchEntry(string agentCode, int[] synchEntryNoList)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.ackSynchRecord(agentCode, synchEntryNoList);
            goldfinger.dispose();
        }

        [WebMethod]
        public void assignShipOrder(string agentCode, string no, string newAgentCode)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.assignShipOrder(agentCode, no, newAgentCode);
            goldfinger.dispose();
        }

        [WebMethod]
        public void reportError(string agentCode, string message)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.reportError(agentCode, message);
            goldfinger.dispose();
        }

        [WebMethod]
        public void createContainerEntry(string agentCode, string containerNo, int type, int documentType, string documentNo, DateTime entryDateTime, DateTime estimatedArrivalDateTime, int locationType, string locationCode, int positionX, int positionY, int sourceType, string sourceCode, float weight)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.createContainerEntry(agentCode, containerNo, type, documentType, documentNo, entryDateTime, estimatedArrivalDateTime, locationType, locationCode, positionX, positionY, sourceType, sourceCode, weight);
            goldfinger.dispose();
        }

        [WebMethod]
        public void createShipment(string agentCode, Konvex.SmartShipping.DataObjects.ShipmentHeader shipmentHeader)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.createShipment(agentCode, shipmentHeader);
            goldfinger.dispose();
        }

        [WebMethod]
        public void createOrder(string agentCode, Konvex.SmartShipping.DataObjects.OrderHeader orderHeader)
        {
            StreamWriter streamWriter = new StreamWriter("C:\\temp\\createorder.xml");
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(orderHeader.GetType());
            x.Serialize(streamWriter.BaseStream, orderHeader);
            streamWriter.Flush();
            streamWriter.Close();
            
                
                IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.createOrder(agentCode, orderHeader);
            goldfinger.dispose();
        }

        [WebMethod]
        public int fetchLastInvoiceNo(string agentCode)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            int invoiceNo = goldfinger.fetchLastInvoiceNo(agentCode);
            goldfinger.dispose();

            return invoiceNo;
        }

        [WebMethod]
        public void initAgentDatabase(string agentCode)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.initAgentDatabase(agentCode);
            goldfinger.dispose();

        }

        [WebMethod]
        public void reportContainerForService(string agentCode, string containerNo)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.reportContainerForService(agentCode, containerNo);
            goldfinger.dispose();

        }

        [WebMethod]
        public Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection getDistancesByDate(DateTime fromDate, DateTime toDate)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create("");
            Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection collection = goldfinger.getDistancesByDate(fromDate, toDate);
            goldfinger.dispose();

            return collection;
        }

        [WebMethod]
        public void setLineOrderContainers(string agentCode, string no, List<string> containerStringList)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setLineOrderContainers(agentCode, no, containerStringList);
            goldfinger.dispose();
        }

        [WebMethod]
        public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setLineOrderStatus(agentCode, no, status, positionX, positionY, shipTime, loadWaitTime, "");
            goldfinger.dispose();
        }

        [WebMethod]
        public void setLineOrderStatus2(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime, string loadWaitReason)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.setLineOrderStatus(agentCode, no, status, positionX, positionY, shipTime, loadWaitTime, loadWaitReason);
            goldfinger.dispose();
        }
        [WebMethod]
        public void reportLineJournal(string agentCode, Konvex.SmartShipping.DataObjects.LineJournal lineJournal)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            goldfinger.reportLineJournal(agentCode, lineJournal);
            goldfinger.dispose();
        }

        [WebMethod]
        public Konvex.SmartShipping.DataObjects.LineJournalCollection getLineJournals(string agentCode, DateTime shipDate)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            Konvex.SmartShipping.DataObjects.LineJournalCollection collection = goldfinger.getLineJournals(agentCode, shipDate);
            goldfinger.dispose();

            return collection;
        }


        [WebMethod]
        public Konvex.SmartShipping.DataObjects.LineOrderCollection getLineOrders(string agentCode)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            Konvex.SmartShipping.DataObjects.LineOrderCollection collection = goldfinger.getLineOrders(agentCode, 0, new DateTime(2000, 1, 1));
            goldfinger.dispose();

            return collection;
        }

        [WebMethod]
        public Konvex.SmartShipping.DataObjects.LineOrderCollection getLineOrders2(string agentCode, int lineJournalNo)
        {
            IGoldfinger goldfinger = GoldFingerFactory.Create(agentCode);
            Konvex.SmartShipping.DataObjects.LineOrderCollection collection = goldfinger.getLineOrders(agentCode, lineJournalNo, new DateTime(2000, 1, 1));
            goldfinger.dispose();

            return collection;
        }

    }
}
