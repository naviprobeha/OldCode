using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebUserAccount : ServiceArgument
	{
		private Database database;

		public string no;
		public string userId;
		public string password;
		public bool closed;
        public string name;
        public string contactNo;
		public string customerNo;
        public string companyName;
        public string address;
        public string address2;
        public string postCode;
        public string city;
        public string countryCode;
        public string billToCompanyName;
        public string billToAddress;
        public string billToAddress2;
        public string billToPostCode;
        public string billToCity;
        public string billToCountryCode;
		public string registrationNo;
        public string companyRole;
		public string email;
        public string phoneNo;
        public string cellPhoneNo;
        public string contactName;
		public DateTime historyDateTime;
		public DateTime lastLoggedOnDateTime;
		public int maxBuyType;
		public float maxBuyLimitPerOrder;
        public string languageCode;

		public WebUserAccount(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
		}

		public WebUserAccount(Database database, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.no = no;

			getFromDatabase();
		}

		public WebUserAccount(Database database, SqlDataReader dataReader)
		{
			this.database = database;

			readData(dataReader);
		}

		public WebUserAccount(Database database, DataRow dataRow)
		{
			this.maxBuyLimitPerOrder = 0;

			this.database = database;

			this.no =  dataRow.ItemArray.GetValue(0).ToString();
			this.userId = dataRow.ItemArray.GetValue(1).ToString();
			this.password = dataRow.ItemArray.GetValue(2).ToString();

			this.closed = false;
			if (int.Parse(dataRow.ItemArray.GetValue(3).ToString()) == 1) this.closed = true;

			this.contactNo = dataRow.ItemArray.GetValue(4).ToString();
            this.customerNo = dataRow.ItemArray.GetValue(5).ToString();

            this.companyName = dataRow.ItemArray.GetValue(6).ToString();
            this.address = dataRow.ItemArray.GetValue(7).ToString();
            this.address2 = dataRow.ItemArray.GetValue(8).ToString();
            this.postCode = dataRow.ItemArray.GetValue(9).ToString();
            this.city = dataRow.ItemArray.GetValue(10).ToString();
            this.countryCode = dataRow.ItemArray.GetValue(11).ToString();
            this.billToCompanyName = dataRow.ItemArray.GetValue(12).ToString();
            this.billToAddress = dataRow.ItemArray.GetValue(13).ToString();
            this.billToAddress2 = dataRow.ItemArray.GetValue(14).ToString();
            this.billToPostCode = dataRow.ItemArray.GetValue(15).ToString();
            this.billToCity = dataRow.ItemArray.GetValue(16).ToString();
            this.billToCountryCode = dataRow.ItemArray.GetValue(17).ToString();

            this.registrationNo = dataRow.ItemArray.GetValue(18).ToString();
			this.email = dataRow.ItemArray.GetValue(19).ToString();
            this.phoneNo = dataRow.ItemArray.GetValue(20).ToString();

            DateTime lastLoggedOnDate = DateTime.Parse(dataRow.ItemArray.GetValue(21).ToString());
            DateTime lastLoggedOnTime = DateTime.Parse(dataRow.ItemArray.GetValue(22).ToString());

			DateTime historyDate = DateTime.Parse(dataRow.ItemArray.GetValue(23).ToString());
			DateTime historyTime = DateTime.Parse(dataRow.ItemArray.GetValue(24).ToString());

			historyDateTime = DateTime.Parse(historyDate.ToString("yyyy-MM-dd")+" "+historyTime.ToString("HH:mm:ss"));
			lastLoggedOnDateTime = DateTime.Parse(lastLoggedOnDate.ToString("yyyy-MM-dd")+" "+lastLoggedOnTime.ToString("HH:mm:ss"));

			this.maxBuyType = int.Parse(dataRow.ItemArray.GetValue(25).ToString());
			this.maxBuyLimitPerOrder = float.Parse(dataRow.ItemArray.GetValue(26).ToString());

            this.companyRole = dataRow.ItemArray.GetValue(27).ToString();
            this.cellPhoneNo = dataRow.ItemArray.GetValue(28).ToString();

            this.name = dataRow.ItemArray.GetValue(29).ToString();
            this.languageCode = dataRow.ItemArray.GetValue(39).ToString();
		}

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [User-ID], [Password], [Closed], [Contact No_], [Customer No_], [Company Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Bill-to Company Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Registration No_], [E-Mail], [Phone No_], [Last Logged On Date], [Last Logged On Time], [History Date], [History Time], [Max Buy Type], [Max Buy Limit _ Order], [Company Role], [Cell Phone No_], [Name], [Language Code] FROM [" + database.getTableName("Web User Account") + "] WITH (NOLOCK) WHERE [No_] = @no AND [Type] = 1");
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
			{
				readData(dataReader);
			}

			dataReader.Close();


		}

		private void readData(SqlDataReader dataReader)
		{

			no = dataReader.GetValue(0).ToString();
			userId = dataReader.GetValue(1).ToString();
			password = dataReader.GetValue(2).ToString();
				
			closed = false;
			if (dataReader.GetValue(3).ToString() == "1") closed = true;

            contactNo = dataReader.GetValue(4).ToString();
			customerNo = dataReader.GetValue(5).ToString();

            companyName = dataReader.GetValue(6).ToString();
            address = dataReader.GetValue(7).ToString();
            address2 = dataReader.GetValue(8).ToString();
            postCode = dataReader.GetValue(9).ToString();
            city = dataReader.GetValue(10).ToString();
            countryCode = dataReader.GetValue(11).ToString();
            billToCompanyName = dataReader.GetValue(12).ToString();
            billToAddress = dataReader.GetValue(13).ToString();
            billToAddress2 = dataReader.GetValue(14).ToString();
            billToPostCode = dataReader.GetValue(15).ToString();
            billToCity = dataReader.GetValue(16).ToString();
            billToCountryCode = dataReader.GetValue(17).ToString();

            registrationNo = dataReader.GetValue(18).ToString();
			email = dataReader.GetValue(19).ToString();
            phoneNo = dataReader.GetValue(20).ToString();

            DateTime lastLoggedOnDate = dataReader.GetDateTime(21);
            DateTime lastLoggedOnTime = dataReader.GetDateTime(22);

			DateTime historyDate = dataReader.GetDateTime(23);
			DateTime historyTime = dataReader.GetDateTime(24);
		
			maxBuyType = int.Parse(dataReader.GetValue(25).ToString());
			maxBuyLimitPerOrder = float.Parse(dataReader.GetValue(26).ToString());

            companyRole = dataReader.GetValue(27).ToString();
            cellPhoneNo = dataReader.GetValue(28).ToString();

            name = dataReader.GetValue(29).ToString();
            languageCode = dataReader.GetValue(30).ToString();
		}


		public void refresh()
		{
			getFromDatabase();
		}

        public string getHistoryProfileValue(string webFormCode, string webFormFieldCode)
        {
            string fieldValue = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT [Value] FROM [" + database.getTableName("Web User Account Profile Line") + "] l, [" + database.getTableName("Web User Account Profile Entry") + "] h WHERE h.[Web User Account No_] = @no AND l.[Web User Account No_] = h.[Web User Account No_] AND l.[Web User Acc Profile Entry No_] = h.[Entry No_] AND h.[Current Profile] = 1 AND l.[Field Code] = @webFormFieldCode");
            databaseQuery.addStringParameter("no", this.no, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                fieldValue = dataReader.GetValue(0).ToString();
            }

            dataReader.Close();


            return fieldValue;
        }


        public void sendUserCredentialsByMail(Infojet infojetContext)
        {
            Configuration configuration = infojetContext.configuration;
            WebSiteNewUserMessageLines newUserMessageLines = new WebSiteNewUserMessageLines(infojetContext);

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.From = new System.Net.Mail.MailAddress(configuration.smtpSender);
            mailMessage.To.Add(this.email);
            mailMessage.Subject = newUserMessageLines.getSubject();
            mailMessage.IsBodyHtml = true;

            DataSet linesDataSet = newUserMessageLines.getLines();

            string body = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\" width=\"400\">";
            
            int i = 0;
            while (i < linesDataSet.Tables[0].Rows.Count)
            {
                string lineText = linesDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                lineText = lineText.Replace("${NAME}", name);
                lineText = lineText.Replace("${USERID}", userId);
                lineText = lineText.Replace("${PASSWORD}", password);

                if (lineText == "") lineText = "&nbsp;";

                body = body + "<tr>";
                body = body + "<td colspan=\"2\" style=\"font-family: Verdana; font-size: 12px\">"+lineText+"</td>";
                body = body + "</tr>";

                i++;
            }


            body = body + "</table>";

            mailMessage.Body = body;


            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = configuration.smtpServer;
                smtpClient.Port = configuration.smtpPort;

                if (configuration.smtpAuthenticate == 1)
                {
                    smtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential smtpAuthentication = new System.Net.NetworkCredential(configuration.smtpUserName, configuration.smtpPassword);
                    smtpClient.Credentials = smtpAuthentication;
                }

                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                throw new Exception("Error sending mail from: " + configuration.smtpSender + ", to: " + this.email + ", Server: " + configuration.smtpServer + ", Exception: " + e.Message);
            }

        }



        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {

            XmlElement containerElement = xmlDoc.CreateElement("webUserAccount");

            XmlElement fieldElement = null;

            fieldElement = xmlDoc.CreateElement("no");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.no));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("userId");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.userId));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("password");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.password));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("customerNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.customerNo));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("name");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.name));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("companyName");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.companyName));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("address");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.address));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("address2");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.address2));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("postCode");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.postCode));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("city");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.city));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("countryCode");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.countryCode));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToCompanyName");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToCompanyName));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToAddress");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToAddress));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToAddress2");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToAddress2));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToPostCode");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToPostCode));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToCity");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToCity));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("billToCountryCode");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.billToCountryCode));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("cellPhoneNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.cellPhoneNo));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("phoneNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.phoneNo));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("email");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.email));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("companyRole");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.companyRole));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("contactNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.contactNo));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("registrationNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.registrationNo));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("maxBuyType");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.maxBuyType.ToString()));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("maxBuyLimitPerOrder");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.maxBuyLimitPerOrder.ToString()));
            containerElement.AppendChild(fieldElement);

            if (this.closed)
            {
                fieldElement = xmlDoc.CreateElement("closed");
                fieldElement.AppendChild(xmlDoc.CreateTextNode("true"));
                containerElement.AppendChild(fieldElement);
            }

            return containerElement;
        }

        #endregion
    }
}
