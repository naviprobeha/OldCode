using System;
using System.Xml;
using System.Messaging;

namespace Navipro.Infojet.WebService
{
	/// <summary>
	/// Summary description for servicedelegator.
	/// </summary>
	public class ServiceDelegator
	{
		private Configuration configuration;

		public ServiceDelegator()
		{
			//
			// TODO: Add constructor logic here
			//
			this.configuration = new Configuration();
			configuration.init();
		}

        public XmlDocument transport(XmlDocument xmlDocument)
        {
            // TODO:  Add TransporterMSMQ.transport implementation
            Guid guid = Guid.NewGuid();

            XmlElement guidElement = xmlDocument.CreateElement("guid");
            XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
            guidElement.AppendChild(guidText);
            xmlDocument.DocumentElement.AppendChild(guidElement);

            Navipro.Infojet.Lib.Configuration configuration = new Navipro.Infojet.Lib.Configuration();
            configuration.init();
            Navipro.Infojet.Lib.Database database = new Navipro.Infojet.Lib.Database(null, configuration);


            Navipro.Infojet.Lib.DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("Service Request Queue") + "] ([Guid], [Inbound Document], [Outbound Document], [Created DateTime], [Processed DateTime], [Processed]) VALUES ('" + guid.ToString() + "', '" + xmlDocument.OuterXml + "', '', GETDATE(), '1754-01-01', 0)");
            databaseQuery.execute();


            net.workanywhere.sirius.ServiceRequestWebService_Binding serviceRequest = new Navipro.Infojet.WebService.net.workanywhere.sirius.ServiceRequestWebService_Binding();
            serviceRequest.Url = configuration.wsAddress;
            serviceRequest.UseDefaultCredentials = true;

            serviceRequest.ProcessRequest(guid.ToString());


            databaseQuery = database.prepare("SELECT [Outbound Document] FROM [" + database.getTableName("Service Request Queue") + "] WHERE [Guid] = '" + guid.ToString() + "'");

            byte[] byteArray = (byte[])databaseQuery.executeScalar();
            string xmlDoc = System.Text.Encoding.Default.GetString(byteArray);


            XmlDocument outDocument = new XmlDocument();
            outDocument.LoadXml(xmlDoc);
            database.close();
            return outDocument;

        }

	}
}
