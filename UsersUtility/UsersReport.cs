using System;
using System.Data;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace UsersUtility
{
    class UsersReport
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        public void GenerateUserRegistrationReport()
        {
            DataTable dtUsr = new DataTable();
            dtUsr = objusr.UserReport(DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"), DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"));
            ExportToPdf(dtUsr,"REG");
            string filen = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_REG_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
            Email.SendMail("info@eastlaw.pk", "Daily Users Report", "EastLaw - Daily Users Report", "EastLaw", filen);
            Email.SendMail("muhammad.abubakar@live.com", "Daily Users Report", "EastLaw - Daily Users Report", "EastLaw", filen);
            //Email.SendMail("muhammad.abubakar@live.com", "Daily Users Report", "EastLaw - Daily Users Report", "EastLaw", filen);
        }
        public void GenerateUserLoginReport()
        {
            DataTable dtUsr = new DataTable();
            dtUsr = objusr.UserLoginReport(DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"), DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyy"));
            ExportToPdfUserLogin(dtUsr, "LOGIN");
            string filen = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_LOGIN_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
            Email.SendMail("info@eastlaw.pk", "Daily Users Login Report", "EastLaw - Daily Users Login Report", "EastLaw", filen);
            Email.SendMail("muhammad.abubakar@live.com", "Daily Users Login Report", "EastLaw - Daily Users Login Report", "EastLaw", filen);
            //Email.SendMail("muhammad.abubakar@live.com", "Daily Users Login Report", "EastLaw - Daily Users Login Report", "EastLaw", filen);
        }
         void ExportToPdf(DataTable dt,string NamePrefix)
        {
            Document document = new Document();
            string file = "";
            file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_" + NamePrefix + "_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var logo = iTextSharp.text.Image.GetInstance("http://eastlaw.pk/images/logo_1.png");
            logo.SetAbsolutePosition(350, 800);
            document.Add(logo);
            var welcomeParagraph = new Paragraph("Users Report - Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
            document.Add(welcomeParagraph);
            //var welcomeParagraph1 = new Paragraph("Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
            //document.Add(welcomeParagraph1);

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
             Document document = new Document();
             string file = "";
             file = (Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UserReport/RPT_" + NamePrefix + "_" + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf"));
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
             document.Open();
             iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
             var logo = iTextSharp.text.Image.GetInstance("http://eastlaw.pk/images/logo_1.png");
             logo.SetAbsolutePosition(350, 800);
             document.Add(logo);
             var welcomeParagraph = new Paragraph("Users Report - Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
             document.Add(welcomeParagraph);
             //var welcomeParagraph1 = new Paragraph("Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
             //document.Add(welcomeParagraph1);

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
                     //table.AddCell(new Phrase(r[6].ToString(), font5));
                     //table.AddCell(new Phrase(r[7].ToString(), font5));
                     //table.AddCell(new Phrase(r[8].ToString(), font5));
                     //table.AddCell(new Phrase(r[9].ToString(), font5));
                     //table.AddCell(new Phrase(r[10].ToString(), font5));
                 }
             }
             document.Add(table);
             document.Close();
         }
    }
}
