<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseDetailsPublic.aspx.cs" Inherits="EastlawUI_v2.CaseDetailsPublic" 
    MasterPageFile="~/Withoutlogin.Master"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <style type="text/css">

#sticknotes ul,li{
  list-style:none;
}
#sticknotes ul{
  overflow:hidden;
  padding:3em;
}
#sticknotes ul li a{
  text-decoration:none;
  color:#000;
  background:#ffc;
  display:block;
  height:80px;
  width:80px;
  padding:1em;
  -moz-box-shadow:5px 5px 7px rgba(33,33,33,1);
  -webkit-box-shadow: 5px 5px 7px rgba(33,33,33,.7);
  box-shadow: 5px 5px 7px rgba(33,33,33,.7);
  -moz-transition:-moz-transform .15s linear;
  -o-transition:-o-transform .15s linear;
  -webkit-transition:-webkit-transform .15s linear;
}
#sticknotes ul li{
  margin:1em;
  float:left;
}
#sticknotes ul li h2{
  font-size:140%;
  font-weight:bold;
  padding-bottom:10px;
}
#sticknotes ul li p{
  font-family:"Reenie Beanie",arial,sans-serif;
  font-size:12pt;
}
#sticknotes ul li a{
  -webkit-transform: rotate(-6deg);
  -o-transform: rotate(-6deg);
  -moz-transform:rotate(-6deg);
}
#sticknotes ul li:nth-child(even) a{
  -o-transform:rotate(4deg);
  -webkit-transform:rotate(4deg);
  -moz-transform:rotate(4deg);
  position:relative;
  top:5px;
  background:#cfc;
}
#sticknotes ul li:nth-child(3n) a{
  -o-transform:rotate(-3deg);
  -webkit-transform:rotate(-3deg);
  -moz-transform:rotate(-3deg);
  position:relative;
  top:-5px;
  background:#ccf;
}
#sticknotes ul li:nth-child(5n) a{
  -o-transform:rotate(5deg);
  -webkit-transform:rotate(5deg);
  -moz-transform:rotate(5deg);
  position:relative;
  top:-10px;
}
#sticknotes ul li a:hover,ul li a:focus{
  box-shadow:10px 10px 7px rgba(0,0,0,.7);
  -moz-box-shadow:10px 10px 7px rgba(0,0,0,.7);
  -webkit-box-shadow: 10px 10px 7px rgba(0,0,0,.7);
  -webkit-transform: scale(1.25);
  -moz-transform: scale(1.25);
  -o-transform: scale(1.25);
  position:relative;
  z-index:5;
}
 ol{text-align:center;}
 ol li{display:inline;padding-right:1em;}
 ol li a{color:#fff;}


</style>
    <style>
        /* Tooltip container */
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black; /* If you want dots under the hoverable text */
            font-size:14px;
            opacity:1;
             padding: 5px 7px;
    box-shadow: 0 0 5px #000;
    z-index:99;
        }

            /* Tooltip text */
            .tooltip .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: black;
                color: #fff;
                text-align: center;
                padding: 5px 0;
                border-radius: 6px;
                /* Position the tooltip text - see examples below! */
                position: absolute;
                z-index: 1;
            }

            /* Show the tooltip text when you mouse over the tooltip container */
            .tooltip:hover .tooltiptext {
                visibility: visible;
            }
    </style>
  
<%-- <asp:UpdatePanel ID="upnl" runat="server" >
     <ContentTemplate>--%>


  <% 
      try
      {

          System.Data.DataTable dt = new System.Data.DataTable();
          EastLawBL.Cases objcases = new EastLawBL.Cases();
          int chkAllow = objcases.IsCasePublic(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
          if (chkAllow == 1)
          {
              dt = objcases.GetCases(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
              string citation = dt.Rows[0]["Citation"].ToString();

              if (!string.IsNullOrEmpty(dt.Rows[0]["CaseSummary"].ToString()))
              {
                  Page.MetaDescription = EastlawUI_v2.CommonClass.GetWords(dt.Rows[0]["CaseSummary"].ToString(), 100);
              }
              else
              {
                  Page.MetaDescription = EastlawUI_v2.CommonClass.GetWords(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", "")), 100);
              }
                                    %>
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#">Search Result</a></li>
    <li  class="current" ><%  Response.Write(citation.ToString()); %></li>
      
</ul>
  </div>
</div>

    <div class="container">
    <div class="row margin_top">
    
    <!-------------- Left Side --------------->
    
    <div class="col-lg-4 col-md-12">
        
        
        <div class="panel panel-default style fixed">
  <div class="panel-heading panel-heading2">Documents on EastLaw<i class="fa fa-file-text-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">

  	<ul>
    <% 
              Response.Write(LinkedCasesLst());  
                              //try
                              //{
                              //    if (Session["MemberID"] != null)
                              //    {

                              //System.Data.DataTable dtLinkedCases = new System.Data.DataTable();
                              EastLawBL.Cases objcase1 = new EastLawBL.Cases();
                              //dtLinkedCases = objcase1.GetLinkedCasesWithDetails(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                              //if (dtLinkedCases != null)
                              //{
                              //    if (dtLinkedCases.Rows.Count > 0)
                              //    {

                              //        for (int a = 0; a < dtLinkedCases.Rows.Count; a++)
                              //        {


                              //            string link = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtLinkedCases.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtLinkedCases.Rows[a]["ID"].ToString());
                              //            Response.Write("<li> <a href='" + link + "' target='_blank'>" + dtLinkedCases.Rows[a]["Citation"].ToString() + "</a></li>");


                              //        }

                              //    }
                              //}

                              System.Data.DataTable dtLawLinkedCases = new System.Data.DataTable();
                              dtLawLinkedCases = objcase1.GetLinkedStatutes(int.Parse(HttpContext.Current.Items["caseid"].ToString()));
                              if (dtLawLinkedCases != null)
                              {
                                  if (dtLawLinkedCases.Rows.Count > 0)
                                  {
                                      dtLawLinkedCases.Columns.Add("link");
                                      for (int a = 0; a < dtLawLinkedCases.Rows.Count; a++)
                                      {
                                          dtLawLinkedCases.Rows[a]["link"] = "<a href='/statutes/" + dtLawLinkedCases.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtLawLinkedCases.Rows[a]["ID"].ToString()) + "' target='_blank'>" + EastlawUI_v2.CommonClass.MakeFirstCap(dtLawLinkedCases.Rows[a]["Title"].ToString()) + "</a>";
                                      }


                                      for (int a = 0; a < dtLawLinkedCases.Rows.Count; a++)
                                      {

                                          Response.Write("<li>" + dtLawLinkedCases.Rows[a]["link"].ToString() + "</li>");

                                      }

                                  }
                              }
                               
                           
                            %>
  
    
    
    </ul>
  </div>
            
</div>
       
        
        </div>
    <div style="position:fixed;padding-top:200px">

    
                </div>
    
    <!-------------- Left Side End --------------->
    
 
    
    <!-------------- right Side --------------->
    
    <div class="col-lg-8 ccol-md-8">
    
    	<div class="row">
    	<%
                                
                              if (dt.Rows.Count > 0)
                              {
                                  Response.Write("<div class='row text-center details'><p><strong>" + dt.Rows[0]["Court"].ToString() + "</strong></p><p><strong>" + dt.Rows[0]["JudgeName"].ToString() + "</strong></p></div>");
                                  Response.Write("<div class='row text-center details footer_inn'><p><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + "</strong></p><p style='color:#DA2128;'><strong>VS</strong></p><p><strong>" + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()) + "</strong></p></div>");
                                  Response.Write("<div class='row text-center details footer_inn'><p><strong>" + dt.Rows[0]["Appeal"].ToString() + " <br />" + dt.Rows[0]["JDate"].ToString() + "<br /><span style='color:#DA2128;'>Reported As</span> [" + dt.Rows[0]["Citation"].ToString() + "] " + GetAlternateCitations(int.Parse(HttpContext.Current.Items["caseid"].ToString())) + "<br />Result: " + dt.Rows[0]["Result"].ToString() + "<br /></strong></p></div>");
                                  Response.Write("<div><br /><span style='color:#DA2128;'>Practice Area:</span> " + dt.Rows[0]["CasePracticeArea"].ToString() + "<br />Tagged Statutes: " + dt.Rows[0]["CaseTaggedStatutes"].ToString() + "<br /></strong></p></div>");
                                  Response.Write("<div class='row margin-top-25'>");



                                  if (!string.IsNullOrEmpty(dt.Rows[0]["CaseSummary"].ToString()))
                                  {
                                      Response.Write(" <div class='row'>");
                                      Response.Write("<button type='button' class='btn_show' data-toggle='collapse' data-target='#demo1' >Show Excerpt</button>");
                                      Response.Write("<div id='demo1' class='collapse' style='background-color: #f1e9e9;'>");
                                      Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString()).ToString().Replace("</p>", "</p><br><br>----") + "</div></div>");
                                      //Response.Write(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString().ToString()) + "</div></div>");
                                  }
                                  //if (Session["MemberID"].ToString() == "3" || Session["MemberID"].ToString() == "1959" || Session["MemberID"].ToString() == "26" || Session["MemberID"].ToString() == "2365" || Session["MemberID"].ToString() == "2369" || Session["MemberID"].ToString() == "2378" || Session["MemberID"].ToString() == "2729" || Session["MemberID"].ToString() == "2627" || Session["MemberID"].ToString() == "2838")
                                  //{
                                  //    Response.Write("<div class='row text-justify order'>"+EastlawUI_v2.CommonClass.AutoCloseHtmlTags(dt.Rows[0]["CaseSummary"].ToString()) + "<br><br>"+"</div>");

                                  //}


                                  //   Response.Write("<p class='text-center'><strong>Order,</strong></p>");
                                  //Response.Write("<div class='row text-justify order'>"+ HighlightText(HighlightTextWithin(LinkedCases(LinkedStatutes(GetAnnotation(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))))))+"</div>");
                                  // Response.Write("<div class='row text-justify order'>" + FormatContent(GetAnnotation(HighlightText(HighlightTextWithin(LinkedCases(LinkedStatutes(EastlawUI_v2.CommonClass.AutoCloseHtmlTags(FormatContent(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))))))) + "</div>");
                                  Response.Write("<div class='row text-justify order'>" + FormatContent(GetAnnotation(LinkedCases(LinkedStatutes(dt.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", ""))))) + "</div>");
                                  Response.Write("</div>");
                              }

          }
          else
          {
              Response.Redirect("/");
          }
      }
      catch { }
            %>
        	
        
    	</div>
    
    </div>
    
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
  
      
            
 
                


  

        


</asp:Content>


