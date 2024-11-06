using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.WebAdmin
{
	/// <summary>
	/// Summary description for mobileusers_modify.
	/// </summary>
	public class mobileusers_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;


		protected MobileUser currentMobileUser;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if (Session["current.user.operator"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];

			currentOperator = (UserOperator)Session["current.user.operator"];
			currentOrganization = (Organization)Session["current.user.organization"];

			if (!currentOperator.checkSecurity(database, currentOrganization, "mobileusers.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			CustomerShipAddresses custShipAddresses = new CustomerShipAddresses();



			if (Request["entryNo"] != null)
			{
				MobileUsers mobileUsers = new MobileUsers();
				
				currentMobileUser = mobileUsers.getEntry(database, currentOrganization.no, int.Parse(Request["entryNo"]));
				if (currentMobileUser == null)
				{
					currentMobileUser = new MobileUser(currentOrganization.no);
				}
				
			}
			else
			{
				currentMobileUser = new MobileUser(currentOrganization.no);
			}

			if (Request["command"] != null)
			{
				currentMobileUser.name = Request["name"];	
				if (Request["changePassword"] == "on")
				{
					currentMobileUser.passWord = Request["password"];
				}
			}


			if (Request["command"] == "save")
			{
			
				currentMobileUser.save(database);

				Response.Redirect("mobileusers.aspx");
			}

			if (Request["command"] == "delete")
			{
				currentMobileUser.delete(database);

				Response.Redirect("mobileusers.aspx");
			}
		
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
