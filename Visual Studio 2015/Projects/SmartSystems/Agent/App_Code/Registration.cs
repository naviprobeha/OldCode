using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using Navipro.Base.Common;
using Navipro.SmartSystems.SystemCore;

[WebService(Namespace = "http://www.navipro.se/smartsystems/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Registration : System.Web.Services.WebService
{

    private Navipro.Base.Common.Configuration configuration;

    public Registration () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

        configuration = new Navipro.Base.Common.Configuration();

        configuration.setConfigValue("serverName", ConfigurationSettings.AppSettings["serverName"]);
        configuration.setConfigValue("database", ConfigurationSettings.AppSettings["dataSource"]);
        configuration.setConfigValue("userName", ConfigurationSettings.AppSettings["userName"]);
        configuration.setConfigValue("password", ConfigurationSettings.AppSettings["password"]);


    }

    [WebMethod]
    public int register(string serialNo, ref string password) 
    {

        int result = 404;

        Database database = new Database(null, configuration);

        Agents agents = new Agents();
        Agent agent = agents.getAgent(database, serialNo);
        if (agent != null)
        {
            if (agent.password == "")
            {
                agent.generatePassword(database);
                password = agent.password;
                result = 201;
            }
            else
            {
                if (agent.password == password) result = 200;               
            }
        }

        database.close();

        return result;
        
    }
    
}
