using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Navipro.SmartSystems.SmartLibrary;

namespace Navipro.SmartSystems.SmartClient
{
    class XmlConfiguration
    {
        public XmlConfiguration()
        {

        }

        public bool loadConfiguration(Configuration configuration)
        {

            try
            {
                string serverUrl = "";
                string serialNo = "";
                string password = "";

                XmlDocument configDoc = new XmlDocument();
                configDoc.Load(GetAppPath() + "\\config.xml");

                XmlElement docElement = configDoc.DocumentElement;

                if (docElement != null)
                {
                    XmlElement serverUrlElement = (XmlElement)docElement.SelectSingleNode("serverUrl");
                    if (serverUrlElement != null)
                    {
                        if (serverUrlElement.FirstChild != null) serverUrl = serverUrlElement.FirstChild.Value;
                    }

                    XmlElement serialNoElement = (XmlElement)docElement.SelectSingleNode("serialNo");
                    if (serialNoElement != null)
                    {
                        if (serialNoElement.FirstChild != null) serialNo = serialNoElement.FirstChild.Value;
                    }

                    XmlElement passwordElement = (XmlElement)docElement.SelectSingleNode("password");
                    if (passwordElement != null)
                    {
                        if (passwordElement.FirstChild != null) password = passwordElement.FirstChild.Value;
                    }

                }

                configuration.setConfigValue("serverUrl", serverUrl);
                configuration.setConfigValue("serialNo", serialNo);
                configuration.setConfigValue("password", password);

                return true;
            }
            catch (Exception e)
            {

            }

            return false;
        }


        public void saveConfiguration(Configuration configuration)
        {
            XmlDocument configDoc = new XmlDocument();

            configDoc.LoadXml("<configuration/>");
            XmlElement docElement = configDoc.DocumentElement;
            
            XmlElement serverUrlElement = configDoc.CreateElement("serverUrl");
            XmlText serverUrlText = configDoc.CreateTextNode(configuration.getConfigValue("serverUrl"));

            XmlElement serialNoElement = configDoc.CreateElement("serialNo");
            XmlText serialNoText = configDoc.CreateTextNode(configuration.getConfigValue("serialNo"));

            XmlElement passwordElement = configDoc.CreateElement("password");
            XmlText passwordText = configDoc.CreateTextNode(configuration.getConfigValue("password"));

            serverUrlElement.AppendChild(serverUrlText);
            serialNoElement.AppendChild(serialNoText);
            passwordElement.AppendChild(passwordText);

            docElement.AppendChild(serverUrlElement);
            docElement.AppendChild(serialNoElement);
            docElement.AppendChild(passwordElement);

            configDoc.Save(GetAppPath() + "\\config.xml");

        }

        private string GetAppPath()
        {
            System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
            string aPath = System.IO.Path.GetDirectoryName(modules[0].FullyQualifiedName);
            if ((aPath != "") && (aPath[aPath.Length - 1] != '\\'))
                aPath += '\\';

            return aPath;
        }
    }
}
