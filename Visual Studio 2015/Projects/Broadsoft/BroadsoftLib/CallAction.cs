using System;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for RegisterAuthentication.
    /// </summary>
    public class CallAction : BroadSoftMessage
    {
        private string userId;
        private string userType;
        private string actionType;
        private string applicationId;
        private ArrayList actionParams;


        public CallAction(string userId, string userType, string actionType, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.userId = userId;
            this.userType = userType;
            this.actionType = actionType;
            this.applicationId = applicationId;

            this.actionParams = new ArrayList();
        }

        public void addActionParameter(string type, string parameter)
        {
            CallActionParameter callActionParameter = new CallActionParameter(type, parameter);
            actionParams.Add(callActionParameter);

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

            XmlElement actionElement = helper.addElement(userElement, "action", "");
            helper.addAttribute(actionElement, "actionType", actionType);

            int i = 0;
            while (i < this.actionParams.Count)
            {
                XmlElement actionParamElement = helper.addElement(actionElement, "actionParam", ((CallActionParameter)actionParams[i]).parameter);
                helper.addAttribute(actionParamElement, "actionParamName", ((CallActionParameter)actionParams[i]).type);
                i++;
            }

            helper.addElement(userElement, "applicationId", applicationId);


            xmlDoc = helper.createHeader(xmlDoc, "callAction", userElement);

            return xmlDoc;
        }


        #endregion
    }
}

