using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ShipOrder
	{
		public string organizationNo;
		public int entryNo;
		public DateTime shipDate;
		public string customerNo;
		public string customerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;

		public string phoneNo;
		public string cellPhoneNo;

		public string details;
		public string comments;
		public int priority;

		public string billToCustomerNo;
		public int paymentType;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public string directionComment;
		public string directionComment2;

		public int positionX;
		public int positionY;

		public string agentCode;
		public int status;
		public DateTime closedDate;

		public DateTime shipTime;
		public DateTime creationDate;

		public string productionSite;

		public string oldAgentCode;

		public ShipOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.customerNo = dataReader.GetValue(2).ToString();
			this.customerName = dataReader.GetValue(3).ToString();
			this.address = dataReader.GetValue(4).ToString();
			this.address2 = dataReader.GetValue(5).ToString();
			this.postCode = dataReader.GetValue(6).ToString();
			this.city = dataReader.GetValue(7).ToString();
			this.countryCode = dataReader.GetValue(8).ToString();
			this.phoneNo = dataReader.GetValue(9).ToString();
			this.cellPhoneNo = dataReader.GetValue(10).ToString();
			this.details = dataReader.GetValue(11).ToString();
			this.comments = dataReader.GetValue(12).ToString();
			this.priority = dataReader.GetInt32(13);
			this.agentCode = dataReader.GetValue(14).ToString();
			this.status = dataReader.GetInt32(15);
			this.closedDate = dataReader.GetDateTime(16);
			this.positionX = dataReader.GetInt32(17);
			this.positionY = dataReader.GetInt32(18);
			this.shipDate = dataReader.GetDateTime(19);
			this.billToCustomerNo = dataReader.GetValue(20).ToString();
			this.customerShipAddressNo = dataReader.GetValue(21).ToString();
			this.shipName = dataReader.GetValue(22).ToString();
			this.shipAddress = dataReader.GetValue(23).ToString();
			this.shipAddress2 = dataReader.GetValue(24).ToString();
			this.shipPostCode = dataReader.GetValue(25).ToString();
			this.shipCity = dataReader.GetValue(26).ToString();
			this.directionComment = dataReader.GetValue(27).ToString();
			this.directionComment2 = dataReader.GetValue(28).ToString();
			this.paymentType = dataReader.GetInt32(29);
			this.shipTime = dataReader.GetDateTime(30);
			this.creationDate = dataReader.GetDateTime(31);
			this.productionSite = dataReader.GetValue(32).ToString();

			this.oldAgentCode = agentCode;

		}

		public ShipOrder(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataRow.ItemArray.GetValue(0).ToString();
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.customerNo = dataRow.ItemArray.GetValue(2).ToString();
			this.customerName = dataRow.ItemArray.GetValue(3).ToString();
			this.address = dataRow.ItemArray.GetValue(4).ToString();
			this.address2 = dataRow.ItemArray.GetValue(5).ToString();
			this.postCode = dataRow.ItemArray.GetValue(6).ToString();
			this.city = dataRow.ItemArray.GetValue(7).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(8).ToString();
			this.phoneNo = dataRow.ItemArray.GetValue(9).ToString();
			this.cellPhoneNo = dataRow.ItemArray.GetValue(10).ToString();
			this.details = dataRow.ItemArray.GetValue(11).ToString();
			this.comments = dataRow.ItemArray.GetValue(12).ToString();
			this.priority = int.Parse(dataRow.ItemArray.GetValue(13).ToString());
			this.agentCode = dataRow.ItemArray.GetValue(14).ToString();
			this.status = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			this.closedDate = DateTime.Parse(dataRow.ItemArray.GetValue(16).ToString());
			this.positionX = int.Parse(dataRow.ItemArray.GetValue(17).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(18).ToString());
			this.shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(19).ToString());
			this.billToCustomerNo = dataRow.ItemArray.GetValue(20).ToString();
			this.customerShipAddressNo = dataRow.ItemArray.GetValue(21).ToString();
			this.shipName = dataRow.ItemArray.GetValue(22).ToString();
			this.shipAddress = dataRow.ItemArray.GetValue(23).ToString();
			this.shipAddress2 = dataRow.ItemArray.GetValue(24).ToString();
			this.shipPostCode = dataRow.ItemArray.GetValue(25).ToString();
			this.shipCity = dataRow.ItemArray.GetValue(26).ToString();
			this.directionComment = dataRow.ItemArray.GetValue(27).ToString();
			this.directionComment2 = dataRow.ItemArray.GetValue(28).ToString();
			this.paymentType = int.Parse(dataRow.ItemArray.GetValue(29).ToString());
			this.shipTime = DateTime.Parse(dataRow.ItemArray.GetValue(30).ToString());
			this.creationDate = DateTime.Parse(dataRow.ItemArray.GetValue(31).ToString());
			this.productionSite = dataRow.ItemArray.GetValue(32).ToString();

			this.oldAgentCode = agentCode;

		}


		public ShipOrder(Database database, string agentCode, DataSet dataSet)
		{
			//
			// TODO: Add constructor logic here
			//
			
			fromDataSet(dataSet);

			if (customerShipAddressNo == "NEW")
			{
				CustomerShipAddress customerShipAddress = new CustomerShipAddress();
				customerShipAddress.organizationNo = organizationNo;
				customerShipAddress.customerNo = customerNo;
				customerShipAddress.name = shipName;
				customerShipAddress.address = shipAddress;
				customerShipAddress.address2 = shipAddress2;
				customerShipAddress.postCode = shipPostCode;
				customerShipAddress.city = shipCity;
				customerShipAddress.productionSite = productionSite;
				customerShipAddress.save(database);

				this.customerShipAddressNo = customerShipAddress.entryNo;
			}



			Customers customers = new Customers();
			Customer customer = customers.getEntry(database, this.organizationNo, this.customerNo);


			if (customer != null)
			{
				this.applySellToCustomer(customer);
			}

			if ((this.customerShipAddressNo != "") && (this.customerShipAddressNo != "NEW"))
			{
				//Fix för att gå runt problemet att någon dataReader är öppen...
				
				CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
				CustomerShipAddress customerShipAddress = customerShipAddresses.getEntry(database, this.organizationNo, this.customerNo, customerShipAddressNo);
				if (customerShipAddress != null) this.applyShipToAddress(customerShipAddress);	

				if (customer != null)
				{
					// Kunder som går att editera.
					if (customer.editable)
					{
						this.customerName = shipName;
						this.address = shipAddress;
						this.address2 = shipAddress2;
						this.postCode = shipPostCode;
						this.city = shipCity;
					}
				}
			}


			save(database);
		}

		public ShipOrder(string organizationNo)
		{
			this.organizationNo = organizationNo;
			this.closedDate = new DateTime(1753, 01, 01, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.creationDate = DateTime.Today;
			this.billToCustomerNo = "";
			this.customerNo = "";
		}

		public ShipOrder(ScheduledShipOrder scheduledShipOrder, DateTime shipDate)
		{
			this.organizationNo = scheduledShipOrder.organizationNo;
			this.shipDate = shipDate;
			this.customerNo = scheduledShipOrder.customerNo;
			this.customerName = scheduledShipOrder.customerName;
			this.address = scheduledShipOrder.address;
			this.address2 = scheduledShipOrder.address2;
			this.postCode = scheduledShipOrder.postCode;
			this.city = scheduledShipOrder.city;
			this.countryCode = scheduledShipOrder.countryCode;
			this.billToCustomerNo = scheduledShipOrder.billToCustomerNo;
			this.phoneNo = scheduledShipOrder.phoneNo;
			this.cellPhoneNo = scheduledShipOrder.cellPhoneNo;
			this.comments = scheduledShipOrder.comments;
			this.paymentType = scheduledShipOrder.paymentType;
			this.positionX = scheduledShipOrder.positionX;
			this.positionY = scheduledShipOrder.positionY;
			this.directionComment = scheduledShipOrder.directionComment;
			this.directionComment2 = scheduledShipOrder.directionComment2;
			this.shipName = scheduledShipOrder.shipName;
			this.shipAddress = scheduledShipOrder.shipAddress;
			this.shipAddress2 = scheduledShipOrder.shipAddress2;
			this.shipPostCode = scheduledShipOrder.shipPostCode;
			this.shipCity = scheduledShipOrder.shipCity;
			this.closedDate = new DateTime(DateTime.Now.Year+10, 1, 1, 0, 0, 0);
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.creationDate = DateTime.Today;
			this.priority = 8;

		}

		public void fromDataSet(DataSet dataset)
		{

			this.entryNo = 0;
			//no = this.agentCode +"-"+ dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			this.organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			this.shipDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			this.customerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			this.customerName = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			this.address = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			this.address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			this.postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			this.city = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			this.countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			this.phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			this.cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();

			//productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			this.paymentType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
			//dairyCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
			//dairyNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
			//reference = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
			//userName = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
			//containerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
			//shipOrderEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
			this.comments = dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
			this.agentCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();

			this.billToCustomerNo = this.customerNo;

			if (dataset.Tables[0].Rows[0].ItemArray.Length > 25)
			{
				this.customerShipAddressNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
				this.shipName = dataset.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
				this.shipAddress = dataset.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
				this.shipAddress2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
				this.shipPostCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();
				this.shipCity = dataset.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
			}
			else
			{
				this.shipName = this.customerName;
				this.shipAddress = this.address;
				this.shipAddress2 = this.address2;
				this.shipPostCode = this.postCode;
				this.shipCity = this.city;
			}

			this.priority = 5;

			shipDate = DateTime.Today;
			this.closedDate = new DateTime(1753, 01, 01, 0, 0, 0);
			this.creationDate = DateTime.Today;


		}

		public ShipOrder(Customer customer)
		{
			this.organizationNo = customer.organizationNo;
			this.closedDate = new DateTime(DateTime.Now.Year+10, 1, 1, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.billToCustomerNo = "";
			this.customerNo = "";
			this.creationDate = DateTime.Today;

			applySellToCustomer(customer);
		}

		public void save(Database database)
		{
			save(database, true);
		}

		public void save(Database database, bool synch)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			this.closedDate = new DateTime(closedDate.Year, closedDate.Month, closedDate.Day, 0, 0, 0, 0);
			this.shipTime = new DateTime(1754, 01, 01, shipTime.Hour, shipTime.Minute, shipTime.Second, shipTime.Millisecond);

			//throw new Exception("Agent: "+this.agentCode);

			if (entryNo == 0)
			{
				database.nonQuery("INSERT INTO [Ship Order] ([Organization No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site]) VALUES ('"+this.organizationNo+"','"+this.customerNo+"','"+this.customerName+"','"+this.address+"','"+this.address2+"','"+this.postCode+"','"+this.city+"','"+this.countryCode+"','"+this.phoneNo+"','"+this.cellPhoneNo+"','"+this.details+"','"+this.comments+"','"+this.priority+"','"+this.billToCustomerNo+"','"+this.customerShipAddressNo+"','"+this.shipName+"','"+this.shipAddress+"','"+this.shipAddress2+"','"+this.shipPostCode+"','"+this.shipCity+"','',"+this.status+",'"+this.closedDate.ToString("yyyy-MM-dd")+"','"+positionX+"','"+positionY+"', '"+shipDate.ToString("yyyy-MM-dd")+"','"+this.directionComment+"','"+this.directionComment2+"','"+paymentType+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+creationDate.ToString("yyyy-MM-dd 00:00:00")+"', '"+this.productionSite+"')");
				entryNo = (int)database.getInsertedSeqNo();
				
			}
			else
			{
				database.nonQuery("UPDATE [Ship Order] SET [Customer No] = '"+customerNo+"', [Customer Name] = '"+customerName+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Cell Phone No] = '"+cellPhoneNo+"', [Details] = '"+details+"', [Comments] = '"+comments+"', [Priority] = '"+priority+"', [Bill-to Customer No] = '"+this.billToCustomerNo+"', [Customer Ship Address No] = '"+this.customerShipAddressNo+"', [Ship Name] = '"+this.shipName+"', [Ship Address] = '"+this.shipAddress+"', [Ship Address 2] = '"+this.shipAddress2+"', [Ship Post Code] = '"+this.shipPostCode+"', [Ship City] = '"+this.shipCity+"', [Agent Code] = '"+agentCode+"', [Status] = '"+status+"', [Closed Date] = '"+closedDate.ToString("yyyy-MM-dd")+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"', [Direction Comment] = '"+directionComment+"', [Direction Comment 2] = '"+directionComment2+"', [Payment Type] = '"+paymentType+"', [Ship Time] = '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Creation Date] = '"+creationDate.ToString("yyyy-MM-dd")+"', [Production Site] = '"+productionSite+"' WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");

				if ((synch) && (agentCode != "")) synchQueue.enqueue(database, this.agentCode, 0, entryNo.ToString(), 0);
			}


		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");
			database.nonQuery("DELETE FROM [Ship Order Line] WHERE [Ship Order Entry No] = '"+entryNo+"'");
			database.nonQuery("DELETE FROM [Ship Order Line ID] WHERE [Ship Order Entry No] = '"+entryNo+"'");

			database.nonQuery("DELETE FROM [Synchronization Queue] WHERE [Type] = 0 AND [Primary Key] = '"+entryNo+"' AND [Action] = 0");

			if (this.agentCode != "")
			{
				SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
				synchQueue.enqueue(database, this.agentCode, 0, entryNo.ToString(), 2);
			}
		}


		public void assignOrder(Database database, string newAgentCode, string source)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			bool deleteFirst = false;
			string oldAgentCode = this.agentCode;

			if ((this.agentCode != newAgentCode) && (this.agentCode != "")) deleteFirst = true;

			if (newAgentCode != "") 
				this.status = 3;
			else
				this.status = 0;

			this.agentCode = newAgentCode;
			this.save(database);

			if (this.oldAgentCode == "")
			{
				log(database, source, "Tilldelad till "+newAgentCode);
			}
			else
			{
				if (newAgentCode == "")
				{
					log(database, source, "Avtilldelad från "+this.agentCode);
				}
				else
				{
					log(database, source, "Omtilldelad från "+this.agentCode+" till "+newAgentCode);
				}
			}

			if (deleteFirst) synchQueue.enqueue(database, oldAgentCode, 0, entryNo.ToString(), 2);


			ShipOrderLines shipOrderLines = new ShipOrderLines();
			DataSet shipOrderLineDataSet = shipOrderLines.getDataSet(database, this.entryNo);
			int i = 0;
			while(i < shipOrderLineDataSet.Tables[0].Rows.Count)
			{
				if (deleteFirst) synchQueue.enqueue(database, oldAgentCode, 9, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 2);
				if (newAgentCode != "") synchQueue.enqueue(database, newAgentCode, 9, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 0);
				


				ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds();
				DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, this.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
				int j = 0;
				while(j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
				{
					if (deleteFirst) synchQueue.enqueue(database, oldAgentCode, 10, shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), 2);
					if (newAgentCode != "") synchQueue.enqueue(database, newAgentCode, 10, shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), 0);
				

					j++;
				}


				i++;
			}


		}


		public void updateDetails(Database database)
		{
			ShipOrderLines shipOrderLines = new ShipOrderLines();
			ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds();

			System.Data.DataSet shipOrderLineDataSet = shipOrderLines.getDataSet(database, this.entryNo);

			Items items = new Items();

			int i = 0;
			this.details = "";

			while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
			{
				Item item = items.getEntry(database, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
				int quantity = int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
				int connectionQuantity = int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString());

				details = details + quantity.ToString()+" "+item.unitOfMeasure+" "+item.description+" ";
				if (connectionQuantity > 0)
				{
					details = details + "("+connectionQuantity+"A) ";
				}

				System.Data.DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, this.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
					
				int j = 0;
				string idDetails = "";
				int testQuantity = 0;

				if (j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
				{
					idDetails = idDetails + "ID: ";
					bool first = true;

					while (j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
					{
						if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1") testQuantity++;

						if (first)
						{
							idDetails = idDetails + shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
							if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1") idDetails = idDetails + "(P)";
							if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() == "1") idDetails = idDetails + "(O)";
							first = false;
						}
						else
						{
							idDetails = idDetails + ", "+shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
							if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1") idDetails = idDetails + "(P)";
							if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() == "1") idDetails = idDetails + "(O)";
						}
							
						j++;
					}
				}

				if (testQuantity > 0)
				{
					details = details + "("+testQuantity+"P) ";
				}

				details = details + idDetails + "; ";

				i++;
			}

			if (details.Length > 200) details = details.Substring(1, 197)+"...";

		}

		public void applySellToCustomer(Customer customer)
		{

			customerNo = customer.no;

			phoneNo = customer.phoneNo;
			cellPhoneNo = customer.cellPhoneNo;
			if (cellPhoneNo.Length > 20) cellPhoneNo = cellPhoneNo.Substring(1, 20);
			
			directionComment = customer.directionComment;
			directionComment2 = customer.directionComment2;

			shipName = customer.name;
			shipAddress = customer.address;
			shipAddress2 = customer.address2;
			shipPostCode = customer.postCode;
			shipCity = customer.city;

			positionX = customer.positionX;
			positionY = customer.positionY;

			productionSite = customer.productionSite;


			if (customer.forceCashPayment) 
			{
				paymentType = 1;
				this.comments = "Kontant betalning";
			}

			if (billToCustomerNo == "")
			{
				applyBillToCustomer(customer);
			}


			if (customer.editable)
			{
				phoneNo = "";
				cellPhoneNo = "";
				directionComment = "";
				directionComment2 = "";
				positionX = 0;
				positionY = 0;
				productionSite = "";
			}

			
		}

		public void applyBillToCustomer(Customer customer)
		{

			billToCustomerNo = customer.no;

			customerName = customer.name;
			address = customer.address;
			address2 = customer.address2;
			postCode = customer.postCode;
			city = customer.city;

			if (customerNo == "")
			{
				applySellToCustomer(customer);
			}


		}

		public void applyShipToAddress(CustomerShipAddress customerShipAddress)
		{

			customerShipAddressNo = customerShipAddress.entryNo;
			shipName = customerShipAddress.name;
			shipAddress = customerShipAddress.address;
			shipAddress2 = customerShipAddress.address2;
			shipPostCode = customerShipAddress.postCode;
			shipCity = customerShipAddress.city;
			if (customerShipAddress.phoneNo != "") phoneNo = customerShipAddress.phoneNo;

			directionComment = customerShipAddress.directionComment;
			directionComment2 = customerShipAddress.directionComment2;

			positionX = customerShipAddress.positionX;
			positionY = customerShipAddress.positionY;

			productionSite = customerShipAddress.productionSite;
		}

		public void log(Database database, string source, string text)
		{

			ShipOrderLogLines shipOrderLogLines = new ShipOrderLogLines();
			shipOrderLogLines.add(database, this, source, text);
		}

		public string getStatusText()
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Avböjd";
			}
			if (status == 2)
			{
				return "Osäker";
			}
			if (status == 3) 
			{
				return "Tilldelad";
			}
			if (status == 4) 
			{
				return "Skickad";
			}
			if (status == 5) 
			{
				return "Bekräftad";
			}
			if (status == 6) 
			{
				return "Lastad";
			}
			if (status == 7) 
			{
				return "Markulerad";
			}

			return "";


		}

		public string getOrganizationName(Database database)
		{
			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, this.organizationNo);
			if (organization != null) return organization.name;
			return "";
		}

		public void changeOrganization(Database database, string newOrganizationNo, string source)
		{
			log(database, source, "Överförd från "+this.organizationNo+" till "+newOrganizationNo+".");

			this.organizationNo = newOrganizationNo;

			database.nonQuery("UPDATE [Ship Order] SET [Organization No] = '"+this.organizationNo+"' WHERE [Entry No] = '"+this.entryNo+"'");
		}

		public void assignLastAgent(Database database, string source)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			ShipmentHeader shipmentHeader = shipmentHeaders.getLastShipmentForCustomer(database, this.customerNo);
			if (shipmentHeader != null)
			{
				this.assignOrder(database, shipmentHeader.agentCode, source);

			}

		}

	}
}
