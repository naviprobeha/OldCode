using System;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using Navipro.Base.Common;
using System.Data;
using Navipro.SmartSystems.SystemCore;

/// <summary>
/// Summary description for Update
/// </summary>
[WebService(Namespace = "http://www.navipro.se/smartsystems/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Update : System.Web.Services.WebService
{
    private Navipro.Base.Common.Configuration configuration;


    public Update()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

        configuration = new Navipro.Base.Common.Configuration();

        configuration.setConfigValue("serverName", ConfigurationSettings.AppSettings["serverName"]);
        configuration.setConfigValue("database", ConfigurationSettings.AppSettings["dataSource"]);
        configuration.setConfigValue("userName", ConfigurationSettings.AppSettings["userName"]);
        configuration.setConfigValue("password", ConfigurationSettings.AppSettings["password"]);
    }

    [WebMethod]
    public string checkForUpdates(string serialNo, string password)
    {
        Database database = new Database(null, configuration);

        XmlDocument updateDoc = new XmlDocument();
        updateDoc.LoadXml("<modules/>");
        XmlElement modulesElement = updateDoc.DocumentElement;


        Agents agents = new Agents();
        if (agents.checkPassword(database, serialNo, password))
        {


            AgentModules agentModules = new AgentModules();
            DataSet agentModuleDataSet = agentModules.getDataSet(database, serialNo);

            int i = 0;
            while (i < agentModuleDataSet.Tables[0].Rows.Count)
            {
                AgentModule agentModule = new AgentModule(agentModuleDataSet.Tables[0].Rows[i]);
                Module module = agentModule.getModule(database);

                if (module.changed > agentModule.lastUpdated)
                {
                    string fileName = serialNo + "_" + module.entryNo.ToString() + ".dll";
                    string moduleUrl = "/agent/modules/" + fileName;

                    if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("modules")))
                    {
                        System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("modules"));
                    }

                
                    module.export(database, System.Web.HttpContext.Current.Server.MapPath("modules") + "\\" + fileName);

                    XmlElement moduleElement = updateDoc.CreateElement("module");
                    moduleElement.SetAttribute("entryNo", module.entryNo.ToString());
                    moduleElement.SetAttribute("type", module.type.ToString());
                    moduleElement.SetAttribute("name", module.name);
                    moduleElement.SetAttribute("className", module.className);
                    moduleElement.SetAttribute("versionNo", module.versionNo);
                    moduleElement.SetAttribute("url", moduleUrl);

                    modulesElement.AppendChild(moduleElement);
                }

                i++;

            }

            database.close();
        }

        return updateDoc.OuterXml;
    }


    [WebMethod]
    public void ackUpdate(string serialNo, string password, int moduleEntryNo)
    {
        Database database = new Database(null, configuration);


        Agents agents = new Agents();
        if (agents.checkPassword(database, serialNo, password))
        {
            AgentModules agentModules = new AgentModules();
            AgentModule agentModule = agentModules.getAgentModule(database, serialNo, moduleEntryNo);
            agentModule.lastUpdated = DateTime.Now.AddMinutes(-10);
            agentModule.save(database);

            string fileName = serialNo + "_" + moduleEntryNo.ToString() + ".dll";

            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("modules") + "\\" + fileName))
            {
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("modules") + "\\" + fileName);
            }

        }
    }
}

