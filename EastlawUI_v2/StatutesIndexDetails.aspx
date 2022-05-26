<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesIndexDetails.aspx.cs" Inherits="EastlawUI_v2.StatutesIndexDetails" 
    MasterPageFile="~/MemberMaster.Master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cntPlaceHolder">
  
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#">Search Result</a></li>
    <li><a href="#" class="current">Details</a></li>
</ul>
  </div>
</div>

    <div class="container">
    <div class="row margin_top">
    
    <!-------------- Left Side --------------->
    
    <div class="col-lg-4 col-md-4">
        
        
        <div class="panel panel-default style">
  <div class="panel-heading panel-heading2">Documents on EastLaw<i class="fa fa-file-text-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">

  	<ul>
      <li> <asp:LinkButton ID="lnkPDF" runat="server" Text="Download PDF" OnClick="lnkPDF_Click" Visible="false"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkWord" runat="server" Text="Download Word" OnClick="lnkWord_Click" Visible="false"></asp:LinkButton></li>
                    
                <%
                    if (string.IsNullOrEmpty(HttpContext.Current.Items["statutespdffilename"].ToString()))
                    {
                        lnkPDF.Visible = false;
                    }
                       // Response.Write("<li><a href='/store/statutesdocs/pdf/" + HttpContext.Current.Items["statutespdffilename"].ToString() + "' target='_blank'>PDF</a></li>");
                    if (string.IsNullOrEmpty(HttpContext.Current.Items["statuteswordfilename"].ToString()))
                    {
                        lnkWord.Visible = false;
                    }
                    //    Response.Write("<li><a href='/store/statutesdocs/word/" + HttpContext.Current.Items["statuteswordfilename"].ToString() + "' target='_blank'>Word</a></li>"); 
                        %>
                                
                                
                                
                               
                           
                           
    
    
    
    </ul>
     
  </div>
            
</div>
        
        
        </div>
 
    
    <!-------------- Left Side End --------------->
    
 
    
    <!-------------- right Side --------------->
    
    <div class="col-lg-8 ccol-md-8">
    
    	<div class="row">
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
                                 Response.Write("<span style='float:right'><a href='/Statutes/" + clsUtilities.RemoveSpecialCharacter(dt.Rows[0]["Title"].ToString()).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[0]["StatutesID"].ToString()) + "'>Go to Index Page</a></span>");
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
                             //if(dt != null)
                             //{
                             //    if (dt.Rows.Count > 0)
                             //    {
                             //        Response.Write("<center><table style='width:90%' border='0' id='up'>");
                             //        for (int a = 0; a < dt.Rows.Count; a++)
                             //        {
                             //            //if (a == 0)
                             //            //{
                             //            //    // Response.Write(dt.Rows[a]["IndexTitle"].ToString() + "<br>");
                             //            //    //Response.Write("<tr><td style='text-align:center'>" + dt.Rows[a]["IndexTitle"].ToString() + "</td>");
                             //            //    Response.Write("<td style='text-align:left'><a href='#"+a+"'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                             //            //}
                             //            //else
                             //            //{
                             //                // Response.Write("<tr><td style='text-align:center'>" + a + "</td>");
                             //                Response.Write("<td style='text-align:left'><a href='#" + a + "'> " + dt.Rows[a]["IndexTitle"].ToString() + "</a></td></tr>");
                             //           // }

                             //        }

                             //        Response.Write("</table></center>");


                             //        for (int b = 0; b < dt.Rows.Count; b++)
                             //        {
                             //            Response.Write("<div id='" + b + "'><h3>" + dt.Rows[b]["IndexTitle"].ToString() + "</h3><p>" + FormatContent(dt.Rows[b]["IndexContent"].ToString() + "</p><br></div>") + "<a href='#up'>Up</a>");
                                         

                             //        }
                             //    }
                             //}
                         }
                     }
                     catch { }
    %>
        	
        
    	</div>
    
    </div>
    
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
    <div class="buttons">
        
        		<a href="#" id="tooltip" class="btn_bgcolor" title="Save into Folder">
                
                	<i class="fa fa-folder-open-o"></i>
                	
                    
                </a>
                
                
                <a href="#" class="btn_bgcolor2"  data-toggle="tooltip" title="Create New Folder" data-placement="left">
                
                	<i class="fa fa-plus"></i>
                	
                    
                </a>
                
                <a href="#" class="btn_bgcolor3" data-toggle="tooltip" title="Add Comment" data-placement="left">
                
                	<i class="fa fa-comment-o"></i>
                	
                    
                </a>
                
                <a href="#" class="btn_bgcolor4" data-toggle="tooltip" title="Report Error" data-placement="left">
                
                	<i class="fa fa-exclamation-triangle"></i>
                	
                    
                </a>
                
                <a href="#" class="btn_bgcolor5" data-toggle="tooltip" title="Download Judgement" data-placement="left">
                
                	<i class="fa fa-download"></i>
                	
                    
                </a>
        
        
        </div>
</asp:Content>