﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{

    public class Worker
    {
        // By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
        public int ID { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Office { get; set; }

        public string Extn { get; set; }

        public string Salary { get; set; }

    }
}