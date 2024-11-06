using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebCart.
    /// </summary>
    public class WebCart
    {
        private Infojet infojetContext;

        private string _webSiteCode;
        private string _code;
        private string _description;
        private string _webShopRegisterCode;
        private bool _showWhenNotLoggedIn;
        private bool _showClearButton;


        public WebCart(Infojet infojetContext, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this._webSiteCode = infojetContext.webSite.code;
            this._code = code;

            getFromDatabase();
        }

        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Web Shop Register Code], [Show When Not Logged In], [Show Clear Button] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", _code, 20);
            databaseQuery.addStringParameter("webSiteCode", _webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            if (dataReader.Read())
            {

                _webSiteCode = dataReader.GetValue(0).ToString();
                _code = dataReader.GetValue(1).ToString();
                _description = dataReader.GetValue(2).ToString();
                _webShopRegisterCode = dataReader.GetValue(3).ToString();

                _showWhenNotLoggedIn = false;
                if (dataReader.GetValue(4).ToString() == "1") _showWhenNotLoggedIn = true;

                _showClearButton = false;
                if (dataReader.GetValue(5).ToString() == "1") _showClearButton = true;

            }

            dataReader.Close();

        }

        public bool showCart
        {
            get
            {
                if ((System.Web.HttpContext.Current.Request.IsAuthenticated) && (infojetContext.userSession != null) && (infojetContext.userSession.webUserAccount.allowOrdering)) return true;
                if ((!System.Web.HttpContext.Current.Request.IsAuthenticated) && (_showWhenNotLoggedIn)) return true;
                return false;
            }
        }

        public bool showClearButton
        {
            get
            {
                return _showClearButton;
            }
        }

        public string webShopRegisterCode
        {
            get
            {
                return _webShopRegisterCode;
            }
        }

        
        public string getFormatedTotalCartAmount()
        {
            return infojetContext.cartHandler.getFormatedTotalCartAmount();
        }

        public string getFormatedTotalCartQuantity()
        {
            float totalQty = infojetContext.cartHandler.getTotalCartQuantity();
            if (totalQty > 0)
            {
                return totalQty + " " + infojetContext.translate("PCE");
            }

            return "";
        }

        public CartItemCollection getCartLines(WebPageLine webPageLine)
        {
            WebCartLines webCartLines = new WebCartLines(infojetContext);
            
            DataSet cartDataSet = null;

            cartDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

            CartItemCollection cartItemCollection = new CartItemCollection();

            if (cartDataSet.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                while (i < cartDataSet.Tables[0].Rows.Count)
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);

                    CartItem cartItem = new CartItem(webCartLine);

                    Item item = Item.get(infojetContext, cartItem.itemNo);

                    //ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    //cartItem.description = itemTranslation.description;
                    cartItem.description = cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString();
                    if (cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString() != "") cartItem.description = cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString();

                    cartItem.unitPrice = webCartLine.unitPrice;

                    cartItem.amount = cartItem.unitPrice * cartItem.quantity;

                    if (infojetContext.webSite.showPriceInclVAT)
                    {
                        cartItem.formatedAmount = infojetContext.systemDatabase.formatCurrency(cartItem.amount * item.getVatFactor());
                    }
                    else
                    {
                        cartItem.formatedAmount = infojetContext.systemDatabase.formatCurrency(cartItem.amount);
                    }

                    cartItemCollection.Add(cartItem);

                    i++;
                }



            }

            return cartItemCollection;

        }

        public void updateQuantity(int entryNo, float quantity)
        {
            if (quantity <= 0)
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Web Site Code] = @webSiteCode");
                databaseQuery.addIntParameter("entryNo", entryNo);
                databaseQuery.addStringParameter("webSiteCode", _webSiteCode, 20);
                databaseQuery.execute();
            }
            else
            {

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Quantity] = @quantity WHERE [Entry No_] = @entryNo AND [Web Site Code] = @webSiteCode");
                databaseQuery.addDecimalParameter("quantity", quantity);
                databaseQuery.addIntParameter("entryNo", entryNo);
                databaseQuery.addStringParameter("webSiteCode", _webSiteCode, 20);
                databaseQuery.execute();
            }
        }

        public void deleteEntry(int entryNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Web Site Code] = @webSiteCode");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("webSiteCode", _webSiteCode, 20);
            databaseQuery.execute();
        }
    }
}
