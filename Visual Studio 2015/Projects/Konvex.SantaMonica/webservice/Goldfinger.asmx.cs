using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Navipro.SantaMonica.Goldfinger;
using Navipro.SantaMonica.Common;
namespace Navipro.SantaMonica.Goldfinger.WebService
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	[WebService(Namespace="http://santamonica.navipro.se/goldfinger")]
	public class Goldfinger : System.Web.Services.WebService
	{
		private Navipro.SantaMonica.Goldfinger.Goldfinger goldfinger;

		public Goldfinger()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			goldfinger = new Navipro.SantaMonica.Goldfinger.Goldfinger();
			goldfinger.init();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status)
		{
			int count = goldfinger.reportStatus(agentCode, rt90x, rt90y, heading, speed, height, status, "");			
			goldfinger.dispose();
			return count;

		}

		[WebMethod]
		public int reportStatusEx(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName)
		{
			int count = goldfinger.reportStatus(agentCode, rt90x, rt90y, heading, speed, height, status, userName);			
			goldfinger.dispose();
			return count;

		}

		[WebMethod]
		public int reportStatusTrip(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
		{
			int count = goldfinger.reportStatus(agentCode, rt90x, rt90y, heading, speed, height, status, userName, tripMeter);			
			goldfinger.dispose();
			return count;

		}

		[WebMethod]
		public DataSet getSynchEntry(string agentCode, ref int type, ref int action, ref string primaryKey, ref int synchEntryNo)
		{
			DataSet dataSet = goldfinger.getSynchRecord(agentCode, ref type, ref action, ref primaryKey, ref synchEntryNo);
			goldfinger.dispose();
			return dataSet;

		}

		[WebMethod]
		public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY)
		{
			goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY);
			goldfinger.dispose();
		}

		[WebMethod]
		public void setShipOrderStatusEx(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
		{
			goldfinger.setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime);
			goldfinger.dispose();
		}

		[WebMethod]
		public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
		{
			goldfinger.setLineOrderStatus(agentCode, no, status, positionX, positionY, shipTime, 0);
			goldfinger.dispose();
		}

		[WebMethod]
		public void setLineOrderStatusEx(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime)
		{
			goldfinger.setLineOrderStatus(agentCode, no, status, positionX, positionY, shipTime, loadWaitTime);
			goldfinger.dispose();
		}

		[WebMethod]
		public void createShipment(string agentCode, DataSet shipmentHeaderDataSet, DataSet shipmentLinesDataSet, DataSet shipmentLineIdsDataSet)
		{
			goldfinger.createShipment(agentCode, shipmentHeaderDataSet, shipmentLinesDataSet, shipmentLineIdsDataSet);
			goldfinger.dispose();
		}

		[WebMethod]
		public void createOrder(string agentCode, DataSet shipmentHeaderDataSet, DataSet shipmentLinesDataSet, DataSet shipmentLineIdsDataSet)
		{
			goldfinger.createOrder(agentCode, shipmentHeaderDataSet, shipmentLinesDataSet, shipmentLineIdsDataSet);
			goldfinger.dispose();
		}
		
		[WebMethod]
		public void reportLineJournal(string agentCode, DataSet lineJournalDataSet)
		{
			goldfinger.reportLineJournal(agentCode, lineJournalDataSet);
			goldfinger.dispose();
		}

		[WebMethod]
		public void setMessageStatus(string agentCode, int messageEntryNo, int status)
		{
			goldfinger.setMessageStatus(agentCode, messageEntryNo, status);
			goldfinger.dispose();
		}

		[WebMethod]
		public void ackSynchEntry(int synchEntryNo)
		{
			goldfinger.ackSynchRecord(synchEntryNo);
			goldfinger.dispose();
		}

		[WebMethod]
		public void assignShipOrder(string agentCode, string no, string newAgentCode)
		{
			goldfinger.assignShipOrder(agentCode, no, newAgentCode);
			goldfinger.dispose();
		}

		[WebMethod]
		public void reportError(string agentCode, string message)
		{
			goldfinger.reportError(agentCode, message);
			goldfinger.dispose();
		}

		[WebMethod]
		public void createContainerEntry(string agentCode, DataSet containerEntryDataSet)
		{
			goldfinger.createContainerEntry(agentCode, containerEntryDataSet);
			goldfinger.dispose();
		}

		[WebMethod]
		public void setFactoryOrderStatus(string agentCode, DataSet factoryOrderDataSet)
		{
			goldfinger.setFactoryOrderStatus(agentCode, factoryOrderDataSet);
			goldfinger.dispose();
		}

		[WebMethod]
		public string getPriceUpdateItemNo(string agentCode)
		{
			string itemNo = goldfinger.getPriceUpdateItemNo(agentCode);
			goldfinger.dispose();
			return itemNo;
		}

		[WebMethod]
		public void acknowledgePriceUpdate(string agentCode, string itemNo, float checksum)
		{
			goldfinger.acknowledgePriceUpdate(agentCode, itemNo, checksum);
			goldfinger.dispose();
		}


	}
}
