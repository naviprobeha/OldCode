using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OutnorthYodaFeedImport
{
    public class Importer
    {
        private int fileNo = 0;
        private const string Token = "22EE6925D781848D392299871319BCBA5D247320B766197CEC24DCE4A5CEA78E";

        public Importer()
        {

        }

        public void process()
        {
            writeLog("Starting import...");

            StreamReader streamReader = downloadFeedFromYoda();
            if (streamReader != null)
            {
                streamReader.ReadLine();

                List<ProductVariant> productList = parseFeed(streamReader);

                streamReader.Close();

                writeLog("No of products: " + productList.Count);

                //lookForProduct(productList, "A046480");
                
                writeLog("Clear all sales prices...");
                clearAllSalesPrices();

                writeLog("Updating...");
                updateSalesPrices(productList);

                writeLog("Sync to devices...");
                syncToDevices();
                
            }


            writeLog("Done!");



        }

        private StreamReader downloadFeedFromYoda()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var webRequest = HttpWebRequest.CreateHttp("https://files.channable.com/twnxS_QK1t84oGIT4gLXkg==.csv");
            webRequest.Method = "GET";

            try
            {
                var webResponse = webRequest.GetResponse();

                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());

                return streamReader;

            }
            catch(Exception e)
            {
                writeLog("Webrequest error: " + e.Message);
                return null;
            }

        }

        private List<ProductVariant> parseFeed(StreamReader streamReader)
        {
            List<ProductVariant> list = new List<ProductVariant>();

            int lines = 0;
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                string[] columns = line.Split(new string[] { "\";\"" }, StringSplitOptions.None);

                ProductVariant productVariant = new ProductVariant(columns);
                if ((productVariant.externalId != "") && (productVariant.itemNo != "") && (productVariant.externalId != null) && (productVariant.itemNo != null))
                {
                    list.Add(productVariant);
                }

                lines++;
            }

            return list;
        }



        private void updateSalesPrices(List<ProductVariant> productList)
        {
            List<ProductVariant> batch = new List<ProductVariant>();

            foreach(ProductVariant variant in productList)
            {
                        
                batch.Add(variant);

                if (batch.Count > 100)
                {
                    sendToCloud(batch);
                    batch = new List<ProductVariant>();
                }
            }

            if (batch.Count > 100)
            {
                sendToCloud(batch);
                batch = new List<ProductVariant>();
            }

        }

        private void lookForProduct(List<ProductVariant> productList, string itemNo)
        {
            List<ProductVariant> batch = new List<ProductVariant>();

            bool found = false;

            foreach (ProductVariant variant in productList)
            {
                if (variant.itemNo == itemNo)
                {
                    Console.WriteLine(variant.itemNo + " " + variant.variantCode + " " + variant.externalId + " " + variant.primaryUnitPrice.ToString() + " " + variant.primarySalesPrice);
                    found = true;
                }
            }

            if (!found) Console.WriteLine(itemNo + " not found...");
            Console.ReadLine();
        }


        private void sendToCloud(List<ProductVariant> batch)
        {
            fileNo++;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(batch);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            

            writeLog("Uploading batch " + fileNo);

            /*
            StreamWriter writer = new StreamWriter("C:\\temp\\outnorth_" + fileNo + ".json");
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
            */


            var webRequest = HttpWebRequest.CreateHttp("https://anyware.se/erpapi/salesPrices");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", Token);
            webRequest.ReadWriteTimeout = 180000;
            webRequest.Timeout = 180000;

            StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();


            try
            {
                var webResponse = webRequest.GetResponse();
                webResponse.Close();

            }
            catch (Exception e)
            {
                writeLog("Webrequest error: " + e.Message);

            }

        }

        private void clearAllSalesPrices()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = "[]";

            var webRequest = HttpWebRequest.CreateHttp("https://anyware.se/erpapi/salesPrices?clear=true");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", Token);
            webRequest.ReadWriteTimeout = 180000;
            webRequest.Timeout = 180000;

            StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();


            try
            {
                var webResponse = webRequest.GetResponse();


            }
            catch (Exception e)
            {
                writeLog("Webrequest error: " + e.Message);

            }

        }

        private void syncToDevices()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = "[]";

            var webRequest = HttpWebRequest.CreateHttp("https://anyware.se/erpapi/salesPrices?syncToDevices=true");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", Token);
            webRequest.ReadWriteTimeout = 180000;
            webRequest.Timeout = 180000;

            StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();


            try
            {
                var webResponse = webRequest.GetResponse();


            }
            catch (Exception e)
            {
                writeLog("Webrequest error: " + e.Message);

            }

        }


        private void writeLog(string message)
        {
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
        }

        private List<string> loadMonitoringSkus()
        {
            try
            {
                StreamReader streamReader = new StreamReader("C:\\temp\\outnorth_monitoring_skus.json");
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json);
            }
            catch(Exception)
            {

            }
            return new List<string>();
        }

        private void saveMonitoringSkus(List<string> list)
        {
            StreamWriter streamWriter = new StreamWriter("C:\\temp\\outnorth_monitoring_skus.json");
            streamWriter.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            streamWriter.Flush();
            streamWriter.Close();
        }

    }
}
