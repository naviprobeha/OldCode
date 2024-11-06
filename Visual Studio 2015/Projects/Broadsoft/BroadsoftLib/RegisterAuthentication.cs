using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for RegisterAuthentication.
    /// </summary>
    public class RegisterAuthentication : BroadSoftMessage
    {
        private string userId;
        private string userType;
        private string applicationId;

        public RegisterAuthentication(string userId, string userType, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.userId = userId;
            this.userType = userType;
            this.applicationId = applicationId;
        }
        #region BroadWorksMessage Members

        public System.Xml.XmlDocument toDOM()
        {
            // TODO:  Add RegisterAuthentication.toDOM implementation

            BroadSoftMessageHelper helper = new BroadSoftMessageHelper();

            XmlDocument xmlDoc = new XmlDocument();



            XmlElement userElement = xmlDoc.CreateElement("user");
            helper.addAttribute(userElement, "userType", userType);
            helper.addElement(userElement, "id", userId);
            helper.addElement(userElement, "applicationId", applicationId);

            xmlDoc = helper.createHeader(xmlDoc, "registerAuthentication", userElement);

            return xmlDoc;
        }

        #endregion
    }
}
