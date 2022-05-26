<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="EastlawUI_v2.RegistrationPage" 
    MasterPageFile="~/Withoutlogin.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <div class="container">
    <div class="row">
    
    <!------------ Left Side ------------->
    
    	
    <!------------ Left Side End ------------->
    
    <!------------ Right Side ------------->
    
    <div class="col-lg-9 col-md-9 center bg_cover">
    
    	<div class="col-lg-8 col-md-8 center">
        
        	<div class="row link text-center">
            
            	<h4>“Register as a member or <a href="/member/member-login">Click Here</a> to Login”</h4>
            
            </div>
        
        	<div class="row">
        
        		<div class="login sign_up" id="divReg" runat="server">
                
                	<div class="style_3">
                    	<i class="fa fa-user icon_color"></i><span class="span_style">New Registration</span>
                    </div>
                    
                    <div>
                    
                    <div class="col-lg-6 col-md-6">
                        <asp:TextBox ID="txtFName" runat="server" class="form-control" placeholder="First Name*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server" ErrorMessage="First Name Required" Text="Required" ForeColor="Red" ControlToValidate="txtFName" Display="Dynamic"></asp:RequiredFieldValidator>
                    
                    </div>
                    
                    <div class="col-lg-6 col-md-6">
                        <asp:TextBox ID="txtLName" runat="server" class="form-control" placeholder="Last Name*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server" ErrorMessage="Last Name Required" Text="Required" ForeColor="Red" ControlToValidate="txtLName" Display="Dynamic"></asp:RequiredFieldValidator>
                    
                    
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                     <%--<input type="text" class="form-control" placeholder="Mobile*" />--%>
                        <asp:TextBox ID="txtMobCountryCode" runat="server" Width="30" Text="" MaxLength="2" Visible="false"></asp:TextBox>  
                        <asp:TextBox ID="txtMobCode" runat="server" Width="60" Text="03" MaxLength="4"></asp:TextBox> - <asp:TextBox ID="txtMobNo" runat="server" Width="80" Text="" MaxLength="7" AutoPostBack="false" OnTextChanged="txtMobNo_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="A" runat="server" ErrorMessage="Country Code" ForeColor="Red" ControlToValidate="txtMobCountryCode" Enabled="false" ></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display = "Dynamic" ValidationGroup="A" runat="server" ErrorMessage="Mobile Code" ForeColor="Red" ControlToValidate="txtMobCode" ></asp:RequiredFieldValidator><br />
                             <asp:RegularExpressionValidator Display = "Dynamic" ValidationGroup="A" ForeColor="Red"  ControlToValidate = "txtMobCode" ID="RegularExpressionValidator2"  ValidationExpression = "^[\s\S]{4,}$" runat="server" ErrorMessage="4 digits required in mobile code."></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtMobCode" Display="Dynamic" ValidationGroup="A" SetFocusOnError="true" ForeColor="Red" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display = "Dynamic"  ValidationGroup="A" runat="server" ErrorMessage="Mobile Number" ForeColor="Red" ControlToValidate="txtMobNo" ></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ValidationGroup="A"  Display = "Dynamic" ForeColor="Red" ControlToValidate = "txtMobNo" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{7,}$" runat="server" ErrorMessage="7 digit required in mobile number."></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="A" ControlToValidate="txtMobNo" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                        
                        <asp:Label ID="lblPhoneNoExist" runat="server"  Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                        <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="false" OnTextChanged="txtEmail_TextChanged" class="form-control" placeholder="Email*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A" runat="server" ErrorMessage="Email Required" Text="Required" ForeColor="Red" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reqfv" runat="server" ErrorMessage="Invalid Email ID" ValidationGroup="A" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblExist" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtEmail" ControlToValidate="txtREmail" ErrorMessage="Email Not Matched" 
                            Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                    
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                        <asp:TextBox ID="txtREmail" runat="server" AutoPostBack="false" OnTextChanged="txtEmail_TextChanged" class="form-control" placeholder="Re-Enter Email*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="A" runat="server" 
                            ErrorMessage="Re-Type Email Required" Text="Required" ForeColor="Red" ControlToValidate="txtREmail" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID" ValidationGroup="A" 
                                ControlToValidate="txtREmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                    
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" placeholder="Password*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="A" runat="server" ErrorMessage="Password Required"
                             Text="Required" ForeColor="Red" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                     ErrorMessage="Not Matched" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                    
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="A" runat="server" ErrorMessage="Confirm Password Required" 
                            Text="Required" ForeColor="Red" ControlToValidate="txtConfirmPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                        	 <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"  class="form-control" placeholder="Confirm Password*"></asp:TextBox>
                    
                    </div>
                          
                    <div class="col-lg-12 col-md-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="A" runat="server" ErrorMessage="Organization Required" 
                            Text="Required" ForeColor="Red" ControlToValidate="txtOrgName" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtOrgName" runat="server" class="form-control" placeholder="Organization*"></asp:TextBox>
                    
                    </div>
                    
                    <div class="col-lg-12 col-md-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="A" runat="server" ErrorMessage="Organization Type Required" 
                            Text="Required" ForeColor="Red" ControlToValidate="ddlOrgType" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="false" class="form-control">
                           
                        </asp:DropDownList>
                    
                    </div>
                    
                    
                    <div class="col-lg-6 col-md-6" style="margin-top:10px;display:none" >
                    <label>Country</label>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="A" runat="server" ErrorMessage="Required" ForeColor="Red"
                              ControlToValidate="ddlCountry" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" disabled="disabled" style="margin-top:3px !important;background:#fff;">
                           
                        </asp:DropDownList>
                    	
                                            
                    </div>
                    
                    <div class="col-lg-12 col-md-12" style="margin-top:10px;">
                   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="A" runat="server" 
                            ErrorMessage="City Required" Text="Required" ForeColor="Red" ControlToValidate="ddlCity" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlCity" runat="server" class="form-control" style="margin-top:3px !important;">
                           
                        </asp:DropDownList>
                    	
                                            
                    </div>
                          <div class="col-lg-12 col-md-12">
                        <asp:TextBox ID="txtPostalAddress" runat="server" class="form-control" placeholder="Complete Postal Address*" TextMode="MultiLine" Height="70"></asp:TextBox>
                    
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                    
                    <label class="text-center">Interested In</label>
                        <asp:CheckBoxList ID="chkPracticeArea" runat="server" RepeatColumns="2" Font-Size="Medium"></asp:CheckBoxList>
                    	
                        
                        
                   
                    </div>
                    
                    </div>
                    
                     <div class="text-center" >
                       <br /> International Users please send your request for Registration to  <a href="mailto:info@eastlaw.pk" target="_top">info@eastlaw.pk</a>
                    
                    </div>
                    
                    <div class="text-center" style="margin-top:15px;">
                    <span><br />
                              <asp:CheckBox ID="chkTNC" runat="server" Checked="true" />&nbsp; 
                            I acknowledge and accept the <a href="/en/Terms-of-Use" target="_blank">Terms and Conditions</a> as applicable</span>
                    	
                         <asp:Label ID="lblMsgReg" runat="server" Visible="false"></asp:Label>
                        
                        <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-danger btn_style" OnClick="btnRegister_Click" ValidationGroup="A"/>
                    
                    </div>
                    <div class="text" style="margin-top:15px;">
                   
                        <asp:ValidationSummary ID="valSUmmaryContactUs" runat="server" ValidationGroup="A"  ForeColor="Red" Font-Bold="true"/>
                    </div>
                    
                    <div class="line" style="display:none">
                    	
                        <div class="login_with">
                        	<span>or register using</span>
                        </div>
                        
                    </div>
                    
                    <div class="row" style="display:none">
                    
                    	<div class="text-center">

    <a href="http://facebook.com" class="btn btn-social-icon btn-facebook">
    <i class="fa fa-facebook"></i></a>    
   
    <a class="btn btn-social-icon btn-google-plus"><i class="fa fa-google-plus"></i></a>
    
    <a class="btn btn-social-icon btn-linkedin"><i class="fa fa-linkedin"></i></a>
    
    <a class="btn btn-social-icon btn-twitter"><i class="fa fa-twitter"></i></a>
    
  </div>
                        
                    
                    </div>
                    
                </div>
                 <div id="divThank" runat="server" style="display:none">
                        <p>Thanks for registration, kindly check your email for confirmation.</p>
                    </div>
                
                
                
                
                
        
    		</div>
            
    	</div>
        
        </div>
    
    <!------------ Right Side End ------------->
    
    	
    </div>  
    </div>
</asp:Content>


