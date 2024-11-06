using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooCommerceWrapperTestGUI
{

    public class Product
    {
        public int[] related_ids { get; set; }
        public object[] variations { get; set; }
        public string[] categories { get; set; }
        public bool shipping_required { get; set; }
        public int id { get; set; }
        public object[] parent { get; set; }
        public string regular_price { get; set; }
        public object weight { get; set; }
        public int total_sales { get; set; }
        public string sku { get; set; }
        public int rating_count { get; set; }
        public bool managing_stock { get; set; }
        public string title { get; set; }
        public bool backordered { get; set; }
        public bool on_sale { get; set; }
        public string status { get; set; }
        public int download_limit { get; set; }
        public bool taxable { get; set; }
        public bool reviews_allowed { get; set; }
        public string description { get; set; }
        public bool purchaseable { get; set; }
        public string sale_price { get; set; }
        public string type { get; set; }
        public string permalink { get; set; }
        public string catalog_visibility { get; set; }
        public int download_expiry { get; set; }
        public Dimensions dimensions { get; set; }
        public object[] cross_sell_ids { get; set; }
        public string price { get; set; }
        public DateTime updated_at { get; set; }
        public object[] attributes { get; set; }
        public string shipping_class { get; set; }
        public bool _virtual { get; set; }
        public bool downloadable { get; set; }
        public object[] upsell_ids { get; set; }
        public DateTime created_at { get; set; }
        public string tax_class { get; set; }
        public object[] tags { get; set; }
        public string price_html { get; set; }
        public bool in_stock { get; set; }
        public bool sold_individually { get; set; }
        public string short_description { get; set; }
        public object[] downloads { get; set; }
        public string tax_status { get; set; }
        public string average_rating { get; set; }
        public string download_type { get; set; }
        public bool shipping_taxable { get; set; }
        public string purchase_note { get; set; }
        public object shipping_class_id { get; set; }
        public bool visible { get; set; }
        public bool backorders_allowed { get; set; }
        public Image[] images { get; set; }
        public int stock_quantity { get; set; }
        public bool featured { get; set; }
    }

    public class Dimensions
    {
        public string length { get; set; }
        public string height { get; set; }
        public string unit { get; set; }
        public string width { get; set; }
    }

    public class Image
    {
        public int position { get; set; }
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public string src { get; set; }
        public string title { get; set; }
        public string alt { get; set; }
        public DateTime updated_at { get; set; }
    }

}

