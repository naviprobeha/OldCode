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
    public class Profile
    {
        public Profile()
        {
            allowedDataViews = new Dictionary<string, DataView>();
            indicators = new List<ProfileIndicator>();
            menuItems = new List<ProfileMenuItem>();
            responseGroups = new List<ResponseGroup>();
            defaultDataViews = new Dictionary<string, DataView>();
        }


        public String name { get; set; }
        public Dictionary<string, DataView> allowedDataViews { get; set; }
        public List<ProfileIndicator> indicators { get; set; }
        public List<ProfileMenuItem> menuItems { get; set; }
        public List<ResponseGroup> responseGroups { get; set; }
        public String todoCaption { get; set; }
        public DataView todoDataView { get; set; }
        public string tableCaption { get; set; } 
        public DataView tableDataView { get; set; }

        public Dictionary<string, DataView> defaultDataViews;

        
        public ProfileChart chart { get; set; }
        public static Profile load(string code, Dictionary<string, DataView> dataViewTable, User user)
        {
            Profile profile = new Profile();

            string appDataPath = System.Web.HttpContext.Current.Server.MapPath("~/app_data");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(appDataPath + "\\Profile_" + code + ".xml");
            XmlElement docElement = xmlDoc.DocumentElement;

            profile.name = docElement.GetAttribute("name");

            XmlNodeList nodeList = docElement.SelectNodes("dataViews/allow");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement dataViewElement = (XmlElement)xmlNode;
                DataView dataView = dataViewTable[dataViewElement.GetAttribute("code")];

                dataView.setParameters(user);

                profile.allowedDataViews.Add(dataView.code, dataView);
            }

            nodeList = docElement.SelectNodes("defaultDataViews/dataView");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement dataViewElement = (XmlElement)xmlNode;
                string type = dataViewElement.GetAttribute("type");
                DataView dataView = dataViewTable[dataViewElement.GetAttribute("code")];

                dataView.setParameters(user);

                profile.defaultDataViews.Add(type, dataView);
            }

            XmlElement todoElement = (XmlElement)docElement.SelectSingleNode("todo");
            if (todoElement != null)
            {
                profile.todoCaption = todoElement.GetAttribute("caption");
                profile.todoDataView = profile.allowedDataViews[todoElement.GetAttribute("dataView")];                
            }


            nodeList = docElement.SelectNodes("dashboard/indicators/indicator");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement indicatorElement = (XmlElement)xmlNode;

                ProfileIndicator profileIndicator = new ProfileIndicator();
                profileIndicator.label = indicatorElement.GetAttribute("label");
                profileIndicator.caption = indicatorElement.GetAttribute("caption");

                profileIndicator.dataView = profile.allowedDataViews[indicatorElement.GetAttribute("dataView")];

                profileIndicator.icon = indicatorElement.GetAttribute("icon");

                profileIndicator.lowerLevel = int.Parse(indicatorElement.GetAttribute("lowerLevel"));
                profileIndicator.upperLevel = int.Parse(indicatorElement.GetAttribute("upperLevel"));

                profile.indicators.Add(profileIndicator);

            }

            XmlElement chartElement = (XmlElement)docElement.SelectSingleNode("dashboard/chart");
            if (chartElement != null)
            {
                profile.chart = ProfileChart.createMonthChart(DateTime.Today.AddMonths(-12), DateTime.Today);

                profile.chart.caption1 = chartElement.GetAttribute("label1");
                profile.chart.caption2 = chartElement.GetAttribute("label2");

                profile.chart.dataView1 = profile.allowedDataViews[chartElement.GetAttribute("dataView1")];
                profile.chart.dataView2 = profile.allowedDataViews[chartElement.GetAttribute("dataView2")];
            }

            XmlElement tableElement = (XmlElement)docElement.SelectSingleNode("dashboard/table");
            if (tableElement != null)
            {
                profile.tableCaption = tableElement.GetAttribute("label");
                profile.tableDataView = profile.allowedDataViews[tableElement.GetAttribute("dataView")];

            }

            nodeList = docElement.SelectNodes("menu/item");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement itemElement = (XmlElement)xmlNode;

                ProfileMenuItem menuItem = new ProfileMenuItem(itemElement.GetAttribute("caption"), itemElement.GetAttribute("controller"), itemElement.GetAttribute("action"), itemElement.GetAttribute("parameters"), itemElement.GetAttribute("icon"));

                XmlNodeList nodeList2 = itemElement.SelectNodes("submenu/item");
                foreach (XmlNode xmlNode2 in nodeList2)
                {
                    XmlElement itemElement2 = (XmlElement)xmlNode2;

                    ProfileMenuItem subMenuItem = new ProfileMenuItem(itemElement2.GetAttribute("caption"), itemElement2.GetAttribute("controller"), itemElement2.GetAttribute("action"), itemElement2.GetAttribute("parameters"), itemElement2.GetAttribute("icon"));

                    menuItem.subMenuItems.Add(subMenuItem);

                }

                profile.menuItems.Add(menuItem);

            }

            nodeList = docElement.SelectNodes("responseGroups/responseGroup");
            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement itemElement = (XmlElement)xmlNode;

                ResponseGroup responseGroup = new ResponseGroup(itemElement.GetAttribute("uri"), itemElement.GetAttribute("caption"));
                if ((itemElement.GetAttribute("primary") != null) && (itemElement.GetAttribute("primary") == "true")) responseGroup.primary = true;

                profile.responseGroups.Add(responseGroup);

            }

            return profile;
        }

        public void updateIndicators(Database database)
        {
            foreach (ProfileIndicator indicator in indicators)
            {
                indicator.update(database);
            }

        }

        public void updateChart(Database database)
        {
            if (chart != null)
            {
                chart.updateChart(database);

            }
        }

        public List<Case> getTable(Database database)
        {
            return Case.getList(database, "", "", "", tableDataView);

        }
    }



}