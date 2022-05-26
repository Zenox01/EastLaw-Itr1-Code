<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="EastlawUI_v2.ForgetPassword" 
    MasterPageFile="~/Withoutlogin.Master"%>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <div class="container">
    <div class="row">
    
    <!------------ Left Side ------------->
    
    	
    <!------------ Left Side End ------------->
    
    <!------------ Right Side ------------->
    
    <div class="col-lg-9 col-md-9 center bg_cover">
    
    	<div class="col-lg-6 col-md-6 center">
        
        	<div class="row link text-center">
            
            	<h3>Forget Password</h3>
            
            </div>
        
        	<div class="row">
        
        		<div class="login">
                
                	<%--<div class="style_3">
                    	<i class="fa fa-user icon_color"></i><span class="span_style">Login</span>
                    </div>--%>
                    
                    <div>
                   <div class="f_row" id="divEmailAdd" runat="server">
                    	 <label for="email">Email Address * <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailID" ></asp:RequiredFieldValidator></label>
                                
                                <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                         
                       </div>
                     <div class="f_row" style="display:none" id="divNewPassword" runat="server">
                    	 <label for="email">New Password * <asp:RequiredFieldValidator ID="rfvNewPass" ValidationGroup="BB" runat="server"
                              ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtNewPassword" Enabled="false"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="cvNewPass" runat="server" 
                                 ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"  Enabled="false"
                                 ErrorMessage="Not Matched" ForeColor="Red"></asp:CompareValidator>
                    	 </label>
                                
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                         
                       </div>
                     <div class="f_row" style="display:none" id="divConfirmNewPassword" runat="server">
                    	 <label for="email">Confirm New Password * <asp:RequiredFieldValidator ID="rfvConfirmNewPass" 
                             ValidationGroup="BB" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtConfirmNewPassword" Enabled="false" ></asp:RequiredFieldValidator></label>
                                
                                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                         
                       </div>
                    
                	
                    </div>
                    
                   
                    
                    <div>
                       
                    

                        <asp:Button ID="btnSendPassword" runat="server" Text="Reset Password" Width="200" ValidationGroup="BB" class="btn btn-danger btn_style" OnClick="btnSendPassword_Click"/>
                        <asp:Button ID="btnUpdatePassword" runat="server" Text="Change Password" Width="200" ValidationGroup="BB" class="btn btn-danger btn_style" Visible="false" OnClick="btnUpdatePassword_Click"/>
                        <asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label>
                    	<%--<input type="button" class="btn btn-danger btn_style" value="Login" />--%>
                    
                    </div>
                    
                    
                    
                    
                    
                    
                    
                
                
                </div>
                
                
                
                
                
        
    		</div>
            
    	</div>
        
        </div>
    
    <!------------ Right Side End ------------->
    
    	
    </div>  
    </div>
</asp:Content>

