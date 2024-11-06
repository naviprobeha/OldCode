using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface
{
    public partial class InfojetControl : System.Web.UI.UserControl
    {
        private Navipro.Infojet.Lib.Infojet infojet;
        private string _part;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            ArrayList pagePartList = infojet.getContent(_part);

            int i = 0;
            while (i < pagePartList.Count)
            {
                Navipro.Infojet.Lib.WebPageLine webPageLine = (Navipro.Infojet.Lib.WebPageLine)pagePartList[i];
                if (webPageLine.type == 0) addTextControl(webPageLine);
                if (webPageLine.type == 1) addImageControl(webPageLine);
                if (webPageLine.type == 2) addMenuControl(webPageLine);
                if (webPageLine.type == 3) addLoginControl(webPageLine);
                if (webPageLine.type == 4) addProductTreeControl(webPageLine);
                if (webPageLine.type == 5) addCartControl(webPageLine);
                if (webPageLine.type == 6) addProductListControl(webPageLine);
                if (webPageLine.type == 7) addCampainControl(webPageLine);
                if (webPageLine.type == 8) addCheckoutControl(webPageLine);
                if (webPageLine.type == 9) addFormControl(webPageLine);
                //if (webPageLine.type == 10) addControl(webPageLine);
                //if (webPageLine.type == 11) addControl(webPageLine);
                if (webPageLine.type == 12) addProductCategoryControl(webPageLine);
                if (webPageLine.type == 13) addProductDetailControl(webPageLine);
                if (webPageLine.type == 14) addUserDefinedControl(webPageLine);
                if (webPageLine.type == 15) addHistoryOrderControl(webPageLine);
                if (webPageLine.type == 16) addHistoryShipmentControl(webPageLine);
                if (webPageLine.type == 17) addHistoryInvoiceControl(webPageLine);
                if (webPageLine.type == 18) addHistoryCrMemoControl(webPageLine);
                if (webPageLine.type == 19) addHistoryCustomerLedgerControl(webPageLine);
                if (webPageLine.type == 20) addSearchResultControl(webPageLine);
                if (webPageLine.type == 21) addNewsFlowControl(webPageLine);
                if (webPageLine.type == 22) addNewsEntryControl(webPageLine);
                if (webPageLine.type == 23) addSearchBoxControl(webPageLine);
                if (webPageLine.type == 24) addRegisterNewsletterControl(webPageLine);
                if (webPageLine.type == 25) addLanguageControl(webPageLine);

                i++;
            }

        }

        public string part
        {
            get
            {
                return _part;
            }
            set
            {
                _part = value;
            }
        }

        private void addControl(string controlName, Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (appPath == "/") appPath = "";

            System.Web.UI.Control control = Page.LoadControl(appPath+"/_taglib/" + controlName);
            Navipro.Infojet.Lib.InfojetUserControl userControl = (Navipro.Infojet.Lib.InfojetUserControl)control;
            userControl.setWebPageLine(webPageLine);

            content.Controls.Add(control);
        }


        private void addTextControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Text.ascx", webPageLine);
        }

        private void addImageControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Image.ascx", webPageLine);
        }

        private void addMenuControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Menu_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addLoginControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            WebLogin webLogin = new WebLogin(infojet, webPageLine.code);

            if (webLogin.type == 0) addControl("Login_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            if (webLogin.type == 1) addControl("NewUser_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            if (webLogin.type == 2) addControl("MyProfile_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            if (webLogin.type == 3) addControl("ChangePassword_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            if (webLogin.type == 4) addControl("ForgotPassword_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addProductTreeControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("ProductTree_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addCartControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Cart_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addProductListControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("ProductList_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addCampainControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Campain_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addCheckoutControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            WebCheckout webCheckout = new WebCheckout(infojet, infojet.webSite.code, webPageLine.code);
            addControl(webCheckout.getUserControl() + "_"+ webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addFormControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Form.ascx", webPageLine);
        }

        private void addProductCategoryControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("ProductCategory_" + webPageLine.code + ".ascx", webPageLine);
        }

        private void addProductDetailControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            WebItemList webItemList = new WebItemList(infojet, webPageLine.code);

            //if (webItemList.getRequestedWebModelNo() != "")
            //{
            addControl("ProductDetailsModel_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            //}
            //else
            //{
            //    addControl("ProductDetails_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            //}
        }

        private void addUserDefinedControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("UserDefined_" + webPageLine.code + ".ascx", webPageLine);
        }

        private void addHistoryOrderControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            CustomerHistory customerHistory = new CustomerHistory(infojet);

            if (customerHistory.showDetailedInformation())           
            {
                if (customerHistory.getRequestedDocumentType() == 0) addControl("HistoryOrder_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 1) addControl("HistoryShipment_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 2) addControl("HistoryInvoice_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 3) addControl("HistoryCrMemo_" + webPageLine.code + ".ascx", webPageLine);
            }
            else
            {
                addControl("HistoryOrderList_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            }
        }

        private void addHistoryShipmentControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            CustomerHistory customerHistory = new CustomerHistory(infojet);

            if (customerHistory.showDetailedInformation())
            {
                if (customerHistory.getRequestedDocumentType() == 0) addControl("HistoryOrder_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 1) addControl("HistoryShipment_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 2) addControl("HistoryInvoice_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 3) addControl("HistoryCrMemo_" + webPageLine.code + ".ascx", webPageLine);
            }
            else
            {
                addControl("HistoryShipmentList_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            }
        }

        private void addHistoryInvoiceControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            CustomerHistory customerHistory = new CustomerHistory(infojet);
            
            if (customerHistory.showDetailedInformation())
            {
                if (customerHistory.getRequestedDocumentType() == 0) addControl("HistoryOrder_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 1) addControl("HistoryShipment_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 2) addControl("HistoryInvoice_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 3) addControl("HistoryCrMemo_" + webPageLine.code + ".ascx", webPageLine);

            }
            else
            {
                addControl("HistoryInvoiceList_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            }
        }

        private void addHistoryCrMemoControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            CustomerHistory customerHistory = new CustomerHistory(infojet);

            if (customerHistory.showDetailedInformation())
            {               
                if (customerHistory.getRequestedDocumentType() == 0) addControl("HistoryOrder_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 1) addControl("HistoryShipment_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 2) addControl("HistoryInvoice_" + webPageLine.code + ".ascx", webPageLine);
                if (customerHistory.getRequestedDocumentType() == 3) addControl("HistoryCrMemo_" + webPageLine.code + ".ascx", webPageLine);

            }
            else
            {
                addControl("HistoryCrMemoList_" + webPageLine.webTypeCode + ".ascx", webPageLine);
            }
        }

        private void addHistoryCustomerLedgerControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("HistoryCustomerLedger_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addSearchResultControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("SearchEngine_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addSearchBoxControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("SearchBox_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }
        
        private void addRegisterNewsletterControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Newsletter_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }
 
        private void addNewsFlowControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("NewsFlow_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addNewsEntryControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("NewsEntry_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

        private void addLanguageControl(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            addControl("Language_" + webPageLine.webTypeCode + ".ascx", webPageLine);
        }

    }
}