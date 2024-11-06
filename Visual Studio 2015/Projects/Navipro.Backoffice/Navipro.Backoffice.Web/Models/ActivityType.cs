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
    public class ActivityType
    {
        public ActivityType()
        {


        }

        public ActivityType(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            commisionTypeCode = dataReader.GetValue(0).ToString();
            code = dataReader.GetValue(1).ToString();
            description = dataReader.GetValue(2).ToString();
            
        }

        [Required]
        [Display(Name = "Uppdragstyp")]
        public String commisionTypeCode { get; set; }

        [Required]
        [Display(Name = "Kod")]
        public String code { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }

        public static List<ActivityType> getList(Database database)
        {
            List<ActivityType> activityTypeList = new List<ActivityType>();

            DatabaseQuery query = database.prepare("SELECT [Commision Type Code], [Code], [Description] FROM [" + database.getTableName("Activity Type") + "] ORDER BY [Commision Type Code], [Code]");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                ActivityType activityType = new ActivityType(dataReader);
                activityTypeList.Add(activityType);

            }

            dataReader.Close();

            return activityTypeList;
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

        public static List<SelectListItem> getSelectList(Database database)
        {
            return getSelectList(database, "");
        }

        public static List<SelectListItem> getSelectList(Database database, string selectedValue)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<ActivityType> activityTypeList = getList(database);

            foreach (ActivityType item in activityTypeList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.commisionTypeCode+"|"+item.code;
                selectItem.Text = item.commisionTypeCode + " - " + item.code;
                if (selectItem.Value.Replace("Å", "A").Replace("Ä", "A").Replace("Ö", "O") == selectedValue) selectItem.Selected = true;
                selectList.Add(selectItem);

            }

            return selectList;
        }

    }



}