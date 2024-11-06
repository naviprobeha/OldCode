using System;
using SerialNET;
using System.Data;


namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for PrinterRW420.
	/// </summary>
	public class PrinterRW420 : PrinterInterface
	{
		private SmartDatabase smartDatabase;
		private DataSetup dataSetup;
		private Port printerPort;
		private bool enabled;

		private bool copy;

		public PrinterRW420(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//

			this.dataSetup = smartDatabase.getSetup();

			SerialNET.License license = new SerialNET.License();
			license.LicenseKey = "f0FGYKKBhLaWA7I1G5KziVM9kfOhUruyfScd";
		}
		#region PrinterInterface Members

		public bool init()
		{
			try
			{
				printerPort = new Port();
				printerPort.ComPort = 4;
				printerPort.BaudRate = 115200;
				printerPort.Parity = SerialNET.Parity.No;
				printerPort.StopBits = SerialNET.StopBits.One;
				printerPort.ByteSize = 8;



				printerPort.Enabled = true;			
				this.enabled = true;

				return true;

			}
			catch(Exception e)
			{
				//System.Windows.Forms.MessageBox.Show(e.ToString());
				if (e.Message != "") {}

				return false;
			}
		}

		public void test()
		{
			char nl = (char)10;

			write("Testdata");
			write(nl+""+nl+""+nl);
		}

		public void printShipment(DataShipmentHeader dataShipmentHeader)
		{
			// TODO:  Add PrinterRW420.printShipment implementation
			if (!this.enabled) return;

			DataOrganization dataOrganization = new DataOrganization(smartDatabase, dataShipmentHeader.organizationNo);

			char nl = (char)10;

			writeRaw("! U1 SETLP 4 0 46");
			writeRaw("! U1 PAGE-WIDTH 800");
			if (this.copy) 
			{
				writeRaw("SVENSK LANTBRUKSTJÄNST AB - KOPIA");
			}
			else
			{
				writeRaw("SVENSK LANTBRUKSTJÄNST AB");
			}
			

			writeRaw("FÖLJESEDEL - "+dataSetup.agentId+"-"+dataShipmentHeader.entryNo.ToString());

			write("! U1 SETLP 7 0 24");
			write(nl+""+nl);

			write("Hämtdatum                     Containernr");
			write("===========================================================");
			write(dataShipmentHeader.shipDate.ToString("yyyy-MM-dd").PadRight(30, ' ')+dataShipmentHeader.containerNo);
			write(nl+""+nl);

			write("Transportör                   Kundinformation");
			write("===========================================================");
			write(dataOrganization.name.PadRight(30, ' ')+dataShipmentHeader.customerName);
			write(dataOrganization.address.PadRight(30, ' ')+dataShipmentHeader.address);
			if ((dataOrganization.address2 != "") || (dataShipmentHeader.address2 != ""))  write(dataOrganization.address2.PadRight(30, ' ')+dataShipmentHeader.address2);
			write((dataOrganization.postCode +" "+ dataOrganization.city).PadRight(30, ' ')+dataShipmentHeader.postCode +" "+ dataShipmentHeader.city);
			write(("Telefonnr: "+dataOrganization.phoneNo).PadRight(30, ' ')+("Telefonnr: "+dataShipmentHeader.phoneNo).PadRight(30, ' '));
			write(("Chaufför: "+dataShipmentHeader.mobileUserName).PadRight(30, ' ')+"Produktionsplatsnr: "+dataShipmentHeader.productionSite);
			write(nl+""+nl);

			write("Hämtadress");
			write("===========================================================");
			if (dataShipmentHeader.customerShipAddressNo == "")
			{
				write("Samma som fakturaadressen");
			}
			else
			{
				write(dataShipmentHeader.shipName);
				write(dataShipmentHeader.shipAddress);
				if (dataShipmentHeader.shipAddress2 != "")  write(dataShipmentHeader.shipAddress2);
				write(dataShipmentHeader.shipPostCode +" "+ dataShipmentHeader.shipCity);				
			}
			write(nl+""+nl);

			string paymentText = "";
			if (dataShipmentHeader.payment == 0)
			{
				paymentText = "Faktura";
			}
			if (dataShipmentHeader.payment == 1)
			{
				paymentText = "Kontant";
			}
			if (dataShipmentHeader.payment == 2)
			{
				paymentText = "Kort";
			}

			write("Referensinformation           Betalning");
			write("===========================================================");
			write(dataShipmentHeader.reference.PadRight(30, ' ')+paymentText);
			write(nl+""+nl);



			write("Samtliga nedanstående animaliska biprodukter kat 1;");
			write("endast för bortskaffande.");
			write("");
			write("                                        Antal              ");
			write("Artikelnr   Beskrivning                 / KG  Avl.   Belopp");
			write("===========================================================");


			bool containsExtraPayment = false;

			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLineDataSet = dataShipmentLines.getShipmentDataSet(dataShipmentHeader);

			int i = 0;
			float totalAmount = 0;

			while (i < shipmentLineDataSet.Tables[0].Rows.Count)
			{
				string itemNo = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
				string description = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
				string unitPrice = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();
				string quantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
				string connectionQuantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
				//string totalAmount = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString();
				string amount = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString();
				string connectionAmount = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString();

				string descriptionText = description.PadRight(27, ' ');
				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "1")
				{
					descriptionText = "*"+description.PadRight(26, ' ');
					containsExtraPayment = true;
				}

				DataItem dataItem = new DataItem(smartDatabase, itemNo);
				if (dataItem.invoiceToJbv) amount = "0.00";

				totalAmount = totalAmount + float.Parse(amount) + float.Parse(connectionAmount);

				write(itemNo.PadRight(11, ' ')+" "+descriptionText+" "+quantity.PadLeft(5, ' ')+" "+connectionQuantity.PadLeft(4, ' ')+" "+amount.PadLeft(8, ' '));

				DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
				DataSet shipmentLineIdDataSet = dataShipmentLineIds.getShipmentLineDataSet(new DataShipmentLine(smartDatabase, int.Parse(shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())));

				int j = 0;
				while (j < shipmentLineIdDataSet.Tables[0].Rows.Count)
				{
					string unitId = shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
					string reMarkUnitId = shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString();
					if (reMarkUnitId != "") reMarkUnitId = "(Reservm.: "+reMarkUnitId+")";
					string testing = "";
					if (reMarkUnitId != "") reMarkUnitId = "("+reMarkUnitId+")";
					if (shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString() == "1") testing = testing + "P";
					if (shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString() == "1") testing = testing + "O";
					if (testing != "") testing = "("+testing+")";

					write("            - "+unitId+" "+reMarkUnitId+" "+testing);

					j++;

				}
				i++;
			}

			write(nl+""+nl);

			float vatAmount = 0;
			float totalAmountVat = 0;

			if (shipmentLineDataSet.Tables[0].Rows.Count > 0)
			{
				vatAmount = (float)(totalAmount * 0.25);
				totalAmountVat = (float)(totalAmount * 1.25);
			}

			write("                                           Belopp: "+(String.Format("{0:f}", totalAmount)).PadLeft(8, ' '));      
			write("                                       Moms (25%): "+(String.Format("{0:f}", vatAmount)).PadLeft(8, ' '));      
			write("                                Belopp inkl. moms: "+(String.Format("{0:f}", totalAmountVat)).PadLeft(8, ' '));      
			
			write(nl+""+nl);
			
			if (containsExtraPayment)
			{
				write("* Maskätna och förruttnade djur debiteras med dubbel taxa.");      
				write(nl+""+nl);
			}
			write("Uppgifterna ovan bekräftas härmed.");      
			write(nl+""+nl+""+nl);

			write("..................................................");
			write("Transportörens signatur");
			write(nl+""+nl+""+nl);
			write("..................................................");
			write("Kundens signatur");
			write(nl+"");
			write("Svensk Lantbrukstjänst  -  Box 734  -  531 17 Lidköping");
			write("Org nr: 556090-9169 - Telefonnr: 0510-868 64");
			write("Företaget innehar F-skattebevis.");

			write(nl+""+nl+""+nl+""+nl+""+nl);
		}

		public void printShipOrder(DataShipOrder dataShipOrder)
		{
			// TODO:  Add PrinterRW420.printShipOrder implementation
			if (!this.enabled) return;

			char nl = (char)10;


			writeRaw("! U1 SETLP 4 0 46");
			writeRaw("! U1 PAGE-WIDTH 800");
			if (this.copy) 
			{
				writeRaw("SVENSK LANTBRUKSTJÄNST AB - KOPIA");
			}
			else
			{
				writeRaw("SVENSK LANTBRUKSTJÄNST AB");
			}
			

			writeRaw("HÄMTORDER - "+dataShipOrder.organizationNo+dataShipOrder.entryNo.ToString());

			write("! U1 SETLP 7 0 24");

			write(nl+""+nl);

			write("Anmälningsdatum");
			write("===============");
			write(dataShipOrder.shipDate.ToString("yyyy-MM-dd"));
			write(nl+""+nl);


			write("Kundinformation ");
			write("===============");
			write(dataShipOrder.customerName);
			write(dataShipOrder.address);
			if (dataShipOrder.address2 != "") write(dataShipOrder.address2);
			write(dataShipOrder.postCode +" "+ dataShipOrder.city);
			write("");
			write("Telefonnr: "+dataShipOrder.phoneNo);
			write(nl+""+nl);

			write("Hämtadress");
			write("==========");
			write(dataShipOrder.shipName);
			write(dataShipOrder.shipAddress);
			if (dataShipOrder.shipAddress2 != "") write(dataShipOrder.shipAddress2);
			write(dataShipOrder.shipPostCode +" "+ dataShipOrder.shipCity);
			write(nl+""+nl);


			write("Innehåll");
			write("========");
			write(dataShipOrder.details);
			write(nl+""+nl);


			write("Vägbeskrivning");
			write("==============");
			
			string directions = dataShipOrder.directionComment + dataShipOrder.directionComment2;

			while (directions.Length > 0)
			{
				string directionLine = "";
				if (directions.Length > 50)
				{
					directionLine = directions.Substring(0, 50);
					int i = 49;
					while ((directionLine[i] != ' ') && (i>0))
					{
						i--;
					}
					directionLine = directionLine.Substring(0, i);
					directions = directions.Substring(i+1);
				}
				else
				{
					directionLine = directions;
					directions = "";
				}

				write(directionLine);

			}
			write(nl+""+nl);


			write(nl+""+nl+""+nl+""+nl+""+nl);
		}

		public void close()
		{

			printerPort.Enabled = false;
			printerPort.Dispose();
		}

		public void setCopy()
		{
			this.copy = true;			
		}

		private void write(string data)
		{
			char ret = (char)13;
			char nl = (char)10;

			/*
			char å = (char)140;
			char ä = (char)138;
			char ö = (char)154;

			char Å = (char)129;
			char Ä = (char)128;
			char Ö = (char)133;

			data = data.Replace('å', å);
			data = data.Replace('ä', ä);
			data = data.Replace('ö', ö);

			data = data.Replace('Å', Å);
			data = data.Replace('Ä', Ä);
			data = data.Replace('Ö', Ö);

			*/
			printerPort.Write(data+ret+nl);
			System.Threading.Thread.Sleep(400);
		}

		private void writeRaw(string data)
		{
			char ret = (char)13;
			char nl = (char)10;

			printerPort.Write(data+ret+nl);
			System.Threading.Thread.Sleep(400);
		}

		public void printPeriodReport(DateTime fromDate, DateTime toDate, string mobileUser)
		{
			// TODO:  Add PrinterRW420.printPeriodReport implementation

			if (!this.enabled) return;

			char nl = (char)10;

			
			writeRaw("! U1 SETLP 4 0 46");
			writeRaw("! U1 PAGE-WIDTH 800");
			write("PERIODRAPPORT");
			
			write("! U1 SETLP 7 0 24");
			write(nl+""+nl);
			write("Bil");
			write("===");
			write(smartDatabase.getSetup().agentId);
			write(nl+""+nl);

			write("Datumintervall");
			write("==============");
			write(fromDate.ToString("yyyy-MM-dd")+" - "+toDate.ToString("yyyy-MM-dd"));
			write(nl+""+nl);

			write("Chaufför");
			write("========");
			if (mobileUser == "")
			{
				write("Alla");
			}
			else
			{
				write(mobileUser);
			}
			write(nl+""+nl);


			write("                                        Antal              ");
			write("Artikelnr   Beskrivning                 / KG  Avl.   Belopp");
			write("===========================================================");


			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLineDataSet = dataShipmentLines.getReportDataSet(fromDate, toDate, mobileUser);

			float cashAmount = 0;
			float cardAmount = 0;
			float invoiceAmount = 0;
			float totalAmount = 0;
			float totalConnectionAmount = 0;

			string shipmentEntryNo = "";
			int i = 0;
			int noOfShipments = 0;
			int totalConnectionQuantity = 0;
			int totalTestQuantity = 0;

			while (i < shipmentLineDataSet.Tables[0].Rows.Count)
			{
				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() != shipmentEntryNo)
				{
					write("");
					string paymentText = "";
					if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "0") paymentText = "Fakt";
					if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "1") paymentText = "Kont";
					if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "2") paymentText = "Kort";

					shipmentEntryNo = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
					write("Containernr: "+shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
					write("Följesedel: "+smartDatabase.getSetup().agentId+"-"+shipmentEntryNo+", "+paymentText+", "+shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()+", "+shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
					noOfShipments++;
				}

				string itemNo = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
				string description = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
				string quantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
				string connectionQuantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString();
				string testQuantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString();
				string amountStr = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();

				DataItem dataItem = new DataItem(smartDatabase, itemNo);
				if (dataItem.invoiceToJbv) amountStr = "0.00";

				float amountInclVat = (float)(float.Parse(amountStr) * 1.25);
				amountStr = String.Format("{0:f}", amountInclVat);


				write(itemNo.PadRight(11, ' ')+" "+description.PadRight(27, ' ')+" "+quantity.PadLeft(5, ' ')+" "+connectionQuantity.PadLeft(4, ' ')+" "+amountStr.PadLeft(8, ' '));

				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "0")
				{
					invoiceAmount = invoiceAmount + float.Parse(amountStr);
				}
				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "1")
				{
					cashAmount = cashAmount + float.Parse(amountStr);
				}
				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() == "2")
				{
					cardAmount = cardAmount + float.Parse(amountStr);
				}

				totalAmount = totalAmount + float.Parse(amountStr);

				if (dataItem.putToDeath)
				{
					totalConnectionQuantity = totalConnectionQuantity + int.Parse(quantity);
					totalConnectionAmount = totalConnectionAmount + float.Parse(amountStr);

				}
				else
				{
					totalConnectionQuantity = totalConnectionQuantity + int.Parse(connectionQuantity);
					totalConnectionAmount = totalConnectionAmount + float.Parse(shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());

				}

				totalTestQuantity = totalTestQuantity + int.Parse(testQuantity);

				DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
				DataSet shipmentLineIdDataSet = dataShipmentLineIds.getShipmentLineDataSet(new DataShipmentLine(smartDatabase, int.Parse(shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString())));

				int j = 0;
				while (j < shipmentLineIdDataSet.Tables[0].Rows.Count)
				{
					string unitId = shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
					string reMarkUnitId = shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString();
					if (reMarkUnitId != "") reMarkUnitId = "(Reservm.: "+reMarkUnitId+")";

					write("            - "+unitId+" "+reMarkUnitId);

					j++;

				}
				i++;
			}

			write(nl+""+nl);

			float vatAmount = 0;
			float totalAmountVat = 0;

			if (shipmentLineDataSet.Tables[0].Rows.Count > 0)
			{
				vatAmount = (float)(totalAmount * 0.25);
				totalAmountVat = (float)(totalAmount * 1.25);

				cashAmount = (float)(cashAmount * 1.25);
				cardAmount = (float)(cardAmount * 1.25);
				invoiceAmount = (float)(invoiceAmount * 1.25);
			}

			//write("                                 Avlivningsbelopp: "+(String.Format("{0:f}", totalConnectionAmount)).PadLeft(8, ' '));      
			//write("                                           Belopp: "+(String.Format("{0:f}", totalAmount - totalConnectionAmount)).PadLeft(8, ' '));      
			//write("                                       Moms (25%): "+(String.Format("{0:f}", vatAmount)).PadLeft(8, ' '));      
			write("                                Belopp inkl. moms: "+(String.Format("{0:f}", totalAmountVat)).PadLeft(8, ' '));      
			write("");

			write("                                          Kontant: "+(String.Format("{0:f}", cashAmount)).PadLeft(8, ' '));      
			write("                                             Kort: "+(String.Format("{0:f}", cardAmount)).PadLeft(8, ' '));      
			write("                                    Att fakturera: "+(String.Format("{0:f}", invoiceAmount)).PadLeft(8, ' '));      

			write("                                Antal följesedlar: "+(noOfShipments.ToString().PadLeft(8, ' ')));      
			write("                                Antal avlivningar: "+(totalConnectionQuantity.ToString().PadLeft(8, ' ')));      
			write("                              Antal provtagningar: "+(totalTestQuantity.ToString().PadLeft(8, ' ')));      
			write("");

			DataItems dataItems = new DataItems(smartDatabase);
			DataSet putToDeathDataSet = dataItems.getPutToDeathDataSet();

			int z = 0;
			while (z < putToDeathDataSet.Tables[0].Rows.Count)
			{
				string description = putToDeathDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString()+":";
				write(description.PadLeft(50, ' ')+" "+(dataShipmentLines.countItems(putToDeathDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString(), fromDate, toDate).ToString().PadLeft(8, ' ')));      			

				z++;
			}


			write(nl+""+nl+""+nl+""+nl+""+nl);

			write("Svensk Lantbrukstjänst  -  Box 734  -  531 17 Lidköping");
			write("Org nr: 556090-9169");

			write(nl+""+nl+""+nl+""+nl+""+nl);

		}

		public void printLineOrder(DataLineOrder dataLineOrder)
		{
			// TODO:  Add PrinterRW420.printLineOrder implementation
			if (!this.enabled) return;

			DataOrganization dataOrganization = new DataOrganization(smartDatabase, dataLineOrder.organizationNo);

			char nl = (char)10;

			writeRaw("! U1 SETLP 4 0 46");
			writeRaw("! U1 PAGE-WIDTH 800");
			if (this.copy) 
			{
				writeRaw("KONVEX AB - KOPIA");
			}
			else
			{
				writeRaw("KONVEX AB");
			}
			writeRaw("FRAKTSEDEL - "+dataLineOrder.entryNo);
			
			write("! U1 SETLP 7 0 24");

			write(nl+""+nl);

			write("Hämtdatum");
			write("=========");
			write(dataLineOrder.shipDate.ToString("yyyy-MM-dd"));
			write(nl+""+nl);

			write("Transportsedel                Konvex leverantörsnr");
			write("===========================================================");
			write(dataLineOrder.lineJournalEntryNo.ToString().PadRight(30, ' ')+dataLineOrder.shippingCustomerNo);
			write(nl+""+nl);


			write("Godsmottagare                 Godsavsändare");
			write("===========================================================");
			write("Konvex AB".PadRight(30, ' ')+dataLineOrder.shippingCustomerName);
			write("Box 734, Esplanaden 24".PadRight(30, ' ')+dataLineOrder.address);
			write("531 17 LIDKÖPING".PadRight(30, ' ')+dataLineOrder.postCode+" "+dataLineOrder.city.ToUpper());
			write("Tel: 0510-868 50".PadRight(30, ' ')+"Tel: "+dataLineOrder.phoneNo);
			write(nl+""+nl);
			write("Transportör                   Bil");
			write("===========================================================");
			write(dataOrganization.name.PadRight(30, ' ')+dataSetup.agentId);
			write(dataOrganization.address);
			if (dataOrganization.address2 != "") write(dataOrganization.address2);
			write(dataOrganization.postCode +" "+ dataOrganization.city);
			if (dataOrganization.phoneNo != "") write("Telefonnr: "+dataOrganization.phoneNo);
			write(nl+""+nl);



			write(nl+""+nl+""+nl);


			write("Kat. 1, Endast för bortskaffande.");
			write("Kat. 2, Får inte användas som foder.");
			write("Kat. 3, Får inte användas som livsmedel.");
			write("");
			write("Containernr   Kategori               Containertyp Vikt (KG)");
			write("===========================================================");


			DataLineOrderContainers dataLineOrderContainers = new DataLineOrderContainers(smartDatabase);
			DataSet lineOrderContainerDataSet = dataLineOrderContainers.getDataSet(dataLineOrder.entryNo);

			int i = 0;

			while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
			{
				string containerNo = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
				string categoryCode = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
				string weight = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
				string containerTypeCode = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
				string categoryDesc = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();

				string category = categoryCode;

				write(containerNo.PadRight(13, ' ')+" "+(category+" "+categoryDesc).PadRight(22, ' ')+" "+containerTypeCode.PadRight(12, ' ')+" "+weight.PadLeft(9, ' '));

				i++;
			}

	
			write(nl+""+nl+""+nl+""+nl);
			
			write("..................................................");
			write("Transportörens signatur");

			write(nl+""+nl+""+nl+""+nl+""+nl);
		}

		public void printFactoryOrder(DataFactoryOrder dataFactoryOrder)
		{

		}

		public void printShipmentNote(DataShipmentHeader dataShipmentHeader)
		{

		}

		#endregion
	}
}
