using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for RegisterAuthentication.
    /// </summary>
    public class Acknowledgement : BroadSoftMessage
    {
        private string userId;
        private string userType;
        private string messageToAck;
        private string applicationId;

        public Acknowledgement(string userId, string userType, string messageToAck, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.userId = userId;
            this.userType = userType;
            this.messageToAck = messageToAck;
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
            helper.addAttribute(userElement, "id", userId);

            XmlElement messageElement = helper.addElement(userElement, "message", "");
            helper.addAttribute(messageElement, "messageName", messageToAck);

            helper.addElement(userElement, "applicationId", applicationId);


            xmlDoc = helper.createHeader(xmlDoc, "acknowledgement", userElement);

            return xmlDoc;
        }

        #endregion
    }
}
