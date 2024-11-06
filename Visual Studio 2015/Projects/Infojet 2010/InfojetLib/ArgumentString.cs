using System;
using System.Xml;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for NewUser.
    /// </summary>
    public class ArgumentString : ServiceArgument
    {
        private string _name;
        private string _value;

        public ArgumentString(string name, string value)
        {
            //
            // TODO: Add constructor logic here
            //
            _name = name;
            _value = value;
        }

        public XmlElement toDOM(XmlDocument xmlDoc)
        {
            XmlElement stringElement = xmlDoc.CreateElement("string");

            XmlElement element = xmlDoc.CreateElement(_name);
            element.AppendChild(xmlDoc.CreateTextNode(_value));
            stringElement.AppendChild(element);


            return stringElement;
        }
    }
}
