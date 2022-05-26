<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EastlawUI_v2.m.Login" 
    MasterPageFile="~/m/General.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="Middle">
     <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
    <div class="sign">
	<div class="container">
    	<div class="margin">
        	<h6>Sign In</h6>
            <div class="backRow">
            	<div class="back"><a href="/m"><img src="/m/images/arrow.png" alt="arrow" class="arrow" />Back</a></div>
            </div>
            
                <div class="row">
                	<div class="name">Email Address* <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailIDLogin" ></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                    	
                        
                        <asp:TextBox ID="txtEmailIDLogin" runat="server" ValidationGroup="BB"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                	<div class="name">Password* <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtPasswordLogin" ></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                        
                                <asp:TextBox ID="txtPasswordLogin" runat="server" TextMode="Password" ValidationGroup="BB"></asp:TextBox>
                        <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="row">
                	<span><br />
                            <%--<input name="" type="checkbox" value="">--%> &nbsp;
                            <asp:CheckBox ID="chkTNC" runat="server" Checked="true" />
                             
                         Use of this service is subject to <a href="/en/Terms-of-Use" target="_blank">Terms & Conditions</a> and <a href="/en/Privacy-Policy" target="_blank">Privacy Policy</a>. Please review this information before proceeding. You must accept the terms and conditions to use the service
                </div>
                <div class="row">
                	<div class="lft"> <asp:CheckBox ID="chkRem" runat="server" Text="Remember Me" /></div>
                   <div class="rgt"><a href="/m/Member/Forget-Password">Forget your password ? </a></div>
                </div>
                <div class="signinRow2">
                	
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="whiteBtn3" OnClick="btnLogin_Click"  ValidationGroup="BB"/>

                </div>
            
        </div>
    </div>
</div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/m/images/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
              </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

