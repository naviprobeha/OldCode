using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VoucherService.Models;

namespace VoucherService.Library
{
    public class VoucherHelper
    {

        public static Voucher getVoucher(string voucherType, string no)
        {
            Configuration configuration = new Configuration();
            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 * FROM [" + database.getTableName("POS Voucher Ledger Entry") + "] WHERE [Voucher Type Code] = @voucherType AND [Voucher No_] = @no AND [Entry Type] = 0 ORDER BY [Created Date] DESC");
            databaseQuery.addStringParameter("voucherType", voucherType, 50);
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            Voucher voucher = null;

            if (dataReader.Read())
            {
                voucher = new Voucher(dataReader);
            }
            dataReader.Close();

            if (voucher != null)
            {
                databaseQuery = database.prepare("SELECT SUM([Amount]) FROM [" + database.getTableName("POS Voucher Ledger Entry") + "] WHERE [Voucher Type Code] = @voucherType AND [Voucher No_] = @no");
                databaseQuery.addStringParameter("voucherType", voucherType, 50);
                databaseQuery.addStringParameter("no", no, 20);

                dataReader = databaseQuery.executeQuery();

                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) voucher.balance = dataReader.GetDecimal(0);
                }
                dataReader.Close();

            }

            database.close();

            return voucher;
        }

        public static VoucherType getVoucherType(string code)
        {
            Configuration configuration = new Configuration();
            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [G_L Account No_], [Voucher Type] FROM [" + database.getTableName("POS General") + "] WHERE [Code] = @code AND [Type] = 3");
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            VoucherType voucherType = null;

            if (dataReader.Read())
            {
                voucherType = new VoucherType(dataReader);
            }
            dataReader.Close();

            database.close();

            return voucherType;
        }



        public static Voucher create(string voucherTypeCode, string no, decimal amount, string orderNo, string currencyCode)
        {
            VoucherType voucherType = VoucherHelper.getVoucherType(voucherTypeCode);

            
            Configuration configuration = new Configuration();
            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 [Entry No_] FROM [" + database.getTableName("POS Voucher Ledger Entry") + "] WHERE [POS Device ID] = 'API' ORDER BY [Entry No_] DESC");
            SqlDataReader dataReader = databaseQuery.executeQuery();
            int entryNo = 1;
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) entryNo = dataReader.GetInt32(0) + 1;
            }
            dataReader.Close();

            DateTime dueDate = DateTime.Today.AddMonths(voucherType.dueDateMonth);

            
            databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("POS Voucher Ledger Entry") + "] ([POS Device ID], [Entry No_], [Type], [Entry Type], [Voucher No_], [Voucher Type Code], [Amount], [Created Date], [Printed Date], [Due Date], [Used Date], [Created Time], [Closed], [Creation Posted], [Closing Posted], [G_L Account No_], [No_ Series], [POS Transaction No_], [Document No_], [Currency Code], [Acknowledged], [Replication]) "+
                                                                                                                "VALUES ('API', @entryNo, @type, 0, @voucherNo, @voucherTypeCode, @amount, @createdDate, @printedDate, @dueDate, @usedDate, @createdTime, 0, 0, 0, @glAccountNo, '', '', @documentNo, @currencyCode, 1, 0)");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addIntParameter("type", voucherType.type);
            databaseQuery.addStringParameter("voucherNo", no, 20);
            databaseQuery.addStringParameter("voucherTypeCode", voucherType.code, 50);
            databaseQuery.addDecimalParameter("amount", (float)amount);
            databaseQuery.addDateTimeParameter("createdDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("printedDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("dueDate", dueDate);
            databaseQuery.addDateTimeParameter("usedDate", DateTime.Parse("1753-01-01"));
            databaseQuery.addDateTimeParameter("createdTime", DateTime.Parse("1754-01-01 " + DateTime.Now.ToString("HH:mm:ss")));
            databaseQuery.addStringParameter("glAccountNo", voucherType.accountNo, 20);
            databaseQuery.addStringParameter("documentNo", orderNo, 20);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            databaseQuery.execute();

            database.close();

            

            return getVoucher(voucherTypeCode, no);
        }


        public static Voucher use(string voucherTypeCode, string no, decimal amount, string orderNo, string currencyCode)
        {
            Configuration configuration = new Configuration();
            Database database = new Database(configuration);

            VoucherType voucherType = VoucherHelper.getVoucherType(voucherTypeCode);

            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 [Entry No_] FROM [" + database.getTableName("POS Voucher Ledger Entry") + "] WHERE [POS Device ID] = 'API' ORDER BY [Entry No_] DESC");
            SqlDataReader dataReader = databaseQuery.executeQuery();
            int entryNo = 1;
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) entryNo = dataReader.GetInt32(0) + 1;
            }
            dataReader.Close();

            DateTime dueDate = DateTime.Today.AddMonths(voucherType.dueDateMonth);

            databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("POS Voucher Ledger Entry") + "] ([POS Device ID], [Entry No_], [Type], [Entry Type], [Voucher No_], [Voucher Type Code], [Amount], [Created Date], [Printed Date], [Due Date], [Used Date], [Created Time], [Closed], [Creation Posted], [Closing Posted], [G_L Account No_], [No_ Series], [POS Transaction No_], [Document No_], [Currency Code], [Acknowledged], [Replication]) " +
                "VALUES ('API', @entryNo, @type, 1, @voucherNo, @voucherTypeCode, @amount, @createdDate, @printedDate, @dueDate, @usedDate, @createdTime, 0, 0, 0, @glAccountNo, '', '', @documentNo, @currencyCode, 1, 0)");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addIntParameter("type", voucherType.type);
            databaseQuery.addStringParameter("voucherNo", no, 20);
            databaseQuery.addStringParameter("voucherTypeCode", voucherType.code, 50);
            databaseQuery.addDecimalParameter("amount", (float)amount*-1);
            databaseQuery.addDateTimeParameter("createdDate", DateTime.Parse("1753-01-01"));
            databaseQuery.addDateTimeParameter("printedDate", DateTime.Parse("1753-01-01"));
            databaseQuery.addDateTimeParameter("dueDate", DateTime.Parse("1753-01-01"));
            databaseQuery.addDateTimeParameter("usedDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("createdTime", DateTime.Parse("1754-01-01 " + DateTime.Now.ToString("HH:mm:ss")));
            databaseQuery.addStringParameter("glAccountNo", voucherType.accountNo, 20);
            databaseQuery.addStringParameter("documentNo", orderNo, 20);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            databaseQuery.execute();

            return getVoucher(voucherTypeCode, no);
        }
    }
}