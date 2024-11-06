using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebService
{
    /// <summary>
    /// Summary description for WebSiteService
    /// </summary>
    [WebService(Namespace = "http://infojet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebSiteService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession=true)]
        public WebSite[] getWebSites()
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            WebSite[] webSiteArray = Navipro.Infojet.Lib.WebSite.getWebSiteArray(infojetContext);

            infojetContext.systemDatabase.close();

            return webSiteArray;
        }

        [WebMethod(EnableSession = true)]
        public WebPage[] getWebPages(string webSiteCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            WebPage[] webPageArray = Navipro.Infojet.Lib.WebPage.getWebPageArray(infojetContext, webSiteCode);

            infojetContext.systemDatabase.close();

            return webPageArray;
        }

        [WebMethod(EnableSession = true)]
        public WebPageLine[] getWebPageLines(string webSiteCode, string webPageCode, int versionNo)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();           
            WebPageLines webPageLines = new WebPageLines(infojetContext.systemDatabase);
            System.Data.DataSet webPageLineDataSet = webPageLines.getWebPageLines(webSiteCode, webPageCode, versionNo);

            WebPageLine[] webPageLineArray = new WebPageLine[webPageLineDataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < webPageLineDataSet.Tables[0].Rows.Count)
            {
                WebPageLine webPageLine = new WebPageLine(infojetContext, webPageLineDataSet.Tables[0].Rows[i]);
                webPageLineArray[i] = webPageLine;

                i++;
                
            }
            
            infojetContext.systemDatabase.close();

            return webPageLineArray;
        }

        [WebMethod(EnableSession = true)]
        public WebTemplatePart[] getWebTemplateParts(string webSiteCode, string webTemplateCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            WebTemplate webTemplate = new WebTemplate(infojetContext.systemDatabase, webSiteCode, webTemplateCode);
            System.Data.DataSet webTemplatePartDataSet = webTemplate.getParts();

            WebTemplatePart[] webTemplatePartArray = new WebTemplatePart[webTemplatePartDataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < webTemplatePartDataSet.Tables[0].Rows.Count)
            {
                WebTemplatePart webTemplatePart = new WebTemplatePart(infojetContext, webTemplatePartDataSet.Tables[0].Rows[i]);
                webTemplatePartArray[i] = webTemplatePart;

                i++;

            }

            infojetContext.systemDatabase.close();

            return webTemplatePartArray;
        }

        [WebMethod(EnableSession = true)]
        public WebTemplate[] getWebTemplates(string webSiteCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
                      
            System.Data.DataSet webTemplateDataSet = WebTemplate.getWebTemplates(infojetContext, webSiteCode);
            WebTemplate[] webTemplateArray = new WebTemplate[webTemplateDataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < webTemplateDataSet.Tables[0].Rows.Count)
            {
                WebTemplate webTemplate = new WebTemplate(infojetContext, webTemplateDataSet.Tables[0].Rows[i]);
                webTemplateArray[i] = webTemplate;

                i++;

            }

            infojetContext.systemDatabase.close();

            return webTemplateArray;
        }

        [WebMethod(EnableSession = true)]
        public string getWebPageLineParagraph(string webSiteCode, string webPageCode, int versionNo, int lineNo, string languageCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebPageLine webPageLine = new WebPageLine(infojetContext, webSiteCode, webPageCode, versionNo, lineNo);
            string text = webPageLine.getText(languageCode);

            infojetContext.systemDatabase.close();

            return text;
        }

        [WebMethod(EnableSession = true)]
        public void createWebPage(string webSiteCode, string webPageCode, string description, string webTemplateCode)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebPage webPage = new WebPage();
            webPage.webSiteCode = webSiteCode;
            webPage.code = webPageCode;
            webPage.description = description;
            webPage.webTemplateCode = webTemplateCode;

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createWebPage", webPage));
            if (!appServerConnection.processRequest())
            {
                throw new Exception(appServerConnection.getLastError());
            }

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public void createWebPageLine(string webSiteCode, string webPageCode, int type, string code, string webTypeCode, int sortOrder)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebPageLine webPageLine = new WebPageLine();
            webPageLine.webSiteCode = webSiteCode;
            webPageLine.webPageCode = webPageCode;
            webPageLine.type = type;
            webPageLine.code = code;
            webPageLine.webTypeCode = webTypeCode;
            webPageLine.sortOrder = sortOrder;

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createWebPageLine", webPageLine));
            if (!appServerConnection.processRequest())
            {
                throw new Exception(appServerConnection.getLastError());
            }

            infojetContext.systemDatabase.close();
        }

        [WebMethod(EnableSession = true)]
        public WebPage getStartPage(string webSiteCode, string webUserAccountNo)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            if (webUserAccountNo == "") webUserAccountNo = null;

            System.Collections.ArrayList webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.startPageCategoryCode, webUserAccountNo);

            infojetContext.systemDatabase.close();

            if (webPageList.Count > 0)
            {
                WebPage webPage = (WebPage)webPageList[0];
                return webPage;
            }

            return null;

        }

        [WebMethod(EnableSession = true)]
        public Byte[] getWebImage(string webImageCode, int width, int height)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebImage webImage = new WebImage(infojetContext, webImageCode);
            Byte[] byteArray = webImage.getByteArray(width, height);

            infojetContext.systemDatabase.close();

            return byteArray;
        }

        [WebMethod(EnableSession = true)]
        public UserSession userProfile_authenticate(string webSiteCode, string userId, string password)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SystemHandler systemHandler = new SystemHandler(infojetContext);
            if (systemHandler.authenticate(infojetContext, userId, password))
            {

                UserSession userSession = infojetContext.userSession;

                System.Collections.ArrayList webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.authenticatedCategoryCode, userSession.webUserAccount.no);

                infojetContext.systemDatabase.close();

                if (webPageList.Count > 0)
                {
                    WebPage webPage = (WebPage)webPageList[0];
                    userSession.startPageCode = webPage.code;
                }


                return userSession;
            }

            return null;

        }


        [WebMethod(EnableSession = true)]
        public WebTextConstantValue[] getTranslations(string webSiteCode, string languageCode)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebTextConstantValue[] webTextConstantValueArray = WebTextConstantValue.getWebTextConstantValueArray(infojetContext, webSiteCode, languageCode);

            infojetContext.systemDatabase.close();

            return webTextConstantValueArray;

        }


        [WebMethod(EnableSession = true)]
        public WebForm getForm(string webSiteCode, string webFormCode, string languageCode, string webUserAccountNo)
        {
            Global.init(webSiteCode, languageCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            WebForm webForm = new WebForm(infojetContext, webSiteCode, webFormCode);
            webForm.updateArrays(webUserAccount);

            infojetContext.systemDatabase.close();

            return webForm;

        }

        [WebMethod(EnableSession = true)]
        public LoginProfileResponse submitLoginProfile(string webSiteCode, string webLoginCode, string webUserAccountNo, FormField[] formFieldArray)
        {
            LoginProfileResponse response = new LoginProfileResponse();
            
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();
            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

            
            if (webUserAccountNo != "") infojetContext.systemHandler.createSession(infojetContext, webUserAccount, webSiteCode);

            ArrayList keyList = new ArrayList();
            ArrayList valueList = new ArrayList();
            ArrayList fileList = new ArrayList();
            string errorMessage = "";

            int i = 0;
            while (i < formFieldArray.Length)
            {
                keyList.Add(formFieldArray[i].fieldCode);
                valueList.Add(formFieldArray[i].value);

                i++;
            }


            WebLogin webLogin = new WebLogin(infojetContext, webLoginCode);
            WebForm webForm = new WebForm(infojetContext, webSiteCode, webLogin.profileFormCode);
            WebFormDocument webFormDocument = new WebFormDocument(webForm, keyList, valueList, fileList);
            webFormDocument.setWebLoginCode(webLogin.code);

            bool result = false;
            if (webLogin.type == 1) result = webLogin.createUser(webFormDocument, out errorMessage);
            if (webLogin.type == 2) result = webLogin.submitProfile(webFormDocument, out errorMessage, webUserAccount);
            //if (webLogin.type == 3) result = webLogin.setPassword(webFormDocument, out errorMessage);
            if (webLogin.type == 4) result = webLogin.createNewPassword(webFormDocument, out errorMessage);

            if (result)
            {

                if (webLogin.profileSubmitCategoryCode != "")
                {                  
                    WebPage webPage = infojetContext.webSite.getWebPageByCategory(webLogin.profileSubmitCategoryCode, infojetContext.userSession);
                    if (webPage != null) response.responseWebPageCode = webPage.code;
                }
                else
                {
                    WebPage webPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.authenticatedCategoryCode, infojetContext.userSession);
                    if (webPage != null) response.responseWebPageCode = webPage.code;
                }

                infojetContext.systemDatabase.close();
                response.result = result;
                response.errorMessage = errorMessage;
                return response;
            }

            infojetContext.systemDatabase.close();

            response.result = false;
            response.errorMessage = errorMessage;

            return response;
        }

        [WebMethod(EnableSession = true)]
        public LoginProfileResponse testSubmitLoginProfile()
        {
            FormField[] formFieldArray = new FormField[2];

            FormField formField1 = new FormField();
            formField1.fieldCode = "EMAIL";
            formField1.value = "hepp@hopp.com";
            formFieldArray[0] = formField1;

            FormField formField2 = new FormField();
            formField2.fieldCode = "MOBIL";
            formField2.value = "12345";
            formFieldArray[1] = formField2;

            return submitLoginProfile("PARTNER", "KP_PROFIL", "YC000058", formFieldArray);


        }

        [WebMethod(EnableSession = true)]
        public LoginProfileResponse testForgetPassword()
        {
            FormField[] formFieldArray = new FormField[2];

            FormField formField1 = new FormField();
            formField1.fieldCode = "CELLPHONE";
            formField1.value = "0735-112233";
            formFieldArray[0] = formField1;

            FormField formField2 = new FormField();
            formField2.fieldCode = "NAME";
            formField2.value = "Gunnar";
            formFieldArray[1] = formField2;

            return submitLoginProfile("PARTNER", "SP_PROFILE", "YC000053", formFieldArray);
        }

        [WebMethod(EnableSession = true)]
        public string submitFormAndGetResponsePage(string webSiteCode, string webFormCode, string webUserAccountNo, WebFormField[] webFormFieldArray)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            ArrayList keyList = new ArrayList();
            ArrayList valueList = new ArrayList();
            ArrayList fileList = new ArrayList();

            int i = 0;
            while (i < webFormFieldArray.Length)
            {
                keyList.Add(webFormFieldArray[i].code);
                valueList.Add(webFormFieldArray[i].fieldValue);

                i++;
            }

            WebForm webForm = new WebForm(infojetContext, webSiteCode, webFormCode);
            WebFormDocument webFormDocument = new WebFormDocument(webForm, keyList, valueList, fileList);

            webForm.submitForm(webFormDocument);

            string responseWebPageCode = "";

            if ((webForm.confirmWebCategoryCode != null) && (webForm.confirmWebCategoryCode != ""))
            {
                WebPage webPage = infojetContext.webSite.getWebPageByCategory(webForm.confirmWebCategoryCode, infojetContext.userSession);
                if (webPage != null)
                {
                    responseWebPageCode = webPage.code; 
                }
            }


            infojetContext.systemDatabase.close();

            return responseWebPageCode;
        }


        [WebMethod(EnableSession = true)]
        public WebNewsCollection getNews(string webSiteCode, string newsCategoryCode, string languageCode)
        {
            Global.init(webSiteCode, languageCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebNewsEntries webNewsEntries = new WebNewsEntries(infojetContext);
            WebNewsCollection webNewsCollection = webNewsEntries.getNews(newsCategoryCode);

            infojetContext.systemDatabase.close();

            return webNewsCollection;

        }

        [WebMethod(EnableSession = true)]
        public WebLogin getLoginProfile(string webSiteCode, string code, string languageCode, string webUserAccountNo)
        {
            Global.init(webSiteCode, languageCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebLogin webLogin = new WebLogin(infojetContext, code);
            if (webLogin.profileFormCode != "")
            {
                WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);

                WebForm webForm = new WebForm(infojetContext, webSiteCode, webLogin.profileFormCode);
                webForm.updateArrays(webUserAccount);

                webLogin.profileForm = webForm;
            }

            infojetContext.systemDatabase.close();

            return webLogin;

        }

        [WebMethod(EnableSession = true)]
        public NavigationItemCollection getMenuItems(string webSiteCode, string code, string currentWebPageCode, string webUserAccountNo, string languageCode)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            WebUserAccount webUserAccount = null;
            if (webUserAccountNo != "")
            {
                webUserAccount = new WebUserAccount(infojetContext.systemDatabase, webUserAccountNo);
                //infojetContext.systemHandler.createSession(infojetContext, webUserAccount, infojetContext.webSite.code);
            }

            WebMenu webMenu = new WebMenu(infojetContext, code);
            NavigationItemCollection navigationItemCollection = webMenu.getMenuItems(infojetContext, webUserAccount, languageCode);
            
            infojetContext.systemDatabase.close();

            return navigationItemCollection;

        }

    }
}
