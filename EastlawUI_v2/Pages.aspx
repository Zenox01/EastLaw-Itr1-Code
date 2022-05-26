<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="EastlawUI_v2.Pages" 
    MasterPageFile="~/Withoutlogin.Master"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    
    <%--<div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="afterlogin.html" class="first">Home</a></li>
 
    
    <li><a href="#" class="current">Legal Maxims And Dictionary</a></li>
</ul>
  </div>
</div>--%>

        <div class="container">
    <div class="row">
    
   
    
   		<div class="col-lg-12 col-md-12">
        
        	<div class="row">
            
            	  <%
            if (HttpContext.Current.Items["pageid"].ToString() == "2")
            {
                
            
             %>
        <div class="grayContainer" style="background-color:white;padding-left:5px;padding-top:5px; width:60%;float:left">
            <div class="srchLft" id="divForm" runat="server">
                
                  
                    <h3>Your Valuable Feedback</h3>
                     <div class="row1">
                         <b>Name * <asp:RequiredFieldValidator ID="rfvFeedbackName" ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Name." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b>
                        <br />
                        
                        <asp:TextBox ID="txtName" runat="server" placeholder="" Width="300" ValidationGroup="SiteFeedback"></asp:TextBox>
                         
                    	
                    	
                    </div>
                <div class="row1">
                    <b>Email ID * <asp:RequiredFieldValidator ID="rfvFeedbackEmail" runat="server" ControlToValidate="txtEmailID" ErrorMessage="Enter Email ID." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="reFeedbackEmailID" runat="server" ValidationGroup="SiteFeedback" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="false"></asp:RegularExpressionValidator>
                    </b>
                        
                        <br />
                    
                        <asp:TextBox ID="txtEmailID" runat="server" placeholder="" Width="300"  ValidationGroup="SiteFeedback"></asp:TextBox>
                    	
                    	
                    </div>
                 <div class="row1">
                    <b> Mobile No * <asp:RequiredFieldValidator ID="rfvFeedbackMobile"  ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtMobile" ErrorMessage="Enter Mobile No." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="reFeedbackMobile"
                   ControlToValidate="txtMobile"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server" Enabled="false"/>
                    </b>
                        <br />
                     
                        <asp:TextBox ID="txtMobile" runat="server" placeholder="" Width="300" ValidationGroup="SiteFeedback"></asp:TextBox>
                    	
                    	
                    </div>
                <div class="row1">
                     <b> Which best describes you? <asp:RequiredFieldValidator ID="rfvDescribeU" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioBestDescribe" 
                         ErrorMessage="Which best describes you?" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b><br />
                    <asp:RadioButtonList ID="radioBestDescribe" runat="server" ValidationGroup="SiteFeedback">
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
                <div class="row1">
                     <b> Are you interested in previewing new research features before they're officially launched?  <asp:RequiredFieldValidator ID="rfvPreviewNewSearch" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioIntrestedOfficialyLaunched" 
                         ErrorMessage="previewing new research features before they're officially launched? " ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b><br />
                    <asp:RadioButtonList ID="radioIntrestedOfficialyLaunched" runat="server" ValidationGroup="SiteFeedback">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                       
                    </div>
                 
                <div class="row1">
                      <b>Want to share your EastLaw experience? (i.e., with other attorneys, press, social media, blog post, etc.) 
                          <asp:RequiredFieldValidator ID="rfvShareYourExperiecne" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="radioCasetext" 
                         ErrorMessage="share your EastLaw experienc" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                      </b><br />
                      <asp:RadioButtonList ID="radioCasetext" runat="server" ValidationGroup="SiteFeedback">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                       
                    </div>
                 <div class="row1">
                    <b> What legal research tools do you or your firm currently pay for?  <asp:RequiredFieldValidator ID="rfvCurrentlyPay" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="chkLstCurrentlypay" 
                         ErrorMessage="currently pay for ?" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b><br />
                      <asp:RadioButtonList ID="chkLstCurrentlypay" runat="server" ValidationGroup="SiteFeedback">
                          <asp:ListItem Text="pakistanlawsite" Value="pakistanlawsite"></asp:ListItem>
                        <asp:ListItem Text="indiankanoon" Value="indiankanoon"></asp:ListItem>
                        <asp:ListItem Text="rehmatullahmalik" Value="rehmatullahmalik"></asp:ListItem>
                          <asp:ListItem Text="LexisNexis" Value="LexisNexis"></asp:ListItem>
                          <asp:ListItem Text="Westlaw" Value="Westlaw"></asp:ListItem>
                           <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                       
                    </div>
                <div class="row1">
                     <b> Do you have feedback about the website or the legal research experience on EastLaw? Tell us here! 
                         <asp:RequiredFieldValidator ID="rfvSiteFeedbackText" 
                          ValidationGroup="SiteFeedback" runat="server" ControlToValidate="txtExpirence" 
                         ErrorMessage="Your feedback about the website ?" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b><br />
                     <asp:TextBox ID="txtExpirence" runat="server" placeholder="" TextMode="MultiLine" Height="150" Width="300" ></asp:TextBox>
                       
                    </div>
                 <div class="row1" style="display:none">
                        <asp:Label ID="Label4" runat="server" Text="Message *" Font-Size="Medium"></asp:Label><br />
                        <asp:TextBox ID="txtMsg" runat="server" placeholder=""  TextMode="MultiLine" Height="150" Width="300"></asp:TextBox>
                    	
                    	
                    </div>
                 <div class="row1">
                 <telerik:RadCaptcha ID="RadCaptcha1" runat="server" 
                                         ErrorMessage="Invalid Captcha Code" ForeColor="Red" CaptchaImage-BackgroundNoise="None" ValidationGroup="SiteFeedback" CaptchaImage-LineNoise="None" CaptchaImage-TextLength="5" CaptchaImage-BackgroundColor="#F5F5F5">
                                     </telerik:RadCaptcha>
                    </div>
                <div class="row1">
                <asp:Button ID="btnSend" runat="server" Text="Submit" CssClass="input2" OnClick="btnSend_Click" ValidationGroup="SiteFeedback"    />
                    
                    <asp:ValidationSummary ID="valsumarySiteFeedback" runat="server" ValidationGroup="SiteFeedback"  ForeColor="Red" Font-Bold="true"/>
                    </div>
                </div>
                <div style="font-weight:bold;color:green;display:none" id="divThank" runat="server" >
                    Thank you for your valuable feedback. Our team shall review it and update you accordingly.
                </div>
            </div>
   
        
        
            <div class="col-sm-4" style="width:40%;float:right">
       <div class="srchLft">
        	<span class="bdr-white" >
                <%
                    try
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        EastLawBL.Pages objpages = new EastLawBL.Pages();
                    
                    dt = objpages.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<div class='cases-forms'><h1 style='text-align:left'>" + dt.Rows[0]["Title"].ToString() + "</h1></div>");
                        Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>");
                    } 
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    %>
            </span>
           
           </div>
       
        </div>
           <%}
            else if (HttpContext.Current.Items["pageid"].ToString() == "7")
             {%>
              <div class="grayContainer" style="background-color:white;padding-left:5px;padding-top:5px; width:60%;float:left">
            <div class="srchLft" id="divFormC" runat="server">
                
                  
                    <h3>Get in touch</h3>
                     <div class="row1">
                         <b>Name * 
                             <asp:RequiredFieldValidator ID="rfvContactName" runat="server"  ValidationGroup="ValContactUs" ControlToValidate="txtNameC" ErrorMessage="Enter Name." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b>
                        <br />
                         
                        <asp:TextBox ID="txtNameC" runat="server" placeholder="" Width="300"  ValidationGroup="ValContactUs"></asp:TextBox>
                    	
                    	
                    </div>
                <div class="row1">
                    <b>Email ID * <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server"  ValidationGroup="ValContactUs" ControlToValidate="txtEmailIDC" ErrorMessage="Enter Email." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reContactEmail" runat="server"  ValidationGroup="ValContactUs" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailIDC" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="false"></asp:RegularExpressionValidator>
                    </b>
                        <br />
                    
                        <asp:TextBox ID="txtEmailIDC" runat="server" placeholder="" Width="300"   ValidationGroup="ValContactUs"></asp:TextBox>
                    	
                    	
                    </div>
                 <div class="row1">
                    <b> Mobile No *<asp:RequiredFieldValidator ID="rfvContactMobile" runat="server" ControlToValidate="txtMobileNoC" ErrorMessage="Enter Mobile No." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator id="reContactPhone"
                   ControlToValidate="txtMobileNoC"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server"  ValidationGroup="ValContactUs" Enabled="false"/>
                    </b>
                        <br />
                     
                        <asp:TextBox ID="txtMobileNoC" runat="server" placeholder="" Width="300"  ValidationGroup="ValContactUs" ></asp:TextBox>
                    	
                    	
                    </div>
                
                 <div class="row1">
                        
                     <b>Message * <asp:RequiredFieldValidator ID="rfvContactMsg" runat="server" ControlToValidate="txtMsgC" ErrorMessage="Enter Message."  ValidationGroup="ValContactUs" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator></b>
                     <br />
                        <asp:TextBox ID="txtMsgC" runat="server" placeholder=""  TextMode="MultiLine" Height="150" Width="300"  ValidationGroup="ValContactUs"></asp:TextBox>
                    	
                    	
                    </div>
                <div class="row1">
                <asp:Button ID="btnSendC" runat="server" Text="Submit" CssClass="input2" OnClick="btnSendC_Click"   ValidationGroup="ValContactUs"   />
                    <asp:ValidationSummary ID="valSUmmaryContactUs" runat="server" ValidationGroup="ValContactUs"  ForeColor="Red" Font-Bold="true"/>
                    </div>
                </div>
                <div style="font-weight:bold;color:green;display:none" id="divThankC" runat="server" >
                    We have received your message and shall get back to you soon, Thank you.
                </div>
            </div>
   
        
        
            <div class="col-sm-4" style="width:40%;float:right">

                <%
                    try
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        EastLawBL.Pages objpages = new EastLawBL.Pages();
                    
                    dt = objpages.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<div class='cases-forms'><h1 style='text-align:left'>" + dt.Rows[0]["Title"].ToString() + "</h1></div>");
                        Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>");
                    } 
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    %>
          
       
        </div>

             <%
                
             }
             else
             { %>
        <div class="col-sm-4" style="width:100%;float:left">
      
                <%
                    try
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        EastLawBL.Pages objpages = new EastLawBL.Pages();
                    
                    dt = objpages.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<div class='cases-forms'><h1 style='text-align:left'>" + dt.Rows[0]["Title"].ToString() + "</h1></div>");
                        Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>");
                    } 
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    %>
         
       
        </div>
        <%} %>
             
               
            </div>
        
        </div>
        
        
        
       
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    </asp:Content>