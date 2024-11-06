﻿using System;
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

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class HistoryOrder_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected CustomerHistoryOrder customerHistoryOrder;
        protected string orderConfirmationPdfUrl;
        protected string pickListPdfUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            CustomerHistory customerHistory = new CustomerHistory(infojet);
            customerHistoryOrder = customerHistory.getOrderInfo(customerHistory.getRequestedDocumentNo());

            orderConfirmationPdfUrl = infojet.webPage.getUrl() + "&docNo=" + customerHistory.getRequestedDocumentNo() + "&docType=0&pdf=true";
            pickListPdfUrl = infojet.webPage.getUrl() + "&docNo=" + customerHistory.getRequestedDocumentNo() + "&docType=4&pdf=true";


            orderLineRepeater.DataSource = customerHistoryOrder.lines;
            orderLineRepeater.DataBind();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}