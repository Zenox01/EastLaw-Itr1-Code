<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentDocumentView.aspx.cs" Inherits="EastlawUI_v2.m.DepartmentDocumentView"
    MasterPageFile="~/m/MemberMaster.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
      <%
            if (Session["MemberID"] != null)
          { 
                 System.Data.DataTable dt = new System.Data.DataTable();
                  EastLawBL.Departments objDept = new EastLawBL.Departments();
                  dt = objDept.GetDepartmentFileDetailsByID(int.Parse(HttpContext.Current.Items["dptdocid"].ToString()));
                 %>
    <div class="contentPage">
	<h1>
    <div class="container">
        <div class="margin">
            <%
              if (dt != null)
              {
                  if (dt.Rows.Count > 0)
                  {
                   
                      Response.Write(dt.Rows[0]["Title"].ToString());
                    
                  }
              }
               %>
        </div>
    </div>
    </h1>

	<div class="container">
    	<div class="margin" style="color:black">
        	

            	   <%
                    if (HttpContext.Current.Items["dptFileContent"] != null)
                    {
                        Response.Write(HttpContext.Current.Items["dptFileContent"].ToString());
                        Response.Write("<span style='float:right'><a href='/m/Departments/All-Departments'>Go to Back Page</a></span>");
                        
                    }
                   
                     %>
                
                
            
        </div>
    </div>
</div>
    <%} %>
    </form>
</asp:Content>

