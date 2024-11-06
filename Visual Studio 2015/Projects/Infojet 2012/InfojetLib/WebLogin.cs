using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebCart.
	/// </summary>
	public class WebLogin
	{
        private Infojet infojetContext;

        public string webSiteCode;
        public string code;
        public string description;
		public bool showCustomerName;
		public bool showLogoffLink;
		public bool showMyProfileLink;
        public int type;
        public string profileFormCode;
        public WebForm profileForm;
        public string profileSubmitCategoryCode;
        public bool showChangePasswordButton;
        public string changePasswordTextConstant;
        public string forgotPasswordConfirmWebPageCode;
        public string forgotPasswordFailedWebPageCode;
        public string newUserFormCode;
        public string newUserResponsePageCode;

		private string lastErrorMessage = "";

        public WebLogin() { }

		public WebLogin(Infojet infojetContext, string code) 
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();
		}

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Show Customer Name], [Show Logoff Link], [Show My Profile Link], [Type], [Profile Form Code], [Show Change Password Button], [Change Password Text Constant], [Profile Submit Category Code], [Forgot Pass_ Confirm Page Code], [Forgot Pass_ Failed Page Code], [New User Form Code], [New User Response Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Login") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				
				this.showCustomerName = false;
				if (dataReader.GetValue(3).ToString() == "1") this.showCustomerName = true;

				this.showLogoffLink = false;
				if (dataReader.GetValue(4).ToString() == "1") this.showLogoffLink = true;
			
				this.showMyProfileLink = false;
				if (dataReader.GetValue(5).ToString() == "1") this.showMyProfileLink = true;

                type = int.Parse(dataReader.GetValue(6).ToString());

                this.profileFormCode = dataReader.GetValue(7).ToString();

                this.showChangePasswordButton = false;
                if (dataReader.GetValue(8).ToString() == "1") this.showChangePasswordButton = true;

                this.changePasswordTextConstant = dataReader.GetValue(9).ToString();

                this.profileSubmitCategoryCode = dataReader.GetValue(10).ToString();

                this.forgotPasswordConfirmWebPageCode = dataReader.GetValue(11).ToString();
                this.forgotPasswordFailedWebPageCode = dataReader.GetValue(12).ToString();

                this.newUserFormCode = dataReader.GetValue(13).ToString();
                this.newUserResponsePageCode = dataReader.GetValue(14).ToString();

			}

			dataReader.Close();
			
		}


		private void createUser(Infojet infojetContext)
		{
            /*
			UserProfile userProfile = new UserProfile();

			userProfile.userType = int.Parse(System.Web.HttpContext.Current.Request["userType"]);
			userProfile.name = System.Web.HttpContext.Current.Request["name"];
			userProfile.name2 = System.Web.HttpContext.Current.Request["name2"];
			userProfile.address = System.Web.HttpContext.Current.Request["address"];
			userProfile.address2 = System.Web.HttpContext.Current.Request["address2"];
			userProfile.postCode = System.Web.HttpContext.Current.Request["postCode"];
			userProfile.city = System.Web.HttpContext.Current.Request["city"];
			userProfile.countryCode = System.Web.HttpContext.Current.Request["countryCode"];
			userProfile.phoneNo = System.Web.HttpContext.Current.Request["phoneNo"];
			userProfile.registrationNo = System.Web.HttpContext.Current.Request["registrationNo"];
			userProfile.personNo = System.Web.HttpContext.Current.Request["personNo"];
			userProfile.email = System.Web.HttpContext.Current.Request["email"];
			userProfile.password = System.Web.HttpContext.Current.Request["password"];

			userProfile.webLoginCode = code;



            WebUserAccounts webUserAccounts = new WebUserAccounts(infojetContext.systemDatabase);
			if (webUserAccounts.getUserAccount(userProfile.email) != null)
			{
				lastErrorMessage = infojetContext.translate("ACCOUNT EXISTS");				
			}
			else
			{
				
				ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createUser", userProfile));
				if (!appServerConnection.processRequest())
				{
					lastErrorMessage = appServerConnection.getLastError();
				}
				else
				{
					if (infojetContext.authenticate(userProfile.email, userProfile.password)) infojetContext.authenticationRedirect();
				}
			}
             * 
             * */
		}



		public bool setPassword(Infojet infojetContext, string newPassword)
		{

    		UserProfile userProfile = new UserProfile();

			userProfile.no = infojetContext.userSession.webUserAccount.no;
			userProfile.password = newPassword;

			userProfile.webLoginCode = code;

			
		    ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "updatePassword", userProfile));
		    if (!appServerConnection.processRequest())
		    {
			    lastErrorMessage = appServerConnection.getLastError();
                return false;
		    }
		    else
		    {
			    infojetContext.userSession.refresh();
			    lastErrorMessage = infojetContext.translate("PASSWORD UPDATED");
		    }

            infojetContext.userSession.webUserAccount.refresh();
            return true;

		}

        public Panel getProfileFormPanel()
        {
            Panel profileFormPanel = null;

            if (type == 2)
            {
                WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.profileFormCode);

                webForm.setFormValues(getProfileValues(webForm));

                profileFormPanel = webForm.getFormPanel();

                Label errorLabel = new Label();
                errorLabel.ID = "errorLabel";
                errorLabel.Text = "";
                errorLabel.CssClass = "errorMessage";
                errorLabel.Visible = false;
                profileFormPanel.Controls.AddAt(0, errorLabel);

                Panel buttonPanel = new Panel();
                buttonPanel.CssClass = "submitBtn";

                Button submitButton = new Button();
                submitButton.Text = infojetContext.translate(webForm.webFormSubmitTextConstantCode);
                submitButton.ValidationGroup = webForm.code;
                submitButton.CssClass = "Button";
                submitButton.Click += new EventHandler(submitButton_Click);
                submitButton.ID = "btn_submit";


                if (this.showChangePasswordButton)
                {
                    Button changePasswordButton = new Button();
                    changePasswordButton.Text = infojetContext.translate(this.changePasswordTextConstant);
                    changePasswordButton.CssClass = "Button";
                    changePasswordButton.Click += new EventHandler(changePasswordButton_Click);
                    changePasswordButton.ID = "btn_changePassword";

                    buttonPanel.Controls.Add(changePasswordButton);

                }

                buttonPanel.Controls.Add(submitButton);
                profileFormPanel.Controls.Add(buttonPanel);

            }

            return profileFormPanel;

        }

        public Panel getNewUserFormPanel()
        {
            Panel newUserFormPanel = null;

            if (type == 1)
            {
                WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.newUserFormCode);

                webForm.setFormValues(getProfileValues(webForm));

                newUserFormPanel = webForm.getFormPanel();

                Label errorLabel = new Label();
                errorLabel.ID = "errorLabel";
                errorLabel.Text = "";
                errorLabel.CssClass = "errorMessage";
                errorLabel.Visible = false;
                newUserFormPanel.Controls.AddAt(0, errorLabel);

                Panel buttonPanel = new Panel();
                buttonPanel.CssClass = "submitBtn";
                
                Button registerUserButton = new Button();
                registerUserButton.Text = infojetContext.translate(webForm.webFormSubmitTextConstantCode);
                registerUserButton.ValidationGroup = webForm.code;
                registerUserButton.CssClass = "Button";
                registerUserButton.Click += new EventHandler(registerUserButton_Click);
                registerUserButton.ID = "btn_submit";

                buttonPanel.Controls.Add(registerUserButton);
                newUserFormPanel.Controls.Add(buttonPanel);

            }

            return newUserFormPanel;

        }

        void registerUserButton_Click(object sender, EventArgs e)
        {
            Panel newUserPanel = (Panel)((Button)sender).Parent;
            Label errorLabel = (Label)newUserPanel.FindControl("errorLabel");

            WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.newUserFormCode);

            WebFormDocument webFormDocument = webForm.readForm(newUserPanel);
            webFormDocument.setWebLoginCode(this.code);

            string errorMessage = "";
            if (createUser(webFormDocument, out errorMessage))
            {
                if (this.newUserResponsePageCode != "")
                {
                    WebPage webPage = new WebPage(infojetContext, webSiteCode, this.newUserResponsePageCode);
                    if (webPage != null) infojetContext.redirect(webPage.getUrl());
                }
                else
                {
                    WebPage webPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.startPageCategoryCode, infojetContext.userSession);
                    if (webPage != null) infojetContext.redirect(webPage.getUrl());
                }
            }
            else
            {
                errorLabel.Text = errorMessage;
                errorLabel.Visible = true;

            }
           
        }

        private Hashtable getProfileValues(WebForm webForm)
        {
            Hashtable profileValueTable = new Hashtable();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase);
            if (infojetContext.userSession != null) webUserAccount = infojetContext.userSession.webUserAccount;

            WebFormFields webFormFields = new WebFormFields(infojetContext.systemDatabase);
            DataSet webFormFieldDataSet = webFormFields.getWebFormFields(webSiteCode, webForm.code);
            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);
                
                if (webFormField.connectionType == 1) profileValueTable.Add(webFormField.code, webUserAccount.companyName);
                if (webFormField.connectionType == 2) profileValueTable.Add(webFormField.code, webUserAccount.address);
                if (webFormField.connectionType == 3) profileValueTable.Add(webFormField.code, webUserAccount.address2);
                if (webFormField.connectionType == 4) profileValueTable.Add(webFormField.code, webUserAccount.postCode);
                if (webFormField.connectionType == 5) profileValueTable.Add(webFormField.code, webUserAccount.city);
                if (webFormField.connectionType == 6) profileValueTable.Add(webFormField.code, webUserAccount.countryCode);
                if (webFormField.connectionType == 7) profileValueTable.Add(webFormField.code, webUserAccount.billToCompanyName);
                if (webFormField.connectionType == 8) profileValueTable.Add(webFormField.code, webUserAccount.billToAddress);
                if (webFormField.connectionType == 9) profileValueTable.Add(webFormField.code, webUserAccount.billToAddress2);
                if (webFormField.connectionType == 10) profileValueTable.Add(webFormField.code, webUserAccount.billToPostCode);
                if (webFormField.connectionType == 11) profileValueTable.Add(webFormField.code, webUserAccount.billToCity);
                if (webFormField.connectionType == 12) profileValueTable.Add(webFormField.code, webUserAccount.billToCountryCode);
                if (webFormField.connectionType == 13) profileValueTable.Add(webFormField.code, webUserAccount.registrationNo);
                if (webFormField.connectionType == 14) profileValueTable.Add(webFormField.code, webUserAccount.email);
                if (webFormField.connectionType == 15) profileValueTable.Add(webFormField.code, webUserAccount.phoneNo);
                if (webFormField.connectionType == 16) profileValueTable.Add(webFormField.code, webUserAccount.cellPhoneNo);
                if (webFormField.connectionType == 17) profileValueTable.Add(webFormField.code, webUserAccount.companyRole);
                if (webFormField.connectionType == 18) profileValueTable.Add(webFormField.code, webUserAccount.name);
                if (webFormField.connectionType == 19) profileValueTable.Add(webFormField.code, webUserAccount.userId);
                if (webFormField.connectionType == 20) profileValueTable.Add(webFormField.code, webUserAccount.password);
                if (webFormField.connectionType == 21) profileValueTable.Add(webFormField.code, webUserAccount.customerNo);
                if (webFormField.connectionType == 29) profileValueTable.Add(webFormField.code, webUserAccount.companyName2);
                if (webFormField.connectionType == 32) profileValueTable.Add(webFormField.code, webUserAccount.billToCompanyName2);


                if (!profileValueTable.Contains(webFormField.code)) profileValueTable.Add(webFormField.code, webUserAccount.getHistoryProfileValue(webForm.code, webFormField.code));
               
                i++;
            }

            return profileValueTable;
        }

        void changePasswordButton_Click(object sender, EventArgs e)
        {
            WebPage changePasswordPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.changePasswordCategoryCode, infojetContext.userSession);
            if (changePasswordPage != null) infojetContext.redirect(changePasswordPage.getUrl());
        }

        void submitButton_Click(object sender, EventArgs e)
        {
            Panel profilePanel = (Panel)((Button)sender).Parent;
            Label errorLabel = (Label)profilePanel.FindControl("errorLabel");

            WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.profileFormCode);

            WebFormDocument webFormDocument = webForm.readForm(profilePanel);
            webFormDocument.setWebLoginCode(this.code);

            string errorMessage = "";
            if (submitProfile(webFormDocument, out errorMessage, infojetContext.userSession.webUserAccount))
            {
                if (this.profileSubmitCategoryCode != "")
                {
                    WebPage webPage = infojetContext.webSite.getWebPageByCategory(this.profileSubmitCategoryCode, infojetContext.userSession);
                    if (webPage != null) infojetContext.redirect(webPage.getUrl());
                }
                else
                {
                    WebPage webPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.authenticatedCategoryCode, infojetContext.userSession);
                    if (webPage != null) infojetContext.redirect(webPage.getUrl());
                }
            }
            else
            {
                errorLabel.Text = errorMessage;
                errorLabel.Visible = true;

            }
        }

        public bool createUser(WebFormDocument webFormDocument, out string errorMessage)
        {
            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createUser", webFormDocument));
            if (!appServerConnection.processRequest())
            {
                errorMessage = appServerConnection.serviceResponse.errorMessage;
            }
            else
            {
                if (appServerConnection.serviceResponse.containsErrors)
                {
                    errorMessage = appServerConnection.serviceResponse.errorMessage;
                }
                else
                {
                    errorMessage = "";
                    return true;
                }
            }

            return false;
        }

        public bool submitProfile(WebFormDocument webFormDocument, out string errorMessage, WebUserAccount webUserAccount)
        {
            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "updateUserProfile", webFormDocument, webUserAccount));
            if (!appServerConnection.processRequest())
            {
                errorMessage = appServerConnection.serviceResponse.errorMessage;
            }
            else
            {
                if (appServerConnection.serviceResponse.containsErrors)
                {
                    errorMessage = appServerConnection.serviceResponse.errorMessage;
                }
                else
                {

                    UserSession userSession = infojetContext.userSession;
                    if (userSession != null)
                    {
                        userSession.refresh();
                        System.Web.HttpContext.Current.Session["currentUserSession"] = userSession;
                    }
                    errorMessage = "";
                    return true;
                }
            }

            return false;
        }

        public bool createNewPassword(WebFormDocument webFormDocument, out string errorMessage)
        {
            WebUserAccount webUserAccount = new WebUserAccount();

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createNewPassword", webFormDocument, webUserAccount));
            if (!appServerConnection.processRequest())
            {
                errorMessage = appServerConnection.serviceResponse.errorMessage;
            }
            else
            {
                if (appServerConnection.serviceResponse.containsErrors)
                {
                    errorMessage = appServerConnection.serviceResponse.errorMessage;
                }
                else
                {

                    UserSession userSession = infojetContext.userSession;
                    if (userSession != null)
                    {
                        userSession.refresh();
                        System.Web.HttpContext.Current.Session["currentUserSession"] = userSession;
                    }
                    errorMessage = "";
                    return true;
                }
            }

            return false;
        }


        public string getLastErrorMessage()
        {
            return this.lastErrorMessage;
        }


        public bool performUserAccountCheck(Infojet infojetContext, string email)
        {

            UserProfile userProfile = new UserProfile();

            userProfile.email = email;
            userProfile.webLoginCode = code;


            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "performUserAccountCheck", userProfile));
            return appServerConnection.processRequest();

        }

    }
}
