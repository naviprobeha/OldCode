using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary of bin content for Items.
    /// </summary>
    public class DataItemBinContent : ServiceArgument
    {
        private string _itemNo;
        private string _variantCode;
        private string _binCode;
        private float _quantity;

        private SmartDatabase smartDatabase;

        public DataItemBinContent(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataItemBinContent(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataItemBinContent(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string binCode { get { return _binCode; } set { _binCode = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }

        public void save()
        {
            try
            {

                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT itemNo, variantCode, binCode FROM itemBinContent WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND binCode = '" + _binCode + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO itemBinContent (itemNo, variantCode, binCode, quantity) VALUES ('" + _itemNo + "', '" + _variantCode + "', '" + _binCode + "', '" + _quantity + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE itemBinContent SET quantity = '" + _quantity + "' WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND binCode = '" + _binCode + "'");
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
                smartDatabase.nonQuery("DELETE FROM itemBinContent WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND binCode = '" + _binCode + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._binCode = (string)dataReader.GetValue(0).ToString();
            this._quantity = dataReader.GetFloat(1);
            this._itemNo = (string)dataReader.GetValue(2).ToString();
            this._variantCode = (string)dataReader.GetValue(3).ToString();
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._binCode = dataRow.ItemArray.GetValue(0).ToString();
            this._quantity = float.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._itemNo = dataRow.ItemArray.GetValue(2).ToString();
            this._variantCode = dataRow.ItemArray.GetValue(3).ToString();
        }

        public static void fromDOM(XmlElement activitiesElement, SmartDatabase smartDatabase)
        {
            deleteItemBinContent(smartDatabase);

            XmlNodeList activityNodeList = activitiesElement.SelectNodes("binContentList");
            int i = 0;
            while (i < activityNodeList.Count)
            {

                XmlElement activityElement = (XmlElement)activityNodeList[i];
                string itemNo = activityElement.GetAttribute("itemNo");
                string variantCode = activityElement.GetAttribute("variantCode");

                string binCode = activityElement.SelectSingleNode("binCode").FirstChild.Value;
                float quantity = float.Parse(activityElement.SelectSingleNode("quantity").FirstChild.Value);

                DataItemBinContent dataItemBinContent = new DataItemBinContent(smartDatabase);

                dataItemBinContent.itemNo = itemNo;
                dataItemBinContent.variantCode = variantCode;
                dataItemBinContent.binCode = binCode;
                dataItemBinContent.quantity = quantity;

                dataItemBinContent.save();

                i++;

            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT binCode, quantity FROM itemBinContent ORDER BY binCode");

            DataSet itemBinContentListDataSet = new DataSet();
            adapter.Fill(itemBinContentListDataSet, "itemBinContent");
            adapter.Dispose();

            return itemBinContentListDataSet;

        }

        public static DataItemBinContent getItemBinContent(SmartDatabase smartDatabase, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT binCode, quantity FROM itemBinContent WHERE itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "'");

            DataItemBinContent dataItemBinContent = null;
 
            if (dataReader.Read())
            {
                dataItemBinContent = new DataItemBinContent(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataItemBinContent;
        }

        public static void deleteItemBinContent(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM itemBinContent");
        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
