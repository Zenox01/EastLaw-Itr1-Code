using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace EastlawUI_v2.adminpanel
{
    public partial class CasesReport : System.Web.UI.Page
    {
        EastLawBL.ErrorReporting objer = new EastLawBL.ErrorReporting();
        EastLawBL.Cases objc = new EastLawBL.Cases();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                Response.Redirect("NotAuthorize.aspx");
        }
        public void ExportExcel_XML(DataTable dt, String FileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); //This would bypass the Application_EndRequest
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReports();
                
            }
            catch { }
        }
        void GenerateReports()
        {
            try
            {
                DataTable dt = new DataTable();
                if (ddlReportType.SelectedValue == "Pending Reported Error")
                {
                    dt = objer.GetPendingErrors();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Remove("Type");
                            dt.Columns.Remove("UserID");
                            dt.Columns.Remove("WorkflowID");

                            dt.AcceptChanges();

                            ExportExcel_XML(dt, "Pending_Reported_Errors" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                        }
                    }

                }
                else if (ddlReportType.SelectedValue == "Fully Tagged Cases (Without Final Review)")
                {
                    dt = objc.GetFullyTaggedCases(0);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ExportExcel_XML(dt, "Fully_Tagged_Cases_(Without_Final_Review)" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                        }
                    }

                }
                else if (ddlReportType.SelectedValue == "Fully Tagged Cases (With Final Review)")
                {
                    dt = objc.GetFullyTaggedCases(1);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ExportExcel_XML(dt, "Fully_Tagged_Cases_(With_Final_Review)" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                        }
                    }

                }
            }
            catch { }
        }
        public void ExportToPdf(DataTable dt)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("/store/users/dailyrpt/RPT_") + DateTime.Now.Date.ToString("ddMMyyy") + ".pdf", FileMode.Create));
            document.Open();
            iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var logo = iTextSharp.text.Image.GetInstance("http://eastlaw.pk/images/logo_1.png");
            logo.SetAbsolutePosition(350, 800);
            document.Add(logo);
            var welcomeParagraph = new Paragraph("Users Report - Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
            document.Add(welcomeParagraph);
            //var welcomeParagraph1 = new Paragraph("Date: " + DateTime.Now.Date.ToString("dd/MMM/yyy"), font8);
            //document.Add(welcomeParagraph1);

            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

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
    }
}