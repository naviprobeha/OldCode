﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Item
    {
        private string _no;
        private string _description;
        private string _searchDescription;
        private string _unitOfMeasure;
        private decimal _unitPrice;
        private bool _addStopItem;
        private string _stopItemNo;
        private bool _requireId;
        private bool _invoiceToJbv;
        private string _connectionItemNo;
        private bool _putToDeath;
        private bool _availableInMobile;
        private bool _requireCashPayment;
        private decimal _directCost;
        private string _categoryCode;
        private bool _availableOnWeb;
        private string _idGroupCode;
        private bool _discardAdminFee;
        private ItemPriceCollection _itemPriceCollection;
        private ItemPriceExtendedCollection _itemPriceExtendedCollection;

        public Item() { }

        public Item(Navipro.SantaMonica.Common.Item item)
        {
            _no = item.no;
            _description = item.description;
            _searchDescription = item.searchDescription;
            _unitOfMeasure = item.unitOfMeasure;
            _unitPrice = item.unitPrice;
            _addStopItem = item.addStopItem;
            _stopItemNo = item.stopItemNo;
            _requireId = item.requireId;
            _invoiceToJbv = item.invoiceToJbv;
            _connectionItemNo = item.connectionItemNo;
            _putToDeath = item.putToDeath;
            _availableInMobile = item.availableInMobile;
            _requireCashPayment = item.requireCashPayment;
            _directCost = item.directCost;
            _categoryCode = item.categoryCode;
            _availableOnWeb = item.availableOnWeb;
            _idGroupCode = item.idGroupCode;
            _discardAdminFee = item.discardAdminFee;
            
            
            _itemPriceCollection = new ItemPriceCollection();
            _itemPriceExtendedCollection = new ItemPriceExtendedCollection();
            
        }

        public void applyPrices(Navipro.SantaMonica.Common.Database database)
        {
            Navipro.SantaMonica.Common.ItemPrices itemPrices = new Navipro.SantaMonica.Common.ItemPrices();
            System.Data.DataSet itemPriceDataSet = itemPrices.getFullDataSet(database, _no);

            int i = 0;
            while (i < itemPriceDataSet.Tables[0].Rows.Count)
            {
                ItemPrice itemPrice = new ItemPrice(itemPriceDataSet.Tables[0].Rows[i]);
                _itemPriceCollection.Add(itemPrice);
                i++;
            }

            Navipro.SantaMonica.Common.ItemPricesExtended itemPricesExtended = new Navipro.SantaMonica.Common.ItemPricesExtended();
            System.Data.DataSet itemPriceExtendedDataSet = itemPricesExtended.getFullDataSet(database, _no);

            i = 0;
            while (i < itemPriceExtendedDataSet.Tables[0].Rows.Count)
            {
                ItemPriceExtended itemPriceExtended = new ItemPriceExtended(itemPriceExtendedDataSet.Tables[0].Rows[i]);
                _itemPriceExtendedCollection.Add(itemPriceExtended);

                i++;
            }

        }

        public string no { get { return _no; } set { _no = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string searchDescription { get { return _searchDescription; } set { _searchDescription = value; } }
        public string unitOfMeasure { get { return _unitOfMeasure; } set { _unitOfMeasure = value; } }
        public decimal unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public bool addStopItem { get { return _addStopItem; } set { _addStopItem = value; } }
        public string stopItemNo { get { return _stopItemNo; } set { _stopItemNo = value; } }
        public bool requireId { get { return _requireId; } set { _requireId = value; } }
        public bool invoiceToJbv { get { return _invoiceToJbv; } set { _invoiceToJbv = value; } }
        public string connectionItemNo { get { return _connectionItemNo; } set { _connectionItemNo = value; } }
        public bool putToDeath { get { return _putToDeath; } set { _putToDeath = value; } }
        public bool availableInMobile { get { return _availableInMobile; } set { _availableInMobile = value; } }
        public bool requireCashPayment { get { return _requireCashPayment; } set { _requireCashPayment = value; } }
        public decimal directCost { get { return _directCost; } set { _directCost = value; } }
        public string categoryCode { get { return _categoryCode; } set { _categoryCode = value; } }
        public bool availableOnWeb { get { return _availableOnWeb; } set { _availableOnWeb = value; } }
        public string idGroupCode { get { return _idGroupCode; } set { _idGroupCode = value; } }
        public bool discardAdminFee { get { return _discardAdminFee; } set { _discardAdminFee = value; } }
        public ItemPriceCollection itemPriceCollection { get { return _itemPriceCollection; } set { _itemPriceCollection = value; } }
        public ItemPriceExtendedCollection itemPriceExtendedCollection { get { return _itemPriceExtendedCollection; } set { _itemPriceExtendedCollection = value; } }

    }
}
