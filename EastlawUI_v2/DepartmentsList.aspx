<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsList.aspx.cs" Inherits="EastlawUI_v2.DepartmentsList" 
    MasterPageFile="~/MemberMaster.Master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
    <div class="container">
<div class="row breadcrum">
      <ul class="bc">
                    <%if (Session["MemberID"] != null)
          { 
          %>
        <li class="first"><a href="/member/member-dashboard"> Home</a></li>
        <% } else { %>
        <li><a href="/"> Home</a></li>
        <%} %>
                       <li><a href="/departments/departments-home"> Departments</a></li>
                    <li class="current"><asp:Label ID="lblCurCrumb" runat="server"></asp:Label></li>
                </ul>


  </div>
</div>

    <div class="container">
    <div class="row">
    
    <!------------ Search Tabs Section ------------->
    
    	
   
    
    <!-------------- Left Side --------------->
        <div class="col-lg-3 col-md-3">
        
        <div id="divRelatedDepartments" runat="server" style="display:none">
        <div class="panel panel-default style" >
  <div class="panel-heading panel-heading2">Related Links<i class="fa fa-snowflake-o" aria-hidden="true" style="float:right;font-size: 19px;"></i></div>
  <div class="panel-body my_panel">
  
  	<ul>
          <%
                        EastLawBL.Departments objdept = new EastLawBL.Departments();
                        //System.Data.DataTable dtSub = new System.Data.DataTable();
                        if (HttpContext.Current.Items["DeptGroupID"] != null)
                        {
                            System.Data.DataTable dtlevel3 = objdept.GetActiveDeptByParent(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString())));
                            if (dtlevel3 != null)
                            {
                                if (dtlevel3.Rows.Count > 0)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        Response.Write("<li><a href='/departments/" + dtlevel3.Rows[l3]["DeptName"].ToString().Replace("/", "-").Replace("'", "") + "." + EncryptDecryptHelper.Encrypt(dtlevel3.Rows[l3]["ID"].ToString()) + "?lstN=" + EncryptDecryptHelper.Encrypt(dtlevel3.Rows[l3]["ID"].ToString()) + "'> " + dtlevel3.Rows[l3]["DeptName"].ToString() + "</a></li> ");
                                        
                                    }
                                }
                                else
                                {
                                    //  objdept.getac
                                    System.Data.DataTable dtlevel2 = objdept.GetActiveDeptByParent(objdept.GetGroupParent(int.Parse(EncryptDecryptHelper.Decrypt(HttpContext.Current.Items["DeptGroupID"].ToString()))));
                                    if (dtlevel2 != null)
                                    {
                                        if (dtlevel2.Rows.Count > 0)
                                        {
                                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                                            {
                                                Response.Write("<li><a  href='/departments/" + dtlevel2.Rows[l2]["DeptName"].ToString().Replace("/", "-").Replace("'", "") + "." + EncryptDecryptHelper.Encrypt(dtlevel2.Rows[l2]["ID"].ToString()) + "'> " + dtlevel2.Rows[l2]["DeptName"].ToString() + "</a></li>");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                         %>
    
    <%--	<li><a href="#">Act, Rules and Amendments</a></li>
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
            <div id="divDepartmentsSearch" runat="server" style="display:none">
            <div class="panel panel-default">
                        <div class="panel-heading"><b>Departments</b></div>
                        <div class="panel-body">
                            <div class="srchRgt" style="width:250px">
                            <div class="row">
<div class="col-xs-12">
   
                        <div class="sCitation">	
                            <div class="fRow">
                        	<%--<div class="left"></div>--%>
                          <%--  <div class="right">--%>
                                <asp:DropDownList ID="ddlDeptTypeGroups" runat="server"   Width="100"></asp:DropDownList> 
                                <asp:DropDownList ID="ddlYear" runat="server"   Width="100"></asp:DropDownList> 
                                <asp:TextBox ID="txtTypesNo" runat="server" ToolTip="Year" Width="100" placeholder="Enter No."></asp:TextBox>
                                <asp:Button ID="btnTypeSearch" runat="server" Text="Search" class="input3" OnClick="btnTypeSearch_Click"    />
                                
                          <%--  </div>--%>
                        </div>
                            
                           <%-- <asp:DropDownList ID="ddlType" runat="server"   Width="100"></asp:DropDownList>
                            <asp:TextBox ID="TextBox1" runat="server" class="input1" ToolTip="Enter No" placeholder="Enter No."  Width="100" ></asp:TextBox>
                           
                            
                            
                            <asp:Button ID="Button1" runat="server" Text="Search" class="redBtn"  OnClick="btnFreeTextSearch_Click"  />--%>
                            
                            <div class="fRow">
                                 <asp:TextBox ID="txtSearch" runat="server" class="input1" ToolTip="Free Search" placeholder="Search By Title"  Width="200" AutoPostBack="True" ValidationGroup="Search" OnTextChanged="txtSearch_TextChanged" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                            
                            <asp:Button ID="btnFreeTextSearch" runat="server" Text="Search" class="input3" OnClick="btnFreeTextSearch_Click"  ValidationGroup="Search"/>
                                </div>
                            <div class="fRow">
                                 <asp:TextBox ID="txtSearchByDate" runat="server" class="input1" ToolTip="Free Search By Date" placeholder="Search By Date DD/MM/YYYY"  Width="200" AutoPostBack="false" ValidationGroup="SearchByDate" ></asp:TextBox>
                                <asp1:CalendarExtender ID="clnStart" runat="server" TargetControlID="txtSearchByDate"
                                Format="dd/MM/yyyy">
                            </asp1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtSearchByDate" runat="server" ControlToValidate="txtSearchByDate" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SearchByDate"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnSearchByDate" runat="server" Text="Search" class="input3" OnClick="btnSearchByDate_Click"   ValidationGroup="SearchByDate"/>
                                </div>
                              <div class="fRow">
                                 <asp:TextBox ID="txtFreeTextSearch" runat="server" class="input1" ToolTip="Free Search" placeholder="Free Text Search"  Width="200" AutoPostBack="false" ValidationGroup="FreeTextSearch" OnTextChanged="txtFreeTextSearch_TextChanged"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtFreeTextSearch" runat="server" ControlToValidate="txtFreeTextSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="FreeTextSearch"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnTextSearch" runat="server" Text="Search" class="input3" OnClick="btnTextSearch_Click"  ValidationGroup="FreeTextSearch" />
                                </div>
                        </div>
                <asp:Label ID="lblMsg" runat="server" Visible ="false"></asp:Label>
    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                </div>
        
        
        </div>

      
    <!-------------- Left Side End --------------->
    
    
    
    <!-------------- right Side --------------->
    <div class="col-lg-9 col-md-9 margin_bot_20">
    
    <div class="row">
    <div class="style0">
    	<div class="pull-left topper" style="width:100%;">
                        <div class="box" style="width:98%;">
            
            	<div class="inner" style="background:#eee;">
            	
                	
                    
                    <input type="text" class="form-control" style="margin-left: 6px;" placeholder="Title">
                    
                    <input type="text" class="form-control" style="margin:0 9px;" placeholder="Year:">
                    
                   <select class="form-control">
                    
                    	<option>
                        	All Subjects
                        </option>
                    	
                    </select>
                
            	</div>
                
                <button class="btn btn-default btn_2" type="submit"><i class="fa fa-search"></i></button>
                
            </div>
                        
                    </div>
                    
                 
    </div>
    </div>
    
    <div class="clearfix"></div>
    
    <asp:GridView ID="gvLst" runat="server" AutoGenerateColumns="false" CssClass="table table-filter" Width="100%" GridLines="None" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvLst_PageIndexChanging">
                    <pagersettings mode="NumericFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"  
            position="TopAndBottom" />
                    <pagerstyle cssclass="gridview" >

</pagerstyle>
                    <Columns>
                          <asp:TemplateField ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="Type" ControlStyle-Width="100">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("DType") %>'></asp:Label>    
                                                
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                
                                
                                <%--<asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label>                --%>
                                                <a href='<%# "/departments/"+  clsUtilities.RemoveSpecialCharacter(Eval("DeptName").ToString())+"/"+ clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.GetWords(Eval("Title").ToString(),3)).ToString()+"."+EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>' ><%# Eval("Title")%></a>
                                                <br />
                                                <br />
                                                <%# Eval("ShortDesc") %>
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>
                         <%-- <asp:TemplateField HeaderText="Year">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>                
                                
                                                <hr />
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                          <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DDate") %>'></asp:Label>                
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" ItemStyle-Width="150">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                                
                                <asp:Label ID="lblDpt" runat="server" Text='<%# Eval("ParentDeptName") %>'></asp:Label>                
                                
                                               
                            </itemtemplate>
                                        </asp:TemplateField>
                                         <%--   <asp:TemplateField HeaderText="View">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                              <a href='<%# "/store/departments/wordfiles/"+  Eval("WordFile") %>' target="_blank">View</a>
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                          <%--<asp:TemplateField HeaderText="View" Visible="false">
                                            <headerstyle cssclass="titleCareer" horizontalalign="Center" />
                                            <itemtemplate>
                              <a href='<%# "/Departments/"+  clsUtilities.RemoveSpecialCharacter(Eval("DeptName").ToString())+"/"+ clsUtilities.RemoveSpecialCharacter(Eval("WordFile").ToString())+"."+EncryptDecryptHelper.Encrypt(Eval("ID").ToString()) %>' >View</a>
                            </itemtemplate>
                                        </asp:TemplateField>--%>
                                       
                                            </Columns>
                </asp:GridView>

       
  
    
    
    </div>
    <!-------------- right Side End --------------->
    
    
     
    	
    </div>  
    </div>
</asp:Content>


