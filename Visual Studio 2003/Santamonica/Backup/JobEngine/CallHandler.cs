using System;
using System.Data;
using System.Collections;
using Navipro.BroadWorks.Lib;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for CallClient.
	/// </summary>
	public class CallHandler
	{

		private Navipro.SantaMonica.Common.Logger logger;
		private Configuration configuration;
		private ArrayList callOperatorList;

		private string serverName;
		private int port;

		public CallHandler(Navipro.SantaMonica.Common.Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;

			loadConfig();

			callOperatorList = new ArrayList();

			Database database = new Database(logger, configuration);

			UserOperators userOperators = new UserOperators();
			DataSet operators = userOperators.getPhoneOperators(database);

			database.close();

			int i = 0;
			while (i < operators.Tables[0].Rows.Count)
			{
				UserOperator userOperator = new UserOperator(operators.Tables[0].Rows[i]);
				OperatorCallClient callOperator = new OperatorCallClient(logger, configuration, userOperator, serverName, port);
				callOperatorList.Add(callOperator);

				i++;
			}
		}


		public void stop()
		{
			int i = 0;
			while(i < callOperatorList.Count)
			{
				((OperatorCallClient)callOperatorList[i]).stop();
				i++;
			}

		}


		private void loadConfig()
		{
			System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
			xmlDoc.Load("config.xml");

			System.Xml.XmlElement docElement = xmlDoc.DocumentElement;
			if (docElement != null)
			{
				System.Xml.XmlElement serverNameElement = (System.Xml.XmlElement)docElement.SelectSingleNode("serverName");
				if (serverNameElement != null)
				{
					if (serverNameElement.FirstChild != null) this.serverName = serverNameElement.FirstChild.Value;
				}				

				System.Xml.XmlElement portElement = (System.Xml.XmlElement)docElement.SelectSingleNode("port");
				if (portElement != null)
				{
					if (portElement.FirstChild != null) this.port = int.Parse(portElement.FirstChild.Value);
				}				
			}

		}
	}
}
