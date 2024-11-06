using System;
using System.Xml;
using System.Data.SqlServerCe;


namespace SmartOrder
{
    /// <summary>
    /// Summary description for Agent.
    /// </summary>

    public class Agent
    {
        private SmartDatabase smartDatabase;

        private string agentIdValue;
        private string userNameValue;
        private string passwordValue;

        public Agent()
        {
            this.agentIdValue = "";
            this.userNameValue = "";
            this.passwordValue = "";
        }

        public Agent(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;

            this.agentIdValue = "";
            this.userNameValue = "";
            this.passwordValue = "";

            refresh();
        }

        public void refresh()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

            if (dataReader.Read())
            {
                try
                {
                    agentIdValue = (string)dataReader.GetValue(3);
                    userNameValue = (string)dataReader.GetValue(4);
                    passwordValue = (string)dataReader.GetValue(5);
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
        }

        public void save()
        {
            try
            {
                SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO setup (primaryKey, agentId, userId, password) VALUES (1, '" + agentId + "', '" + userName + "', '" + password + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE setup SET agentId = '" + agentId + "', userId = '" + userName + "', password = '" + password + "' WHERE primaryKey = " + dataReader.GetValue(0));
                    dataReader.Close();
                }
                dataReader.Dispose();

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public string agentId
        {
            set
            {
                agentIdValue = value;
            }
            get
            {
                return agentIdValue;
            }
        }

        public string userName
        {
            set
            {
                userNameValue = value;
            }
            get
            {
                return userNameValue;
            }
        }

        public string password
        {
            set
            {
                passwordValue = value;
            }
            get
            {
                return passwordValue;
            }
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement agentElement = xmlDocumentContext.CreateElement("AGENT");
            XmlElement idElement = xmlDocumentContext.CreateElement("ID");
            XmlElement userNameElement = xmlDocumentContext.CreateElement("USER_NAME");
            XmlElement passwordElement = xmlDocumentContext.CreateElement("PASSWORD");

            idElement.AppendChild(xmlDocumentContext.CreateTextNode(agentIdValue));
            userNameElement.AppendChild(xmlDocumentContext.CreateTextNode(userNameValue));
            passwordElement.AppendChild(xmlDocumentContext.CreateTextNode(passwordValue));

            agentElement.AppendChild(idElement);
            agentElement.AppendChild(userNameElement);
            agentElement.AppendChild(passwordElement);

            return agentElement;
        }
    }
}
