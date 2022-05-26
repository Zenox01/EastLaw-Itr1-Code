<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsHome.aspx.cs" Inherits="EastlawUI_v2.m.DepartmentsHome" 
    MasterPageFile="~/m/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
    <div class="contentPage">

	<h1>
    <div class="container">
        <div class="margin">
         Department
        </div>
        
    </div>
    </h1>

	
	<div class="comingSoon">
    	<div class="container">
        	<div class="margin">
                <div class="departRow">
                    <div class="textRow">Search For Departments</div>
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                <asp:DropDownList ID="ddlDeptTypeGroups" runat="server"></asp:DropDownList> 
                            </div>
                            <div class="cols">
                                  <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList> 
                               
                            </div>
                            <div class="cols">
                                <asp:TextBox ID="txtTypesNo" runat="server" ToolTip="Year" CssClass="input"  placeholder="Enter No."></asp:TextBox>
                            </div>
                        </div>
                        <div class="rgt4">
                             <asp:Button ID="btnTypeSearch" runat="server" Text="Search" class="whtBtn4" OnClick="btnTypeSearch_Click"    />
                        </div>
                    </div>

                </div>

                <div class="departRow">
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                <asp:TextBox ID="txtSearch" runat="server" class="input" ToolTip="Free Search" placeholder="Search By Title"  AutoPostBack="True" ValidationGroup="Search" OnTextChanged="txtSearch_TextChanged" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="rgt4">
                            <asp:Button ID="btnFreeTextSearch" runat="server" Text="Search" class="whtBtn4" OnClick="btnFreeTextSearch_Click"  ValidationGroup="Search"/>
                            
                        </div>
                    </div>

                </div>

                <div class="departRow">
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                <asp:TextBox ID="txtSearchByDate" runat="server" class="input" ToolTip="Free Search By Date" placeholder="Search By Date DD/MM/YYYY"  AutoPostBack="false" ValidationGroup="SearchByDate" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtSearchByDate" runat="server" ControlToValidate="txtSearchByDate" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SearchByDate" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="rgt4">
                            <asp:Button ID="btnSearchByDate" runat="server" Text="Search" class="whtBtn4" OnClick="btnSearchByDate_Click"   ValidationGroup="SearchByDate"/>
                        </div>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Visible ="false"></asp:Label>
                </div>
                 <div class="departRow">
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                <asp:TextBox ID="txtFreeTextSearch" runat="server" class="input" ToolTip="Free Search" placeholder="Free Text Search"  AutoPostBack="false" ValidationGroup="FreeTextSearch" OnTextChanged="txtFreeTextSearch_TextChanged"  ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtFreeTextSearch" runat="server" ControlToValidate="txtFreeTextSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="FreeTextSearch" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="rgt4">
                            <asp:Button ID="btnTextSearch" runat="server" Text="Search" class="whtBtn4" OnClick="btnTextSearch_Click"  ValidationGroup="FreeTextSearch" />
                        </div>
                    </div>

                </div>
                <%
                    if (Session["MemberID"] != null)
                    {

                        EastLawBL.Departments objdept = new EastLawBL.Departments();
                        System.Data.DataTable dtlevel1 = objdept.GetActiveDeptByParent(0);
                        if (dtlevel1 != null)
                        {
                            for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                            {
                                Response.Write("<div class='departRow'>"
                                    + "<div class='textRow'><div class='buttonRow'><a href='/m/Departments/" + dtlevel1.Rows[l1]["DeptName"].ToString() + "." + EncryptDecryptHelper.Encrypt(dtlevel1.Rows[l1]["ID"].ToString()) + "' class='redBtn3'>" + dtlevel1.Rows[l1]["DeptName"].ToString() + "</a></div></div></div>"); 
                            }
                        }

                    }
                    %>
            <%--  <div class="departRow">
              	<div class="textRow">FBR</div>
                <div class="buttonRow">
                	<a href="#" class="redBtn3">Customs</a>
                    <a href="#" class="redBtn3">Income Tax</a>
                    <a href="#" class="redBtn3">Sales Tax</a>
                </div>
                 <div class="buttonRow2">
                	<a href="#" class="whtBtn3">Circular</a>
                    <a href="#" class="whtBtn3">Jurusuducation (Order)</a>
                    <a href="#" class="whtBtn3">Notifications/S.R.O</a>
                </div>
              </div>--%>
              
              
             
              
            
              
              
                
               <br /><br /><br /><br /><br /><br />	 
                
            </div>
        </div>
        <div class="clear"></div>
    </div>

</div>
        </form>
</asp:Content>


