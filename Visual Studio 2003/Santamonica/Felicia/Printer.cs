using System;
using SerialNET;
using System.Data;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Printer.
	/// </summary>
	public class Printer
	{
		private PrinterInterface printerInterface;

		public Printer(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//

			DataSetup dataSetup = smartDatabase.getSetup();

			if (dataSetup.printer == "RW420")
			{
				printerInterface = new PrinterRW420(smartDatabase);
			}
			else
			{
				printerInterface = new PrinterPB42(smartDatabase);
			}
		

		}

		public bool init()
		{
			return printerInterface.init();
		}

		public void test()
		{
			printerInterface.test();
		}

		public void printShipment(DataShipmentHeader dataShipmentHeader)
		{
			printerInterface.printShipment(dataShipmentHeader);
		}

		public void printShipOrder(DataShipOrder dataShipOrder)
		{
			printerInterface.printShipOrder(dataShipOrder);
		}

		public void close()
		{
			printerInterface.close();
		}

		public void setCopy()
		{
			printerInterface.setCopy();
		}

		public void printPeriodReport(DateTime fromDate, DateTime toDate, string mobileUser)
		{
			printerInterface.printPeriodReport(fromDate, toDate, mobileUser);
		}


		public void printLineOrder(DataLineOrder dataLineOrder)
		{
			printerInterface.printLineOrder(dataLineOrder);
		}

		public void printFactoryOrder(DataFactoryOrder dataFactoryOrder)
		{
			printerInterface.printFactoryOrder(dataFactoryOrder);
		}

		public void printShipmentNote(DataShipmentHeader dataShipmentHeader)
		{
			printerInterface.printShipmentNote(dataShipmentHeader);
		}

	}
}