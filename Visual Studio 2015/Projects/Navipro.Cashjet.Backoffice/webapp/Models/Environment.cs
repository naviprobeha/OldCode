using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace SmartAdminMvc
{
    public class Environment
    {
        [Key, MaxLength(20)]
        public string code { get; set; }
        [MaxLength(50)]
        public string description { get; set; }
        [MaxLength(20)]
        public string price1CurrencyCode { get; set; }
        [MaxLength(30)]
        public string price1Description { get; set; }
        [MaxLength(20)]
        public string price2CurrencyCode { get; set; }
        [MaxLength(30)]
        public string price2Description { get; set; }
        [MaxLength(20)]
        public string price3CurrencyCode { get; set; }
        [MaxLength(30)]
        public string price3Description { get; set; }
        [MaxLength(20)]
        public string price4CurrencyCode { get; set; }
        [MaxLength(30)]
        public string price4Description { get; set; }
        [MaxLength(20)]
        public string ownerNo { get; set; }
        [MaxLength(50)]
        public string ownerName { get; set; }


        public static List<Environment> getList(SystemDatabase systemDatabase)
        {
            return systemDatabase.Database.SqlQuery<Environment>("SELECT code, description, price1CurrencyCode, price1Description, price2CurrencyCode, price2Description, price3CurrencyCode, price3Description, price4CurrencyCode, price4Description, ownerNo, ownerName FROM Environment").ToList<Environment>();
        }

        public static List<Environment> getList(SystemDatabase systemDatabase, string userId)
        {
            return systemDatabase.Database.SqlQuery<Environment>("SELECT e.code, e.description, e.price1CurrencyCode, e.price1Description, e.price2CurrencyCode, e.price2Description, e.price3CurrencyCode, e.price3Description, e.price4CurrencyCode, e.price4Description, ownerNo, ownerName FROM Environment e, UserEnvironment ue WHERE e.code = ue.environmentCode AND ue.userId = @userId", new SqlParameter("userId", userId)).ToList<Environment>();
        }


    }
}