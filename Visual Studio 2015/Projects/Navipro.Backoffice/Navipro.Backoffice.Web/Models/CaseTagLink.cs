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
    public class CaseTagLink
    {
        public CaseTagLink()
        {


        }

        public CaseTagLink(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            caseNo = dataReader.GetValue(0).ToString();
            caseTagCode = dataReader.GetValue(1).ToString();
        }

        [Required]
        [Display(Name = "Ärendenr")]
        public String caseNo { get; set; }

        [Required]
        [Display(Name = "Tagg")]
        public String caseTagCode { get; set; }


        public static List<CaseTagLink> getList(Database database, string caseNo)
        {
            List<CaseTagLink> caseTagLinkList = new List<CaseTagLink>();

            DatabaseQuery query = database.prepare("SELECT [Case No_], [Case Tag Code] FROM [" + database.getTableName("Case Tag Link") + "] WHERE [Case No_] = @caseNo");
            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseTagLink caseTagLink = new CaseTagLink(dataReader);
                caseTagLinkList.Add(caseTagLink);

            }

            dataReader.Close();

            return caseTagLinkList;
        }



        public static List<SelectListItem> getSelectList(Database database, string caseNo)
        {
            return getSelectList(database, caseNo, new string[] { "" });
        }

        public static List<SelectListItem> getSelectList(Database database, string caseNo, string[] selectedValue)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<CaseTagLink> caseTagLinkList = getList(database, caseNo);

            foreach (CaseTagLink item in caseTagLinkList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.caseTagCode;
                selectItem.Text = item.caseTagCode;

                if (selectedValue.Contains(item.caseTagCode))
                {
                    selectItem.Selected = true;
                }
                selectList.Add(selectItem);

            }

            return selectList;
        }

        public static string[] getSelectedArray(List<CaseTagLink> caseTagLinkList)
        {
            string[] array = new string[caseTagLinkList.Count];

            int i = 0;
            foreach (CaseTagLink item in caseTagLinkList)
            {
                array[i] = item.caseTagCode;
                i++;
            }

            return array;
        }

    }



}