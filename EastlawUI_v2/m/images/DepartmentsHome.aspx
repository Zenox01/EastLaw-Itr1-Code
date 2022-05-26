<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsHome.aspx.cs" Inherits="EastlawUI_v2.DepartmentsHome" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <link href="css/department.css" rel="stylesheet" type="text/css" />
       <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
    <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">Department</a></li>
</ul>
  </div>
</div>

    <div class="container">
    <div class="row">
    
   		<div class="col-lg-8 col-md-8">
        
        	
            <div class="heading_style">
            
            	<h3>Search from <asp:Label ID="lblDepTitle" runat="server"></asp:Label> </h3>
            
            </div>
        
        	<div class="box">
            
            	<div class="inner">
            	<asp:DropDownList ID="ddlDeptTypeGroups" runat="server"   class="form-control" style="    margin-left: 6px;" ValidationGroup="GroupSearch"></asp:DropDownList> 
                
                     <asp:DropDownList ID="ddlYear" runat="server"   class="form-control" style="margin:0 6px;"></asp:DropDownList> 
                   
                    <asp:TextBox ID="txtTypesNo" runat="server" ToolTip="No" class="form-control" placeholder="Enter No:"></asp:TextBox>
                 
                
            	</div>
                 <asp:Button ID="btnTypeSearch" runat="server" Text="Search" class="btn btn-default btn_2" OnClick="btnTypeSearch_Click"  ValidationGroup="GroupSearch"  />
                <asp:RequiredFieldValidator ID="rfvDeptTypeGroup" runat="server" ControlToValidate="ddlDeptTypeGroups" InitialValue="0" ErrorMessage="Dept Type Required" Display="Dynamic"
                                    ValidationGroup="GroupSearch" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvGroupYear" runat="server" ControlToValidate="ddlYear" InitialValue="0" ErrorMessage="Year Required" Display="Dynamic"
                                    ValidationGroup="GroupSearch" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvGroupNo" runat="server" ControlToValidate="txtTypesNo" ErrorMessage="No. Required" Display="Dynamic"
                                    ValidationGroup="GroupSearch" ForeColor="Red">
                                </asp:RequiredFieldValidator>
         
                
            </div>
            
            <div class="clearfix"></div>
            <div class="box">
            <div class="col-lg-6 col-md-6" style="padding-left:0;">
        
        
        
        	<label>Search by Title*</label>
            <div>
                 <asp:TextBox ID="txtSearch" runat="server" ToolTip="Free Search" class="form-control text_field2" placeholder="Search by Title*" AutoPostBack="True" ValidationGroup="Search" OnTextChanged="txtSearch_TextChanged" ></asp:TextBox>
                        <%--<input class="form-control text_field2" placeholder="Search by Title*" name="srch-term" id="srch-term" type="text">--%>
                        <div class="input-group-btn">
                            <%--<button class="btn btn-default" style="height: 34px;background:#eee;" type="submit"><i class="fa fa-search"></i></button>--%>
                              <asp:Button ID="btnFreeTextSearch" runat="server" Text="Search" class="btn btn-default" style="height: 34px;background:#eee;" OnClick="btnFreeTextSearch_Click"  ValidationGroup="Search"/>
                        </div>
                    </div>
        
        
        </div>
        
        <div class="col-lg-6 col-md-6" style="padding-right:6px;">
        	<label>Search By Date</label>
            <div>
             <asp:TextBox ID="txtSearchByDate" runat="server" class="form-control text_field2" ToolTip="Free Search By Date" placeholder="Search By Date DD/MM/YYYY"  Width="200" AutoPostBack="false" ValidationGroup="SearchByDate" ></asp:TextBox>
             <asp1:calendarextender ID="clnStart" runat="server" TargetControlID="txtSearchByDate"
                                Format="dd/MM/yyyy">
                            </asp1:calendarextender>
            <asp:RequiredFieldValidator ID="rfvtxtSearchByDate" runat="server" ControlToValidate="txtSearchByDate" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SearchByDate" Display="Dynamic"></asp:RequiredFieldValidator>
<%--        	<div id="datepicker" class="input-group date" data-date-format="mm-dd-yyyy">

    <input class="form-control" type="text" style="background:#fff;" readonly="readonly" />
    <span class="input-group-addon">
    <i class="fa fa-search"></i></span>
			</div>--%>
        <div class="input-group-btn">
        <asp:Button ID="btnSearchByDate" runat="server" Text="Search" class="btn btn-default" OnClick="btnSearchByDate_Click"   ValidationGroup="SearchByDate"/>
        </div>
                </div>
        </div>
        
        </div>
        
        <div class="box">
        
        
        <div style="width:100%;">
            <asp:TextBox ID="txtFreeTextSearch" runat="server" class="form-control text_field" placeholder="Free Text Search*" ToolTip="Free Search"  AutoPostBack="false" ValidationGroup="FreeTextSearch" OnTextChanged="txtFreeTextSearch_TextChanged"  ></asp:TextBox>
                            
             <div class="input-group-btn">
                            <asp:Button ID="btnTextSearch" runat="server" Text="Search" class="btn btn-default" OnClick="btnTextSearch_Click"  ValidationGroup="FreeTextSearch" />
                 <asp:RequiredFieldValidator ID="rfvtxtFreeTextSearch" runat="server" ControlToValidate="txtFreeTextSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="FreeTextSearch" Display="Dynamic"></asp:RequiredFieldValidator>
                 </div>
             <asp:Label ID="lblMsg" runat="server" Visible ="false"></asp:Label>
                        <%--<input class="form-control text_field" placeholder="Free Text Search*" name="srch-term" id="srch-term" type="text">
                        <div class="input-group-btn">
                            <button class="btn btn-default" style="height: 34px;background:#eee;" type="submit"><i class="fa fa-search"></i></button>
                        </div>--%>
                    </div>
        
        </div>
        
        </div>
        
        
        
        <div class="col-lg-4 col-md-4">
        
        
        <div class="panel panel-default style" >
  <div class="panel-heading panel-heading2">Related Links<i class="fa fa-snowflake-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
  
  	<ul >
       <%
     if (Session["MemberID"] != null)
     {
          EastLawBL.Departments objdept = new EastLawBL.Departments();
         System.Data.DataTable dtlevel1 = new System.Data.DataTable();
         System.Data.DataTable dtlevel2 = new System.Data.DataTable();

       
         if (Request.QueryString["dptkp"] != null)
         {
             dtlevel1 = objdept.GetActiveDeptByParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())));
             if (dtlevel1 != null)
             {
                 Response.Write("<h4>" + EastlawUI_v2.CommonClass.MakeFirstCap(Request.QueryString["dptn"].ToString()) + "</h4>");
                 for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                 {

                     Response.Write("<li><a href='/departments/" + dtlevel1.Rows[l1]["DeptName"].ToString().Replace("/", "-").Replace("'", "") + "." + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "?lstN=" + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "'> " + dtlevel1.Rows[l1]["DeptName"].ToString() + "</a> ");
                         dtlevel2 = objdept.GetActiveDeptByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                         if (dtlevel2 != null)
                         {
                             Response.Write("<ul style='padding-left:50px'>");

                             for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                             {
                                 Response.Write("<li><a href='#'> " + dtlevel2.Rows[l2]["DeptName"].ToString() + "</a></li>");
                             }
                             Response.Write("</ul>");
                         }
                         
                         Response.Write("</li>");

                    
                     
                    // Response.Write("<li><a href='/departments/departments-home?dptn" + dtlevel1.Rows[l1]["DeptName"].ToString().ToLower().Replace(" ", "-") + "&dptkp=" + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "'> " + dtlevel1.Rows[l1]["DeptName"].ToString() + "</a></li>");
                 }
             }
         }
         else
         {
             dtlevel1 = objdept.GetActiveDeptByParent(0);
             if (dtlevel1 != null)
             {
                 for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                 {
                     Response.Write("<li><a href='/departments/departments-home?dptn=" + dtlevel1.Rows[l1]["DeptName"].ToString().ToLower().Replace(" ", "-").Replace("'", "") + "&dptkp=" + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "'> " + dtlevel1.Rows[l1]["DeptName"].ToString() + "</a>");
                     //dtlevel2 = objdept.GetActiveDeptByParent(int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                     //if (dtlevel2 != null)
                     //{
                     //    Response.Write("<ul style='padding-left:50px'>");

                     //    for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                     //    {
                     //        Response.Write("<li><a href='#'> " + dtlevel2.Rows[l2]["DeptName"].ToString() + "</a></li>");
                     //    }
                     //    Response.Write("</ul>");
                     //}
                         
                     Response.Write("</li>");
                 }
             }
         }

         
     }
     %>
    	<%--<li><a href="#">Act, Rules and Amendments</a></li>
        <li><a href="#">Notifications</a></li>
        <li><a href="#">Circulars</a></li>
        <li><a href="#">Publication</a></li>
        <li><a href="#">Services Categories</a></li>
        <li><a href="#">Archieve</a></li>
        <li><a href="#">Service Categories</a></li>
        <li><a href="#">Forms</a></li>--%>
    
    </ul>
  
  </div>
</div>
        
        
        </div>
    	
    </div>  
    </div>
    <div class="clearfix"></div>

    <div class="container">
        
        	<div class="row">
        
        
                <%
                                                            System.Data.DataTable dtUpdatePanel = new System.Data.DataTable();
                                                            EastLawBL.Departments objd = new EastLawBL.Departments();
                                                            if (Request.QueryString["dptkp"] != null)
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Notification");
                                                            else
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Notification");
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if (dtUpdatePanel.Rows.Count > 0)
                                                                {
                                                                %>
        		<div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Notification</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                          
                                                                    //if (Request.QueryString["dptkp"] != null)
                                                                    //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Notification");
                                                                    //else
                                                                    //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Notification");
                                                                    //if (dtUpdatePanel != null)
                                                                    //{
                                                                        for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                        {
                                                                            Response.Write("<li class='news-item'>"
                                                                               + "<table cellpadding='4' width=310px><tr>"
                                                                                + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                                + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["DDate"].ToString() + "<br />"
                                                                                + "<b>Department:</b> " + dtUpdatePanel.Rows[a]["ParentDeptName"].ToString() + "<br />"
                                                                                //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                                + " <br><span style='float:right'> <a href='/departments/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["DeptName"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                            if (a == 15)
                                                                                break;
                                                                        }
                                                                    //}
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer">
       <%
                                                                    if (Request.QueryString["dptkp"] != null)
                                                                        Response.Write("<a href='/departments/latest-notifications/?param=" + Request.QueryString["dptkp"].ToString() + "&trans=" + Request.QueryString["dptn"].ToString() + "'>View More</a>");
                                                                    else
                                                                        Response.Write("<a href='/departments/latest-notifications/'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
                     <%}
                                                            }%>
        
        
        
        
        <%
                                                            if (Request.QueryString["dptkp"] != null)
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Circular");
                                                            else
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Circular");
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if (dtUpdatePanel.Rows.Count > 0)
                                                                {
%>
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Circular</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                                    //if (Request.QueryString["dptkp"] != null)
                                                                    //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Circular");
                                                                    //else
                                                                    //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Circular");
                                                                    //if (dtUpdatePanel != null)
                                                                    //{
                                                                        for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                        {
                                                                            Response.Write("<li class='news-item'>"
                                                                               + "<table cellpadding='4' width=310px><tr>"
                                                                                + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                                + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["DDate"].ToString() + "<br />"
                                                                                + "<b>Department:</b> " + dtUpdatePanel.Rows[a]["ParentDeptName"].ToString() + "<br />"
                                                                                //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                                + "  <br><span style='float:right'>  <a href='/departments/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["DeptName"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                            if (a == 15)
                                                                                break;
                                                                        }
                                                                   // }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> 
    <%
                                                                    if (Request.QueryString["dptkp"] != null)
                                                                        Response.Write("<a href='/departments/latest-circular/?param=" + Request.QueryString["dptkp"].ToString() + "&trans=" + Request.QueryString["dptn"].ToString() + "'>View More</a>");
                                                                    else
                                                                        Response.Write("<a href='/departments/latest-circular/'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
            <%}
                                                            } %>
        
        
        
        
        
                <%
                                                            if (Request.QueryString["dptkp"] != null)
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Other");
                                                            else
                                                                dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Other");    
                                                            if (dtUpdatePanel != null)
                                                            {
                                                                if(dtUpdatePanel.Rows.Count > 0)
                                                                {
                                                                %>
        
        <div class="col-lg-4 col-md-4">
        
        	<div class="panel panel-default">
<div class="panel-heading"> <span class="glyphicon glyphicon-list-alt"></span><b>Other</b></div>
<div class="panel-body">
<div class="row">
<div class="col-xs-12" style="padding:0;">
<ul class="demo1">
<%
                                                            //if (Request.QueryString["dptkp"] != null)
                                                            //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["dptkp"].ToString())), "Other");
                                                            //else
                                                            //    dtUpdatePanel = objd.GetDepartmentFilesByGroupAndDeptParent(0, "Other");    
                                                            //if (dtUpdatePanel != null)
                                                            //{
                                                                for (int a = 0; a < dtUpdatePanel.Rows.Count; a++)
                                                                {
                                                                    Response.Write("<li class='news-item'>"
                                                                       + "<table cellpadding='4' width=310px><tr>"
                                                                        + "<td><b>" + dtUpdatePanel.Rows[a]["Title"].ToString().Replace("_", " ") + " ...</b><br />"
                                                                        + "<b>Date:</b> " + dtUpdatePanel.Rows[a]["DDate"].ToString() + "<br />"
                                                                        + "<b>Department:</b> " + dtUpdatePanel.Rows[a]["ParentDeptName"].ToString() + "<br />"
                                                                        //+ EastlawUI_v2.CommonClass.GetWords(dtLatest.Rows[a]["judgment"].ToString().Replace("<p>","").Replace("</p>",""), 6) + ""
                                                                        + "  <br><span style='float:right'> <a href='/departments/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["DeptName"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dtUpdatePanel.Rows[a]["Title"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dtUpdatePanel.Rows[a]["ID"].ToString()) + "'>Read more...</a></span></td></tr></table></li>");
                                                                    if (a == 15)
                                                                        break;
                                                                } 
                                                          //  }
                                                            
                                                        %>
</ul>
</div>
</div>
</div>
<div class="panel-footer"> 
     <%
                                                                    if (Request.QueryString["dptkp"] != null)
                                                                        Response.Write("<a href='/departments/latest-other/?param=" + Request.QueryString["dptkp"].ToString() + "&trans=" + Request.QueryString["dptn"].ToString() + "'>View More</a>");
                                                                    else
                                                                        Response.Write("<a href='/departments/latest-other/'>View More</a>");
                                                                    
         %>
</div>
</div>
        
        </div>
                    <%}
                                                            } %>
        		
        	
            </div>
        
        </div>
                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                  </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>


