using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class ItemConfiguration : System.Web.UI.UserControl
    {
        private string _itemNo = "";
        protected WebItemConfigHeader webItemConfigHeader;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_itemNo == "") _itemNo = Request["itemNo"];

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            webItemConfigHeader = WebItemConfigHeader.getConfiguration(infojet, _itemNo);
            if (webItemConfigHeader != null)
            {
                configPanel.Controls.AddAt(0, webItemConfigHeader.getFormPanel());
            }
            else
            {
                configPanel.Visible = false;
            }

        }

        public string itemNo
        {
            set
            {
                _itemNo = value;
            }
        }
    }
}