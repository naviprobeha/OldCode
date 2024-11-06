using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace Navipro.SmartSystems.SmartLibrary
{
    public class Module
    {
        public int entryNo;
        public int type;
        public string name;
        public string className;
        public string versionNo;

        public Module(XmlElement xmlElement)
        {
            fromDom(xmlElement);
        }

        private void fromDom(XmlElement xmlElement)
        {
            entryNo = int.Parse(xmlElement.GetAttribute("entryNo"));
            type = int.Parse(xmlElement.GetAttribute("type"));
            name = xmlElement.GetAttribute("name");
            className = xmlElement.GetAttribute("className");
            versionNo = xmlElement.GetAttribute("versionNo");

        }

        public XmlElement toDom(XmlDocument xmlDoc)
        {
            XmlElement xmlElement = xmlDoc.CreateElement("module");
            xmlElement.SetAttribute("entryNo", entryNo.ToString());
            xmlElement.SetAttribute("type", type.ToString());
            xmlElement.SetAttribute("name", name);
            xmlElement.SetAttribute("className", className);
            xmlElement.SetAttribute("versionNo", versionNo);

            return xmlElement;
        }

        public void update(string url)
        {

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";

            WebResponse response = req.GetResponse();

            Stream stream = response.GetResponseStream();

            int cbRead = 0;

            FileStream wrt = new FileStream(GetAppPath() + "\\module" + entryNo.ToString() + ".dll", FileMode.Create);

            Byte[] data = new Byte[1024];

            long totalSize = response.ContentLength;
            int received = 0;

            cbRead = stream.Read(data, 0, 1024);
            while (cbRead > 0)
            {
                wrt.Write(data, 0, cbRead);

                received = received + cbRead;

                cbRead = stream.Read(data, 0, 1024);
            }

            wrt.Close();


        }


        public void loadModule(Configuration configuration)
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(GetAppPath() + "\\module" + entryNo.ToString() + ".dll");
            //IModule module = (IModule)assembly.CreateInstance(className);
            //module.init(configuration);

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
