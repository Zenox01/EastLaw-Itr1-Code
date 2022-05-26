using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Net;


namespace EastlawUI_v2
{
    public partial class Subscription : System.Web.UI.Page
    {
        EastLawBL.Plans objplan = new EastLawBL.Plans();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["nval"] != null)
                {
                    GetActivePlans();
                    // CompAccess(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["nval"].ToString())));
                }
                else
                {
                    GetActivePlans();
                }

            }
        }
        void GetActivePlans()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objplan.GetActivePlansFrontEnd();
                gvPlans.DataSource = dt;
                gvPlans.DataBind();

                DataTable dtCorporate = new DataTable();
                dtCorporate = objplan.GetActiveCorporatePlansFrontEnd();
                gvCorporatePlans.DataSource = dtCorporate;
                gvCorporatePlans.DataBind();
            }
            catch { }
        }
        void CompAccess(int UserID)
        {
            try
            {
                DataTable dt = objusr.GetUsers(UserID);
                DataTable dtExist = new DataTable();
                dtExist = objusr.CheckOrderExist(UserID, 1);
                if (dtExist != null)
                {
                    if (dtExist.Rows.Count > 0)
                    {
                        lblEmailIDForExist.Text = dt.Rows[0]["EmailID"].ToString();
                        divOrderExist.Style["Display"] = "";
                        divPlans.Style["Display"] = "none";
                        divConfirm.Style["Display"] = "none";

                    }
                    else
                    {


                        int chk = objusr.AddInvoice(UserID, 1, UserID.ToString(), "Unpaid", 0, UserID, "CompAccess");
                        if (chk > 0)
                        {
                            lblPlan.Text = "1 x Complimentary Package - 15 Days Free Subscription";
                            lblPrice1.Text = "Rs. 0";
                            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                            // gvPlans.Visible = false;
                            lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                            //string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                            string CustName = dt.Rows[0]["FullName"].ToString();

                            GeneratePDF(chk.ToString(), (UserID.ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), "1 x Complimentary Package - 15 Days Free Subscription", "Rs. 0");

                            string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, "1 x Complimentary Package - 15 Days Free Subscription", "Rs. 0");
                            Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "Eastlaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                            string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), (UserID.ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblInvNo.Text, "1 x Complimentary Package - 15 Days Free Subscription", "Rs. 0");

                            Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), Adminmailcntn, "Order Request - Invoice", "Eastlaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                            Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), Adminmailcntn, "Order Request - Invoice", "Eastlaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                            
                            divPlans.Style["Display"] = "none";
                            divConfirm.Style["Display"] = "";

                        }
                    }
                }
            }
            catch { }
        }
        protected void gvPlans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPlans.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblPlanName = default(Label);
                Label lblPrice = default(Label);
                Label lblPlanDays = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int PlanID = Convert.ToInt32(hd.Value);
                    lblPlanName = (Label)row.FindControl("lblPlanName");
                    lblPrice = (Label)row.FindControl("lblPrice");
                    lblPlanDays = (Label)row.FindControl("lblNoOfDays");

                    Session["SelectedPlanID"] = hd.Value;
                    Session["SelectedPlanName"] = lblPlanName.Text;
                    Session["SelectedPlanPrice"] = lblPrice.Text;
                    Session["SelectedPlanDays"] = lblPlanDays.Text;
                    Response.Redirect("/member/review-order/");
                    //if (Session["ExpiredMemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["ExpiredMemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID, Session["ExpiredMemberID"].ToString(), "Unpaid", 0, int.Parse(Session["ExpiredMemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text,dt.Rows[0]["FullName"].ToString(),dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["ExpiredMemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString() ;


                    //            }
                    //        }
                    //    }
                    //}
                    //else if (Session["MemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["MemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["MemberID"].ToString()), PlanID, Session["MemberID"].ToString(), "Unpaid", 0, int.Parse(Session["MemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text, dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["MemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString();


                    //            }
                    //        }
                    //    }
                }
                else
                {
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                }



            }



            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Subscription.aspx", "gvPlans_RowEditing", ex.Message);
            }
        }
        protected void gvCorporatePlans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvCorporatePlans.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblPlanName = default(Label);
                Label lblPrice = default(Label);
                Label lblPlanDays = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int PlanID = Convert.ToInt32(hd.Value);
                    lblPlanName = (Label)row.FindControl("lblPlanName");
                    lblPrice = (Label)row.FindControl("lblPrice");
                    lblPlanDays = (Label)row.FindControl("lblNoOfDays");

                    Session["SelectedPlanID"] = hd.Value;
                    Session["SelectedPlanName"] = lblPlanName.Text;
                    Session["SelectedPlanPrice"] = lblPrice.Text;
                    Session["SelectedPlanDays"] = lblPlanDays.Text;
                    Response.Redirect("/member/review-order/");
                    //if (Session["ExpiredMemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["ExpiredMemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID, Session["ExpiredMemberID"].ToString(), "Unpaid", 0, int.Parse(Session["ExpiredMemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text,dt.Rows[0]["FullName"].ToString(),dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["ExpiredMemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString() ;


                    //            }
                    //        }
                    //    }
                    //}
                    //else if (Session["MemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["MemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["MemberID"].ToString()), PlanID, Session["MemberID"].ToString(), "Unpaid", 0, int.Parse(Session["MemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text, dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["MemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString();


                    //            }
                    //        }
                    //    }
                }
                else
                {
                    Response.Redirect("/member/lember-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                }



            }



            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Subscription.aspx", "gvPlans_RowEditing", ex.Message);
            }
        }
        void GeneratePDF(string InvID, string InvNo, string CustomerName, string CityCountry, string EmailID, string PlanName, string Price)
        {
            string html = FillDynamicValues(InvID, InvNo, CustomerName, CityCountry, EmailID, PlanName, Price);


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringReader sr = new StringReader(html);
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    // pdfDoc.Add(new Paragraph(pdfDoc.BottomMargin, "<span style='font-style:italic'>This is a computer generated Invoice and does not require signature. </span><br /><div style='border-top: 1px solid #000;width:100%'><br />www.eastlaw.pk<br />Lahore, Pakistan.<br /></div>"));
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("/store/users/invoices/INV_") + InvID + ".pdf", FileMode.Create));
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

                    writer.PageEvent = new Footer();

                    //Paragraph welcomeParagraph = new Paragraph("Hello, World!");

                    //pdfDoc.Add(welcomeParagraph);

                    pdfDoc.Close();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfDoc);
                    //Response.End();
                }
            }
        }
        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.SpacingAfter = 10F;
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Header"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Footer"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
        public partial class Footer : PdfPageEventHelper
        {

            public override void OnEndPage(PdfWriter writer, Document doc)
            {

                Paragraph footer = new Paragraph("This is a computer generated Invoice and does not require signature", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                footer.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.TotalWidth = 400;
                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell = new PdfPCell(footer);
                cell.Border = 0;
                cell.PaddingLeft = 10;
                footerTbl.AddCell(cell);

                footerTbl.WriteSelectedRows(0, -1, 0, 40, writer.DirectContent);

                Paragraph footer1 = new Paragraph("www.eastlaw.pk", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL));

                footer1.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl1 = new PdfPTable(1);
                footerTbl1.TotalWidth = 400;
                footerTbl1.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell1 = new PdfPCell(footer1);
                cell1.Border = 0;
                cell1.PaddingLeft = 10;
                footerTbl1.AddCell(cell1);

                footerTbl1.WriteSelectedRows(0, -1, 0, 30, writer.DirectContent);


                Paragraph footer11 = new Paragraph("Lahore, Pakistan", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                footer11.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl11 = new PdfPTable(1);
                footerTbl11.TotalWidth = 400;
                footerTbl11.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell11 = new PdfPCell(footer11);
                cell11.Border = 0;
                cell11.PaddingLeft = 10;
                footerTbl11.AddCell(cell11);

                footerTbl11.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            }

        }
        string FillDynamicValues(string OrderNo, string InvNo, string InvoiceTo, string CityCountry, string EmailID, string PlanName, string Price)
        {
            try
            {



                string html = "";

                string file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/Invoice.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##ORDNO##", OrderNo.PadLeft(6, '0'));
                html = html.Replace("##INVDT##", DateTime.Now.Date.ToString("dd-MM-yyyy"));
                html = html.Replace("##INVDUE##", DateTime.Now.Date.AddDays(3).ToString("dd-MM-yyyy"));
                html = html.Replace("##FULLNAME##", InvoiceTo);
                html = html.Replace("##CITYCOUNTRY##", CityCountry);
                html = html.Replace("##EMLID##", EmailID);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", Price);
                html = html.Replace("##PRCTOTAL##", Price);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string EmailContent(string Name, string InvNo, string PlanName, string Price)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InvoiceEmail.html");
                //file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InvoiceEmailComp.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", Price);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string AdminEmailContent(string Name, string CUSTEMAIL, string InvNo, string OrdNo, string PlanName, string Price)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/AdminInvoiceEmail.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", Price);
                html = html.Replace("##CUSTEMAIL##", CUSTEMAIL);
                html = html.Replace("##ORDNM##", OrdNo);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }

        void SendOrderSMS(string OrderNo, string Name, string MobileNo)
        {
            try
            {
                //string smstxt = " Dear "+Name+", Thank you for placing Order "+OrderNo+". Kindly make the required payment and send us a "
                //+"copy of the Deposit Slip at info@eastlaw.pk to confirm your order.";

                string smstxt = " Dear " + Name + ", Thank you for placing free renewal request of eastlaw.pk, your request will be process within 24 hours.";

                string mobilenumber = MobileNo;
                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1943&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

                //HTTP connection
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }
    }
}