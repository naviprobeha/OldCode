using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class Agent
    {
        public string serialNo;
        public string code;
        public string description;
        public string password;
        public bool enabled;
        public DateTime lastCheckIn;
        public DateTime passwordCreated;
        public DateTime configurationUpdated;
        public string groupCode;

        private string updateMethod;

        public Agent(SqlDataReader dataReader)
        {
            //
            // TODO: Add constructor logic here
            //
            serialNo = dataReader.GetValue(0).ToString();
            code = dataReader.GetValue(1).ToString();
            description = dataReader.GetValue(2).ToString();
            password = dataReader.GetValue(3).ToString();

            enabled = false;
            if (dataReader.GetValue(4).ToString() == "1") enabled = true;

            DateTime lastCheckInDate = dataReader.GetDateTime(5);
            DateTime lastCheckInTime = dataReader.GetDateTime(6);

            this.lastCheckIn = new DateTime(lastCheckInDate.Year, lastCheckInDate.Month, lastCheckInDate.Day, lastCheckInTime.Hour, lastCheckInTime.Minute, lastCheckInTime.Second);

            DateTime passwordCreatedDate = dataReader.GetDateTime(7);
            DateTime passwordCreatedTime = dataReader.GetDateTime(8);

            this.passwordCreated = new DateTime(passwordCreatedDate.Year, passwordCreatedDate.Month, passwordCreatedDate.Day, passwordCreatedTime.Hour, passwordCreatedTime.Minute, passwordCreatedTime.Second);

            groupCode = dataReader.GetValue(9).ToString();

            DateTime configurationUpdatedDate = dataReader.GetDateTime(10);
            DateTime configurationUpdatedTime = dataReader.GetDateTime(11);

            this.configurationUpdated = new DateTime(configurationUpdatedDate.Year, configurationUpdatedDate.Month, configurationUpdatedDate.Day, configurationUpdatedTime.Hour, configurationUpdatedTime.Minute, configurationUpdatedTime.Second);
            
            updateMethod = "";
        }

        public Agent(DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //
            serialNo = dataRow.ItemArray.GetValue(0).ToString();
            code = dataRow.ItemArray.GetValue(1).ToString();
            description = dataRow.ItemArray.GetValue(2).ToString();
            password = dataRow.ItemArray.GetValue(3).ToString();

            enabled = false;
            if (dataRow.ItemArray.GetValue(4).ToString() == "1") enabled = true;

            DateTime lastCheckInDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
            DateTime lastCheckInTime = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());

            this.lastCheckIn = new DateTime(lastCheckInDate.Year, lastCheckInDate.Month, lastCheckInDate.Day, lastCheckInTime.Hour, lastCheckInTime.Minute, lastCheckInTime.Second);

            DateTime passwordCreatedDate = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
            DateTime passwordCreatedTime = DateTime.Parse(dataRow.ItemArray.GetValue(8).ToString());

            this.passwordCreated = new DateTime(passwordCreatedDate.Year, passwordCreatedDate.Month, passwordCreatedDate.Day, passwordCreatedTime.Hour, passwordCreatedTime.Minute, passwordCreatedTime.Second);


            groupCode = dataRow.ItemArray.GetValue(9).ToString();

            DateTime configurationUpdatedDate = DateTime.Parse(dataRow.ItemArray.GetValue(10).ToString());
            DateTime configurationUpdatedTime = DateTime.Parse(dataRow.ItemArray.GetValue(11).ToString());

            this.configurationUpdated = new DateTime(configurationUpdatedDate.Year, configurationUpdatedDate.Month, configurationUpdatedDate.Day, configurationUpdatedTime.Hour, configurationUpdatedTime.Minute, configurationUpdatedTime.Second);


            updateMethod = "";
        }


        public void save(Database database)
        {
            int enabledVal = 0;
            if (this.enabled) enabledVal = 1;

            try
            {
                if (updateMethod == "D")
                {
                    database.nonQuery("DELETE FROM [Agent] WHERE [Serial No] = '" + serialNo + "'");

                }
                else
                {
                    SqlDataReader dataReader = database.query("SELECT [Serial No] FROM [Agent] WHERE [Serial No] = '" + serialNo + "'");

                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        database.nonQuery("UPDATE [Agent] SET [Code] = '" + this.code + "', [Description] = '" + description + "', [Password] = '" + this.password + "', [Enabled] = '" + enabledVal + "', [Last Check-In Date] = '" + lastCheckIn.ToString("yyyy-MM-dd") + "', [Last Check-In Time] = '" + lastCheckIn.ToString("1754-01-01 HH:mm:ss") + "', [Password Created Date] = '" + passwordCreated.ToString("yyyy-MM-dd") + "', [Password Created Time] = '" + passwordCreated.ToString("1754-01-01 HH:mm:ss") + "', [Group Code] = '" + groupCode + "',  [Configuration Updated Date] = '" + configurationUpdated.ToString("yyyy-MM-dd") + "', [Configuration Updated Time] = '" + configurationUpdated.ToString("1754-01-01 HH:mm:ss") + "' WHERE [Serial No] = '" + serialNo + "'");
                    }
                    else
                    {
                        dataReader.Close();
                        database.nonQuery("INSERT INTO [Agent] ([Serial No], [Code], [Description], [Password], [Enabled], [Last Check-In Date], [Last Check-In Time], [Password Created Date], [Password Created Time], [Group Code], [Configuration Updated Date], [Configuration Updated Time]) VALUES ('" + serialNo + "', '" + code + "','" + description + "','" + password + "','" + enabledVal + "','" + lastCheckIn.ToString("yyyy-MM-dd 00:00:00") + "', '" + lastCheckIn.ToString("1754-01-01 HH:mm:ss") + "', '" + passwordCreated.ToString("yyyy-MM-dd 00:00:00") + "', '" + passwordCreated.ToString("1754-01-01 HH:mm:ss") + "', '" + this.groupCode + "', '" + configurationUpdated.ToString("yyyy-MM-dd 00:00:00") + "', '" + configurationUpdated.ToString("1754-01-01 HH:mm:ss") + "')");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Error on agent update: " + e.Message + " (" + database.getLastSQLCommand() + ")");
            }

        }

        public void delete(Database database)
        {
            updateMethod = "D";
            save(database);
        }

        public void generatePassword(Database database)
        {
            this.password = Guid.NewGuid().ToString();
            this.passwordCreated = DateTime.Now;
            save(database);
        }
    }
}
