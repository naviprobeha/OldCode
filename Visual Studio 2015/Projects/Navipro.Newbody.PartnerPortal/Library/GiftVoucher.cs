using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;
using System.Xml;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class GiftVoucher : ServiceArgument
    {
        private string _no;
        private string _controlNo;
        private string _itemNo;
        private int _packageQty;
        private string _name;
        private string _address;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _purchasedItemNo1;
        private string _purchasedItemNo2;
        private string _purchasedItemNo3;

        public GiftVoucher()
        { }

        public string no { get { return _no; } set { _no = value; } }
        public string controlNo { get { return _controlNo; } set { _controlNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public int packageQty { get { return _packageQty; } set { _packageQty = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string purchasedItemNo1 { get { return _purchasedItemNo1; } set { _purchasedItemNo1 = value; } }
        public string purchasedItemNo2 { get { return _purchasedItemNo2; } set { _purchasedItemNo2 = value; } }
        public string purchasedItemNo3 { get { return _purchasedItemNo3; } set { _purchasedItemNo3 = value; } }


        public static GiftVoucher getGiftVoucher(Navipro.Infojet.Lib.Infojet infojetContext, string no, string controlNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT g.[No_], g.[Control No_], g.[Source No_], i.[Gift Card Package Qty_] FROM [" + infojetContext.systemDatabase.getTableName("Gift Voucher Ledger Entries") + "] g, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE g.[Source No_] = i.[No_] AND g.[No_] = @no AND g.[Control No_] = @controlNo AND [Open] = 1");
            databaseQuery.addStringParameter("no", no, 20);
            databaseQuery.addStringParameter("controlNo", controlNo, 20);
            SqlDataReader dataReader = databaseQuery.executeQuery();

            GiftVoucher giftVoucher = null;

            if (dataReader.Read())
            {
                giftVoucher = new GiftVoucher();
                giftVoucher.no = dataReader.GetValue(0).ToString();
                giftVoucher.controlNo = dataReader.GetValue(1).ToString();
                giftVoucher.itemNo = dataReader.GetValue(2).ToString();
                giftVoucher.packageQty = dataReader.GetInt32(3);
            }

            dataReader.Close();

            return giftVoucher;
        }

        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {
            XmlElement containerElement = xmlDoc.CreateElement("giftVoucher");

            containerElement.SetAttribute("no", no);
            containerElement.SetAttribute("name", name);
            containerElement.SetAttribute("address", address);
            containerElement.SetAttribute("postCode", postCode);
            containerElement.SetAttribute("city", city);
            containerElement.SetAttribute("countryCode", countryCode);
            containerElement.SetAttribute("purchasedItemNo1", purchasedItemNo1);
            containerElement.SetAttribute("purchasedItemNo2", purchasedItemNo2);
            containerElement.SetAttribute("purchasedItemNo3", purchasedItemNo3);

            return containerElement;

        }

        #endregion
    }

}
