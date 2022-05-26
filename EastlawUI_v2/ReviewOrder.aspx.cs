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
using System.Collections.Specialized;
using System.Security.Cryptography;
namespace EastlawUI_v2
{
    public partial class ReviewOrder : System.Web.UI.Page
    {
        EastLawBL.Plans objplan = new EastLawBL.Plans();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //                GeneratePDF("002752", "24392752", "Lahore High Court", "Lahore, Pakistan", "sheraz.hassan@lhc.gov.pk", "Corporate Premium Package B - 12 Months - Upto 5 Users - 365 Day", "40480");
                GetSelectedPlan();
            }
        }
        void GetSelectedPlan()
        {
            try
            {
                if (Session["SelectedPlanID"] != null)
                {
                    lblPlanName.Text = Session["SelectedPlanName"].ToString();
                    lblPlanAmt.Text = Session["SelectedPlanPrice"].ToString();
                    lblPlanDays.Text = Session["SelectedPlanDays"].ToString();
                }
            }
            catch { }
        }
        bool PlaceOrder()
        {
            try
            {
                int PlanID = Convert.ToInt32(Session["SelectedPlanID"].ToString());
                string lblPlanName = Session["SelectedPlanName"].ToString();
                string lblPrice = Session["SelectedPlanPrice"].ToString();
                lblPrice = lblPrice.Replace(",", "");
                string PaymentMethod = "";
                if (radioBankTransfer.Checked == true)
                    PaymentMethod = "Bank Transfer";
                else if (radioCourier.Checked == true)
                    PaymentMethod = "Demand Draft / PO / Cross Cheque";
                else if (radioEasyPaisa.Checked == true)
                    PaymentMethod = "EasyPaisa";
                else if (radioJazzCashCard.Checked == true)
                    PaymentMethod = "JazzCashMasterCard";
                else if (radioJazzCashDebitCard.Checked == true)
                    PaymentMethod = "JazzCashDebitCard";
                else if (radioJazzCashShop.Checked == true)
                    PaymentMethod = "JazzCashShop";
                else if (radioJazzCashMobileAccount.Checked == true)
                    PaymentMethod = "JazzCashMobileAccount";


                if (Session["ExpiredMemberID"] != null)
                {
                    DataTable dt = objusr.GetUsers(int.Parse(Session["ExpiredMemberID"].ToString()));

                    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID);
                    if (dtExist != null)
                    {
                        if (dtExist.Rows.Count > 0)
                        {
                            lblEmailIDForExist.Text = dt.Rows[0]["EmailID"].ToString();
                            divOrderExist.Style["Display"] = "";
                            tbFields.Style["Dispay"] = "none";

                        }
                        else
                        {
                            int chk = objusr.AddInvoice(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID, Session["ExpiredMemberID"].ToString(), "Unpaid", 0, int.Parse(Session["ExpiredMemberID"].ToString()), PaymentMethod);
                            if (chk > 0)
                            {
                                //lblPlan.Text = "1 x " + lblPlanName.Text;
                                //lblPrice1.Text = lblPrice.Text;
                                //lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                                //lblInvNo.Text = chk.ToString().PadLeft(6, '0');
                                string CustName = dt.Rows[0]["FullName"].ToString();

                                GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.ToString(), lblPrice.ToString());

                                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), chk.ToString().PadLeft(6, '0'), lblPlanName.ToString(), lblPrice.ToString(), lblPlanDays.Text);
                                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), chk.ToString().PadLeft(6, '0'), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.ToString(), lblPrice.ToString());
                                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                SendOrderSMS(chk.ToString().PadLeft(6, '0'), dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString(), lblPlanDays.Text, lblPlanAmt.Text);



                                Session["OrderAmt"] = lblPrice.ToString();
                                Session["OrderRefNum"] = chk.ToString().PadLeft(6, '0');
                                Session["OrderMemberID"] = Session["ExpiredMemberID"].ToString();
                                Session["OrderPlanID"] = PlanID.ToString();

                                return true;
                                //Response.Redirect("/Member/Order-Confirmation");


                            }
                        }
                    }
                }
                else if (Session["MemberID"] != null)
                {
                    DataTable dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));

                    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["MemberID"].ToString()), PlanID);
                    if (dtExist != null)
                    {
                        if (dtExist.Rows.Count > 0)
                        {
                            lblEmailIDForExist.Text = dt.Rows[0]["EmailID"].ToString();
                            divOrderExist.Style["Display"] = "";
                            tbFields.Style["Display"] = "none";


                        }
                        else
                        {
                            int chk = objusr.AddInvoice(int.Parse(Session["MemberID"].ToString()), PlanID, Session["MemberID"].ToString(), "Unpaid", 0, int.Parse(Session["MemberID"].ToString()), PaymentMethod);
                            if (chk > 0)
                            {
                                //lblPlan.Text = "1 x " + lblPlanName.Text;
                                //lblPrice1.Text = lblPrice.Text;
                                // lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                                // gvPlans.Visible = false;
                                // lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                                string CustName = dt.Rows[0]["FullName"].ToString();


                                GeneratePDF(chk.ToString(), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.ToString(), lblPrice.ToString());

                                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), chk.ToString().PadLeft(6, '0'), lblPlanName.ToString(), lblPrice.ToString(), lblPlanDays.Text);
                                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), chk.ToString().PadLeft(6, '0'), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.ToString(), lblPrice.ToString());
                                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                                SendOrderSMS(chk.ToString().PadLeft(6, '0'), dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString(), lblPlanDays.Text, lblPlanAmt.Text);



                                Session["OrderAmt"] = lblPrice.ToString();
                                Session["OrderRefNum"] = chk.ToString().PadLeft(6, '0');
                                Session["OrderMemberID"] = Session["MemberID"].ToString();
                                Session["OrderPlanID"] = PlanID.ToString();

                                return true;



                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
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
                //html = html.Replace("##INVDT##", "19/12/2016");
                //html = html.Replace("##INVDUE##", "19/12/2016");
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

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioEasyPaisa.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                    {
                        EasyPaisa();
                    }
                }
                else if (radioBankTransfer.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                        Response.Redirect("/member/order-confirmation");

                }
                else if (radioCourier.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                        Response.Redirect("/member/order-confirmation");

                }
                else if (radioJazzCashMobileAccount.Checked == true)
                {
                    bool chk =  PlaceOrder();
                    if (chk == true)
                    {

                        //Session["OrderRefNum"] = "55545";
                        //Session["OrderAmt"] = "5000";
                        JazzCashAccount(this.Page, "MWALLET");
                        Session["JazzCash"] = "Yes";

                    }

                }
                else if (radioJazzCashCard.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                    {

                        //Session["OrderRefNum"] = "55545";
                        //Session["OrderAmt"] = "5000";
                        JazzCashAccount(this.Page, "MIGS");
                        Session["JazzCash"] = "Yes";

                    }

                }
                else if (radioJazzCashDebitCard.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                    {

                        //Session["OrderRefNum"] = "55545";
                        //Session["OrderAmt"] = "5000";
                        JazzCashAccount(this.Page, "PAY");
                        Session["JazzCash"] = "Yes";

                    }

                }
                else if (radioJazzCashShop.Checked == true)
                {
                    bool chk = PlaceOrder();
                    if (chk == true)
                    {

                        //Session["OrderRefNum"] = "55545";
                        //Session["OrderAmt"] = "5000";
                        JazzCashAccount(this.Page, "OTC");
                        Session["JazzCash"] = "Yes";

                    }

                }
                //else
                //{
                //    PlaceOrder();
                //}
            }
            catch { }
        }

        async void EasyPaisa()
        {
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("storeId", "4023"));
                values.Add(new KeyValuePair<string, string>("amount", Session["OrderAmt"].ToString().Replace("Rs. ", "")));
                values.Add(new KeyValuePair<string, string>("postBackURL", "https://eastlaw.pk/Member/Order-Confirmation"));
                values.Add(new KeyValuePair<string, string>("orderRefNum", Session["OrderRefNum"].ToString()));
                values.Add(new KeyValuePair<string, string>("expiryDate", DateTime.Now.AddDays(30).ToString("yyyyMMdd hhmmss")));
                values.Add(new KeyValuePair<string, string>("merchantHashedReq", "as;dlkjfaslk==asdfasdfasdf"));
                values.Add(new KeyValuePair<string, string>("autoRedirect", "0"));
                //  values.Add(new KeyValuePair<string, string>(" paymentMethod", " OTC_PAYMENT_METHOD"));
                var content = new FormUrlEncodedContent(values);
                //  var response = client.PostAsync("http://202.69.8.50:9080/easypay/Index.jsf", content);
                //  var response = await client.PostAsync("http://202.69.8.50:9080/easypay/Index.jsf", content);
                var response = await client.PostAsync("https://easypay.easypaisa.com.pk/easypay/Index.jsf", content);
                var responseString = await response.Content.ReadAsStringAsync();


                string url = response.RequestMessage.RequestUri.Query;

                //Response.Write("URL Val: " + values[0].ToString() + "<br>");
                //Response.Write("URL Val: " + values[1].ToString() + "<br>");
                //Response.Write("URL Val: " + values[2].ToString() + "<br>");
                //Response.Write("URL Val: " + values[3].ToString() + "<br>");
                //Response.Write("URL Val: " + values[4].ToString() + "<br>");
                //Response.Write("URL Val: " + values[5].ToString() + "<br>");
                //Response.Write("URL Val: " + values[6].ToString() + "<br>");
                //Response.Write("URL Val: " + response.RequestMessage.ToString() + "<br>");


                string[] parts = url.Split(new char[] { '?', '&' });

                easypaisa obje = new easypaisa();
                obje.Aut_Code = parts[1].ToString().Replace("auth_token=", "");

                Session["autcode"] = parts[1].ToString().Replace("auth_token=", "");


                var formPostText = @"<html><body><div>
                                    <form method=""POST"" action=""https://easypay.easypaisa.com.pk/easypay/Confirm.jsf"" name=""frm2Post"">
                                    <input type=""hidden"" name=""auth_token"" value=""" + parts[1].ToString().Replace("auth_token=", "") + @""" /> 
                                    <input type=""hidden"" name=""postBackURL"" value=""https://eastlaw.pk//Member/Order-Confirmation"" /> 
                                    </form></div><script type=""text/javascript"">document.frm2Post.submit();</script></body></html>
                                    ";
                Response.Write(formPostText);

                //                var formPostText = @"<html><body><div>
                //                <form method=""POST"" action=""https://easypay.easypaisa.com.pk/easypay/Confirm.jsf"" name=""frm2Post"">
                //                <input type=""hidden"" name=""auth_token"" value=""" + parts[1].ToString().Replace("auth_token=", "") + @""" /> 
                //                <input type=""hidden"" name=""postBackURL"" value=""https://eastlaw.pk/Member/Order-Confirmation"" /> 
                //                </form></div><script type=""text/javascript"">document.frm2Post.submit();</script></body></html>
                //                ";
                //Response.Write(formPostText);

            }
        }

        #region JazzCash
        async void JazzCashAccount(Page page,string trxType)
        {
            //Test Details
            //string MerchantID = "MC18189";
            //string Password = "8gw48v3w2y";
            //string TxnDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string TxnExpiryDateTime = DateTime.Now.AddDays(7).ToString("yyyyMMddHHmmss");
            //string TxnReNo = "TXN" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //string Version = "1.1";
            //double Amount = double.Parse(Session["OrderAmt"].ToString().Replace("Rs. ", "") + "00");// double.Parse(Session["OrderAmt"].ToString());
            //string url = "https://sandbox.jazzcash.com.pk/CustomerPortal/transactionmanagement/merchantform";
            //string TxnCurrency = "PKR";
            //string IntegeratedSalt = "1t2c35hy2w";
            //string TxnType = trxType;
            //string ReturnUrl = "http://localhost:4253/member/order-confirmation.com";
            ////string ReturnUrl = "https://eastlaw.pk/member/order-confirmation";


            ////Live Details
            string MerchantID = "00148113";
            string Password = "w6vu5u63z0";
            string TxnDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string TxnExpiryDateTime = DateTime.Now.AddDays(7).ToString("yyyyMMddHHmmss");
            string TxnReNo = "TXN" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string Version = "1.1";
            double Amount = double.Parse(Session["OrderAmt"].ToString().Replace("Rs. ", "") + "00");
            string url = "https://payments.jazzcash.com.pk/CustomerPortal/transactionmanagement/merchantform";
            string TxnCurrency = "PKR";
            string IntegeratedSalt = "w0aa6yy9vw";
            string TxnType = trxType;
            //string ReturnUrl = "localhost:7837/Payments.aspx";
            string ReturnUrl = "https://eastlaw.pk/member/order-confirmation";

            NameValueCollection data = new NameValueCollection();
            data.Add("pp_Version", Version);
            data.Add("pp_TxnType", TxnType);
            data.Add("pp_Language", "EN");
            data.Add("pp_MerchantID", MerchantID);
            data.Add("pp_SubMerchantID", "");
            data.Add("pp_Password", Password);
            data.Add("pp_BankID", "");
            data.Add("pp_ProductID", "");
            data.Add("pp_TxnRefNo", TxnReNo);
            data.Add("pp_Amount", Amount.ToString());
            data.Add("pp_TxnCurrency", TxnCurrency);
            data.Add("pp_TxnDateTime", TxnDateTime);
            data.Add("pp_BillReference", Session["OrderRefNum"].ToString());
            data.Add("pp_Description", lblPlanName.Text.Replace("-",""));
            data.Add("pp_TxnExpiryDateTime", TxnExpiryDateTime);
            data.Add("pp_ReturnURL", ReturnUrl);
            data.Add("ppmpf_1", "1");
            data.Add("ppmpf_2", "2");
            data.Add("ppmpf_3", "3");
            data.Add("ppmpf_4", "4");
            data.Add("ppmpf_5", "5");

            //Amount             Bill ref       Description        Language        MerchantID        Password        Return URL       Currency    TxnDateTime        TxnRefNo        Version
            string secureHash = IntegeratedSalt
                + "&" + data.Get("pp_Amount")
                + "&" + data.Get("pp_BillReference")
                + "&" + data.Get("pp_Description")
                + "&" + data.Get("pp_Language")
                + "&" + data.Get("pp_MerchantID")
                + "&" + data.Get("pp_Password")
                + "&" + data.Get("pp_ReturnURL")
                + "&" + data.Get("pp_TxnCurrency")
                + "&" + data.Get("pp_TxnDateTime")
                + "&" + data.Get("pp_TxnExpiryDateTime")
                + "&" + data.Get("pp_TxnRefNo")
                + "&" + data.Get("pp_TxnType")
                + "&" + data.Get("pp_Version")
                + "&" + data.Get("ppmpf_1")
                + "&" + data.Get("ppmpf_2")
                + "&" + data.Get("ppmpf_3")
                + "&" + data.Get("ppmpf_4")
                + "&" + data.Get("ppmpf_5");

            data.Add("pp_SecureHash", HashMach(secureHash, IntegeratedSalt));

            //Prepare the Posting form
            string strForm = PreparePOSTForm(url, data);

            //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new LiteralControl(strForm));
        }
        // Form Creation method for submission
        private static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "onlineform";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");

            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }

            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
            //return strForm.ToString();
        }
        // Hash Generated Method
        private static string HashMach(String data, String key)
        {
            //String data = " Your Concatenated String ";
            //String key = "Your Hashkey";
            var secretBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA256(secretBytes))
            {
                byte[] iso88591data = Encoding.GetEncoding("ISO-8859-1").GetBytes(data);
                var hash = hmac.ComputeHash(iso88591data);
                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    hex.AppendFormat("{0:x2}", b);

                return hex.ToString();
            }
        }
        #endregion
    }
}