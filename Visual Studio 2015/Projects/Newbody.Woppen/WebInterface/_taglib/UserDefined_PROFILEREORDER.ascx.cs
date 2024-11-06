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
using Navipro.Newbody.Woppen.Library;

namespace WebInterface._taglib
{
    public partial class UserDefined_PROFILEREORDER : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected string sendOrderLink;
        protected SalesIDCollection salesIdCollection;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            if (Request["reorderCommand"] == "toggle")
            {
                SalesID salesId = new SalesID(infojet, Request["reorderSalesId"]);

                OrderItemCollection orderItemCollection = new OrderItemCollection();
                OrderItem orderItem = new OrderItem();
                orderItem.salesId = salesId.code;
                if (salesId.additionalOrder) orderItem.method = 0;
                if (!salesId.additionalOrder) orderItem.method = 1;

                orderItemCollection.Add(orderItem);

                orderItemCollection.setSalesId(salesId.code);

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "reorderSetup", orderItemCollection));
                if (!appServerConnection.processRequest())
                {
                    errorMessageLabel.Text = appServerConnection.getLastError();
                }


            }
            
            salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);

            int i = 0;
            while (i < salesIdCollection.Count)
            {
                SalesID salesId = salesIdCollection[i];
                if (!salesId.isPrimaryContactPerson(infojet.userSession.webUserAccount.no)) salesIdCollection.Remove(salesId);

                i++;
            }

            
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}