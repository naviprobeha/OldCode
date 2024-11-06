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
	public class consumerCapacity_generate : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Consumer currentConsumer;

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

			Consumers consumers = new Consumers();
			currentConsumer = consumers.getEntry(database, Request["consumerNo"]);


			if (Request["command"] == "generate")
			{
				DateTime fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), int.Parse(Request["fromDateDay"]));
				DateTime toDate = new DateTime(int.Parse(Request["toDateYear"]), int.Parse(Request["toDateMonth"]), int.Parse(Request["toDateDay"]));

				float capacityValue = 0;
				try
				{
					capacityValue = float.Parse(Request["capacity"]);
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}

				ConsumerCapacities consumerCapacities = new ConsumerCapacities();

				fromDate = fromDate.AddDays(-1);
				DateTime currentDate = fromDate;

				int i = 1;
				while (currentDate < toDate)
				{
					currentDate = fromDate.AddDays(i);

					bool createCapacity = false;
					if ((currentDate.DayOfWeek == DayOfWeek.Monday) && (Request["monday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Tuesday) && (Request["tuesday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Wednesday) && (Request["wednesday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Thursday) && (Request["thursday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Friday) && (Request["friday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Saturday) && (Request["saturday"] == "on")) createCapacity = true;
					if ((currentDate.DayOfWeek == DayOfWeek.Sunday) && (Request["sunday"] == "on")) createCapacity = true;

					if (createCapacity)
					{
						int h = int.Parse(Request["fromHour"]);
						while (h <= int.Parse(Request["toHour"]))
						{
						


							ConsumerCapacity consumerCapacity = consumerCapacities.getEntry(database, Request["consumerNo"], new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, h, 0, 0));
							if (consumerCapacity == null)
							{
								consumerCapacity = new ConsumerCapacity();
							}

							consumerCapacity.consumerNo = Request["consumerNo"];
							consumerCapacity.date = currentDate;
							consumerCapacity.timeOfDay = new DateTime(1754, 1, 1, h, 0, 0);
							consumerCapacity.plannedCapacity = capacityValue;
							consumerCapacity.save(database);

							h++;
						}
					}
				
					i++;				
				}

				ConsumerInventories consumerInventories = new ConsumerInventories();
				consumerInventories.recalculateInventories(database, Request["consumerNo"], fromDate, toDate.AddDays(1));

				Response.Redirect("consumerCapacity.aspx?consumerNo="+currentConsumer.no+"&firstDay="+fromDate.ToString("yyyy-MM-dd"));
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
