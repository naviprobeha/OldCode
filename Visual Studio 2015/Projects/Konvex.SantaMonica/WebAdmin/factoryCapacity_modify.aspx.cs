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

namespace WebAdmin
{
	/// <summary>
	/// Summary description for linejournals.
	/// </summary>
	public class factoryCapacity_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

		protected int currentYear;
		protected int currentWeek;
		protected DateTime firstDay;

		protected Hashtable capacityTable;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "consumerCapacity.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			Factories factories = new Factories();
			currentFactory = factories.getEntry(database, Request["factoryNo"]);

			firstDay = DateTime.Parse(Request["firstDay"]);

			currentYear = firstDay.Year;
			currentWeek = CalendarHelper.GetWeek(firstDay);
	
			FactoryCapacities factoryCapacities = new FactoryCapacities();
			capacityTable = factoryCapacities.getHashtable(database, currentFactory.no, firstDay, firstDay.AddDays(6));
			

			if (Request["command"] == "save")
			{
				int i = 0;
				while (i < 7)
				{
					int h = 0;
					while (h < 24)
					{
						float capacityValue = 0;
						try
						{
							capacityValue = float.Parse(Request["capacity_"+i+"_"+h]);
						}
						catch(Exception ex)
						{
							if (ex.Message != "") {}
						}

						FactoryCapacity factoryCapacity = factoryCapacities.getEntry(database, Request["factoryNo"], new DateTime(firstDay.AddDays(i).Year, firstDay.AddDays(i).Month, firstDay.AddDays(i).Day, h, 0, 0));
						if (factoryCapacity == null)
						{
							factoryCapacity = new FactoryCapacity();
						}

						factoryCapacity.factoryNo = Request["factoryNo"];
						factoryCapacity.date = firstDay.AddDays(i);
						factoryCapacity.timeOfDay = new DateTime(1754, 1, 1, h, 0, 0);
						factoryCapacity.plannedCapacity = capacityValue;
						factoryCapacity.save(database);

						h++;
					}
				
					i++;				
				}

				Response.Redirect("factoryCapacity.aspx?factoryNo="+currentFactory.no+"&firstDay="+firstDay.ToString("yyyy-MM-dd"));
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
