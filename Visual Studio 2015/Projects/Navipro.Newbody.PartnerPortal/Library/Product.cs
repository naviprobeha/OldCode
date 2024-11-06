using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.Infojet.Lib;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class Product
    {
        private string _modelNo = "";
        private string _description = "";
        private string _text = "";
        private float _unitPrice = 0;
        private string _unitOfMeasure = "";
        private WebImage _webImage = null;
        private string _itemCategoryCode = "";
        private string _itemCategoryDescription = "";
        private string _composition = "";
        private int _qtyInPackage = 0;

        private ProductSku[] _productSkuArray;

        public Product()
        {

        }

        public string modelNo { get { return _modelNo; } set { _modelNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string text { get { return _text; } set { _text = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public string unitOfMeasure { get { return _unitOfMeasure; } set { _unitOfMeasure = value; } }
        public string webImageCode { get { if (_webImage != null) return _webImage.code; return ""; } set { } }
        public DateTime lastModified { get { if (_webImage != null) return _webImage.lastModified; return DateTime.MinValue; } set { } }
        public string itemCategoryCode { get { return _itemCategoryCode; } set { _itemCategoryCode = value; } }
        public string itemCategoryDescription { get { return _itemCategoryDescription; } set { _itemCategoryDescription = value; } }
        public ProductSku[] productSkuArray { get { return _productSkuArray; } set { _productSkuArray = value; } }
        public string composition { get { return _composition; } set { _composition = value; } }
        public int qtyInPackage { get { return _qtyInPackage; } set { _qtyInPackage = value; } }

        public void setImage(WebImage webImage)
        {
            _webImage = webImage;
        }


        public static ProductCollection getVoucherProducts(Infojet.Lib.Infojet infojetContext)
        {
            ProductCollection productCollection = new ProductCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_], i.[Description], [Description 2], [Unit Price], [Sales Unit of Measure], [Manufacturer Code], [Lead Time Calculation], [Unit List Price], [Item Disc_ Group], [Size], [Composition], [Package Qty_], [Item Category Code], [Product Group Code] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Item Category") + "] c WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Bin Content") + "] bc WITH (NOLOCK) WHERE i.[Item Category Code] = c.[Code] AND c.[Web Availability] = 0 AND bc.[Item No_] = i.[No_] AND bc.[Bin Code] = 'PLOCK' ORDER BY i.[No_]");

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            Navipro.Infojet.Lib.Items items = new Navipro.Infojet.Lib.Items();
            Hashtable inventoryTable = items.getItemInfo(dataSet, infojetContext, false, true);

            

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                string description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                string itemCategory = dataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString();
                string productGroup = dataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString();

                //Fix
                if ((itemNo != "REA2855") && (itemNo != "REA2856") && (itemNo != "REA2857"))
                {

                    ItemInfo itemInfo = (ItemInfo)inventoryTable[itemNo];
                    if (itemInfo != null)
                    {
                        if (itemInfo.inventory > 0)
                        {
                            Product product = new Product();
                            product.modelNo = itemNo;
                            product.description = description;
                            product.itemCategoryCode = itemCategory;

                            productCollection.Add(product);

                        }
                    }

                }
                i++;
            }

            return productCollection;

        }

    }
}
