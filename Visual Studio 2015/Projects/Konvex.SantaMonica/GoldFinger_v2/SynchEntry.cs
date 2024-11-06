using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Konvex.SmartShipping.DataObjects;

using Newtonsoft.Json.Linq;

namespace Konvex.SmartShipping.Goldfinger
{
    public class SynchEntry
    {
        private int _entryNo;
        private int _type;
        private int _action;
        private string _primaryKey;

        private ShipOrderHeader _shipOrderHeader;
        private Organization _organization;
        private MobileUser _mobileUser;
        private Agent _agent;
        private OrganizationLocation _organizationLocation;
        private Message _message;
        private Container _container;
        private Category _category;
        private Customer _customer;
        private Item _item;
        private ItemPrice _itemPrice;
        private ItemPriceExtended _itemPriceExtended;
        private LineJournal _lineJournal;
        private LineOrder _lineOrder;

        private Navipro.SantaMonica.Common.SynchronizationQueueEntry synchronizationQueueEntry;

        public SynchEntry()
        {
        }

        public SynchEntry(Navipro.SantaMonica.Common.SynchronizationQueueEntry synchronizationQueueEntry)
        {
            this.synchronizationQueueEntry = synchronizationQueueEntry;
            _entryNo = synchronizationQueueEntry.entryNo;
            _type = synchronizationQueueEntry.type;
            _action = synchronizationQueueEntry.action;
            _primaryKey = synchronizationQueueEntry.primaryKey;
        }

        public void delete(Navipro.SantaMonica.Common.Database database)
        {
            synchronizationQueueEntry.delete(database);
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public int action { get { return _action; } set { _action = value; } }
        public string primaryKey { get { return _primaryKey; } set { _primaryKey = value; } }

        public ShipOrderHeader shipOrderHeader { get { return _shipOrderHeader; } set { _shipOrderHeader = value; } }
        public Organization organization { get { return _organization; } set { _organization = value; } }
        public MobileUser mobileUser { get { return _mobileUser; } set { _mobileUser = value; } }
        public Agent agent { get { return _agent; } set { _agent = value; } }
        public OrganizationLocation organizationLocation { get { return _organizationLocation; } set { _organizationLocation = value; } }
        public Message message { get { return _message; } set { _message = value; } }
        public Container container { get { return _container; } set { _container = value; } }
        public Category category { get { return _category; } set { _category = value; } }
        public Customer customer { get { return _customer; } set { _customer = value; } }
        public Item item { get { return _item; } set { _item = value; } }
        public ItemPrice itemPrice { get { return _itemPrice; } set { _itemPrice = value; } }
        public ItemPriceExtended itemPriceExtended { get { return _itemPriceExtended; } set { _itemPriceExtended = value; } }
        //public LineJournal lineJournal { get { return _lineJournal; } set { _lineJournal = value; } }
        //public LineOrder lineOrder { get { return _lineOrder; } set { _lineOrder = value; } }

        public static SynchEntry fromJsonObject(JObject jsonObject, Navipro.SantaMonica.Common.Agent agent)
        {
            SynchEntry entry = new SynchEntry();
            entry.entryNo = (int)jsonObject["entryNo"];
            entry.type = (int)jsonObject["type"];
            entry.action = (int)jsonObject["action"];
            entry.primaryKey = (string)jsonObject["primaryKey"];

            try
            {
                if (entry.type == 0)
                {
                    JToken token = jsonObject["shipOrderHeader"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }
                    entry.shipOrderHeader = ShipOrderHeader.fromJsonObject((JObject)jsonObject["shipOrderHeader"]);

                }
                if (entry.type == 1)
                {
                    JToken token = jsonObject["shipCustomer"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.customer = Customer.fromJsonObject((JObject)jsonObject["shipCustomer"]);
                    entry.customer.organizationNo = agent.organizationNo;
                }
                if (entry.type == 2)
                {
                    JToken token = jsonObject["item"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.item = Item.fromJsonObject((JObject)jsonObject["item"]);
                }

                if (entry.type == 5)
                {
                    JToken token = jsonObject["message"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.message = Message.fromJsonObject((JObject)jsonObject["message"]);
                    entry.message.organizationNo = agent.organizationNo;
                }
                if (entry.type == 6)
                {
                    JToken token = jsonObject["company"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }
                    entry.organization = Organization.fromJsonObject((JObject)jsonObject["company"]);
                }
                if (entry.type == 8)
                {
                    JToken token = jsonObject["mobileUser"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.mobileUser = MobileUser.fromJsonObject((JObject)jsonObject["mobileUser"]);
                }
                if (entry.type == 11)
                {
                    JToken token = jsonObject["agent"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.agent = Agent.fromJsonObject((JObject)jsonObject["agent"]);
                }
                if (entry.type == 12)
                {
                    JToken token = jsonObject["container"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.container = Container.fromJsonObject((JObject)jsonObject["container"]);
                }
                if (entry.type == 16)
                {
                    JToken token = jsonObject["companyLocation"];

                    if (token.Type != JTokenType.Object)
                    {
                        entry.action = 2;
                        return entry;
                    }

                    entry.organizationLocation = OrganizationLocation.fromJsonObject((JObject)jsonObject["companyLocation"]);
                }
            }
            catch(Exception e)
            {

                throw new Exception(entry.entryNo + ": "+e.Message);
            }

            return entry;
        }
    }
}
