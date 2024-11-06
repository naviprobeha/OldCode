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
	public class consumerCapacity_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Consumer currentConsumer;

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

			Consumers consumers = new Consumers();
			currentConsumer = consumers.getEntry(database, Request["consumerNo"]);

			firstDay = DateTime.Parse(Request["firstDay"]);

			currentYear = firstDay.Year;
			currentWeek = CalendarHelper.GetWeek(firstDay);
	
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			capacityTable = consumerCapacities.getHashtable(database, currentConsumer.no, firstDay, firstDay.AddDays(6));
			

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

						ConsumerCapacity consumerCapacity = consumerCapacities.getEntry(database, Request["consumerNo"], new DateTime(firstDay.AddDays(i).Year, firstDay.AddDays(i).Month, firstDay.AddDays(i).Day, h, 0, 0));
						if (consumerCapacity == null)
						{
							consumerCapacity = new ConsumerCapacity();
						}

						consumerCapacity.consumerNo = Request["consumerNo"];
						consumerCapacity.date = firstDay.AddDays(i);
						consumerCapacity.timeOfDay = new DateTime(1754, 1, 1, h, 0, 0);
						consumerCapacity.plannedCapacity = capacityValue;
						consumerCapacity.save(database);

						h++;
					}
				
					i++;				
				}

				ConsumerInventories consumerInventories = new ConsumerInventories();
				consumerInventories.recalculateInventories(database, Request["consumerNo"], firstDay, firstDay.AddDays(8));


				Response.Redirect("consumerCapacity.aspx?consumerNo="+currentConsumer.no+"&firstDay="+firstDay.ToString("yyyy-MM-dd"));
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
