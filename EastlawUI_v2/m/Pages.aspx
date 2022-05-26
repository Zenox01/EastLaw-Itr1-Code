<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="EastlawUI_v2.m.Pages" 
    MasterPageFile="~/m/MemberMaster.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <form runat="server">
       <div class="contentPage">
             
           <%
            if (HttpContext.Current.Items["pageid"].ToString() == "2")
            {
             %>
            <h1>
    <div class="container">
        <div class="margin">
            Your Valuable Feedback
        </div>
        
    </div>
    </h1>
             <div class="resources grayBg mrBtm">
       <div class="container">
            <div class="margin">
                
                <div class="adSearch" id="divForm" runat="server">
                	<div class="row">
                        <div class="find_legislation">
                            <div class="lft">
                                Name * <asp:RequiredFieldValidator ID="rfvFeedbackName" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Name." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:TextBox ID="txtName" runat="server" placeholder="" class="input1"></asp:TextBox>
                               
                                </div>
                            </div>
                            
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                                Email ID * <asp:RequiredFieldValidator ID="rfvFeedbackEmail" runat="server" ControlToValidate="txtEmailID" ErrorMessage="Enter Email ID." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="reFeedbackEmailID" runat="server" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="false"></asp:RegularExpressionValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:TextBox ID="txtEmailID" runat="server" placeholder="" class="input1"></asp:TextBox>
                               
                                </div>
                            </div>
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                                Mobile No * <asp:RequiredFieldValidator ID="rfvFeedbackMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Enter Mobile No." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="reFeedbackMobile"
                   ControlToValidate="txtMobile"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server" Enabled="false"/>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:TextBox ID="txtMobile" runat="server" placeholder="" class="input1" ></asp:TextBox>
                               
                                </div>
                            </div>
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                               Which best describes you?
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                   <asp:RadioButtonList ID="radioBestDescribe" runat="server">
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
                        <div class="find_legislation">
                            <div class="lft">
                               Are you interested in previewing new research features before they're officially launched?
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:RadioButtonList ID="radioIntrestedOfficialyLaunched" runat="server">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                               
                                </div>
                            </div>
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                              Want to share your EastLaw experience? (i.e., with other attorneys, press, social media, blog post, etc.)
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:RadioButtonList ID="radioCasetext" runat="server">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                               
                                </div>
                            </div>
                            
                        </div>
                         <div class="find_legislation">
                            <div class="lft">
                              What legal research tools do you or your firm currently pay for?
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:RadioButtonList ID="chkLstCurrentlypay" runat="server">
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
                         <div class="find_legislation">
                            <div class="lft">
                              Do you have feedback about the website or the legal research experience on EastLaw? Tell us here!
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                     <asp:TextBox ID="txtExpirence" runat="server" placeholder="" TextMode="MultiLine" Height="150"  CssClass="input1"></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                             Message *
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                     <<asp:TextBox ID="txtMsg" runat="server" placeholder=""  TextMode="MultiLine" Height="150"  CssClass="input1"></asp:TextBox>
                                </div>
                                <div class="searchBtn3">
                                     <asp:Button ID="btnSend" runat="server" Text="Submit" CssClass="input2" OnClick="btnSend_Click"    />
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                 <div style="font-weight:bold;color:green;display:none" id="divThank" runat="server" >
                    Thank you for your valuable feedback. Our team shall review it and update you accordingly.
                </div>
                
            </div>
            
        </div>
        <div class="clear"></div>
   </div> 	

          
           <%}
              else if (HttpContext.Current.Items["pageid"].ToString() == "7")
              { %>
           <h1>
    <div class="container">
        <div class="margin">
            Get in touch
        </div>
        
    </div>
    </h1>
           <div class="resources grayBg mrBtm">
       <div class="container">
            <div class="margin">
                
                <div class="adSearch" id="divFormC" runat="server">
                	<div class="row">
                        <div class="find_legislation">
                            <div class="lft">
                                Name * <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtNameC" ErrorMessage="Enter Name." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:TextBox ID="txtNameC" runat="server" placeholder="" CssClass="input1" ></asp:TextBox>
                               
                                </div>
                            </div>
                            
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                                Email ID * <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtEmailIDC" ErrorMessage="Enter Email." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reContactEmail" runat="server" ErrorMessage="Invalid Email ID" ControlToValidate="txtEmailIDC" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="false"></asp:RegularExpressionValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                    <asp:TextBox ID="txtEmailIDC" runat="server" placeholder="" CssClass="input1" ></asp:TextBox>
                               
                                </div>
                            </div>
                            
                            
                        </div>
                        <div class="find_legislation">
                            <div class="lft">
                                Mobile No *<asp:RequiredFieldValidator ID="rfvContactMobile" runat="server" ControlToValidate="txtMobileNoC" ErrorMessage="Enter Mobile No." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator id="reContactPhone"
                   ControlToValidate="txtMobileNoC"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only" ForeColor="Red"
                   runat="server" Enabled="false"/>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                      <asp:TextBox ID="txtMobileNoC" runat="server" placeholder="" CssClass="input1" ></asp:TextBox>
                                </div>
                            </div>
                            
                            
                        </div>
                         <div class="find_legislation">
                            <div class="lft">
                               Message * <asp:RequiredFieldValidator ID="rfvContactMsg" runat="server" ControlToValidate="txtMsgC" ErrorMessage="Enter Message." ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="rgt">
                                <div class="input">
                                      
                                    <asp:TextBox ID="txtMsgC" runat="server" placeholder=""  TextMode="MultiLine" Height="150" CssClass="input1"></asp:TextBox>
                                </div>
                                <div class="searchBtn3">
                                    <asp:Button ID="btnSendC" runat="server" Text="Submit" CssClass="input2" OnClick="btnSendC_Click"     />
                                </div>

                            </div>
                            
                            
                        </div>
                        </div>
                    </div>
                <div style="font-weight:bold;color:green;display:none" id="divThankC" runat="server" >
                    We have received your message and shall get back to you soon, Thank you.
                </div>
                </div>
           </div>
               </div>
           <%} %>
            <%
                 try
                 {
                     System.Data.DataTable dt = new System.Data.DataTable();
                     EastLawBL.Pages objpages = new EastLawBL.Pages();
                     dt = objpages.GetPages(int.Parse(HttpContext.Current.Items["pageid"].ToString()));
                     if (dt.Rows.Count > 0)
                     {
                        %>
           <h1>
    <div class="container">
        <div class="margin">
             <%     
                         Response.Write(dt.Rows[0]["Title"].ToString());
                   
                    %>
        </div>
    </div>
    </h1>
           <div class="margin">
        	<p>
                <%Response.Write("<p>" + dt.Rows[0]["FullContent"].ToString() + "</p>"); %>
            </p>
        </div>
           <%}

                 }
                 catch (Exception ex)
                 {
                     Response.Write(ex.Message);
                 } %>
           </div>
        </form>
</asp:Content>


