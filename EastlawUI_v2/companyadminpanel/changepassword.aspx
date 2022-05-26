<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepassword.aspx.cs" Inherits="EastlawUI_v2.companyadminpanel.changepassword" 
           MasterPageFile="~/companyadminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlcHolder">
    <asp:UpdatePanel ID="upPnl" runat="server">
        <ContentTemplate>

    <div class="col-md-8">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Change Password</h4>
                            </div>
                            <div class="content">
                               
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>
                                       Old Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtOldPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                            </div>
                                        </div>
                                        </div>
                                        <div class="row">
                                       
                                        <div class="col-md-5">
                                            <div class="form-group">
                                           <label>
                                       New Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                            </div>
                                        </div>
                                            </div>
                                            <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                 <label>
                                       Confirm Password: *
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                     ControlToValidate="txtOldPassword" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                            </div>
                                        </div>
                                        
                                         
                                        </div>
                                    
                                    
                                
                                   <asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Change Password" Width="170" OnClick="btnSave_Click"  />

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


