using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EastlawUI_v2.adminpanel.newsletter
{
    public partial class viewnewsletter : System.Web.UI.Page
    {
        NewsletterHTMLGenerator objNG = new NewsletterHTMLGenerator();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["id"] != null))
                Response.Write(objNG.GenerateNewsletter(2, int.Parse(Request.QueryString["id"].ToString())));
        }
    }
}