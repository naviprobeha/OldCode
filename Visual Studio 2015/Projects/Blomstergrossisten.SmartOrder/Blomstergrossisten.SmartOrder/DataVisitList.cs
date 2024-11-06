using System;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataVisitList.
    /// </summary>
    public class DataVisitList
    {
        private SmartDatabase smartDatabase;

        public DataVisitList(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
        }


        public void add(DataCustomer dataCustomer)
        {
            try
            {
                smartDatabase.nonQuery("UPDATE customer SET visible = 'Ja' WHERE no = '" + dataCustomer.no + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public void remove(DataCustomer dataCustomer)
        {
            try
            {
                smartDatabase.nonQuery("UPDATE customer SET visible = '' WHERE no = '" + dataCustomer.no + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public bool check(DataCustomer dataCustomer)
        {

            try
            {
                SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM customer WHERE visble = 'Ja' AND no = '" + dataCustomer.no + "'");

                if (dataReader.Read())
                {
                    dataReader.Dispose();
                    return true;
                }
                dataReader.Dispose();
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

            return false;

        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter customerAdapter = smartDatabase.dataAdapterQuery("SELECT no, name, city, blocked FROM customer WHERE blocked = 0 ORDER BY name");

            DataSet customerDataSet = new DataSet();
            customerAdapter.Fill(customerDataSet, "activeCustomer");
            customerAdapter.Dispose();

            return customerDataSet;
        }

    }
}
