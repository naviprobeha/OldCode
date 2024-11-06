using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Xml;

namespace Navipro.Backoffice.Web.Models
{
    public class CaseMember
    {
        public CaseMember()
        {
            email = "";
            name = "";
            type = 0;
            resourceNo = "";
            customerNo = "";
            defaultJobNo = "";
            phoneNo = "";
            cellPhoneNo = "";

        }

        public CaseMember(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            email = dataReader.GetValue(0).ToString();
            name = dataReader.GetValue(1).ToString();
            type = int.Parse(dataReader.GetValue(2).ToString());
            resourceNo = dataReader.GetValue(3).ToString();
            customerNo = dataReader.GetValue(4).ToString();
            defaultJobNo = dataReader.GetValue(5).ToString();
            phoneNo = dataReader.GetValue(6).ToString();
            cellPhoneNo = dataReader.GetValue(7).ToString();

        }

        [Required]
        [Display(Name = "E-post")]
        public String email { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public String name { get; set; }

        public int type { get; set; }
        public String resourceNo { get; set; }

        [Display(Name = "Kundnr")]
        public String customerNo { get; set; }

        [Display(Name = "Kundnamn")]
        public String customerName { get; set; }

        [Display(Name = "Standardprojekt")]
        public String defaultJobNo { get; set; }
        public String defaultJobDescription { get; set; } 

        [Display(Name = "Telefonnr")]
        public String phoneNo { get; set; }

        [Display(Name = "Mobiltelefonnr")]
        public String cellPhoneNo { get; set; }


        public static List<CaseMember> getList(Database database)
        {
            
            return getList(database, "", null);
        }

        public static List<CaseMember> getList(Database database, string customerNo)
        {
            return getList(database, customerNo, null);
        }

        public static List<CaseMember> getList(Database database, string customerNo, DataView dataView)
        {
            List<CaseMember> caseMemberList = new List<CaseMember>();

            string filterString = "";
            string noOfRecords = "";
            string orderByString = "[Name]";

            if ((customerNo != null) && (customerNo != "")) filterString = "AND [Customer No_] = @customerNo";

            if (dataView != null)
            {
                if (dataView.query != "") filterString = filterString + " AND " + dataView.query;
                if (dataView.noOfRecords > 0) noOfRecords = "TOP " + dataView.noOfRecords;
                if (dataView.orderBy != "") orderByString = dataView.orderBy;
            }

            DatabaseQuery query = database.prepare("SELECT " + noOfRecords + " [E-mail], [Name], [Type], [Resource No_], [Customer No_], [Default Job No_], [Phone No_], [Cell Phone No_] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE 1=1 " + filterString+" ORDER BY "+orderByString);
            if ((customerNo != null) && (customerNo != "")) query.addStringParameter("customerNo", customerNo, 20);
            
            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseMember caseMember = new CaseMember(dataReader);
                caseMemberList.Add(caseMember);

            }

            dataReader.Close();

            return caseMemberList;
        }

        public static List<CaseMember> getResources(Database database)
        {
            List<CaseMember> caseMemberList = new List<CaseMember>();

            DatabaseQuery query = database.prepare("SELECT [E-mail], [Name], [Type], [Resource No_], [Customer No_], [Default Job No_], [Phone No_], [Cell Phone No_] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE [Type] = 1");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseMember caseMember = new CaseMember(dataReader);
                caseMemberList.Add(caseMember);

            }

            dataReader.Close();

            return caseMemberList;
        }


        public static CaseMember getEntry(Database database, string email)
        {
            CaseMember caseMember = null;

            DatabaseQuery query = database.prepare("SELECT [E-mail], [Name], [Type], [Resource No_], [Customer No_], [Default Job No_], [Phone No_], [Cell Phone No_] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE [E-mail] = @email");
            query.addStringParameter("email", email, 100);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseMember = new CaseMember(dataReader);
            }

            dataReader.Close();

            return caseMember;
        }

        public static string[] getOrdererList(Database database)
        {
            List<CaseMember> caseMemberList = getList(database);
            String[] ordererList = new String[caseMemberList.Count];

            int i = 0;
            foreach (CaseMember item in caseMemberList)
            {
                ordererList[i] = item.email;

                i++;
            }

            return ordererList;
        }

        public static List<SelectListItem> getResourceSelectList(Database database, string[] selectedValue)
        {
            return getResourceSelectList(database, selectedValue, false);
        }
        


        public static List<SelectListItem> getResourceSelectList(Database database, string[] selectedValue, bool includeBlank)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<CaseMember> resourceList = getResources(database);

            if (includeBlank)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = "- Ej tilldelad -";
                selectListItem.Value = "";
                selectList.Add(selectListItem);

            }

            if (selectedValue == null) selectedValue = new string[1];

            foreach (CaseMember item in resourceList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.resourceNo;
                selectItem.Text = item.name;
                
                if (selectedValue.Contains(item.resourceNo))
                {
                    selectItem.Selected = true;
                }
                selectList.Add(selectItem);
                
            }

            return selectList;
        }

        public static CaseMember getEntryByPhoneNo(Database database, string phoneNo)
        {
            CaseMember caseMember = null;

            if (phoneNo.Substring(0, 3) == "+46") phoneNo = "0" + phoneNo.Substring(3);

            DatabaseQuery query = database.prepare("SELECT [E-mail], [Name], [Type], [Resource No_], [Customer No_], [Default Job No_], [Phone No_], [Cell Phone No_] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE REPLACE(REPLACE([Phone No_], '-', ''), ' ', '') = @phoneNo OR REPLACE(REPLACE([Alt_ Phone No_], '-', ''), ' ', '') = @phoneNo OR REPLACE(REPLACE([Cell Phone No_], '-', ''), ' ', '') = @phoneNo");
            query.addStringParameter("phoneNo", phoneNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
               caseMember = new CaseMember(dataReader);
            }

            dataReader.Close();

            return caseMember;
        }

        public static List<CaseMember> applyCustomerInfo(List<CaseMember> caseMemberList, Dictionary<string, Customer> customerTable)
        {
            int i = 0;
            while (i < caseMemberList.Count)
            {
                if (customerTable.ContainsKey(caseMemberList[i].customerNo))
                {
                    Customer customer = customerTable[caseMemberList[i].customerNo];
                    caseMemberList[i].customerName = customerTable[caseMemberList[i].customerNo].name;
                }
                i++;
            }

            return caseMemberList;
        }

        public XmlDocument toDOM()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><caseMember/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "email", email, "");
            NAVConnection.addElement(docElement, "name", name, "");
            NAVConnection.addElement(docElement, "phoneNo", phoneNo, "");
            NAVConnection.addElement(docElement, "cellPhoneNo", cellPhoneNo, "");
            NAVConnection.addElement(docElement, "customerNo", customerNo, "");
            NAVConnection.addElement(docElement, "defaultJobNo", defaultJobNo, "");

            return xmlDoc;
        }

        public void submitUpdate(Database database, User user)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "updateCaseMember", toDOM(), out responseElement);

        }

        public void submitCreate(Database database, User user)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCaseMember", toDOM(), out responseElement);

        }

        public void submitDelete(Database database, User user)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "deleteCaseMember", toDOM(), out responseElement);

        }
    }



}