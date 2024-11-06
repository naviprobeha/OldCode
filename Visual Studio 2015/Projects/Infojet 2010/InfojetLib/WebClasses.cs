using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebClasses
	{
		private Database database;


		public WebClasses(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getClasses()
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Code], [Description], [Font Family], [Font Size], [Font Weight], [Font Style], [Font Color Code], [Text Decoration], [Background Color Code] FROM ["+database.getTableName("Web Class")+"]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

		public void renderCss(StringWriter stringWriter)
		{
			DataSet classDataSet = getClasses();

			int i = 0;
			while (i < classDataSet.Tables[0].Rows.Count)
			{
				WebClass webClass = new WebClass(database, classDataSet.Tables[0].Rows[i]);

				webClass.renderCss(stringWriter);

				i++;
			}
		}

	}
}
