using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Data;

namespace Navipro.SMSCom.Lib
{

    public class SMSCom
    {
        private Navipro.Base.Common.Logger logger;
        private Navipro.Base.Common.Configuration configuration;
        private mCore.SMS mCoreSMS;
        private Thread thread;

        private bool running;

        public SMSCom(Navipro.Base.Common.Logger logger, Navipro.Base.Common.Configuration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

            
            log("Starting SMSCom...", 0);
            this.mCoreSMS = new mCore.SMS();

            
            mCore.License objLic = mCoreSMS.License();
            objLic.Company = "NAVIPRO AB";
            objLic.LicenseType = "LITE-DISTRIBUTION";
            objLic.Key = "XHJT-NCLY-6KVF-M9EA";

            mCoreSMS.NewMessageReceived += new mCore.SMS.NewMessageReceivedEventHandler(mCoreSMS_NewMessageReceived);

            log("Licensed: "+objLic.IsLicensed.ToString(), 0);
            log("Setting communication parameters...", 0);

            mCoreSMS.Port = configuration.getConfigValue("gsm_port");
            mCoreSMS.BaudRate = mCore.BaudRate.BaudRate_19200;
            mCoreSMS.DataBits = mCore.DataBits.Eight;
            mCoreSMS.Parity = mCore.Parity.None;
            mCoreSMS.StopBits = mCore.StopBits.One;
            mCoreSMS.FlowControl = mCore.FlowControl.RTS_CTS;
            mCoreSMS.PIN = configuration.getConfigValue("gsm_pin");

            mCoreSMS.NewMessageIndication = true;
            mCoreSMS.AutoDeleteNewMessage = true;

            int i = 0;
            while (i <= 60)
            {
                i++;
                mCoreSMS.Command("AT+CMGD=1");
            }


            log("Establishing connection with modem...", 0);
            if (mCoreSMS.Connect())
            {
                log("Communication established...", 0);
            }
            else
            {
                log("Failed to establish communication with modem...", 2);
            }

            running = true;
            thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        public void run()
        {
            while (running)
            {
                sendUnsentMessages();

                Thread.Sleep(2000);
            }

        }

        public void sendMessage(string phoneNo, string text)
        {
            log("Sending message to " + phoneNo + ": " + text, 0);
            mCoreSMS.SendSMS(phoneNo, text);
        }

        public void stop()
        {
            log("Shutting down connections...", 0);
            running = false;
            mCoreSMS.Disconnect();
            mCoreSMS.Dispose();
        }

        void mCoreSMS_NewMessageReceived(object sender, mCore.NewMessageReceivedEventArgs e)
        {
            bool messageSubmitted = false;

            while (!messageSubmitted)
            {
                try
                {
                    log("Incoming message from " + e.Phone + ": " + e.TextMessage, 0);

                    Navipro.Base.Common.Database database = new Navipro.Base.Common.Database(logger, configuration);

                    SMSMessage smsMessage = new SMSMessage();
                    smsMessage.type = 0;
                    smsMessage.phoneNo = e.Phone;
                    smsMessage.message = e.TextMessage;
                    smsMessage.receivedDateTime = DateTime.Now;
                    smsMessage.save(database);

                    messageSubmitted = true;
                }
                catch (Exception ex)
                {

                    logger.write("SMSCom", 2, "Exception: " + ex.Message);

                    Thread.Sleep(10000);
                }
            }
        }

        private void log(string message, int type)
        {
            logger.write("SMSCom", type, message);
        }

        private void sendUnsentMessages()
        {
            try
            {
                Navipro.Base.Common.Database database = new Navipro.Base.Common.Database(logger, configuration);

                SMSMessages smsMessages = new SMSMessages();
                DataSet sendDataSet = smsMessages.getUnhandledDataSet(database, 1);
                int i = 0;
                if (sendDataSet.Tables[0].Rows.Count > 0)
                {
                    logger.write("SMSCom", 0, "Sending unsent messages...");

                    while (i < sendDataSet.Tables[0].Rows.Count)
                    {
                        SMSMessage smsMessage = new SMSMessage(sendDataSet.Tables[0].Rows[i]);

                        sendMessage(smsMessage.phoneNo, smsMessage.message);

                        smsMessage.handled = true;
                        smsMessage.save(database);

                        i++;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.write("SMSCom", 2, "Exception: " + ex.Message);
            }

        }
    }
}
