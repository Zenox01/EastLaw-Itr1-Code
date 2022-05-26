using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace AdminUtility
{
    class UsersReport
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        public void GenerateUserRegistrationReport()
        {
            try
            {
                DataTable dtUsr = new DataTable();
                dtUsr = objusr.UserReport(DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"), DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"));
                ExportToPdf(dtUsr, "REG");
                string filen = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_REG_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
               
                Email.AWSSimpleEmail("muhammad.abubakar@live.com", "Daily Users Report", "EastLaw - Daily Users Report", "EastLaw", filen);
                Email.AWSSimpleEmail("staff1@eastlaw.pk", "Daily Users Report", "EastLaw - Daily Users Report", "EastLaw", filen);
                
            }
            catch(Exception ex)
            {
                WriteLogs("UserReg_Report" + DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy") + " Error Message: " + ex.Message, "UserRegReport_Failed_Error");
            }
        }
        public void GenerateUserLoginReport()
        {
            try
            {
                DataTable dtUsr = new DataTable();
                dtUsr = objusr.UserLoginReport(DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"), DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"));
                ExportToPdfUserLogin(dtUsr, "LOGIN");
                string filen = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_LOGIN_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
               
                Email.AWSSimpleEmail("muhammad.abubakar@live.com", "Daily Users Login Report", "EastLaw - Daily Users Login Report", "EastLaw", filen);

               
            }
            catch(Exception ex)
            {
                WriteLogs("Userlogin_Report" + DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy") + " Error Message: "+ ex.Message, "UserLoginReport_Failed_Error");
            }
        }
         void ExportToPdf(DataTable dt,string NamePrefix)
        {
            Document document = new Document();
            string file = "";
            file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_" + NamePrefix + "_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            
            var welcomeParagraph = new Paragraph("Users Report - Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
            document.Add(welcomeParagraph);
            

            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPRow row = null;
            //float[] widths = new float[] { 4f, 4f, 4f, 4f };

            //table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Users"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {

                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    table.AddCell(new Phrase(r[0].ToString(), font5));
                    table.AddCell(new Phrase(r[1].ToString(), font5));
                    table.AddCell(new Phrase(r[2].ToString(), font5));
                    table.AddCell(new Phrase(r[3].ToString(), font5));
                    table.AddCell(new Phrase(r[4].ToString(), font5));
                    table.AddCell(new Phrase(r[5].ToString(), font5));
                    table.AddCell(new Phrase(r[6].ToString(), font5));
                    table.AddCell(new Phrase(r[7].ToString(), font5));
                    table.AddCell(new Phrase(r[8].ToString(), font5));
                    table.AddCell(new Phrase(r[9].ToString(), font5));
                    table.AddCell(new Phrase(r[10].ToString(), font5));
                }
            }
            document.Add(table);
            document.Close();
        }
         void ExportToPdfUserLogin(DataTable dt, string NamePrefix)
         {
            dt.WriteXml("test");
             Document document = new Document();
             string file = "";
             file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_" + NamePrefix + "_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
             document.Open();
             iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
             
             var welcomeParagraph = new Paragraph("Users Report - Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
             document.Add(welcomeParagraph);
             

             iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);

             PdfPTable table = new PdfPTable(dt.Columns.Count);
             PdfPRow row = null;
             

             table.WidthPercentage = 100;
             int iCol = 0;
             string colname = "";
             PdfPCell cell = new PdfPCell(new Phrase("Users"));

             cell.Colspan = dt.Columns.Count;

             foreach (DataColumn c in dt.Columns)
             {

                 table.AddCell(new Phrase(c.ColumnName, font5));
             }

             foreach (DataRow r in dt.Rows)
             {
                 if (dt.Rows.Count > 0)
                 {
                     table.AddCell(new Phrase(r[0].ToString(), font5));
                     table.AddCell(new Phrase(r[1].ToString(), font5));
                     table.AddCell(new Phrase(r[2].ToString(), font5));
                     table.AddCell(new Phrase(r[3].ToString(), font5));
                     table.AddCell(new Phrase(r[4].ToString(), font5));
                     table.AddCell(new Phrase(r[5].ToString(), font5));
                     table.AddCell(new Phrase(r[6].ToString(), font5));
                     table.AddCell(new Phrase(r[7].ToString(), font5));
                     table.AddCell(new Phrase(r[8].ToString(), font5));
                     table.AddCell(new Phrase(r[9].ToString(), font5));
                     //table.AddCell(new Phrase(r[9].ToString(), font5));
                     //table.AddCell(new Phrase(r[10].ToString(), font5));
                 }
             }
             document.Add(table);
             document.Close();
         }
        public void WriteLogs(string msg, string filename)
        {
            try
            {
                string FolderName = DateTime.Now.ToString("MMyyyy");

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + ""));

                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/" + filename + ".txt"), true);
                
                sw.WriteLine("------------------------------------" + DateTime.Now + "-------------------");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/LoggingError" + ".txt"), true);
                
                sw.WriteLine("------------------- Error In Loging eroor -------------------" + DateTime.Now.AddMinutes(28) + "-------------------");
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
