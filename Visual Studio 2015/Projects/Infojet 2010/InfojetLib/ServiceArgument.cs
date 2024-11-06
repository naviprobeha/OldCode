using System;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ServiceArgument.
	/// </summary>
	public interface ServiceArgument
	{
		XmlElement toDOM(XmlDocument xmlDoc);
	}
}
