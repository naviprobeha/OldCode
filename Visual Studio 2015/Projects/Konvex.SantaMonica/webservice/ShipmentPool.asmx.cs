using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace SantaMonica
{
	/// <summary>
	/// Summary description for ShipmentPool.
	/// </summary>
	///
	[WebService(Namespace="http://santamonica.navipro.se/shipmentpool")]
	public class ShipmentPool : System.Web.Services.WebService
	{
		private Navipro.SantaMonica.Goldfinger.ShipmentPool shipmentPool;

		public ShipmentPool()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			shipmentPool = new Navipro.SantaMonica.Goldfinger.ShipmentPool();
			shipmentPool.init();

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
		public DataSet getShipments(string password, DateTime fromDate, DateTime toDate)
		{
			DataSet dataSet = shipmentPool.getShipments(password, fromDate, toDate);
			shipmentPool.dispose();
			return dataSet;
		}

		[WebMethod]
		public DataSet getShipment(string password, string no)
		{
			DataSet dataSet = shipmentPool.getShipment(password, no);
			shipmentPool.dispose();
			return dataSet;
		}

		[WebMethod]
		public DataSet getShipmentLines(string password, string no)
		{
			DataSet dataSet = shipmentPool.getShipmentLines(password, no);
			shipmentPool.dispose();
			return dataSet;
		}

		[WebMethod]
		public DataSet getShipmentLineIds(string password, string no, int shipmentLineEntryNo)
		{
			DataSet dataSet = shipmentPool.getShipmentLineIds(password, no, shipmentLineEntryNo);
			shipmentPool.dispose();
			return dataSet;
		}

		[WebMethod]
		public void markShipmentIDAsSent(int idEntryNo, string comments)
		{
			shipmentPool.markShipmentIDAsSent(idEntryNo, comments);
			shipmentPool.dispose();
		}
	}
}
