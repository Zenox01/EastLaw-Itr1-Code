<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PracticeAreaSearchGeneral.aspx.cs" Inherits="EastlawUI_v2.m.PracticeAreaSearchGeneral" 
    MasterPageFile="~/m/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
     <div class="contentPage">

	<h1>
    <div class="container">
        <div class="margin">
         <%
        Response.Write("<title>" + HttpContext.Current.Items["Title"].ToString() + "</title>"); %>
    <asp:Label ID="lblPA" runat="server" Text="Banking & Finance"></asp:Label>
        </div>
        
    </div>
    </h1>

	
	<div class="comingSoon">
    	<div class="container">
        	<div class="margin">
                <div class="departRow">
                    <div class="textRow">Search For Departments  <asp:Label ID="lblPAID" runat="server" Text="" Visible="false"></asp:Label></div>
                    

                </div>

                <div class="departRow">
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                 <asp:TextBox ID="txtSearch" runat="server" placeholder="Search From Practice Area"  CssClass="input"  ></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="rfvtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="rgt4">
                             <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="whtBtn4"  ValidationGroup="Search" />
                            
                        </div>
                    </div>

                </div>

                 <div class="departRow">
                    <div class="textRow">Quick Search</div>
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                 <asp:TextBox ID="txtSection" runat="server" class="input" ToolTip="Year" placeholder="Section"></asp:TextBox>
                               
                            </div>
                            <div class="cols">
                                 <asp:DropDownList ID="ddlTaggedStatutes" class="input" runat="server"></asp:DropDownList>
                               
                            </div>
                        </div>
                        <div class="rgt4">
                             <asp:Button ID="btnQuickFind" runat="server" Text="Search" class="whtBtn4" OnClick="btnQuickFind_Click"  />
                        </div>
                    </div>

                </div>
                 <div class="departRow">
                    <div class="textRow">Department Search</div>
                    <div class="inputSrchRow">
                        <div class="lft4">
                            <div class="cols">
                                <asp:DropDownList ID="ddlDeptTypeGroups" runat="server" class="input"  ></asp:DropDownList> 
                               
                            </div>
                            <div class="cols">
                               <asp:TextBox ID="txtTypesNo" runat="server" ToolTip="No" class="input" placeholder="Enter No."></asp:TextBox>
                               
                            </div>
                        </div>
                        <div class="rgt4">
                             
                             <asp:Button ID="btnTypeSearch" runat="server" Text="Search" class="whtBtn4" OnClick="btnTypeSearch_Click"    />
                        </div>
                    </div>

                </div>
                
           
               <br /><br /><br /><br /><br /><br />	 
                
            </div>
        </div>
        <div class="clear"></div>
    </div>

</div>
        </form>
</asp:Content>


