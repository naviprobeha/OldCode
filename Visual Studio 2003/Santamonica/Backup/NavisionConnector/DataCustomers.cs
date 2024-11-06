using System;
using System.Xml;
using Navipro.SantaMonica.Common;
using NavisionConnector.se.navipro.dev1;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for DataCustomers.
	/// </summary>
	public class DataCustomers
	{
		private Configuration configuration;

		public DataCustomers(XmlElement tableElement, Organization organization, Configuration configuration)
		{
			this.configuration = configuration;
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, organization);
		}

		public void fromDOM(XmlElement tableElement, Organization organization)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				Quiksilver quikSilver = new Quiksilver();
				quikSilver.Url = configuration.webServiceUrl;
				quikSilver.updateCustomer(record.OuterXml, organization.no);

				//DataCustomer dataCustomer = new DataCustomer(record, organization, true);

				i++;
			}
		}
	}
}
