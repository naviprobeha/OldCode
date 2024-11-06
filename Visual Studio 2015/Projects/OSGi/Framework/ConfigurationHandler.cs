using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Navipro.OSGi.Interfaces;

namespace Navipro.OSGi.Framework
{
    public class ConfigurationHandler
    {
        private FrameworkFactory _frameworkFactory;

        public ConfigurationHandler(FrameworkFactory frameworkFactory)
        {
            this._frameworkFactory = frameworkFactory;
        }

        public Configuration load()
        {
            Configuration configuration = new Configuration();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\config.xml");

                XmlElement docElement = xmlDoc.DocumentElement;

                XmlNodeList xmlNodeList = docElement.SelectNodes("modules/module");

                int i = 0;
                while (i < xmlNodeList.Count)
                {
                    XmlElement moduleElement = (XmlElement)xmlNodeList.Item(i);

                    string assembly = moduleElement.GetAttribute("assembly");
                    string className = moduleElement.GetAttribute("className");
                    string moduleName = moduleElement.GetAttribute("moduleName");
                    string action = moduleElement.GetAttribute("action");

                    _frameworkFactory.moduleHandler.loadModule(moduleName, assembly, className);
                    if (action.ToUpper() == "START") _frameworkFactory.moduleHandler.startModule(moduleName);

                    i++;
                }

                xmlNodeList = docElement.SelectNodes("properties/property");

                i = 0;
                while (i < xmlNodeList.Count)
                {
                    XmlElement moduleElement = (XmlElement)xmlNodeList.Item(i);

                    string key = moduleElement.GetAttribute("key");
                    string value = moduleElement.GetAttribute("value");

                    configuration.setValue(key, value);

                    i++;
                }

            return configuration;
        }
    }
}
