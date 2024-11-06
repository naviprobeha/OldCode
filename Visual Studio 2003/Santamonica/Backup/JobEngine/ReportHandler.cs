using System;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine

{
	/// <summary>
	/// Summary description for LineOrderManagement.
	/// </summary>
	public class ReportHandler
	{
		private Configuration configuration;
		private Logger logger;

		private Thread thread;
		private bool running;

		private string smtpServer;
		private string smtpSender;

		public ReportHandler(Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;
			running = true;

			thread = new Thread(new ThreadStart(run));
			thread.Start();

			smtpServer = configuration.smtpServer;
			smtpSender = configuration.smtpSender;
		}

		public void run()
		{
			log("Handler started.", 0);

			while(running)
			{
				try
				{
					this.processReportJobs();
				}
				catch(Exception e)
				{
					log("Exception: "+e.Message, 2);
				}

				Thread.Sleep(60000);
			}
			log("Handler stopped.", 0);
		}

		public void stop()
		{
			this.running = false;
		}

		private void processReportJobs()
		{
			Database database = new Database(logger, configuration);

			ReportJobs reportJobs = new ReportJobs();
			DataSet reportJobDataSet = reportJobs.getDataSet(database);

			int i = 0;
			while (i < reportJobDataSet.Tables[0].Rows.Count)
			{
				ReportJob reportJob = new ReportJob(reportJobDataSet.Tables[0].Rows[i]);
				if ((reportJob.lastSentDateTime.Year == 1753) && (reportJob.initialSendDateTime < DateTime.Now)) sendReport(reportJob, database);
				if ((reportJob.lastSentDateTime.Year > 1753) && (reportJob.lastSentDateTime.AddMilliseconds(reportJob.sendInterval) < DateTime.Now)) sendReport(reportJob, database);

				i++;
			}

			database.close();
		}

		private void sendReport(ReportJob reportJob, Database database)
		{
			log("Processing report job: "+reportJob.code, 0);

			System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(reportJob.assembly);
			if (assembly != null)
			{
				Report report = (Report)assembly.CreateInstance(reportJob.reportName);
			
				report.setDatabase(database);

				string body = "<style type=\"css/text\"> td { font-family: verdana; font-size: 12px } </style>" + report.renderReport();

				System.Web.Mail.MailMessage mailMessage = new System.Web.Mail.MailMessage();
				mailMessage.From = smtpSender;
				mailMessage.To = reportJob.emailAddress;
				mailMessage.Subject = "Rapport: "+report.getName();
				mailMessage.Body = body;
				mailMessage.BodyFormat = System.Web.Mail.MailFormat.Html;

				System.Web.Mail.SmtpMail.SmtpServer = smtpServer;
				System.Web.Mail.SmtpMail.Send(mailMessage);
			
				log ("Sending report: "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
				reportJob.lastSentDateTime = DateTime.Now;
				reportJob.save(database);

				log("Report sent.", 0);
			}
			else
			{
				log("Report assembly "+reportJob.assembly+" not found.", 0);
			}
		}

		private void log(string message, int type)
		{
			logger.write("[ReportHandler] "+message, type);
		}


	}
}
