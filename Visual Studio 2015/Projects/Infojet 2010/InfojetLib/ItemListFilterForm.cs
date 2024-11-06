using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ItemListFilterForm.
	/// </summary>
	public class ItemListFilterForm
	{
		public string code;
		public bool showForm;
		public bool showImages;
		public bool showItemNo;
		public bool showInventoryOnly;
		
		public bool showInventory;
		public bool showUnitPrice;
		public bool showUnitListPrice;
		public bool showManufacturer;
		public bool showBuy;

		public int sorting1;
		public int sorting2;
		public string filterText;
		public bool markZeroInventoryItems;

		public ArrayList itemAttributes;
        public Hashtable itemAttributeFilterTable;
		

		public ItemListFilterForm(WebItemList webItemList, bool showForm)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = webItemList.code;
			this.showForm = showForm;
			this.filterText = "";

            itemAttributeFilterTable = new Hashtable();

		}

		public void init()
		{
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"] != null) this.showImages = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"] != null) this.showItemNo = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showInventoryOnly"] != null) this.showInventoryOnly = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showInventoryOnly"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_sorting1"] != null) this.sorting1 = int.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_sorting1"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_sorting2"] != null) this.sorting2 = int.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_sorting2"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_filterText"] != null) this.filterText = System.Web.HttpContext.Current.Session["IL_"+code+"_filterText"].ToString();
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_itemAttributeFilterTable"] != null) this.itemAttributeFilterTable = (Hashtable)System.Web.HttpContext.Current.Session["IL_" + code + "_itemAttributeFilterTable"];
		}

        public void save()
        {
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_showImages", showImages);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_showItemNo", showItemNo);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_showInventoryOnly", showInventoryOnly);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_sorting1", sorting1);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_sorting2", sorting2);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_filterText", filterText);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_itemAttributeFilterTable", itemAttributeFilterTable);

        }

		public void setStartupValues(bool showImages, bool showItemNo)
		{
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"] == null) this.showImages = showImages;
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"] == null) this.showItemNo = showItemNo;

		}

        public ItemAttributeCollection getFilteredItemAttributes(Infojet infojetContext)
        {
            return ItemAttributeVisibilities.getFilteredItemAttributes(infojetContext, this.code);
        }

        public void setItemAttributeFilterValue(string itemAttributeCode, string attributeValue)
        {
            if (itemAttributeFilterTable.ContainsKey(itemAttributeCode))
            {
                itemAttributeFilterTable[itemAttributeCode] = attributeValue;
            }
            else
            {
                itemAttributeFilterTable.Add(itemAttributeCode, attributeValue);
            }

            if (attributeValue == "")
            {
                itemAttributeFilterTable.Remove(itemAttributeCode);
            }
        }

        public void clearItemAttributeFilters()
        {
            itemAttributeFilterTable.Clear();
        }

        public string getItemAttributeFilterValue(string itemAttributeCode)
        {
            if (itemAttributeFilterTable.ContainsKey(itemAttributeCode))
            {
                return (string)itemAttributeFilterTable[itemAttributeCode];
            }

            return "";
        }

		public string getFieldName(string field)
		{
			return "IL_"+code+"_"+field;
		}

		public string getSorting()
		{
			string sortingText = "";

			if (sorting1 > 0)
			{
				if (sorting1 == 1) sortingText = "[Description]";
				if (sorting1 == 2) sortingText = "[Manufacturer Code]";
			}

			if (sorting2 > 0)
			{
				if (sortingText != "") sortingText = sortingText + ", ";

				if (sorting2 == 1) sortingText = sortingText + "[Description]";
				if (sorting2 == 2) sortingText = sortingText + "[Manufacturer Code]";
				if (sorting2 == 3) sortingText = sortingText + "[Inventory]";
				if (sorting2 == 4) sortingText = sortingText + "[Unit Price]";
				if (sorting2 == 5) sortingText = sortingText + "[Unit Price] DESC";
			}

			return sortingText;
		}

		public string getFilter()
		{
			if (filterText != "") return "[Description] LIKE '%"+filterText.ToUpper()+"%'";
			return "";
		}


	}
}
