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
    public class CaseStatus
    {
        public CaseStatus()
        {


        }

        public CaseStatus(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            code = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
            sortOrder = dataReader.GetInt32(2);
            allowClosing = false;
            if (dataReader.GetValue(3).ToString() == "1") allowClosing = true;
        }

        [Required]
        [Display(Name = "Kod")]
        public String code { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }

        [Required]
        [Display(Name = "Sortering")]
        public int sortOrder { get; set; }

  
        public bool allowClosing { get; set; }

        public static List<CaseStatus> getList(Database database)
        {
            List<CaseStatus> statusList = new List<CaseStatus>();

            DatabaseQuery query = database.prepare("SELECT [Code], [Description], [Sort Order], [Allow Closing] FROM [" + database.getTableName("Case Status") + "] WITH (NOLOCK) WHERE [Sort Order] > 0 ORDER BY [Sort Order]");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseStatus caseStatus = new CaseStatus(dataReader);
                statusList.Add(caseStatus);

            }

            dataReader.Close();

            return statusList;
        }

        public static CaseStatus getEntryByInt(Database database, int sortOrder)
        {
            CaseStatus caseStatus = null;

            DatabaseQuery query = database.prepare("SELECT [Code], [Description], [Sort Order], [Allow Closing] FROM [" + database.getTableName("Case Status") + "] WITH (NOLOCK) WHERE [Sort Order] = @sortOrder");
            query.addIntParameter("sortOrder", sortOrder);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseStatus = new CaseStatus(dataReader);                
            
            }

            dataReader.Close();

            return caseStatus;
        }

        public static List<SelectListItem> getSelectList(Database database)
        {
            return getSelectList(database);
        }

        public static List<SelectListItem> getSelectList(Database database, string defaultStatus)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<CaseStatus> statusList = getList(database);

            foreach (CaseStatus item in statusList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.code;
                selectItem.Text = item.code;
                if (item.code.Replace("Å", "A").Replace("Ä", "A").Replace("Ö", "O") == defaultStatus) selectItem.Selected = true;
                selectList.Add(selectItem);

            }

            return selectList;
        }

    }



}