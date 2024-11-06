using System;
using System.Collections;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine

{
	/// <summary>
	/// Summary description for LineOrderManagement.
	/// </summary>
	public class SMSHandler
	{
		private Configuration configuration;
		private Logger logger;

		private Thread thread;
		private bool running;

		public SMSHandler(Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;
			running = true;

			thread = new Thread(new ThreadStart(run));
			thread.Start();

		}

		public void run()
		{
			log("Handler started.", 0);

			while(running)
			{
				try
				{
					this.processMessages();
				}
				catch(Exception e)
				{
					log("Exception: "+e.Message, 2);
				}

				Thread.Sleep(2000);
			}
			log("Handler stopped.", 0);
		}

		public void stop()
		{
			this.running = false;
		}

		private void processMessages()
		{
			Database database = new Database(logger, configuration);

			SMSMessages smsMessages = new SMSMessages();
			DataSet incomingMessagesDataSet = smsMessages.getUnhandledDataSet(database, 0);
			int i = 0;
			while (i < incomingMessagesDataSet.Tables[0].Rows.Count)
			{
				SMSMessage smsMessage = new SMSMessage(incomingMessagesDataSet.Tables[0].Rows[i]);
				processSmsMessage(smsMessage, database);

				i++;
			}

			database.close();
		}

		private void processSmsMessage(SMSMessage smsMessage, Database database)
		{
			string phoneNo = "0"+smsMessage.phoneNo.Substring(3);
			string phoneNo1 = phoneNo.Substring(0, 4)+"-"+phoneNo.Substring(4);
			string phoneNo2 = phoneNo.Substring(0, 3)+"-"+phoneNo.Substring(3);

			log("Incoming message from "+phoneNo1+": "+smsMessage.message, 0);

			Customers customers = new Customers();
			Customer customer = customers.findFirstCustomerByPhoneNo(database, phoneNo1);
			if (customer != null)
			{
				createShipOrder(customer, smsMessage, database);
			}
			else
			{
				customer = customers.findFirstCustomerByPhoneNo(database, phoneNo2);
				if (customer != null)
				{
					createShipOrder(customer, smsMessage, database);
				}
				else
				{
					log("Customer not found.", 0);
					composeMessage(smsMessage.phoneNo, "Ditt telefonnummer är inte registrerat. Var god ring Svensk Lantbrukstjänst på telefon 010-490 99 00.", database);
				}
			}

			smsMessage.handled = true;
			smsMessage.save(database);

		}

		private void createShipOrder(Customer customer, SMSMessage smsMessage, Database database)
		{

			Items items = new Items();

			ArrayList itemList = smsMessage.getCommaSeparatedList();
			ArrayList orderLines = new ArrayList();

			int i = 0;

			while (i < itemList.Count)
			{
				ArrayList wordList = this.splitItemLine((string)itemList[i]);

				log("Interpreting line: "+(string)itemList[i]+", no of words: "+wordList.Count, 0);

				int quantity = 0;
				string itemNo = "";
				string id = "";
				bool testing = false;
				bool postMortem = false;
				bool putToDeath = false;

				int j = 0;
				while (j < wordList.Count)
				{
					bool wordDone = false;

					if ((!wordDone) && (((string)wordList[j]).ToUpper().IndexOf("PROV") > -1))
					{
						testing = true;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper().IndexOf("OBD") > -1))
					{
						postMortem = true;
						wordDone = true;
					}
						
					if ((!wordDone) && (((string)wordList[j]).ToUpper().IndexOf("AVL") > -1))
					{
						putToDeath = true;
						wordDone = true;
					}
					
					if ((!wordDone) && ((((string)wordList[j]).Length > 2) && (((string)wordList[j]).Substring(0, 2).ToUpper() == "ID")))
					{
						id = ((string)wordList[j]).Substring(2);
						if (id[0] == ':') id = id.Substring(1);
						wordDone = true;
					}
						
					log("Hepp", 0);

					if ((!wordDone) && ((((string)wordList[j])[0] == '0') || (((string)wordList[j])[0] == '1') || (((string)wordList[j])[0] == '2') || (((string)wordList[j])[0] == '3') || (((string)wordList[j])[0] == '4') || (((string)wordList[j])[0] == '5') || (((string)wordList[j])[0] == '6') || (((string)wordList[j])[0] == '7') || (((string)wordList[j])[0] == '8') || (((string)wordList[j])[0] == '9')))
					{
						try
						{
							quantity = int.Parse((string)wordList[j]);
							wordDone = true;
						}
						catch (Exception)
						{
						}
					}
					
					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "EN"))
					{
						log("EN:ig", 0);

						quantity = 1;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "TVÅ"))
					{
						quantity = 2;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "TRE"))
					{
						quantity = 3;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "FYRA"))
					{
						quantity = 4;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "FEM"))
					{
						quantity = 5;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "SEX"))
					{
						quantity = 6;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "SJU"))
					{
						quantity = 7;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "ÅTTA"))
					{
						quantity = 8;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "NIO"))
					{
						quantity = 9;
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "ST"))
					{
						wordDone = true;
					}

					if ((!wordDone) && (((string)wordList[j]).ToUpper() == "KG"))
					{
						wordDone = true;
					}

					if (!wordDone)
					{
										
						string itemDescription = (string)wordList[j];

						Item item = null;

						while ((item == null) && (itemDescription.Length > 2))
						{
							item = items.findEntry(database, itemDescription);
							if (item == null) itemDescription = itemDescription.Substring(0, itemDescription.Length-1);
						}
						if (item != null) 
						{
							itemNo = item.no;
							if (quantity == 0) quantity = 1;

						}

						wordDone = true;
					}

					j++;
				}

				if (itemNo != "")
				{
					log("Adding item: "+itemNo+", "+quantity+", "+id, 0);

					SMSItem smsItem = new SMSItem();
					smsItem.itemNo = itemNo;
					smsItem.quantity = quantity;
					smsItem.id = id;
					smsItem.testing = testing;
					smsItem.postMortem = postMortem;
					smsItem.putToDeath = putToDeath;
					orderLines.Add(smsItem);
				}
				else
				{
					log("Error in translation.", 0);
					this.composeMessage(smsMessage.phoneNo, "Felaktig inmatning. Ex: 1 mjölkko ID:SE001243-1234-9. Separera djuren med komma (,).", database); 
					return;
				}

				i++;
			}


			ShipOrder shipOrder = new ShipOrder(customer);
			shipOrder.shipDate = DateTime.Today;
			shipOrder.comments = "Anmäld via SMS.";
			shipOrder.save(database, false);

			i = 0;
			while (i < orderLines.Count)
			{
				SMSItem smsItem = (SMSItem)orderLines[i];					
				Item item = items.getEntry(database, smsItem.itemNo);

				if (item.requireCashPayment)
				{
					shipOrder.paymentType = 1;
				}

				ShipOrderLine shipOrderLine = new ShipOrderLine(shipOrder);
				shipOrderLine.itemNo = item.no;
				shipOrderLine.connectionItemNo = item.connectionItemNo;
				shipOrderLine.quantity = smsItem.quantity;

				ItemPriceExtended itemPriceExt = new ItemPriceExtended(database, item, smsItem.quantity, customer);
				if (itemPriceExt.lineAmount > 0)
				{
					shipOrderLine.amount = (float)itemPriceExt.lineAmount;
					shipOrderLine.unitPrice = (float)(decimal.Round(itemPriceExt.lineAmount / smsItem.quantity, 2));
				}
				else
				{
					ItemPrice itemPrice = new ItemPrice(database, item, smsItem.quantity, customer);
					if (itemPrice.unitPrice > 0)
					{
						shipOrderLine.unitPrice = (float)itemPrice.unitPrice;
						shipOrderLine.amount = (float)(itemPrice.unitPrice * smsItem.quantity);
					}
					else
					{
						shipOrderLine.unitPrice = (float)item.unitPrice;
						shipOrderLine.amount = (float)(item.unitPrice * smsItem.quantity);
					}
				}
				if (item.invoiceToJbv)
				{
					shipOrderLine.unitPrice = 0;
					shipOrderLine.amount = 0;
				}

				if (item.connectionItemNo != "")
				{
					Item connectionItem = items.getEntry(database, item.connectionItemNo);

					shipOrderLine.connectionItemNo = connectionItem.no;
					if (smsItem.putToDeath) shipOrderLine.connectionQuantity = 1;

					ItemPriceExtended connItemPriceExt = new ItemPriceExtended(database, connectionItem, shipOrderLine.connectionQuantity, customer);
					if (connItemPriceExt.lineAmount > 0)
					{
						shipOrderLine.connectionAmount = (float)(connItemPriceExt.lineAmount);
						shipOrderLine.connectionUnitPrice = (float)decimal.Round(connItemPriceExt.lineAmount / shipOrderLine.connectionQuantity, 2);
					}
					else
					{
						ItemPrice connItemPrice = new ItemPrice(database, connectionItem, shipOrderLine.connectionQuantity, customer);
						if (connItemPrice.unitPrice > 0)
						{
							shipOrderLine.connectionUnitPrice = (float)connItemPrice.unitPrice;
							shipOrderLine.connectionAmount = (float)(connItemPrice.unitPrice * shipOrderLine.connectionQuantity);
						}
						else
						{
							shipOrderLine.connectionUnitPrice = (float)item.unitPrice;
							shipOrderLine.connectionAmount = (float)(item.unitPrice * shipOrderLine.connectionQuantity);
						}
					}

				}

				shipOrderLine.totalAmount = shipOrderLine.amount + shipOrderLine.connectionAmount;
				shipOrderLine.save(database);


				if (smsItem.id != "")
				{

					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = smsItem.id;
					if (smsItem.testing) shipOrderLineId.bseTesting = true;
					if (smsItem.postMortem) shipOrderLineId.postMortem = true;

					shipOrderLineId.save(database);
				}


				i++;
			}

			shipOrder.updateDetails(database);
			shipOrder.save(database, false);

			log("Ship Order created. Order no: "+shipOrder.entryNo, 0);
			this.composeMessage(smsMessage.phoneNo, "Ordernr: "+shipOrder.entryNo+", "+shipOrder.details+". Tack för din anmälning.", database);
		}

		private void composeMessage(string phoneNo, string message, Database database)
		{
			SMSMessage responseMessage = new SMSMessage();
			responseMessage.type = 1;
			responseMessage.phoneNo = phoneNo;
			responseMessage.message = message;
			responseMessage.receivedDateTime = DateTime.Now;
			responseMessage.save(database);

		}


		private void log(string message, int type)
		{
			logger.write("[SMSHandler] "+message, type);
		}

		public ArrayList splitItemLine(string itemLine)
		{
			ArrayList arrayList = new ArrayList();
	
			string buffer = itemLine;

			while (buffer.IndexOf(" ") > 0)
			{
				string word = buffer.Substring(0, buffer.IndexOf(" "));
				buffer = buffer.Substring(buffer.IndexOf(" ")+1);

				arrayList.Add(word);
			}

			log("("+buffer+")", 0);
			if (buffer.Length > 1) arrayList.Add(buffer);

			return arrayList;
		}

	}
}
