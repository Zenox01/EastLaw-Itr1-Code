<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesIndexDetails.aspx.cs" Inherits="EastlawUI_v2.m.StatutesIndexDetails"
    MasterPageFile="~/m/MemberMaster.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="contentPage">
        <%try
          {
              if (Session["MemberID"] != null)
              {
                 
                   %>
           <ul>
                <asp:LinkButton ID="lnkPDF" runat="server" Text="Download PDF" OnClick="lnkPDF_Click" Visible="false"></asp:LinkButton><br /><br />
                <asp:LinkButton ID="lnkWord" runat="server" Text="Download Word" OnClick="lnkWord_Click" Visible="false"></asp:LinkButton>
                    
                <%
                    if (string.IsNullOrEmpty(HttpContext.Current.Items["statutespdffilename"].ToString()))
                    {
                        lnkPDF.Visible = false;
                    }
                    if (string.IsNullOrEmpty(HttpContext.Current.Items["statuteswordfilename"].ToString()))
                    {
                        lnkWord.Visible = false;
                    }
                    %>
                  
            	
               
            </ul>
	<%--<h1>
    <div class="container">
        <div class="margin">
            <%
                  if (dt.Rows.Count > 0)
                  {
                      Response.Write(EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[0]["Respondent"].ToString()));
                  }
                 %>
           
        </div>
    </div>
    </h1>--%>

	<div class="container">
    	<div class="margin">
        	<p>
            	<%
                  try
                  {
                      System.Data.DataTable dt = new System.Data.DataTable();
                      EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                      if (Request.QueryString["cont"] == null)
                      {
                          dt = objstate.GetStatutesIndex(int.Parse(HttpContext.Current.Items["statutesindexid"].ToString()));
                          if (dt.Rows.Count > 0)
                          {
                              Response.Write("<span style='float:right'><a href='/m/Statutes/" + clsUtilities.RemoveSpecialCharacter(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["StatutesID"].ToString()) + "'>Go to Index Page</a></span>");
                              Response.Write("<br><strong>INDEX: " + dt.Rows[0]["IndexTitle"].ToString() + "</strong><br>");
                              Response.Write(FormatContent(dt.Rows[0]["IndexContent"].ToString()));


                          }
                      }
                      else if (Request.QueryString["cont"] != null)
                      {
                          dt = objstate.GetStatutesIndexByStatutesID(int.Parse(HttpContext.Current.Items["statutesid"].ToString()));
                          if (!string.IsNullOrEmpty(dt.Rows[0]["PDFFileName"].ToString()))
                          {
                              //  Response.Write("<embed width='100%' height='700' src='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "#toolbar=1&navpanes=1&scrollbar=1'></embed>");
                              Response.Write("<object data='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                          }
                          else if (!string.IsNullOrEmpty(dt.Rows[0]["WordFileName"].ToString()))
                          {
                              //Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/statutesdocs/pdf/" + dt.Rows[0]["PDFFileName"].ToString() + "'>to the PDF!</a></p></object>");
                              Response.Write("<object data='/store/statutesdocs/word/" + dt.Rows[0]["WordFileName"].ToString() + "'>You do not have Word installed on your machine</object>");
                          }
                         
                      }
                  }
                  catch { }
                     %>
            </p>
        </div>
    </div>
        <%}
          }
          catch { } %>
</div>
</asp:Content>


