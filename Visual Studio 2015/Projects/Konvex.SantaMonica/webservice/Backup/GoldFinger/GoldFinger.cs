using Navipro.SantaMonica.Common;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
namespace Konvex.SmartShipping.Goldfinger
{
    public class Goldfinger : Logger
    {
        private Configuration configuration;
        private Database database;

        public bool init()
        {
            this.configuration = new Configuration();
            if (!this.configuration.initWeb())
            {
                return false;
            }
            this.database = new Database(this, this.configuration);
            return true;
        }

        public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            serverLogging.log(agentCode, "AgentStatus: " + agentCode + ", " + rt90x + ", " + rt90y + ", " + (decimal)(int)heading + ", " + (decimal)speed + ", " + (decimal)height + ", " + status + ", " + userName + ", " + tripMeter);

            if (status == 0) userName = "";

            try
            {
                Agents agents = new Agents();
                Navipro.SantaMonica.Common.Agent agent = agents.getAgent(this.database, agentCode);
                int result;
                if (agent != null)
                {
                    AgentTransaction agentTransaction = new AgentTransaction(agentCode, rt90x, rt90y, (decimal)(int)heading, (decimal)(int)speed, (decimal)(int)height, status, userName, tripMeter);
                    agentTransaction.save(this.database);
                    SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries(agentCode);
                    result = synchronizationQueueEntries.getCount(this.database);
                    return result;
                }
                result = 0;
                return result;
            }
            catch (Exception ex)
            {

                //serverLogging.log(agentCode, "Debug: Hepp");
                serverLogging.log(agentCode, "ReportStatus: " + ex.Message);
            }
            return 0;
        }

        public void createContainerEntry(string agentCode, string containerNo, int type, int documentType, string documentNo, DateTime entryDateTime, DateTime estimatedArrivalDateTime, int locationType, string locationCode, int positionX, int positionY, int sourceType, string sourceCode, float weight)
        {
            Agents agents = new Agents();
            Agent agent = agents.getAgent(database, agentCode);
            if (agent == null) return;

            if ((type == 2) && (estimatedArrivalDateTime == DateTime.MinValue)) return;

            if (locationCode != null)
            {
                OrganizationLocations organizationLocations = new OrganizationLocations();
                OrganizationLocation organizationLocation = organizationLocations.getEntry(this.database, agent.organizationNo, locationCode);
                if (organizationLocation != null) locationType = organizationLocation.type + 1;
            }

            ServerLogging serverLogging = new ServerLogging(this.database);
            serverLogging.log(agentCode, "Entry datetime: " + entryDateTime.ToString("yyyy-MM-dd") + ", " + estimatedArrivalDateTime.ToString("yyyy-MM-dd"));
            try
            {
                if (locationCode == "")
                {
                        Organizations organizations = new Organizations();
                        Organization organization = organizations.getOrganization(database, agent.organizationNo);
                        if (organization != null)
                        {
                            locationCode = organization.shippingCustomerNo;
                            locationType = 1;
                        }
                }

                serverLogging.log(agentCode, "Create Container Entry");

                if (estimatedArrivalDateTime == DateTime.MinValue) estimatedArrivalDateTime = new DateTime(1753, 1, 1);
                
                ContainerEntry containerEntry = new ContainerEntry();
                containerEntry.containerNo = containerNo;
                containerEntry.type = type;
                containerEntry.creatorType = 0;
                containerEntry.creatorNo = agentCode;
                containerEntry.documentType = documentType;
                containerEntry.documentNo = documentNo;
                containerEntry.entryDateTime = entryDateTime;
                containerEntry.estimatedArrivalDateTime = estimatedArrivalDateTime;
                containerEntry.locationType = locationType;
                containerEntry.locationCode = locationCode;
                containerEntry.positionX = positionX;
                containerEntry.positionY = positionY;
                containerEntry.receivedDateTime = DateTime.Now;
                containerEntry.sourceType = sourceType;
                containerEntry.sourceCode = sourceCode;
                containerEntry.weight = weight;
                containerEntry.save(database);
                


                if (containerEntry.type == 2)
                {
                    serverLogging.log(agentCode, "[ArrivalReport] Finding LineOrder for container " + containerEntry.containerNo);
                    LineOrders lineOrders = new LineOrders();
                    LineOrder containerLineOrder = lineOrders.getContainerLineOrder(this.database, containerEntry.containerNo, agentCode);
                    if (containerLineOrder != null)
                    {
                        serverLogging.log(agentCode, string.Concat(new object[]
						{
							"[ArrivalReport] Setting confirmed datetime to ",
							containerEntry.estimatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm"),
							", LineOrder: ",
							containerLineOrder.entryNo
						}));
                        containerLineOrder.confirmOrder(this.database, containerEntry.estimatedArrivalDateTime, containerEntry.locationCode);
                    }
                }
                if (containerEntry.type == 1 && containerEntry.locationType == 2 && containerEntry.documentNo == "")
                {
                    this.directUnloadOfLineOrder(containerEntry);
                }
            }
            catch (Exception ex)
            {
                serverLogging.log(agentCode, "[CreateContainerEntry] " + ex.Message);
            }
        }
        public SynchEntryCollection getSynchRecord(string agentCode)
        {
            SynchEntryCollection synchEntryCollection = new SynchEntryCollection();

            this.checkItemPriceUpdates(agentCode);

            Agents agents = new Agents();
            Navipro.SantaMonica.Common.Agent agent = agents.getAgent(this.database, agentCode);

            if (agent != null)
            {
                bool flag = true;
                int count = 0;
                int lastEntryNo = 0;

                while ((flag) && (count < 50))
                {
                    SynchEntry synchEntry = null;
                    flag = false;
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_SHIP_ORDER, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE_ID, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_LINE_JOURNAL, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_LINE_ORDER, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        synchEntry = this.getSynchRecords(agent, SynchronizationQueueEntries.SYNC_ALL, lastEntryNo);
                        if (synchEntry != null)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        lastEntryNo = synchEntry.entryNo;
                        synchEntryCollection.Add(synchEntry);
                    }
                    count++;
                }
            }

            return synchEntryCollection;
        }

        private SynchEntry getSynchRecords(Navipro.SantaMonica.Common.Agent agent, int recordType, int lastEntryNo)
        {
            try
            {
                SynchronizationQueueEntry synchronizationQueueEntry = null;
                SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries(agent.code);
                if (recordType == -1)
                {
                    synchronizationQueueEntry = synchronizationQueueEntries.getFirstEntryEx(this.database, lastEntryNo);
                }
                else
                {
                    synchronizationQueueEntry = synchronizationQueueEntries.getFirstEntryEx(this.database, recordType, lastEntryNo);
                }
                SynchEntry synchEntry = null;
                if (synchronizationQueueEntry != null)
                {
                    synchEntry = new SynchEntry(synchronizationQueueEntry);
                    synchEntry = this.composeDataSet(agent, synchEntry);
                    return synchEntry;
                }
            }
            catch (Exception) { }

            return null;
        }
        private SynchEntry composeDataSet(Navipro.SantaMonica.Common.Agent agent, SynchEntry synchEntry)
        {
            if (synchEntry.action == 2)
            {
                return synchEntry;
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER)
            {
                ShipOrders shipOrders = new ShipOrders(agent.code);
                //shipOrders.setStatus(this.database, synchEntry.primaryKey, 4);
                ShipOrder shipOrder = shipOrders.getEntry(database, synchEntry.primaryKey);
                Konvex.SmartShipping.DataObjects.ShipOrderHeader shipOrderHeader = new Konvex.SmartShipping.DataObjects.ShipOrderHeader(shipOrder);
                shipOrderHeader.applyLines(database);
                synchEntry.shipOrderHeader = shipOrderHeader;
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_CUSTOMER)
            {
                Customers customers = new Customers();
                Customer dataCustomer = customers.getEntry(this.database, agent.getOrganization(this.database), synchEntry.primaryKey);
                if (dataCustomer != null)
                {
                    Konvex.SmartShipping.DataObjects.Customer customer = new Konvex.SmartShipping.DataObjects.Customer(dataCustomer);
                    synchEntry.customer = customer;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM)
            {
                Items items = new Items();
                Item dataItem = items.getEntry(this.database, synchEntry.primaryKey);
                if (dataItem != null)
                {
                    Konvex.SmartShipping.DataObjects.Item item = new Konvex.SmartShipping.DataObjects.Item(dataItem);
                    item.applyPrices(database);
                    synchEntry.item = item;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM_PRICE)
            {
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_MESSAGE)
            {
                Messages messages = new Messages();
                Navipro.SantaMonica.Common.Message dataMessage = messages.getEntry(this.database, agent.organizationNo, synchEntry.primaryKey);
                if (dataMessage != null)
                {
                    Konvex.SmartShipping.DataObjects.Message message = new Konvex.SmartShipping.DataObjects.Message(dataMessage);
                    synchEntry.message = message;
                    setMessageStatus(agent.code, int.Parse(synchEntry.primaryKey), 1);
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_ORGANIZATION)
            {
                Organizations organizations = new Organizations();
                Navipro.SantaMonica.Common.Organization dataOrganization = organizations.getOrganization(this.database, synchEntry.primaryKey);
                if (dataOrganization != null)
                {
                    Konvex.SmartShipping.DataObjects.Organization organization = new Konvex.SmartShipping.DataObjects.Organization(dataOrganization);
                    synchEntry.organization = organization;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM_PRICE_EXTENDED)
            {
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_MOBILE_USER)
            {
                MobileUsers mobileUsers = new MobileUsers();
                Navipro.SantaMonica.Common.MobileUser dataMobileUser = mobileUsers.getEntry(database, int.Parse(synchEntry.primaryKey));
                if (dataMobileUser != null)
                {
                    Konvex.SmartShipping.DataObjects.MobileUser mobileUser = new Konvex.SmartShipping.DataObjects.MobileUser(dataMobileUser);
                    synchEntry.mobileUser = mobileUser;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE)
            {
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE_ID)
            {
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_AGENT)
            {
                Agents agents = new Agents();
                Navipro.SantaMonica.Common.Agent dataAgent = agents.getAgent(this.database, synchEntry.primaryKey);
                if (dataAgent != null)
                {
                    if (dataAgent.enabled)
                    {
                        Konvex.SmartShipping.DataObjects.Agent synchAgent = new Konvex.SmartShipping.DataObjects.Agent(dataAgent);
                        synchAgent.description = synchAgent.code + " " + synchAgent.description;
                        synchEntry.agent = synchAgent;
                    }
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_ORGANIZATION_LOCATION)
            {
                OrganizationLocations organizationLocations = new OrganizationLocations();
                Navipro.SantaMonica.Common.OrganizationLocation dataOrgLocation = organizationLocations.getEntry(this.database, agent.organizationNo, synchEntry.primaryKey);
                if (dataOrgLocation != null)
                {
                    Konvex.SmartShipping.DataObjects.OrganizationLocation orgLocation = new Konvex.SmartShipping.DataObjects.OrganizationLocation(dataOrgLocation);
                    synchEntry.organizationLocation = orgLocation;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_CONTAINER)
            {
                Containers containers = new Containers();
                Navipro.SantaMonica.Common.Container dataContainer = containers.getEntry(this.database, synchEntry.primaryKey);
                if (dataContainer != null)
                {
                    Konvex.SmartShipping.DataObjects.Container container = new Konvex.SmartShipping.DataObjects.Container(dataContainer);
                    synchEntry.container = container;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_CATEGORY)
            {
                Categories categories = new Categories();
                Navipro.SantaMonica.Common.Category dataCategory = categories.getEntry(this.database, synchEntry.primaryKey);
                if (dataCategory != null)
                {
                    Konvex.SmartShipping.DataObjects.Category category = new Konvex.SmartShipping.DataObjects.Category(dataCategory);
                    synchEntry.category = category;
                }
            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_JOURNAL)
            {
                LineJournals lineJournals = new LineJournals();                
                LineJournal lineJournal = lineJournals.getEntry(database, synchEntry.primaryKey);
                Konvex.SmartShipping.DataObjects.LineJournal lineJournalObject = new Konvex.SmartShipping.DataObjects.LineJournal(lineJournal);
                //synchEntry.lineJournal = lineJournalObject;

            }
            if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_ORDER)
            {
                LineOrders lineOrders = new LineOrders();
                LineOrder lineOrder = lineOrders.getEntry(database, synchEntry.primaryKey);
                Konvex.SmartShipping.DataObjects.LineOrder lineOrderObject = new Konvex.SmartShipping.DataObjects.LineOrder(lineOrder);
                //synchEntry.lineOrder = lineOrderObject;

                lineOrderObject.applyLines(database);
            }
            return synchEntry;
        }

        public void ackSynchRecord(int[] synchEntryNoList)
        {
            try
            {
                SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries();

                foreach (int synchEntryNo in synchEntryNoList)
                {
                    SynchronizationQueueEntry entry = synchronizationQueueEntries.getEntry(this.database, synchEntryNo);
                    if (entry != null)
                    {
                        /*
                        if (entry.type == SynchronizationQueueEntries.SYNC_LINE_JOURNAL && entry.action < 2)
                        {
                            LineJournals lineJournals = new LineJournals();
                            LineJournal entry2 = lineJournals.getEntry(this.database, entry.primaryKey);
                            if (entry2 != null)
                            {
                                lineJournals.setStatus(this.database, entry.primaryKey, 5);
                            }
                        }
                         * */
                        entry.delete(this.database);
                    }
                }
                //SynchronizationHistoryEntries synchronizationHistoryEntries = new SynchronizationHistoryEntries();
                //synchronizationHistoryEntries.enqueue(this.database, entry);

                //synchronizationQueueEntries.deleteAllEntries(database, synchEntryNo);
            }
            catch (Exception) { }
        }

        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
        {
            setShipOrderStatus(agentCode, no, status, positionX, positionY, shipTime, "", 0);
        }

        public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, string comment, int distance)
        {
            try
            {
                if (status == 8) status = 1;
                if (status == 7) status = 2;

                if (status < 20)
                {
                    ServerLogging serverLogging = new ServerLogging(this.database);
                    serverLogging.log(agentCode, string.Concat(new object[]
					{
						"SetShipOrderStatus: [No] ",
						no,
						", [Status] ",
						status
					}));
                    ShipOrders shipOrders = new ShipOrders(agentCode);
                    ShipOrder agentEntry = shipOrders.getAgentEntry(this.database, agentCode, no);
                    if (agentEntry != null)
                    {
                        agentEntry.status = status;
                        if (status == 6)
                        {
                            Customers customers = new Customers();
                            customers.getEntry(this.database, agentEntry.organizationNo, agentEntry.customerNo);
                            agentEntry.positionX = positionX;
                            agentEntry.positionY = positionY;
                            agentEntry.closedDate = shipTime;
                            agentEntry.shipTime = shipTime;
                        }
                        agentEntry.log(this.database, agentCode, "Ändrat status till " + agentEntry.getStatusText());
                        if ((comment != null) && (comment != ""))
                        {
                            agentEntry.comments = comment;

                            agentEntry.log(this.database, agentCode, "Kommentar: " + comment);
                        }
                        agentEntry.save(this.database, false);

                    }

                    if (distance > 0)
                    {
                        database.nonQuery("INSERT INTO [Ship Order Distance] ([Ship Order No], [Distance], [Posting Date]) VALUES ('" + no + "', '" + distance + "', '"+System.DateTime.Today.ToString("yyyy-MM-dd 00:00:00")+"')");
                    }
                }
            }
            catch (Exception ex)
            {
                ServerLogging serverLogging2 = new ServerLogging(this.database);
                serverLogging2.log(agentCode, "SetShipOrderStatus: [No] " + no + ", [Message] " + ex.Message);
            }
        }
        public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            try
            {
                serverLogging.log(agentCode, "SetLineOrderStatus");
                LineOrders lineOrders = new LineOrders();
                LineOrder entry = lineOrders.getEntry(this.database, no);
                if (entry != null)
                {
                    LineJournal journal = entry.getJournal(this.database);
                    if (journal != null && journal.agentCode == agentCode)
                    {
                        entry.status = status;
                        if (status == 7)
                        {
                            Agents agents = new Agents();
                            Navipro.SantaMonica.Common.Agent agent = agents.getAgent(this.database, agentCode);
                            if (journal.status == 5)
                            {
                                journal.status = 6;
                            }
                            if (entry.positionX == 0 && entry.positionY == 0)
                            {
                                entry.positionX = positionX;
                                entry.positionY = positionY;
                            }
                            entry.isLoaded = true;
                            entry.driverName = agent.userName;
                            entry.loadWaitTime = loadWaitTime;
                            entry.closedDateTime = new DateTime(shipTime.Year, shipTime.Month, shipTime.Day, shipTime.Hour, shipTime.Minute, shipTime.Second);
                            entry.save(this.database, false);
                            ShippingCustomers shippingCustomers = new ShippingCustomers();
                            ShippingCustomer entry2 = shippingCustomers.getEntry(this.database, entry.shippingCustomerNo);
                            if (entry2 != null && entry2.positionX == 0 && entry2.positionY == 0)
                            {
                                entry2.positionX = positionX;
                                entry2.positionY = positionY;
                                entry2.save(this.database);
                            }
                            journal.save(this.database);
                            serverLogging.log(agentCode, "SetLineOrderStatus: Status: " + entry.status + ", RecalcArrivalTime Begin");
                            journal.recalculateArrivalTime(this.database);
                            serverLogging.log(agentCode, "SetLineOrderStatus: RecalcArrivalTime Done");
                        }
                        else
                        {
                            if (entry.isLoaded)
                            {
                                entry.status = 7;
                            }
                            entry.save(this.database, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                serverLogging.log(agentCode, "SetLineOrderStatus: [No] " + no + ", [Message] " + ex.Message);
            }
        }
        
        public void reportLineJournal(string agentCode, Konvex.SmartShipping.DataObjects.LineJournal lineJournal)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            serverLogging.log(agentCode, "ReportLineJournal");
            
            try
            {
                if (lineJournal != null)
                {
                    LineJournals lineJournals = new LineJournals();
                    lineJournals.reportJournal(this.database, agentCode, lineJournal.entryNo.ToString(), lineJournal.status, (int)lineJournal.measuredDistance, (int)lineJournal.reportedDistance, (int)lineJournal.reportedDistanceSingle, (int)lineJournal.reportedDistanceTrailer, (int)lineJournal.dropWaitTime);
                }
            }
            catch (Exception ex)
            {
                serverLogging.log(agentCode, "ReportLineJournal: " + ex.Message);
            }
            
            
        }

        public void setFactoryOrderStatus(string agentCode, DataSet factoryOrderDataSet)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            try
            {
                serverLogging.log(agentCode, "SetFactoryOrderStatus!");
                serverLogging.log(agentCode, "FactoryOrderEntryNo: " + factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                FactoryOrders factoryOrders = new FactoryOrders();
                FactoryOrder entry = factoryOrders.getEntry(this.database, factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                if (entry != null)
                {
                    if (entry.status != int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                    {
                        Agents agents = new Agents();
                        Navipro.SantaMonica.Common.Agent agent = agents.getAgent(this.database, agentCode);
                        entry.status = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(29).ToString());
                        if (entry.status == 3)
                        {
                            serverLogging.log(agentCode, "Pickup");
                            entry.driverName = agent.userName;
                            DateTime dateTime = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                            DateTime dateTime2 = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(31).ToString());
                            entry.pickupDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime2.Hour, dateTime2.Minute, dateTime2.Second);
                            entry.quantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
                            entry.loadDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(36).ToString());
                            entry.loadWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(37).ToString());
                            entry.phValueShipping = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(40).ToString().Replace(".", ","));
                            if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 41)
                            {
                                entry.loadReasonValue = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(41).ToString());
                                entry.loadReasonText = factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();
                            }
                            if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 45)
                            {
                                entry.extraDist = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(45).ToString());
                                entry.extraTime = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(46).ToString());
                            }
                            if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 47)
                            {
                                entry.agentCleaningStatus = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(47).ToString());
                                entry.agentCleaningComment = factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(48).ToString();
                            }
                            entry.save(this.database, false);
                            int arg_445_0 = entry.factoryType;
                        }
                        if (entry.status == 4)
                        {
                            serverLogging.log(agentCode, "Drop");
                            entry.dropDriverName = agent.userName;
                            if (entry.consumerPositionX == 0 && entry.consumerPositionY == 0)
                            {
                                entry.consumerPositionX = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());
                                entry.consumerPositionY = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());
                            }
                            entry.quantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
                            entry.closedDateTime = entry.arrivalDateTime;
                            entry.realQuantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
                            entry.consumerLevel = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(35).ToString());
                            entry.loadDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(36).ToString());
                            entry.loadWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(37).ToString());
                            entry.dropDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(38).ToString());
                            entry.dropWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(39).ToString());
                            entry.phValueShipping = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(40).ToString().Replace(".", ","));
                            if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 43)
                            {
                                entry.dropReasonValue = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(43).ToString());
                                entry.dropReasonText = factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(44).ToString();
                            }
                            if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 45)
                            {
                                entry.extraDist = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(45).ToString());
                                entry.extraTime = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(46).ToString());
                            }
                            entry.navisionStatus = 1;
                            entry.save(this.database, false, false);
                            serverLogging.log(agentCode, "Setting arrivaltime");
                            entry.setArrivalTime(this.database, DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(33).ToString()));
                            serverLogging.log(agentCode, "Done setting arrivaltime");
                            Consumers consumers = new Consumers();
                            Consumer entry2 = consumers.getEntry(this.database, entry.consumerNo);
                            if (entry2 != null && entry2.positionX == 0 && entry2.positionY == 0)
                            {
                                entry2.positionX = entry.consumerPositionX;
                                entry2.positionY = entry.consumerPositionY;
                                entry2.save(this.database);
                            }
                            serverLogging.log(agentCode, string.Concat(new object[]
							{
								"SetFactoryOrderStatus: No: ",
								entry.entryNo,
								", Status: ",
								entry.status
							}));
                            ConsumerInventories consumerInventories = new ConsumerInventories();
                            consumerInventories.setActualInventory(this.database, entry);
                            serverLogging.log(agentCode, "SetFactoryOrderStatus: Recalculating inventory");
                            entry.save(this.database, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                serverLogging.log(agentCode, "SetFactoryOrderStatus: [Agent] " + agentCode + ", [Message] " + ex.Message);
            }
        }
        public void createShipment(string agentCode, Konvex.SmartShipping.DataObjects.ShipmentHeader fromShipmentHeader)
        {
            if (fromShipmentHeader == null)
            {
                ServerLogging serverLogging = new ServerLogging(this.database);
                serverLogging.log(agentCode, "CreateShipment: Empty dataset... Deleting...");
                return;
            }
            try
            {
                ServerLogging serverLogging2 = new ServerLogging(this.database);
                Navipro.SantaMonica.Common.ShipmentHeader shipmentHeader = new Navipro.SantaMonica.Common.ShipmentHeader(database, fromShipmentHeader.agentCode + "-" + fromShipmentHeader.no);

                if (fromShipmentHeader.agentCode + "-" + fromShipmentHeader.no == "CLX-20750")
                {
                    return;
                }

                shipmentHeader.no = fromShipmentHeader.agentCode + "-" + fromShipmentHeader.no;
                shipmentHeader.organizationNo = fromShipmentHeader.organizationNo;
                shipmentHeader.shipDate = fromShipmentHeader.shipDate;
                shipmentHeader.customerNo = fromShipmentHeader.customerNo;
                shipmentHeader.customerName = fromShipmentHeader.customerName;
                shipmentHeader.address = fromShipmentHeader.address;
                shipmentHeader.address2 = fromShipmentHeader.address2;
                shipmentHeader.postCode = fromShipmentHeader.postCode;
                shipmentHeader.city = fromShipmentHeader.city;
                shipmentHeader.countryCode = fromShipmentHeader.countryCode;
                shipmentHeader.phoneNo = fromShipmentHeader.phoneNo;
                shipmentHeader.cellPhoneNo = fromShipmentHeader.cellPhoneNo;
                shipmentHeader.productionSite = fromShipmentHeader.productionSite;
                shipmentHeader.positionX = fromShipmentHeader.positionX;
                shipmentHeader.positionY = fromShipmentHeader.positionY;
                shipmentHeader.payment = fromShipmentHeader.payment;
                shipmentHeader.dairyCode = fromShipmentHeader.dairyCode;
                shipmentHeader.dairyNo = fromShipmentHeader.dairyNo;
                shipmentHeader.reference = fromShipmentHeader.reference;
                shipmentHeader.userName = fromShipmentHeader.userName;
                shipmentHeader.containerNo = fromShipmentHeader.containerNo;
                shipmentHeader.shipOrderEntryNo = fromShipmentHeader.shipOrderEntryNo;
                shipmentHeader.customerShipAddressNo = fromShipmentHeader.customerShipAddressNo;
                shipmentHeader.shipName = fromShipmentHeader.shipName;
                shipmentHeader.shipAddress = fromShipmentHeader.shipAddress;
                shipmentHeader.shipAddress2 = fromShipmentHeader.shipAddress2;
                shipmentHeader.shipPostCode = fromShipmentHeader.shipPostCode;
                shipmentHeader.shipCity = fromShipmentHeader.shipCity;
                shipmentHeader.invoiceNo = fromShipmentHeader.invoiceNo;
                shipmentHeader.surfaceNotCorrect = fromShipmentHeader.surfaceNotCorrect;
                shipmentHeader.agentCode = fromShipmentHeader.agentCode;

                if (shipmentHeader.phoneNo.Length > 20)
                {
                    shipmentHeader.phoneNo = shipmentHeader.phoneNo.Substring(1, 20);
                }
                if (shipmentHeader.cellPhoneNo.Length > 20)
                {
                    shipmentHeader.cellPhoneNo = shipmentHeader.cellPhoneNo.Substring(1, 20);
                }
                if (shipmentHeader.customerName.Length > 50)
                {
                    shipmentHeader.customerName = shipmentHeader.customerName.Substring(1, 50);
                }
                if (shipmentHeader.address.Length > 50)
                {
                    shipmentHeader.address = shipmentHeader.address.Substring(1, 50);
                }
                if (shipmentHeader.address2.Length > 50)
                {
                    shipmentHeader.address2 = shipmentHeader.address2.Substring(1, 50);
                }
                if (shipmentHeader.city.Length > 50)
                {
                    shipmentHeader.city = shipmentHeader.city.Substring(1, 50);
                }

                shipmentHeader.save(this.database);

                int i = 0;
                while (i < fromShipmentHeader.shipmentLineCollection.Count)
                {

                    Navipro.SantaMonica.Common.ShipmentLine shipmentLine = new Navipro.SantaMonica.Common.ShipmentLine();
                    shipmentLine.originalEntryNo = fromShipmentHeader.shipmentLineCollection[i].entryNo;
                    shipmentLine.shipmentNo = shipmentHeader.no;
                    shipmentLine.itemNo = fromShipmentHeader.shipmentLineCollection[i].itemNo;
                    shipmentLine.description = fromShipmentHeader.shipmentLineCollection[i].description;
                    shipmentLine.quantity = fromShipmentHeader.shipmentLineCollection[i].quantity;
                    shipmentLine.connectionQuantity = fromShipmentHeader.shipmentLineCollection[i].connectionQuantity;
                    shipmentLine.unitPrice = fromShipmentHeader.shipmentLineCollection[i].unitPrice;
                    shipmentLine.amount = fromShipmentHeader.shipmentLineCollection[i].amount;
                    shipmentLine.connectionUnitPrice = fromShipmentHeader.shipmentLineCollection[i].connectionUnitPrice;
                    shipmentLine.connectionAmount = fromShipmentHeader.shipmentLineCollection[i].connectionAmount;
                    shipmentLine.totalAmount = fromShipmentHeader.shipmentLineCollection[i].totalAmount;
                    shipmentLine.connectionItemNo = fromShipmentHeader.shipmentLineCollection[i].connectionItemNo;
                    shipmentLine.extraPayment = fromShipmentHeader.shipmentLineCollection[i].extraPayment;
                    shipmentLine.testQuantity = fromShipmentHeader.shipmentLineCollection[i].testQuantity;

                    shipmentLine.agentCode = fromShipmentHeader.agentCode;


                    Items items = new Items();
                    Navipro.SantaMonica.Common.Item entry = items.getEntry(database, shipmentLine.itemNo);
                    if (entry.invoiceToJbv)
                    {
                        shipmentLine.totalAmount = shipmentLine.connectionAmount;
                        shipmentLine.unitPrice = 0f;
                        shipmentLine.amount = 0f;
                    }

                    shipmentLine.save(database);

                    int j = 0;
                    while (j < fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection.Count)
                    {
                        ShipmentLineId shipmentLineId = new ShipmentLineId();

                        shipmentLineId.entryNo = 0;
                        shipmentLineId.originalEntryNo = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].entryNo;
                        shipmentLineId.shipmentNo = shipmentHeader.no;
                        shipmentLineId.shipmentLineEntryNo = shipmentLine.entryNo;
                        shipmentLineId.unitId = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].unitId;
                        shipmentLineId.type = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].type;
                        shipmentLineId.reMarkUnitId = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].reMarkUnitId;
                        shipmentLineId.bseTesting = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].bseTesting;
                        shipmentLineId.postMortem = fromShipmentHeader.shipmentLineCollection[i].shipmentLineIdCollection[j].postMortem;
                        shipmentLineId.save(database);            

                        j++;
                    }

                    i++;
                }


                shipmentHeader.setStatus(this.database, 1);
                
                ShipmentContainerQueueEntries shipmentContainerQueueEntries = new ShipmentContainerQueueEntries();
                shipmentContainerQueueEntries.enqueue(this.database, shipmentHeader.no, shipmentHeader.containerNo);
                if (shipmentHeader.customerShipAddressNo == "NEW")
                {
                    CustomerShipAddress customerShipAddress = new CustomerShipAddress();
                    customerShipAddress.name = shipmentHeader.shipName;
                    customerShipAddress.address = shipmentHeader.shipAddress;
                    customerShipAddress.address2 = shipmentHeader.shipAddress2;
                    customerShipAddress.postCode = shipmentHeader.shipPostCode;
                    customerShipAddress.city = shipmentHeader.shipCity;
                    customerShipAddress.productionSite = shipmentHeader.productionSite;
                    customerShipAddress.organizationNo = shipmentHeader.organizationNo;
                    customerShipAddress.customerNo = shipmentHeader.customerNo;
                    customerShipAddress.positionX = shipmentHeader.positionX;
                    customerShipAddress.positionY = shipmentHeader.positionY;
                    customerShipAddress.save(this.database);
                    shipmentHeader.customerShipAddressNo = customerShipAddress.entryNo;
                    shipmentHeader.save(this.database);
                }
                if (shipmentHeader.positionX > 0 && shipmentHeader.positionY > 0)
                {
                    if (shipmentHeader.customerShipAddressNo != "" && shipmentHeader.customerShipAddressNo != null)
                    {
                        this.database.nonQuery(string.Concat(new object[]
						{
							"UPDATE [Customer Ship Address] SET [Position X] = '",
							shipmentHeader.positionX,
							"', [Position Y] = '",
							shipmentHeader.positionY,
							"' WHERE [Entry No] = '",
							shipmentHeader.customerShipAddressNo,
							"' AND [Position X] = 0"
						}));
                    }
                    else
                    {
                        this.database.nonQuery(string.Concat(new object[]
						{
							"UPDATE [Customer] SET [Position X] = '",
							shipmentHeader.positionX,
							"', [Position Y] = '",
							shipmentHeader.positionY,
							"' WHERE [No] = '",
							shipmentHeader.customerNo,
							"' AND [Position X] = 0"
						}));
                    }
                }
                if (shipmentHeader.shipOrderEntryNo > 0)
                {
                    ShipOrders shipOrders = new ShipOrders();
                    ShipOrder entry = shipOrders.getEntry(this.database, shipmentHeader.organizationNo, shipmentHeader.shipOrderEntryNo.ToString());
                    if (entry != null && entry.status < 6)
                    {
                        entry.status = 6;
                        entry.save(this.database, false);
                    }
                }
                serverLogging2.log(agentCode, "CreateShipment: DONE, "+shipmentHeader.no);
            }
            catch (Exception ex)
            {
                ServerLogging serverLogging3 = new ServerLogging(this.database);
                serverLogging3.log(agentCode, "CreateShipment: ERROR");
                throw new Exception(ex.Message);
            }
        }
        public void createOrder(string agentCode, Konvex.SmartShipping.DataObjects.OrderHeader fromOrderHeader)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            try
            {
                serverLogging.log(agentCode, "CreateOrder: Organization [" + fromOrderHeader.organizationNo + "], Customer [" + fromOrderHeader.customerNo + "]");

                if (fromOrderHeader.customerNo == "")
                {
                    serverLogging.log(agentCode, "CreateOrder: Missing customer no. Exiting.");
                    return;
                }

                Customers customers = new Customers();
                Customer customer = customers.getEntry(database, fromOrderHeader.organizationNo, fromOrderHeader.customerNo);


                if (customer != null)
                {
                    ShipOrder shipOrder = new ShipOrder(fromOrderHeader.organizationNo);
                    shipOrder.applySellToCustomer(customer);
                    shipOrder.creationDate = DateTime.Today;
                    shipOrder.shipDate = fromOrderHeader.shipDate;
                    shipOrder.paymentType = fromOrderHeader.paymentType;
                    shipOrder.shipName = fromOrderHeader.shipName;
                    shipOrder.shipAddress = fromOrderHeader.shipAddress;
                    shipOrder.shipAddress2 = fromOrderHeader.shipAddress2;
                    shipOrder.shipPostCode = fromOrderHeader.shipPostCode;
                    shipOrder.shipCity = fromOrderHeader.shipCity;
                    shipOrder.createdBy = 3;
                    shipOrder.comments = fromOrderHeader.comments;
                    shipOrder.agentCode = "";
                    shipOrder.countryCode = "";

                    shipOrder.save(this.database, false);

                    int i = 0;
                    while (i < fromOrderHeader.orderLineCollection.Count)
                    {
                        ShipOrderLine shipOrderLine = new ShipOrderLine(shipOrder);
                        shipOrderLine.originalEntryNo = fromOrderHeader.orderLineCollection[i].entryNo;
                        shipOrderLine.itemNo = fromOrderHeader.orderLineCollection[i].itemNo;
                        shipOrderLine.quantity = fromOrderHeader.orderLineCollection[i].quantity;
                        shipOrderLine.unitPrice = fromOrderHeader.orderLineCollection[i].unitPrice;
                        shipOrderLine.amount = fromOrderHeader.orderLineCollection[i].amount;
                        shipOrderLine.connectionItemNo = fromOrderHeader.orderLineCollection[i].connectionItemNo;
                        shipOrderLine.connectionQuantity = fromOrderHeader.orderLineCollection[i].connectionQuantity;
                        shipOrderLine.connectionUnitPrice = fromOrderHeader.orderLineCollection[i].connectionUnitPrice;
                        shipOrderLine.connectionAmount = fromOrderHeader.orderLineCollection[i].connectionAmount;
                        shipOrderLine.totalAmount = fromOrderHeader.orderLineCollection[i].totalAmount;
                        shipOrderLine.save(database);

                        int j = 0;
                        while (j < fromOrderHeader.orderLineCollection[i].orderLineIdCollection.Count)
                        {
                            ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
                            shipOrderLineId.shipOrderLineEntryNo = shipOrderLine.entryNo;
                            shipOrderLineId.unitId = fromOrderHeader.orderLineCollection[i].orderLineIdCollection[j].unitId;
                            shipOrderLineId.postMortem = fromOrderHeader.orderLineCollection[i].orderLineIdCollection[j].postMortem;
                            shipOrderLineId.bseTesting = fromOrderHeader.orderLineCollection[i].orderLineIdCollection[j].bseTesting;
                            shipOrderLineId.save(database);

                            j++;
                        }

                        i++;
                    }

                    shipOrder.log(this.database, agentCode, "Körorder skapad.");

                    serverLogging.log(agentCode, "CreateOrder: Updating details, " + shipOrder.entryNo.ToString());
                    shipOrder.updateDetails(this.database);
                    shipOrder.save(database);

                    serverLogging.log(agentCode, "CreateOrder: Details done.");

                    if ((fromOrderHeader.agentCode != "") && (fromOrderHeader.agentCode != "-"))
                    {
                        serverLogging.log(agentCode, "CreateOrder: Assigning.");
                        shipOrder.assignOrder(this.database, fromOrderHeader.agentCode, agentCode);
                        serverLogging.log(agentCode, "CreateOrder: Asign done.");
                    }
                }
                else
                {
                    string message = "CreateOrder: [" + fromOrderHeader.organizationNo + "][" + fromOrderHeader.customerNo + "] Customer does not exist.";
                    serverLogging.log(agentCode, message);
                    throw new Exception(message);

                }
            }
            catch (Exception ex)
            {
                serverLogging.log(agentCode, "CreateOrder: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void setMessageStatus(string agentCode, int messageEntryNo, int status)
        {
            try
            {
                Agents agents = new Agents();
                Navipro.SantaMonica.Common.Agent agent = agents.getAgent(this.database, agentCode);
                if (agent != null)
                {
                    MessageAgents messageAgents = new MessageAgents();
                    MessageAgent entry = messageAgents.getEntry(this.database, messageEntryNo, agentCode);
                    if (entry != null)
                    {
                        entry.status = status;
                        if (status == 2)
                        {
                            entry.ackDateTime = DateTime.Now;
                        }
                        entry.save(this.database);
                    }
                }
            }
            catch (Exception) { }
        }
        public void assignShipOrder(string agentCode, string no, string newAgentCode)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            try
            {
                if (newAgentCode == "-")
                {
                    newAgentCode = "";
                }
                ShipOrders shipOrders = new ShipOrders(agentCode);
                ShipOrder entry = shipOrders.getEntry(this.database, no);

                serverLogging.log(agentCode, "AssignShipOrder: [" + no + "], New agent: " + newAgentCode);

                if (entry != null && entry.status < 6)
                {
                    entry.assignOrder(this.database, newAgentCode, agentCode);
                }
                SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries();
                synchronizationQueueEntries.enqueue(this.database, agentCode, 0, no, 2);
            }
            catch (Exception ex)
            {

                serverLogging.log(agentCode, string.Concat(new string[]
				{
					"AssignShipOrder: [No] ",
					no,
					", [Agent Code] ",
					newAgentCode,
					", [Message] ",
					ex.Message
				}));
            }
        }
        public void dispose()
        {
            this.database.close();
        }
        public void reportError(string agentCode, string message)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            serverLogging.log(agentCode, "Agent Error: " + message);
        }

        private void checkItemPriceUpdates(string agentCode)
        {
            AgentItemPriceUpdates agentItemPriceUpdates = new AgentItemPriceUpdates();
            DataSet dataSet = agentItemPriceUpdates.getDataSet(this.database, agentCode);
            string result;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                AgentItemPriceUpdate agentItemPriceUpdate = new AgentItemPriceUpdate(dataSet.Tables[0].Rows[0]);

                SynchronizationQueueEntries syncEntries = new SynchronizationQueueEntries();
                syncEntries.enqueue(database, agentCode, 2, agentItemPriceUpdate.itemNo, 0);

                agentItemPriceUpdate.delete(database);
            }
        }

        public void acknowledgePriceUpdate(string agentCode, string itemNo, float checksum)
        {
            try
            {
                AgentItemPriceUpdates agentItemPriceUpdates = new AgentItemPriceUpdates();
                agentItemPriceUpdates.acknowledge(this.database, agentCode, itemNo, checksum);
            }
            catch (Exception) { }
        }
        private void directUnloadOfLineOrder(ContainerEntry containerEntry)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            if (containerEntry.sourceType == 0)
            {
                serverLogging.log(containerEntry.sourceCode, "Direct unload of line order");
                LineOrders lineOrders = new LineOrders();
                LineOrder containerLineOrder = lineOrders.getContainerLineOrder(this.database, containerEntry.containerNo);
                if (containerLineOrder != null)
                {
                    serverLogging.log(containerEntry.sourceCode, "Line order: " + containerLineOrder.entryNo);
                    LineJournals lineJournals = new LineJournals();
                    LineJournal lineJournal = lineJournals.createJournal(this.database, containerEntry.sourceCode, containerEntry.entryDateTime);
                    if (lineJournal != null)
                    {
                        int lineJournalEntryNo = containerLineOrder.lineJournalEntryNo;
                        containerLineOrder.lineJournalEntryNo = lineJournal.entryNo;
                        containerLineOrder.isLoaded = true;
                        containerLineOrder.status = 10;
                        containerLineOrder.shipTime = containerEntry.entryDateTime;
                        containerLineOrder.createdByCode = "DIRECT";
                        containerLineOrder.save(this.database, false);

                        Factories factories = new Factories();
                        Factory factory = factories.getEntry(database, lineJournal.arrivalFactoryCode);
                        if (factory.factoryTypeCode != "KONVEX")
                        {
                            if (containerEntry.weight > 0f)
                            {
                                LineOrderContainers lineOrderContainers = new LineOrderContainers();
                                LineOrderContainer entry = lineOrderContainers.getEntry(this.database, containerLineOrder.entryNo, containerEntry.containerNo);
                                if (entry != null)
                                {
                                    entry.realWeight = containerEntry.weight / 1000f;
                                    entry.isScaled = true;
                                    entry.isSentToScaling = true;
                                    entry.scaledDateTime = containerEntry.entryDateTime;
                                    entry.save(this.database);
                                }
                            }
                        }
                        if (lineJournalEntryNo > 0)
                        {
                            LineJournal entry2 = lineJournals.getEntry(this.database, lineJournalEntryNo.ToString());
                            if (entry2 != null)
                            {
                                entry2.status = 1;
                                entry2.save(this.database);
                                SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries();
                                synchronizationQueueEntries.enqueue(this.database, entry2.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, containerLineOrder.entryNo.ToString(), 2);
                            }
                        }
                        lineJournal.agentCode = containerEntry.sourceCode;
                        lineJournal.arrivalFactoryCode = containerEntry.locationCode;
                        lineJournal.status = 8;
                        lineJournal.save(this.database);
                        serverLogging.log(containerEntry.sourceCode, "Line Journal created: " + lineJournal.entryNo);
                        containerEntry.documentType = 2;
                        containerEntry.documentNo = lineJournal.entryNo.ToString();
                        containerEntry.save(this.database);
                    }
                }
            }
        }

        public int fetchLastInvoiceNo(string agentCode)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            serverLogging.log(agentCode, "Fetching last invoice no.");

            try
            {
                int invoiceNo = 0;

                System.Data.SqlClient.SqlDataReader dataReader = database.query("SELECT TOP 1 [Invoice No] FROM [Shipment Header] WHERE [Agent Code] = '" + agentCode.Replace("'", "").Replace("\"", "") + "' AND [Invoice No] <> '' ORDER BY [Invoice No] DESC");
                if (dataReader.Read())
                {
                    string invoiceNoStr = dataReader.GetValue(0).ToString();
                    dataReader.Close();
                    serverLogging.log(agentCode, "Invoice no: " + invoiceNoStr);
                    invoiceNoStr = invoiceNoStr.Substring(3);
                    invoiceNoStr = invoiceNoStr.Substring(agentCode.Length);
                    invoiceNo = int.Parse(invoiceNoStr);
                    return invoiceNo;
                }
                dataReader.Close();

            }
            catch (Exception e)
            {
                serverLogging.log(agentCode, "Exception: " + e.Message);
            }

            return 0;
        }

        public void initAgentDatabase(string agentCode)
        {
            Agents agents = new Agents();
            Agent agent = agents.getAgent(database, agentCode);

            SynchronizationQueueEntries synchronizationQueueEntries = new SynchronizationQueueEntries();

            synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_ORGANIZATION, agent.organizationNo, 0);

            DataSet agentDataSet = agents.getDataSet(database, agent.organizationNo);
            int i = 0;
            while (i < agentDataSet.Tables[0].Rows.Count)
            {
                Agent subAgent = new Agent(agentDataSet.Tables[0].Rows[i]);
                synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_AGENT, subAgent.code, 0);

                i++;
            }

            MobileUsers mobileUsers = new MobileUsers();
            DataSet userDataSet = mobileUsers.getDataSet(database, agent.organizationNo);
            i = 0;
            while (i < userDataSet.Tables[0].Rows.Count)
            {
                synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_MOBILE_USER, userDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 0);

                i++;
            }

            OrganizationLocations organizationLocations = new OrganizationLocations();
            DataSet organizationLocationDataSet = organizationLocations.getDataSet(database, agent.organizationNo);
            i = 0;
            while (i < organizationLocationDataSet.Tables[0].Rows.Count)
            {
                synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_ORGANIZATION_LOCATION, organizationLocationDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), 0);

                i++;
            }

            Containers containers = new Containers();
            DataSet containerDataSet = containers.getDataSet(database);
            i = 0;
            while (i < containerDataSet.Tables[0].Rows.Count)
            {
                Container container = new Container(containerDataSet.Tables[0].Rows[i]);
                synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_CONTAINER, container.no, 0);

                i++;
            }

            Categories categories = new Categories();
            DataSet categoryDataSet = categories.getDataSet(database);
            i = 0;
            while (i < categoryDataSet.Tables[0].Rows.Count)
            {
                synchronizationQueueEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_CATEGORY, categoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 0);

                i++;
            }


        }


        public void write(string message, int type)
        {
        }

        public void updateDetails(Database database, ShipOrder shipOrder)
        {
            shipOrder.checkAdminFee(database);
            ShipOrderLines shipOrderLines = new ShipOrderLines();
            ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds();
            DataSet dataSet = shipOrderLines.getDataSet(database, shipOrder.entryNo);
            Items items = new Items();
            int i = 0;
            shipOrder.details = "";
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Item entry = items.getEntry(database, dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                int num = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                int num2 = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString());
                shipOrder.details = string.Concat(new string[]
				{
					shipOrder.details,
					num.ToString(),
					" ",
					entry.unitOfMeasure,
					" ",
					entry.description,
					" "
				});
                if (num2 > 0)
                {
                    shipOrder.details = string.Concat(new object[]
					{
						shipOrder.details,
						"(",
						num2,
						"A) "
					});
                }
                DataSet dataSet2 = shipOrderLineIds.getDataSet(database, shipOrder.entryNo, int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
                int j = 0;
                string text = "";
                int num3 = 0;
                if (j < dataSet2.Tables[0].Rows.Count)
                {
                    text += "ID: ";
                    bool flag = true;
                    while (j < dataSet2.Tables[0].Rows.Count)
                    {
                        if (dataSet2.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1")
                        {
                            num3++;
                        }
                        if (flag)
                        {
                            text += dataSet2.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
                            if (dataSet2.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1")
                            {
                                text += "(P)";
                            }
                            if (dataSet2.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() == "1")
                            {
                                text += "(O)";
                            }
                            flag = false;
                        }
                        else
                        {
                            text = text + ", " + dataSet2.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
                            if (dataSet2.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1")
                            {
                                text += "(P)";
                            }
                            if (dataSet2.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() == "1")
                            {
                                text += "(O)";
                            }
                        }
                        j++;
                    }
                }
                if (num3 > 0)
                {
                    shipOrder.details = string.Concat(new object[]
					{
						shipOrder.details,
						"(",
						num3,
						"P) "
					});
                }
                shipOrder.details = shipOrder.details + text + "; ";
                i++;
            }
            if (shipOrder.details.IndexOf("(P)") > 0 && shipOrder.comments.Length > 0 && shipOrder.comments[0] != 'X')
            {
                shipOrder.comments = "X" + shipOrder.comments;
            }
            if (shipOrder.details.IndexOf("(O)") > 0 && shipOrder.comments.Length > 0 && shipOrder.comments[0] != 'X')
            {
                shipOrder.comments = "X" + shipOrder.comments;
            }
            if (shipOrder.details.IndexOf("A)") > 0 && shipOrder.comments.Length > 0 && shipOrder.comments[0] != 'X')
            {
                shipOrder.comments = "X" + shipOrder.comments;
            }
            if (shipOrder.details.Length > 200)
            {
                shipOrder.details = shipOrder.details.Substring(1, 197) + "...";
            }
        }

        public void reportContainerForService(string agentCode, string containerNo)
        {
            Containers containers = new Containers();
            Container container = containers.getEntry(database, containerNo);
            if (container != null)
            {
                container.reportService(database, 0, agentCode, 0, "");

            }

        }

        public Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection getDistancesByDate(DateTime fromDate, DateTime toDate)
        {
            Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection collection = new Konvex.SmartShipping.DataObjects.ShipOrderDistanceCollection();

            System.Data.SqlClient.SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Ship Order No], [Distance], [Posting Date] FROM [Ship Order Distance] WHERE [Posting Date] >= '" + fromDate.ToString("yyyy-MM-dd") + "' AND [Posting Date] <= '" + toDate.ToString("yyyy-MM-dd") + "'");
            System.Data.DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Konvex.SmartShipping.DataObjects.ShipOrderDistance shipOrderDistance = new Konvex.SmartShipping.DataObjects.ShipOrderDistance(dataSet.Tables[0].Rows[i]);
                collection.Add(shipOrderDistance);

                i++;
            }

            return collection;
        }


        public Konvex.SmartShipping.DataObjects.LineOrderCollection getLineOrders(string agentCode)
        {
            Konvex.SmartShipping.DataObjects.LineOrderCollection lineOrderCollection = new Konvex.SmartShipping.DataObjects.LineOrderCollection();

            LineOrders lineOrders = new LineOrders();
            DataSet dataSet = lineOrders.getActiveDataSet(database, agentCode, DateTime.Today.AddDays(-3), DateTime.Today.AddDays(10));

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                LineOrder lineOrder = new LineOrder(dataSet.Tables[0].Rows[i]);
                Konvex.SmartShipping.DataObjects.LineOrder lineOrderObject = new Konvex.SmartShipping.DataObjects.LineOrder(lineOrder);
                lineOrderObject.applyLines(database);

                lineOrderCollection.Add(lineOrderObject);

                i++;
            }

            return lineOrderCollection;
        }

        public Konvex.SmartShipping.DataObjects.LineJournalCollection getLineJournals(string agentCode, DateTime shipDate)
        {
            Konvex.SmartShipping.DataObjects.LineJournalCollection lineJournalCollection = new Konvex.SmartShipping.DataObjects.LineJournalCollection();

            LineJournals lineJournals = new LineJournals();
            ArrayList list = lineJournals.getJournals(database, agentCode, shipDate);

            int i = 0;
            while (i < list.Count)
            {
                Konvex.SmartShipping.DataObjects.LineJournal lineJournalObject = new Konvex.SmartShipping.DataObjects.LineJournal((LineJournal)list[i]);
                lineJournalCollection.Add(lineJournalObject);

                i++;
            }

            return lineJournalCollection;
        }

        public void setLineOrderContainers(string agentCode, string no, List<string> containerStringList)
        {
            ServerLogging serverLogging = new ServerLogging(this.database);
            try
            {
                List<string> containerStringListToCorrect = new List<string>();
                

                serverLogging.log(agentCode, "SetLineOrderContainers");
                LineOrders lineOrders = new LineOrders();
                LineOrder entry = lineOrders.getEntry(this.database, no);
                if (entry == null)
                {
                    throw new Exception("Illegal line order no " + no + ", agent " + agentCode);
                }



                LineJournal journal = entry.getJournal(this.database);
                if (journal != null && journal.agentCode == agentCode)
                {
                    DataSet containerDataSet = entry.getContainers(database);
                    foreach (string containerNo in containerStringList)
                    {
                        bool found = false;
                        int i = 0;
                        while (i < containerDataSet.Tables[0].Rows.Count)
                        {
                            if (containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() == containerNo)
                            {
                                serverLogging.log(agentCode, "Container " + containerNo + " OK");
                                found = true;
                            }
                            i++;
                        }
                        if (!found)
                        {
                                containerStringListToCorrect.Add(containerNo);
                                serverLogging.log(agentCode, "Container " + containerNo + " added.");
                        }
                    }

                    if (containerStringListToCorrect.Count == 0)
                    {
                        serverLogging.log(agentCode, "All containers match!");

                        return; //All containers match!
                    }
                    else
                    {
                        serverLogging.log(agentCode, "All containers didnt match.");
                    }


                    foreach (string containerNo in containerStringListToCorrect)
                    {
                        serverLogging.log(agentCode, "Checking container " + containerNo + ".");

                        LineOrder containerLineOrder = lineOrders.getContainerLineOrder(database, containerNo);
                        if (containerLineOrder == null)
                        {
                            serverLogging.log(agentCode, "Creating new line order line.");

                            LineOrderContainer lineOrderContainer = new LineOrderContainer(entry);
                            lineOrderContainer.containerNo = containerNo;
                            lineOrderContainer.save(database);


                        }
                        else
                        {
                            serverLogging.log(agentCode, "Moving container from another line order: " + containerLineOrder.entryNo);

                            if (containerLineOrder.shippingCustomerNo != entry.shippingCustomerNo)
                            {
                                throw new Exception("Shipping Customer No must be " + entry.shippingCustomerNo + " on lineOrder " + containerLineOrder.entryNo + ".");
                            }

                            database.nonQuery("UPDATE [Line Order Container] SET [Line Order Entry No] = '" + entry.entryNo + "' WHERE [Line Order Entry No] = '" + containerLineOrder.entryNo + "' AND [Container No] = '" + containerNo.Replace("'", "") + "'");
                            database.nonQuery("UPDATE [Line Order Shipment] SET [Line Order Entry No] = '" + entry.entryNo + "' WHERE [Line Order Entry No] = '" + containerLineOrder.entryNo + "' AND [Container No] = '" + containerNo.Replace("'", "") + "'");

                            containerLineOrder.updateDetails(database);
                        }


                    }

                    int j = 0;
                    while (j < containerDataSet.Tables[0].Rows.Count)
                    {
                        if (!containerStringList.Contains(containerDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString()))
                        {

                            serverLogging.log(agentCode, "Container " + containerDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() + " on order, not loaded. Removing.");
                            ShippingCustomers shippingCustomers = new ShippingCustomers();
                            ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, entry.shippingCustomerNo);
                            LineOrder newLineOrder = new LineOrder(shippingCustomer);
                            newLineOrder.organizationNo = entry.organizationNo;
                            newLineOrder.comments = entry.comments;
                            newLineOrder.details = entry.details;
                            newLineOrder.status = 3;
                            newLineOrder.save(database);

                            database.nonQuery("UPDATE [Line Order Container] SET [Line Order Entry No] = '" + newLineOrder.entryNo + "' WHERE [Line Order Entry No] = '" + entry.entryNo + "' AND [Container No] = '" + containerDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString().Replace("'", "") + "'");
                            database.nonQuery("UPDATE [Line Order Shipment] SET [Line Order Entry No] = '" + newLineOrder.entryNo + "' WHERE [Line Order Entry No] = '" + entry.entryNo + "' AND [Container No] = '" + containerDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString().Replace("'", "") + "'");

                            
                        }
                        j++;
                    }

                    entry.updateDetails(database);
                    
                }

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (errorMessage.Length > 200) errorMessage = errorMessage.Substring(0, 200);
                serverLogging.log(agentCode, "Error: " + errorMessage);

            }
        }
    }
}
