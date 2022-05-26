<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMUserExpiry.aspx.cs" Inherits="EastlawUI_v2.adminpanel.CRMUserExpiry" 
    MasterPageFile="~/adminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
    <style>
        th.sortasc a  

    {

       display:block; padding:0 4px 0 15px; 

        background:url(media/img/view_sort_ascending.png) no-repeat;  

    }

    

    th.sortdesc a 

    {

        display:block; padding:0 4px 0 15px; 

       background:url(media/img/view_sort_descending.png) no-repeat;

  }
    #center250b { width: 100%; margin: auto; border: none; }
#fixedtop2 { position: fixed; width: 80%; top: 100px;background-color:white;  z-index: 50; } 
    </style>
     
    <asp:UpdatePanel ID="upl" runat="server">
        <ContentTemplate>

      <section class="content-header">
                    <h2>
                        User
                        <small>Users Expiry</small>
                    </h2>
                   
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                        <div class="col-xs-12">
                            
                            <!-- general form elements disabled -->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Users List</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                  
                                     

                                  
                                </div><!-- /.box-body -->
                                 <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
                                <div class="alert alert-danger alert-dismissable" id="divError" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction failed ... </strong>
                                </div>
                                <div class="alert alert-info alert-dismissable" id="divSuccess" runat="server" style="display: none">
                                    <button type="button" class="close" data-dismiss="alert">
                                        ×</button>
                                    <strong>Transaction success !</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
                    Width="100%" AllowPaging="True" PageSize="20" class="table table-bordered table-hover"
                     onpageindexchanging="gv_PageIndexChanging" >

                    <Columns>
               
                        <asp:TemplateField  SortExpression="CustNo">
                             <HeaderTemplate>
        <asp:LinkButton ID="lnkSortCustNo" runat="server" Text="User #" CommandName="Sort" CommandArgument="CustNo" />                
    </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  SortExpression="UserType" Visible="true">
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
        <asp:LinkButton ID="lnkSortEmail" runat="server" Text="Email ID" CommandName="Sort" CommandArgument="EmailID" />                
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
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
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
            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>



                            </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



