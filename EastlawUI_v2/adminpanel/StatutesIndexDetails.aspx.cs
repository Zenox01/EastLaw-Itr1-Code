using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace EastlawUI_v2.adminpanel
{
    public partial class StatutesIndexDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                Response.Redirect("NotAuthorize.aspx");
        }
        public string FormatContent(string InputTxt)
        {
            string content = InputTxt;

            content = content.Replace("Go to Index Page", "").Replace("eastlawdic.aspx", "/eastlawdic.aspx").ToString();
            content = Regex.Replace(content, "</?(a|A).*?>", string.Empty);
            //content = Regex.Replace(content, "</?(img|IMG).*?>", string.Empty);



            return content;


            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }
    }
}