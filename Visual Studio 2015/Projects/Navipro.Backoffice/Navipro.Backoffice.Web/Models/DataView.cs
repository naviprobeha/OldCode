using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Xml;

namespace Navipro.Backoffice.Web.Models
{
    public class DataView
    {
        public DataView()
        {
            parameterTable = new Dictionary<string, string>();

        }


        public String code { get; set; }

        public String name { get; set; }

        public String type { get; set; }
        public String query { get; set; }

        public String select { get; set; }

        public String groupBy { get; set; }

        public String orderBy { get; set; }

        public int noOfRecords { get; set; }

        public Dictionary<string, string> parameterTable;
        public static Dictionary<string, DataView> load()
        {
            Dictionary<string, DataView> dataViewTable = new Dictionary<string, DataView>();

            string appDataPath = System.Web.HttpContext.Current.Server.MapPath("~/app_data");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(appDataPath + "\\dataviews.xml");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlNodeList nodeList = docElement.SelectNodes("dataView");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement dataViewElement = (XmlElement)xmlNode;

                DataView dataView = new DataView();
                dataView.code = dataViewElement.GetAttribute("code");
                dataView.name = dataViewElement.GetAttribute("name");
                dataView.type = dataViewElement.GetAttribute("type");
                dataView.query = dataViewElement.GetAttribute("query");
                dataView.select = dataViewElement.GetAttribute("select");
                dataView.groupBy = dataViewElement.GetAttribute("groupBy");
                dataView.orderBy = dataViewElement.GetAttribute("orderBy");
                if ((dataViewElement.GetAttribute("recordCount") != null) && (dataViewElement.GetAttribute("recordCount") != ""))
                {
                    dataView.noOfRecords = int.Parse(dataViewElement.GetAttribute("recordCount"));
                }
                dataViewTable.Add(dataView.code, dataView);

            }

            return dataViewTable;
        }

        public void setParameters(User user)
        {
            query = query.Replace("@myResourceNo", "'"+user.resourceNo+"'");
            query = query.Replace("@customerNo", "'"+user.customerNo+"'");

        }
    }



}