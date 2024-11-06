using System;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemTranslation : ProductTranslation
	{
		private Database database;

		private string _itemNo;
        private string _languageCode;
        private string _description;
        private string _description2;
        private string _variantCode;

		public ItemTranslation(Database database, string itemNo, string variantCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this._itemNo = itemNo;
			this._variantCode = variantCode;
			this._languageCode = languageCode;

			getFromDatabase();
		}

		public ItemTranslation(Database database, string itemNo, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this._itemNo = itemNo;
			this._variantCode = "";
			this._languageCode = languageCode;

			getFromDatabase();
		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Item No_], [Language Code], [Description], [Description 2], [Variant Code] FROM [" + database.getTableName("Item Translation") + "] WHERE [Item No_] = @itemNo AND [Language Code] = @languageCode AND [Variant Code] = @variantCode");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				_itemNo = dataReader.GetValue(0).ToString();
				_languageCode = dataReader.GetValue(1).ToString();
				_description = dataReader.GetValue(2).ToString();
				_description2 = dataReader.GetValue(3).ToString();
				_variantCode = dataReader.GetValue(4).ToString();

			}

			dataReader.Close();


		}

        public string itemNo { get { return _itemNo; } }
        public string languageCode { get { return _languageCode; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public string variantCode { get { return _variantCode; } }


	}
}
