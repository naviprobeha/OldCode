using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.IO;

namespace Navipro.Backoffice.Web.Models
{
    public class CaseAttachment
    {
        public CaseAttachment()
        {


        }

        public CaseAttachment(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            caseNo = dataReader.GetValue(0).ToString();
            caseLogLineNo = int.Parse(dataReader.GetValue(1).ToString());
            entryNo = int.Parse(dataReader.GetValue(2).ToString());
            createdDateTime = DateTime.Parse(dataReader.GetDateTime(3).ToString("yyyy-MM-dd")+" "+dataReader.GetDateTime(4).ToString("HH:mm:ss"));
            fileName = dataReader.GetValue(5).ToString();
            errorMessage = dataReader.GetValue(6).ToString();
            fromEmail = dataReader.GetValue(7).ToString();
            fromName = dataReader.GetValue(8).ToString();

        }

        [Required]
        [Display(Name = "Ärendenr")]
        public String caseNo { get; set; }

        public int caseLogLineNo { get; set; }
        public int entryNo { get; set; }
        
        public DateTime createdDateTime { get; set; }

        [Required]
        [Display(Name = "Filnamn")]
        public String fileName { get; set; }

        
        [Display(Name = "Felmeddelande")]
        public String errorMessage { get; set; }

        public String fromEmail { get; set; }
        public String fromName { get; set; }

        public byte[] data { get; set; }

        public byte[] getAttachmentContent(Database database)
        {

            DatabaseQuery query = database.prepare("SELECT [Data] FROM [CaseAttachment] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND [File Name] = @fileName");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addStringParameter("fileName", fileName, 100);

            byte[] imageData = (byte[])query.executeScalar();


            return imageData;


        }

        public byte[] getAttachmentContent_old(Database database)
        {

            DatabaseQuery query = database.prepare("SELECT [BLOB] FROM [" + database.getTableName("Case Attachment") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND [Case Log Line No_] = @caseLogLineNo AND [Entry No_] = @entryNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("caseLogLineNo", caseLogLineNo);
            query.addIntParameter("entryNo", entryNo);


            byte[] imageData = (byte[])query.executeScalar();


            return imageData;


        }


        public static List<CaseAttachment> getList(Database database, string caseNo)
        {
            List<CaseAttachment> caseAttachmentList = new List<CaseAttachment>();

            DatabaseQuery query = database.prepare("SELECT [Case No_], [Case Log Line No_], [Entry No_], [Created Date], [Created Time], [Filename], [Error Message], [From E-mail], [From Name] FROM [" + database.getTableName("Case Attachment") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo");
            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseAttachment caseAttachment = new CaseAttachment(dataReader);
                caseAttachmentList.Add(caseAttachment);

            }

            dataReader.Close();

            return caseAttachmentList;
        }


        public static CaseAttachment getEntry(Database database, string caseNo, int lineNo, int entryNo)
        {
            CaseAttachment caseAttachment = new CaseAttachment();

            DatabaseQuery query = database.prepare("SELECT [Case No_], [Case Log Line No_], [Entry No_], [Created Date], [Created Time], [Filename], [Error Message], [From E-mail], [From Name] FROM [" + database.getTableName("Case Attachment") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND [Case Log Line No_] = @caseLogLineNo AND [Entry No_] = @entryNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("caseLogLineNo", lineNo);
            query.addIntParameter("entryNo", entryNo);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseAttachment = new CaseAttachment(dataReader);
                

            }

            dataReader.Close();

            return caseAttachment;
        }

        public void create(Database database, string caseNo, int lineNo)
        {
            DatabaseQuery query = database.prepare("DELETE FROM [CaseAttachment] WHERE [Case No_] = @caseNo AND [File Name] = @fileName");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addStringParameter("fileName", fileName, 100);

            query.execute();

            query = database.prepare("INSERT INTO [CaseAttachment] ([Case No_], [Line No_], [File Name], [From E-mail], [From Name], [Created Date Time], [Data]) VALUES (@caseNo, @lineNo, @fileName, @fromEmail, @fromName, GETDATE(), @data)");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("lineNo", lineNo);
            query.addStringParameter("fileName", fileName, 100);
            query.addStringParameter("fromEmail", fromEmail, 100);
            query.addStringParameter("fromName", fromName, 100);

            query.addBlobParameter("data", data, data.Length);

            query.execute();

        }

    }



}