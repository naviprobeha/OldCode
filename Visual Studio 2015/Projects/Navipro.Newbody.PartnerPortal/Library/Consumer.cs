using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.Infojet.Lib;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class Consumer
    {
        private int _entryNo = 0;
        private string _webUserAccountNo;
        private string _name;
        private string _phoneNo;
        private string _email;
        private string _ocrNo;

        public Consumer()
        {

        }

        public Consumer(DataRow dataRow)
        {
            _entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            _webUserAccountNo = dataRow.ItemArray.GetValue(1).ToString();
            _name = dataRow.ItemArray.GetValue(2).ToString();
            _phoneNo = dataRow.ItemArray.GetValue(3).ToString();
            _email = dataRow.ItemArray.GetValue(4).ToString();
            _ocrNo = dataRow.ItemArray.GetValue(5).ToString();
        }

        public Consumer(SqlDataReader dataReader)
        {
            _entryNo = dataReader.GetInt32(0);
            _webUserAccountNo = dataReader.GetValue(1).ToString();
            _name = dataReader.GetValue(2).ToString();
            _phoneNo = dataReader.GetValue(3).ToString();
            _email = dataReader.GetValue(4).ToString();
            _ocrNo = dataReader.GetValue(5).ToString();
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string webUserAccountNo { get { return _webUserAccountNo; } set { _webUserAccountNo = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string ocrNo { get { return _ocrNo; } set { _ocrNo = value; } }

        public void createOcrNo()
        {
            _ocrNo = "13"+entryNo.ToString().PadLeft(8, '0');
            _ocrNo = _ocrNo + ((_ocrNo.Length+2) % 10).ToString();
            _ocrNo = _ocrNo + CalculateChecksum(_ocrNo);


        }

        private int CalculateChecksum(string pnr)
        {
            char[] pnrChars = pnr.ToCharArray();

            string newNumberString = "";

            bool include = true;

            for (int charCount = 0; charCount < pnrChars.Length; charCount++)
            {
                int number = Convert.ToInt32(pnrChars[charCount]);
                if (include)
                    number *= 2;

                include = !include;


                newNumberString += number.ToString();
            }

            char[] newCharAray = newNumberString.ToCharArray();
            int newNumber = 0;

            for (int charCount = 0; charCount < newCharAray.Length; charCount++)
            {
                int number = Convert.ToInt32(newCharAray[charCount]);
                newNumber += number;
            }

            return (((newNumber / 10) + 1) * 10) - newNumber;


        }

        public static Consumer[] getDataSetArray(Database database, string webUserAccountNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Web User Account No_], [Name], [Phone No_], [E-Mail], [OCR No_] FROM [" + database.getTableName("Consumer") + "] WITH (NOLOCK) WHERE [Web User Account No_] = @webUserAccountNo ORDER BY [Name]");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            Consumer[] consumerArray = new Consumer[dataSet.Tables[0].Rows.Count];
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Consumer consumer = new Consumer(dataSet.Tables[0].Rows[i]);
                consumerArray[i] = consumer;


                i++;
            }


            return consumerArray;
        }

        public static Consumer[] getSalesIDDataSetArray(Database database, string webUserAccountNo, string salesId)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Web User Account No_], [Name], [Phone No_], [E-Mail], [OCR No_] FROM [" + database.getTableName("Consumer") + "] c WITH (NOLOCK), [" + database.getTableName("SalesID Consumer") + "] s WITH (NOLOCK) WHERE c.[Web User Account No_] = @webUserAccountNo AND c.[Entry No_] = s.[Consumer Entry No_] AND s.[Sales ID] = @salesId AND c.[Web User Account No_] = s.[Web User Account No_] ORDER BY [Name]");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            Consumer[] consumerArray = new Consumer[dataSet.Tables[0].Rows.Count];
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Consumer consumer = new Consumer(dataSet.Tables[0].Rows[i]);
                consumerArray[i] = consumer;


                i++;
            }


            return consumerArray;
        }

        public static void updateConsumer(Database database, Consumer consumer)
        {
            DatabaseQuery databaseQuery = database.prepare("UPDATE [" + database.getTableName("Consumer") + "] SET [Name] = @name, [Phone No_] = @phoneNo, [E-Mail] = @email WHERE [Entry No_] = @entryNo AND [Web User Account No_] = @webUserAccountNo");
            databaseQuery.addIntParameter("@entryNo", consumer.entryNo);
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@name", consumer.name, 50);
            databaseQuery.addStringParameter("@phoneNo", consumer.phoneNo, 30);
            databaseQuery.addStringParameter("@email", consumer.email, 100);

            databaseQuery.execute();
        }

        public static Consumer createConsumer(Database database, Consumer consumer)
        {
            DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("Consumer") + "] ([Web User Account No_], [Name], [Phone No_], [E-Mail], [OCR No_]) VALUES (@webUserAccountNo, @name, @phoneNo, @email, '')");
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@name", consumer.name, 50);
            databaseQuery.addStringParameter("@phoneNo", consumer.phoneNo, 30);
            databaseQuery.addStringParameter("@email", consumer.email, 100);

            databaseQuery.execute();

            consumer.entryNo = (int)database.getInsertedSeqNo();
            consumer.createOcrNo();

            databaseQuery = database.prepare("UPDATE [" + database.getTableName("Consumer") + "] SET [OCR No_] = @ocrNo WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("@entryNo", consumer.entryNo);
            databaseQuery.addStringParameter("@ocrNo", consumer.ocrNo, 30);
            databaseQuery.execute();

            return consumer;
        }

        public static Consumer getConsumerFromOCR(Database database, string ocrNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Web User Account No_], [Name], [Phone No_], [E-Mail], [OCR No_] FROM [" + database.getTableName("Consumer") + "] WHERE [OCR No_] = @ocr");
            databaseQuery.addStringParameter("@ocr", ocrNo, 30);

            Consumer consumer = null;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                consumer = new Consumer(dataReader);
            }

            dataReader.Close();

            return consumer;
        }

        public static void deleteConsumer(Database database, Consumer consumer)
        {
            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Consumer") + "] WHERE [Entry No_] = @entryNo AND [Web User Account No_] = @webUserAccountNo");
            databaseQuery.addIntParameter("@entryNo", consumer.entryNo);
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);

            databaseQuery.execute();

        }

        public static void addConsumerToSalesId(Database database, string salesId, Consumer consumer)
        {
            DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("SalesID Consumer") + "] ([Web User Account No_], [Sales ID], [Consumer Entry No_]) VALUES (@webUserAccountNo, @salesId, @consumerEntryNo)");
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addIntParameter("@consumerEntryNo", consumer.entryNo);

            databaseQuery.execute();

        }

        public static void setPaid(Database database, string salesId, Consumer consumer, bool paid)
        {
            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("SalesID Consumer Paid") + "] WHERE [Web User Account No_] = @webUserAccountNo AND [Sales ID] = @salesId AND [Consumer Entry No_] = @consumerEntryNo");
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addIntParameter("@consumerEntryNo", consumer.entryNo);

            databaseQuery.execute();

            if (paid)
            {
                databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("SalesID Consumer Paid") + "] ([Web User Account No_], [Sales ID], [Consumer Entry No_], [Paid]) VALUES (@webUserAccountNo, @salesId, @consumerEntryNo, '1')");
                databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
                databaseQuery.addStringParameter("@salesId", salesId, 20);
                databaseQuery.addIntParameter("@consumerEntryNo", consumer.entryNo);

                databaseQuery.execute();
            }
        }

        public static void removeConsumerFromSalesId(Database database, string salesId, Consumer consumer)
        {
            
            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo AND [Extra 4] = @consumerEntryNo");
            databaseQuery.addIntParameter("@consumerEntryNo", consumer.entryNo);
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            databaseQuery.execute();

            databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("SalesID Consumer") + "] WHERE [Consumer Entry No_] = @consumerEntryNo AND [Web User Account No_] = @webUserAccountNo AND [Sales ID] = @salesId");
            databaseQuery.addIntParameter("@consumerEntryNo", consumer.entryNo);
            databaseQuery.addStringParameter("@webUserAccountNo", consumer.webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            databaseQuery.execute();

        }

        public static void removeAllConsumersFromSalesId(Database database, string salesId)
        {

            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId");
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            databaseQuery.execute();

            databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("SalesID Consumer") + "] WHERE [Sales ID] = @salesId");
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            databaseQuery.execute();

        }

    }
}
