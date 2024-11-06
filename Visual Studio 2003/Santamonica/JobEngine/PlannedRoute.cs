using System;
using System.Collections;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for PlanedRoute.
	/// </summary>
	public class PlannedRoute
	{
		public int lineJournalEntryNo;
		public int calculatedDistance;
		public int endingDistance;
		public int endingTime;
		public int totalDistance;
		public int totalTime;
		public string agentStorageGroup;
		public string arrivalFactoryNo;

		public ArrayList orders;

		public PlannedRoute()
		{
			//
			// TODO: Add constructor logic here
			//
			this.orders = new ArrayList();
		}

		public void addOrder(LineOrder lineOrder)
		{
			this.orders.Add(lineOrder);
		}

	}
}
