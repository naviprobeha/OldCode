using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ServerStatusRequest.
    /// </summary>
    public class ServerStatusRequest : BroadSoftMessage
    {
        private string userUid;
        private string userType;
        private string applicationId;

        public ServerStatusRequest(string userUid, string userType, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.userUid = userUid;
            this.userType = userType;
            this.applicationId = applicationId;
        }
        #region BroadWorksMessage Members

        public XmlDocument toDOM()
        {
            // TODO:  Add ServerStatusRequest.toDOM implementation

            BroadSoftMessageHelper helper = new BroadSoftMessageHelper();

            XmlDocument xmlDoc = new XmlDocument();



            XmlElement userElement = xmlDoc.CreateElement("user");
            helper.addAttribute(userElement, "userType", userType);
            helper.addAttribute(userElement, "userUid", userUid);
            helper.addElement(userElement, "applicationId", applicationId);

            xmlDoc = helper.createHeader(xmlDoc, "serverStatusRequest", userElement);

            return xmlDoc;

        }

        #endregion
    }
}
