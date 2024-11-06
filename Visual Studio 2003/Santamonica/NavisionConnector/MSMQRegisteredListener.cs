using System;
using System.Xml;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for MSMQRegisteredListener.
	/// </summary>
	public interface MSMQRegisteredListener
	{
		void msmqDocumentReceived(XmlDocument document);
	}
}
