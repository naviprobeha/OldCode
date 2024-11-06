using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SMSCom.Lib
{
    public class SMSMessage
    {
    
		public int entryNo;
        public int type;
		public string phoneNo;
        public string message;
        public DateTime receivedDateTime;
        public bool handled;

		private string updateMethod;

		public SMSMessage(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.type = dataReader.GetInt32(1);
			this.phoneNo = dataReader.GetValue(2).ToString();
			this.message = dataReader.GetValue(3).ToString();
			
            this.handled = false;
            if (dataReader.GetInt32(4) == 1) this.handled = true;

            DateTime receivedDate = dataReader.GetDateTime(5);
            DateTime receivedTime = dataReader.GetDateTime(6);
            this.receivedDateTime = DateTime.Parse(receivedDate.ToString("yyyy-MM-dd") + " " + receivedTime.ToString("HH:mm:ss"));

			updateMethod = "";

		}

        public SMSMessage(DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //
            this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this.type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this.phoneNo = dataRow.ItemArray.GetValue(2).ToString();
            this.message = dataRow.ItemArray.GetValue(3).ToString();

            this.handled = false;
            if (int.Parse(dataRow.ItemArray.GetValue(4).ToString()) == 1) this.handled = true;

            DateTime receivedDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
            DateTime receivedTime = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this.receivedDateTime = DateTime.Parse(receivedDate.ToString("yyyy-MM-dd") + " " + receivedTime.ToString("HH:mm:ss"));

            updateMethod = "";

        }

        public SMSMessage()
		{
            entryNo = 0;
            updateMethod = "";
    	}

		public void save(Database database)
		{
            int handledVal = 0;
            if (handled) handledVal = 1;

            message = message.Replace("'", "");

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [SMS Message] WHERE [Entry No] = '"+entryNo+"'");

				}
				else
				{
                    //throw new Exception("Hepp");

					SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [SMS Message] WHERE [Entry No] = '"+entryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [SMS Message] SET [Type] = '"+type+"', [Phone No] = '"+phoneNo+"', [Message] = '"+message+"', [Handled] = '"+handledVal+"', [Received Date] = '"+receivedDateTime.ToString("yyyy-MM-dd")+"', [Received Time] = '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"' WHERE [Entry No] = '"+entryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [SMS Message] ([Type], [Phone No], [Message], [Handled], [Received Date], [Received Time]) VALUES ('"+type+"', '"+phoneNo+"','"+message+"','"+handledVal+"', '"+receivedDateTime.ToString("yyyy-MM-dd")+"', '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on sms message update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

    }

}
