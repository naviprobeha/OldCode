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
	/// Summary description for linejournals_view.
	/// </summary>
	public class linejournals_view : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DataSet lineOrderDataSet;
		protected DataSet containerEntryDataSet;

		protected LineJournal currentLineJournal;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "linejournals.aspx"))
			{
				if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOperation.aspx"))
				{
					Response.Redirect("default.htm");				
				}
				else
				{
					Response.Redirect("factoryOperation_view.aspx?lineJournalNo="+Request["lineJournalNo"]);				
				}
			}



			if (Request["lineJournalNo"] != null)
			{
				LineJournals lineJournals = new LineJournals();
				
				currentLineJournal = lineJournals.getEntry(database, Request["lineJournalNo"]);
				if (currentLineJournal == null)
				{
					currentLineJournal = new LineJournal();
				}
			
			}
			else
			{
				currentLineJournal = new LineJournal();
			}


			if (Request["command"] == "toggleForcedAssignment")
			{
				if (currentLineJournal.forcedAssignment) 
					currentLineJournal.forcedAssignment = false;
				else
					currentLineJournal.forcedAssignment = true;

				currentLineJournal.status = 1;
				currentLineJournal.save(database);
			}

			if (Request["command"] == "updateArrivalTime")
			{
				
				currentLineJournal.recalculateArrivalTime(database);
			}

			if (Request["command"] == "loadOrders")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					currentLineJournal.loadAllOrders(database, 1, currentOperator.userId);
				}
			}

			if (Request["command"] == "unloadOrders")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					currentLineJournal.setJournalUnloaded(database, 1, currentOperator.userId);
					currentLineJournal.sentToFactory = false;
					currentLineJournal.save(database);
				}
			}

			if (Request["command"] == "moveToDay")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					currentLineJournal.shipDate = DateTime.Today;
					currentLineJournal.save(database);
				}
			}

			if (Request["command"] == "reportJournal")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					currentLineJournal.setJournalReported(database);
				}
			}

			if (Request["command"] == "setInvoiceStatus")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					if (currentLineJournal.invoiceReceived)
					{
						currentLineJournal.setInvoiceStatus(database, false);
					}
					else
					{
						currentLineJournal.setInvoiceStatus(database, true);
					}
				}
			}

			if (Request["command"] == "openJournal")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					if (Request["sendJournal"] == "1")
					{
						currentLineJournal.openJournal(database, true);
					}
					else
					{
						currentLineJournal.openJournal(database, false);
					}
				}
			}

			if (Request["command"] == "recalcTime")
			{
				this.recalculateArrivalTime(database, currentLineJournal);
			}

			LineOrders lineOrders = new LineOrders();
			lineOrderDataSet = lineOrders.getJournalDataSet(database, currentLineJournal.entryNo);

			Navipro.SantaMonica.Common.ContainerEntries containerEntries = new ContainerEntries();
			containerEntryDataSet = containerEntries.getDocumentDataSet(database, 2, currentLineJournal.entryNo.ToString());

		}


		public void recalculateArrivalTime(Database database, LineJournal lineJournal)
		{
			if (lineJournal.isReadyToSend(database))
			{
				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);
			
				//Get departure time

				DateTime departureDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime firstLoadingDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime arrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime calculatedArrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);

				int i = 0;
				while(i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

					if (firstLoadingDateTime.Year == 1753) firstLoadingDateTime = lineOrder.confirmedToDateTime;
					if (lineOrder.confirmedToDateTime < firstLoadingDateTime) firstLoadingDateTime = lineOrder.confirmedToDateTime;

					i++;
				}

				if (firstLoadingDateTime < DateTime.Now) firstLoadingDateTime = DateTime.Now;


				// Find container lead time
			
				Organizations organizations = new Organizations();
				Organization organization = organizations.getOrganization(database, lineJournal.organizationNo);

				int qtyContainers = lineJournal.countContainers(database);
				int containerLeadTime = qtyContainers * organization.containerLoadTime;

				System.Collections.ArrayList lineJournalList = null;

				// Check loaded lineOrders

				i = 0;
				bool prevPickedUp = true;

				if (lineOrderDataSet.Tables[0].Rows.Count > 0)
				{
					// Set departure time
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[0]);
					int firstTravelTime = lineOrder.travelTime;
					departureDateTime = firstLoadingDateTime.AddMinutes(firstTravelTime*-1);

					//Console.WriteLine("Startdate: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));

					// Planning by Departure and Arrival time
					LineJournals lineJournals = new LineJournals();
					lineJournalList = lineJournals.getPlanningJournalsByArrivalTime(database, lineJournal.agentCode);

					//Console.WriteLine("Orders loaded: "+this.countOrdersLoaded(database));
					if (lineJournal.countOrdersLoaded(database) == 0)
					{
						int j = 0;
						while (j < lineJournalList.Count)
						{
							LineJournal planLineJournal = (LineJournal)lineJournalList[j];
							Response.Write("Comparing to route: "+planLineJournal.entryNo+"<br>");
							//Console.WriteLine("Comparing to route: "+planLineJournal.entryNo);
							if (((planLineJournal.entryNo < lineJournal.entryNo) && (planLineJournal.arrivalDateTime.Year >= 2000)) || (planLineJournal.countOrdersLoaded(database) > 0))
							{
								if (planLineJournal.entryNo > lineJournal.entryNo)
								{
									planLineJournal.recalculateArrivalTime(database);
								}

								Response.Write("Applying route: "+planLineJournal.entryNo+"<br>");
								//Console.WriteLine("Applying route: "+planLineJournal.entryNo);
								departureDateTime = planLineJournal.arrivalDateTime.AddMinutes(containerLeadTime);
							}
							j++;
						}
					}

					Response.Write("Startdate: "+departureDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");
					//Console.WriteLine("Startdate: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));

					// Set arrival time
					firstLoadingDateTime = departureDateTime.AddMinutes(firstTravelTime);
					arrivalDateTime = departureDateTime.AddMinutes(containerLeadTime + lineJournal.totalTime);
					calculatedArrivalDateTime = departureDateTime;


					while(i < lineOrderDataSet.Tables[0].Rows.Count)
					{
						lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

						int lineOrderContainers = lineOrder.countContainers(database);

						if ((lineOrder.status == 7) || (lineOrder.status == 10))
						{

							if (prevPickedUp)
							{
								if (lineOrder.closedDateTime.Year < 2000) lineOrder.closedDateTime = new DateTime(lineOrder.shipDate.Year, lineOrder.shipDate.Month, lineOrder.shipDate.Day, lineOrder.closedDateTime.Hour, lineOrder.closedDateTime.Minute, lineOrder.closedDateTime.Second);

								if (departureDateTime.AddMinutes(lineOrder.travelTime + (organization.containerLoadTime*lineOrderContainers)) > lineOrder.closedDateTime) departureDateTime = lineOrder.closedDateTime.AddMinutes((lineOrder.travelTime + (organization.containerLoadTime*lineOrderContainers)) * -1);
								calculatedArrivalDateTime = lineOrder.closedDateTime;
								Response.Write("DepartureDateTime: "+departureDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");
							}
							else
							{
								calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineOrder.travelTime + (lineOrderContainers*organization.containerLoadTime));
							}
						}
						else
						{
							prevPickedUp = false;

							calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineOrder.travelTime + (lineOrderContainers*organization.containerLoadTime));
						}


						i++;

					}

					calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineJournal.endingTravelTime);
				}

				Response.Write("Setting start: "+departureDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");
				Response.Write("Setting end  : "+calculatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");

				//Console.WriteLine("Setting start: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));
				//Console.WriteLine("Setting end  : "+calculatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm"));


				lineJournal.departureDateTime = departureDateTime;
				lineJournal.arrivalDateTime = calculatedArrivalDateTime;
				lineJournal.save(database);

				//Console.WriteLine("Set start: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));
				//Console.WriteLine("Set end  : "+arrivalDateTime.ToString("yyyy-MM-dd HH:mm"));


				// Re-schedule other routes
				if (lineJournalList != null)
				{
					LineJournals lineJournals = new LineJournals();

					int k = 0;
					while (k < lineJournalList.Count)
					{
						LineJournal planLineJournal = (LineJournal)lineJournalList[k];
						planLineJournal = lineJournals.getEntry(database, planLineJournal.entryNo.ToString());

						if ((planLineJournal.entryNo > lineJournal.entryNo) && (planLineJournal.countOrdersLoaded(database) == 0))
						{							
							//Console.WriteLine("Planning route: "+planLineJournal.entryNo);
							planLineJournal.recalculateArrivalTime(database);
						}
						k++;
					}
				}

				//lineJournal.enqueueInventoryUpdate(database);
				lineJournal.updateEstimatedInventory(database);

			}
			else
			{
				lineJournal.departureDateTime = new DateTime(1753, 1, 1, 0, 0,0);
				lineJournal.arrivalDateTime = new DateTime(1753, 1, 1, 0, 0,0);
				lineJournal.save(database);

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
