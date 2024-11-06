using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataItemCrossReference
    {
        private string _documentNo = "";
        private string _itemNo = "";
        private string _variantCode = "";
        private string _unitOfMeasureCode = "";
        private string _crossReferenceCode = "";

        private SmartDatabase smartDatabase;

        public DataItemCrossReference(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataItemCrossReference(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataItemCrossReference(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string unitOfMeasureCode { get { return _unitOfMeasureCode; } set { _unitOfMeasureCode = value; } }
        public string crossReferenceCode { get { return _crossReferenceCode; } set { _crossReferenceCode = value; } }

        public void save()
        {

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT crossReferenceCode FROM itemCrossReference WHERE documentNo = '"+_documentNo+"' AND itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND unitOfMeasureCode = '"+unitOfMeasureCode+"' AND crossReferenceCode = '"+_crossReferenceCode+"'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO itemCrossReference (documentNo, itemNo, variantCode, unitOfMeasureCode, crossReferenceCode) VALUES ('" + _documentNo + "', '" + _itemNo + "', '" + _variantCode + "', '" + _unitOfMeasureCode + "', '" + _crossReferenceCode + "')");
                }

                dataReader.Close();
                dataReader.Dispose();

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        public void delete()
        {
            try
            {
                smartDatabase.nonQuery("DELETE FROM itemCrossReference WHERE documentNo = '" + _documentNo + "' AND itemNo = '" + _itemNo + "' AND variantCode = '"+_variantCode+"' AND unitOfMeasureCode = '"+_unitOfMeasureCode+"'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._documentNo = (string)dataReader.GetValue(0);
            this._itemNo = (string)dataReader.GetValue(1);
            this._variantCode = (string)dataReader.GetValue(2);
            this._unitOfMeasureCode = (string)dataReader.GetValue(3);
            this._crossReferenceCode = (string)dataReader.GetValue(4);
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._documentNo = (string)dataRow.ItemArray.GetValue(0);
            this._itemNo = (string)dataRow.ItemArray.GetValue(1);
            this._variantCode = (string)dataRow.ItemArray.GetValue(2);
            this._unitOfMeasureCode = (string)dataRow.ItemArray.GetValue(3);
            this._crossReferenceCode = (string)dataRow.ItemArray.GetValue(4);

        }

        public static void fromDOM(XmlElement pickLineElement, SmartDatabase smartDatabase, DataPickLine dataPickLine)
        {

            XmlNodeList itemCrossRefsNodeList = pickLineElement.SelectNodes("itemCrossRefs/code");
            int i = 0;

            while (i < itemCrossRefsNodeList.Count)
            {
                XmlElement itemCrossRef = (XmlElement)itemCrossRefsNodeList[i];

                string crossReferenceCode = itemCrossRef.FirstChild.Value;


                DataItemCrossReference dataItemCrossRef = new DataItemCrossReference(smartDatabase);
                dataItemCrossRef.documentNo = dataPickLine.documentNo;
                dataItemCrossRef.itemNo = dataPickLine.itemNo;
                dataItemCrossRef.variantCode = dataPickLine.variantCode;
                dataItemCrossRef.unitOfMeasureCode = "";
                dataItemCrossRef.crossReferenceCode = crossReferenceCode;
                dataItemCrossRef.save();

                i++;
            }

        }

        public static void fromDOM(XmlElement pickLineElement, SmartDatabase smartDatabase, DataStoreLine dataStoreLine)
        {
            

            XmlNodeList itemCrossRefsNodeList = pickLineElement.SelectNodes("itemCrossRefs/code");
            int i = 0;
            while (i < itemCrossRefsNodeList.Count)
            {
                XmlElement itemCrossRef = (XmlElement)itemCrossRefsNodeList[i];

                string crossReferenceCode = itemCrossRef.FirstChild.Value;

                DataItemCrossReference dataItemCrossRef = new DataItemCrossReference(smartDatabase);
                dataItemCrossRef.documentNo = dataStoreLine.documentNo;
                dataItemCrossRef.itemNo = dataStoreLine.itemNo;
                dataItemCrossRef.variantCode = dataStoreLine.variantCode;
                dataItemCrossRef.unitOfMeasureCode = "";
                dataItemCrossRef.crossReferenceCode = crossReferenceCode;
                dataItemCrossRef.save();

                i++;
            }

        }


        public static DataItemCrossReference getItem(SmartDatabase smartDatabase, string documentNo, string code)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, itemNo, variantCode, unitOfMeasureCode, crossReferenceCode FROM itemCrossReference WHERE documentNo = '" + documentNo + "' AND crossReferenceCode = '"+code+"'");

            DataItemCrossReference dataItemCrossReference = null;

            if (dataReader.Read())
            {
                dataItemCrossReference = new DataItemCrossReference(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataItemCrossReference;
        }

        public static DataItemCrossReference getEan(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, itemNo, variantCode, unitOfMeasureCode, crossReferenceCode FROM itemCrossReference WHERE documentNo = '" + documentNo + "' AND itemNo = '" + itemNo + "' AND variantCode = '"+variantCode+"'");

            DataItemCrossReference dataItemCrossReference = null;

            if (dataReader.Read())
            {
                dataItemCrossReference = new DataItemCrossReference(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataItemCrossReference;
        }

    }

}
