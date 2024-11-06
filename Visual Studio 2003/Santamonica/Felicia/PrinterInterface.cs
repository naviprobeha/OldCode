using System;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for PrinterInterface.
	/// </summary>
	public interface PrinterInterface
	{
		bool init();
		void test();
		void printShipment(DataShipmentHeader dataShipmentHeader);
		void printShipOrder(DataShipOrder dataShipOrder);
		void close();
		void setCopy();
		void printPeriodReport(DateTime fromDate, DateTime toDate, string mobileUser);
		void printLineOrder(DataLineOrder dataLineOrder);
		void printFactoryOrder(DataFactoryOrder dataFactoryOrder);
		void printShipmentNote(DataShipmentHeader dataShipmentHeader);
	}
}
