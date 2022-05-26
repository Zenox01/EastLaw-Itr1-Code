<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="EastlawUI_v2.companyadminpanel.ManageUsers"
    MasterPageFile="~/companyadminpanel/Site1.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlcHolder">
    <div class="row">
    <div class="col-12">
        <div class="content-header">Manage User</div>
  
    </div>
</div>
<section id="extended">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
           
                <div class="card-body">
                    <div class="card-block" style="padding: 0;">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-responsive-md-md text-center"
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing"  OnRowCommand="gv_RowCommand1" AllowSorting="true" OnSorting="gv_Sorting">

                    <Columns>
                          <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit Record" CommandName="Edit"
                                    ImageUrl="~/adminpanel/media/img/edit.png" Height="16" Width="16" CausesValidation="false" Visible="false" />
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete Record" CommandName="Delete" Visible="true"
                                    ImageUrl="~/adminpanel/media/img/Delete.png" Height="16" Width="16" CausesValidation="false" OnClientClick='return confirm("Are you sure you want to delete this user?");' />
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                         <asp:TemplateField HeaderText="Reset Password" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary" Width="140" CommandName="ResetPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                       

                        <asp:TemplateField  SortExpression="CustNo">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCustNo" runat="server" Text="User #" CommandName="Sort" CommandArgument="CustNo" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  SortExpression="UserType" Visible="false">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSort2" runat="server" Text="User Type" CommandName="Sort" CommandArgument="UserType" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FullName">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSort" runat="server" Text="Full Name" CommandName="Sort" CommandArgument="FullName" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField  SortExpression="EmailID">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortEmail" runat="server" Text="User Name" CommandName="Sort" CommandArgument="EmailID" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PlanName" >
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPlan" runat="server" Text="Plan Name" CommandName="Sort" CommandArgument="PlanName" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField SortExpression="OrgName" >
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCompany" runat="server" Text="Org Name" CommandName="Sort" CommandArgument="OrgName" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PhoneNo">
                              <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPhoneNo" runat="server" Text="Phone No" CommandName="Sort" CommandArgument="PhoneNo" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField SortExpression="Status">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortStatus" runat="server" Text="Status" CommandName="Sort" CommandArgument="Status" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField  SortExpression="FormatedExpire" >
                              <HeaderTemplate>
        <asp:LinkButton ID="lnkSortPlanExpire" runat="server" Text="Plan Expire" CommandName="Sort" CommandArgument="FormatedExpire" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPlanExpire" runat="server" Text='<%# Eval("FormatedExpire") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="ExpireIn" >
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortNoOfDays" runat="server" Text="No. Of Days" CommandName="Sort" CommandArgument="ExpireIn" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("ExpireIn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="strActive">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortActive" runat="server" Text="Active" CommandName="Sort" CommandArgument="strActive" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("strActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="CreatedOn">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCreatedOn" runat="server" Text="Reg Date" CommandName="Sort" CommandArgument="CreatedOn" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FirstLogin">
                            <HeaderTemplate>
        <asp:LinkButton ID="lnkSortFirstLogin" runat="server" Text="First Login" CommandName="Sort" CommandArgument="FirstLogin" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFirstLogin" runat="server" Text='<%# Eval("FirstLogin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                      


                    </Columns>
                </asp:GridView> 

                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

</asp:Content>


