using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel.newsletter
{
    public partial class NewsletterList : System.Web.UI.Page
    {
        EastLawBL.Newsletter objn = new EastLawBL.Newsletter();
        NewsletterHTMLGenerator objNG = new NewsletterHTMLGenerator();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetNewsletter(0);
        }
        void GetNewsletter(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objn.GetNewsletter(ID);
                dt.Columns.Add("strActive");
                if (dt.Rows.Count > 0)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["Active"].ToString() == "1")
                            dt.Rows[a]["strActive"] = "Yes";
                        else
                            dt.Rows[a]["strActive"] = "No";
                    }
                    dt.AcceptChanges();

                    gvLst.DataSource = dt;
                    gvLst.DataBind();
                }
            }
            catch { }
        }

        protected void gvLst_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvLst.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);
                    Response.Redirect("AddNewsletter.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));

                }
            }
            catch
            {
            }
        }

        protected void gvLst_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvLst.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);
                Label lblNewsletterTitle = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    lblNewsletterTitle = (Label)row.FindControl("lblNewsletterTitle");
                    int ID = Convert.ToInt32(hd.Value);
                    SendEmail(ID, lblNewsletterTitle.Text);
                }



            }
            catch
            { }
        }
        void SendEmail(int NewsletterListID, string Subjecta)
        {
            NewsletterHTMLGenerator objNG = new NewsletterHTMLGenerator();
            string Content = "";
            Content = objNG.GenerateNewsletter(1, NewsletterListID);

            // }
            //return Content;


            DataTable dtEmails = new DataTable();

            int EChk = 0;
            //Email.SendMail("umar.mughal83@gmail.com", Content + Footer("umar.mughal83@gmail.com"), Subjecta, "eastlaw.pk", "");
            Email.SendMail("muhammad.abubakar@live.com", Content + Footer("a4mgroup@yahoo.com"), Subjecta, "eastlaw.pk", "");

            //    }
            //}

            //return "";

        }
        string Footer(string EmailID)
        {
            string Content = "<table width='800' border='0' cellspacing='1' cellpadding='0'>"
                  + "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333; text-align:center; background-color:#FFF'>You can <a href='http://fabbyshop.com/unsubscribe.aspx?email=" + EmailID.ToString() + "'> unsubscribe </a> this newsletter at any time .<br />"
                 + "We respect your personal information and we will not share them with any other parties.<br />For further details , please review our terms and conditions.</td></tr></table>";
            return Content;
        }




        protected void gvLst_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvLst.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);
                Label lblNewsletterTitle = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    lblNewsletterTitle = (Label)row.FindControl("lblNewsletterTitle");
                    int ID = Convert.ToInt32(hd.Value);
                    // SendEmail(ID, lblNewsletterTitle.Text);
                    var res = objNG.GenerateNewsletter(1, ID);
                    PDFGenerator.GeneratePDF(res, "NewsLetter");
                }



            }
            catch
            { }
        }
    }
}