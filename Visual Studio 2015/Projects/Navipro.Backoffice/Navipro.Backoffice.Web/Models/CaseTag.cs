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
    public class CaseTag
    {
        public CaseTag()
        {


        }

        public CaseTag(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            code = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
        }

        [Required]
        [Display(Name = "Kod")]
        public String code { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }


        public static List<CaseTag> getList(Database database)
        {
            List<CaseTag> caseTagList = new List<CaseTag>();

            DatabaseQuery query = database.prepare("SELECT [Code], [Description] FROM [" + database.getTableName("Case Tag") + "] WITH (NOLOCK)");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseTag caseTag = new CaseTag(dataReader);
                caseTagList.Add(caseTag);

            }

            dataReader.Close();

            return caseTagList;
        }

 

        public static List<SelectListItem> getSelectList(Database database)
        {
            return getSelectList(database, new string[] { "" });
        }

        public static List<SelectListItem> getSelectList(Database database, string[] selectedValue)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<CaseTag> caseTagList = getList(database);

            foreach (CaseTag item in caseTagList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.code;
                selectItem.Text = item.description;

                if (selectedValue.Contains(item.code))
                {
                    selectItem.Selected = true;                    
                }
                selectList.Add(selectItem);

            }

            return selectList;
        }

    }



}