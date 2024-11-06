using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace SmartAdminMvc
{
    public class UserEnvironment
    {
        [Key, Column(Order = 0), MaxLength(20)]
        public string userId { get; set; }
        [Key, Column(Order = 1), MaxLength(20)]
        public string environmentCode { get; set; }

        [MaxLength(20)]
        public string roleCode { get; set; }


        public static async Task<List<UserEnvironment>> getFirst(SystemDatabase systemDatabase, string userId)
        {
            return await systemDatabase.Database.SqlQuery<UserEnvironment>("SELECT userId, environmentCode, roleCode FROM UserEnvironment").ToListAsync();
        }
    }
}