using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebClass
	{
		private Database database;

		public string code;
		public string description;
		public string fontFamily;
		public string fontSize;
        public int fontWeight;
		public int fontStyle;
		public string fontColorCode;
		public int textDecoration;
		public string backgroundColorCode;

		public WebClass(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.code = code;

			getFromDatabase();
		}

		public WebClass(Database database, DataRow dataRow)
		{
			this.database = database;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
			this.fontFamily = dataRow.ItemArray.GetValue(2).ToString();
			this.fontSize = dataRow.ItemArray.GetValue(3).ToString();
			this.fontWeight = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.fontStyle = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.fontColorCode = dataRow.ItemArray.GetValue(6).ToString();
			this.textDecoration = int.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.backgroundColorCode = dataRow.ItemArray.GetValue(8).ToString();
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Font Family], [Font Size], [Font Weight], [Font Style], [Font Color Code], [Text Decoration], [Background Color Code] FROM ["+database.getTableName("Web Class")+"] WHERE [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				description = dataReader.GetValue(1).ToString();
				fontFamily = dataReader.GetValue(2).ToString();
				fontSize = dataReader.GetValue(3).ToString();
				fontWeight = int.Parse(dataReader.GetValue(4).ToString());
				fontStyle = int.Parse(dataReader.GetValue(5).ToString());
				fontColorCode = dataReader.GetValue(6).ToString();
				textDecoration = int.Parse(dataReader.GetValue(7).ToString());
				backgroundColorCode = dataReader.GetValue(8).ToString();

			}

			dataReader.Close();


		}

		public void renderCss(StringWriter stringWriter)
		{
			stringWriter.WriteLine("."+this.code+" {");
			if (this.fontFamily != "") stringWriter.WriteLine("font-family: "+this.fontFamily+";");
			if (this.fontSize != "") stringWriter.WriteLine("font-size: "+this.fontSize+";");
			if (this.fontColorCode != "")
			{
				WebColor webColor = new WebColor(database, this.fontColorCode);
				stringWriter.WriteLine("color: "+webColor.color+";");
			}
			if (this.backgroundColorCode != "")
			{
				WebColor webColor = new WebColor(database, this.backgroundColorCode);
				stringWriter.WriteLine("background-color: "+webColor.color+";");
			}
			if (this.fontWeight == 1) stringWriter.WriteLine("font-weight: bold;");
			if (this.fontStyle == 1) stringWriter.WriteLine("font-style: italic;");
			if (this.textDecoration == 1) stringWriter.WriteLine("text-decoration: underline;");
			

			stringWriter.WriteLine("}");
		}
	}
}
