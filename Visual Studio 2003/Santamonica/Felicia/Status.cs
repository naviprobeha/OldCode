using System;
using System.Data;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Status.
	/// </summary>
	public class Status
	{
		public int status;
		public int rt90x;
		public int rt90y;
		public float speed;
		public float heading;
		public float height;
		public int tripMeter;
		public double tripMeterDetails;
		public DateTime lastUpdated;
		public string mobileUserName;
		public string containerNo;

		public DateTime lastSaveTime;

		private DataStatus dataStatus;
		private SmartDatabase smartDatabase;

		public Status(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			rt90x = 0;
			rt90y = 0;
			heading = 0;
			height = 0;
			
			lastUpdated = System.DateTime.Now;
			lastSaveTime = System.DateTime.Now;
			containerNo = "";

			this.smartDatabase = smartDatabase;
			this.dataStatus = new DataStatus(smartDatabase);
			this.tripMeter = dataStatus.tripMeter;
			this.tripMeterDetails = (double)dataStatus.tripMeter * 1000;
		}

		public string getStatusText()
		{
			if (status == 0) return "Utstämplad";
			if (status == 1) return "Instämplad";
			if (status == 2) return "På rast";
			return "";
		}

		public DataStatus getData()
		{
			return dataStatus;
		}
		
		public void loadContainer()
		{
			string proposeContainerNo = containerNo;

			if (proposeContainerNo == "")
			{
				if (dataStatus.containerNo != "") proposeContainerNo = dataStatus.containerNo;
			}

			Keyboard keyboard = new Keyboard(30);
			keyboard.setStartTab(1);
			keyboard.setHeaderText("Ange containernr");
			keyboard.setInputString(proposeContainerNo);
			keyboard.ShowDialog();

			if (keyboard.getInputString() == "")
			{
				MessageBox.Show("Containernr får inte vara blankt.", "Container", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				return;
			}

			containerNo = keyboard.getInputString();

			
			keyboard.Dispose();

			if (containerNo != dataStatus.containerNo)
			{
				dataStatus.containerNo = containerNo;
				dataStatus.arrivalTime = dataStatus.getDefaultArrivalTime();
				dataStatus.commit();

				DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
				dataContainerEntry.containerNo = containerNo;
				dataContainerEntry.entryDateTime = DateTime.Now;
				dataContainerEntry.type = 0;  // Load Container
				dataContainerEntry.positionX = this.rt90x;
				dataContainerEntry.positionY = this.rt90y;
				dataContainerEntry.locationType = 1;
				dataContainerEntry.locationCode = getOrganizationLocation();
				dataContainerEntry.commit();

				DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
				dataContainerLoads.loadContainer(containerNo);
			}
			
		}


		public void unLoadContainer()
		{
			DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
			dataContainerEntry.containerNo = dataStatus.containerNo;
			dataContainerEntry.entryDateTime = DateTime.Now;
			dataContainerEntry.type = 1;  // UnLoad Container
			dataContainerEntry.positionX = this.rt90x;
			dataContainerEntry.positionY = this.rt90y;
			dataContainerEntry.locationType = 1;
			dataContainerEntry.locationCode = getOrganizationLocation();
			dataContainerEntry.commit();

			dataStatus.containerNo = "";
			dataStatus.arrivalTime = dataStatus.getDefaultArrivalTime();
			dataStatus.commit();

			DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
			dataContainerLoads.unloadContainer(containerNo);

			this.containerNo = "";
		}

		public void setArrivalTime(DateTime arrivalTime)
		{
			DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
			dataContainerEntry.containerNo = dataStatus.containerNo;
			dataContainerEntry.entryDateTime = DateTime.Now;
			dataContainerEntry.type = 2;  // Set Arrival Time
			dataContainerEntry.positionX = this.rt90x;
			dataContainerEntry.positionY = this.rt90y;
			dataContainerEntry.estimatedArrivalTime = arrivalTime;
			dataContainerEntry.locationType = 1;
			dataContainerEntry.locationCode = getOrganizationLocation();
			dataContainerEntry.commit();

			dataStatus.arrivalTime = arrivalTime;
			dataStatus.commit();

		}

		public string getOrganizationLocation()
		{
			string location = "";

			DataOrganizationLocations dataOrganizationLocations = new DataOrganizationLocations(smartDatabase);
			if (dataOrganizationLocations.count() > 1)
			{
				OrganizationLocationList orgLocationList = new OrganizationLocationList(smartDatabase);
				orgLocationList.ShowDialog();

				location = orgLocationList.getOrganizationLocation();

				orgLocationList.Dispose();
			}
			else
			{
				if (dataOrganizationLocations.count() > 0)
				{
					DataSet dataSet = dataOrganizationLocations.getDataSet();
					location = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
				}
			}

			return location;
		}

		public int countContainers()
		{
			DataContainerLoads dataContainerLoads = new DataContainerLoads(smartDatabase);
			return dataContainerLoads.countContainers();

		}

		public void updatePosition(int positionX, int positionY, int height)
		{		
			if ((this.speed > 0) && (this.status == 1))
			{
				int diffX = positionX - this.rt90x;
				int diffY = positionY - this.rt90y;

				double vector = Math.Sqrt((diffX*diffX) + (diffY*diffY));

				this.tripMeterDetails = this.tripMeterDetails + vector;
				this.tripMeter = (int)Math.Round(this.tripMeterDetails / 1000, 0);

			}
	
			this.rt90x = positionX;
			this.rt90y = positionY;
			this.height = height;
		
			this.lastUpdated = DateTime.Now;

			if (DateTime.Now > lastSaveTime.AddMinutes(1))
			{
				dataStatus.tripMeter = this.tripMeter;
				dataStatus.commit();

				lastSaveTime = DateTime.Now;

			}

		}

		public void updateSpeed(int speed, int heading)
		{
			this.speed = speed;
			this.heading = heading;

		}

		public void clearTripMeter()
		{
			this.tripMeter = 0;
			this.tripMeterDetails = 0;
			dataStatus.tripMeter = 0;
			dataStatus.commit();
		}			

	}
}
