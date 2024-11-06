using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using Microsoft.Win32;

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

			while(running)
			{
				j++;
				if (j >= 120)
				{

					try
					{
						processWeights();
						checkMissingTransactions();
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

		private void log(string message, int type)
		{
			logger.write("[ScaleHandler] "+message, type);
		}

		private void processWeights()
		{

			Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner scaleRunner = new Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner();
			scaleRunner.Url = configuration.webServiceUrl;

			Database database = new Database(logger, configuration);


			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT Transnr FROM "+database.prefix+".TRANSSPECIAL");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "trans");
			adapter.Dispose();

			string transNo = "";
			
			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				transNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
			
				adapter = database.dataAdapterQuery("SELECT Transnr, Container, Referens, Transtid, Kund, Leverantor, Artikel, Nettovikt, Slapcont, Vaglangd, Bil, Uppdrag, Status, Container2 FROM "+database.prefix+".TRANS WHERE Transnr = '"+transNo+"'");

				DataSet transEntryDataSet = new DataSet();
				adapter.Fill(transEntryDataSet, "trans");
				adapter.Dispose();

				adapter = database.dataAdapterQuery("SELECT Anmarkning FROM "+database.prefix+".TRANSSUB WHERE Transnr = '"+transNo+"'");

				DataSet transSubEntryDataSet = new DataSet();
				adapter.Fill(transSubEntryDataSet, "trans");
				adapter.Dispose();
		

				int type = 0;
				if (transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != configuration.customerNo) type = 1;

				log("Creating scale entry: "+transNo, 0);
				scaleRunner.createScaleEntry2(configuration.factoryCode, type, transEntryDataSet, transSubEntryDataSet);

				if (transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "2")
				{
					database.nonQuery("DELETE FROM "+database.prefix+".UPPDRAG WHERE Login = 'JOB' AND Uppdrag = '"+transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()+"'");
					database.nonQuery("DELETE FROM "+database.prefix+".IDENT WHERE Login = 'JOB' AND Uppdrag = '"+transEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()+"'");
				}

				database.nonQuery("DELETE FROM "+database.prefix+".TRANSSPECIAL WHERE Transnr = '"+transNo+"'");


				i++;
			}

			database.close();

			
		}

		private void checkMissingTransactions()
		{
			Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner scaleRunner = new Navipro.SantaMonica.Goldfinger.WebService.ScaleRunner();
			scaleRunner.Url = configuration.webServiceUrl;

			Database database = new Database(logger, configuration);


			string[] missingArray = scaleRunner.getMissingScaleEntries(configuration.factoryCode);

			if (missingArray.Length > 0)
			{
				int i = 0;
				while (i < missingArray.Length)
				{
					int transNo = int.Parse(missingArray[i].ToString());
                    
					try
					{
						database.nonQuery("INSERT INTO "+database.prefix+".TRANSSPECIAL (Transnr) VALUES ('"+transNo+"')");
					}
					catch(Exception){}

					i++;
				}
			}

			missingArray = scaleRunner.getUnfinishedScaleEntries(configuration.factoryCode);

			if (missingArray != null)
			{
				if (missingArray.Length > 0)
				{
					int i = 0;
					while (i < missingArray.Length)
					{
						int transNo = int.Parse(missingArray[i].ToString());

						try
						{
							database.nonQuery("INSERT INTO "+database.prefix+".TRANSSPECIAL (Transnr) VALUES ('"+transNo+"')");
						}
						catch(Exception){}

						i++;
					}
				}
			}

			database.close();
		}

	}
}
