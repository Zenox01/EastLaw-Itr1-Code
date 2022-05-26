<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EastlawUI_v2.m.Register"
    MasterPageFile="~/m/General.Master" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="Middle">
    <asp:UpdatePanel ID="upPnlTop" runat="server">
              <ContentTemplate>
    <div class="sign">
	<div class="container">
         <div id="divThank" runat="server" style="display:none">
                        <p>Thanks for registration, kindly check your email for confirmation.</p>
                    </div>
    	<div class="margin" id="divReg" runat="server">
        	<h6>Sign Up for Trial</h6>
            <div class="backRow">
            	<div class="back"><a href="/m"><img src="/m/images/arrow.png" alt="arrow" class="arrow" />Back</a></div>
            </div>
            
                <div class="row">
                	<div class="left">
                    	<span>
                    	<div class="name">First Name<span>*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtFName"></asp:RequiredFieldValidator></div>
                        <div class="inputRow">
                            <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                        </div>
                        </span>
                    </div>
                    
                    <div class="right">
                    <span>
                    	<div class="name">Last Name<span>*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtLName"></asp:RequiredFieldValidator></div>
                        <div class="inputRow">
                            <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                        </div>
                     </span>
                    </div>
                </div>
                
                <div class="row">
                	<div class="name">Email<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reqfv" runat="server" ErrorMessage="Invalid Email ID" ValidationGroup="A" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblExist" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                    <div class="inputRow">
                    	<asp:TextBox ID="txtEmail" runat="server"  TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                	<div class="name">Mobile Number<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="A" runat="server" ErrorMessage="Country Code" ForeColor="Red" ControlToValidate="txtMobCountryCode" Enabled="false" ></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display = "Dynamic" ValidationGroup="A" runat="server" ErrorMessage="Mobile Code" ForeColor="Red" ControlToValidate="txtMobCode" ></asp:RequiredFieldValidator><br />
                             <asp:RegularExpressionValidator Display = "Dynamic" ValidationGroup="A" ForeColor="Red"  ControlToValidate = "txtMobCode" ID="RegularExpressionValidator2" ValidationExpression = "^[\s\S]{4,}$" runat="server" ErrorMessage="4 digits required in mobile code."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display = "Dynamic"  ValidationGroup="A" runat="server" ErrorMessage="Mobile Number" ForeColor="Red" ControlToValidate="txtMobNo" ></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ValidationGroup="A"  Display = "Dynamic" ForeColor="Red" ControlToValidate = "txtMobNo" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{7,}$" runat="server" ErrorMessage="7 digit required in mobile number."></asp:RegularExpressionValidator></div>
                    <div class="inputRow">
                    	<asp:TextBox ID="txtMobCountryCode" runat="server" Width="30" Text="" MaxLength="2" Visible="false"></asp:TextBox>  <asp:TextBox ID="txtMobCode" runat="server" Width="60" Text="03" MaxLength="4" ></asp:TextBox> - <asp:TextBox ID="txtMobNo" runat="server" Width="150" Text="" MaxLength="7" AutoPostBack="false" ></asp:TextBox>
                        <asp:Label ID="lblPhoneNoExist" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                
                 <div class="row">
                	<div class="name">Choose Password<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>&nbsp;
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Not Matched" ForeColor="Red"></asp:CompareValidator></div>
                    <div class="inputRow">
                    	<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                	<div class="name">Confirm Password<span>*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                	<div class="name">Orginization Name<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtOrgName"></asp:RequiredFieldValidator></div>
                    <div class="inputRow">
                    <asp:TextBox ID="txtOrgName" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                <div class="row">
                	<div class="left1">
                    	<span>
                    	<div class="name">Type of Organisation<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="A" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlOrgType" InitialValue="0"></asp:RequiredFieldValidator></div>
                        <div class="inputRow">
                           <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="false" >
                           
                        </asp:DropDownList>
                        </div>
                        </span>
                    </div>
                    
                    
                    </div>
                </div>
            <div class="row">
                <div class="left1">
                    <span>
                	<div class="name">City<span>*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="ddlCity" InitialValue="0"></asp:RequiredFieldValidator></div>
                        <div class="inputRow">
                        <asp:DropDownList ID="ddlCity" runat="server" >
                           
                        </asp:DropDownList>
                        </div>
                        </span>
                    </div>
                </div>
                 <div class="row">
                	<div class="name">Postal Address</div>
                    <div class="inputRow">
                    <asp:TextBox ID="txtPostalAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                	<div class="name">Intrested In<span>*</span>
                            </div>
                    <div class="">
                    	<asp:CheckBoxList ID="chkPracticeArea" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                    </div>
                </div>
                <div class="row">
                	<div class="terms"><asp:CheckBox ID="chkTNC" runat="server" Checked="true" />
                        I acknowledge and accept the <a class="termsa" href="#"><span>Terms and Conditions</span></a> as applicable</div>
                </div>
                <div class="signinRow2">
                	<asp:Label ID="lblMsgReg" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="whiteBtn3" OnClick="btnRegister_Click" ValidationGroup="A"/>
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



