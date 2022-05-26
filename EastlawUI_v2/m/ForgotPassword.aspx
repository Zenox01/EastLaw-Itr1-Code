<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="EastlawUI_v2.m.ForgotPassword" 
    MasterPageFile="~/m/General.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="Middle">
    <div class="sign">
	<div class="container">
    	<div class="margin">
        	<h6>Forgot Password</h6>
            <div class="backRow">
            	<div class="back"><a href="/m"><img src="/m/images/arrow.png" alt="arrow" class="arrow" />Back</a></div>
            </div>
            
                
                
                <div class="row" id="divEmailAdd" runat="server">
                	<div class="name">Email ID * <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailID" ></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                <asp:TextBox ID="txtEmailID" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                    

             <div class="row" style="display:none" id="divNewPassword" runat="server">
                	<div class="name">New Password *<asp:RequiredFieldValidator ID="rfvNewPass" ValidationGroup="BB" runat="server"
                              ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtNewPassword" Enabled="false"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="cvNewPass" runat="server" 
                                 ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"  Enabled="false"
                                 ErrorMessage="Not Matched" ForeColor="Red"></asp:CompareValidator></div>
                    <div class="inputRow">
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

              <div class="row" style="display:none" id="divConfirmNewPassword" runat="server">
                	<div class="name">Confirm New Password * <asp:RequiredFieldValidator ID="rfvConfirmNewPass" 
                             ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtConfirmNewPassword" Enabled="false" ></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                
               
                
                
                <div class="signinRow2">
                	
                    <asp:Button ID="btnSendPassword" runat="server" Text="Reset Password" CssClass="whiteBtn3" ValidationGroup="BB" OnClick="btnSendPassword_Click"/>
                        <asp:Button ID="btnUpdatePassword" runat="server" Text="Change Password" CssClass="whiteBtn3" Width="200" ValidationGroup="BB" Visible="false" OnClick="btnUpdatePassword_Click"/>
                </div>
                
                
                <div class="confirmMsg"><asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label></div>
            
        </div>
    </div>
</div>
</asp:Content>



