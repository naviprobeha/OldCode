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

        public XmlDocument transport(string method, string secret, string xmlDocument)
        {
            // TODO:  Add TransporterMSMQ.transport implementation
            Guid guid = Guid.NewGuid();

            //XmlElement guidElement = xmlDocument.CreateElement("guid");
            //XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
            //guidElement.AppendChild(guidText);
            //xmlDocument.DocumentElement.AppendChild(guidElement);

            Navipro.Infojet.Lib.Configuration configuration = new Navipro.Infojet.Lib.Configuration();
            configuration.init();
            Navipro.Infojet.Lib.Database database = new Navipro.Infojet.Lib.Database(null, configuration);


            Navipro.Infojet.Lib.DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("Service Request Queue") + "] ([Guid], [Method Name], [Inbound Document], [Outbound Document], [Created DateTime], [Processed DateTime], [Processed]) VALUES ('" + guid.ToString() + "', '" + method + "', '" + xmlDocument + "', '', GETDATE(), '1754-01-01', 0)");
            databaseQuery.execute();


            net.workanywhere.servicerequest.ServiceRequest serviceRequest = new Navipro.Infojet.WebService.net.workanywhere.servicerequest.ServiceRequest();
            serviceRequest.Url = this.configuration.wsAddress;
            serviceRequest.UseDefaultCredentials = true;


            serviceRequest.ProcessRequest(guid.ToString(), secret);


            databaseQuery = database.prepare("SELECT [Outbound Document] FROM [" + database.getTableName("Service Request Queue") + "] WHERE [Guid] = '" + guid.ToString() + "'");

            byte[] byteArray = (byte[])databaseQuery.executeScalar();
            //string xmlDoc = System.Text.Encoding.Default.GetString(byteArray);
            string xmlDoc = System.Text.Encoding.UTF8.GetString(byteArray);

            if (xmlDoc.IndexOf("<") > 0)
            {
                xmlDoc = xmlDoc.Substring(xmlDoc.IndexOf("<"));
            }
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:\\temp\\output.xml");
            streamWriter.WriteLine(xmlDoc);
            streamWriter.Flush();
            streamWriter.Close();

            XmlDocument outDocument = new XmlDocument();
            outDocument.LoadXml(xmlDoc);
            database.close();
            return outDocument;

        }

	}
}
