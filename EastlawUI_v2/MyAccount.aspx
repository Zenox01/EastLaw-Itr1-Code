<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="EastlawUI_v2.MyAccount" 
    MasterPageFile="~/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlaceHolder">
       <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">Change Password</a></li>
   
</ul>
  </div>
</div>
         <asp:UpdatePanel ID="upPnlTop" runat="server">
            <ContentTemplate>
    <div class="container">
    <div class="row">
    
    <!------------ Left Side ------------->
    
    	
    <!------------ Left Side End ------------->
    
    <!------------ Right Side ------------->
    
    <div class="col-lg-9 col-md-9 center bg_cover">
    
    	<div class="col-lg-6 col-md-6 center">
        
        	<div class="row link text-center">
            
            	<h4>Change Password</h4>
            
            </div>
        
        	<div class="row">
        
        		<div class="login">
                
                	<div class="style_3">
                    	<span class="span_style">Change Password</span>
                    </div>
                    
                    <div>
                    
                    
                        
                          <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" class="form-control" placeholder="Enter Old Password" ValidationGroup="BB"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="BB" runat="server"
                              ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtOldPassword" Enabled="false" Display="Dynamic"></asp:RequiredFieldValidator>

                          <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"  ValidationGroup="BB" class="form-control" placeholder="Enter New Password" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPass" ValidationGroup="BB" runat="server"
                              ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtNewPassword" Enabled="false" Display="Dynamic"></asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="cvNewPass" runat="server" 
                                 ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" ValidationGroup="BB"  Enabled="false"
                                 ErrorMessage="Not Matched" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
                        
                        
                                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" class="form-control" placeholder="Confirm New Password" ValidationGroup="BB"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvConfirmNewPass" 
                              runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtConfirmNewPassword" Enabled="false" Display="Dynamic" ValidationGroup="BB"></asp:RequiredFieldValidator></label>
                                
                	
                    </div>
                    
                   
                    
                    <div>
                       
                    
                         <asp:Button ID="btnUpdatePassword" runat="server" Text="Change Password" Width="210" class="btn btn-danger btn_style" ValidationGroup="BB" OnClick="btnUpdatePassword_Click"  />
                        <br />
                        <asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label>
                    </div>
                    
                   
                    
                    
                    
                    
                 
                
                
                </div>
                
                
                
                
                
        
    		</div>
            
    	</div>
        
        </div>
    
    <!------------ Right Side End ------------->
    
    	
    </div>  
    </div>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                    <ProgressTemplate>
                     
                           <div class="modal1">
        <div class="center1">
           <img alt="" src="/style/img/ajax_loader_big.gif" />
        </div>
    </div>
                                
                           
                      
                    </ProgressTemplate>
                </asp:UpdateProgress>
                </ContentTemplate>
           </asp:UpdatePanel>
     
    </asp:Content>