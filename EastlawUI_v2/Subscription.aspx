<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="EastlawUI_v2.Subscription" 
    MasterPageFile="~/Withoutlogin.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
    <style>
        .line1 {
   width: 112px;
   height: 47px;
   border-bottom: 1px solid red;
   -webkit-transform:
       translateY(-20px)
       translateX(5px)
       rotate(27deg); 
   position: absolute;
   /* top: -20px; */
}
    </style>
      <%-- <asp:UpdatePanel ID="uppnl" runat="server">
        <ContentTemplate>
             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppnl">
        <ProgressTemplate>
            <div class="overlay">
                <div class="center">
                    <img alt="" src="/images/ajax-loader3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div class="container"> 
    <div class="row" >
    	      <%--<img src="/images/final.jpg" width="100%" />  --%>
      <h3>Subscription Plans</h3>
        
        	<div class="grayContainer" style="background-color:white">
                 <div id="divOrderExist" runat="server" style="display:none" >
                     <h3>Please check your email</h3>
                            
                            We have just sent you an email at  <asp:Label ID="lblEmailIDForExist" runat="server"></asp:Label>. containing the details of your Order. Your Account will be made Active within 24Hrs.

                               <br /><br />
                            For any queries, please feel free to send us an email at <a href="mailto:info@eastlaw.pk" target="_top">info@eastlaw.pk</a>
                     </div>
                <div id="divConfirm" runat="server"  style="display:none">
                    <button onclick="printContent('print')">Print Order</button>
                    <div id="print">
                        <div style="float:left;width:55%;text-align:left">
                            <h3>THANK YOU FOR YOUR ORDER AS MENTIONED BELOW:</h3>
                            <span style="color:blue">
                                Your Ordered Subscription:<br /><br />
                                <asp:Label ID="lblPlan" runat="server" Text="1 user" ForeColor="#00b050" Font-Bold="true"></asp:Label>
                            </span>
                            <span style="color:blue;">
                              <br />  <b>Total Price: <asp:Label ID="lblPrice1" runat="server" ></asp:Label>/-</b>
                            </span>
                            <br />
                            <br />

                            <h3>Please check your email</h3>
                            
                            We have just sent you an email at  <asp:Label ID="lblEmailID" runat="server" Font-Bold="true"></asp:Label>. containing the details of your Order along with a copy of the Invoice(<i>unpaid</i>).

                               <br /><br />
                            
                            <p style="display:none">
                                You are requested to make the payment transfer in either of our Bank Accounts as stated below and Email the
                                 copy of the Payment Receipt to <a href="mailto:info@eastlaw.pk" target="_blank">info@eastlaw.pk</a>  &  <a href="mailto:support@eastlaw.pk" target="_blank">support@eastlaw.pk</a>. 
                                Please clearly mention the Order# and Username in your email.<br /></p>
            <h4 style="display:none"><u><i>Bank Deposit or Online Transfer:</i></u></h4>
    
    <p style="color:rgb(0,112,192);font-size:13pt;display:none">
        1) <b>United Bank Limited</b><br />
        &nbsp;&nbsp;&nbsp; Account Title:  <span style="color:red">EAST LAW (PRIVATE) LIMITED</span><br />
        &nbsp;&nbsp;&nbsp; Current Account No#: <b>0109225401822</b><br />
        &nbsp;&nbsp;&nbsp; Branch: Lytton Road Branch, Mozang, Lahore.
    </p>
    <p style="color:rgb(0,112,192);font-size:13pt;display:none">
        2) <b> Bank Alfalah Limited</b><br />
        &nbsp;&nbsp;&nbsp; Account Title:  <span style="color:red">EAST LAW (PRIVATE) LIMITED</span><br />
        &nbsp;&nbsp;&nbsp; Current Account No#: <b>0061005166475</b><br />
        &nbsp;&nbsp;&nbsp; Branch: Shahdin Manzil, Lahore.
    </p>
                                
                                
                                </p>
                                <br />
                                Your Account will be made Active within 24Hrs.<%-- of receiving the Payment Confirmation.--%>
                                <br />
                                <br />
                                For any queries, please feel free to send us an email at <a href="mailto:info@eastlaw.pk" target="_blank">info@eastlaw.pk</a>.
                                <br />
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
        </p>

                            
                            
                        </div>
                        <div style="float:left;width:25%;text-align:center;background-color:wheat">
<h3 style="color:blue">Order #</h3>
                            <asp:Label ID="lblInvNo" runat="server" Font-Size="Large"></asp:Label>
                            <hr />
                            <span style="font-weight:bold;color:red;padding-bottom:2px">Payment Pending</span>
                            <span><a href="/Easypaisa_Confirmation.aspx" ><img src="/images/EPLogo.png" width="100" height""/>Pay</a> </span>
                              
                        </div>
                        </div>
                     
                    </div>
            	<div class="srchLft" id="divPlans" runat="server" >
                    
                   <h4>Individual</h4>

                   <asp:GridView ID="gvPlans" runat="server" Width="100%" AutoGenerateColumns="false" OnRowEditing="gvPlans_RowEditing" CellPadding="20" >
                            <HeaderStyle BackColor="#D11515" ForeColor="White"  VerticalAlign="Middle" />
                       
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Plan" ItemStyle-Width="500">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                         <HeaderTemplate>
         <div style="text-align:center;">Validity (No. of days)</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                      <HeaderTemplate>
         <div style="text-align:center;">Price</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-decoration:line-through">
                                            <%--<%# "Rs. "+( (Convert.ToDouble(Eval("Price"))/100 * 20) + (Convert.ToDouble(Eval("Price"))))  %>--%>
                                           <%-- <%# "Rs. "+ Eval("OldPrice","{0:#,##}") %>--%>
                                        </div>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# "Rs. "+ Eval("Price","{0:#,##}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText=""  >
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Text="Choose Plan" class="btn btn-danger btn_style" CommandName="Edit"  />
                                         <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    <h4>Corporate</h4>
                    <asp:GridView ID="gvCorporatePlans" runat="server" Width="100%" AutoGenerateColumns="false" OnRowEditing="gvCorporatePlans_RowEditing" CellPadding="20" >
                            <HeaderStyle BackColor="#D11515" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Plan"  ItemStyle-Width="500">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanName" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                         <HeaderTemplate>
         <div style="text-align:center;">Validity (No. of days)</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NoofDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                      <HeaderTemplate>
         <div style="text-align:center;">Price</div>
     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server"  Text='<%# "Rs. "+ Eval("Price","{0:#,##}") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText=""  >
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Text="Choose Plan" class="btn btn-danger btn_style" Width="150" CommandName="Edit"  />
                                         <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    
    
                	<div class="row1">
                        
                       
                        
                    </div>
                    
                  
                  
                  <%--<div class="row1">
                  	<a href="#">This could be a hyperlink</a>
                    <a href="#">This could be a hyperlink</a>
                    <a href="#">This could be a hyperlink</a>
                  </div>--%>
                   
                    
                    
                </div>
               
            </div>
            
		
       
         
			            </div>
       
        
        
    </div>
       <%--     
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    </asp:Content>