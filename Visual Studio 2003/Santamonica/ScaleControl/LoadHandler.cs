using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace Navipro.SantaMonica.ScaleControl
{
    /// <summary>
    /// Summary description for SynchHandler.
    /// </summary>
    public class LoadHandler
    {
        private Logger logger;
        private Configuration configuration;
        private Thread thread;
        private bool running;

        public LoadHandler(Configuration configuration, Logger logger)
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

            int j = 30;

            while (running)
            {
                j++;
                if (j >= 30)
                {
                    try
                    {
                        foreach (string factoryCode in configuration.factoryList)
                        {
                            log("Transfering containers for factory " + factoryCode, 0);
                            transferContainers(factoryCode);
                        }
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

        private void transferContainers(string factoryCode)
        {
            ArrayList orderList = new ArrayList();

            Database database = new Database(logger, configuration);

            string lineOrderListJson = configuration.makeApiCall("scaleApi/getContainersToScale/" + factoryCode, "GET", "");
            JArray jLineOrders = JArray.Parse(lineOrderListJson);

            string customerNo = configuration.customerNo;
            if (configuration.factoryCustomerNo.ContainsKey(factoryCode)) customerNo = configuration.factoryCustomerNo[factoryCode];

            int i = 0;
            while (i < jLineOrders.Count)
            {
                JObject jLineOrder = (JObject)jLineOrders[0];

                string containerNo = (string)jLineOrder["containerNo"];
                string categoryCode = (string)jLineOrder["categoryCode"];

                log("Processing container: " + containerNo, 0);

                int lineOrderEntryNo = (int)jLineOrder["entryNo"];
                int routeEntryNo = (int)jLineOrder["lineRouteEntryNo"];

                string routeJson = configuration.makeApiCall("scaleApi/getLineRoute/" + routeEntryNo, "GET", "");
                JObject jLineRoute = JObject.Parse(routeJson);


                string agentCode =(string)jLineRoute["agentCode"];
                string organizationCode = (string)jLineRoute["companyNo"];

                string shippingCustomerNo = (string)jLineOrder["lineCustomerNo"];
                string details = (string)jLineOrder["details"];

                ViktoriaKund customer = new ViktoriaKund(database, shippingCustomerNo);
                if (!customer.recordExists)
                {
                    customer.namn = (string)jLineOrder["lineCustomerName"];
                    customer.adress1 = (string)jLineOrder["address"];
                    customer.adress2 = (string)jLineOrder["address2"];
                    customer.postnr = (string)jLineOrder["postCode"];
                    customer.ort = (string)jLineOrder["city"];
                    log(customer.getQuery(), 0);
                    customer.save();
                }

                string containerTypeCode = (string)jLineOrder["containerTypeCode"];

                log("Sending containerNo " + containerNo+" for factory "+factoryCode, 0);


                ViktoriaUppdrag assignment = new ViktoriaUppdrag(database, lineOrderEntryNo.ToString() + "-" + containerNo);
                assignment.benamning = customer.namn;
                assignment.container = containerNo;
                assignment.leverantor = shippingCustomerNo;
                assignment.kund = customerNo;  // Konvex Mosserud
                assignment.artikel = categoryCode;
                if (configuration.combinedContainers)
                {
                    assignment.vaglangd = 1;
                    assignment.anmarkning = details;
                }
                else
                {
                    assignment.vaglangd = 1;
                    assignment.anmarkning = "";
                }

                assignment.referens = lineOrderEntryNo.ToString();
                assignment.anlaggning = configuration.factoryId[factoryCode];

                assignment.save();

                int containerNoInt = 0;
                try
                {
                    containerNoInt = int.Parse(containerNo);
                }
                catch (Exception)
                { }

                if ((containerNoInt != 1) && (containerNoInt != 2))
                {
                    ViktoriaIdent ident = new ViktoriaIdent(database, containerNo);
                    ident.slapcont = containerTypeCode;
                    ident.uppdrag = assignment.uppdrag;
                    ident.bil = agentCode;
                    ident.transportor = "R " + routeEntryNo.ToString();
                    ident.behorighet = 5;

                    ident.save();
                }

                configuration.makeApiCall("scaleApi/setContainerAsSent/" + lineOrderEntryNo, "GET", "");

                i++;
            }

            database.close();

        }


 

		private void log(string message, int type)
		{
			logger.write("[LoadHandler] "+message, type);
		}

		private ArrayList parseContainerEntryDataSet(DataSet containerEntryDataSet)
		{
			ArrayList unloadedContainers = new ArrayList();

			int i = 0;
			while (i < containerEntryDataSet.Tables[0].Rows.Count)
			{
				unloadedContainers.Add(containerEntryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());	

				i++;
			}

			return unloadedContainers;
		}

		private void printDocument(int lineOrderEntryNo)
		{
			if (configuration.print)
			{
				log("Printing line order: "+lineOrderEntryNo, 0);
			
				System.Net.WebRequest webRequest = System.Net.WebRequest.Create(configuration.printDocumentUrl.Replace("%1", lineOrderEntryNo.ToString()));
				System.Net.WebResponse webResponse = webRequest.GetResponse();
				System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
				string content = streamReader.ReadToEnd();
				streamReader.Close();
				webResponse.Close();

				string contentFileName = System.Environment.GetEnvironmentVariable("TEMP")+"\\"+lineOrderEntryNo.ToString()+".htm";

				System.IO.StreamWriter streamWriter = System.IO.File.CreateText(contentFileName);
				streamWriter.WriteLine(content);
				streamWriter.Flush();
				streamWriter.Close();

				string arguments = "file=\""+contentFileName+"\" nogui";
				if (configuration.printer != "") arguments = arguments + " printername=\""+configuration.printer+"\"";

				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(configuration.printCommand, arguments));
			}
		}
	}
}
