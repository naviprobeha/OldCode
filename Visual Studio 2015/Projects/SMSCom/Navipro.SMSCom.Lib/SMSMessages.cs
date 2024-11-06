using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SMSCom.Lib
{
    public class SMSMessages
    {


        public DataSet getUnhandledDataSet(Database database, int type)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Type], [Phone No], [Message], [Handled], [Received Date], [Received Time] FROM [SMS Message] WHERE [Type] = '" + type + "' AND [Handled] = 0");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "smsMessage");
            adapter.Dispose();

            return dataSet;

        }

    }
}
