using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navipro.Backoffice.Library
{
    public class MailSender
    {
        private Configuration configuration;
        private Logger logger;
        private Database database;

        public MailSender(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;

            database = new Database(configuration);

        }

        public void processMail()
        {
            bool run = true;
            while (run)
            {
                CaseLog caseLog = CaseLog.getFirstUnsentMessage(database);
                if (caseLog != null)
                {
                    sendEmail(caseLog);
                }
                else
                {
                    run = false;
                }
                run = false;
            }
        }

        private void sendEmail(CaseLog caseLog)
        {
            caseLog.refreshBody(database);
            caseLog.refreshReceiptients(database);

            Chilkat.MailMan mailClient = new Chilkat.MailMan();
            mailClient.UnlockComponent("SNaviProMAILQ_0Do3m4NOncVH");
            mailClient.MailHost = configuration.mailServer;
            mailClient.SmtpPort = 25;
            mailClient.SmtpUsername = configuration.userName;
            mailClient.SmtpPassword = configuration.password;
            mailClient.SmtpSsl = false;
           
            foreach (string emailAddress in caseLog.receivers.Keys)
            {
                try
                {
                    logger.write("Sending mail: " + caseLog.subject + " (" + emailAddress + ")", 0);
                    Chilkat.Email email = new Chilkat.Email();
                    email.FromAddress = configuration.emailAddress;
                    email.FromName = "NaviPro Helpdesk";
                    email.Subject = caseLog.subject;
                    email.AddTo(emailAddress, caseLog.receivers[emailAddress]);
                    email.SetHtmlBody(caseLog.body);
                    bool success = mailClient.SendEmail(email);
                    if (!success) logger.write("Sending failed: "+mailClient.LastErrorText, 2);
                }
                catch (Exception e)
                {
                    this.logger.write("Exception: " + e.Message, 2);
                }
            }
        }
    }
}
