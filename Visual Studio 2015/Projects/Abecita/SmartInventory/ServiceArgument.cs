using System;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for ServiceArgument.
	/// </summary>
	public interface ServiceArgument
	{
		XmlElement toDOM(XmlDocument xmlDocumentContext);
		void postDOM();
	}
}
