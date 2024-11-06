using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class Module
    {

        public int entryNo;
        public int type;
        public string name;
        public string className;
        public string versionNo;
        public DateTime changed;

        private string updateMethod;

        public Module(SqlDataReader dataReader)
        {
            entryNo = dataReader.GetInt32(0);
            type = int.Parse(dataReader.GetValue(1).ToString());
            name = dataReader.GetValue(2).ToString();
            className = dataReader.GetValue(3).ToString();
            versionNo = dataReader.GetValue(4).ToString();

            DateTime changedDate = dataReader.GetDateTime(5);
            DateTime changedTime = dataReader.GetDateTime(6);

            this.changed = new DateTime(changedDate.Year, changedDate.Month, changedDate.Day, changedTime.Hour, changedTime.Minute, changedTime.Second);

        }

        public Module(DataRow dataRow)
        {
            entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            type = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            name = dataRow.ItemArray.GetValue(2).ToString();
            className = dataRow.ItemArray.GetValue(3).ToString();
            versionNo = dataRow.ItemArray.GetValue(4).ToString();

            DateTime changedDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
            DateTime changedTime = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());

            this.changed = new DateTime(changedDate.Year, changedDate.Month, changedDate.Day, changedTime.Hour, changedTime.Minute, changedTime.Second);


        }

        public void save(Database database)
        {
            try
            {
                if (updateMethod == "D")
                {
                    database.nonQuery("DELETE FROM [Module] WHERE [Entry No] = '" + entryNo + "'");

                }
                else
                {
                    SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Module] WHERE [Entry No] = '" + entryNo + "'");

                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        database.nonQuery("UPDATE [Module] SET [Entry No] = '" + this.entryNo + "', [Type] = '"+ this.type +"', [Name] = '" + this.name + "', [Class Name] = '"+this.className+"', [Version No] = '" + this.versionNo + "', [Changed Date] = '" + changed.ToString("yyyy-MM-dd") + "', [Changed Time] = '" + changed.ToString("1754-01-01 HH:mm:ss") + "' WHERE [Entry No] = '" + entryNo + "'");
                    }
                    else
                    {
                        dataReader.Close();
                        database.nonQuery("INSERT INTO [Module] ([Entry No], [Type], [Name], [Class Name], [Version No], [Changed Date], [Changed Time]) VALUES ('" + entryNo + "', '"+ type +"', '" + name + "', '"+this.className+"', '" + versionNo + "', '" + changed.ToString("yyyy-MM-dd") + "', '" + changed.ToString("1754-01-01 HH:mm:ss") + "')");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Error on module update: " + e.Message + " (" + database.getLastSQLCommand() + ")");
            }

        }

        public void delete(Database database)
        {
            updateMethod = "D";
            save(database);
        }

        public void export(Database database, string fileName)
        {
            byte[] byteArray = (byte[])database.scalarQuery("SELECT [Binary] FROM [Module] WHERE [Entry No] = '" + entryNo + "'");

            FileStream fileStream = File.Create(fileName);

            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Flush();
            fileStream.Close();
        }
    }
}
