using System;
using System.Data;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ExtendedTextLine.
	/// </summary>
	public class ExtendedTextLine
	{
		private Database database;

		public int tableName;
		public string no;
		public string languageCode;
		public int textNo;
		public int lineNo;
		public string text;

		public ExtendedTextLine(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;

			fromDataRow(dataRow);
		}

		private void fromDataRow(DataRow dataRow)
		{
			this.tableName = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.no = dataRow.ItemArray.GetValue(1).ToString();
			this.languageCode = dataRow.ItemArray.GetValue(2).ToString();
			this.textNo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.lineNo = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.text = dataRow.ItemArray.GetValue(5).ToString();

		}
	}
}
