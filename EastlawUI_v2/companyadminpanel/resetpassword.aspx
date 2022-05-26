<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetpassword.aspx.cs" Inherits="EastlawUI_v2.companyadminpanel.resetpassword"
       MasterPageFile="~/companyadminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlcHolder">
    <asp:UpdatePanel ID="upPnl" runat="server">
        <ContentTemplate>

    <div class="col-md-8">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Reset User Password</h4>
                            </div>
                            <div class="content">
                               
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Email ID/User <i> (This will be login name and should be unique)</i>: *
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtEmailID" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                  </label>
                                                
                                                <asp:TextBox ID="txtEmailID" runat="server" class="form-control"> </asp:TextBox>
                                            </div>
                                        </div>
                                       
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                 <label>
                                       Full Name: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtFullName" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtFullName" runat="server" class="form-control" Enabled="false" > </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                 <label>
                                       Phone Number: *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                     ControlToValidate="txtPhone" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                
                                    </label>
                                     <asp:TextBox ID="txtPhone" runat="server" class="form-control" Enabled="false"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                               <label>
                                        New Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                     ControlToValidate="txtNewPassword" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                   <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" Width="250"> </asp:TextBox>
                                     
                                            </div>
                                        </div>
                                         <div class="col-md-4">
                                         <div class="form-group">
                                    <label >
                                        Genrate Password:<br />
                                           
                                    </label>
                                    <span style="display:none"> <asp:CheckBox ID="chkActive" runat="server" class="form-control" /></span>
                                          <br />   <asp:Button ID="btnGenerateAutoPassword" runat="server" CssClass="btn btn-primary" Text="Generate" Width="140" CausesValidation="false" OnClick="btnGenerateAutoPassword_Click" />
                                </div>
                                    </div>
                                        </div>
                                    
                                    
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-fill pull-right" Text="Reset Password" Width="150" OnClick="btnSave_Click" />
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
                                <br />
                                    <asp:Label ID="lblConfirmation" runat="server" ForeColor="Red"></asp:Label>
                                    <div class="clearfix"></div>
                               
                            </div>
                        </div>
                    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



