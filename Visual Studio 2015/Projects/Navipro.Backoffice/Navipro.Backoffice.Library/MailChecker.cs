using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Navipro.Backoffice.Library
{
    public class MailChecker
    {
        private Configuration configuration;
        private Logger logger;
        private Database database;

        public MailChecker(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;

            database = new Database(configuration);

        }

        public void checkEmail()
        {

            Chilkat.MailMan mailClient = new Chilkat.MailMan();
            mailClient.UnlockComponent("SNaviProMAILQ_0Do3m4NOncVH");
            mailClient.MailHost = configuration.mailServer;
            mailClient.MailPort = 110;
            mailClient.PopUsername = configuration.userName;
            mailClient.PopPassword = configuration.password;
            mailClient.PopSsl = true;


            try
            {
                int noOfMessages = mailClient.CheckMail();
                if (noOfMessages > 0) logger.write("Messages: " + noOfMessages, 1);
                if (noOfMessages > 0)
                {

                    Chilkat.EmailBundle bundle = mailClient.GetHeaders(10, 0, 5);


                    if (bundle.MessageCount > 0)
                    {
                        Chilkat.Email email = bundle.GetEmail(0);
                        email = mailClient.GetFullEmail(email);

                        processEmail(email);
                        
                        mailClient.DeleteEmail(email);
                    }
                    else
                    {
                        this.logger.write("No new messages.", 1);
                    }
                }
                mailClient.Pop3EndSession();


            }
            catch (Exception e)
            {
                this.logger.write("Exception: " + e.Message, 2);
            }

        }


        private void processEmail(Chilkat.Email email)
        {
            logger.write("", 1);
            logger.write("Processing email...", 1);
            logger.write("From: " + email.FromAddress, 1);
            logger.write("Message-ID: " + email.GetHeaderField("Message-ID"), 1);
            logger.write("References: " + email.GetHeaderField("References"), 1);
            logger.write("In-Reply-To: " + email.GetHeaderField("In-Reply-To"), 1);
            logger.write("Subject: " + email.Subject, 1);
            logger.write("Encoding: " + email.Charset, 1);

            string caseNo = "";
            if(!checkIfEmailIsExistingCase(email.GetHeaderField("Message-ID"), out caseNo))
            {
                if (checkReferences(email.GetHeaderField("References"), out caseNo))
                {
                    logger.write("Adding reply...", 2);
                        
                    CaseHandler.CreateCaseReply(configuration, email, logger, caseNo);
                }
                else
                {
                    logger.write("Creating case...", 2);
                    CaseHandler.CreateCase(configuration, email, logger);
                }

            }
            
 
        }

        private bool checkIfEmailIsExistingCase(string messageId, out string caseNo)
        {
            caseNo = "";

            DatabaseQuery query = database.prepare("SELECT [Case No_] FROM [" + database.getTableName("Case Log") + "] WHERE [Message-ID] = @messageId");
            query.addStringParameter("messageId", messageId, 100);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseNo = dataReader.GetValue(0).ToString();
                dataReader.Close();
                return true;
            }
            dataReader.Close();

            return false;
        }

        private bool checkReferences(string references, out string caseNo)
        {
            caseNo = "";
            if (references == null) return false;

            string[] referenceArray = references.Split(new string[] { " " }, StringSplitOptions.None);
            if (referenceArray == null) return false;

            foreach (string reference in referenceArray)
            {
                logger.write("Checking reference: " + reference, 2);

                if (checkIfEmailIsExistingCase(reference, out caseNo)) return true;
            }
            return false;
        }

        public void close()
        {
            database.close();
        }

    }
}
