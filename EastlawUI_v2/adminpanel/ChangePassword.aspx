<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ChangePassword"
    MasterPageFile="~/adminpanel/Site1.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Users
                        <small>Password</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Password</li>
                    </ol>
                </section>
      
            <section class="content">
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">Change Password</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                                <div class="form-group">
                                    <label>
                                       Old Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtOldPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       New Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Confirm Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                </div>

                                
               <asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Change Password" Width="150" OnClick="btnSave_Click"  />





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
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (right) -->

                </div>
                

            </section>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
