using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class PDFDocumentLog
    {

        private string _documentNo;
        private string _base64String;

        public PDFDocumentLog() { }

        public PDFDocumentLog(string documentNo, string base64String)
        {
            _documentNo = documentNo;
            _base64String = base64String;

        }

        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public string base64String { get { return _base64String; } set { _base64String = value; } }


        public static string getBase64Document(Database database, int documentType, string documentNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Attachment] FROM [" + database.getTableName("PDF Document Log") + "] WHERE [Document Type] = @documentType AND [Document No_] = @documentNo");
            databaseQuery.addIntParameter("@documentType", documentType);
            databaseQuery.addStringParameter("@documentNo", documentNo, 20);

            byte[] byteArray = (byte[])databaseQuery.executeScalar();
            if (byteArray != null)
            {
                try
                {
                    if (byteArray != null) return System.Convert.ToBase64String(byteArray);
                    return "";

                }
                catch (Exception)
                { }
            }

            return "";

        }

    }

}
