﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartAdminMvc
{
    public class MyConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<SystemDatabase>
    {
        public MyConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }
    }
}