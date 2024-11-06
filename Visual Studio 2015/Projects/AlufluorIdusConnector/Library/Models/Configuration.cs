using NaviPro.Alufluor.Idus.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class Configuration
    {
        public string bcOdataUrl { get; set; }
        public string bcSoapUrl { get; set; }
        public string bcUserName { get; set; }
        public string bcPassword { get; set; }
        public string idusUrl { get; set; }

        public string idusApiUserName { get; set; }
        public string idusUserName { get; set; }
        public string idusPassword { get; set; }
        public int intervalMinutes { get; set; }

        public Configuration()
        {

        }

        public void load()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:\\program files (x86)\\IdusBCConnection\\config.xml");

            XmlElement docElement = xmlDoc.DocumentElement;

            bcOdataUrl = XmlHelper.getNodeValue(docElement, "bcOdataUrl");
            bcSoapUrl = XmlHelper.getNodeValue(docElement, "bcSoapUrl");
            bcUserName = XmlHelper.getNodeValue(docElement, "bcUserName");
            bcPassword = XmlHelper.getNodeValue(docElement, "bcPassword");
            idusUrl = XmlHelper.getNodeValue(docElement, "idusUrl");
            idusApiUserName = XmlHelper.getNodeValue(docElement, "idusApiUserName");
            idusUserName = XmlHelper.getNodeValue(docElement, "idusUserName");
            idusPassword = XmlHelper.getNodeValue(docElement, "idusPassword");
            intervalMinutes = int.Parse(XmlHelper.getNodeValue(docElement, "intervalMinutes"));

        }

        public void check()
        {
            if ((bcOdataUrl == null) || (bcOdataUrl == "")) throw new Exception("bcOdataUrl not set.");
            if ((bcSoapUrl == null) || (bcSoapUrl == "")) throw new Exception("bcSoapUrl not set.");
            if ((bcUserName == null) || (bcUserName == "")) throw new Exception("bcUserName not set.");
            if ((bcPassword == null) || (bcPassword == "")) throw new Exception("bcPassword not set.");
            if ((idusUrl == null) || (idusUrl == "")) throw new Exception("idusUrl not set.");
            if ((idusApiUserName == null) || (idusApiUserName == "")) throw new Exception("idusApiUserName not set.");
            if ((idusUserName == null) || (idusUserName == "")) throw new Exception("idusUserName not set.");
            if ((idusPassword == null) || (idusPassword == "")) throw new Exception("idusPassword not set.");
            if (intervalMinutes == 0) throw new Exception("intervalMinutes not set.");
        }
    }
}
