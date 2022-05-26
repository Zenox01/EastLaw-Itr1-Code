<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitationDetails.aspx.cs" Inherits="EastlawUI_v2.m.CitationDetails"
    MasterPageFile="~/m/MemberMaster.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="contentPage">
        <%try
          {
              if (Session["MemberID"] != null)
              {
                  System.Data.DataTable dt = new System.Data.DataTable();
                  EastLawBL.Cases objcases = new EastLawBL.Cases();

                  dt = objcases.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                   %>
	<h1>
    <div class="container">
        <div class="margin">
            <%
                  if (dt.Rows.Count > 0)
                  {
                      Response.Write(EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()));
                  }
                 %>
            About EastLaw.pk
        </div>
    </div>
    </h1>

	<div class="container">
    	<div class="margin">
        	<p>
            	<%
                  if (dt.Rows.Count > 0)
                  {
                      Response.Write("<center><strong> " + dt.Rows[0]["Court"].ToString() + "<br/>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></center><br>");
                      Response.Write("<table border='0' style='width:100%;text-align:center'>"
                         + "<tr>"
                         + "<td style='width:45%;text-align:right'><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></td>"
                         + "<td style='width: 10%;color:red'>VS </td>"
                         + "<td style='width:45%;text-align:left'><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + " </strong></td></tr></table><br/><br/>");
                      Response.Write("<h6>" + dt.Rows[0]["Appeal"].ToString() + "</span><br/>" + dt.Rows[0]["JDate"].ToString() + "<br/><br/>");
                      if (string.IsNullOrEmpty(dt.Rows[0]["CitationRef"].ToString()))
                          Response.Write("<font style='color:red'>Reported As</font> [" + dt.Rows[0]["Citation"].ToString() + "]<br/><br/>");
                      else
                          Response.Write("<font style='color:red'>Reported As</font> [" + dt.Rows[0]["CitationRef"].ToString() + "]<br/><br/>");

                      Response.Write("Keywords: " + dt.Rows[0]["Keywords"].ToString() + "<br><br>Result: " + dt.Rows[0]["Result"].ToString() + "</h6>");

                      //Remove headnotes allow only for specific Users
                      if (Session["MemberID"] != null)
                      {
                          if (Session["MemberID"].ToString() == "12" || Session["MemberID"].ToString() == "8")
                          {
                              Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["HeadNotes"].ToString()) + "<br><br>");
                          }
                          if (Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "1959")
                          {
                              Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString()) + "<br><br>");
                          }
                      }

                      Response.Write(HighlightText(HighlightTextWithin(LinkedCases(LinkedStatutes(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", ""))))))));
                      
                  }
                     %>
            </p>
        </div>
    </div>
        <%}
          }
          catch { } %>
</div>
</asp:Content>


