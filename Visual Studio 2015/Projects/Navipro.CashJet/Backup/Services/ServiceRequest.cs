using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Xml;

namespace Navipro.CashJet.Services
{
    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-1319ADB3EF1F";
        public const string intfguid = "D030D214-C984-496a-87E7-41732C114F1F";
        public const string eventguid = "D030D214-C984-496a-87E7-41732C114F5F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IServiceRequest
    {
        [DispId(1)]
        void init(string url);
        [DispId(2)]
        string performService(string requestFileName);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.CashJet.Services"), ClassInterface(ClassInterfaceType.None)]
    public class ServiceRequest : IServiceRequest
    {
        string webServiceUrl = "";

        #region IServiceRequest Members

        public void init(string url)
        {
            webServiceUrl = url;
        }

        public string performService(string inputFileName)
        {
            CashjetWebService.ServiceRequest serviceRequest = new CashjetWebService.ServiceRequest();
            serviceRequest.Url = this.webServiceUrl;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inputFileName);

            string returnXmlDoc = serviceRequest.performService(xmlDoc.OuterXml);       

            XmlDocument xmlDocOut = new XmlDocument();
            xmlDocOut.LoadXml(returnXmlDoc);

            string outputFileName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\response_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            xmlDocOut.Save(outputFileName);

            return outputFileName;

        }

        #endregion




    }
}
