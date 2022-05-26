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
using System.Net.Http;

namespace EastlawUI_v2.adminpanel
{
    public partial class GenerateUserOrder : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect("default.aspx");
                    }
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    GetActivePlans(0);
                }
            }
            catch { }
        }
        void GetActivePlans(int PlanID)
        {
            try
            {
                DataTable dt = new DataTable();
                if (PlanID == 0)
                {
                    dt = objPlan.GetActivePlans();
                    ddlPlan.DataValueField = "ID";
                    ddlPlan.DataTextField = "PlanName";
                    ddlPlan.DataSource = dt;
                    ddlPlan.DataBind();

                    ddlPlan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
                else
                {
                    dt = objPlan.GetPlans(PlanID);
                    if (dt.Rows.Count > 0)
                    {
                        lblPlanNoOfDays.Text = dt.Rows[0]["NoofDays"].ToString();
                        txtAmt.Text = dt.Rows[0]["Price"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetActivePlans", ex.Message);
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivePlans(int.Parse(ddlPlan.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", ex.Message);
            }
        }
        bool PlaceOrder(string EmailID)
        {
            try
            {
                int PlanID = int.Parse(ddlPlan.SelectedValue);
                string PaymentMethod = ddlPaymentMethod.SelectedValue;

                //string txtNoOfdays = "";
                string PlanName = "";
                string PlanAmt = "";
                string PlanNoOfDays = "";
                GetActivePlansDetails(int.Parse(ddlPlan.SelectedValue), ref PlanName, ref PlanNoOfDays, ref PlanAmt);

                DataTable dt = objusr.CheckUserExistByEmail(EmailID);

                    DataTable dtExist = objusr.CheckOrderExist( int.Parse(dt.Rows[0]["ID"].ToString()), PlanID);
                    if (dtExist != null)
                    {
                        //if (dtExist.Rows.Count > 0)
                        //{
                        //    //lblEmailIDForExist.Text = dt.Rows[0]["EmailID"].ToString();
                        //    divError.InnerHtml = "Order Already Exist";
                        //    divError.Style["Display"] = "";

                        //}
                        //else
                        //{
                            int chk = objusr.AddInvoice(int.Parse(dt.Rows[0]["ID"].ToString()), PlanID, dt.Rows[0]["ID"].ToString(), "Unpaid", 0, 999999, PaymentMethod);
                            if (chk > 0)
                            {
                                //lblPlan.Text = "1 x " + lblPlanName.Text;
                                //lblPrice1.Text = lblPrice.Text;
                                //lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                                //lblInvNo.Text = chk.ToString().PadLeft(6, '0');
                                string CustName = dt.Rows[0]["FullName"].ToString();

                                GeneratePDF(chk.ToString(), (dt.Rows[0]["ID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), ddlPlan.SelectedItem.Text, txtAmt.Text.Trim());

                                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), chk.ToString().PadLeft(6, '0'), ddlPlan.SelectedItem.Text, txtAmt.Text.Trim(), PlanNoOfDays.ToString());
                                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), chk.ToString().PadLeft(6, '0'), (dt.Rows[0]["ID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), ddlPlan.SelectedItem.Text, txtAmt.Text.Trim());
                                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                SendOrderSMS(chk.ToString().PadLeft(6, '0'), dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString(), PlanNoOfDays, txtAmt.Text.Trim());

                                return true;
                                
                            }
                       // }
                    }
                
                
                return false;

            }
            catch
            {
                return false;
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
                    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    Document pdfDoc = new Document(PageSize.A4);
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


                //Paragraph footer11 = new Paragraph("Lahore, Pakistan", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                //footer11.Alignment = Element.ALIGN_CENTER;

                //PdfPTable footerTbl11 = new PdfPTable(1);
                //footerTbl11.TotalWidth = 400;
                //footerTbl11.HorizontalAlignment = Element.ALIGN_CENTER;
                //PdfPCell cell11 = new PdfPCell(footer11);
                //cell11.Border = 0;
                //cell11.PaddingLeft = 10;
                //footerTbl11.AddCell(cell11);
                // footerTbl11.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

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
                html = html.Replace("##INVDT##", String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now.Date));
                html = html.Replace("##INVDUE##", String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now.Date.AddDays(3)));
                html = html.Replace("##FULLNAME##", InvoiceTo);
                html = html.Replace("##CITYCOUNTRY##", CityCountry);
                html = html.Replace("##EMLID##", EmailID);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                html = html.Replace("##PRCTOTAL##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string EmailContent(string Name, string InvNo, string PlanName, string Price, string Days)
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
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                html = html.Replace("##PLNDYS##", Days);
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
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
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
        void SendOrderSMS(string OrderNo, string Name, string MobileNo, string NoOfDays, string Price)
        {
            try
            {
                //string smstxt = " Dear "+Name+", Thank you for placing Order "+OrderNo+". Kindly make the required payment and send us a "
                //+"copy of the Deposit Slip at info@eastlaw.pk to confirm your order.";

                //string smstxt = " Dear " + Name + ", Thank you for placing free renewal request of eastlaw.pk, your request will be process within 24 hours.";
                string smstxt = " Thank you for placing Order No. " + OrderNo + " for Subscription Package of " + NoOfDays + " Days. Please make the payment of Rs. " + string.Format("{0:0,0}", int.Parse(Price.ToString().Replace("Rs. ", ""))) + "/- and update us for Account Activation. Customer Care No. 03108131610";
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

        void GetActivePlansDetails(int PlanID, ref string PlanName, ref string NoOfDays, ref string Amt)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objPlan.GetPlans(PlanID);
                if (dt.Rows.Count > 0)
                {
                    PlanName = dt.Rows[0]["PlanName"].ToString();
                    NoOfDays = dt.Rows[0]["NoofDays"].ToString();
                    Amt = dt.Rows[0]["Price"].ToString();

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                bool chk = PlaceOrder(txtEmailID.Text);
                if (chk == true)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch { }
        }
    }
}