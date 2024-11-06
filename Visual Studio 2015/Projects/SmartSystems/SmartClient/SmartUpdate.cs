using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartClient.Update;
using Navipro.SmartSystems.SmartLibrary;

namespace Navipro.SmartSystems.SmartClient
{
    public class SmartUpdate
    {
        private Configuration configuration;
        private Logger logger;

        public SmartUpdate(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public bool performUpdate(Modules modules)
        {

            logger.write("", 0, "Letar efter uppdateringar...");

            Update update = new Update();

            string serialNo = configuration.getConfigValue("serialNo");
            string password = configuration.getConfigValue("password");
            string url = configuration.getConfigValue("serverUrl");

            update.Url = url + "/Agent/Update.asmx";

            try
            {
                string moduleXmlDoc = update.checkForUpdates(serialNo, password);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(moduleXmlDoc);

                XmlElement docElement = xmlDoc.DocumentElement;
                XmlNodeList nodeList = docElement.SelectNodes("module");
                int i = 0;
                while (i < nodeList.Count)
                {
                    XmlElement moduleElement = (XmlElement)nodeList.Item(i);
                    Module updatedModule = new Module(moduleElement);

                    logger.write("", 0, "Uppdaterar " + updatedModule.name + "...");

                    if (modules.get(updatedModule.type, updatedModule.entryNo) == null)
                    {
                        modules.add(updatedModule);
                    }

                    string updateUrl = moduleElement.GetAttribute("url");

                    updatedModule.update(url + updateUrl);
                    modules.saveModuleConfig();

                    update.ackUpdate(serialNo, password, updatedModule.entryNo);

                    logger.write("", 1, "Klar.");

                    i++;
                }

                return true;

            }
            catch (Exception e)
            {
                logger.write("", 0, e.Message);
            }

            logger.write("", 0, "Uppdateringen misslyckades.");

            return false;


        }
    }
}
