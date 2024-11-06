using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Navipro.Infojet.Lib;
using System.Web.UI;

namespace Navipro.Infojet.ObjectSources
{
    [DataObject]
    public class MenuDataSource
    {
        private Navipro.Infojet.Lib.Infojet infojetContext;
        private string webMenuCode;

        public MenuDataSource(Navipro.Infojet.Lib.Infojet infojetContext, string webMenuCode)
        {
            this.infojetContext = infojetContext;
            this.webMenuCode = webMenuCode;

        }

        public IHierarchicalEnumerable getMenuItems()
        {
            WebMenu webMenu = new WebMenu(infojetContext.systemDatabase, infojetContext.webSite.code, webMenuCode);

            return webMenu.getMenuItems(infojetContext);

        }
    }
}
