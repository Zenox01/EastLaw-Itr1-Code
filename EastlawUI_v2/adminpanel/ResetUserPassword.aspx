<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetUserPassword.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ResetUserPassword"
    MasterPageFile="~/adminpanel/Site1.Master" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="usercontrol/usersearch_actionpanel.ascx" tagname="usersearch_actionpanel" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntplc">
        
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             
      <section class="content-header">
                    <h1>
                        Users
                        <small>Reset User Password</small>
                    </h1>
             
                </section>
      
            <section class="content">
                <uc1:usersearch_actionpanel ID="usersearch_actionpanel1" runat="server" />
                <div class="row">
                    <!-- left column -->
                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-6">
                        <!-- general form elements disabled -->
                        <div class="box box-warning">
                            <div class="box-header">
                                <h3 class="box-title">User Details</h3>
                            </div>
                             
                            <!-- /.box-header -->
                            <div class="box-body">
                                
                               
                                <div class="form-group">
                                    <label>
                                        Email ID <i> (This will be login name and should be unique)</i>: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtEmailID" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ></asp:RegularExpressionValidator>
                                                
                                    </label>
                                    <asp:TextBox ID="txtEmailID" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                
                                <div class="form-group">
                                    <label>
                                       Full Name: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtFullName" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtFullName" runat="server" class="form-control" Enabled="false" > </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                       Phone Number: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                     ControlToValidate="txtPhone" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtPhone" runat="server" class="form-control" Enabled="false"> </asp:TextBox>
                                </div>
                             
                                
                                <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" Enabled="false"/>
                                </div>
                                
                                 <div class="form-group" >
                                    <label>
                                        New Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="txtNewPassword" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" Width="250"> </asp:TextBox>
                                     <asp:Button ID="btnGenerateAutoPassword" runat="server" CssClass="btn btn-primary" Text="Generate Password" Width="150" CausesValidation="false" OnClick="btnGenerateAutoPassword_Click" />
                                </div>
                                



                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Width="150" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
               
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Reset Password" Width="150" OnClick="btnSave_Click" />

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