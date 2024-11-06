using System;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for RegisterAuthentication.
    /// </summary>
    public class MonitoringUsersRequest : BroadSoftMessage
    {
        private string userId;
        private string userType;
        private ArrayList monitoringUsers;
        private string applicationId;


        public MonitoringUsersRequest(string userId, string userType, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.userId = userId;
            this.userType = userType;
            this.monitoringUsers = new ArrayList();
            this.applicationId = applicationId;
        }

        public void add(string userName)
        {
            this.monitoringUsers.Add(userName);
        }

        #region BroadWorksMessage Members

        public System.Xml.XmlDocument toDOM()
        {
            // TODO:  Add RegisterAuthentication.toDOM implementation

            BroadSoftMessageHelper helper = new BroadSoftMessageHelper();

            XmlDocument xmlDoc = new XmlDocument();



            XmlElement userElement = xmlDoc.CreateElement("user");
            helper.addAttribute(userElement, "userType", userType);
            helper.addAttribute(userElement, "id", userId);

            XmlElement monitoringElement = helper.addElement(userElement, "monitoring", "");
            helper.addAttribute(monitoringElement, "monType", "Add");

            int i = 0;
            while (i < this.monitoringUsers.Count)
            {
                helper.addElement(userElement, "monUser", monitoringUsers[i].ToString());
                i++;
            }

            helper.addElement(userElement, "applicationId", applicationId);

            xmlDoc = helper.createHeader(xmlDoc, "monitoringUsersRequest", userElement);

            return xmlDoc;
        }

        #endregion
    }
}
