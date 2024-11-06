using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace Navipro.SantaMonica.Goldfinger.WebService
{
	/// <summary>
	/// Summary description for ScaleRunner.
	/// </summary>
	public class ScaleRunner : System.Web.Services.WebService
	{
		private Navipro.SantaMonica.Goldfinger.ScaleRunner scaleRunner;

		public ScaleRunner()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			scaleRunner = new Navipro.SantaMonica.Goldfinger.ScaleRunner();
			scaleRunner.init();

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
		public DataSet getUnSentFactoryLineJournals(string factoryCode)
		{
			DataSet lineJournalDataSet = scaleRunner.getUnSentFactoryLineJournals(factoryCode);
			scaleRunner.dispose();
			return lineJournalDataSet;
		}

		[WebMethod]
		public DataSet getLineJournalOrders(int lineJournalEntryNo)
		{
			DataSet lineOrderDataSet = scaleRunner.getLineJournalOrders(lineJournalEntryNo);
			scaleRunner.dispose();
			return lineOrderDataSet;
		}


		[WebMethod]
		public DataSet getLineOrderContainers(int lineOrderEntryNo)
		{
			DataSet lineOrderContainerDataSet = scaleRunner.getLineOrderContainers(lineOrderEntryNo);
			scaleRunner.dispose();
			return lineOrderContainerDataSet;
		}

		[WebMethod]
		public DataSet getContainerEntry(string containerNo)
		{
			DataSet containerDataSet = scaleRunner.getContainer(containerNo);
			scaleRunner.dispose();
			return containerDataSet;
		}


		[WebMethod]
		public void setLineJournalAsSent(int lineJournalEntryNo)
		{
			scaleRunner.setLineJournalAsSent(lineJournalEntryNo);
			scaleRunner.dispose();
		}

		[WebMethod]
		public void setLineOrderContainerWeight(int lineOrderEntryNo, string containerNo, float weight, DateTime scaledDateTime)
		{
			scaleRunner.setLineOrderContainerWeight(lineOrderEntryNo, containerNo, weight, scaledDateTime);
			scaleRunner.dispose();
		}

		[WebMethod]
		public DataSet getScaleEntries(string factoryCode, int status)
		{
			DataSet dataSet = scaleRunner.getScaleEntries(factoryCode, status);
			scaleRunner.dispose();

			return dataSet;
		}

		[WebMethod]
		public void createScaleEntry(string factoryCode, int type, DataSet transactionDataSet)
		{
			scaleRunner.createScaleEntry(factoryCode, type, transactionDataSet);
			scaleRunner.dispose();
		}

		[WebMethod]
		public void createScaleEntry2(string factoryCode, int type, DataSet transactionDataSet, DataSet transactionSubDataSet)
		{
			scaleRunner.createScaleEntry(factoryCode, type, transactionDataSet, transactionSubDataSet);
			scaleRunner.dispose();
		}

		[WebMethod]
		public string[] getMissingScaleEntries(string factoryCode)
		{
			string[] array = scaleRunner.getMissingScaleEntries(factoryCode);
			scaleRunner.dispose();

			return array;
		}

		[WebMethod]
		public string[] getUnfinishedScaleEntries(string factoryCode)
		{
			string[] array = scaleRunner.getUnfinishedScaleEntries(factoryCode);
			scaleRunner.dispose();

			return array;
		}

		[WebMethod]
		public DataSet getContainersToScale(string factoryCode)
		{
			DataSet dataSet = scaleRunner.getContainersToScale(factoryCode);
			scaleRunner.dispose();

			return dataSet;
		}

		[WebMethod]
		public DataSet getLineOrder(int lineOrderEntry)
		{
			DataSet dataSet = scaleRunner.getLineOrder(lineOrderEntry);
			scaleRunner.dispose();

			return dataSet;
		}

		[WebMethod]
		public DataSet getLineJournal(int lineJournalEntry, string factoryCode)
		{
			DataSet dataSet = scaleRunner.getLineJournal(lineJournalEntry, factoryCode);
			scaleRunner.dispose();

			return dataSet;
		}

		[WebMethod]
		public void setContainerAsSent(int lineOrderEntryNo, int lineOrderContainerEntryNo)
		{
			scaleRunner.setContainerAsSent(lineOrderEntryNo, lineOrderContainerEntryNo);
			scaleRunner.dispose();
		}

		[WebMethod]
		public DataSet getLineJournalEntries(int lineJournalEntryNo)
		{
			DataSet dataSet = scaleRunner.getLineJournalEntries(lineJournalEntryNo);
			scaleRunner.dispose();
			return dataSet;
		}
	}
}
