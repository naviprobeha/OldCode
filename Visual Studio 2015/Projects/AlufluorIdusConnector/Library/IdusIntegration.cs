using NaviPro.Alufluor.Idus.Library.Connectors;
using NaviPro.Alufluor.Idus.Library.Helpers;
using NaviPro.Alufluor.Idus.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library
{
    public class IdusIntegration
    {
        private Logger logger;
        private Configuration configuration;
        private Thread thread;
        private bool running;
        public IdusIntegration(Logger logger)
        {
            this.logger = logger;
            this.configuration = new Configuration();
            configuration.load();
        }

        public void start()
        {
            log("INFO", "Starting Idus Integration.");

            log("INFO", "Checking configuration...");

            try
            {
                configuration.check();
            }
            catch(Exception e)
            {
                log("ERROR", "Invalid configuration: "+e.Message);
                return;
            }


            running = true;
            thread = new Thread(new ThreadStart(run));
            thread.Start();

        }

        public void stop()
        {
            log("INFO", "Stopping Idus Integration.");

            running = false;
            thread.Abort();           
        }
        

        void run()
        {
            DateTime lastRun = DateTime.Now.AddDays(-1);

            while(running)
            {
                if (lastRun.AddMinutes(configuration.intervalMinutes) < DateTime.Now)
                {

                    log("INFO", "Running...");

                    updateDimensions("KST", "3");
                    updateDimensions("POS", "4");
                    updateDimensions("PROJ", "5");
                    updateGLAccounts();

                    transferPurchaseList(DateTime.Today.AddDays(-30), DateTime.Today.AddDays(1));
                    lastRun = DateTime.Now;
                }
                Thread.Sleep(1000);

            }
        }

        private void updateDimensions(string bcDimensionCode, string idusAccountType)

        {
            BCConnector bcConnector = new BCConnector(configuration, logger);
            IdusConnector idusConnector = new IdusConnector(configuration, logger);


            List<BCDimensionValue> dimensionValueList = bcConnector.GetDimensionValues();
            List<IdusAccount> idusAccountList = idusConnector.getAccounts(idusAccountType);

            log("INFO", "Processing dimension values for " + bcDimensionCode + " (" + idusAccountType + ")");

            if (idusAccountList == null)
            {
                log("ERROR", "Unexpected response from Idus. Aborting...");
                return;

            }




            foreach (BCDimensionValue dimValue in dimensionValueList.Where(d => d.dimensionCode == bcDimensionCode))
            {
                log("INFO", "Checking dimension value " + dimValue.dimensionCode + " " + dimValue.code);

                IdusAccount idusAccount = idusAccountList.FirstOrDefault(a => a.fields.FAccount == dimValue.code);
                if (idusAccount != null)
                {
                    if (idusAccount.fields.FAccountText != dimValue.name)
                    {
                        log("INFO", "Updating dimension value " + dimValue.dimensionCode + " " + dimValue.code);

                        idusConnector.updateAccount(dimValue.toIdusAccount(idusAccountType));
                    }
                }
                else
                {
                    log("INFO", "Adding dimension value " + dimValue.dimensionCode + " " + dimValue.code);

                    idusConnector.createAccount(dimValue.toIdusAccount(idusAccountType));
                }
            }


        }
        

        private void updateGLAccounts()
        {
            BCConnector bcConnector = new BCConnector(configuration, logger);
            IdusConnector idusConnector = new IdusConnector(configuration, logger);

            List<BCGLAccount> glAccountList = bcConnector.GetGLAccounts();
            List<IdusAccount> idusAccountList = idusConnector.getAccounts("1");

            foreach (BCGLAccount glAccount in glAccountList)
            {
                log("INFO", "Checking gl account " + glAccount.no);
                IdusAccount idusAccount = idusAccountList.FirstOrDefault(a => a.fields.FAccount == glAccount.no);
                if (idusAccount != null)
                {
                    if (idusAccount.fields.FAccountText != glAccount.name)
                    {
                        log("INFO", "Updating gl account " + glAccount.no);

                        idusConnector.updateAccount(glAccount.toIdusAccount());
                    }
                }
                else
                {
                    log("INFO", "Adding gl account " + glAccount.no);

                    idusConnector.createAccount(glAccount.toIdusAccount());
                }
            }
        }


        private void transferPurchaseList(DateTime fromDate, DateTime toDate)
        {
            BCConnector bcConnector = new BCConnector(configuration, logger);
            IdusConnector idusConnector = new IdusConnector(configuration, logger);

            List<IdusPurchase> purchaseList = idusConnector.getPurchaseList(fromDate, toDate);
            if (purchaseList == null)
            {

                log("ERROR", "Unexpected response from Idus. Aborting...");
                return;

            }

            List<BCPurchaseLine> bcList = new List<BCPurchaseLine>();

            foreach (IdusPurchase idusPurchase in purchaseList)
            {
                bcList = idusPurchase.fields.toBCPurchaseLine(bcList);
            }

            log("INFO", "Pushing " + bcList.Count + " purchases to BC.");

            bcConnector.PushPurchaseList(bcList);

        }



        private void log(string type, string message)
        {
            logger.write(type, message);
        }
    }
}
