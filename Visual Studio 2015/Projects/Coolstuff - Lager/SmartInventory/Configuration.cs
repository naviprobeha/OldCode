﻿using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Navipro.SmartInventory
{
    public class Configuration
    {
        private string _webServiceUrl = "";
        private string _languageCode = "";
        private string _agentId = "";
        private string _purchaseOrderNoPrefix = "";
        private DataUserCollection _userCollection;


        public Configuration()
        {

            read();
        }

        private bool read()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("\\Program Files\\SmartInventory\\config.xml");

                XmlElement docElement = xmlDoc.DocumentElement;

                _webServiceUrl = docElement.GetElementsByTagName("webServiceUrl").Item(0).FirstChild.Value;
                _agentId = docElement.GetElementsByTagName("agentId").Item(0).FirstChild.Value;
                _languageCode = docElement.GetElementsByTagName("languageCode").Item(0).FirstChild.Value;
                _purchaseOrderNoPrefix = docElement.GetElementsByTagName("purchaseOrderNoPrefix").Item(0).FirstChild.Value;

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public string agentId { get { return _agentId; } }
        public string webServiceUrl { get { return _webServiceUrl; } }
        public string languageCode { get { return _languageCode; } }
        public string purchaseOrderNoPrefix { get { return _purchaseOrderNoPrefix; } }
        public DataUserCollection userCollection { get { return _userCollection; } set { _userCollection = value; } }


        public string getUserNoFromName(string name)
        {
            int i = 0;
            while (i < userCollection.Count)
            {
                if (userCollection[i].name == name) return userCollection[i].code;
                i++;
            }
            return "";
        }

    }
}
