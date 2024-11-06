using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Xml;

namespace Navipro.CashJet.ChartControls
{
    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-93F1-1319ADB3EF1F";
        public const string intfguid = "D030D214-C984-496a-87F7-41732C114F1F";
        public const string eventguid = "D030D214-C984-496a-87F7-41732C114F5F";


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
    public interface IChartFactory
    {
        [DispId(1)]
        void renderBarChart(string inputXmlFile, string outputChartFile);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.CashJet.ChartControls"), ClassInterface(ClassInterfaceType.None)]
    public class ChartFactory : IChartFactory
    {

        public void renderBarChart(string inputXmlFile, string outputChartFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inputXmlFile);

            BarGraph barGraph = new BarGraph(xmlDoc, outputChartFile);
            barGraph.render();
        }
    }
}
