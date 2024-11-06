﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace DemoService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "urn:microsoft-dynamics-schemas/codeunit/POSClientWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class POSClientWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string PerformService(string method, string xmlRecordData)
        {
            if (method == "getDashboardDetails") return GetDashboardDetails.xml;



            return "";

        }
    }
}
