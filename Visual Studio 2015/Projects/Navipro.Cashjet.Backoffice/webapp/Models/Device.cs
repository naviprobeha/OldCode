using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace SmartAdminMvc
{
    public class Device
    {
        [Key, MaxLength(20)]
        public string code { get; set; }
        [MaxLength(50)]
        public string description { get; set; }

        [MaxLength(10)]
        public string serialNo { get; set; }

        [MaxLength(20)]
        public string storeCode { get; set; }

        [MaxLength(20)]
        public string lastReceiptNo { get; set; }

        [MaxLength(20)]
        public string lastRegisteredReceiptNo { get; set; }

        public int autoLogOff { get; set; }

        [MaxLength(100)]
        public string deviceToken { get; set; }

        public int deviceType { get; set; }

        [NotMapped]
        public string deviceTypeString
        {
            get
            {
                if (deviceType == 0) return "POS";
                if (deviceType == 1) return "WEB";
                return "";
            }
        }

        public static List<Device> getList(SystemDatabase systemDatabase, Environment environment)
        {
            return systemDatabase.Database.SqlQuery<Device>("SELECT * FROM [" + environment.code + "$Device]").ToList<Device>();
        }

        public static List<Device> getFirst(SystemDatabase systemDatabase, Environment environment)
        {
            return systemDatabase.Database.SqlQuery<Device>("SELECT TOP 1 * FROM [" + environment.code + "$Device]").ToList<Device>();
        }

        public static void checkEnvironmentTableStructure(SystemDatabase systemDatabase, Environment environment)
        {
            try
            {
                getFirst(systemDatabase, environment);
            }
            catch (Exception e)
            {
                systemDatabase.Database.ExecuteSqlCommand("SELECT * INTO [" + environment.code + "$Device] FROM Device");
            }


        }
    }
}