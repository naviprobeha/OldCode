using System;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CallbackArgument.
	/// </summary>
	public class ItemListArgument
	{
		public Item item;
        public string extendedText;
		public ArrayList itemImages;
		public WebItemImage itemImage;
		public string formatedUnitPrice;
		public string formatedUnitListPrice;
		public float inventory;
		public string inventoryText;
		public string link;

		public ItemListArgument(Item item)
		{
			//
			// TODO: Add constructor logic here
			//
			this.item = item;
			this.itemImages = new ArrayList();
			
		}

		public string getExtendedText()
		{
			return getExtendedText(0);
		}

		public string getExtendedText(int maxLength)
		{
			if (maxLength == 0) return extendedText;
			if (maxLength > extendedText.Length) return extendedText;

			string extendedTextPart = extendedText;

			extendedTextPart = extendedText.Substring(0, maxLength);

			int i = 1;
			while((extendedTextPart[maxLength-i] != ' ') && (i < maxLength-1))
			{
				i++;
			}

			return extendedTextPart.Substring(0, maxLength-i)+"...";
		}

        public string description
        {
            get
            {
                return item.description;
            }
        }


        
	}
}
