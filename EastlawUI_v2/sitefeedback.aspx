<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sitefeedback.aspx.cs" Inherits="EastlawUI_v2.sitefeedback" 
    MasterPageFile="~/Withoutlogin.Master"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <div class="container">
    
    	
        <div class="col-lg-7 col-md-7 center">
        
        	<div class="cover-up" id="divForm" runat="server">
            
            
            <legend class="text-center">Site Feedback</legend>
                
            <div class="form-group">
            	<div class="col-lg-6 col-md-6 ">
                
                	
                    <asp:TextBox ID="txtName" runat="server" placeholder="Your Name" Width="300" ValidationGroup="SiteFeedback" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFeedbackName" ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Name." ForeColor="Red" Enabled="true"  Display="Dynamic"></asp:RequiredFieldValidator>
                
                </div>
                
                <div class="col-lg-6 col-md-6">
                    <asp:TextBox ID="txtEmailID" runat="server" placeholder="Your Email" Width="300"  ValidationGroup="SiteFeedback" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFeedbackEmail" runat="server" ControlToValidate="txtEmailID" ErrorMessage="Enter Email ID." ForeColor="Red" Enabled="true" Display="Dynamic"> </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="reFeedbackEmailID" runat="server" ValidationGroup="SiteFeedback" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="true" Display="Dynamic"> </asp:RegularExpressionValidator>
                	
                
                </div>
            </div>
            
            <div class="form-group">
            
            	<div class="col-lg-12 col-md-12">
                    <asp:TextBox ID="txtMobile" runat="server" placeholder="Phone" Width="300" ValidationGroup="SiteFeedback" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFeedbackMobile"  ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtMobile" ErrorMessage="Enter Mobile No." 
                    ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="reFeedbackMobile"
                   ControlToValidate="txtMobile"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server" Enabled="true" />
                	
                
                </div>
            
            </div>
            
            
            <div class="form-group">
            
                <div class="col-lg-12 col-md-12 text-center">
                    <h4>Which best describes you ?</h4>
                </div>
                
                <div class="col-lg-12 col-md-12">
                <div class="row">
                
                	<asp:RequiredFieldValidator ID="rfvDescribeU" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioBestDescribe" 
                         ErrorMessage="Which best describes you?" ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RadioButtonList ID="radioBestDescribe" runat="server" ValidationGroup="SiteFeedback" RepeatColumns="2">
                        <asp:ListItem Text="In-house counsel" Value="In-house counsel"></asp:ListItem>
                        <asp:ListItem Text="Law firm partner" Value="Law firm partner"></asp:ListItem>
                        <asp:ListItem Text="Law firm associate" Value="Law firm associate"></asp:ListItem>
                        <asp:ListItem Text="Solo practice" Value="Solo practice"></asp:ListItem>
                        <asp:ListItem Text="Government lawyer" Value="Government lawyer"></asp:ListItem>
                        <asp:ListItem Text="Academic" Value="Academic"></asp:ListItem>
                        <asp:ListItem Text="Law student" Value="Law student"></asp:ListItem>
                        <asp:ListItem Text="Law librarian" Value="Law librarian"></asp:ListItem>
                        <asp:ListItem Text="Legal support staff" Value="Legal support staff"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                
                </div>
                </div>
                
            </div>
            
            <div class="form-group">
            
                <div class="col-lg-12 col-md-12">
                    <h5>Are you interested in previewing new research features before they're officially launched?</h5>
                </div>
                
                <div class="col-lg-12 col-md-12">
                <div class="row">
                
                	<asp:RequiredFieldValidator ID="rfvPreviewNewSearch" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioIntrestedOfficialyLaunched" 
                         ErrorMessage="previewing new research features before they're officially launched? " ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RadioButtonList ID="radioIntrestedOfficialyLaunched" runat="server" ValidationGroup="SiteFeedback">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                       
                
                </div>
                </div>
                
            </div>
            
            
            <div class="form-group">
            
                <div class="col-lg-12 col-md-12">
                    <h5>Want to share your EastLaw experience? (i.e., with other attorneys, press, social media, blog post, etc.)</h5>
                </div>
                
                <div class="col-lg-12 col-md-12">
                <div class="row">
                
                	<asp:RequiredFieldValidator ID="rfvShareYourExperiecne" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioCasetext" 
                         ErrorMessage="share your EastLaw experienc" ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                      
                      <asp:RadioButtonList ID="radioCasetext" runat="server" ValidationGroup="SiteFeedback">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                
                </div>
                </div>
                
            </div>
            
            <div class="form-group">
            
                <div class="col-lg-12 col-md-12">
                    <h5>What legal research tools do you or your firm currently pay for?</h5>
                </div>
                
                <div class="col-lg-12 col-md-12">
                <div class="row">
                
                	<asp:RequiredFieldValidator ID="rfvCurrentlyPay" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="chkLstCurrentlypay" 
                         ErrorMessage="currently pay for ?" ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:RadioButtonList ID="chkLstCurrentlypay" runat="server" ValidationGroup="SiteFeedback">
                          <asp:ListItem Text="pakistanlawsite" Value="pakistanlawsite"></asp:ListItem>
                        <asp:ListItem Text="indiankanoon" Value="indiankanoon"></asp:ListItem>
                        <asp:ListItem Text="rehmatullahmalik" Value="rehmatullahmalik"></asp:ListItem>
                          <asp:ListItem Text="LexisNexis" Value="LexisNexis"></asp:ListItem>
                          <asp:ListItem Text="Westlaw" Value="Westlaw"></asp:ListItem>
                           <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                
                </div>
                </div>
                
            </div>
            
            
            
          <div class="form-group">
            
            <div class="col-lg-12 col-md-12">
                    <h5>Do you have feedback about the website or the legal research experience on EastLaw? Tell us here! </h5>
                </div>
            
            	<div class="col-lg-12 col-md-12">
          
                     <asp:TextBox ID="txtExpirence" runat="server" placeholder="Your Message ..." TextMode="MultiLine" Height="150" Width="300" ></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvSiteFeedbackText" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtExpirence" 
                         ErrorMessage="Your feedback about the website ?" ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                	
                
                </div>
            
            </div>
                   <div class="form-group">
                        <telerik:RadCaptcha ID="RadCaptcha1" runat="server" 
                                         ErrorMessage="Invalid Captcha Code" ForeColor="Red" CaptchaImage-BackgroundNoise="None" ValidationGroup="SiteFeedback" CaptchaImage-LineNoise="None" CaptchaImage-TextLength="5" CaptchaImage-BackgroundColor="#F5F5F5">
                                     </telerik:RadCaptcha>
                  
                </div>
            
            <div class="col-lg-12 col-md-12">
           	 
                <asp:Button ID="btnSend" runat="server" Text="Submit" CssClass="btn btn-danger my_bt" OnClick="btnSend_Click" ValidationGroup="SiteFeedback"    />
                    
                    <asp:ValidationSummary ID="valsumarySiteFeedback" runat="server" ValidationGroup="SiteFeedback"  ForeColor="Red" Font-Bold="true"/>
                
            </div>
            
            </div>
         <div style="font-weight:bold;color:green;display:none" id="divThank" runat="server" >
                    Thank you for your valuable feedback. Our team shall review it and update you accordingly.
                </div>
        
        </div>
        
        
    
    
    </div>
</asp:Content>


