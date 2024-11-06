using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;

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

			while(running)
			{
				j++;
				if (j >= 30)
				{
					try
					{
						transferContainers();
					}
					catch(Exception e)
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

		private void transferContainers()
		{
			ArrayList orderList = new ArrayList();

			Database database = new Database(logger, configuration);

			Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner scaleRunner = new Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner();
			scaleRunner.Url = configuration.webServiceUrl;
			
			DataSet containerDataSet = scaleRunner.getContainersToScale(configuration.factoryCode);

			int i = 0;
			while (i < containerDataSet.Tables[0].Rows.Count)
			{
				string containerNo = containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
				string categoryCode = containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();

				log("Processing container: "+containerNo, 0);

				int lineOrderEntryNo = int.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());

				DataSet lineOrderDataSet = scaleRunner.getLineOrder(lineOrderEntryNo);
				if (lineOrderDataSet.Tables[0].Rows.Count > 0)
				{
					int lineJournalEntryNo = int.Parse(lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());

					DataSet lineJournalDataSet = scaleRunner.getLineJournal(lineJournalEntryNo, configuration.factoryCode);
					if (lineJournalDataSet.Tables[0].Rows.Count > 0)
					{

						string agentCode = lineJournalDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
						string organizationCode = lineJournalDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

						string shippingCustomerNo = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
						string details = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

						ViktoriaKund customer = new ViktoriaKund(database, shippingCustomerNo);
						if (!customer.recordExists)
						{
							customer.namn = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
							customer.adress1 = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
							customer.adress2 = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
							customer.postnr = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
							customer.ort = lineOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
							customer.save();
						}

						DataSet lineOrderContainerDataSet = scaleRunner.getLineOrderContainers(lineOrderEntryNo);

						DataSet containerEntryDataSet = scaleRunner.getContainerEntry(containerNo);
						string containerTypeCode = containerEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

						log("Sending containerNo "+containerNo, 0);


						ViktoriaUppdrag assignment = new ViktoriaUppdrag(database, lineOrderEntryNo.ToString()+"-"+containerNo);
						assignment.benamning = customer.namn;
						assignment.container = containerNo;
						assignment.leverantor = shippingCustomerNo;
						assignment.kund = configuration.customerNo;  // Konvex Mosserud
						assignment.artikel = categoryCode;
						if (configuration.combinedContainers)
						{
							assignment.vaglangd = lineOrderContainerDataSet.Tables[0].Rows.Count;
							assignment.anmarkning = details;
						}
						else
						{
							assignment.vaglangd = 1;
							assignment.anmarkning = "";
						}

						assignment.referens = lineOrderEntryNo.ToString();
							
						assignment.save();

						int containerNoInt = 0;
						try
						{
							containerNoInt = int.Parse(containerNo);
						}
						catch(Exception)
						{}
							
						if ((containerNoInt != 1) && (containerNoInt != 2))
						{
							ViktoriaIdent ident = new ViktoriaIdent(database, containerNo);
							ident.slapcont = containerTypeCode;
							ident.uppdrag = assignment.uppdrag;
							ident.bil = agentCode;
							ident.transportor = "R "+lineJournalEntryNo.ToString();

							ident.save();
						}

						scaleRunner.setContainerAsSent(lineOrderEntryNo, int.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));

						if (!orderList.Contains(lineOrderEntryNo.ToString()))
						{
							this.printDocument(lineOrderEntryNo);
							orderList.Add(lineOrderEntryNo.ToString());
						}
					}


				}

				//scaleRunner.setLineJournalAsSent(lineJournalEntryNo);

				i++;
			}

			database.close();

		}

		private void transferJournals()
		{
			Database database = new Database(logger, configuration);

			
			Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner scaleRunner = new Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner();
			scaleRunner.Url = configuration.webServiceUrl;
			DataSet lineJournalDataSet = scaleRunner.getUnSentFactoryLineJournals(configuration.factoryCode);


			int i = 0;
			while (i < lineJournalDataSet.Tables[0].Rows.Count)
			{
				int lineJournalEntryNo = int.Parse(lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
				string agentCode = lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
				string organizationCode = lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

				log("Sending route "+lineJournalEntryNo+" to factory.", 0);

				DataSet containerEntriesDataSet = scaleRunner.getLineJournalEntries(lineJournalEntryNo);
				ArrayList unloadedContainers = parseContainerEntryDataSet(containerEntriesDataSet);


				DataSet lineOrderDataSet = scaleRunner.getLineJournalOrders(lineJournalEntryNo);
				int j = 0;
				while (j < lineOrderDataSet.Tables[0].Rows.Count)
				{

					int lineOrderEntryNo = int.Parse(lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

					log("Sending order "+lineOrderEntryNo, 0);

					string shippingCustomerNo = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString();
					string details = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(13).ToString();

					ViktoriaKund customer = new ViktoriaKund(database, shippingCustomerNo);
					if (!customer.recordExists)
					{
						customer.namn = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString();
						customer.adress1 = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString();
						customer.adress2 = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString();
						customer.postnr = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(8).ToString();
						customer.ort = lineOrderDataSet.Tables[0].Rows[j].ItemArray.GetValue(9).ToString();
						customer.save();
					}

					DataSet containerDataSet = scaleRunner.getLineOrderContainers(lineOrderEntryNo);
					int k = 0;
					while (k < containerDataSet.Tables[0].Rows.Count)
					{
						string containerNo = containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(2).ToString();
						string categoryCode = containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(3).ToString();

						if (unloadedContainers.Contains(containerNo))
						{
							DataSet containerEntryDataSet = scaleRunner.getContainerEntry(containerNo);
							string containerTypeCode = containerEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

							log("Sending containerNo "+containerNo, 0);


							ViktoriaUppdrag assignment = new ViktoriaUppdrag(database, lineOrderEntryNo.ToString()+"-"+containerNo);
							assignment.benamning = customer.namn;
							assignment.container = containerNo;
							assignment.leverantor = shippingCustomerNo;
							assignment.kund = configuration.customerNo;  // Konvex Mosserud
							assignment.artikel = categoryCode;
							if (configuration.combinedContainers)
							{
								assignment.vaglangd = containerDataSet.Tables[0].Rows.Count;
								assignment.anmarkning = details;
							}
							else
							{
								assignment.vaglangd = 1;
								assignment.anmarkning = "";
							}

							assignment.referens = lineOrderEntryNo.ToString();
							
							assignment.save();

							int containerNoInt = 0;
							try
							{
								containerNoInt = int.Parse(containerNo);
							}
							catch(Exception)
							{}
							
							if ((containerNoInt != 1) && (containerNoInt != 2))
							{
								ViktoriaIdent ident = new ViktoriaIdent(database, containerNo);
								ident.slapcont = containerTypeCode;
								ident.uppdrag = assignment.uppdrag;
								ident.bil = agentCode;
								ident.transportor = "R "+lineJournalEntryNo.ToString();

								ident.save();
							}

							unloadedContainers.Remove(containerNo);
						}


						k++;
					}

					this.printDocument(lineOrderEntryNo);

					j++;
				}

				scaleRunner.setLineJournalAsSent(lineJournalEntryNo);

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
