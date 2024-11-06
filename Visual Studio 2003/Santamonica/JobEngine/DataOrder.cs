using System;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for DataRoute.
	/// </summary>
	public class DataOrder
	{
		public string orderNumber;
		public string customerName;
		public string routeName;
		public int sequence;
		public int travelTime;
		public int travelDistance;
		public string departureFactoryCode;
		public string arrivalFactoryCode;
		public int totalDistance;
		public int totalTime;
		public string factoryCode;

		public DataOrder(string textLine)
		{
			//
			// TODO: Add constructor logic here
			//

			string recordType = FileUtility.getNextField(ref textLine);
			this.orderNumber = FileUtility.getNextField(ref textLine);
			this.customerName = FileUtility.getNextField(ref textLine);
			this.routeName = FileUtility.getNextField(ref textLine);

			string sequenceString = FileUtility.getNextField(ref textLine);
			this.sequence = int.Parse(sequenceString);

			string travelTimeString = FileUtility.getNextField(ref textLine).Replace(".",",");
			this.travelTime = int.Parse(travelTimeString);

			string travelDistanceString = FileUtility.getNextField(ref textLine).Replace(".",",");
			this.travelDistance = (int)float.Parse(travelDistanceString);


			this.departureFactoryCode = FileUtility.getNextField(ref textLine);
			this.arrivalFactoryCode = FileUtility.getNextField(ref textLine);
			
			string totalDistanceString = FileUtility.getNextField(ref textLine).Replace(".",",");
			this.totalDistance = (int)float.Parse(totalDistanceString);

			string totalTimeString = FileUtility.getNextField(ref textLine).Replace(".",",");
			this.totalTime = int.Parse(totalTimeString);

			this.factoryCode = FileUtility.getNextField(ref textLine);

		}

	}
}
