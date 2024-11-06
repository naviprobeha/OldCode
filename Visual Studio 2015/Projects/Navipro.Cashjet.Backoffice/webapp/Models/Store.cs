using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace SmartAdminMvc
{
    public class Store
    {
        [Key, MaxLength(20)]
        public string code { get; set; }
        [MaxLength(50)]
        public string name { get; set; }

        [MaxLength(50)]
        public string address { get; set; }

        [MaxLength(20)]
        public string postCode { get; set; }

        [MaxLength(50)]
        public string city { get; set; }

        [MaxLength(20)]
        public string countryCode { get; set; }

        [MaxLength(30)]
        public string registrationNo { get; set; }

        [MaxLength(30)]
        public string vatRegistrationNo { get; set; }

        [MaxLength(20)]
        public string phoneNo { get; set; }

        [MaxLength(100)]
        public string email { get; set; }

        [MaxLength(100)]
        public string homePage { get; set; }

        [MaxLength(20)]
        public string salesLocationCode { get; set; }
        [MaxLength(20)]
        public string orderLocationCode { get; set; }
        
        public byte[] logo { get; set; }
        
        public int receiptItemIdentification { get; set; }

        [MaxLength(50)]
        public string receiptTextLine1 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine2 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine3 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine4 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine5 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine6 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine7 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine8 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine9 { get; set; }
        [MaxLength(50)]
        public string receiptTextLine10 { get; set; }

        public bool showBarCodeOnReceipt { get; set; }
        public bool showBarCodeOnVoucher { get; set; }

        public bool showLogoOnVoucher { get; set; }

        [MaxLength(50)]
        public string voucherTextLine1 { get; set; }
        [MaxLength(50)]
        public string voucherTextLine2 { get; set; }

        [MaxLength(50)]
        public string creditReceiptTextLine1 { get; set; }
        [MaxLength(50)]
        public string creditReceiptTextLine2 { get; set; }

        [MaxLength(20)]
        public string mainButtonPanelCode { get; set; }

        [MaxLength(20)]
        public string managerButtonPanelCode { get; set; }

        [MaxLength(20)]
        public string closingButtonPanelCode { get; set; }

        [MaxLength(20)]
        public string paymentButtonPanelCode { get; set; }
       
        public int displayUnitPrice { get; set; }

        public int maxQuantityAllowed { get; set; }

        [MaxLength(20)]
        public string creditReceiptTypeCode { get; set; }
        
        public bool askCreditReceiptNegativeAmount { get; set; }

        [MaxLength(20)]
        public string floatPaymentTypeCode { get; set; }

        public bool allowCustomerRefScanning { get; set; }

        public decimal roundingPrecision { get; set; }

        [MaxLength(20)]
        public string klarnaMerchantId { get; set; }

        [MaxLength(30)]
        public string klarnaUserName { get; set; }

        [MaxLength(100)]
        public string klarnaPassword { get; set; }

        [MaxLength(20)]
        public string klarnaPaymentTypeCode { get; set; }


        public static List<Store> getList(SystemDatabase systemDatabase, Environment environment)
        {
            return systemDatabase.Database.SqlQuery<Store>("SELECT * FROM [" + environment.code + "$Store]").ToList<Store>();
        }

        public static List<Store> getFirst(SystemDatabase systemDatabase, Environment environment)
        {
            return systemDatabase.Database.SqlQuery<Store>("SELECT TOP 1 * FROM ["+environment.code+"$Store]").ToList<Store>();
        }

        public static void checkEnvironmentTableStructure(SystemDatabase systemDatabase, Environment environment)
        {
            try
            {
                getFirst(systemDatabase, environment);
            }
            catch (Exception e)
            {                
                systemDatabase.Database.ExecuteSqlCommand("SELECT * INTO [" + environment.code + "$Store] FROM Store");
            }


        }
    }
}