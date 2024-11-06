using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;

namespace Navipro.SmartSystems.SmartLibrary
{
    public class Modules
    {
        private Hashtable coreTable;
        private Hashtable moduleTable;
        private Hashtable driverTable;

        public Modules()
        {
            this.coreTable = new Hashtable();
            this.moduleTable = new Hashtable();
            this.driverTable = new Hashtable();

            loadModuleConfig();
            
        }

        private void loadModuleConfig()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<modules/>");

            if (System.IO.File.Exists(GetAppPath()+"\\modules.xml"))
            {
                xmlDoc.Load(GetAppPath() + "\\modules.xml");
            }

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlNodeList nodeList = docElement.SelectNodes("module");
            int i = 0;
            while (i < nodeList.Count)
            {
                XmlElement moduleElement = (XmlElement)nodeList.Item(i);

                Module module = new Module(moduleElement);
                if (module.type == 0) this.coreTable.Add(module.entryNo, module);
                if (module.type == 1) this.moduleTable.Add(module.entryNo, module);
                if (module.type == 2) this.driverTable.Add(module.entryNo, module);


                i++;
            }

        }

        public void saveModuleConfig()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<modules/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            IDictionaryEnumerator coreTableEnum = coreTable.GetEnumerator();
            while (coreTableEnum.MoveNext())
            {
                XmlElement moduleElement = ((Module)coreTableEnum.Value).toDom(xmlDoc);
                docElement.AppendChild(moduleElement);
            }

            IDictionaryEnumerator moduleTableEnum = moduleTable.GetEnumerator();
            while (moduleTableEnum.MoveNext())
            {
                XmlElement moduleElement = ((Module)moduleTableEnum.Value).toDom(xmlDoc);
                docElement.AppendChild(moduleElement);
            }

            IDictionaryEnumerator driverTableEnum = driverTable.GetEnumerator();
            while (driverTableEnum.MoveNext())
            {
                XmlElement moduleElement = ((Module)driverTableEnum.Value).toDom(xmlDoc);
                docElement.AppendChild(moduleElement);
            }

            xmlDoc.Save(GetAppPath() + "\\modules.xml");
        }

        public Module get(int type, int entryNo)
        {
            if (type == 0) return (Module)coreTable[entryNo];
            if (type == 1) return (Module)moduleTable[entryNo];
            if (type == 2) return (Module)driverTable[entryNo];
            return null;
        }

        public void add(Module module)
        {
            if (module.type == 0) coreTable.Add(module.entryNo, module);
            if (module.type == 1) moduleTable.Add(module.entryNo, module);
            if (module.type == 2) driverTable.Add(module.entryNo, module);

        }

        public Hashtable getModuleTable(int type)
        {
            if (type == 0) return coreTable;
            if (type == 1) return moduleTable;
            if (type == 2) return driverTable;
            return null;
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
