using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebPageLine
	{
		public string webSiteCode;
		public string webPageCode;
		public int versionNo;
		public int lineNo;
		public string webTemplatePartCode;
		public int type;
		public string code;
		public string description;
		public int sortOrder;
		public string languageCode;
        public string webTypeCode;
	
		private Infojet infojetContext;

        public WebPageLine(Infojet infojetContext, string webSiteCode, string webPageCode, int versionNo, int lineNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
			this.webSiteCode = webSiteCode;
			this.webPageCode = webPageCode;
			this.versionNo = versionNo;
			this.lineNo = lineNo;

			getFromDatabase();
		}
        
        public WebPageLine(Infojet infojetContext, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;

			this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this.webPageCode = dataRow.ItemArray.GetValue(1).ToString();
			this.versionNo = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.lineNo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.webTemplatePartCode = dataRow.ItemArray.GetValue(4).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.code = dataRow.ItemArray.GetValue(6).ToString();
			this.description = dataRow.ItemArray.GetValue(7).ToString();
			this.sortOrder = int.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.languageCode = dataRow.ItemArray.GetValue(9).ToString();
            this.webTypeCode = dataRow.ItemArray.GetValue(10).ToString();
		}
        

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Page Code], [Version No_], [Line No_], [Web Template Part Code], [Type], [Code], [Description], [Sort Order], [Language Code], [Web Type Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Page Code] = @webPageCode AND [Version No_] = @versionNo AND [Line No_] = @lineNo");
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addIntParameter("lineNo", lineNo);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
				webPageCode = dataReader.GetValue(1).ToString();
				versionNo = dataReader.GetInt32(2);
				lineNo = dataReader.GetInt32(3);
				webTemplatePartCode = dataReader.GetValue(4).ToString();
				type = int.Parse(dataReader.GetValue(5).ToString());
				code = dataReader.GetValue(6).ToString();
				description = dataReader.GetValue(7).ToString();
				sortOrder = dataReader.GetInt32(8);
				languageCode = dataReader.GetValue(9).ToString();
                webTypeCode = dataReader.GetValue(10).ToString();
			}

			dataReader.Close();
			
		}


        public string getText()
        {

            WebPageLineTexts webPageLineTexts = new WebPageLineTexts(infojetContext.systemDatabase);
            DataSet dataSet = webPageLineTexts.getDataSet(this, infojetContext.languageCode, this.versionNo);
              
            string content = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebPageLineText webPageLineText = new WebPageLineText(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);

                content = content + " "+ translateKeyWords(webPageLineText.text);

                i++;
            }

            return content; // +"Ver: " + this.versionNo;
        }

        public string getContent()
        {
            if (infojetContext.isDesignMode())
            {
                StringWriter stringWriter = new StringWriter();

                EditorManagerControl editorManagerControl = new EditorManagerControl(infojetContext, this);
                editorManagerControl.Render(stringWriter);

                return stringWriter.ToString();

            }
            else
            {
                return getText();
            }
        }

        public string translateKeyWords(string text)
        {
            text = translateRequestVars(text);

            return text;
        }

        public string translateRequestVars(string text)
        {
            while (text.IndexOf("#=") >= 0)
            {
                string preString = text.Substring(0, text.IndexOf("#="));
                string tempString = text.Substring(text.IndexOf("#=") + 2);
                if (tempString.IndexOf("#") >= 0)
                {
                    string varName = tempString.Substring(0, tempString.IndexOf("#"));
                    string postString = tempString.Substring(tempString.IndexOf("#") + 1);
                    text = preString + System.Web.HttpContext.Current.Request[varName] + postString;
                }
                
            }

            return text;
        }

    }
}
