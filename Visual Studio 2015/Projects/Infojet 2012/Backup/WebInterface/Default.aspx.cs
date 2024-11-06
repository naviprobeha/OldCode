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

namespace Navipro.Infojet.WebInterface
{
    public partial class _Default : System.Web.UI.Page
    {
        protected Navipro.Infojet.Lib.Infojet infojet;

        
        protected override PageStatePersister PageStatePersister
        {

            get
            {

                return new SessionPageStatePersister(this);

            }

        }
        

        protected void Page_Load(object sender, EventArgs e)
        {           
            infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.init();
            infojet.loadPage();

            pageForm.Action = Request.RawUrl;

            try
            {
                searchButton.Text = infojet.translate("SEARCH");
            }
            catch (Exception) { }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            infojet.performSearch(searchQueryBox.Text);

        }



 
    }
}