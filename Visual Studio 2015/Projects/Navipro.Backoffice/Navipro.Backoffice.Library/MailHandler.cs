using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Navipro.Backoffice.Library
{
    public class MailHandler
    {
        private Configuration configuration;
        private Thread thread;
        private bool running;
        private Logger logger;

        public MailHandler(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;

            thread = new Thread(new ThreadStart(run));
        }

        public void start()
        {
            logger.write("Starting mailchecker.", 1);

            running = true;
            thread.Start();
        }

        public void stop()
        {
            logger.write("Stopping mailchecker.", 1);

            running = false;
        }

        public void run()
        {
            
            logger.write("Mailchecker started.", 1);

            while(running)
            {
                processEmailCheck();
                sendEmails();

                Thread.Sleep(5000);
            }
            logger.write("Mailchecker stopped.", 1);

        }

        private void processEmailCheck()
        {
            logger.write("Checking mail...", 1);
            MailChecker mailChecker = new MailChecker(configuration, logger);
            try
            {
                mailChecker.checkEmail();
            }
            catch (Exception e)
            {
                logger.write("Exception: " + e.Message, 2);
            }
            mailChecker.close();


        }

        private void sendEmails()
        {
            MailSender mailSender = new MailSender(configuration, logger);
            mailSender.processMail();
        }
    }
}
