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
    public partial class UsersReports : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
                txtFromDate.Text = DateTime.Now.Date.ToString("MM/dd/yyy");
                txtToDate.Text = DateTime.Now.Date.ToString("MM/dd/yyy");

                //DataTable dtUsr = new DataTable();
                //dtUsr = objusr.UserReport(txtFromDate.Text, txtToDate.Text);
                //ExportToPdf(dtUsr);
            }

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
                if (ddlReportType.SelectedValue == "Users")
                {
                    DataTable dtUsr = new DataTable();
                    dtUsr = objusr.UserReport(txtFromDate.Text, txtToDate.Text);
                   
                    ExportExcel_XML(dtUsr, "Users_Report_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                }
                else if (ddlReportType.SelectedValue == "UserLogin")
                {
                    DataTable dtUsr = new DataTable();
                    dtUsr = objusr.UserLoginReport(txtFromDate.Text, txtToDate.Text);
                    ExportExcel_XML(dtUsr, "Users_Login_Report_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                }
                else if (ddlReportType.SelectedValue == "Orders")
                {
                    DataTable dtUsr = new DataTable();
                    dtUsr = objusr.UserOrdersReport(txtFromDate.Text, txtToDate.Text);
                    ExportExcel_XML(dtUsr, "Users_Orders_Report_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
                }
                else if (ddlReportType.SelectedValue == "UsersLog")
                {
                    DataTable dtUsr = new DataTable();
                    dtUsr = objusr.GetAuditLogByUserEmail(txtEmailID.Text,txtFromDate.Text, txtToDate.Text);
                    ExportExcel_XML(dtUsr, "Users_Logs_"+txtEmailID.Text+"_" + DateTime.Now.Date.ToString("dd/MM/yyy"));
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

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "UsersLog")
            {
                divUserEmailID.Style["Display"] = "";
                rfvEmail.Enabled = true;
            }
            else
            {
                divUserEmailID.Style["Display"] = "none";
                rfvEmail.Enabled = false;
            }
        }
    }
}