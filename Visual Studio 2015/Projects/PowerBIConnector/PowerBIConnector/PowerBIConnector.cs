using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace NaviPro.PowerBIConnector
{
    public class PowerBIConnector
    {
        private AuthenticationContext authenticationContext = null;
        private string resourceUri = "https://analysis.windows.net/powerbi/api";
        private string authorityUri = "https://login.windows.net/common/oauth2/authorize";
        private string powerBiAppUri = "https://api.powerbi.com/v1.0/myorg";

        private string userName = "";
        private string password = "";
        private string clientId = "";
        private string groupId = "";
        private Dataset currentDataset = null;
        private List<PowerBITable> tableList;
        private List<PowerBIRecord> recordList;

        public PowerBIConnector(string userName, string password, string clientId, string groupId)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.userName = userName;
            this.password = password;
            this.clientId = clientId;
            this.groupId = groupId;
            tableList = new List<PowerBITable>();
            recordList = new List<PowerBIRecord>();
        }
        public void CreateTableStructure(string datasetName, string jsonData)
        {

            string token = GetAccessToken(userName, password, clientId);

            currentDataset = GetDataset(token, groupId, datasetName);
            if (currentDataset == null)
            {
                currentDataset = CreateDataSet(token, groupId, jsonData);
            }
            
            
        }

        public void CreateTableStructure(string datasetName)
        {

            string jsonData = "{ \"name\": \"" + datasetName.Replace("\"", "") + "\", \"tables\": [ ";
            string tableJson = "";
            foreach (PowerBITable table in tableList)
            {
                if (tableJson != "") tableJson = tableJson + ", ";
                tableJson = tableJson + table.toJson();
            }
            jsonData = jsonData + tableJson + "	] }";

            CreateTableStructure(datasetName, jsonData);

        }

        public void UpdateTableSchema(string datasetName, PowerBITable table)
        {
            string token = GetAccessToken(userName, password, clientId);

            if (currentDataset == null) currentDataset = GetDataset(token, groupId, datasetName);
            if (currentDataset.name != datasetName) currentDataset = GetDataset(token, groupId, datasetName);

            string jsonData = table.toJson();

            // Create a POST web request
            string uri = GetTableUri(groupId, currentDataset, table.name);
            HttpWebRequest request = CreateRequest(uri, "PUT", token, jsonData);

            // Get HttpWebResponse from the POST request
            string responseContent = GetResponse(request, jsonData);

            
        }

        public void AddTable(PowerBITable table)
        {
            tableList.Add(table);
        }

        public void AddRecord(PowerBIRecord record)
        {
            recordList.Add(record);
        }

        public void AddData(string datasetName, string tableName, string jsonData)
        {

            string token = GetAccessToken(userName, password, clientId);

            if (currentDataset == null) currentDataset = GetDataset(token, groupId, datasetName);
            if (currentDataset.name != datasetName) currentDataset = GetDataset(token, groupId, datasetName);

            AddTableRows(token, groupId, currentDataset, tableName, jsonData);

        }

        public void AddData(string datasetName, string tableName)
        {
            string jsonData = "{ \"rows\": [ ";
            string recordJson = "";
            int recordCount = 0;
            foreach (PowerBIRecord record in recordList)
            {
                recordCount++;
                if (recordJson != "") recordJson = recordJson + ", ";
                recordJson = recordJson + record.toJson();

                if (recordCount > 1000)
                {
                    jsonData = jsonData + recordJson + "]}";
                    try
                    {
                        AddData(datasetName, tableName, jsonData);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error sending data to Power BI: "+jsonData);
                        Console.ReadLine();
                    }
                    jsonData = "{ \"rows\": [ ";
                    recordCount = 0;
                }
            }
            jsonData = jsonData + recordJson + "] }";

            AddData(datasetName, tableName, jsonData);

            recordList = new List<PowerBIRecord>();
        }

        public void ClearData(string datasetName, string tableName)
        {

            string token = GetAccessToken(userName, password, clientId);

            if (currentDataset == null) currentDataset = GetDataset(token, groupId, datasetName);
            if (currentDataset.name != datasetName) currentDataset = GetDataset(token, groupId, datasetName);

            ClearTableRows(token, groupId, currentDataset, tableName);

        }

        private void ClearTableRows(string token, string groupId, Dataset dataset, string tableName)
        {
            // Create a DELETE web request
            HttpWebRequest request = CreateRequest(GetTableRowsUri(groupId, dataset, tableName), "DELETE", token);

            // Get HttpWebResponse from the DELETE request
            string responseContent = GetResponse(request, "");

        }

        private void AddTableRows(string token, string groupId, Dataset dataset, string tableName, string jsonData)
        {

            // Create a POST web request
            //throw new Exception(GetTableRowsUri(groupId, dataset, tableName));
            //throw new Exception(GetTableRowsUri(groupId, dataset, tableName));
            HttpWebRequest request = CreateRequest(GetTableRowsUri(groupId, dataset, tableName), "POST", token, jsonData);

            // Get HttpWebResponse from the POST request
            try
            {
                string responseContent = GetResponse(request, jsonData);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message + ": " + jsonData);
            }
        }

        private Dataset GetDataset(string token, string groupId, string datasetName)
        {
            Dataset[] datasetList = GetDatasets(token, groupId);
            foreach (Dataset dataset in datasetList)
            {
                if (dataset.name == datasetName) return dataset;
            }
            return null;
        }

        private Table[] GetTables(string token, string groupId, Dataset dataset)
        {
                // Create a GET web request
                HttpWebRequest request = CreateRequest(GetTablesUri(groupId, dataset), "GET", token);

                // Get HttpWebResponse from the GET request
                string responseContent = GetResponse(request, "");

                // Deserialize JSON response into objects
                GetTablesResponse response = JsonConvert.DeserializeObject<GetTablesResponse>(responseContent);


                return response.value;
        }

        private Dataset CreateDataSet(string token, string groupId, string jsonData)
        {
            string retentionPolicy = "None";

            // Create a POST web request
            string uri = GetDatasetsUri(groupId) + String.Format("?defaultRetentionPolicy={0}", retentionPolicy);
            HttpWebRequest request = CreateRequest(uri, "POST", token, jsonData);

            // Get HttpWebResponse from the POST request
            string responseContent = GetResponse(request, jsonData);

            // Deserialize JSON response into objects
            return JsonConvert.DeserializeObject<Dataset>(responseContent);
        }

        private string GetAccessToken(string userName, string password, string clientId)
        {

            // Validate application settings have been entered
            if ((clientId == String.Empty))
            {
                throw new ArgumentException("Client ID must be set");
            }

            string token = null;

            if (authenticationContext == null)
            {
                // Create an instance of TokenCache to cache the access token
                TokenCache tokenCache = new TokenCache();

                var credential = new UserCredential(userName, password);

                // Create an instance of AuthenticationContext to acquire an Azure access token
                authenticationContext = new AuthenticationContext(authorityUri, tokenCache);

                // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint
                token = authenticationContext.AcquireToken(resourceUri, clientId, credential).AccessToken;
                

            }
            else
            {
                // Retrieve the token from the cache
                token = authenticationContext.AcquireTokenSilent(resourceUri, clientId).AccessToken;

                //Console.WriteLine("Existing token retrieved from cache");
            }

            return token;
        }

 

        private string GetDatasetsUri(string groupId)
        {
            if (groupId == "") return powerBiAppUri + "/datasets";
            return powerBiAppUri + String.Format("/groups/{0}", groupId) + "/datasets";
        }

 
        private string GetTablesUri(string groupId, Dataset dataset)
        {
            return GetDatasetsUri(groupId) + String.Format("/{0}/tables", dataset.id);
        }

        private string GetTableUri(string groupId, Dataset dataset, string tableName)
        {
            return GetTablesUri(groupId, dataset) + String.Format("/{0}", tableName);
        }

        private string GetTableRowsUri(string groupId, Dataset dataset, string tableName)
        {
            return GetTablesUri(groupId, dataset) + String.Format("/{0}/rows", tableName);
        }

        private HttpWebRequest CreateRequest(string uri, string method, string accessToken, string jsonContent)
        {
            HttpWebRequest request = CreateRequest(uri, method, accessToken);

            byte[] byteArray = Encoding.UTF8.GetBytes(jsonContent);
            request.ContentLength = byteArray.Length;

            // Write JSON byte[] into a stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);
            }

            return request;
        }

        private HttpWebRequest CreateRequest(string uri, string method, string accessToken)
        {
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = method;
            request.ContentLength = 0;
            request.ContentType = "application/json";

            // Add access token to the header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken));

            return request;
        }

        private string GetResponse(HttpWebRequest request, string jsonContent)
        {
            string response = string.Empty;

            if (request.ContentLength > 0)
            {
            }

            try
            {
                using (HttpWebResponse httpResponse = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    // Retrieve a StreamReader that holds the response stream
                    using (StreamReader reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                    {
                        response = reader.ReadToEnd();
                    }
                }

                return response;
            }
            catch(WebException webException)
            {
                Console.WriteLine("Exception: " + webException.Message);
                //Console.WriteLine(jsonContent);
                throw new Exception("Exception: " + webException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
                //Console.WriteLine(jsonContent);
                //Console.ReadLine();
                throw new Exception("Exception: " + exception.Message);
            }

            return null;
        }

        private Dataset[] GetDatasets(string token, string groupId)
        {

            // Create a GET web request
            HttpWebRequest request = CreateRequest(GetDatasetsUri(groupId), "GET", token);

            // Get HttpWebResponse from GET request
            string responseContent = GetResponse(request, "");

            // Deserialize JSON response into objects
            GetDatasetsResponse response = JsonConvert.DeserializeObject<GetDatasetsResponse>(responseContent);

            return response.value;

        }

        public class Dataset
        {
            public string id { get; set; }
            public string name { get; set; }
            //public string defaultRetentionPolicy { get; set; }
        }

        public class GetTablesResponse
        {
            public string odatacontext { get; set; }
            public Table[] value { get; set; }
        }
        public class Table
        {
            public string name { get; set; }
        }
        public class GetDatasetsResponse
        {
            public string odatacontext { get; set; }
            public Dataset[] value { get; set; }
        }

    }
}
