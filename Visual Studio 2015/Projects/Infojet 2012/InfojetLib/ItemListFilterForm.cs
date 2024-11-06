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
        public int defaultSorting;
		public string filterText;
		public bool markZeroInventoryItems;

		public ArrayList itemAttributes;
        public Hashtable itemAttributeFilterTable;


        public int currentPage;
        public int noOfItemsPerPage;

		public ItemListFilterForm(WebItemList webItemList, bool showForm)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = webItemList.code;
			this.showForm = showForm;
			this.filterText = "";
            this.currentPage = 1;

            itemAttributeFilterTable = new Hashtable();

		}

		public void init()
		{
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_sorting1"] == null) this.sorting1 = defaultSorting; ;

			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"] != null) this.showImages = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"] != null) this.showItemNo = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showInventoryOnly"] != null) this.showInventoryOnly = bool.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_showInventoryOnly"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_sorting1"] != null) this.sorting1 = int.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_sorting1"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_sorting2"] != null) this.sorting2 = int.Parse(System.Web.HttpContext.Current.Session["IL_"+code+"_sorting2"].ToString());
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_filterText"] != null) this.filterText = System.Web.HttpContext.Current.Session["IL_"+code+"_filterText"].ToString();
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_itemAttributeFilterTable"] != null) this.itemAttributeFilterTable = (Hashtable)System.Web.HttpContext.Current.Session["IL_" + code + "_itemAttributeFilterTable"];

            if (System.Web.HttpContext.Current.Session["IL_" + code + "_currentPage"] != null) this.currentPage = int.Parse(System.Web.HttpContext.Current.Session["IL_" + code + "_currentPage"].ToString());
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_noOfItemsPerPage"] != null) this.noOfItemsPerPage = int.Parse(System.Web.HttpContext.Current.Session["IL_" + code + "_noOfItemsPerPage"].ToString());

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

            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_currentPage", currentPage);
            System.Web.HttpContext.Current.Session.Add("IL_" + code + "_noOfItemsPerPage", noOfItemsPerPage);

        }

        public void clearFilterText()
        {
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_filterText"] != null) System.Web.HttpContext.Current.Session["IL_" + code + "_filterText"] = "";
            this.filterText = "";

            if (System.Web.HttpContext.Current.Session["IL_" + code + "_currentPage"] != null) System.Web.HttpContext.Current.Session["IL_" + code + "_currentPage"] = "0";
            this.currentPage = 0;

        }

		public void setStartupValues(bool showImages, bool showItemNo, int noOfItemsPerPage)
		{
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showImages"] == null) this.showImages = showImages;
			if (System.Web.HttpContext.Current.Session["IL_"+code+"_showItemNo"] == null) this.showItemNo = showItemNo;
            if (System.Web.HttpContext.Current.Session["IL_" + code + "_noOfItemsPerPage"] == null) this.noOfItemsPerPage = noOfItemsPerPage;
		}

        public ItemAttributeCollection getFilteredItemAttributes(Infojet infojetContext, DataSet itemDataSet)
        {
            return ItemAttributeVisibilities.getFilteredItemAttributes(infojetContext, this.code, itemDataSet);
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

            if (sorting1 == 0) sortingText = "[Sort Order], [No_]";
            if (sorting1 == 1) sortingText = "[Description]";
            if (sorting1 == 2) sortingText = "[Unit Price]";
            if (sorting1 == 3) sortingText = "[Manufacturer Code]";

			if (sorting2 > 0)
			{
				if (sorting2 == 1) sortingText = sortingText + " DESC";
			}

			return sortingText;
		}

		public string getFilter()
		{
			if (filterText != "") return "([Description] LIKE '%"+filterText.ToUpper()+"%') OR ([No_] LIKE '%"+filterText.ToUpper()+"%')";
			return "";
		}


	}
}
