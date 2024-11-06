using System;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataInvoiceNo.
    /// </summary>
    public class DataInvoiceNo
    {
        public string no;
        public string orderNo;

        private SmartDatabase smartDatabase;


        public DataInvoiceNo(SmartDatabase smartDatabase, string orderNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

            this.orderNo = orderNo;

            save();
        }

        private void save()
        {
            try
            {
                smartDatabase.nonQuery("INSERT INTO invoiceNo (orderNo) VALUES ('" + this.orderNo + "')");
                SqlCeDataReader dataReader = smartDatabase.query("SELECT no FROM invoiceNo WHERE no = @@IDENTITY");
                if (dataReader.Read())
                {
                    this.no = format(dataReader.GetInt32(0).ToString());

                }
                dataReader.Dispose();

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private string format(string no)
        {
            return no.PadLeft(4, '0');
        }
    }
}
