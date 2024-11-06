using System;
using System.Xml;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebFormDocument.
	/// </summary>
	public class WebFormDocument : ServiceArgument
	{
		private WebForm webForm;
        private string webLoginCode;
		public ArrayList keyList;
		public ArrayList valueList;
        public ArrayList fileList;

		public WebFormDocument(WebForm webForm, ArrayList keyList, ArrayList valueList, ArrayList fileList)
		{
			//
			// TODO: Add constructor logic here
			//
			this.webForm = webForm;
			this.keyList = keyList;
			this.valueList = valueList;
            this.fileList = fileList;
		}

        public void setWebLoginCode(string webLoginCode)
        {
            this.webLoginCode = webLoginCode;
        }

		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add WebFormDocument.toDOM implementation

			XmlElement webFormElement = xmlDoc.CreateElement("webForm");

			XmlElement codeElement = xmlDoc.CreateElement("code");
			codeElement.AppendChild(xmlDoc.CreateTextNode(webForm.code));

            XmlElement webLoginCodeElement = xmlDoc.CreateElement("webLoginCode");
            webLoginCodeElement.AppendChild(xmlDoc.CreateTextNode(webLoginCode));

			XmlElement fieldsElement = xmlDoc.CreateElement("fields");

			int i = 0;
			while (i < keyList.Count)
			{
               
                try
				{
					XmlElement fieldElement = xmlDoc.CreateElement("field");

					XmlElement fieldCodeElement = xmlDoc.CreateElement("code");
					fieldCodeElement.AppendChild(xmlDoc.CreateTextNode(keyList[i].ToString()));

					XmlElement valueElement = xmlDoc.CreateElement("value");
					valueElement.AppendChild(xmlDoc.CreateTextNode(valueList[i].ToString()));

					fieldElement.AppendChild(fieldCodeElement);
					fieldElement.AppendChild(valueElement);

					fieldsElement.AppendChild(fieldElement);
				}
				catch(Exception)
				{
					throw new Exception("Code: "+keyList[i]+", Counts: "+keyList.Count+";"+valueList.Count+", "+i);
				}
				i++;
			}

			webFormElement.AppendChild(codeElement);
            webFormElement.AppendChild(webLoginCodeElement);
			webFormElement.AppendChild(fieldsElement);

			return webFormElement;
		}

		#endregion
	}
}
