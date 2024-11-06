using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using Microsoft.Navision.CFront;

namespace Navipro.CashJet.Replication
{

    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F2-1319ADB3EF5F";
        public const string intfguid = "D030D214-C984-496a-87E8-41732C114F1E";
        public const string eventguid = "D030D214-C984-496a-87E8-41732C114F1F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IDataDistributor
    {
        [DispId(1)]
        bool ConnectOpenDatabaseSQL(string serverName, string database, int cacheSize, bool useCommitCache, bool useNTAuthentication, string userId, string password);
        [DispId(2)]
        bool ConnectOpenDatabaseNative(string serverName, string database, int cacheSize, bool useCommitCache, bool useNTAuthentication, string userId, string password);
        [DispId(3)]
        void OpenCompany(string companyName);
        [DispId(4)]
        void HideErrors(bool hideErrors);
        [DispId(5)]
        void SetNavisionPath(string path);
        [DispId(6)]
        void LoadLicenseFile(string licenseFile);
        [DispId(7)]
        void AbortWriteTransaction();
        [DispId(8)]
        int AllocRecord(int tableHandle);
        [DispId(9)]
        void BeginWriteTransaction();
        [DispId(10)]
        void AllowKeyNotFound();
        [DispId(11)]
        void AllowRecordExists();
        [DispId(12)]
        void AllowTableNotFound();
        [DispId(13)]
        void AllowRecordNotFound();
        [DispId(14)]
        void DisAllowAll();
        [DispId(15)]
        void CalcField(int tableHandle, int recordHandle, int fieldNo);
        [DispId(16)]
        void CalcSum(int tableHandle, int recordHandle, int fieldNo);
        [DispId(17)]
        void CheckLicenseFile(int objectNo);
        [DispId(18)]
        void CloseCompany();
        [DispId(19)]
        void CloseTable(int tableHandle);
        [DispId(20)]
        void CloseDatabase();
        [DispId(21)]
        string companyName { get; }
        [DispId(22)]
        bool CompareRecords(int tableHandle, int destinationRecordHandle, int sourceRecordHandle);
        [DispId(22)]
        bool CompareStrings(string leftValue, string rightValue);
        [DispId(23)]
        void CopyRecord(int tableHandle, int destinationRecordHandle, int sourceRecordHandle);
        [DispId(24)]
        string CryptPassword(string userId, string password);
        [DispId(25)]
        string databaseName { get; }
        [DispId(26)]
        bool DeleteRecord(int tableHandle, int recordHandle);
        [DispId(27)]
        void DeleteRecords(int tableHandle);
        [DispId(28)]
        int DuplicateRecord(int tableHandle, int recordHandle);
        [DispId(29)]
        void EndWriteTransaction();
        [DispId(30)]
        string FieldCaption(int tableHandle, int fieldNo);
        [DispId(31)]
        int FieldClass(int tableHandle, int fieldNo);
        [DispId(32)]
        int FieldCount(int tableHandle);
        [DispId(33)]
        int FieldLength(int tableHandle, int fieldNo);
        [DispId(34)]
        string FieldName(int tableHandle, int fieldNo);
        [DispId(35)]
        int FieldNo(int tableHandle, string fieldName);
        [DispId(36)]
        string FieldOption(int tableHandle, int fieldNo);
        [DispId(37)]
        string FieldOptionCaption(int tableHandle, int fieldNo);
        [DispId(38)]
        string FieldToString(int tableHandle, int recordHandle, int fieldNo);
        [DispId(39)]
        int FieldType(int tableHandle, int fieldNo);
        [DispId(40)]
        bool FindFirstRecord(int tableHandle, int recordHandle);
        [DispId(41)]
        bool FindLastRecord(int tableHandle, int recordHandle);
        [DispId(42)]
        bool FindRecord(int tableHandle, int recordHandle, string searchMethod);
        [DispId(43)]
        bool FindSet(int tableHandle, int recordHandle, bool forUpdate, bool updateKey);
        [DispId(44)]
        void FreeRecord(int recordHandle);
        [DispId(45)]
        int[] GetCurrentKey(int tableHandle);
        [DispId(46)]
        string GetFilter(int tableHandle, int fieldNo);
        [DispId(47)]
        string GetView(int tableHandle, bool useFieldNames);
        [DispId(48)]
        void Init();
        [DispId(49)]
        void InitRecord(int tableHandle, int recordHandle);
        [DispId(50)]
        bool InsertRecord(int tableHandle, int recordHandle);
        [DispId(51)]
        int KeyCount(int tableHandle);
        [DispId(52)]
        void LockTableNoWait(int tableHandle);
        [DispId(53)]
        void LockTableWait(int tableHandle);
        [DispId(54)]
        bool ModifyRecord(int tableHandle, int recordHandle);
        [DispId(55)]
        int NextField(int tableHandle, int fieldNo);
        [DispId(56)]
        int NextRecord(int tableHandle, int recordHandle, int step);
        [DispId(57)]
        int NextTable(int tableNo);
        [DispId(58)]
        int OpenTable(int tableNo);
        [DispId(59)]
        int OpenTemporaryTable(int tableNo);
        [DispId(60)]
        int RecordCount(int tableHandle);
        [DispId(61)]
        bool RenameRecord(int tableHandle, int newRecordHandle, int oldRecordHandle);
        [DispId(62)]
        void SelectLatestVersion();
        [DispId(63)]
        void SetFilter(int tableHandle, int fieldNo, string value);
        [DispId(64)]
        void SetView(int tableHandle, string view);
        [DispId(65)]
        void StringToField(int tableHandle, int recordHandle, int fieldNo, string fieldValue);
        [DispId(66)]
        bool SetFieldData(int tableHandle, int recordHandle, int fieldNo, string fieldValue);
        [DispId(67)]
        string TableCaption(int tableHandle);
        [DispId(68)]
        bool TableIsSame(int tableNo1, int tableNo2);
        [DispId(69)]
        string TableName(int tableHandle);
        [DispId(70)]
        int TableNo(string tableName);
        [DispId(71)]
        int UserCount { get; }
        [DispId(72)]
        string UserId { get; }
        [DispId(73)]
        void DisconnectServer();
        [DispId(74)]
        string LastErrorMessage { get; }
        [DispId(75)]
        string GetFieldData(int tableHandle, int recordHandle, int fieldNo);
        [DispId(76)]
        void Dispose();
        [DispId(77)]
        bool FieldExists(int tableHandle, int fieldNo);

    }

    [Guid(Guids.coclsguid), ProgId("Navipro.CashJet.Replication"), ClassInterface(ClassInterfaceType.None)]
    public class DataDistributor : IDataDistributor
    {
        private CFrontDotNet cFront;
        private string lastErrorMessage;

        public DataDistributor()
        {
            cFront = CFrontDotNet.Instance;
        }

        public bool ConnectOpenDatabaseSQL(string serverName, string database, int cacheSize, bool useCommitCache, bool useNTAuthentication, string userId, string password)
        {
            Microsoft.Navision.CFront.CFrontDotNet.DriverType = NavisionDriverType.Sql;

            try
            {
            cFront.ConnectServerAndOpenDatabase(serverName, NavisionNetType.SqlDefault, database, cacheSize, useCommitCache, useNTAuthentication, userId, password);
            }
            catch (Exception e)
            {
                this.lastErrorMessage = e.Message;
                return false;
            }
            return true;
        }

        public bool ConnectOpenDatabaseNative(string serverName, string database, int cacheSize, bool useCommitCache, bool useNTAuthentication, string userId, string password)
        {
            Microsoft.Navision.CFront.CFrontDotNet.DriverType = NavisionDriverType.Native;

            try
            {
                cFront.ConnectServerAndOpenDatabase(serverName, NavisionNetType.NativeTcp, database, cacheSize, useCommitCache, useNTAuthentication, userId, password);
            }
            catch (Exception e)
            {
                this.lastErrorMessage = e.Message;
                return false;
            }
            return true;
        }

        public void DisconnectServer()
        {
            try
            {

                cFront.DisconnectServer();
            }
            catch (Exception e) 
            {
                lastErrorMessage = e.Message;
            }
        }

        public void OpenCompany(string companyName)
        {
            cFront.OpenCompany(companyName);
        }

        public void HideErrors(bool hideErrors)
        {
            cFront.HideErrors = hideErrors;
        }

        public void SetNavisionPath(string path)
        {
            
            Microsoft.Navision.CFront.CFrontDotNet.NavisionPath = path;        
        }

        public void LoadLicenseFile(string licenseFile)
        {
            cFront.LoadLicenseFile(licenseFile);

            
        }

        public void AbortWriteTransaction()
        {
            cFront.AbortWriteTransaction();

        }

        public int AllocRecord(int tableHandle)
        {
            return cFront.AllocRecord(tableHandle);
        }

        public void BeginWriteTransaction()
        {
            cFront.BeginWriteTransaction();
        }

        public void AllowKeyNotFound()
        {
            cFront.Allow(NavisionAllowedError.KeyNotFound);
        }
        public void AllowRecordExists()
        {
            cFront.Allow(NavisionAllowedError.RecordExists);
        }
        public void AllowTableNotFound()
        {
            cFront.Allow(NavisionAllowedError.TableNotFound);
        }
        public void AllowRecordNotFound()
        {
            cFront.Allow(NavisionAllowedError.RecordNotFound);
        }
        public void DisAllowAll()
        {
            cFront.Allow(NavisionAllowedError.None);
            
        }

        public void CalcField(int tableHandle, int recordHandle, int fieldNo)
        {
            int [] a = new int[1];
            a[0] = fieldNo;
            cFront.CalcFields(tableHandle, recordHandle, a);
        }

        public void CalcSum(int tableHandle, int recordHandle, int fieldNo)
        {
            int[] a = new int[1];
            a[0] = fieldNo;
            cFront.CalcSums(tableHandle, recordHandle, a);            
        }

        public void CheckLicenseFile(int objectNo)
        {
            cFront.CheckLicenseFile(objectNo);
            
        }

        public void CloseCompany()
        {
            cFront.CloseCompany();
        }

        public void CloseTable(int tableHandle)
        {
            cFront.CloseTable(tableHandle);
        }

        public void CloseDatabase()
        {
            cFront.CloseDatabase();
            
        }
        public string companyName
        {
            get
            {
                return cFront.CompanyName;
            }
        }

        public bool CompareRecords(int tableHandle, int destinationRecordHandle, int sourceRecordHandle)
        {
            return cFront.CompareRecords(tableHandle, destinationRecordHandle, sourceRecordHandle);
        }

        public bool CompareStrings(string leftValue, string rightValue)
        {
            return cFront.CompareStrings(leftValue, rightValue);
        }

        public void CopyRecord(int tableHandle, int destinationRecordHandle, int sourceRecordHandle)
        {
            cFront.CopyRecord(tableHandle, destinationRecordHandle, sourceRecordHandle);
            
        }

        public string CryptPassword(string userId, string password)
        {
            return cFront.CryptPassword(userId, password);
            
        }

        public string databaseName
        {
            get
            {
                return cFront.DatabaseName;
            }
        }

        public bool DeleteRecord(int tableHandle, int recordHandle)
        {
            return cFront.DeleteRecord(tableHandle, recordHandle);
        }

        public void DeleteRecords(int tableHandle)
        {
            cFront.DeleteRecords(tableHandle);
        }

        public int DuplicateRecord(int tableHandle, int recordHandle)
        {
            return cFront.DuplicateRecord(tableHandle, recordHandle);
        }

        public void EndWriteTransaction()
        {
            cFront.EndWriteTransaction();
        }

        public string FieldCaption(int tableHandle, int fieldNo)
        {
            return cFront.FieldCaption(tableHandle, fieldNo);
        }

        public int FieldClass(int tableHandle, int fieldNo)
        {
            if (cFront.FieldClass(tableHandle, fieldNo) == NavisionFieldClass.Normal) return 0;
            if (cFront.FieldClass(tableHandle, fieldNo) == NavisionFieldClass.FlowField) return 1;
            if (cFront.FieldClass(tableHandle, fieldNo) == NavisionFieldClass.FlowFilter) return 2;
            return 0;
        }

        public int FieldCount(int tableHandle)
        {
            return cFront.FieldCount(tableHandle);
        }

        public bool FieldExists(int tableHandle, int fieldNo)
        {
            try
            {
                string fieldName = cFront.FieldName(tableHandle, fieldNo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public int FieldLength(int tableHandle, int fieldNo)
        {
            return cFront.FieldLength(tableHandle, fieldNo);
        }

        public string FieldName(int tableHandle, int fieldNo)
        {
            return cFront.FieldName(tableHandle, fieldNo);
        }


        public int FieldNo(int tableHandle, string fieldName)
        {
            return cFront.FieldNo(tableHandle, fieldName);
        }

        public string FieldOption(int tableHandle, int fieldNo)
        {
            return cFront.FieldOption(tableHandle, fieldNo);
        }

        public string FieldOptionCaption(int tableHandle, int fieldNo)
        {
            return cFront.FieldOptionCaption(tableHandle, fieldNo);
        }

        public string FieldToString(int tableHandle, int recordHandle, int fieldNo)
        {
            return cFront.FieldToString(tableHandle, recordHandle, fieldNo);
        }

        public int FieldType(int tableHandle, int fieldNo)
        {
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.None) return 0;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Text) return 1;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Integer) return 2;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Decimal) return 3;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Code) return 4;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Boolean) return 5;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.BigInteger) return 6;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Binary) return 7;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Blob) return 8;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Date) return 9;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.DateFormula) return 10;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.DateTime) return 11;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Duration) return 12;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Guid) return 13;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Option) return 14;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.Time) return 15;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.RecordId) return 16;
            if (cFront.FieldType(tableHandle, fieldNo) == NavisionFieldType.TableFilter) return 17;
            return 0;
        }


        public bool FindFirstRecord(int tableHandle, int recordHandle)
        {
            return cFront.FindFirstRecord(tableHandle, recordHandle);
        }

        public bool FindLastRecord(int tableHandle, int recordHandle)
        {
            return cFront.FindLastRecord(tableHandle, recordHandle);
        }

        public bool FindRecord(int tableHandle, int recordHandle, string searchMethod)
        {
            return cFront.FindRecord(tableHandle, recordHandle, searchMethod);
        }

        public bool FindSet(int tableHandle, int recordHandle, bool forUpdate, bool updateKey)
        {
            return cFront.FindSet(tableHandle, recordHandle, forUpdate, updateKey);
        }

        public void FreeRecord(int recordHandle)
        {
            cFront.FreeRecord(recordHandle);
        }

        public int[] GetCurrentKey(int tableHandle)
        {
            return cFront.GetCurrentKey(tableHandle);
        }

        public string GetFilter(int tableHandle, int fieldNo)
        {
            return cFront.GetFilter(tableHandle, fieldNo);
        }

        public string GetView(int tableHandle, bool useFieldNames)
        {
            return cFront.GetView(tableHandle, useFieldNames);
        }

        public void Init()
        {
            cFront.Init();
        }

        public void InitRecord(int tableHandle, int recordHandle)
        {
            cFront.InitRecord(tableHandle, recordHandle);
        }

        public bool InsertRecord(int tableHandle, int recordHandle)
        {
            return cFront.InsertRecord(tableHandle, recordHandle);
        }

        public int KeyCount(int tableHandle)
        {
            return cFront.KeyCount(tableHandle);
        }

        public void LockTableNoWait(int tableHandle)
        {
            cFront.LockTable(tableHandle, NavisionTableLockMode.LockNoWait);
        }

        public void LockTableWait(int tableHandle)
        {
            cFront.LockTable(tableHandle, NavisionTableLockMode.LockWait);
        }

        public bool ModifyRecord(int tableHandle, int recordHandle)
        {
            return cFront.ModifyRecord(tableHandle, recordHandle);
        }

        public int NextField(int tableHandle, int fieldNo)
        {
            return cFront.NextField(tableHandle, fieldNo);
        }

        public int NextRecord(int tableHandle, int recordHandle, int step)
        {
            return (int)cFront.NextRecord(tableHandle, recordHandle, (short)step);
        }

        public int NextTable(int tableNo)
        {
            return cFront.NextTable(tableNo);
        }

        public int OpenTable(int tableNo)
        {
            return cFront.OpenTable(tableNo);
        }

        public int OpenTemporaryTable(int tableNo)
        {
            return cFront.OpenTemporaryTable(tableNo);
        }

        public int RecordCount(int tableHandle)
        {
            return cFront.RecordCount(tableHandle);
        }

        public bool RenameRecord(int tableHandle, int newRecordHandle, int oldRecordHandle)
        {
            return cFront.RenameRecord(tableHandle, newRecordHandle, oldRecordHandle);
        }

        public void SelectLatestVersion()
        {
            cFront.SelectLatestVersion();
        }

        public void SetFilter(int tableHandle, int fieldNo, string value)
        {
            cFront.SetFilter(tableHandle, fieldNo, value);
        }

        public void SetView(int tableHandle, string view)
        {
            cFront.SetView(tableHandle, view);
        }

        public void StringToField(int tableHandle, int recordHandle, int fieldNo, string fieldValue)
        {
            cFront.StringToField(tableHandle, recordHandle, fieldNo, fieldValue);
        
        }

        

        public bool SetFieldData(int tableHandle, int recordHandle, int fieldNo, string fieldValue)
        {
            try
            {
                cFront.SetFieldData(tableHandle, recordHandle, fieldNo, cFront.FieldType(tableHandle, fieldNo), GetBytesByType(cFront.FieldType(tableHandle, fieldNo), fieldValue, tableHandle, fieldNo));
            }
            catch (Exception e)
            {
                lastErrorMessage = e.Message;
                return false;
            }

            return true;
        }
        

        public string TableCaption(int tableHandle)
        {
            return cFront.TableCaption(tableHandle);
        }

        public bool TableIsSame(int tableNo1, int tableNo2)
        {
            return cFront.TableIsSame(tableNo1, tableNo2);
        }

        public string TableName(int tableHandle)
        {
            return cFront.TableName(tableHandle);
        }

        public int TableNo(string tableName)
        {
            return cFront.TableNo(tableName);
        }

        public int UserCount
        {
            get
            {
                return cFront.UserCount;
            }
        }

        public string UserId
        {
            get
            {
                return cFront.UserId;
            }
        }


        private byte[] GetBytesByType(NavisionFieldType type, String value, int tableHandle, int fieldNo)
        {
            switch (type)
            {
                case NavisionFieldType.Text:
                    {
                        NavisionText n = NavisionText.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Option:
                    {
                        NavisionOption n = new NavisionOption(Convert.ToInt32(value));
                        return n.GetBytes();
                    }
                case NavisionFieldType.BigInteger:
                    {
                        NavisionBigInteger n = NavisionBigInteger.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Binary:
                    {
                        NavisionBinary n = NavisionBinary.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Blob:
                    {
                        NavisionBlob n = NavisionBlob.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Boolean:
                    {
                        NavisionBoolean n = NavisionBoolean.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Code:
                    {
                        NavisionCode n = NavisionCode.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Date:
                    {
                        NavisionDate n = NavisionDate.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.DateFormula:
                    {
                        NavisionDateFormula n = NavisionDateFormula.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.DateTime:
                    {
                        NavisionDateTime n = NavisionDateTime.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Decimal:
                    {
                        NavisionDecimal n = NavisionDecimal.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Duration:
                    {
                        NavisionDuration n = NavisionDuration.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Guid:
                    {
                        NavisionGuid n = NavisionGuid.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Integer:
                    {
                        NavisionInteger n = new NavisionInteger(int.Parse(value));
                        return n.GetBytes();
                    }
                case NavisionFieldType.RecordId:
                    {
                        NavisionRecordId n = NavisionRecordId.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.TableFilter:
                    {
                        NavisionTableFilter n = NavisionTableFilter.Parse(value);
                        return n.GetBytes();
                    }
                case NavisionFieldType.Time:
                    {
                        NavisionTime n = NavisionTime.Parse(value);
                        return n.GetBytes();
                    }
            }
            return new byte[] { };
        }


        private string GetValueAsText(NavisionValue value)
        {
            switch (value.FieldType)
            {
                case NavisionFieldType.Text:
                    {
                        NavisionText n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Option:
                    {
                        NavisionOption n = value;
                        return n.Value.ToString();
                    }
                case NavisionFieldType.BigInteger:
                    {
                        NavisionBigInteger n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Binary:
                    {
                        NavisionBinary n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Blob:
                    {
                        NavisionBlob n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Boolean:
                    {
                        NavisionBoolean n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Code:
                    {
                        NavisionCode n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Date:
                    {
                        NavisionDate n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.DateFormula:
                    {
                        NavisionDateFormula n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.DateTime:
                    {
                        NavisionDateTime n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Decimal:
                    {
                        NavisionDecimal n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Duration:
                    {
                        NavisionDuration n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Guid:
                    {
                        NavisionGuid n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Integer:
                    {
                        NavisionInteger n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.RecordId:
                    {
                        NavisionRecordId n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.TableFilter:
                    {
                        NavisionTableFilter n = value;
                        return n.ToString();
                    }
                case NavisionFieldType.Time:
                    {
                        NavisionTime n = value;
                        return n.ToString();
                    }
            }
            return "";
        }


        public string LastErrorMessage
        {
            get
            {
                return lastErrorMessage;
            }
        }

        public string GetFieldData(int tableHandle, int recordHandle, int fieldNo)
        {
            try
            {
                NavisionValue value = cFront.GetFieldData(tableHandle, recordHandle, fieldNo);
                return GetValueAsText(value);
            }
            catch (Exception e)
            {
                lastErrorMessage = e.Message;
            }

            
            return "";
        }

        public void Dispose()
        {
            cFront.Dispose();
            
        }
    }
}
