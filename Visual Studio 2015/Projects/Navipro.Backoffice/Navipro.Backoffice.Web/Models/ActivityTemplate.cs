using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class ActivityTemplate
    {
        public ActivityTemplate()
        {


        }

        public ActivityTemplate(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            code = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(0).ToString()+" "+dataReader.GetValue(1).ToString();

        }

        [Required]
        [Display(Name = "Kod")]
        public String code { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }

        public static List<ActivityTemplate> getList(Database database, string resourceNo)
        {
            List<ActivityTemplate> activityTemplateList = new List<ActivityTemplate>();

            DatabaseQuery query = database.prepare("SELECT [Key Code], [Description] FROM [" + database.getTableName("Quick Key") + "] WHERE [Resource No_] = @resourceNo ORDER BY [Key Code]");
            query.addStringParameter("resourceNo", resourceNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                ActivityTemplate activityTemplate = new ActivityTemplate(dataReader);
                activityTemplateList.Add(activityTemplate);

            }

            dataReader.Close();

            return activityTemplateList;
        }

        public static ActivityType getEntry(Database database, string commisionTypeCode, string code)
        {
            ActivityType activityType = null;

            DatabaseQuery query = database.prepare("SELECT [Code], [Description] FROM [" + database.getTableName("Activity Type") + "] WHERE [Commision Type Code] = @commisionType AND [Code] = @code");
            query.addStringParameter("commisionTypeC0de", commisionTypeCode, 20);
            query.addStringParameter("code", code, 20);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                activityType = new ActivityType(dataReader);

            }

            dataReader.Close();

            return activityType;
        }



     }



}