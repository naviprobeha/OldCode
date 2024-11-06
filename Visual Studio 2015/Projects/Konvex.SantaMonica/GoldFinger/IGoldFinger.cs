using Navipro.SantaMonica.Common;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.IO;

namespace Konvex.SmartShipping.Goldfinger
{
    public interface IGoldfinger
    {
        void init(Configuration configuration, Database database);

        int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter);

        void createContainerEntry(string agentCode, string containerNo, int type, int documentType, string documentNo, DateTime entryDateTime, DateTime estimatedArrivalDateTime, int locationType, string locationCode, int positionX, int positionY, int sourceType, string sourceCode, float weight);

        SynchEntryCollection getSynchRecord(string agentCode);

        void ackSynchRecord(string agentCode, int[] synchEntryNoList);

        void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime);

        void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment, int distance);
        void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime, string loadWaitReason);
        void reportLineJournal(string agentCode, Konvex.SmartShipping.DataObjects.LineJournal lineJournal);

        void setFactoryOrderStatus(string agentCode, DataSet factoryOrderDataSet);

        void createShipment(string agentCode, Konvex.SmartShipping.DataObjects.ShipmentHeader fromShipmentHeader);

        void createOrder(string agentCode, Konvex.SmartShipping.DataObjects.OrderHeader fromOrderHeader);

        void setMessageStatus(string agentCode, int messageEntryNo, int status);

        void assignShipOrder(string agentCode, string no, string newAgentCode);

        void dispose();

        void reportError(string agentCode, string message);


        void acknowledgePriceUpdate(string agentCode, string itemNo, float checksum);

        int fetchLastInvoiceNo(string agentCode);

        void initAgentDatabase(string agentCode);

        void reportContainerForService(string agentCode, string containerNo);

        Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection getDistancesByDate(DateTime fromDate, DateTime toDate);

        Konvex.SmartShipping.DataObjects.LineOrderCollection getLineOrders(string agentCode, int lineJournalNo, DateTime date);

        Konvex.SmartShipping.DataObjects.LineJournalCollection getLineJournals(string agentCode, DateTime shipDate);

        void setLineOrderContainers(string agentCode, string no, List<string> containerStringList);

    }
}
