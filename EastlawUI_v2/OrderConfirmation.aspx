<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="EastlawUI_v2.OrderConfirmation" 
    MasterPageFile="~/Withoutlogin.Master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
       <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">Order Review</a></li>
   
</ul>
  </div>
</div>
      <div class="container">
    <div class="row">
    
    
    
   		<div class="col-lg-8 col-md-8">
        
        	    <div class="row" id="container" >
    	        
      <h3>Subscription</h3>
        
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
                            
                            We have just sent you an email at  <asp:Label ID="lblEmailID" runat="server" Font-Bold="true"></asp:Label> containing the details of your Order along with a copy of the Invoice(<i>unpaid</i>).

                               <br /><br />
                            
                           
                                <p>
                                    You are requested to please make the payment transfer using any one of the methods stated below and update us.
                                </p>
                                <h3 style="color:red"><u>Payment Methods:</u></h3>
                                <p>You can transfer the  payment in either of our Bank Accounts as stated below and Email the scanned copy of the Payment Receipt to <a href="mailto:info@eastlaw.pk" target="_blank">info@eastlaw.pk</a>. Please  clearly  mention the Order# and Username in your email.</p>
                                <br />
            <h4 ><u><i>Bank Deposit / Online Transfer:</i></u></h4>
    
                            <p style="color: black; font-size: 13pt; ">
                                <b>1) United Bank Limited</b><br />
                                &nbsp;&nbsp;&nbsp; Account Title:  <span style="color: red">EAST LAW (Pvt.) Ltd</span><br />
                                &nbsp;&nbsp;&nbsp; Current Account No#: <b>0109225401822</b><br />
                                &nbsp;&nbsp;&nbsp; Branch: Lytton Road Branch, Mozang, Lahore.
                            </p>
                            <p style="color: black; font-size: 13pt;">
                                <b>2) Bank Alfalah Limited</b><br />
                                &nbsp;&nbsp;&nbsp; Account Title:  <span style="color: red">EAST LAW (Pvt.) Ltd</span><br />
                                &nbsp;&nbsp;&nbsp; Current Account No#: <b>00661005166475</b><br />
                                &nbsp;&nbsp;&nbsp; Branch: Shahdin Manzil, Lahore.
                            </p>
                            <p style="color: black; font-size: 13pt;">
                                <b>3) Habib Bank Limited</b><br />
                                &nbsp;&nbsp;&nbsp; Account Title:  <span style="color: red">EAST LAW (Pvt.) Ltd</span><br />
                                &nbsp;&nbsp;&nbsp; Current Account No#: <b>05547900884503</b><br />
                                &nbsp;&nbsp;&nbsp; Branch: Nisbet Road, Lahore.
                            </p>
                            <p style="color: black; font-size: 13pt">
                                <b>4) Payment via Demand Draft/PO/Cross Cheque payable</b> to <b>East Law (Pvt.) Ltd</b>.
     Please send the payment instrument via registered post to our office at:
                                <br />
            EastLaw (Pvt.) Ltd.<br />
            Building No. 39 - Link Farid Kot Road,<br />
            Lahore, Pakistan.<br>
            Tel#: 042-37311670

                            </p>
                            <p style="color: black; font-size: 13pt;display:none">
                                <b>4) Payment via EasyPaisa:</b> You can also make payment using Telenor EasyPaisa. Please select
    EasyPaisa Payment option in My Subscription at www.eastlaw.pk.


                            </p>
                                
                                
                                
                                <br />
                                Your Account will be made Active within 24 Hrs of receiving the Payment Confirmation. Please feel free to contact us for any further information that you may require.<%-- of receiving the Payment Confirmation.--%>
                                <br />
                                <br />
                                <%--For any queries, please feel free to send us an email at <a href="mailto:info@eastlaw.pk" target="_blank">info@eastlaw.pk</a>.--%>
                                <br />
                                <p>
                                    
        Thank you,<br />
        <i> Helpine#: 0304-1116670</i><br />
                                    <i>WhatsApp Number: 0304-1116670</i><br />
                                    <i>( Mon to Sat - 10:00am – 6:00pm )</i><br />
                                    <i>(Saturday - 10:00am – 3:00pm )</i>
        <span style="color:red"><b>EastLaw (Pvt) Ltd.</b></span><br />
        Lahore, Pakistan.<br />
        
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
       

                            
                            
                        </div>
                        <div style="float:left;width:25%;text-align:center;background-color:wheat">
<h3 style="color:blue">Order #</h3>
                            <asp:Label ID="lblInvNo" runat="server" Font-Size="Large"></asp:Label>
                            <hr />
                            <span style="font-weight:bold;color:red;padding-bottom:2px">
                                <asp:Label ID="lblPaymentStatus" runat="server" ></asp:Label>
                                </span>
                            
                              
                        </div>
                        </div>
                     
                    </div>
            	
               
            </div>
            
		
       
         
			            
       
        
        
    </div>
        
        </div>
        
        
        
        
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    </asp:Content>