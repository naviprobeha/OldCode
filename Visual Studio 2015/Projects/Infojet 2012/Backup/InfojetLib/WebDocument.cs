using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebDocument
    {
        private string _fileName;
        private int _type;

        private Infojet infojetContext;

        public WebDocument(Infojet infojetContext, string fileName)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._fileName = fileName;

        }

        public WebDocument(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._fileName = dataRow.ItemArray.GetValue(0).ToString();
            this._type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
        }

        public string fileName { get { return _fileName; } set { _fileName = value; } }
        public int type { get { return _type; } set { _type = value; } }


        public static WebDocument getEntry(Infojet infojetContext, string fileName)
        {
            WebDocument webDocument = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Filename], [Type] FROM [" + infojetContext.systemDatabase.getTableName("Web Document") + "] WHERE [Filename] = @fileName");
            databaseQuery.addStringParameter("fileName", fileName, 100);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                webDocument = new WebDocument(infojetContext, dataSet.Tables[0].Rows[i]);

                i++;
            }


            return webDocument;
        }

        public void downloadDocument()
        {
            try
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Document] FROM [" + infojetContext.systemDatabase.getTableName("Web Document") + "] WHERE [Filename] = @fileName");
                databaseQuery.addStringParameter("fileName", this._fileName, 100);

                byte[] docByteArray = (byte[])databaseQuery.executeScalar();
                if (docByteArray != null)
                {
                    try
                    {
                        if (_type == 0) System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        if (_type == 1) System.Web.HttpContext.Current.Response.ContentType = "application/ms-word";
                        if (_type == 2) System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename="+_fileName);
                        System.Web.HttpContext.Current.Response.BinaryWrite(docByteArray);

                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    //throw new Exception("ImageNotFoundException: "+this.code);
                }
            }
            catch (Exception)
            {
                //throw new Exception("Image: "+this.code+", Exception: "+e.Message);
            }

        }
    }
}
