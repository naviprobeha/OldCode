using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Navipro.Backoffice.Library
{
    public class CaseLog
    {
        public string caseNo { get; set; }
        public int lineNo { get; set; }
        public string subject { get; set; }
        public string messageId { get; set; }
        public string fromName { get; set; }
        public string fromEmail { get; set; }
        public string body { get; set; }

        public Dictionary<string, string> receivers;

        public CaseLog(SqlDataReader dataReader)
        {
            caseNo = dataReader.GetValue(0).ToString();
            lineNo = dataReader.GetInt32(1);
            fromName = dataReader.GetValue(2).ToString();
            fromEmail = dataReader.GetValue(3).ToString();
            subject = dataReader.GetValue(4).ToString();
            messageId = dataReader.GetValue(5).ToString();

            receivers = new Dictionary<string, string>();
        }

        public void refreshReceiptients(Database database)
        {

            DatabaseQuery query = database.prepare("SELECT [Receiver E-mail], [Name] FROM [" + database.getTableName("Case Log Receiver") + "] WHERE [Case No_] = @caseNo AND [Case Log Line No_] = @caseLogLineNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("caseLogLineNo", lineNo);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                receivers.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
            }
            dataReader.Close();

        }

        public void refreshBody(Database database)
        {

            DatabaseQuery query = database.prepare("SELECT [External Comments] FROM [" + database.getTableName("Case Log") + "] WHERE [Case No_] = @caseNo AND [Line No_] = @lineNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("lineNo", lineNo);

            byte[] blobData = (byte[])query.executeScalar();

            body = System.Text.Encoding.Default.GetString(blobData);
        }

        public static CaseLog getFirstUnsentMessage(Database database)
        {
            CaseLog caseLog = null;

            DatabaseQuery query = database.prepare("SELECT TOP 1 cl.[Case No_], cl.[Line No_], cl.[From Name], cl.[From E-mail], c.[Subject], c.[Message-ID] FROM [" + database.getTableName("Case Log") + "] cl, [" + database.getTableName("Case") + "] c WHERE cl.[Case No_] = c.[No_] AND [Send As E-mail] = 1 AND [E-mail Sent] = 0");
           
            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseLog = new CaseLog(dataReader);
            }
            dataReader.Close();

            return caseLog;
        }
    }
}
