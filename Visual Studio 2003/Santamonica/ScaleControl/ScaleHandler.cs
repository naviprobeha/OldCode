using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Navipro.SantaMonica.ScaleControl
{
    /// <summary>
    /// Summary description for SynchHandler.
    /// </summary>
    public class ScaleHandler
    {
        private Logger logger;
        private Configuration configuration;
        private Thread thread;
        private bool running;

        public ScaleHandler(Configuration configuration, Logger logger)
        {
            this.logger = logger;
            this.configuration = configuration;
            //
            // TODO: Add constructor logic here
            //


            thread = new Thread(new ThreadStart(run));
            thread.Start();

        }

        public void start()
        {
            running = true;
        }

        public void run()
        {

            int j = 120;

            while (running)
            {
                j++;
                if (j >= 120)
                {

                    try
                    {

                        processWeights();
                        checkMissingTransactions();
                    }
                    catch (Exception e)
                    {
                        log(e.Message, 1);
                    }

                    j = 0;
                }

                Thread.Sleep(1000);
            }

            log("Stopped...", 0);

        }

        public void stop()
        {
            running = false;
        }

        private void log(string message, int type)
        {
            logger.write("[ScaleHandler] " + message, type);
        }

        private void processWeights()
        {


            Database database = new Database(logger, configuration);


            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT Transnr, Sanddatum FROM " + database.prefix + ".TRANSSPECIAL");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "trans");
            adapter.Dispose();

            string transNo = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                transNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                DateTime createdDate = DateTime.Now;
                DateTime.TryParse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), out createdDate);

                adapter = database.dataAdapterQuery("SELECT Transnr, Container, Referens, Transtid, Kund, Leverantor, Artikel, Nettovikt, Slapcont, Vaglangd, Bil, Uppdrag, Status, Container2, Anlaggning FROM " + database.prefix + ".TRANS WHERE Transnr = '" + transNo + "'");

                DataSet transEntryDataSet = new DataSet();
                adapter.Fill(transEntryDataSet, "trans");
                adapter.Dispose();

                if (transEntryDataSet.Tables[0].Rows.Count > 0)
                {

                    adapter = database.dataAdapterQuery("SELECT Anmarkning FROM " + database.prefix + ".TRANSSUB WHERE Transnr = '" + transNo + "'");

                    DataSet transSubEntryDataSet = new DataSet();
                    adapter.Fill(transSubEntryDataSet, "trans");
                    adapter.Dispose();

                    string comment = "";
                    if (transSubEntryDataSet.Tables[0].Rows.Count > 0)
                    {
                        comment = transSubEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }

                    int type = 0;
                    if (transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()) type = 1;

                    log("Creating scale entry: " + transNo, 0);

                    int lineOrderEntryNo = 0;
                    if (transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString().IndexOf("-") > 0)
                    {
                        lineOrderEntryNo = int.Parse(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString().Substring(0, transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString().IndexOf("-")));
                    }

                    int entryNo = int.Parse(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    string lineCustomerNo = "";
                    string factoryCode = configuration.factoryCode;
                    if (configuration.customerFactoryCode.ContainsKey(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(14).ToString())) factoryCode = configuration.customerFactoryCode[transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()];


                    log("Type: " + type.ToString()+", VendorNo: "+ transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()+", CustomerNo: "+ transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(), 0);

                    if (type == 0)
                    {
                        string vendorNo = transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                        lineCustomerNo = vendorNo;

                        log("Type=0, FactoryCustomerNo " + vendorNo + " exists: " + configuration.factoryCustomerNo.ContainsKey(vendorNo).ToString(), 0);

                        //if (configuration.customerFactoryCode.ContainsKey(vendorNo)) factoryCode = configuration.customerFactoryCode[vendorNo];
                    }
                    else
                    {
                        string customerNo = transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                        log("Type=1, FactoryCustomerNo " + customerNo + " exists: " + configuration.factoryCustomerNo.ContainsKey(customerNo).ToString(), 0);

                        lineCustomerNo = customerNo;
                        //if (configuration.customerFactoryCode.ContainsKey(customerNo)) factoryCode = configuration.customerFactoryCode[customerNo];


                    }


                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    JsonTextWriter writer = new JsonTextWriter(sw);

                    writer.WriteStartObject();

                    writer.WritePropertyName("factoryNo");
                    writer.WriteValue(factoryCode);

                    writer.WritePropertyName("type");
                    writer.WriteValue(type);

                    writer.WritePropertyName("entryNo");
                    writer.WriteValue(entryNo);

                    writer.WritePropertyName("containerNo");
                    writer.WriteValue(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                    writer.WritePropertyName("reference");
                    writer.WriteValue(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());

                    string dateTime = transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    if (dateTime == "") dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



                    writer.WritePropertyName("entryDateTime");
                    writer.WriteValue(dateTime);

                    writer.WritePropertyName("lineCustomerNo");
                    writer.WriteValue(lineCustomerNo);


                    writer.WritePropertyName("containerTypeCode");
                    writer.WriteValue(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());

                    writer.WritePropertyName("agentCode");
                    writer.WriteValue(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());

                    writer.WritePropertyName("lineOrderEntryNo");
                    writer.WriteValue(lineOrderEntryNo);


                    float weight = 0;
                    float.TryParse(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString().Replace(".", ","), out weight);

                    log("Convert " + transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() + " to float: " + weight.ToString(), 0);


                    writer.WritePropertyName("weight");
                    writer.WriteValue(weight);

                    writer.WritePropertyName("categoryCode");
                    writer.WriteValue(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());

                    log("Converting " + transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() + " to int", 0);

                    writer.WritePropertyName("status");
                    writer.WriteValue(int.Parse(transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()));

                    writer.WritePropertyName("noOfContainers");
                    writer.WriteValue(1);

                    writer.WritePropertyName("comment");
                    writer.WriteValue(comment);

                    writer.WriteEndObject();


                    string payload = sb.ToString();
                    log(payload, 0);

                    configuration.makeApiCall("scaleApi/createScaleEntry/" + entryNo + "?type=" + type, "POST", payload);

                    if (transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "2")
                    {
                        database.nonQuery("DELETE FROM " + database.prefix + ".UPPDRAG WHERE Login = 'JOB' AND Uppdrag = '" + transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() + "'");
                        database.nonQuery("DELETE FROM " + database.prefix + ".IDENT WHERE Login = 'JOB' AND Uppdrag = '" + transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() + "'");
                    }

                    database.nonQuery("DELETE FROM " + database.prefix + ".TRANSSPECIAL WHERE Transnr = '" + transNo + "'");
                }
                else
                {
                    if (createdDate.AddDays(5) < DateTime.Today)
                    {
                        database.nonQuery("DELETE FROM " + database.prefix + ".TRANSSPECIAL WHERE Transnr = '" + transNo + "'");
                    }
                }

                i++;
            }

            database.close();


        }

        private void checkMissingTransactions()
        {
            Database database = new Database(logger, configuration);
            string entryArrayJson = configuration.makeApiCall("scaleApi/getMissingScaleEntries/" + configuration.factoryCode, "GET", "");

            
            JArray array = JArray.Parse(entryArrayJson);
            /*
            for (int i = 0; i < array.Count; i++)
            {
                int transNo = int.Parse(array[i].ToString());

                try
                {
                    database.nonQuery("INSERT INTO " + database.prefix + ".TRANSSPECIAL (Transnr) VALUES ('" + transNo + "')");
                }
                catch (Exception) { }

            }
            */


            entryArrayJson = configuration.makeApiCall("scaleApi/getUnfinishedScaleEntries/" + configuration.factoryCode, "GET", "");

            array = JArray.Parse(entryArrayJson);

            for (int i = 0; i < array.Count; i++)
            {
                int transNo = int.Parse(array[i].ToString());

                try
                {
                    database.nonQuery("INSERT INTO " + database.prefix + ".TRANSSPECIAL (Transnr) VALUES ('" + transNo + "')");
                }
                catch (Exception) { }

            }  

			database.close();
            
		}

	}
}
