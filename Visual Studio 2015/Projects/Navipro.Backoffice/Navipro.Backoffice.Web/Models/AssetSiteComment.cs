using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;

namespace Navipro.Backoffice.Web.Models
{
    public class AssetSiteComment
    {
        public AssetSiteComment()
        {
         

        }

        public AssetSiteComment(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            assetSiteNo = dataReader.GetValue(0).ToString();
            name = dataReader.GetValue(1).ToString();
            code = dataReader.GetValue(2).ToString();
            text = dataReader.GetValue(3).ToString();
            lastUpdatedDate = dataReader.GetDateTime(4);
            lastUpdatedUser = dataReader.GetValue(5).ToString();
        }

        [Required]
        public String assetSiteNo { get; set; }

        public String name { get; set; }

        [Required]
        public String code { get; set; }

        public String text { get; set; }

        public DateTime lastUpdatedDate { get; set; }

        public string lastUpdatedDateText { get { return lastUpdatedDate.ToString("yyyy-MM-dd"); } }

        public String lastUpdatedUser { get; set; }

        public static List<AssetSiteComment> getList(Database database, string customerNo, string userIpAddress)
        {
            List<AssetSiteComment> assetSiteComments = new List<AssetSiteComment>();

            if (database.configuration.checkInternalNetwork(userIpAddress))
            {
                DatabaseQuery query = database.prepare("SELECT h.[No_], h.[Description], l.[Code], l.[Text], l.[Date], l.[User ID] FROM [" + database.getTableName("Asset Site Header") + "] h, [" + database.getTableName("Asset Site Comment Line") + "] l WHERE h.[Customer No_] = @customerNo AND h.[No_] = l.[Asset Site No_] ORDER BY l.[Asset Site No_], l.[Line No_]");
                query.addStringParameter("customerNo", customerNo, 20);

                Customer customer = new Customer();

                string lastAssetSite = "";

                SqlDataReader dataReader = query.executeQuery();
                while (dataReader.Read())
                {
                    AssetSiteComment assetSiteComment = new AssetSiteComment(dataReader);
                    if (assetSiteComment.assetSiteNo == lastAssetSite) assetSiteComment.name = "";
                    lastAssetSite = assetSiteComment.assetSiteNo;

                    assetSiteComments.Add(assetSiteComment);
                }
                dataReader.Close();
            }

            return assetSiteComments;
        }

        public static Dictionary<string, Customer> getDictionary(Database database)
        {
            Dictionary<string, Customer> customerTable = new Dictionary<string, Customer>();

            DatabaseQuery query = database.prepare("SELECT [No_], [Name] FROM [" + database.getTableName("Customer") + "]");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Customer customer = new Customer(dataReader);
                customerTable.Add(customer.no, customer);

            }
            dataReader.Close();

            return customerTable;
        }

    }



}