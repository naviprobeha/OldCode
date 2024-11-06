using Navipro.SantaMonica.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Konvex.SmartShipping.DataObjects;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Konvex.SmartShipping.Goldfinger
{
    public class Goldfinger2022 : IGoldfinger, Logger
    {
        private Configuration configuration;
        private Database database;

        public void init(Configuration configuration, Database database)
        {
            this.database = database;
            this.configuration = configuration;

        }

        public Goldfinger getOldGoldfinger()
        {
            Goldfinger oldGoldfinger = new Goldfinger();
            oldGoldfinger.init(this.configuration, this.database);
            return oldGoldfinger;
        }

        public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
        {
            
            JObject statusObject = new JObject();
            statusObject["rt90x"] = rt90x;
            statusObject["rt90y"] = rt90y;
            statusObject["heading"] = heading;
            statusObject["speed"] = speed;
            statusObject["height"] = height;
            statusObject["status"] = status;
            statusObject["userName"] = userName;
            statusObject["tripMeter"] = tripMeter;

            /*
            if (agentCode == "ECF")
            {
                StreamWriter streamWriter = new StreamWriter("C:\\temp\\agentstatus_" + agentCode + ".log", true);
                streamWriter.WriteLine("Status: " + statusObject.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }
            */

            string response = makeApiCall("agentApi/reportStatus/" + agentCode, "POST", statusObject.ToString());
            int count = int.Parse(response);

            

            return count;
        }

        public void acknowledgePriceUpdate(string agentCode, string itemNo, float checksum)
        {
            throw new NotImplementedException();
        }

        public void ackSynchRecord(string agentCode, int[] synchEntryNoList)
        {
            JArray array = new JArray();
            foreach(int i in synchEntryNoList)
            {
                array.Add(i);
            }

           
            

            makeApiCall("agentApi/ackSyncRecord", "POST", array.ToString());
            
        }

        public void assignShipOrder(string agentCode, string no, string newAgentCode)
        {
            StreamWriter streamWriter = new StreamWriter("C:\\temp\\assignagent.log", true);
            streamWriter.WriteLine("[" + agentCode + "] Assigning shiporder " + no + " to [" + newAgentCode + "]");
            streamWriter.Flush();
            streamWriter.Close();


            JObject assignAgentObject = new JObject();
            assignAgentObject.Add("agentCode", agentCode);
            assignAgentObject.Add("newAgentCode", newAgentCode);

            makeApiCall("agentApi/assignShipOrder/"+no, "POST", assignAgentObject.ToString());

        }

        public void createOrder(string agentCode, OrderHeader fromOrderHeader)
        {

            JObject orderObject = fromOrderHeader.toJsonObject();

            makeApiCall("agentApi/createOrder/" + agentCode, "POST", orderObject.ToString());


            //Goldfinger oldGf = getOldGoldfinger();
            //oldGf.createOrder(agentCode, fromOrderHeader);


        }

        public void createShipment(string agentCode, DataObjects.ShipmentHeader fromShipmentHeader)
        {
            if (fromShipmentHeader.organizationNo == "GEOL") fromShipmentHeader.organizationNo = "DICK2";

            Agents agents = new Agents();
            Navipro.SantaMonica.Common.Agent currentAgent = agents.getAgent(database, agentCode);

            if (currentAgent != null)
            {
                fromShipmentHeader.organizationNo = currentAgent.organizationNo;
            }

            JObject shipmentJObject = fromShipmentHeader.toJsonObject();

            makeApiCall("agentApi/createShipment/"+agentCode, "POST", shipmentJObject.ToString());

            return;
            Goldfinger oldGf = getOldGoldfinger();
            oldGf.createShipment(agentCode, fromShipmentHeader);
            
            this.database.nonQuery(string.Concat(new object[]
            {
                            "UPDATE [Shipment Header] SET [Status] = 2 ",
                            "WHERE [No] = '",
                            fromShipmentHeader.no,
                            "'"
            }));
        }

        public int fetchLastInvoiceNo(string agentCode)
        {
            Goldfinger oldGf = getOldGoldfinger();
            return oldGf.fetchLastInvoiceNo(agentCode);


            if (agentCode == "ABC") return 1;
            if (agentCode == "NCL") return 1;
            throw new NotImplementedException();
        }

        public SynchEntryCollection getSynchRecord(string agentCode)
        {
            Agents agents = new Agents();
            Navipro.SantaMonica.Common.Agent currentAgent = agents.getAgent(database, agentCode);


            string content = makeApiCall("agentApi/getSyncRecords/" + agentCode, "GET", "");


            SynchEntryCollection collection = SynchEntryCollection.fromJson(content, currentAgent);
            return collection;
        }

        public void setMessageStatus(string agentCode, int messageEntryNo, int status)
        {
            JObject statusObject = new JObject();
            statusObject.Add("agentCode", agentCode);
            statusObject.Add("status", status);

            makeApiCall("agentApi/setMessageStatus/" + messageEntryNo, "POST", statusObject.ToString());

        }

        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
        {
            setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, "", 0);
        }

        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment, int distance)
        {
            JObject statusObject = new JObject();
            statusObject.Add("agentCode", agentCode);
            statusObject.Add("status", status);
            statusObject.Add("positionX", positionX);
            statusObject.Add("positionY", positionY);
            statusObject.Add("shipTime", shipTime);
            statusObject.Add("distance", distance);
            statusObject.Add("comment", comment);

            makeApiCall("agentApi/setShipOrderStatus/" + no, "POST", statusObject.ToString());
        }


        public void initAgentDatabase(string agentCode)
        {

        }

        public void createContainerEntry(string agentCode, string containerNo, int type, int documentType, string documentNo, DateTime entryDateTime, DateTime estimatedArrivalDateTime, int locationType, string locationCode, int positionX, int positionY, int sourceType, string sourceCode, float weight)
        {
            makeApiCall("agentApi/createContainerEntry/" + agentCode + "?containerNo=" + containerNo + "&type=" + type + "&documentType=" + documentType + "&documentNo=" + documentNo + "&entryDateTime=" + entryDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "&estimatedArrivalDateTime=" + estimatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "&locationType=" + locationType + "&locationCode=" + locationCode + "&positionX=" + positionX + "&positionY=" + positionY + "&sourceType=" + sourceType + "&sourceCode=" + sourceCode + "&weight=" + weight, "POST", "{}");
        }



        public void dispose()
        {
            database.close();
        }



        public ShipOrderDistanceCollection getDistancesByDate(DateTime fromDate, DateTime toDate)
        {
            Goldfinger oldGoldfinger = new Goldfinger();
            oldGoldfinger.init(configuration, database);
            ShipOrderDistanceCollection distCollection = oldGoldfinger.getDistancesByDate(fromDate, toDate);
            return distCollection;
        }

        public LineJournalCollection getLineJournals(string agentCode, DateTime shipDate)
        {
            string content = makeApiCall("agentApi/getRoutes/" + agentCode + "?shipmentDate=" + shipDate.ToString("yyyy-MM-dd"), "GET", "");

            LineJournalCollection lineJournalList = LineJournalCollection.fromJson(content);
            return lineJournalList;

        }

        public LineOrderCollection getLineOrders(string agentCode, int lineJournalNo, DateTime date)
        {
            string content = makeApiCall("agentApi/getLineOrders/" + agentCode + "?routeEntryNo=" + lineJournalNo, "GET", "");

            LineOrderCollection lineOrderList = LineOrderCollection.fromJson(content);
            return lineOrderList;

        }


        public void reportContainerForService(string agentCode, string containerNo)
        {
            Goldfinger oldGoldfinger = new Goldfinger();
            oldGoldfinger.init(configuration, database);
            oldGoldfinger.reportContainerForService(agentCode, containerNo);
        }

        public void reportError(string agentCode, string message)
        {
            Goldfinger oldGoldfinger = new Goldfinger();
            oldGoldfinger.init(configuration, database);
            oldGoldfinger.reportError(agentCode, message);
            oldGoldfinger.dispose();

        }

        public void reportLineJournal(string agentCode, DataObjects.LineJournal lineJournal)
        {
            makeApiCall("agentApi/reportRoute/" + agentCode, "POST", lineJournal.toJsonObject());
            return;

        }


        public void setFactoryOrderStatus(string agentCode, DataSet factoryOrderDataSet)
        {
            Goldfinger oldGoldfinger = new Goldfinger();
            oldGoldfinger.init(configuration, database);
            oldGoldfinger.setFactoryOrderStatus(agentCode, factoryOrderDataSet);
        }

        public void setLineOrderContainers(string agentCode, string no, List<string> containerStringList)
        {
            JArray containerArray = new JArray();
            foreach (string containerNo in containerStringList)
            {
                containerArray.Add(containerNo);
            }

            makeApiCall("agentApi/setLineOrderContainers/" + agentCode + "?lineOrderEntryNo=" + no, "POST", containerArray.ToString());
            return;

        }

        public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime, string loadWaitReason)
        {
            makeApiCall("agentApi/setLineOrderStatus/" + agentCode + "?entryNo=" + no + "&status=" + status + "&positionX=" + positionX + "&positionY=" + positionY + "&shipmentDateTime=" + shipTime.ToString("yyyy-MM-dd HH:mm:ss") + "&loadWaitTime=" + loadWaitTime + "&loadWaitReason=" + loadWaitReason, "POST", "{}");
            return;
        }


        public void write(string message, int type)
        {            
        }

        private string makeApiCall(string endpoint, string method, string payload)
        {
            WebRequest webRequest = HttpWebRequest.Create("https://smartshipping.workanywhere.se/" + endpoint);
            webRequest.ContentType = "application/json";
            webRequest.Method = method;
            webRequest.Headers.Add("Authorization", "hepphepp");

            if (payload != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(payload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            return content;

        }

        private string makeTestApiCall(string endpoint, string method, string payload)
        {
            WebRequest webRequest = HttpWebRequest.Create("https://test.smartshipping.workanywhere.se/" + endpoint);
            webRequest.ContentType = "application/json";
            webRequest.Method = method;
            webRequest.Headers.Add("Authorization", "hepphepp");

            if (payload != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(payload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            ServicePointManager.ServerCertificateValidationCallback = delegate (
                Object obj, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors errors)
            {
                return (true);
            };


            WebResponse response = webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            return content;

        }
    }
}
