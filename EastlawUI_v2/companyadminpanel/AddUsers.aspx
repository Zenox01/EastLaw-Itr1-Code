<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUsers.aspx.cs" Inherits="EastlawUI_v2.companyadminpanel.AddUsers" 
    MasterPageFile="~/companyadminpanel/Site1.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlcHolder">
    <asp:UpdatePanel ID="upPnl" runat="server">
        <ContentTemplate>

    <div class="col-md-8">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Add/Edit User</h4>
                            </div>
                            <div class="content">
                               
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Full Name:* <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                     ControlToValidate="txtFullName" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtFullName" runat="server" class="form-control" > </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Login UserName:* <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                     ControlToValidate="txtEmailID" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator> <asp:Label ID="lblExist" runat="server" Visible="false" ForeColor="Red"></asp:Label></label>
                                                <asp:TextBox ID="txtEmailID" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtEmailID_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Password: *<asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server"
                                                     ControlToValidate="txtPassword" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Phone Number: * <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                     ControlToValidate="txtPhone" ErrorMessage="Required" ForeColor="Red"  ></asp:RequiredFieldValidator></label>
                                               <asp:TextBox ID="txtPhone" runat="server" class="form-control" > </asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-md-4">
                                         <div class="form-group">
                                    <label>
                                        Active:
                                           
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" class="form-control" />
                                </div>
                                    </div>
                                        </div>
                                    
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-fill pull-right" Text="Save" Width="150" OnClick="btnSave_Click" />
                                <br />
                                    <asp:Label ID="lblConfirmation" runat="server" ForeColor="Red"></asp:Label>
                                    <div class="clearfix"></div>
                               
                            </div>
                        </div>
                    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



