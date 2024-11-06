using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace Navipro.SmartInventory
{

    public class DataUserCollection : CollectionBase
    {
        public DataUser this[int index]
        {
            get { return (DataUser)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataUser dataUser)
        {
            return (List.Add(dataUser));
        }
        public int IndexOf(DataUser dataUser)
        {
            return (List.IndexOf(dataUser));
        }
        public void Insert(int index, DataUser dataUser)
        {
            List.Insert(index, dataUser);
        }
        public void Remove(DataUser dataUser)
        {
            List.Remove(dataUser);
        }
        public bool Contains(DataUser dataUser)
        {
            return (List.Contains(dataUser));
        }

        public DataSet getDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("user");
            
            DataColumn dataColumn = new DataColumn("code");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn("name");
            dataTable.Columns.Add(dataColumn);
            
            int i = 0;
            while(i < Count)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = this[i].code;
                dataRow[1] = this[i].name;
                dataTable.Rows.Add(dataRow);

                i++;
            }

            dataSet.Tables.Add(dataTable);
            return dataSet;
        }

    }
}
