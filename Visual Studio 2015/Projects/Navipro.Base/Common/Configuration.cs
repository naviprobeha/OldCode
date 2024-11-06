using System;
using System.Collections;
using System.Xml;
using Microsoft.Win32;

namespace Navipro.Base.Common
{
    public class Configuration
    {
        private Hashtable configurationValues;

		public Configuration()
		{
            this.configurationValues = new Hashtable();
		}

        public string getConfigValue(string key)
        {
            if (configurationValues[key] != null)
            {
                return (string)configurationValues[key];
            }
            return "";
        }

        public void setConfigValue(string key, string value)
        {
            configurationValues.Add(key, value);
        }

        public void readFromRegistry(string registryKey)
		{
			RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryKey);

            string[] valueNames = regKey.GetValueNames();
            int i = 0;
            while (i < valueNames.Length)
            {
				this.setConfigValue(valueNames[i].ToString(), regKey.GetValue(valueNames[i]).ToString());
			}
		}

        public void readFromFile(string xmlFileName)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            XmlElement docElement = xmlDoc.DocumentElement;
            if (docElement != null)
            {
                XmlNodeList nodeList = docElement.ChildNodes;
                int i = 0;
                while (i < nodeList.Count)
                {
                    XmlElement xmlElement = (XmlElement)nodeList.Item(i);
                    string name = xmlElement.Name;
                    string value = "";
                    if (xmlElement.FirstChild != null) value = xmlElement.FirstChild.Value;

                    this.setConfigValue(name, value);

                    i++;
                }

            }
        }
	}
}

