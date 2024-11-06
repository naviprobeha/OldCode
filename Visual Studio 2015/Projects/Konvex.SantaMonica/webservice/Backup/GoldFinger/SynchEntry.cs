using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Konvex.SmartShipping.DataObjects;

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
    }
}
