using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class BoschProduct
    {
        public string Sku { set; get; } = "";
        public string Name { set; get; } = "";
        public int MinQuantity { set; get; } = 0;
        public int MaxQuantity { set; get; } = 999;
        public int Inventory { set; get; } = 0;
        public DateTime StartDate { set; get; }

        public DateTime EndDate { get; set; }


        public dynamic Price { get; set; }


        public List<dynamic> DisplayNames { get; set; }
        public List<dynamic> Images { get; set; }
        public List<dynamic> Reviews { get; set; }

        public List<BoschProductVariation> Variations { get; set; }
        public dynamic Category { get; set; }

        public BoschProduct(Item item)
        {
            this.Sku = item.no;
            this.Name = item.description;
            this.Inventory = (int)item.inventory;
            this.StartDate = new DateTime(2020, 1, 1);
            this.EndDate = new DateTime(2029, 1, 1);
            this.Price = new
            {
                Currency = "PT",
                List = item.unitPrice,
                Sale = item.unitPrice
            };

            DisplayNames = new List<dynamic>();
            foreach(ItemTranslation translation in item.itemTranslations)
            {
                DisplayNames.Add(new
                {
                    Value = translation.description,
                    LanguageCode = translation.getLocale()
                });
            };

            Images = new List<dynamic>();
            if ((item.imageUrl != null) && (item.imageUrl != ""))
            {
                Images.Add(new {
                    SortOrder = 0,
                    Url = item.imageUrl,
                    Group = "detail",
                    Name = Sku + ".jpg"
                });
            }

            Reviews = new List<dynamic>();
            foreach (ItemTranslation translation in item.itemTranslations)
            {
                Reviews.Add(new
                {
                    Content = translation.productText,
                    ReviewType = "FullReview",
                    LanguageCode = translation.getLocale()
                });
            };

            Category = new
            {
                Id = item.productGroup,
                Name = item.productGroup
            };

            Variations = new List<BoschProductVariation>();
            foreach (ItemVariant variant in item.itemVariants)
            {
                Variations.Add(new BoschProductVariation(item, variant));
            };
        }


    }
}

