using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Navipro.Sandberg.Common
{
    public class CommentLine
    {

        public int tableName;
        public string no;
        public int lineNo;
        public DateTime date;
        public string code;
        public string comment;
        public string variantCode;

        public CommentLine(DataRow dataRow)
        {
            this.tableName = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this.no = dataRow.ItemArray.GetValue(1).ToString();
            this.lineNo = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.date = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.code = dataRow.ItemArray.GetValue(4).ToString();
            this.comment = dataRow.ItemArray.GetValue(5).ToString();
            this.variantCode = dataRow.ItemArray.GetValue(6).ToString();

        }


    }
}
