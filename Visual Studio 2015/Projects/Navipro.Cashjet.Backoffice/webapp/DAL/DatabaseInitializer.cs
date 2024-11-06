using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartAdminMvc
{
    public class DatabaseInitializer
    {
        protected void Seed(SystemDatabase context)
        {
            //Environments
            var environments = new List<Environment>
            {
            new Environment{code="DEMO",description="Demo",price1CurrencyCode="SEK",price1Description="Normal (SEK)",price2CurrencyCode="SEK",price2Description="Outlet (SEK)",price3CurrencyCode="EUR",price3Description="Normal (EUR)",price4CurrencyCode="EUR",price4Description="Outlet (EUR)", ownerNo="", ownerName=""},
            new Environment{code="NAVIPRO",description="Navipro",price1CurrencyCode="SEK",price1Description="Normal (SEK)",price2CurrencyCode="SEK",price2Description="Outlet (SEK)",price3CurrencyCode="EUR",price3Description="Normal (EUR)",price4CurrencyCode="EUR",price4Description="Outlet (EUR)", ownerNo="", ownerName=""}
            };

            environments.ForEach(s => context.Environments.Add(s));

            //UserEnvironments
            var userEnvironments = new List<UserEnvironment>
            {
            new UserEnvironment{userId="hakan@navipro.se",environmentCode="DEMO",roleCode="NORMAL"}
            };

            userEnvironments.ForEach(s => context.UserEnvironments.Add(s));

            context.SaveChanges();

            
        }
    }
}