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
using Navipro.Newbody.Woppen.Library;
using Navipro.Infojet.Lib;

namespace WebInterface
{
    public partial class product_slide : System.Web.UI.Page
    {
        protected SalesID salesId;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected DataSet categoryDataSet;
        protected bool reloadPage;
        protected int width;
        protected string backgroundClass;
        protected bool cartReleased;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();
            }
            else
            {
                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null) Response.End();
            }

            WebUserAccount salesPersonWebUserAccount = infojet.userSession.webUserAccount;
            if ((Request["salesPersonWebUserAccountNo"] != null) && (Request["salesPersonWebUserAccountNo"] != ""))
            {
                if (salesId.isContactPerson(infojet.userSession.webUserAccount.no))
                {
                    salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, Request["salesPersonWebUserAccountNo"]);
                }
                else
                {
                    Response.End();
                }
            }

            bool cartIsReleased = salesId.checkReleasedCart(salesPersonWebUserAccount.no);


            if (salesId != null)
            {
                width = 498;
                //if (Request["webPage"] == "SP_ORDER") width = 1000;
                //if (Request["webPage"] == "SP_ORDER2") width = 498;
                //if (Request["webPage"] == "SP_ORDER3") width = 498;

                backgroundClass = "";
                if (Request["webPage"] == "SP_ORDER3") backgroundClass = "background-color: #f5f5f5; border: #cecfc6 1px solid;";

                if (Request["command"] == "addItems")
                {

                    categoryDataSet = salesId.getProductGroups();

                    string lastCategory = "";

                    int i = 0;
                    while (i < categoryDataSet.Tables[0].Rows.Count)
                    {

                        Navipro.Newbody.Woppen.Library.ProductGroup productGroup = new Navipro.Newbody.Woppen.Library.ProductGroup(infojet.systemDatabase, categoryDataSet.Tables[0].Rows[i]);

                        if (lastCategory != productGroup.code)
                        {
                            System.Data.DataSet productDataSet = productGroup.getProducts(infojet.systemDatabase, salesId.itemSelection);

                            int j = 0;
                            while (j < productDataSet.Tables[0].Rows.Count)
                            {
                                Navipro.Infojet.Lib.Item item = new Navipro.Infojet.Lib.Item(infojet.systemDatabase, productDataSet.Tables[0].Rows[j]);
                                if ((Request["qty"+item.no + "box"] != "") && (Request["qty"+item.no + "box"] != null))
                                {
                                    int quantity = 0;
                                    try
                                    {
                                        quantity = int.Parse(Request["qty"+item.no + "box"]);
                                    }
                                    catch (Exception) { }

                                    if (quantity > 0)
                                    {
                                        string released = "";
                                        if (cartIsReleased) released = "1";
                                        infojet.cartHandler.addItemToCart(item.no, quantity.ToString(), false, salesId.getItemSize(item.no), salesId.code, released, "", "", "", salesPersonWebUserAccount);
                                    }
                                }

                                j++;
                            }

                        }
                        i++;
                        lastCategory = productGroup.code;

                    }


                    reloadPage = true;

                }

                if (!reloadPage)
                {
                    categoryDataSet = salesId.getProductGroups();
                }

                if (infojet.userSession.webUserAccount.no != salesId.contactWebUserAccountNo) cartReleased = salesId.checkReleasedCart(salesPersonWebUserAccount.no);
            }
        }
    }
}
