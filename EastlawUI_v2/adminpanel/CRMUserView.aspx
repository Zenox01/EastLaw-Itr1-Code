<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMUserView.aspx.cs" Inherits="EastlawUI_v2.adminpanel.CRMUserView" 
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
                        <small>Users List</small>
                    </h2>
                   
                </section>
      
    <section class="content">
                    <div class="row">
                        <!-- left column -->
                        <!--/.col (left) -->
                        <!-- right column -->
                        <%--<div class="col-md-6">--%>
                        <div class="col-xs-12">
                               <%-- <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>--%>
                            <%--<div id="center250b">--%>
                            <div class="box box-warning">
                                
<%--<div id="fixedtop2">--%>


                <div class="box-header">
                    <h3 class="box-title">Search</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                </div>
                <!-- /.box-body -->
                  <table style="width:100%">
                        <tr>
                        
                        <td style="width:24%">Name: </td>
                        <td style="width:25%"><asp:TextBox ID="txtName" runat="server" Width="220" class="form-control"> </asp:TextBox></td>
                            <td style="width:24%"></td>
                        <td style="width:25%"></td>
                            
                    </tr>
                    <tr>
                        <td>Email ID: </td>
                        <td> <asp:TextBox ID="txtSearchEmailID" runat="server" Width="220" class="form-control"> </asp:TextBox></td>
                         <td>Mobile Number: </td>
                        <td> <asp:TextBox ID="txSearchtMobileNo" runat="server" Width="220" class="form-control"> </asp:TextBox></td>
                        <td><asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-primary"  Width="100" OnClick="btnSearch_Click" CausesValidation="false" />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>

                        </td>
                    </tr>
                    <tr >
                        <td colspan="5">   
                            </td>
                    </tr>
                  
                </table>
                
   <%-- </div>--%>
</div>
            <%--</div>--%>
            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
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
                     onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" onrowdeleting="gv_RowDeleting" 
                        onrowediting="gv_RowEditing"  OnRowCommand="gv_RowCommand1" AllowSorting="true" OnSorting="gv_Sorting">

                    <Columns>
                          <asp:TemplateField HeaderText="Update Plan" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnUpdatePlan" runat="server" Text="Update Plan (7 Days Comp)" CssClass="btn btn-primary" Width="180" CommandName="UpdatePlan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Plan" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnUpdatePlan3Days" runat="server" Text="Update Plan (3 Days Comp)" CssClass="btn btn-primary"  CommandName="UpdatePlan3Days" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Plan" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnUpdatePlan30Days" runat="server" Text="Update Plan (30 Days Comp)" CssClass="btn btn-primary"  CommandName="UpdatePlan30Days" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Plan" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnUpdatePlan3Months" runat="server" Text="Update Plan (3 Months)" CssClass="btn btn-primary"  CommandName="UpdatePlan3Months" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Status" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-primary"  CommandName="UpdateStatus" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Reset Password" Visible="true">
                            <ItemTemplate>
                                 <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary"  CommandName="ResetPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
               <asp:TemplateField  HeaderText="Comments">
                            <ItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="100" Width="200" ></asp:TextBox><br /><br />
                                <asp:Button ID="btnAddComments" runat="server" Text="Add Comment" CssClass="btn btn-primary"  CommandName="AddComment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                <asp:Label ID="lblCommentsConfirmation" runat="server" ></asp:Label>
                                <br />
                                <br />
                                <a class="btn btn-block btn-primary" data-toggle="modal" data-target="#compose-modal<%# Eval("CustNo") %>"><i class="fa fa-pencil"></i> View Comments History</a>
                <div class="modal fade" id="compose-modal<%# Eval("CustNo") %>" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-user"></i> Comments</h4>
                    </div>
                    <form action="#" method="post">
                        <div class="modal-body">
                            <div class="form-group">
                               <%-- <div class="input-group">
                                    <span class="input-group-addon"><b>Judge Name:</b><%# Eval("JudgeName") %></span>
                                    
                                </div>--%>
                            </div>
                               <span class="input-group-addon">History</span><br />
                               <p class="help-block"><%# Eval("UserCRMComments") %></p>
                           <div class="form-group">
                                <div class="input-group">
                                 
                                </div>
                            </div>
                           
                           

                        </div>
                        <div class="modal-footer clearfix">

                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i> Discard</button>

                            
                        </div>
                    </form>
                </div><!-- /.modal-content -->
            </div>
        </div>
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
            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>



                            </div><!-- /.box -->
                        </div><!--/.col (right) -->
                         
                    </div>   
                </section>
            
       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



