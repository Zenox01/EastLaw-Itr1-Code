<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentDocumentView.aspx.cs" Inherits="EastlawUI_v2.DepartmentDocumentView"
    MasterPageFile="~/MemberMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cntPlaceHolder">
  
    <div class="container">
<div class="row breadcrum">
    <ul class="bc">
        <%if (Session["MemberID"] != null)
          { 
          %>
        <li  class="first"><a href="/member/member-dashboard"> Home</a></li>
        <% } else { %>
        <li><a href="/"> Home</a></li>
        <%} %>
                   
                       <li ><a href="/departments/departments-home"> Departments</a></li>
        
                    <li> <a id="lblCurCrumb1Link" runat="server"><asp:Label ID="lblCurCrumb1" runat="server"></asp:Label></a></li>
        <%--<li> <a id="lblCurCrumb2Link" runat="server"><asp:Label ID="lblCurCrumb2" runat="server"></asp:Label></a></li>--%>
        <li><a id="lblCurCrumb3Link" runat="server"><asp:Label ID="lblCurCrumb3" runat="server"></asp:Label></a></li>
        <li class="current"><asp:Label ID="lblCurCrumb4" runat="server"></asp:Label></li>
        
                </ul>
<%--<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#">Search Result</a></li>
    <li><a href="#" class="current">Details</a></li>
</ul>--%>
  </div>
</div>

    <div class="container">
    <div class="row margin_top">
    
    <!-------------- Left Side --------------->
    
    <div class="col-lg-4 col-md-4">
        
        
        <div class="panel panel-default style">
  <div class="panel-heading panel-heading2">Detail<i class="fa fa-file-text-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
      <%
                  System.Data.DataTable dt = new System.Data.DataTable();
                  EastLawBL.Departments objDept = new EastLawBL.Departments();
                  dt = objDept.GetDepartmentFileDetailsByID(int.Parse(HttpContext.Current.Items["dptdocid"].ToString()));
                  if (dt != null)
                  {
                      if(dt.Rows.Count > 0)
                      {
                          
                          Response.Write("<ul>");
                          Response.Write("<li><h4>Title:</h4> " + dt.Rows[0]["Title"].ToString() + "</li>");
                          Response.Write("<li><h4>Date:</h4> " + dt.Rows[0]["DDate"].ToString() + "</li>");
                          Response.Write("<li><h4>Type:</h4> " + dt.Rows[0]["DType"].ToString() + "</li>");
                          Response.Write("<li><h4>Number:</h4> " + dt.Rows[0]["No"].ToString() + "</li>");
                          Response.Write("</ul>");
                          
                      }
                      
                       
                  }
                   %>

  
     
  </div>
            
</div>
        
        
        </div>
 
    
    <!-------------- Left Side End --------------->
    
 
    
    <!-------------- right Side --------------->
    
    <div class="col-lg-8 ccol-md-8">
    
    	<div class="row">
      <%
              if (dt.Rows[0]["FileType"].ToString() == "Word")
              {
                  if (HttpContext.Current.Items["dptFileContent"] != null)
                  {


                      //Response.Write(EastLawUI.CommonClass.CleanHtml(HttpContext.Current.Items["dptFileContent"].ToString()));
                      Response.Write(HttpContext.Current.Items["dptFileContent"].ToString());
                      Response.Write("<span style='float:right'><a href='/departments/departments-home'>Go to Back Page</a></span>");
                      //Spire.Doc.Document document = new Spire.Doc.Document();

                      //string path = Server.MapPath("/store/departments/wordfiles/" + HttpContext.Current.Items["dptWordFile"].ToString());
                      //document.LoadFromFile(path);

                      ////Save doc file to html

                      //document.SaveToFile(Server.MapPath("/store/departments/depttemp/" + HttpContext.Current.Items["dptWordFile"].ToString() + ".html"), Spire.Doc.FileFormat.Html);

                      //string html =System.IO.File.ReadAllText(Server.MapPath("/store/departments/depttemp/" + HttpContext.Current.Items["dptWordFile"].ToString() + ".html"));

                      //Response.Write(EastLawUI.CommonClass.CleanHtml(html));
                  }
              }
              else if (dt.Rows[0]["FileType"].ToString() == "PDF")
              {
                  Response.Write("<object data='/store/departments/wordfiles/" + dt.Rows[0]["WordFile"].ToString() + "' type='application/pdf' width='100%' height='700'><p>Alternative text - include a link <a href='/store/departments/wordfiles/" + dt.Rows[0]["WordFile"].ToString() + "'>to the PDF!</a></p></object>"); 
              }
                   
                     %>
                
        
    	</div>
    
    </div>
    
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
    <div class="buttons">
        
         <a href="#" class="btn_bgcolor4"  data-toggle="modal" data-target="#myModal2" title="Report Error" data-placement="left">
                
                	<i class="fa fa-exclamation-triangle"></i>
                	
                    
                </a>
        		<%--<a href="#" id="tooltip" class="btn_bgcolor" title="Save into Folder">
                
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
                	
                    
                </a>--%>
        
        
        </div>
    <div id="myModal2" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Report Error</h4>
      </div>
      <div class="modal-body">
           <asp:UpdatePanel ID="uppPnlReportError" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
         <table style="width:100%">
            <tr id="trErrorReportFields" runat="server">
               <%-- <td>Comment: </td>--%>
                <td><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="300" Height="100" placeholder="Enter your comments"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment" ErrorMessage="Please enter your comments"  ForeColor="Red" Display="Dynamic"
            ValidationGroup="AA"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button  OnClick="btnFill_Click" ID="btnFill" runat="server" Text="Report Error" ValidationGroup="AA"  CssClass="btn btn-danger btn_style"  />
                    <asp:Label id="lblErrorMsg" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr id="trErrorReport" runat="server" style="display:none">
               
                <td >
                    Thanks for your valuable comments, we will definatily review your comment.
                    </td>
                    
            </tr>
        </table>
           </ContentTemplate>
          </asp:UpdatePanel>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>
</asp:Content>