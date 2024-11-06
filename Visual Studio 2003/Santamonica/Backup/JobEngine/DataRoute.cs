using System;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for DataRoute.
	/// </summary>
	public class DataRoute
	{
		public string routeName;
		public string routeNumber;
		public string arrivalFactoryCode;
		public string agentCode;

		public DataRoute(string textLine)
		{
			//
			// TODO: Add constructor logic here
			//
			
			string recordType = FileUtility.getNextField(ref textLine);
			this.routeName = FileUtility.getNextField(ref textLine);
			this.routeNumber = FileUtility.getNextField(ref textLine);
			this.arrivalFactoryCode = FileUtility.getNextField(ref textLine);
			this.agentCode = FileUtility.getNextField(ref textLine);
		}

	}
}
