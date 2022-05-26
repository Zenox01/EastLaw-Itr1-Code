<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contactus.aspx.cs" Inherits="EastlawUI_v2.contactus" 
    MasterPageFile="~/Withoutlogin.Master"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <asp:UpdatePanel runat="server">
                    <ContentTemplate>
    <div class="container">
    
    	
        <div class="col-lg-6 col-md-6">
        
        	<div class="cover-up" id="divFormC" runat="server">
            
            
            <legend class="text-center">Get In Touch</legend>
            <div class="form-group">
            	<div class="col-lg-6 col-md-6 ">
                
                        <asp:TextBox ID="txtNameC" runat="server" placeholder="Your Name" CssClass="form-control"  ValidationGroup="ValContactUs"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContactName" runat="server"  ValidationGroup="ValContactUs" ControlToValidate="txtNameC" ErrorMessage="Enter Name."
                         ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                	
                
                </div>
                
                <div class="col-lg-6 col-md-6">
                    
                        <asp:TextBox ID="txtEmailIDC" runat="server" placeholder="Your Email"  CssClass="form-control"  ValidationGroup="ValContactUs"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server"  ValidationGroup="ValContactUs" ControlToValidate="txtEmailIDC" ErrorMessage="Enter Email." 
                        ForeColor="Red" Enabled="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reContactEmail" runat="server"  ValidationGroup="ValContactUs"
                             ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailIDC" ForeColor="Red"
                             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="true" Display="Dynamic"></asp:RegularExpressionValidator>
                    	
                	
                
                </div>
            </div>
            
            <div class="form-group">
            
            	<div class="col-lg-12 col-md-12">
                <asp:TextBox ID="txtMobileNoC" runat="server" placeholder="Phone"   ValidationGroup="ValContactUs" CssClass="form-control" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContactMobile" runat="server" ControlToValidate="txtMobileNoC"
                         ErrorMessage="Enter Mobile No." ForeColor="Red" Enabled="true" Display="Dynamic" ValidationGroup="ValContactUs"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator id="reContactPhone"
                   ControlToValidate="txtMobileNoC"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server"  ValidationGroup="ValContactUs" Enabled="true"/>
               
                
                </div>
            
            </div>
            
            <div class="form-group">
            
            	<div class="col-lg-12 col-md-12">
                
                     <br />
                        <asp:TextBox ID="txtMsgC" runat="server" placeholder="Your Message ..."  CssClass="form-control" TextMode="MultiLine" Height="150" Width="400"  ValidationGroup="ValContactUs"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvContactMsg" runat="server" ControlToValidate="txtMsgC" ErrorMessage="Enter Message." 
                         ValidationGroup="ValContactUs" ForeColor="Red" Enabled="true" Display="Dynamic"> </asp:RequiredFieldValidator>
                    	
                	
                
                </div>
            
            </div>
             <div class="col-lg-12 col-md-12">
                            <div id="recaptchaContainer"></div>
                            <span id="reqCaptcha" style="display:none;float: left;margin-top: 10px;" class="isa_error"></span>
                 </div>
            <div class="col-lg-12 col-md-12">
           	 
                   <asp:Button ID="btnSendC" runat="server" Text="Submit" CssClass="btn btn-danger my_bt" OnClick="btnSendC_Click"   ValidationGroup="ValContactUs" OnClientClick="return IsValidCaptcha();"  />
                    <asp:ValidationSummary ID="valSUmmaryContactUs" runat="server" ValidationGroup="ValContactUs"  ForeColor="Red" Font-Bold="true"/>
                    
            </div>
            
            </div>
             <div style="font-weight:bold;color:green;display:none" id="divThankC" runat="server" >
                    We have received your message and shall get back to you soon, Thank you.
                </div>
        
        </div>
        
        <div class="col-lg-6 col-md-6">
        
        	<div class="cover-up text-center">
        	
            	<legend class="text-center">Contact Us</legend>	
            
            	<div class="col-lg-12 col-md-12">
                
                	<h4><strong>EastLaw (Pvt.) Ltd.</strong></h4>
                	<h4>39-Link Farid Kot Road,
Lahore, Pakistan.</h4>
					<h4><a href="http://www.eastlaw.pk/" target="_blank">www.EastLaw.pk</a></h4>
                    <h4 style="margin-top:15px;">Phone Number #: 042-37311670</h4>
                    <h4 style="margin-top:15px;">Helpline #: 0304-1116670</h4>
                    <h4>WhatsApp Number:  0304-1116670</h4>
                    <h5><i>( Mon to Sat - 10:00am – 6:00pm )</i></h5>
                    <h5><i>(Saturday - 10:00am – 3:00pm )</i></h5>
                    
                    <h4 style="margin-top:23px;"><b>General Queries:</b> info@eastlaw.pk</h4>
                    <%--<h4><b>Technical/Legal Queries:</b> support@eastlaw.pk</h4>--%>
                    
                </div>
            
            
            </div>
        
        </div>
    
    
    </div>

     <div class="clear"></div>
    <script src="https://www.google.com/recaptcha/api.js?onload=loadCaptcha&render=explicit"></script>

    <script type="text/javascript">


        function pageLoad() {
            //alert('page Ok!')
            loadCaptcha();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadCaptcha);

       // var apikey = '6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI'; // localhost
        var apikey = '6LfKmU0UAAAAACD-fHOawKPOauf4kJdNPY-v7YAV'; // live 

        function loadCaptcha() {
            //  alert('Test');
            var captchaContainer = null;
            captchaContainer = grecaptcha.render('recaptchaContainer', {
                'sitekey': apikey,
                'callback': function (response) {
                    //$('id$=txtCaptcha').val(response);
                    //jQuery('[id$=txtCaptcha]').val(response);
                    $('#hdnCaptchaRes').val(response);
                    console.log(response);
                },
                'theme': 'default'
            });
        };

        //var xhttp = new XMLHttpRequest();
        //xhttp.open("POST", "https://www.google.com/recaptcha/api/siteverify", true);
        ////xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        //xhttp.send("secret=6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe&response=Ford");
        //if (this.readyState == 4 && this.status == 200) {
        //    console.log(this.responseText);
        //}
    </script>
                                                </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>
