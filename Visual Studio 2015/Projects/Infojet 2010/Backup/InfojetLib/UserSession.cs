using System;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for UserSession.
	/// </summary>
	public class UserSession
	{
		private Database database;

		
		public WebUserAccount webUserAccount;
		public Customer customer;
		public Contact contact;
		public string clientIp;
		public string clientAgent;

		private WebSite webSite;

		public UserSession(Database database, WebSite webSite, WebUserAccount webUserAccount)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webUserAccount = webUserAccount;
			this.contact = new Contact(database, webUserAccount.contactNo);

			this.webSite = webSite;
			
		}


		public void refresh()
		{
			customer.refresh();
			contact.refresh();
			webUserAccount.refresh();

		}


        public string getProfilePageUrl(Infojet infojetContext)
        {
            WebPage myProfile = infojetContext.webSite.getWebPageByCategory("MY PROFILE", this);
            if (myProfile != null)
            {
                return myProfile.getUrl();
            }
            return "";

        }
	}
}
